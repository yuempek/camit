using AForge.Video;
using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using IPC;
using System.Windows.Forms;
namespace iSpyApplication.Video
{
	public class JPEGStream2 : IVideoSource
	{
		private const int BufferSize = 1048576;
		private const int ReadSize = 1024;
		private bool _stopping = false;
		public string Headers = "";
		private System.Threading.Thread _thread;
		private System.Threading.ManualResetEvent _stopEvent;
        private ReasonToFinishPlaying _reasonToStop = ReasonToFinishPlaying.StoppedByUser;
		public event NewFrameEventHandler NewFrame;
		public event VideoSourceErrorEventHandler VideoSourceError;
		public event PlayingFinishedEventHandler PlayingFinished;
        public int ThreadKillDelay = 50;

        #region setter/getters
        private string _cookies = "";
		public string Cookies
		{
			get
			{
				return this._cookies;
			}
			set
			{
				this._cookies = value;
			}
		}
		
        private bool _usehttp10;
		public bool UseHTTP10
		{
			get
			{
				return this._usehttp10;
			}
			set
			{
				this._usehttp10 = value;
			}
		}
		
        private string _userAgent = "";
		public string HttpUserAgent
		{
			get
			{
				return this._userAgent;
			}
			set
			{
				this._userAgent = value;
			}
		}
        
        private bool _useSeparateConnectionGroup;
        public bool SeparateConnectionGroup
		{
			get
			{
				return this._useSeparateConnectionGroup;
			}
			set
			{
				this._useSeparateConnectionGroup = value;
			}
		}
        
        private bool _preventCaching = true;
        public bool PreventCaching
		{
			get
			{
				return this._preventCaching;
			}
			set
			{
				this._preventCaching = value;
			}
		}
        
        private int _frameInterval;
        public int FrameInterval
		{
			get
			{
				return this._frameInterval;
			}
			set
			{
				this._frameInterval = value;
			}
		}
        
        private string _source;
        public virtual string Source
		{
			get
			{
				return this._source;
			}
			set
			{ 
                Uri s = new Uri(value);
                try { this.Login = s.UserInfo.Split(':')[0]; } catch { }
                try { this.Password = s.UserInfo.Split(':')[1]; } catch { }
                this._source = value;
			}
		}
        
        private string _login;
        public string Login
		{
			get
			{
				return this._login;
			}
			set
			{
				this._login = value;
			}
		}
        
        private string _password;
        public string Password
		{
			get
			{
				return this._password;
			}
			set
			{
				this._password = value;
			}
		}
        
        private IWebProxy _proxy;
        public IWebProxy Proxy
		{
			get
			{
				return this._proxy;
			}
			set
			{
				this._proxy = value;
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
        
        private long _bytesReceived;
        public long BytesReceived
		{
			get
			{
				long bytesReceived = this._bytesReceived;
				this._bytesReceived = 0L;
				return bytesReceived;
			}
		}

        private int _requestTimeout = 10000;
        public int RequestTimeout
		{
			get
			{
				return this._requestTimeout;
			}
			set
			{
				this._requestTimeout = value;
			}
		}
		
        public bool IsRunning
		{
			get
			{
				if (this._thread != null)
				{
					if (!this._thread.Join(0))
					{
						return true;
					}
					this.Free();
				}
				return false;
			}
		}
        
        private bool _forceBasicAuthentication;
        public bool ForceBasicAuthentication
		{
			get
			{
				return this._forceBasicAuthentication;
			}
			set
			{
				this._forceBasicAuthentication = value;
			}
		}
        #endregion

        public JPEGStream2()
		{
		}
		public JPEGStream2(string source)
		{
			this.Source = source;
		}
		
        public void Start()
		{
			if (!this.IsRunning)
			{
				if (string.IsNullOrEmpty(this._source))
				{
					throw new System.ArgumentException("Video source is not specified.");
				}
				this._framesReceived = 0;
				this._bytesReceived = 0L;
				this._stopEvent = new System.Threading.ManualResetEvent(false);
				this._thread = new System.Threading.Thread(new System.Threading.ThreadStart(this.WorkerThread))
				{
					Name = this._source
				};
				this._thread.Start();
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
                this._stopping = false;

                this._thread = null;
                if (this._stopEvent != null)
                {
                    this._stopEvent.Close();
                    this._stopEvent.Dispose();
                    this._stopEvent = null;
                }
            }
            if (!this._stopping)
            {
                if (this.PlayingFinished != null)
                {
                    this.PlayingFinished(this, this._reasonToStop);
                }
            }

            // this._thread.Join(this.ThreadKillDelay);
            // if (this._thread != null && !this._thread.Join(System.TimeSpan.Zero))
            // {
            // 	this._thread.Abort();
            // }
            // this.Free();
        }
        private void Free()
        {
            this._thread = null;
            this._stopEvent.Close();
            this._stopEvent = null;
        }
        private void WorkerThread()
		{
			byte[] buffer = new byte[1048576];
			HttpWebRequest httpWebRequest = null;
			WebResponse webResponse = null;
			System.IO.Stream stream = null;
			System.Random random = new System.Random((int)System.DateTime.Now.Ticks);
			int num = 0;
			while (!this._stopEvent.WaitOne(0, false) && !this._stopping)
			{
				int num2 = 0;
				try
				{
					System.DateTime now = System.DateTime.Now;
					if (!this._preventCaching)
					{
						httpWebRequest = (HttpWebRequest)WebRequest.Create(this._source);
					}
					else
					{
						httpWebRequest = (HttpWebRequest)WebRequest.Create(string.Concat(new object[]
						{
							this._source,
							(this._source.IndexOf('?') == -1) ? '?' : '&',
							"fake=",
							random.Next()
						}));
					}
					if (this._proxy != null)
					{
						httpWebRequest.Proxy = this._proxy;
					}
					if (this._usehttp10)
					{
						httpWebRequest.ProtocolVersion = HttpVersion.Version10;
					}
					httpWebRequest.Timeout = this._requestTimeout;
					httpWebRequest.AllowAutoRedirect = true;
					if (this._login != null && this._password != null && this._login != string.Empty)
					{
						httpWebRequest.Credentials = new NetworkCredential(this._login, this._password);
					}
					if (this._useSeparateConnectionGroup)
					{
						httpWebRequest.ConnectionGroupName = this.GetHashCode().ToString(System.Globalization.CultureInfo.InvariantCulture);
					}
					string text = "";
					if (!string.IsNullOrEmpty(this._login))
					{
						text = System.Convert.ToBase64String(System.Text.Encoding.Default.GetBytes(this._login + ":" + this._password));
						httpWebRequest.Headers["Authorization"] = "Basic " + text;
					}
					if (!string.IsNullOrEmpty(this._cookies))
					{
						this._cookies = this._cookies.Replace("[AUTH]", text);
						CookieContainer cookieContainer = new CookieContainer();
						string[] array = this._cookies.Split(new char[]
						{
							';'
						});
						string[] array2 = array;
						for (int i = 0; i < array2.Length; i++)
						{
							string text2 = array2[i];
							if (!string.IsNullOrEmpty(text2))
							{
								string[] array3 = text2.Split(new char[]
								{
									'='
								});
								if (array3.Length == 2)
								{
									Cookie cookie = new Cookie(array3[0].Trim(), array3[1].Trim());
									cookieContainer.Add(new Uri(httpWebRequest.RequestUri.ToString()), cookie);
								}
							}
						}
						httpWebRequest.CookieContainer = cookieContainer;
					}
					if (!string.IsNullOrEmpty(this.Headers))
					{
						this.Headers = this.Headers.Replace("[AUTH]", text);
						string[] array4 = this._cookies.Split(new char[]
						{
							';'
						});
						string[] array5 = array4;
						for (int j = 0; j < array5.Length; j++)
						{
							string text3 = array5[j];
							if (!string.IsNullOrEmpty(text3))
							{
								string[] array6 = text3.Split(new char[]
								{
									'='
								});
								if (array6.Length == 2)
								{
									httpWebRequest.Headers.Add(array6[0], array6[1]);
								}
							}
						}
					}
					webResponse = httpWebRequest.GetResponse();
					stream = webResponse.GetResponseStream();
					stream.ReadTimeout = this._requestTimeout;
					while (!this._stopEvent.WaitOne(0, false))
					{
						if (num2 > 1047552)
						{
							num2 = 0;
						}
						int num3;
						if ((num3 = stream.Read(buffer, num2, 1024)) == 0)
						{
							break;
						}
						num2 += num3;
						this._bytesReceived += (long)num3;
					}
					if (!this._stopEvent.WaitOne(0, false))
					{
						this._framesReceived++;
						if (this.NewFrame != null)
						{
							System.Drawing.Bitmap bitmap = (System.Drawing.Bitmap)System.Drawing.Image.FromStream(new System.IO.MemoryStream(buffer, 0, num2));
							this.NewFrame(this, new NewFrameEventArgs(bitmap));
							bitmap.Dispose();
						}
					}
					if (this._frameInterval > 0)
					{
						System.TimeSpan timeSpan = System.DateTime.Now.Subtract(now);
						int num4 = this._frameInterval - (int)timeSpan.TotalMilliseconds;
						if (num4 > 0 && this._stopEvent.WaitOne(num4, false))
						{
                            _reasonToStop = ReasonToFinishPlaying.DeviceLost;
							break;
						}
					}
					num = 0;
				}
				catch (System.Threading.ThreadAbortException)
				{
                    _reasonToStop = ReasonToFinishPlaying.DeviceLost;
					break;
				}
				catch (System.Exception ex)
				{
					MainForm.LogErrorToFile(ex.Message);
					num++;
					if (num > 3)
					{
						_reasonToStop = ReasonToFinishPlaying.DeviceLost;
						break;
					}
					System.Threading.Thread.Sleep(250);
				}
				finally
				{
					if (httpWebRequest != null)
					{
						httpWebRequest.Abort();
						httpWebRequest = null;
					}
					if (stream != null)
					{
						stream.Close();
						stream = null;
					}
					if (webResponse != null)
					{
						webResponse.Close();
						webResponse = null;
					}
				}
				if (this._stopEvent.WaitOne(0, false))
				{
                    _reasonToStop = ReasonToFinishPlaying.StoppedByUser;
					break;
				}
			}
            ShutDown("");
		}
        private void ShutDown(string errmsg)
        {
            (new System.Threading.Thread(new System.Threading.ThreadStart(this.Stop))).Start();
        }
         
	}
}
