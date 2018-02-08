using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Drawing;
using System.Runtime.Serialization;

namespace IPC.Camera.Components
{
    public partial class CameraViewerGrid : Control
    {
        private List<CameraViewer> cameraViewerList = null;
        private CameraViewer selectedCameraViwer = null;
        public CameraViewer SelectedCameraViewer 
        {
            get { return selectedCameraViwer; }
            set {
                if (selectedCameraViwer != null)
                {
                    selectedCameraViwer.Selected = false;
                }
                selectedCameraViwer = value;
                if(value != null) selectedCameraViwer.Selected = true;
                ReDrawCameraGrid();
            }
        }


        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<CameraViewer> CameraViewerList
        {
            get { return cameraViewerList; }
            set 
            {
                //dispose all exist childs
                if (cameraViewerList != null) {
                    this.SuspendLayout();
                    foreach (CameraViewer viewer in cameraViewerList)
                    {
                        viewer.Dispose();
                    }
                    this.ResumeLayout();
                    cameraViewerList.Clear();
                    cameraViewerList = null;
                }

                cameraViewerList = value;
                if (cameraViewerList == null) cameraViewerList = new List<CameraViewer>();
                ReDrawCameraGrid();
            }
        }

        public CameraViewerGrid()
        {
            this.CameraViewerList = new List<CameraViewer>();
            InitializeComponent();
        }

        public CameraViewerGrid(IContainer container)
        {
            container.Add(this);
            this.CameraViewerList = new List<CameraViewer>();
            InitializeComponent();
        }

        public void StartAll() 
        {
            foreach (CameraViewer viewer in cameraViewerList)
            {
                viewer.Camera.Start();
            }
        }

        public void ReDrawCameraGrid()
        {
            Monitor.Enter(this);

            double usableAreaPercentage = 0.98;
            double cameraRatio = 4.0 / 3.0;   //  Camera width / Camera height  
            int noc = cameraViewerList.Count; //  number of camera
            int col = 0, row = 0;

            if (noc != 0)
            {
                col = Convert.ToInt32(Math.Ceiling(Math.Sqrt(noc)));
                row = Convert.ToInt32(Math.Ceiling(noc * 1.0 / col));
            }

            int width = this.ClientRectangle.Width;
            int height = this.ClientRectangle.Height;

            int viewerWidth = 0;
            int viewerHeight = 0;

            if (col != 0)
            {
                viewerWidth = Math.Min(Convert.ToInt32(width * usableAreaPercentage)  / col, Convert.ToInt32(cameraRatio * height * usableAreaPercentage) / row);
                viewerHeight = Convert.ToInt32(viewerWidth / cameraRatio);
            }

            int startPosX = (width - col * viewerWidth) / 2;
            int startPosY = (height - row * viewerHeight) / 2;

            int i = 0;
            int paddingX = viewerWidth * 3 / 100 / 2;
            int paddingY = viewerHeight * 3 / 100 / 2;
            this.SuspendLayout();
            foreach (CameraViewer viewer in CameraViewerList)
            {
                viewer.Location = new Point(startPosX + viewerWidth * (i % col) + paddingX, startPosY + viewerHeight * (i / col) + paddingY);
                viewer.Size = new Size(viewerWidth - paddingX * 2, viewerHeight - paddingY * 2);
                i++;
            }

            if (selectedCameraViwer != null) {
                selectedCameraViwer.BringToFront();
                int xmargin = selectedCameraViwer.Size.Width * 20 / 100 / 2;
                int ymargin = selectedCameraViwer.Size.Height * 20 / 100 / 2;
                selectedCameraViwer.Location = new Point(selectedCameraViwer.Location.X - xmargin, selectedCameraViwer.Location.Y - ymargin);
                selectedCameraViwer.Size = new Size(selectedCameraViwer.Size.Width + 2*xmargin, selectedCameraViwer.Size.Height + 2*ymargin);
            }

            this.ResumeLayout();
            Monitor.Exit(this);
        }

        public void AddCameraViewer(CameraViewer cameraViewer)
        {
            if (cameraViewer != null)
            {
                cameraViewer.Click += new EventHandler(cameraViewer_Click);
                this.Controls.Add(cameraViewer);
                this.CameraViewerList.Add(cameraViewer);
            }
            ReDrawCameraGrid();
        }

        public void DisposeAllCameras() 
        {
            foreach (CameraViewer cameraViewer in this.CameraViewerList)
            {
                cameraViewer.Click -= new EventHandler(cameraViewer_Click);
                this.Controls.Remove(cameraViewer);
                cameraViewer.Camera.ReconnectWhenError = false;
                cameraViewer.Camera.Stop();
                cameraViewer.Camera = null;
                cameraViewer.Dispose();
            }
            this.CameraViewerList.Clear();
            ReDrawCameraGrid();
        }

        public void DisposeCameraViever(CameraViewer cameraViewer)
        {
            cameraViewer.Click -= new EventHandler(cameraViewer_Click);
            this.Controls.Remove(cameraViewer);
            cameraViewer.Camera.ReconnectWhenError = false;
            cameraViewer.Camera.Stop();
            cameraViewer.Camera = null;
            cameraViewer.Dispose();
            this.CameraViewerList.Remove(cameraViewer);
            ReDrawCameraGrid();
        }

        private void cameraViewer_Click(object sender, EventArgs e)
        {
            this.SelectedCameraViewer = (CameraViewer)sender;
        }

        public void AddStreamCamera(StreamCamera scamera) 
        {
            AddCameraViewer(new CameraViewer(){Camera = scamera});
        }

        private void CameraViewerGrid_Resize(object sender, System.EventArgs e)
        {
            ReDrawCameraGrid();
        }

        private void CameraViewerGrid_Click(object sender, EventArgs e)
        {
            if (selectedCameraViwer != null)
            {
                selectedCameraViwer.Selected = false;
            }
            selectedCameraViwer = null;
            ReDrawCameraGrid();
        }

        private void CameraViewerGrid_ControlAdded(object sender, ControlEventArgs e)
        {
            //if (e.Control is CameraViewer) {
            //    CameraViewer cv = e.Control as CameraViewer;
            //    cv.Selected = false;
            //    selectedCameraViwer = null;
            //    ReDrawCameraGrid();
            //}
            this.SelectedCameraViewer = null;
        }
    }
}
