using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFmpegPlayer
{
    class MediaPlayer
    {
        private VideoRenderer vplayer;

        public void DecodedVideoFrameCallback(VideoFrame vframe)
        {
            vplayer.Render(vframe);
        }
    }
}
