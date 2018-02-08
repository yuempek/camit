using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AForge.Video.VFW;
using System.Threading;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace IPC.Video
{
    class VideoWriter
    {
        private AVIWriter writer;
        private List<Bitmap> buffer;
        public string Filename = "";

        public int FrameRate 
        {
            get { return this.writer.FrameRate; }
            set { this.writer.FrameRate = value; }
        }

        private Thread recorderThread;
        public bool isOpen = false;
        
        public Mutex mutex = new Mutex();

        public VideoWriter(string codec) 
        {
            writer = new AVIWriter(codec);
            buffer = new List<Bitmap>();
        }
        public VideoWriter()
        {
            writer = new AVIWriter();
            buffer = new List<Bitmap>();
        }

        public void Open(string filename, int width, int height) 
        {
            Monitor.Enter(this);
            if (isOpen == false)
            {
                try
                {
                    writer.Open(filename, width, height);
                    isOpen = true;
                }
                catch (Exception e)
                {
                    writer.Codec = "WMV3";
                    if(File.Exists(filename)) File.Delete(filename);
                }
            }
            Monitor.Exit(this);
        }

        public void AddFrame(Bitmap B)
        {
            Monitor.Enter(this);
            buffer.Add(B);
            if (recorderThread == null) 
            {
                recorderThread = new System.Threading.Thread(new System.Threading.ThreadStart(this.recorder));
                recorderThread.Start();
            }
            Monitor.Exit(this);
        }

        private void recorder()
        {
            while(true)
            {
                Monitor.Enter(this);
                while (buffer.Count == 0)
                {
                    Monitor.Exit(this);
                    Thread.Sleep(100);
                    Monitor.Enter(this);
                }
                Bitmap B = buffer[0];
                //buffer[0].Dispose();
                buffer.RemoveAt(0);
                Monitor.Exit(this);

                mutex.WaitOne();
                if (isOpen == true)
                {
                   writer.AddFrame(B);
                }
                mutex.ReleaseMutex();

                B.Dispose();
            }
        }

        public void Close()
        {
            mutex.WaitOne();
            if (isOpen == true)
                writer.Close();
            isOpen = false;
            mutex.ReleaseMutex();
        }
    }
}
