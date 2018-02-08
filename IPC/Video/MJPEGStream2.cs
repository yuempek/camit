using AForge.Video;
using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using IPC;
namespace iSpyApplication.Video
{
    public class MJPEGStream2 : IVideoSource
    {
        private const int BufSize = 1048576;
        private const int ReadSize = 1024;
        private System.Threading.Thread _thread;
        private System.Threading.ManualResetEvent _stopEvent;
        private System.Threading.ManualResetEvent _reloadEvent;
        private bool _needsPrivacyEnabled;
        private System.DateTime _needsPrivacyEnabledTarget = System.DateTime.MinValue;
        public string Headers = "";
        public string DecodeKey;
        public event NewFrameEventHandler NewFrame;
        public event VideoSourceErrorEventHandler VideoSourceError;
        public event PlayingFinishedEventHandler PlayingFinished;

        #region setter/getters
        private bool _useSeparateConnectionGroup = true;
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
        
        private string _source;
        public string Source
        {
            get
            {
                return this._source;
            }
            set
            {
                Uri s = new Uri(value);
                try { this.Login = s.UserInfo.Split(':')[0]; }
                catch { }
                try { this.Password = s.UserInfo.Split(':')[1]; }
                catch { }
                this._source = value;
                if (this._thread != null)
                {
                    this._reloadEvent.Set();
                }
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
        
        private string _userAgent = "Mozilla/5.0";
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
                    if (!this._thread.Join(System.TimeSpan.Zero))
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
        #endregion
        
        //TODO will be making "restartable safe" like FFMPEGStream.cs or JPEGStream.cs
        public MJPEGStream2()
        {
        }
        public MJPEGStream2(string source)
        {
            this._source = source;
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
                this._reloadEvent = new System.Threading.ManualResetEvent(false);
                this._thread = new System.Threading.Thread(new System.Threading.ThreadStart(this.WorkerThread))
                {
                    Name = this._source
                };
                this._thread.Start();
            }
        }
        public void SignalToStop()
        {
            if (this._thread != null)
            {
                this._stopEvent.Set();
            }
        }
        public void WaitForStop()
        {
            if (this.IsRunning)
            {
                this._stopEvent.Set();
                this._thread.Join(MainForm.ThreadKillDelay);
                if (this._thread != null && !this._thread.Join(System.TimeSpan.Zero))
                {
                    this._thread.Abort();
                }
                this.Free();
            }
        }
        public void Stop()
        {
            this.WaitForStop();
        }
        private void Free()
        {
            this._thread = null;
            if (this._stopEvent != null)
            {
                this._stopEvent.Close();
                this._stopEvent.Dispose();
            }
            this._stopEvent = null;
            if (this._reloadEvent != null)
            {
                this._reloadEvent.Close();
                this._reloadEvent.Dispose();
            }
            this._reloadEvent = null;
        }
        private void WorkerThread()
        {
            byte[] array = new byte[1048576];
            byte[] array2 = new byte[]
			{
				255,
				216,
				255
			};
            System.Text.ASCIIEncoding aSCIIEncoding = new System.Text.ASCIIEncoding();
            ReasonToFinishPlaying reason = ReasonToFinishPlaying.StoppedByUser;
            while (!this._stopEvent.WaitOne(0, false))
            {
                this._reloadEvent.Reset();
                HttpWebRequest httpWebRequest = null;
                WebResponse webResponse = null;
                System.IO.Stream stream = null;
                string text = null;
                bool flag = false;
                int num = 0;
                int num2 = 0;
                int num3 = 0;
                int num4 = 1;
                int num5 = 0;
                try
                {
                    httpWebRequest = this.GenerateRequest(this._source);
                    webResponse = httpWebRequest.GetResponse();
                    string contentType = webResponse.ContentType;
                    string[] array3 = contentType.Split(new char[]
					{
						'/'
					});
                    int num6;
                    byte[] array4;
                    if (array3[0] == "application" && array3[1] == "octet-stream")
                    {
                        num6 = 0;
                        array4 = new byte[0];
                    }
                    else
                    {
                        if (!(array3[0] == "multipart") || !contentType.Contains("mixed"))
                        {
                            if (contentType == "text/html")
                            {
                                try
                                {
                                    System.IO.StreamReader streamReader = new System.IO.StreamReader(webResponse.GetResponseStream());
                                    string text2 = streamReader.ReadToEnd();
                                    if (text2.IndexOf("setup_kakulens.html", System.StringComparison.Ordinal) != -1 && this.DisablePrivacy(httpWebRequest))
                                    {
                                        this._needsPrivacyEnabledTarget = System.DateTime.Now.AddSeconds(4.0);
                                        this._needsPrivacyEnabled = true;
                                    }
                                }
                                catch (System.Exception ex)
                                {
                                    MainForm.LogExceptionToFile(ex);
                                }
                            }
                            throw new System.Exception("Invalid content type.");
                        }
                        int num7 = contentType.IndexOf("boundary", 0, System.StringComparison.Ordinal);
                        if (num7 != -1)
                        {
                            num7 = contentType.IndexOf("=", num7 + 8, System.StringComparison.Ordinal);
                        }
                        if (num7 == -1)
                        {
                            num6 = 0;
                            array4 = new byte[0];
                        }
                        else
                        {
                            text = contentType.Substring(num7 + 1);
                            text = text.Trim(new char[]
							{
								' ',
								'"'
							});
                            array4 = aSCIIEncoding.GetBytes(text);
                            num6 = array4.Length;
                            flag = false;
                        }
                    }
                    stream = webResponse.GetResponseStream();
                    stream.ReadTimeout = this._requestTimeout;
                    while (!this._stopEvent.WaitOne(0, false) && !this._reloadEvent.WaitOne(0, false))
                    {
                        if (num2 > 1047552)
                        {
                            num3 = (num2 = (num = 0));
                        }
                        int num8;
                        if ((num8 = stream.Read(array, num2, 1024)) == 0)
                        {
                            throw new System.ApplicationException();
                        }
                        num2 += num8;
                        num += num8;
                        this._bytesReceived += (long)num8;
                        if (num6 != 0 && !flag)
                        {
                            num3 = ByteArrayUtils.Find(array, array4, 0, num);
                            if (num3 == -1)
                            {
                                continue;
                            }
                            for (int i = num3 - 1; i >= 0; i--)
                            {
                                byte b = array[i];
                                if (b == 10 || b == 13)
                                {
                                    break;
                                }
                                text = (char)b + text;
                            }
                            array4 = aSCIIEncoding.GetBytes(text);
                            num6 = array4.Length;
                            flag = true;
                        }
                        if (num4 == 1 && num >= array2.Length)
                        {
                            num5 = ByteArrayUtils.Find(array, array2, num3, num);
                            if (num5 != -1)
                            {
                                num3 = num5 + array2.Length;
                                num = num2 - num3;
                                num4 = 2;
                            }
                            else
                            {
                                num = array2.Length - 1;
                                num3 = num2 - num;
                            }
                        }
                        while (num4 == 2 && num != 0 && num >= num6)
                        {
                            int num9 = ByteArrayUtils.Find(array, (num6 != 0) ? array4 : array2, num3, num);
                            if (num9 != -1)
                            {
                                this._framesReceived++;
                                if (this.NewFrame != null && !this._stopEvent.WaitOne(0, false))
                                {
                                    System.Drawing.Bitmap bitmap;
                                    if (!string.IsNullOrEmpty(this.DecodeKey))
                                    {
                                        byte[] bytes = System.Text.Encoding.ASCII.GetBytes(this.DecodeKey);
                                        using (System.IO.MemoryStream memoryStream = new System.IO.MemoryStream(array, num5 + array2.Length, array2.Length + bytes.Length))
                                        {
                                            byte[] array5 = new byte[bytes.Length];
                                            memoryStream.Read(array5, 0, bytes.Length);
                                            if (!ByteArrayUtils.UnsafeCompare(bytes, array5))
                                            {
                                                throw new System.Exception("Image Decode Failed - Check the decode key matches the encode key on ispy server");
                                            }
                                        }
                                        using (System.IO.MemoryStream memoryStream2 = new System.IO.MemoryStream(array, num5 + bytes.Length, num9 - num5 - bytes.Length))
                                        {
                                            memoryStream2.Seek(0L, System.IO.SeekOrigin.Begin);
                                            memoryStream2.WriteByte(array2[0]);
                                            memoryStream2.WriteByte(array2[1]);
                                            memoryStream2.WriteByte(array2[2]);
                                            memoryStream2.Seek(0L, System.IO.SeekOrigin.Begin);
                                            bitmap = (System.Drawing.Bitmap)System.Drawing.Image.FromStream(memoryStream2);
                                            this.NewFrame(this, new NewFrameEventArgs(bitmap));
                                            goto IL_467;
                                        }
                                        goto IL_429;
                                    }
                                    goto IL_429;
                                IL_467:
                                    bitmap.Dispose();
                                    bitmap = null;
                                    if (this._needsPrivacyEnabled && this._needsPrivacyEnabledTarget < System.DateTime.Now && this.EnablePrivacy(httpWebRequest))
                                    {
                                        this._needsPrivacyEnabled = false;
                                        goto IL_49C;
                                    }
                                    goto IL_49C;
                                IL_429:
                                    using (System.IO.MemoryStream memoryStream3 = new System.IO.MemoryStream(array, num5, num9 - num5))
                                    {
                                        bitmap = (System.Drawing.Bitmap)System.Drawing.Image.FromStream(memoryStream3);
                                        this.NewFrame(this, new NewFrameEventArgs(bitmap));
                                    }
                                    goto IL_467;
                                }
                            IL_49C:
                                num3 = num9 + num6;
                                num = num2 - num3;
                                System.Array.Copy(array, num3, array, 0, num);
                                num2 = num;
                                num3 = 0;
                                num4 = 1;
                            }
                            else
                            {
                                if (num6 != 0)
                                {
                                    num = num6 - 1;
                                    num3 = num2 - num;
                                }
                                else
                                {
                                    num = 0;
                                    num3 = num2;
                                }
                            }
                        }
                    }
                }
                catch (System.ApplicationException)
                {
                    System.Threading.Thread.Sleep(250);
                }
                catch (System.Threading.ThreadAbortException)
                {
                    break;
                }
                catch (System.Exception ex2)
                {
                    MainForm.LogExceptionToFile(ex2);
                    reason = ReasonToFinishPlaying.DeviceLost;
                    break;
                }
                finally
                {
                    if (httpWebRequest != null)
                    {
                        try
                        {
                            httpWebRequest.Abort();
                        }
                        catch
                        {
                        }
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
                    break;
                }
            }
            if (this.PlayingFinished != null)
            {
                this.PlayingFinished(this, reason);
            }
        }
        private bool DisablePrivacy(HttpWebRequest request)
        {
            string text = request.RequestUri.AbsoluteUri;
            text = text.Substring(0, text.IndexOf(request.RequestUri.AbsolutePath, System.StringComparison.Ordinal));
            bool result = false;
            for (int i = 0; i < 5; i++)
            {
                HttpWebRequest httpWebRequest = this.GenerateRequest(text + "/Set?Func=Powerdown&Kind=1&Data=0");
                httpWebRequest.Method = "GET";
                httpWebRequest.ContentType = "application/x-www-form-urlencoded";
                httpWebRequest.Timeout = 4000;
                try
                {
                    HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                    if (httpWebResponse.StatusCode == HttpStatusCode.OK)
                    {
                        System.IO.Stream responseStream = httpWebResponse.GetResponseStream();
                        if (responseStream != null)
                        {
                            System.IO.StreamReader streamReader = new System.IO.StreamReader(responseStream, true);
                            string a = streamReader.ReadToEnd().Trim();
                            if (a == "Return:0")
                            {
                                result = true;
                                break;
                            }
                        }
                    }
                    httpWebResponse.Close();
                }
                catch (System.Exception ex)
                {
                    MainForm.LogExceptionToFile(ex);
                }
                System.Threading.Thread.Sleep(2000);
            }
            return result;
        }
        private bool EnablePrivacy(HttpWebRequest request)
        {
            string text = request.RequestUri.AbsoluteUri;
            text = text.Substring(0, text.IndexOf(request.RequestUri.AbsolutePath, System.StringComparison.Ordinal));
            bool result = false;
            for (int i = 0; i < 5; i++)
            {
                HttpWebRequest httpWebRequest = this.GenerateRequest(text + "/Set?Func=Powerdown&Kind=1&Data=1");
                httpWebRequest.Method = "GET";
                httpWebRequest.ContentType = "application/x-www-form-urlencoded";
                try
                {
                    HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                    if (httpWebResponse.StatusCode == HttpStatusCode.OK)
                    {
                        System.IO.Stream responseStream = httpWebResponse.GetResponseStream();
                        if (responseStream != null)
                        {
                            System.IO.StreamReader streamReader = new System.IO.StreamReader(responseStream, true);
                            string a = streamReader.ReadToEnd().Trim();
                            if (a == "Return:0")
                            {
                                result = true;
                                break;
                            }
                        }
                    }
                    httpWebResponse.Close();
                }
                catch (System.Exception ex)
                {
                    MainForm.LogExceptionToFile(ex);
                }
                System.Threading.Thread.Sleep(2000);
            }
            return result;
        }
        private HttpWebRequest GenerateRequest(string source)
        {
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(source);
            if (this._userAgent != null)
            {
                httpWebRequest.UserAgent = this._userAgent;
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
            return httpWebRequest;
        }
    }
}
