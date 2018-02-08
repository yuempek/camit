using AForge.Video;
using AForge.Video.FFMPEG;
using iSpyApplication.Audio;
using iSpyApplication.Audio.streams;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using IPC;
namespace iSpyApplication.Video
{
	public class FFMPEGStream : IVideoSource, IAudioSource, ISupportsAudio
	{
		private class DelayedFrame : System.IDisposable
		{
			public System.Drawing.Bitmap B;
			public readonly double ShowTime;
			public DelayedFrame(System.Drawing.Bitmap b, double showTime, double delay)
			{
				this.B = b;
				this.ShowTime = showTime + delay;
			}
			public void Dispose()
			{
				if (this.B != null)
				{
					this.B.Dispose();
					this.B = null;
				}
			}
		}
		private class DelayedAudio
		{
			public readonly byte[] A;
			public readonly double ShowTime;
			public DelayedAudio(byte[] a, double showTime, double delay)
			{
				this.A = a;
				this.ShowTime = showTime + delay;
			}
		}
		
        private const int MAXBuffer = 60;
		private ManualResetEvent _stopEvent;
		private Thread _thread;
		private int _stopWatchOffset;
		private int _initialSeek = -1;
        private bool _realtime;
        private bool _stopping;
        private double _delay;
        private Thread _tOutput;
        private VideoFileReader _vfr;
        private DateTime _lastFrame = DateTime.MinValue;
        private BufferedWaveProvider _waveProvider;
        private List<FFMPEGStream.DelayedFrame> _videoframes;
        private List<FFMPEGStream.DelayedAudio> _audioframes;
        private ReasonToFinishPlaying _reasonToStop = ReasonToFinishPlaying.StoppedByUser;
        private readonly Stopwatch _sw = new Stopwatch();
        private readonly object _threadLock = new object();
        private volatile bool _bufferFull;
        
        public int RTSPMode;
        public int AnalyzeDuration = 2000;
        public int Timeout = 8000;
        public int ThreadKillDelay = 50;
        public bool Seekable;
        public bool NoBuffer;
        public long Duration;
        public double PlaybackRate = 1.0;
        public string Cookies = "";
		public string UserAgent = "";
		public string Headers = "";
        public SampleChannel SampleChannel;
        public IAudioSource OutAudio;
        public event NewFrameEventHandler NewFrame;
		public event VideoSourceErrorEventHandler VideoSourceError;
		public event PlayingFinishedEventHandler PlayingFinished;
		public event DataAvailableEventHandler DataAvailable;
		public event LevelChangedEventHandler LevelChanged;
		public event AudioFinishedEventHandler AudioFinished;
		public event HasAudioStreamEventHandler HasAudioStream;
		public BufferedWaveProvider WaveOutProvider
		{
			get;
			set;
		}
		public VolumeWaveProvider16New VolumeProvider
		{
			get;
			set;
		}
        
        private string _source;
        public string Source
		{
			get
			{
				return this._source;
			}
			set
			{
				this._source = value;
			}
		}
		
        //TODO
        public long BytesReceived
		{
			get
			{
				return 0L;
			}
		}
        
        private int _framesReceived;
        public int FramesReceived
		{
			get
			{
				int framesReceived = this._framesReceived;
				this._framesReceived = 0;
				return framesReceived;
			}
		}
		
        public bool IsRunning
		{
			get
			{
				return this.ThreadAlive;
			}
		}
		
        private bool ThreadAlive
		{
			get
			{
				bool result;
				lock (this._threadLock)
				{
					result = (this._thread != null && !this._thread.Join(System.TimeSpan.Zero));
				}
				return result;
			}
		}
        
        private bool _paused;
        public bool IsPaused
		{
			get
			{
				return this._paused;
			}
		}
		
        private bool IsFileSource
		{
			get
			{
				return this._source != null && this._source.IndexOf("://", System.StringComparison.Ordinal) == -1;
			}
		}
        
        private float _gain;
        public float Gain
		{
			get
			{
				return this._gain;
			}
			set
			{
				this._gain = value;
				if (this.SampleChannel != null)
				{
					this.SampleChannel.Volume = value;
				}
			}
		}
        
        private bool _listening;
        public bool Listening
		{
			get
			{
				return this.IsRunning && this._listening;
			}
			set
			{
				if (this.RecordingFormat == null)
				{
					this._listening = false;
					return;
				}
				if (this.WaveOutProvider != null)
				{
					if (this.WaveOutProvider.BufferedBytes > 0)
					{
						this.WaveOutProvider.ClearBuffer();
					}
					this.WaveOutProvider = null;
				}
				if (value)
				{
					this.WaveOutProvider = new BufferedWaveProvider(this.RecordingFormat)
					{
						DiscardOnBufferOverflow = true
					};
				}
				this._listening = value;
			}
		}
		
        //TODO
        public WaveFormat RecordingFormat
		{
			get;
			set;
		}
		
        public FFMPEGStream()
		{
		}
		public FFMPEGStream(string source)
		{
			this._source = source;
		}
        private void FfmpegListener()
        {
            this._reasonToStop = ReasonToFinishPlaying.StoppedByUser;
            this._vfr = null;
            bool flag = false;
            string errmsg = "";
            try
            {
                Program.WriterMutex.WaitOne();
                this._vfr = new VideoFileReader();
                int num = this._source.IndexOf("://", System.StringComparison.Ordinal);
                if (num > -1)
                {
                    this._source = this._source.Substring(0, num).ToLower() + this._source.Substring(num);
                }
                this._vfr.Open(this._source);
                flag = true;
            }
            catch (System.Exception ex)
            {
                MainForm.LogErrorToFile(ex.Message + ": " + this._source);
            }
            finally
            {
                try
                {
                    Program.WriterMutex.ReleaseMutex();
                }
                catch (Exception e)
                {
                }

            }
            if (this._vfr == null || !this._vfr.IsOpen || !flag)
            {
                this.ShutDown("Could not open stream: " + this._source);
                return;
            }
            if (this._stopEvent.WaitOne(0))
            {
                this.ShutDown("");
                return;
            }
            bool hasaudio = false;
            this._realtime = !this.IsFileSource;
            if (!this._realtime)
            {
                this.NoBuffer = false;
            }

            //TODO 
            // if (this._vfr.Channels > 0)
            // {
            // 	hasaudio = true;
            // 	this.RecordingFormat = new WaveFormat(this._vfr.SampleRate, 16, this._vfr.Channels);
            // 	this._waveProvider = new BufferedWaveProvider(this.RecordingFormat)
            // 	{
            // 		DiscardOnBufferOverflow = true,
            // 		BufferDuration = System.TimeSpan.FromMilliseconds(2500.0)
            // 	};
            // 	this.SampleChannel = new SampleChannel(this._waveProvider);
            // 	this.SampleChannel.PreVolumeMeter += new System.EventHandler<StreamVolumeEventArgs>(this.SampleChannelPreVolumeMeter);
            // 	if (this.HasAudioStream != null)
            // 	{
            // 		this.HasAudioStream(this, System.EventArgs.Empty);
            // 	}
            // }
            this.HasAudioStream = null;
            //this.Duration = this._vfr.Duration;
            if (!this.NoBuffer)
            {
                this._tOutput = new System.Threading.Thread(new System.Threading.ThreadStart(this.FrameEmitter))
                {
                    Name = "ffmpeg frame emitter"
                };
                this._tOutput.Start();
            }
            else
            {
                this._tOutput = null;
            }
            this._videoframes = new System.Collections.Generic.List<FFMPEGStream.DelayedFrame>();
            this._audioframes = new System.Collections.Generic.List<FFMPEGStream.DelayedAudio>();
            double num2 = 0.0;
            double num3 = 0.0;
            System.DateTime minValue = System.DateTime.MinValue;
            this._lastFrame = System.DateTime.Now;
            try
            {
                while (!this._stopEvent.WaitOne(5) && !this._stopping)
                {
                    this._bufferFull = (!this._realtime && (this._videoframes.Count > 60 || this._audioframes.Count > 60));
                    if (!this._paused && !this._bufferFull)
                    {
                        if (this.DecodeFrame(10, hasaudio, ref num3, ref num2, ref minValue))
                        {
                            this._reasonToStop = ReasonToFinishPlaying.EndOfStreamReached;
                            break;
                        }
                        //if (this.NoBuffer && !this._stopEvent.WaitOne(0))
                        if (!this._stopEvent.WaitOne(0))
                        {
                            if (this._videoframes.Count > 0)
                            {
                                FFMPEGStream.DelayedFrame delayedFrame = this._videoframes[0];
                                if (delayedFrame.B != null)
                                {
                                    if (this.NewFrame != null)
                                    {
                                        this.NewFrame(this, new NewFrameEventArgs(delayedFrame.B));
                                    }
                                    delayedFrame.B.Dispose();
                                }
                                this._videoframes.RemoveAt(0);
                            }
                            if (this._audioframes.Count > 0)
                            {
                                FFMPEGStream.DelayedAudio delayedAudio = this._audioframes[0];
                                if (delayedAudio.A != null)
                                {
                                    this.ProcessAudio(delayedAudio.A);
                                }
                                this._audioframes.RemoveAt(0);
                            }
                        }
                    }
                }
                this.StopOutput();
            }
            catch (System.Exception ex2)
            {
                MainForm.LogExceptionToFile(ex2);
                errmsg = ex2.Message;
            }
            if (this.SampleChannel != null)
            {
                this.SampleChannel.PreVolumeMeter -= new System.EventHandler<StreamVolumeEventArgs>(this.SampleChannelPreVolumeMeter);
                this.SampleChannel = null;
            }
            if (this._waveProvider != null && this._waveProvider.BufferedBytes > 0)
            {
                this._waveProvider.ClearBuffer();
            }
            this.ShutDown(errmsg);
        }
        public void Start()
		{
			if (this.IsRunning)
			{
				return;
			}
			this._framesReceived = 0;
			this._stopEvent = new System.Threading.ManualResetEvent(false);
			lock (this._threadLock)
			{
				this._thread = new System.Threading.Thread(new System.Threading.ThreadStart(this.FfmpegListener))
				{
					Name = "ffmpeg " + this._source
				};
				this._thread.Start();
                Program.Threads.Add(this._thread);
			}
			this.Seekable = this.IsFileSource;
			this._initialSeek = -1;
			this._stopWatchOffset = 0;
		}
		public void Play()
		{
			this._paused = false;
			this._sw.Start();
		}
		public void Pause()
		{
			this._paused = true;
			this._sw.Stop();
		}
		private void FrameEmitter()
		{
			this._sw.Reset();
			bool first = true;
			while (!this._stopEvent.WaitOne(5))
			{
				first = this.EmitFrame(first);
			}
		}
		private bool EmitFrame(bool first)
		{
			try
			{
				if (this._videoframes.Count > 0)
				{
					FFMPEGStream.DelayedFrame delayedFrame = this._videoframes[0];
					if (delayedFrame != null)
					{
						if (first)
						{
							first = false;
							this._sw.Start();
						}
						if ((double)(this._sw.ElapsedMilliseconds + (long)this._stopWatchOffset) > delayedFrame.ShowTime)
						{
							if (delayedFrame.B != null)
							{
								if (this.NewFrame != null)
								{
									this.NewFrame(this, new NewFrameEventArgs(delayedFrame.B));
								}
								delayedFrame.B.Dispose();
							}
							this._videoframes.RemoveAt(0);
						}
					}
					else
					{
						this._videoframes.RemoveAt(0);
					}
				}
				if (this._audioframes.Count > 0)
				{
					FFMPEGStream.DelayedAudio delayedAudio = this._audioframes[0];
					if (delayedAudio != null)
					{
						if (first)
						{
							first = false;
							this._sw.Start();
						}
						long num = this._sw.ElapsedMilliseconds + (long)this._stopWatchOffset;
						if ((double)num > delayedAudio.ShowTime)
						{
							if (delayedAudio.A != null)
							{
								this.ProcessAudio(delayedAudio.A);
							}
							this._audioframes.RemoveAt(0);
						}
					}
					else
					{
						this._audioframes.RemoveAt(0);
					}
				}
			}
			catch (System.Exception ex)
			{
				MainForm.LogExceptionToFile(ex);
			}
			return first;
		}
		private void ShutDown(string errmsg)
		{
			bool flag = !string.IsNullOrEmpty(errmsg);
			if (flag)
			{
				this._reasonToStop = ReasonToFinishPlaying.DeviceLost;
			}
			this.StopOutput();
			if (this.IsFileSource && !flag)
			{
				this._reasonToStop = ReasonToFinishPlaying.StoppedByUser;
			}
			if (this._vfr != null && this._vfr.IsOpen)
			{
				try
				{
					this._vfr.Dispose();
				}
				catch (System.Exception ex)
				{
					MainForm.LogExceptionToFile(ex);
				}
			}
            (new System.Threading.Thread(new System.Threading.ThreadStart(this.Stop))).Start();
		}
		private bool DecodeFrame(int analyseInterval, bool hasaudio, ref double firstmaxdrift, ref double maxdrift, ref System.DateTime dtAnalyse)
		{
			object obj = this._vfr.ReadVideoFrame();
			if (this._stopEvent.WaitOne(0))
			{
				return false;
			}
			this._lastFrame = System.DateTime.Now;
            if (obj == null)
            {
                return true;
            }

            System.Drawing.Bitmap b = obj as System.Drawing.Bitmap;
            if (dtAnalyse == System.DateTime.MinValue)
            {
                dtAnalyse = System.DateTime.Now.AddSeconds((double)analyseInterval);
            }
            double showTime2 = System.DateTime.Now.Ticks; // (double)this._vfr.VideoTime;
            if (this._realtime)
            {
                double num = (double)(showTime2 - this._sw.ElapsedMilliseconds);
                if (dtAnalyse > System.DateTime.Now)
                {
                    if (System.Math.Abs(num) > System.Math.Abs(maxdrift))
                    {
                        maxdrift = num;
                    }
                }
                else
                {
                    if (firstmaxdrift > 0.0)
                    {
                        this._delay = 0.0 - (maxdrift - firstmaxdrift);
                    }
                    else
                    {
                        firstmaxdrift = maxdrift;
                    }
                    maxdrift = 0.0;
                    dtAnalyse = System.DateTime.Now.AddSeconds((double)analyseInterval);
                }
            }
            this._videoframes.Add(new FFMPEGStream.DelayedFrame(b, showTime2, this._delay));
            
				
			return false;
		}
		private void StopOutput()
		{
			if (this._tOutput != null)
			{
				this._stopEvent.Set();
				this._tOutput.Join();
				this._tOutput = null;
			}
			this.ClearBuffer();
		}
		private void ProcessAudio(byte[] data)
		{
			try
			{
				if (this.DataAvailable != null)
				{
					this._waveProvider.AddSamples(data, 0, data.Length);
					float[] buffer = new float[data.Length];
					this.SampleChannel.Read(buffer, 0, data.Length);
					this.DataAvailable(this, new DataAvailableEventArgs((byte[])data.Clone()));
					if (this.WaveOutProvider != null && this.Listening)
					{
						this.WaveOutProvider.AddSamples(data, 0, data.Length);
					}
				}
			}
			catch (System.NullReferenceException)
			{
			}
			catch (System.Exception ex)
			{
				MainForm.LogExceptionToFile(ex);
			}
		}
		private void SampleChannelPreVolumeMeter(object sender, StreamVolumeEventArgs e)
		{
			if (this.LevelChanged != null)
			{
				this.LevelChanged(this, new LevelChangedEventArgs(e.MaxSampleValues));
			}
		}
		private void ClearBuffer()
		{
			if (this._videoframes != null)
			{
				this._videoframes.DisposeAll();
				this._videoframes.Clear();
			}
			if (this._audioframes != null)
			{
				this._audioframes.DisposeAll();
				this._audioframes.Clear();
			}
		}
		public void Seek(float percentage)
		{
			int num = System.Convert.ToInt32((double)this.Duration / 1000.0 * (double)percentage);
			if (this.Seekable)
			{
				this._sw.Stop();
				this.ClearBuffer();
				this._initialSeek = num;
				//this._vfr.Seek(num);
				this._stopWatchOffset = num * 1000;
				this._sw.Reset();
				this._sw.Start();
			}
		}
		
        public void SignalToStop()
		{
			this.Stop();
		}

		public void WaitForStop()
		{
			this.Stop();
		}

		public void Stop()
		{
			if (this._thread == null)
			{
				return;
			}
			if (this.IsRunning && !this._stopping)
			{
				this._stopping = true;
				this._stopEvent.Set();
				while (this.IsRunning)
				{
					try
					{
						this._thread.Join(this.ThreadKillDelay);
					}
					catch
					{
					}
					Application.DoEvents();
				}
				this.Listening = false;
				this._stopping = false;
				this._thread = null;
				
                if (this._stopEvent != null)
                {
                    this._stopEvent.Close();
                    this._stopEvent.Dispose();
                    this._stopEvent = null;
                }
            }
            if (!this._stopping) {
                if (this.PlayingFinished != null)
                {
                    this.PlayingFinished(this, this._reasonToStop);
                }
                if (this.AudioFinished != null)
                {
                    this.AudioFinished(this, this._reasonToStop);
                }
            }
		}
	}
}
