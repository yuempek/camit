// Camera Vision
//
// Copyright © Andrew Kirillov, 2005-2006
// andrew.kirillov@gmail.com
//
// Copyright © Yunus Emre PEKTAS, 2014
// yuempek@gmail.com

namespace IPC.Camera
{
    using System;
	using System.Drawing;
	using System.Threading;
    using AForge.Video;
    using System.IO;
    using IPC.Video;
    using iSpyApplication.Video;

	/// <summary>
	/// Camera class
	/// </summary>
	public class StreamCamera
	{
        public enum StreamType
        {
            MPEG,
            MJPEG,
            JPEG
        };

        public IPCFileFormat.CamerasCamera IPCFormatCamera;
        public CameraModel CameraModel;
        public event EventHandler NewFrame;
        public event PlayingFinishedEventHandler PlayingFinished;
        

        private bool reconnectWhenError = true;
        public bool ReconnectWhenError
        {
            get { return reconnectWhenError; }
            set { reconnectWhenError = value;  }
        }
        private int id = 0;
        public int ID
		{
			get { return id; }
			set { id = value; }
		}
        
        private string name;
        public string Name
		{
			get { return name; }
			set { name = value; }
		}
        
        private string description = "";
        public string Description
		{
			get { return description; }
			set { description = value; }
		}

        private object configuration = null;
        public object Configuration
		{
			get { return configuration; }
			set { configuration = value; }
		}
        
        private IVideoSource videoSource = null;
        public IVideoSource VideoSource
		{
            get { return videoSource; }
            set {
                try { videoSource.NewFrame -= new NewFrameEventHandler(videoSourceNewFrame); } catch { }
                try { videoSource.PlayingFinished -= new PlayingFinishedEventHandler(videoSourcePlayingFinished);} catch { }
                try { videoSource.VideoSourceError -= new VideoSourceErrorEventHandler(videoSourceVideoSourceError); } catch { }
                try { videoSource.Stop(); } catch { }
                videoSource = value;
                if(videoSource != null){
                    videoSource.NewFrame += new NewFrameEventHandler(videoSourceNewFrame);
                    videoSource.PlayingFinished += new PlayingFinishedEventHandler(videoSourcePlayingFinished);
                    videoSource.VideoSourceError += new VideoSourceErrorEventHandler(videoSourceVideoSourceError);
                }
            }
		}

        private void videoSourceVideoSourceError(object sender, VideoSourceErrorEventArgs eventArgs)
        {
            //throw new IOException();
        }

        private void videoSourcePlayingFinished(object sender, ReasonToFinishPlaying reason)
        {
            if (this.PlayingFinished != null)
            {
                this.PlayingFinished(this, reason);
            }
            if (reconnectWhenError && reason != ReasonToFinishPlaying.StoppedByUser)
            {
                this.VideoSource.Start();
            }
        }
        
        private Bitmap lastFrame = null;
        public Bitmap LastFrame
		{
			get { return lastFrame; }
		}

        private int width = -1;
        public int Width
		{
			get { return width; }
		}
		
        private int height = -1;
        public int Height
		{
			get { return height; }
		}
		
        public int FramesReceived
		{
			get { return (videoSource == null) ? 0 : videoSource.FramesReceived; }
		}
		
        public long BytesReceived
		{
			get { return (videoSource == null) ? 0 : videoSource.BytesReceived; }
		}
		
        public bool Running
		{
			get { return (videoSource == null) ? false : videoSource.IsRunning; }
		}

		public StreamCamera()
            : this(null)
        { }
		public StreamCamera(IVideoSource videoSource)
            :this(videoSource, "")
		{
        }
        public StreamCamera(IVideoSource videoSource, string name)
        {
            this.VideoSource = videoSource;
            this.name = name;
        }

		public void CloseVideoSource()
		{
			
            if (videoSource != null)
			{
                videoSource.Stop();
                videoSource = null;
			}
			// dispose old frame
			if (lastFrame != null)
			{
				lastFrame.Dispose();
				lastFrame = null;
			}
			width = -1;
			height = -1;
		}

		public void Start()
		{
            if (videoSource != null)
            {
                this.videoSource.Start();
            }
		}

		public void SignalToStop()
		{
			if (videoSource != null)
			{
				videoSource.SignalToStop();
			}
		}

		public void WaitForStop()
		{
			// lock
			Monitor.Enter(this);

			if (videoSource != null)
			{
				videoSource.WaitForStop();
			}
			// unlock
			Monitor.Exit(this);
		}

		public void Stop()
		{
			// lock
			Monitor.Enter(this);

			if (videoSource != null)
			{
				videoSource.Stop();
			}
			// unlock
			Monitor.Exit(this);
		}

		public void Lock()
		{
			Monitor.Enter(this);
		}

		public void Unlock()
		{
			Monitor.Exit(this);
		}

        private string FileNameDateStamp()
        {
            //TODO uniq
            DateTime n = DateTime.Now;
            return n.Year.ToString("0000") + "." + n.Month.ToString("00") + "." + n.Day.ToString("00") + " " + n.Hour.ToString("00") + ".00";
        }
        //private Video.ezMpeg mpeg = new Video.ezMpeg();
        private string lastTimeStamp = null;
        private VideoWriter writer = new VideoWriter("DIVX");

        private void videoSourceNewFrame(object sender, NewFrameEventArgs e)
        {
            string currentTimeStamp;
            String stamp;
            SizeF measuredSize;
            RectangleF recf;
            int labelMargin = 3;

            Monitor.Enter(this);
            // dispose old frame
			if (lastFrame != null)
			{
				lastFrame.Dispose();
			}

            lastFrame = AForge.Imaging.Image.Clone(e.Frame);
            Graphics g = Graphics.FromImage(lastFrame);

            #region draw label
            //draw label
            Font drawFont = new Font("Arial", lastFrame.Height / 40, FontStyle.Regular);
            SolidBrush drawBrush = new SolidBrush(Color.FromArgb(255, Color.Red));
            SolidBrush fillBrush = new SolidBrush(Color.FromArgb(64, Color.White));
            int totalLabelHeigth = 0;
            
            //name
            stamp = this.Name;
            measuredSize = g.MeasureString(stamp, drawFont);
            recf = new RectangleF(lastFrame.Width - measuredSize.Width, totalLabelHeigth + labelMargin, measuredSize.Width, measuredSize.Height);
            g.FillRectangle(fillBrush, recf);
            g.DrawString(stamp, drawFont, drawBrush, recf);
            totalLabelHeigth += labelMargin + Convert.ToInt32(measuredSize.Height);
            
            //date
            stamp = DateTime.Now.ToString();
            measuredSize = g.MeasureString(stamp, drawFont);
            recf = new RectangleF(lastFrame.Width - measuredSize.Width, totalLabelHeigth + labelMargin, measuredSize.Width, measuredSize.Height);
            g.FillRectangle(fillBrush, recf);
            g.DrawString(stamp, drawFont, drawBrush, recf);
            totalLabelHeigth += labelMargin + Convert.ToInt32(measuredSize.Height);

            drawBrush.Dispose();
            fillBrush.Dispose();
            drawFont.Dispose();

            g.Flush();
            //lastFrame = new Bitmap(lastFrame.Width, lastFrame.Height, g);
            #endregion

            // image dimension
			width = lastFrame.Width;
			height = lastFrame.Height;

            #region recording
            //recording
            if(MainForm.Recording && !String.IsNullOrEmpty(MainForm.RecordingPath)){
                currentTimeStamp = FileNameDateStamp();
                if (lastTimeStamp == null) lastTimeStamp = currentTimeStamp;
                if (writer == null) writer = new VideoWriter("mpeg");
                if (!writer.isOpen)
                {
                    int i = 0;
                    String FileName = MainForm.RecordingPath.TrimEnd("\\".ToCharArray()) + "\\" + this.name + "_" + currentTimeStamp;
                    while (File.Exists(FileName + ( i==0 ? "" : "_" + i.ToString() ) + ".avi"))  i++;
                    FileName = FileName + ( i==0 ? "" : "_" + i.ToString() ) + ".avi";
                    writer.FrameRate = 1;
                    if (this.VideoSource is JPEGStream2) writer.FrameRate = 1;
                    if (this.VideoSource is MJPEGStream2) writer.FrameRate = 1;
                    if (this.VideoSource is FFMPEGStream) writer.FrameRate = 25;
                    writer.Open(FileName, width, height);
                }
                else
                {
                    writer.AddFrame(AForge.Imaging.Image.Clone(lastFrame));
                }
                if (!currentTimeStamp.Equals(lastTimeStamp))
                {
                    writer.Close();
                    lastTimeStamp = currentTimeStamp;
                }
            }
            else
            {
                if (writer.isOpen)
                {
                    writer.Close();
                }
            }
            #endregion

            // unlock
			Monitor.Exit(this);
            
			// notify client
            if (NewFrame != null) this.NewFrame(this, e);
			
		}

    }
}
