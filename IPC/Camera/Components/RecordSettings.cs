using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace IPC.Camera.Components
{
    public partial class RecordSettings : Form
    {

        private String recordingPath;
        public String RecordingPath
        {
            get { return recordingPath; }
            set {
                recordingPath = value;
                textBox_RecordingPath.Text = value;
            }
        }

        public RecordSettings()
        {
            InitializeComponent();
        }

        private void button_OpenFolderBrowsingDialog_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog_RecordPath.ShowDialog() == System.Windows.Forms.DialogResult.OK) 
            {
                textBox_RecordingPath.Text = folderBrowserDialog_RecordPath.SelectedPath;
            }
        }

        private void button_RecordingPathOK_Click(object sender, EventArgs e)
        {
            IntPtr pAviStream;

            if (!Directory.Exists(textBox_RecordingPath.Text)) 
            {
                MessageBox.Show("Path is not exists.");
                return;
            }

            /*
            string tmpFile = Application.StartupPath + "\\tmp.avi";
            try { File.Delete(tmpFile);}
            catch (Exception){}

            AviFile.AviManager aviManager = new AviFile.AviManager(tmpFile, false);
            AviFile.VideoStream aviStream = aviManager.AddVideoStream(true, 1, new Bitmap(1,1));
            CompressOptions = aviStream.CompressOptions;
            aviManager.Close();
 
            if (CompressOptions.fccHandler == 0)
            {
                return;
            }//*/
            
            recordingPath = textBox_RecordingPath.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button_RecordingPathCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
