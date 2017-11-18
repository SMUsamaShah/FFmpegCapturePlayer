using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFmpegPlayer
{
    class FrameBuffer
    {
        private int frame_rate;
        private Queue<VideoFrame> video_frame_queue;
    }
}
