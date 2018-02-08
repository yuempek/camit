using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Threading;
using System.Windows.Forms;

namespace IPC.Camera.Components
{
	/// <summary>
	/// Summary description for CameraWindow.
	/// </summary>
	public class CameraViewer : PictureBox
	{
		private StreamCamera camera = null;
        [Browsable(false)]
		public StreamCamera Camera
		{
			get { return camera; }
			set
			{
				// lock
				Monitor.Enter(this);

				// detach event
				if (camera != null)
				{
					camera.NewFrame -= new EventHandler(cameraNewFrame);
				}

				camera = value;
				

				// atach event
				if (camera != null)
				{
					camera.NewFrame += new EventHandler(cameraNewFrame);
				}

				// unlock
				Monitor.Exit(this);
			}
		}

        private bool isFullScreen = false;
        public bool IsFullScreen 
        {
            get { return isFullScreen; }
        }

        private bool selected = false;
        public bool Selected {
            get 
            {
                return selected;
            }
            set {
                selected = value;
                Invalidate();
            }
        }        

		public CameraViewer()
		{
			SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer |
				ControlStyles.ResizeRedraw | ControlStyles.UserPaint, true);

            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.Size = new System.Drawing.Size(320, 240);
            this.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.DoubleClick += new EventHandler(CameraViewer_DoubleClick);
		}

        void CameraViewer_DoubleClick(object sender, EventArgs e)
        {
            if (!IsFullScreen)
            {
                this.isFullScreen = true;
                Control oldParent = this.Parent;
                DockStyle oldDock = this.Dock;
                FullScreenCameraViewer fscv = new FullScreenCameraViewer(this);
                fscv.ShowDialog();
                oldParent.Controls.Add(this);
                this.Dock = oldDock;
                this.isFullScreen = false;
            }
        }

		protected override void OnPaint(PaintEventArgs pe)
		{
			// lock
			Monitor.Enter(this);

			Graphics	g = pe.Graphics;
			Rectangle	rc = this.ClientRectangle;
            Pen        pen = new Pen(Color.Black, 1);
            Pen        penActive = new Pen(Color.White, 2);


			if (camera != null)
			{
				camera.Lock();

				// draw frame
				if (camera.LastFrame != null)
				{
                    g.DrawImage(camera.LastFrame, rc.X + 1, rc.Y + 1, rc.Width - 2, rc.Height - 2);
                }
				else
				{
					// Create font and brush
					Font drawFont = new Font("Arial", 12);
					SolidBrush drawBrush = new SolidBrush(Color.White);

					g.DrawString("Connecting ...", drawFont, drawBrush, new PointF(5, 5));

					drawBrush.Dispose();
					drawFont.Dispose();
				}

				camera.Unlock();
			}
            // draw rectangle
            if (this.selected)
                g.DrawRectangle(penActive, rc.X, rc.Y, rc.Width - 2, rc.Height - 2);
            else
                g.DrawRectangle(pen, rc.X, rc.Y, rc.Width - 1, rc.Height - 1);

            pen.Dispose();
            penActive.Dispose();

			// unlock
			Monitor.Exit(this);

			base.OnPaint(pe);
		}

		private void cameraNewFrame(object sender, System.EventArgs e)
		{
			Invalidate();
		}
	
    }
}
