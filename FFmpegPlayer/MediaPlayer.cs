using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Security;
using FFmpegPlayerCLI;
using System.Drawing.Imaging;
using System.Drawing;
using System.Threading;

namespace AdDetectVideoPlayer
{
    // TODO: This class should be divided/refactored
    class MediaPlayer
    {
        private VideoRenderer vplayer;

        FFWrapper.FrameDecodedCallback_ ffc;

        FFWrapper ffmpeg;

        public MediaPlayer(VideoRenderer vplayer)
        {
            this.vplayer = vplayer;
            ffmpeg = new FFWrapper();
            ffc = new FFWrapper.FrameDecodedCallback_(DecodedVideoFrameCallback);
            ffmpeg.RegisterDecodedCallback(ffc);
        }

        public void OpenAndRun(string url)
        {
            ffmpeg.Open(url);

            new Thread(() => {
                ffmpeg.DecodeThread();
            }).Start();
        }

        public void DecodedVideoFrameCallback(VideoFrame vframe)
        {
            vplayer.Render(vframe);
        }

        public void DecodedVideoFrameCallback(IntPtr data, IntPtr linesize, int width, int height, Int64 pts)
        {
            VideoFrame vframe = new VideoFrame(data, linesize, width, height, pts);
            vplayer.Render(vframe);
            Thread.Sleep(33);
        }
    }
}
