using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using iSpyApplication.Video;
using AForge.Video;
using System.Drawing.Imaging;
using AForge;
using System.Threading;
using IPC.Camera;
using IPC.Camera.Components;
using System.Xml.Serialization;
using System.IO;
using System.Linq;

namespace IPC
{
    public partial class MainForm : Form
    {
        private static bool _logging = true;
        public static readonly StringBuilder LogFile = new StringBuilder(100000);
        public static int ThreadKillDelay = 10000;
        public static bool Reallyclose = false;
        public static String LastOpenedFile = null;
        public static List<IPCFileFormat.CamerasCamera> CameraList = new List<IPCFileFormat.CamerasCamera>();
        public static String RecordingPath = null;
        public static bool Recording = false;
        public static String ApplicationConfigFile = Path.Combine(Application.StartupPath, "configs.ini");
//        public static IPCFileFormat.Cameras CameraList;
    
        public MainForm(string IPCFilePath)
        {
            InitializeComponent();
            OpenIPCFile(IPCFilePath);
            Initialize();
        }

        public MainForm()
        {
            InitializeComponent();
            Initialize();
        }

        internal static void LogExceptionToFile(Exception ex, string info)
        {
	        ex.HelpLink = info + ": " + ex.Message;
	        MainForm.LogExceptionToFile(ex);
        }
        internal static void LogExceptionToFile(Exception ex)
        {
            if (!MainForm._logging)
            {
                return;
            }
            try
            {
                string text = string.Concat(new object[]
		                                                {
			                                                ex.HelpLink,
			                                                "<br/>",
			                                                ex.Message,
			                                                "<br/>",
			                                                ex.Source,
			                                                "<br/>",
			                                                ex.StackTrace,
			                                                "<br/>",
			                                                ex.InnerException,
			                                                "<br/>",
			                                                ex.Data
		                                                }
                );
                MainForm.LogFile.Append(string.Concat(new string[]
		                                                        {
			                                                        "<tr><td style=\"color:red\" valign=\"top\">Exception:</td><td valign=\"top\">",
			                                                        DateTime.Now.ToLongTimeString(),
			                                                        "</td><td valign=\"top\">",
			                                                        text,
			                                                        "</td></tr>"
		                                                        }
                ));
            }
            catch
            {
            }
        }
        internal static void LogErrorToFile(string message, string e)
        {
            MainForm.LogErrorToFile(string.Format(message, e));
        }
        internal static void LogErrorToFile(string message)
        {
            if (!MainForm._logging)
            {
                return;
            }
            try
            {
                MainForm.LogFile.Append(string.Concat(new string[]
		{
			"<tr><td style=\"color:red\" valign=\"top\">Error</td><td valign=\"top\">",
			DateTime.Now.ToLongTimeString(),
			"</td><td valign=\"top\">",
			message,
			"</td></tr>"
		}));
            }
            catch
            {
            }
        }

        private void Initialize() 
        {
            RecordingPath = INI.IniFile.ReadValue(ApplicationConfigFile, "main", "RecordPath");
            if (!Directory.Exists(RecordingPath)) RecordingPath = "";
        }

        public void OpenIPCFile(String IPCFilePath) 
        {
            if (File.Exists(IPCFilePath))
            {
                LoadIPCFile(IPCFilePath);
            }

            mainCameraGrid.DisposeAllCameras();

            foreach (IPCFileFormat.CamerasCamera camera in CameraList)
            {
                String UserName = camera.UserName;
                String Password = camera.Password;
                String CameraName = camera.Name;
                String CameraIp = camera.Ip;
                String sourceURL = camera.ModelInfo.SourceUrl;
                CameraConnectionString.ReplaceAllPattern(ref sourceURL, UserName, Password, CameraIp, "", "", "", "");

                switch (camera.ModelInfo.StreamType)
                {
                    case "JPEG":
                        mainCameraGrid.AddStreamCamera(new StreamCamera(new JPEGStream2(sourceURL), CameraName) { IPCFormatCamera = camera});
                        break;
                    case "MJPEG":
                        mainCameraGrid.AddStreamCamera(new StreamCamera(new MJPEGStream2(sourceURL), CameraName) { IPCFormatCamera = camera });
                        break;
                    case "MPEG":
                        mainCameraGrid.AddStreamCamera(new StreamCamera(new FFMPEGStream(sourceURL), CameraName) { IPCFormatCamera = camera });
                        break;
                    default: break;
                }
            }

            mainCameraGrid.StartAll();
        }
        
        public void LoadIPCFile(string fileName)
        {
            try
            {
                var s = new XmlSerializer(typeof(IPCFileFormat.Cameras));
                IPCFileFormat.Cameras c;
                using (var fs = new FileStream(fileName, FileMode.Open))
                {
                    fs.Position = 0;
                    using (TextReader reader = new StreamReader(fs))
                    {
                        c = (IPCFileFormat.Cameras)s.Deserialize(reader);
                        reader.Close();
                    }
                    fs.Close();
                }
                CameraList = c.Camera != null ? c.Camera.ToList() : new List<IPCFileFormat.CamerasCamera>();
                LastOpenedFile = fileName;
            }
            catch (Exception)
            {
                MessageBox.Show("The file couldn't be opened. File may be damaged or wrong.");
            }
        }

        public void SaveIPCFile(string fileName)
        {
            var s = new XmlSerializer(typeof(IPCFileFormat.Cameras));
            using (var fs = new FileStream(fileName, FileMode.Create))
            {
                using (var writer = new StreamWriter(fs))
                {
                    fs.Position = 0;
                    s.Serialize(writer, new IPCFileFormat.Cameras() { Camera = CameraList.ToArray() });
                    writer.Close();
                }
                fs.Close();
            }
            LastOpenedFile = fileName;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            MainForm.Reallyclose = true;
            MainForm.Recording = false;
            Thread.Sleep(2000);
        }

        private void menuItem_Camera_Add_Click(object sender, EventArgs e)
        {
            CreateCamera cameraCreator = new CreateCamera();
            DialogResult result = (cameraCreator).ShowDialog();
            if(result == DialogResult.OK){
                //TODO
                IPCFileFormat.CamerasCamera IPCFormatCamera;
                IPCFormatCamera = new IPCFileFormat.CamerasCamera();
                IPCFormatCamera.ModelInfo = new IPCFileFormat.CamerasCameraModelInfo();
                IPCFormatCamera.ModelInfo.Controls = new IPCFileFormat.CamerasCameraModelInfoControls();
                IPCFormatCamera.ModelInfo.Controls.StandartControls = new IPCFileFormat.CamerasCameraModelInfoControlsStandartControls();
                IPCFormatCamera.ModelInfo.Controls.StandartControls.Up = new IPCFileFormat.CamerasCameraModelInfoControlsStandartControlsUP();
                IPCFormatCamera.ModelInfo.Controls.StandartControls.Down = new IPCFileFormat.CamerasCameraModelInfoControlsStandartControlsDown();
                IPCFormatCamera.ModelInfo.Controls.StandartControls.Left = new IPCFileFormat.CamerasCameraModelInfoControlsStandartControlsLeft();
                IPCFormatCamera.ModelInfo.Controls.StandartControls.Right = new IPCFileFormat.CamerasCameraModelInfoControlsStandartControlsRight();
                IPCFormatCamera.ModelInfo.Controls.StandartControls.ZoomIn = new IPCFileFormat.CamerasCameraModelInfoControlsStandartControlsZoomIn();
                IPCFormatCamera.ModelInfo.Controls.StandartControls.ZoomOut = new IPCFileFormat.CamerasCameraModelInfoControlsStandartControlsZoomOut();

                IPCFormatCamera.ID = DateTime.Now.Millisecond;
                IPCFormatCamera.Ip = cameraCreator.CameraIP;
                IPCFormatCamera.Name = cameraCreator.CameraName;
                IPCFormatCamera.Order = mainCameraGrid.CameraViewerList.Count + 1;
                IPCFormatCamera.Password = cameraCreator.LoginPassword;
                IPCFormatCamera.UserName = cameraCreator.LoginName;
                IPCFormatCamera.ModelInfo.Model = (cameraCreator.SelectedCameraModel == null ? "Undefined Model" : cameraCreator.SelectedCameraModel.Name);
                IPCFormatCamera.ModelInfo.SourceUrl = cameraCreator.CameraSourceAddress;
                IPCFormatCamera.ModelInfo.StreamType = cameraCreator.SelectedCameraType.ToString();
                IPCFormatCamera.ModelInfo.Controls.StandartControls.Up.StartEvent = cameraCreator.TiltUpStartCommand;
                IPCFormatCamera.ModelInfo.Controls.StandartControls.Up.StopEvent = cameraCreator.TiltUpStopCommand;
                IPCFormatCamera.ModelInfo.Controls.StandartControls.Down.StartEvent = cameraCreator.TiltDownStartCommand;
                IPCFormatCamera.ModelInfo.Controls.StandartControls.Down.StopEvent = cameraCreator.TiltDownStopCommand;
                IPCFormatCamera.ModelInfo.Controls.StandartControls.Left.StartEvent = cameraCreator.PanLeftStartCommand;
                IPCFormatCamera.ModelInfo.Controls.StandartControls.Left.StopEvent = cameraCreator.PanLeftStopCommand;
                IPCFormatCamera.ModelInfo.Controls.StandartControls.Right.StartEvent = cameraCreator.PanRightStartCommand;
                IPCFormatCamera.ModelInfo.Controls.StandartControls.Right.StopEvent = cameraCreator.PanRightStopCommand;
                IPCFormatCamera.ModelInfo.Controls.StandartControls.ZoomIn.StartEvent = cameraCreator.ZoomInStartCommand;
                IPCFormatCamera.ModelInfo.Controls.StandartControls.ZoomIn.StopEvent = cameraCreator.ZoomInStopCommand;
                IPCFormatCamera.ModelInfo.Controls.StandartControls.ZoomOut.StartEvent = cameraCreator.ZoomOutStartCommand;
                IPCFormatCamera.ModelInfo.Controls.StandartControls.ZoomOut.StopEvent = cameraCreator.ZoomOutStopCommand;
                IPCFormatCamera.Stamps = new IPCFileFormat.CamerasCameraStamp[] { new IPCFileFormat.CamerasCameraStamp() { position = 3, active = true, showDate = true, showName = true } };

                cameraCreator.Camera.IPCFormatCamera = IPCFormatCamera;
                CameraList.Add(IPCFormatCamera);
                mainCameraGrid.AddStreamCamera(cameraCreator.Camera);
                cameraCreator.Camera.Start();
            }
        }

        private void menuItem_File_SaveAs_Click(object sender, EventArgs e)
        {
            if (saveFileDialog_IpcFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                SaveIPCFile(saveFileDialog_IpcFile.FileName);
            }
        }

        private void menuItem_File_Open_Click(object sender, EventArgs e)
        {
            if (openFileDialog_IpcFile.ShowDialog() == System.Windows.Forms.DialogResult.OK) 
            {
                OpenIPCFile(openFileDialog_IpcFile.FileName);
            }
        }

        private void menuItem_File_Save_Click(object sender, EventArgs e)
        {
            if (LastOpenedFile != null)
            {
                SaveIPCFile(LastOpenedFile);
            }
            else
            {
                menuItem_File_SaveAs_Click(sender, e);
            }
        }

        private void menuItem_Camera_Delete_Click(object sender, EventArgs e)
        {
            if (mainCameraGrid.SelectedCameraViewer == null) 
            {
                MessageBox.Show("Please select a camera from grid;");
                return;
            }
            CameraViewer cv = mainCameraGrid.SelectedCameraViewer;
            if (MessageBox.Show("Selected Camera("+cv.Camera.Name+") will be deleted.\nDo you confirm?","Delete Camera",MessageBoxButtons.OKCancel,MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.OK)
            {
                CameraList.Remove(cv.Camera.IPCFormatCamera);
                mainCameraGrid.DisposeCameraViever(cv);
            }
        }

        private void menuItem_Camera_Edit_Click(object sender, EventArgs e)
        {

        }

        private void menuItem_File_Exit_Click(object sender, EventArgs e)
        {

        }

        private void menuItem_Help_About_Click(object sender, EventArgs e)
        {

        }

        private void menuItem_Record_Settings_Click(object sender, EventArgs e)
        {
            RecordSettings r = new RecordSettings();
            r.RecordingPath = RecordingPath;
            if (r.ShowDialog() == System.Windows.Forms.DialogResult.OK) 
            {
                RecordingPath = r.RecordingPath;
                INI.IniFile.WriteValue(ApplicationConfigFile, "main", "RecordPath", RecordingPath);
            }
            r.Dispose();
            r = null;
        }




    }
}
