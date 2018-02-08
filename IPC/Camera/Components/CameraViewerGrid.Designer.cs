using System.Windows.Forms;
namespace IPC.Camera.Components
{
    partial class CameraViewerGrid
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // CameraViewerGrid
            // 
            this.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.Click += new System.EventHandler(this.CameraViewerGrid_Click);
            this.ControlAdded += new System.Windows.Forms.ControlEventHandler(this.CameraViewerGrid_ControlAdded);
            this.Resize += new System.EventHandler(this.CameraViewerGrid_Resize);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
