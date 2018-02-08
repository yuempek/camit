using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using IPC.Collections;
using AForge.Video;
using iSpyApplication.Video;

namespace IPC.Camera.Components
{
    public partial class CreateCamera : Form
    {
        public enum Position
        {
            LeftTop,
            LeftMiddle,
            LeftBottom,
            CenterTop,
            CenterMiddle,
            CenterBottom,
            RightTop,
            RightMiddle,
            RightBottom
        };

        public StreamCamera.StreamType SelectedCameraType;
        public CameraModel SelectedCameraModel;
        public StreamCamera Camera;
        public Position LabelPosition = Position.RightTop;
        public String CameraName;
        public String CameraIP;
        public String LoginName;
        public String LoginPassword;
        public String CameraSourceAddress;
        public String PanLeftStartCommand;
        public String PanLeftStopCommand;
        public String PanRightStartCommand;
        public String PanRightStopCommand;
        public String TiltDownStartCommand;
        public String TiltDownStopCommand;
        public String TiltUpStartCommand;
        public String TiltUpStopCommand;
        public String ZoomInStartCommand;
        public String ZoomInStopCommand;
        public String ZoomOutStartCommand;
        public String ZoomOutStopCommand;


        public CreateCamera()
        {
            InitializeComponent();
            comboBox_cameraModel.Items.Add("----Create New Model----");
            foreach (CameraModel cameraModel in CameraModel.CameraModels)
            {
                comboBox_cameraModel.Items.Add(new ComboboxItem<CameraModel>(cameraModel.Name, cameraModel));
            }
            comboBox_cameraModel.SelectedIndex = 0;
            comboBox_CameraType.Items.Add(new ComboboxItem<StreamCamera.StreamType>("JPEG Source", StreamCamera.StreamType.JPEG));
            comboBox_CameraType.Items.Add(new ComboboxItem<StreamCamera.StreamType>("MJPEG Source", StreamCamera.StreamType.MJPEG));
            comboBox_CameraType.Items.Add(new ComboboxItem<StreamCamera.StreamType>("MPEG Source(rtsp, h264)", StreamCamera.StreamType.MPEG));
        }

        private void comboBox_cameraModel_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            if (cb.SelectedIndex != 0) {
                CameraModel cm =((ComboboxItem<CameraModel>)cb.SelectedItem).value;
                this.SelectedCameraModel = cm;
                foreach (ComboboxItem<StreamCamera.StreamType> item in comboBox_CameraType.Items)
	            {//TODO
                    if (StreamCamera.StreamType.JPEG == item.value)
                    {
                        comboBox_CameraType.SelectedItem = item;
                    }
	            } 
                textBox_CameraSourceAddress.Text = cm.Config.NetworkCameraJPEGimage;

                textBox_PanLeftStartCommand.Text = cm.Config.NetworkCamPanLeft;
                textBox_PanLeftStopCommand.Text = cm.Config.NetworkCamPanLeft_Up;
                textBox_PanRightStartCommand.Text = cm.Config.NetworkCamPanRight;
                textBox_PanRightStopCommand.Text = cm.Config.NetworkCamPanRight_Up;
                textBox_TiltDownStartCommand.Text = cm.Config.NetworkCamTiltDown;
                textBox_TiltDownStopCommand.Text = cm.Config.NetworkCamTiltDown_Up;
                textBox_TiltUpStartCommand.Text = cm.Config.NetworkCamTiltUp;
                textBox_TiltUpStopCommand.Text = cm.Config.NetworkCamTiltUp_Up;
                textBox_ZoomInStartCommand.Text = cm.Config.NetworkCamZoomIN;
                textBox_ZoomInStopCommand.Text = cm.Config.NetworkCamZoomIN_Up;
                textBox_ZoomOutStartCommand.Text = cm.Config.NetworkCamZoomOut;
                textBox_ZoomOutStopCommand.Text = cm.Config.NetworkCamZoomOut_Up;
            }
            else
            {
                this.SelectedCameraModel = null;
            }
        }

        private String ReplacePattersIn(string command) 
        {
            String c = command;

            CameraConnectionString.ReplaceUserNamePattern(ref c, this.LoginName);
            CameraConnectionString.ReplacePasswordPattern(ref c, this.LoginPassword);
            CameraConnectionString.ReplaceIPPattern(ref c, this.CameraIP);
            CameraConnectionString.ReplaceFPSPattern(ref c, "");
            CameraConnectionString.ReplaceImageSizePattern(ref c, "");
            CameraConnectionString.ReplaceXPattern(ref c, "");
            CameraConnectionString.ReplaceYPattern(ref c, "");

            return c;
        }
 
        private void fillAllVariable() 
        {
            this.LoginName = textBox_SourceUserName.Text;
            this.LoginPassword = textBox_SourcePassword.Text;
            this.CameraName = textBox_CameraName.Text;
            this.CameraIP = textBox_CameraIP.Text;

            this.CameraSourceAddress = ReplacePattersIn(textBox_CameraSourceAddress.Text);

            this.PanLeftStartCommand = ReplacePattersIn(textBox_PanLeftStartCommand.Text);
            this.PanLeftStopCommand = ReplacePattersIn(textBox_PanLeftStopCommand.Text);
            this.PanRightStartCommand = ReplacePattersIn(textBox_PanRightStartCommand.Text);
            this.PanRightStopCommand = ReplacePattersIn(textBox_PanRightStopCommand.Text);
            this.TiltDownStartCommand = ReplacePattersIn(textBox_TiltDownStartCommand.Text);
            this.TiltDownStopCommand = ReplacePattersIn(textBox_TiltDownStopCommand.Text);
            this.TiltUpStartCommand = ReplacePattersIn(textBox_TiltUpStartCommand.Text);
            this.TiltUpStopCommand = ReplacePattersIn(textBox_TiltUpStopCommand.Text);
            this.ZoomInStartCommand = ReplacePattersIn(textBox_ZoomInStartCommand.Text);
            this.ZoomInStopCommand = ReplacePattersIn(textBox_ZoomInStopCommand.Text);
            this.ZoomOutStartCommand = ReplacePattersIn(textBox_ZoomOutStartCommand.Text);
            this.ZoomOutStopCommand = ReplacePattersIn(textBox_ZoomOutStopCommand.Text);

            if (this.Camera != null)
            {
                this.Camera.ReconnectWhenError = false;
                this.Camera.Stop();
                this.Camera = null;
            }
            this.Camera = null;
            switch (SelectedCameraType)
            {
                case StreamCamera.StreamType.JPEG:
                    this.Camera = new StreamCamera(new JPEGStream2(CameraSourceAddress), CameraName);
                    break;
                case StreamCamera.StreamType.MJPEG:
                    this.Camera = new StreamCamera(new MJPEGStream2(CameraSourceAddress), CameraName);
                    break;
                case StreamCamera.StreamType.MPEG:
                    this.Camera = new StreamCamera(new FFMPEGStream(CameraSourceAddress), CameraName);
                    break;
                default:
                    break;
            }
        }

        private void button_CreateCameraViewer_Click(object sender, EventArgs e)
        {
            if(!ControlBeforeAction()) return;
            fillAllVariable();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button_SaveAsCameraModel_Click(object sender, EventArgs e)
        {

        }

        private bool ControlBeforeAction() 
        {
            if (String.IsNullOrEmpty(comboBox_CameraType.Text))
            {
                MessageBox.Show("Please select the stream type of the camera!");
                return false;
            }
            if (String.IsNullOrEmpty(textBox_CameraIP.Text))
            {
                MessageBox.Show("Please enter camera ip!");
                return false;
            }
            if (String.IsNullOrEmpty(textBox_CameraSourceAddress.Text))
            {
                MessageBox.Show("Please enter camera connection string!");
                return false;
            }

            return true;
        }

        private void comboBox_CameraType_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SelectedCameraType = ((ComboboxItem<StreamCamera.StreamType>)comboBox_CameraType.SelectedItem).value;
        }

        private void button_SaveCameraModel_Click(object sender, EventArgs e)
        {

        }

        private void button_Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void radioButton_Position_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton r = (RadioButton)sender;
            if (!r.Checked) return;

            switch (r.TabIndex)
        	{
                case 1: this.LabelPosition = Position.LeftTop; break;
                case 2: this.LabelPosition = Position.LeftMiddle; break;
                case 3: this.LabelPosition = Position.LeftBottom; break;
                case 4: this.LabelPosition = Position.CenterTop; break;
                case 5: this.LabelPosition = Position.CenterMiddle; break;
                case 6: this.LabelPosition = Position.CenterBottom; break;
                case 7: this.LabelPosition = Position.RightTop; break;
                case 8: this.LabelPosition = Position.RightMiddle; break;
                case 9: this.LabelPosition = Position.RightBottom; break;
	    	    
                default: break;
	        }
        }

        private void CreateCamera_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void button_TestConnection_Click(object sender, EventArgs e)
        {
            if (!ControlBeforeAction()) return;
            fillAllVariable();
            cameraViewer_TestConnection.Camera = this.Camera;
            cameraViewer_TestConnection.Camera.Start();
        }
    }
}
