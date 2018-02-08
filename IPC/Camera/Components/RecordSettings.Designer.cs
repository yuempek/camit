namespace IPC.Camera.Components
{
    partial class RecordSettings
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBox_RecordingPath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.folderBrowserDialog_RecordPath = new System.Windows.Forms.FolderBrowserDialog();
            this.button_OpenFolderBrowsingDialog = new System.Windows.Forms.Button();
            this.button_RecordingPathOK = new System.Windows.Forms.Button();
            this.button_RecordingPathCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBox_RecordingPath
            // 
            this.textBox_RecordingPath.Location = new System.Drawing.Point(16, 29);
            this.textBox_RecordingPath.Name = "textBox_RecordingPath";
            this.textBox_RecordingPath.Size = new System.Drawing.Size(366, 20);
            this.textBox_RecordingPath.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Recording Path";
            // 
            // button_OpenFolderBrowsingDialog
            // 
            this.button_OpenFolderBrowsingDialog.Location = new System.Drawing.Point(388, 27);
            this.button_OpenFolderBrowsingDialog.Name = "button_OpenFolderBrowsingDialog";
            this.button_OpenFolderBrowsingDialog.Size = new System.Drawing.Size(30, 23);
            this.button_OpenFolderBrowsingDialog.TabIndex = 2;
            this.button_OpenFolderBrowsingDialog.Text = "...";
            this.button_OpenFolderBrowsingDialog.UseVisualStyleBackColor = true;
            this.button_OpenFolderBrowsingDialog.Click += new System.EventHandler(this.button_OpenFolderBrowsingDialog_Click);
            // 
            // button_RecordingPathOK
            // 
            this.button_RecordingPathOK.Location = new System.Drawing.Point(342, 59);
            this.button_RecordingPathOK.Name = "button_RecordingPathOK";
            this.button_RecordingPathOK.Size = new System.Drawing.Size(75, 23);
            this.button_RecordingPathOK.TabIndex = 3;
            this.button_RecordingPathOK.Text = "OK";
            this.button_RecordingPathOK.UseVisualStyleBackColor = true;
            this.button_RecordingPathOK.Click += new System.EventHandler(this.button_RecordingPathOK_Click);
            // 
            // button_RecordingPathCancel
            // 
            this.button_RecordingPathCancel.Location = new System.Drawing.Point(261, 59);
            this.button_RecordingPathCancel.Name = "button_RecordingPathCancel";
            this.button_RecordingPathCancel.Size = new System.Drawing.Size(75, 23);
            this.button_RecordingPathCancel.TabIndex = 3;
            this.button_RecordingPathCancel.Text = "Cancel";
            this.button_RecordingPathCancel.UseVisualStyleBackColor = true;
            this.button_RecordingPathCancel.Click += new System.EventHandler(this.button_RecordingPathCancel_Click);
            // 
            // RecordSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(435, 94);
            this.Controls.Add(this.button_RecordingPathCancel);
            this.Controls.Add(this.button_RecordingPathOK);
            this.Controls.Add(this.button_OpenFolderBrowsingDialog);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox_RecordingPath);
            this.Name = "RecordSettings";
            this.Text = "Record Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox_RecordingPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog_RecordPath;
        private System.Windows.Forms.Button button_OpenFolderBrowsingDialog;
        private System.Windows.Forms.Button button_RecordingPathOK;
        private System.Windows.Forms.Button button_RecordingPathCancel;
    }
}