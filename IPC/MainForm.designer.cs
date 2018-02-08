using System.Windows.Forms;
using System;
using System.IO;
namespace IPC
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip_mainMenu = new System.Windows.Forms.MenuStrip();
            this.menuItem_File = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem_File_Open = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem_File_Save = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem_File_SaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem_File_Exit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem_View = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem_Camera = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem_Camera_Add = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem_Camera_Edit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem_Camera_Delete = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem_Record = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem_Record_Settings = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem_Help = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem_Help_About = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog_IpcFile = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog_IpcFile = new System.Windows.Forms.SaveFileDialog();
            this.pictureBox_RecordButton = new System.Windows.Forms.PictureBox();
            this.mainCameraGrid = new IPC.Camera.Components.CameraViewerGrid(this.components);
            this.menuStrip_mainMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_RecordButton)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip_mainMenu
            // 
            this.menuStrip_mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItem_File,
            this.menuItem_View,
            this.menuItem_Camera,
            this.menuItem_Record,
            this.menuItem_Help});
            this.menuStrip_mainMenu.Location = new System.Drawing.Point(0, 0);
            this.menuStrip_mainMenu.Name = "menuStrip_mainMenu";
            this.menuStrip_mainMenu.Size = new System.Drawing.Size(784, 24);
            this.menuStrip_mainMenu.TabIndex = 3;
            this.menuStrip_mainMenu.Text = "Main Menu";
            // 
            // menuItem_File
            // 
            this.menuItem_File.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.menuItem_File.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItem_File_Open,
            this.menuItem_File_Save,
            this.menuItem_File_SaveAs,
            this.menuItem_File_Exit});
            this.menuItem_File.Name = "menuItem_File";
            this.menuItem_File.Size = new System.Drawing.Size(37, 20);
            this.menuItem_File.Text = "&File";
            // 
            // menuItem_File_Open
            // 
            this.menuItem_File_Open.Name = "menuItem_File_Open";
            this.menuItem_File_Open.Size = new System.Drawing.Size(114, 22);
            this.menuItem_File_Open.Text = "Open";
            this.menuItem_File_Open.Click += new System.EventHandler(this.menuItem_File_Open_Click);
            // 
            // menuItem_File_Save
            // 
            this.menuItem_File_Save.Name = "menuItem_File_Save";
            this.menuItem_File_Save.Size = new System.Drawing.Size(114, 22);
            this.menuItem_File_Save.Text = "Save";
            this.menuItem_File_Save.Click += new System.EventHandler(this.menuItem_File_Save_Click);
            // 
            // menuItem_File_SaveAs
            // 
            this.menuItem_File_SaveAs.Name = "menuItem_File_SaveAs";
            this.menuItem_File_SaveAs.Size = new System.Drawing.Size(114, 22);
            this.menuItem_File_SaveAs.Text = "Save As";
            this.menuItem_File_SaveAs.Click += new System.EventHandler(this.menuItem_File_SaveAs_Click);
            // 
            // menuItem_File_Exit
            // 
            this.menuItem_File_Exit.Name = "menuItem_File_Exit";
            this.menuItem_File_Exit.Size = new System.Drawing.Size(114, 22);
            this.menuItem_File_Exit.Text = "Exit";
            this.menuItem_File_Exit.Click += new System.EventHandler(this.menuItem_File_Exit_Click);
            // 
            // menuItem_View
            // 
            this.menuItem_View.Name = "menuItem_View";
            this.menuItem_View.Size = new System.Drawing.Size(44, 20);
            this.menuItem_View.Text = "&View";
            // 
            // menuItem_Camera
            // 
            this.menuItem_Camera.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItem_Camera_Add,
            this.menuItem_Camera_Edit,
            this.menuItem_Camera_Delete});
            this.menuItem_Camera.Name = "menuItem_Camera";
            this.menuItem_Camera.Size = new System.Drawing.Size(60, 20);
            this.menuItem_Camera.Text = "&Camera";
            // 
            // menuItem_Camera_Add
            // 
            this.menuItem_Camera_Add.Name = "menuItem_Camera_Add";
            this.menuItem_Camera_Add.Size = new System.Drawing.Size(107, 22);
            this.menuItem_Camera_Add.Text = "&Add";
            this.menuItem_Camera_Add.Click += new System.EventHandler(this.menuItem_Camera_Add_Click);
            // 
            // menuItem_Camera_Edit
            // 
            this.menuItem_Camera_Edit.Name = "menuItem_Camera_Edit";
            this.menuItem_Camera_Edit.Size = new System.Drawing.Size(107, 22);
            this.menuItem_Camera_Edit.Text = "&Edit";
            this.menuItem_Camera_Edit.Click += new System.EventHandler(this.menuItem_Camera_Edit_Click);
            // 
            // menuItem_Camera_Delete
            // 
            this.menuItem_Camera_Delete.Name = "menuItem_Camera_Delete";
            this.menuItem_Camera_Delete.Size = new System.Drawing.Size(107, 22);
            this.menuItem_Camera_Delete.Text = "Delete";
            this.menuItem_Camera_Delete.Click += new System.EventHandler(this.menuItem_Camera_Delete_Click);
            // 
            // menuItem_Record
            // 
            this.menuItem_Record.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItem_Record_Settings});
            this.menuItem_Record.Name = "menuItem_Record";
            this.menuItem_Record.Size = new System.Drawing.Size(56, 20);
            this.menuItem_Record.Text = "&Record";
            // 
            // menuItem_Record_Settings
            // 
            this.menuItem_Record_Settings.Name = "menuItem_Record_Settings";
            this.menuItem_Record_Settings.Size = new System.Drawing.Size(116, 22);
            this.menuItem_Record_Settings.Text = "Settings";
            this.menuItem_Record_Settings.Click += new System.EventHandler(this.menuItem_Record_Settings_Click);
            // 
            // menuItem_Help
            // 
            this.menuItem_Help.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItem_Help_About});
            this.menuItem_Help.Name = "menuItem_Help";
            this.menuItem_Help.Size = new System.Drawing.Size(44, 20);
            this.menuItem_Help.Text = "&Help";
            // 
            // menuItem_Help_About
            // 
            this.menuItem_Help_About.Name = "menuItem_Help_About";
            this.menuItem_Help_About.Size = new System.Drawing.Size(107, 22);
            this.menuItem_Help_About.Text = "About";
            this.menuItem_Help_About.Click += new System.EventHandler(this.menuItem_Help_About_Click);
            // 
            // openFileDialog_IpcFile
            // 
            this.openFileDialog_IpcFile.FileName = "openFileDialog_IpcFile";
            this.openFileDialog_IpcFile.Filter = "CAMit Files|*.IPC";
            this.openFileDialog_IpcFile.RestoreDirectory = true;
            // 
            // saveFileDialog_IpcFile
            // 
            this.saveFileDialog_IpcFile.DefaultExt = "ipc";
            this.saveFileDialog_IpcFile.Filter = "CAMit Files|*.IPC";
            this.saveFileDialog_IpcFile.RestoreDirectory = true;
            // 
            // pictureBox_RecordButton
            // 
            this.pictureBox_RecordButton.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.pictureBox_RecordButton.Image = ((System.Drawing.Image)(resources.GetObject("recbutton_off")));
            this.pictureBox_RecordButton.Location = new System.Drawing.Point(4, 27);
            this.pictureBox_RecordButton.Name = "pictureBox_RecordButton";
            this.pictureBox_RecordButton.Size = new System.Drawing.Size(32, 32);
            this.pictureBox_RecordButton.TabIndex = 4;
            this.pictureBox_RecordButton.TabStop = false;
            this.pictureBox_RecordButton.Click += new System.EventHandler(this.pictureBox_RecordButton_Click);
            this.pictureBox_RecordButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_RecordButton_MouseDown);
            // 
            // mainCameraGrid
            // 
            this.mainCameraGrid.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.mainCameraGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainCameraGrid.Location = new System.Drawing.Point(0, 24);
            this.mainCameraGrid.Name = "mainCameraGrid";
            this.mainCameraGrid.SelectedCameraViewer = null;
            this.mainCameraGrid.Size = new System.Drawing.Size(784, 538);
            this.mainCameraGrid.TabIndex = 2;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.pictureBox_RecordButton);
            this.Controls.Add(this.mainCameraGrid);
            this.Controls.Add(this.menuStrip_mainMenu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip_mainMenu;
            this.MinimumSize = new System.Drawing.Size(450, 350);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CAMit - IP Camera Viewer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.menuStrip_mainMenu.ResumeLayout(false);
            this.menuStrip_mainMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_RecordButton)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Camera.Components.CameraViewerGrid mainCameraGrid;
        private System.Windows.Forms.MenuStrip menuStrip_mainMenu;
        private System.Windows.Forms.ToolStripMenuItem menuItem_File;
        private System.Windows.Forms.ToolStripMenuItem menuItem_View;
        private System.Windows.Forms.ToolStripMenuItem menuItem_Camera;
        private System.Windows.Forms.ToolStripMenuItem menuItem_Camera_Add;
        private System.Windows.Forms.ToolStripMenuItem menuItem_Camera_Edit;
        private System.Windows.Forms.ToolStripMenuItem menuItem_Camera_Delete;
        private System.Windows.Forms.ToolStripMenuItem menuItem_File_Open;
        private System.Windows.Forms.ToolStripMenuItem menuItem_File_Save;
        private System.Windows.Forms.ToolStripMenuItem menuItem_File_SaveAs;
        private System.Windows.Forms.ToolStripMenuItem menuItem_Help;
        private System.Windows.Forms.ToolStripMenuItem menuItem_File_Exit;
        private System.Windows.Forms.OpenFileDialog openFileDialog_IpcFile;
        private System.Windows.Forms.SaveFileDialog saveFileDialog_IpcFile;
        private System.Windows.Forms.ToolStripMenuItem menuItem_Record;
        private System.Windows.Forms.ToolStripMenuItem menuItem_Record_Settings;
        private System.Windows.Forms.ToolStripMenuItem menuItem_Help_About;
        private System.Windows.Forms.PictureBox pictureBox_RecordButton;


        private void pictureBox_RecordButton_MouseDown(object sender, MouseEventArgs e)
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.pictureBox_RecordButton.Image = ((System.Drawing.Image)(resources.GetObject("recbutton_down")));
        }

        private void pictureBox_RecordButton_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(MainForm.RecordingPath)) 
            { 
                menuItem_Record_Settings_Click(null, null);
                if (!Directory.Exists(MainForm.RecordingPath)) return;
            }

            MainForm.Recording = !MainForm.Recording;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            if (MainForm.Recording)
            {
                this.pictureBox_RecordButton.Image = ((System.Drawing.Image)(resources.GetObject("recbutton_on")));
            }
            else 
            {
                this.pictureBox_RecordButton.Image = ((System.Drawing.Image)(resources.GetObject("recbutton_off")));
            }
        }
    }
}

