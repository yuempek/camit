using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IPC.Camera.Components;
using System.Net;
using System.Threading;

namespace IPC.Camera.Components
{
    public partial class FullScreenCameraViewer : Form
    {
        private bool mouseDown = false;
        private ControllerPanel controlPanel;
        private int mx, my, mdx, mdy;
        private CameraViewer viewer;
        private System.Windows.Forms.Timer timerPan;
        private System.Windows.Forms.Timer timerTilt;
        private System.Windows.Forms.Timer timerZoom;
        private double RPSPan = 0;
        private double RPSTilt = 0;
        private double RPSZoom = 0;
        private int RPSCoefPan = 5;
        private int RPSCoefTilt = 5;
        private int RPSCoefZoom = 5;
        private int sensitivityCoefPan = 50;
        private int sensitivityCoefTilt = 50;
        private int sensitivityCoefZoom = 1;
                
        public FullScreenCameraViewer(CameraViewer viewer)
        {
            InitializeComponent();

            this.timerPan = new System.Windows.Forms.Timer();
            this.timerTilt = new System.Windows.Forms.Timer();
            this.timerZoom = new System.Windows.Forms.Timer();

            this.timerPan.Tick += new EventHandler(timerPan_Tick);
            this.timerTilt.Tick += new EventHandler(timerTilt_Tick);
            this.timerZoom.Tick += new EventHandler(timerZoom_Tick);

            this.viewer = viewer;    
            this.SuspendLayout();
                viewer.Dock = System.Windows.Forms.DockStyle.Fill;
                viewer.MouseMove += new MouseEventHandler(FullScreenCameraViewer_MouseMove);
                viewer.MouseDown += new MouseEventHandler(FullScreenCameraViewer_MouseDown);
                viewer.MouseUp += new MouseEventHandler(FullScreenCameraViewer_MouseUp);
                viewer.MouseWheel += new MouseEventHandler(FullScreenCameraViewer_MouseWheel);
                this.MouseWheel += new MouseEventHandler(FullScreenCameraViewer_MouseWheel);
                viewer.Paint += new PaintEventHandler(viewer_Paint);
            this.Controls.Add(viewer);
            this.ResumeLayout(false);
        }

        void timerPan_Tick(object sender, EventArgs e)
        {
            if (mouseDown)
            {
                Monitor.Enter(this);
                if (RPSPan > 0)
                {
                    RequestTurnLeftStart();
                    Thread.Sleep(5);
                    RequestTurnLeftStop();
                }
                if (RPSPan < 0)
                {
                    RequestTurnRightStart();
                    Thread.Sleep(5);
                    RequestTurnRightStop();
                }
                Monitor.Exit(this);
            }
        }

        void timerTilt_Tick(object sender, EventArgs e)
        {
            if (mouseDown)
            {
                Monitor.Enter(this);
                if (RPSTilt > 0)
                {
                    RequestTurnUpStart();
                    Thread.Sleep(5);
                    RequestTurnUpStop();
                }
                if (RPSTilt < 0)
                {
                    RequestTurnDownStart();
                    Thread.Sleep(5);
                    RequestTurnDownStop();
                }
                Monitor.Exit(this);
            }
        }

        void timerZoom_Tick(object sender, EventArgs e)
        {
            Monitor.Enter(this);
            if (RPSZoom > 0)
            {
                RequestZoomInStart();
                Thread.Sleep(5);
                RequestZoomInStop();
            }
            if (RPSZoom < 0)
            {
                RequestZoomOutStart();
                Thread.Sleep(5);
                RequestZoomOutStart();
            }
            Monitor.Exit(this);
        }

        void viewer_Paint(object sender, PaintEventArgs e)
        {
            if (mouseDown && mdx+mdy > 0 && mx+my > 0)
            {
                Graphics g = e.Graphics;
                Pen pen = new Pen(Color.FromArgb(32, Color.White), 30);
                Pen pen2 = new Pen(Color.FromArgb(64, Color.Black), 32);
                int tx = mx / sensitivityCoefPan * sensitivityCoefPan;
                int ty = my / sensitivityCoefTilt * sensitivityCoefTilt;

                //g.DrawLine(pen2, new Point(mdx, mdy), new Point(tx + ((mdx - tx) % sensitivityCoefPan), ty + ((mdy - ty) % sensitivityCoefTilt)));
                //g.DrawLine(pen, new Point(mdx, mdy), new Point(tx + ((mdx - tx) % sensitivityCoefPan), ty + ((mdy - ty) % sensitivityCoefTilt)));
                g.DrawLine(pen2, new Point(mdx, mdy), new Point(mx , my));
                g.DrawLine(pen, new Point(mdx, mdy), new Point(mx, my));
                pen.Dispose();
            }
        }

        private void FullScreenCameraViewer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) 
            {
                this.Close();
            }
        }

        private void FullScreenCameraViewer_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                timerPan.Interval = 100000;
                timerTilt.Interval = 100000;
                timerZoom.Interval = 100000;

                timerPan.Start();
                timerTilt.Start();
                timerZoom.Start();

                mdx = e.X;
                mdy = e.Y;
                mx = 0;
                my = 0;
                mouseDown = true;
            }
        }

        private void FullScreenCameraViewer_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                mouseDown = false;
                timerPan.Interval = 100000;
                timerTilt.Interval = 100000;
                timerZoom.Interval = 100000;

                timerPan.Stop();
                timerTilt.Stop();
                timerZoom.Stop();

                RequestTurnDownStop();
                RequestTurnLeftStop();
                RequestTurnRightStop();
                RequestTurnUpStop();
                RequestZoomInStop();
                RequestZoomOutStop();
            }
        }

        private void FullScreenCameraViewer_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                Monitor.Enter(this);
                mx = e.X;
                my = e.Y;

                RPSPan = 0;
                RPSTilt = 0;
                
                if (((mdx - mx) / sensitivityCoefPan) != 0)
                {
                    RPSPan = ((mdx - mx) / sensitivityCoefPan);
                    RPSPan = RPSPan / RPSCoefPan;
                }
                if (((mdy - my) / sensitivityCoefTilt) != 0)
                {
                    RPSTilt = ((mdy - my) / sensitivityCoefTilt);
                    RPSTilt = RPSTilt / RPSCoefTilt;
                }
                //RPSZoom = ((mdy - my) / sensitivityCoefZoom); 

                if (RPSPan != 0 && timerPan.Interval != Convert.ToInt32(1000 / Math.Abs(RPSPan)))
                {
                    if(Enabled) timerPan.Stop();
                    timerPan.Interval = Convert.ToInt32(1000 / Math.Abs(RPSPan));
                    timerPan.Start();
                    timerPan_Tick(sender, e);
                }
                if (RPSTilt != 0 && timerTilt.Interval != Convert.ToInt32(1000 / Math.Abs(RPSTilt)))
                {
                    if (Enabled) timerTilt.Stop();
                    timerTilt.Interval = Convert.ToInt32(1000 / Math.Abs(RPSTilt));
                    timerTilt.Start();
                    timerTilt_Tick(sender, e);
                }
                //timerZoom.Interval = 100000;
                Monitor.Exit(this);
            }
            viewer.Invalidate();
        }

        private void FullScreenCameraViewer_FormClosing(object sender, FormClosingEventArgs e)
        {
            viewer.MouseMove -= new MouseEventHandler(FullScreenCameraViewer_MouseMove);
            viewer.MouseDown -= new MouseEventHandler(FullScreenCameraViewer_MouseDown);
            viewer.MouseUp -= new MouseEventHandler(FullScreenCameraViewer_MouseUp);
            viewer.Paint -= new PaintEventHandler(viewer_Paint);
        }

        private void FullScreenCameraViewer_MouseWheel(object sender, MouseEventArgs e) 
        {
            if (e.Delta > 0)
            {
                RequestZoomInStart();
                Thread.Sleep(5);
                RequestZoomInStop();
            }
            if (e.Delta < 0)
            {
                RequestZoomOutStart();
                Thread.Sleep(5);
                RequestZoomOutStart();
            }
        }

        private void RequestTurnLeftStart() {
            try
            {
                String url = this.viewer.Camera.IPCFormatCamera.ModelInfo.Controls.StandartControls.Left.StartEvent;
                if (!String.IsNullOrEmpty(url))
                {
                    CameraConnectionString.ReplaceAllPattern(ref url, this.viewer.Camera.IPCFormatCamera);
                    Thread rt = new Thread(() => Requester(url));
                    rt.Start();
                }
            }
            catch (Exception){}
        }
        private void RequestTurnLeftStop() {
            try
            {
                String url = this.viewer.Camera.IPCFormatCamera.ModelInfo.Controls.StandartControls.Left.StopEvent;
                if (!String.IsNullOrEmpty(url))
                {
                    CameraConnectionString.ReplaceAllPattern(ref url, this.viewer.Camera.IPCFormatCamera);
                    Thread rt = new Thread(() => Requester(url));
                    rt.Start();
                }
            }
            catch (Exception) { }
        }
        private void RequestTurnRightStart()
        {
            try
            {
                String url = this.viewer.Camera.IPCFormatCamera.ModelInfo.Controls.StandartControls.Right.StartEvent;
                if (!String.IsNullOrEmpty(url))
                {
                    CameraConnectionString.ReplaceAllPattern(ref url, this.viewer.Camera.IPCFormatCamera);
                    Thread rt = new Thread(() => Requester(url));
                    rt.Start();
                }
            }
            catch (Exception) { }
        }
        private void RequestTurnRightStop()
        {
            try
            {
                String url = this.viewer.Camera.IPCFormatCamera.ModelInfo.Controls.StandartControls.Right.StopEvent;
                if (!String.IsNullOrEmpty(url))
                {
                    CameraConnectionString.ReplaceAllPattern(ref url, this.viewer.Camera.IPCFormatCamera);
                    Thread rt = new Thread(() => Requester(url));
                    rt.Start();
                }
            }
            catch (Exception) { }
        }
        private void RequestTurnUpStart()
        {
            try
            {
                String url = this.viewer.Camera.IPCFormatCamera.ModelInfo.Controls.StandartControls.Up.StartEvent;
                if (!String.IsNullOrEmpty(url))
                {
                    CameraConnectionString.ReplaceAllPattern(ref url, this.viewer.Camera.IPCFormatCamera);
                    Thread rt = new Thread(() => Requester(url));
                    rt.Start();
                }
            }
            catch (Exception) { }
        }
        private void RequestTurnUpStop()
        {
            try
            {
                String url = this.viewer.Camera.IPCFormatCamera.ModelInfo.Controls.StandartControls.Up.StopEvent;
                if (!String.IsNullOrEmpty(url))
                {
                    CameraConnectionString.ReplaceAllPattern(ref url, this.viewer.Camera.IPCFormatCamera);
                    Thread rt = new Thread(() => Requester(url));
                    rt.Start();
                }
            }
            catch (Exception) { }
        }
        private void RequestTurnDownStart()
        {
            try
            {
                String url = this.viewer.Camera.IPCFormatCamera.ModelInfo.Controls.StandartControls.Down.StartEvent;
                if (!String.IsNullOrEmpty(url))
                {
                    CameraConnectionString.ReplaceAllPattern(ref url, this.viewer.Camera.IPCFormatCamera);
                    Thread rt = new Thread(() => Requester(url));
                    rt.Start();
                }
            }
            catch (Exception) { }
        }
        private void RequestTurnDownStop()
        {
            try
            {
                String url = this.viewer.Camera.IPCFormatCamera.ModelInfo.Controls.StandartControls.Down.StopEvent;
                if (!String.IsNullOrEmpty(url))
                {
                    CameraConnectionString.ReplaceAllPattern(ref url, this.viewer.Camera.IPCFormatCamera);
                    Thread rt = new Thread(() => Requester(url));
                    rt.Start();
                }
            }
            catch (Exception) { }
        }
        private void RequestZoomInStart()
        {
            try
            {
                String url = this.viewer.Camera.IPCFormatCamera.ModelInfo.Controls.StandartControls.ZoomIn.StartEvent;
                if (!String.IsNullOrEmpty(url))
                {
                    CameraConnectionString.ReplaceAllPattern(ref url, this.viewer.Camera.IPCFormatCamera);
                    Thread rt = new Thread(() => Requester(url));
                    rt.Start();
                }
            }
            catch (Exception) { }
        }
        private void RequestZoomInStop()
        {
            try
            {
                String url = this.viewer.Camera.IPCFormatCamera.ModelInfo.Controls.StandartControls.ZoomIn.StopEvent;
                if (!String.IsNullOrEmpty(url))
                {
                    CameraConnectionString.ReplaceAllPattern(ref url, this.viewer.Camera.IPCFormatCamera);
                    Thread rt = new Thread(() => Requester(url));
                    rt.Start();
                }
            }
            catch (Exception) { }
        }
        private void RequestZoomOutStart()
        {
            try
            {
                String url = this.viewer.Camera.IPCFormatCamera.ModelInfo.Controls.StandartControls.ZoomOut.StartEvent;
                if (!String.IsNullOrEmpty(url))
                {
                    CameraConnectionString.ReplaceAllPattern(ref url, this.viewer.Camera.IPCFormatCamera);
                    Thread rt = new Thread(() => Requester(url));
                    rt.Start();
                }
            }
            catch (Exception) { }
        }
        private void RequestZoomOutStop()
        {
            try
            {
                String url = this.viewer.Camera.IPCFormatCamera.ModelInfo.Controls.StandartControls.ZoomOut.StopEvent;
                if (!String.IsNullOrEmpty(url))
                {
                    CameraConnectionString.ReplaceAllPattern(ref url, this.viewer.Camera.IPCFormatCamera);
                    Thread rt = new Thread(() => Requester(url));
                    rt.Start();
                }
            }
            catch (Exception) { }
        }

        private void Requester(string URL) 
        {
            HttpWebRequest httpWebRequest;
            WebResponse webResponse = null;
            String user = this.viewer.Camera.IPCFormatCamera.UserName;
            String password = this.viewer.Camera.IPCFormatCamera.Password;
			
            System.Random random = new System.Random((int)System.DateTime.Now.Ticks);
			
            httpWebRequest = (HttpWebRequest)WebRequest.Create(string.Concat(new object[]
						{
							URL,
							(URL.IndexOf('?') == -1) ? '?' : '&',
							"fake=",
							random.Next()
						}));
            httpWebRequest.Timeout = 10000;
            httpWebRequest.AllowAutoRedirect = true;
            httpWebRequest.Credentials = new NetworkCredential(user, password);
            httpWebRequest.Headers["Authorization"] = "Basic " + System.Convert.ToBase64String(System.Text.Encoding.Default.GetBytes(user + ":" + password));
            try
            {
                webResponse = httpWebRequest.GetResponse();
                webResponse.Close();
            }
            catch (WebException e){
                
            }
            
            //stream = webResponse.GetResponseStream();
            //stream.ReadTimeout = this._requestTimeout;
//            Console.WriteLine(httpWebRequest.);		
        }

        private void FullScreenCameraViewer_Resize(object sender, EventArgs e)
        {
            sensitivityCoefPan = Width * 3 / 100;
            sensitivityCoefTilt = Height * 3 / 100;
        }
    }
}
