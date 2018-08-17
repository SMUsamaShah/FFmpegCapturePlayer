using FFmpegPlayerCLI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AdDetectVideoPlayer
{
    /// <summary>
    /// Decode using native decoder and put each decoded frame
    /// as VideoFrame in VideoFrameQueue
    /// </summary>
    class VideoDecoder
    {
        private VideoFrameQueue frameQueue;
        private const int DECODE_FPS = 30;
        private int decoded_frame_number = 0;

        private FFWrapper.FrameDecodedCallback_ decodedCallback;
        private FFWrapper ffmpeg;

        private Thread decodeThread;

        private string inputUrl;

        public VideoDecoder()
        {
            Init();
        }

        public VideoDecoder(VideoFrameQueue outputVframeQueue) 
            : this()
        {
            this.frameQueue = outputVframeQueue;
        }

        private void Init()
        {
            ffmpeg = new FFWrapper();
            ffmpeg.CreateDecoder((int)VideoFrame.ImageFormat.YUV420P);
            //ffmpeg.CreateDecoder((int)VideoFrame.ImageFormat.BGR24);

            decodedCallback = new FFWrapper.FrameDecodedCallback_(DecodedVideoFrameCallback);
            ffmpeg.RegisterDecodedCallback(decodedCallback);
        }

        public void SetInputUrl(string url)
        {
            this.inputUrl = url;
        }

        public bool Open()
        {
            String error = ffmpeg.Open(inputUrl);
            if(error != "") {
                System.Windows.Forms.MessageBox.Show(error);
                return false;
            }
            return true;
        }

        public void StartDecodeThread()
        {
            if (decodeThread != null && decodeThread.IsAlive)
                return;

            // decoder thread
            decodeThread = new Thread(() => {
                ffmpeg.DecodeLoop();
            });
            decodeThread.Start();
        }

        public void StopDecodeThread()
        {
            decodeThread.Abort();
        }

        public void SetVideoFrameQueue(VideoFrameQueue frameQueue)
        {
            this.frameQueue = frameQueue;
        }

        /// <summary>
        /// Frame conver
        /// </summary>
        /// <param name="vframe"></param>
        private void ProcessVFrame(VideoFrame vframe)
        {
            frameQueue.Push(vframe);
            // control decoding speed
            Thread.Sleep(1000 / DECODE_FPS);
        }

        /// <summary>
        /// call back from decoder thread. Frame data must be saved in this step or gets lost forever.
        /// </summary>
        /// <param name="pData"></param>
        /// <param name="linesize"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="pts"></param>
        /// <param name="format">int corresponding FFmpeg AVPixelFormat enum format</param>
        public void DecodedVideoFrameCallback(IntPtr pData, int size, int width, int height, Int64 pts, int format)
        {
            decoded_frame_number++; // assign index
            VideoFrame vframe = VideoFrame.CreateVideoFrame(pData, size, width, height, pts, format, decoded_frame_number);
            ProcessVFrame(vframe);
        }
    }
}
