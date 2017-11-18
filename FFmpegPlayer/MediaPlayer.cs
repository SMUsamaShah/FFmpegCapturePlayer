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
        private AudioRenderer aplayer;

        public void DecodedVideoFrameCallback(VideoFrame vframe)
        {
            vplayer.Render(vframe);
        }

        public void DecodedAudioFrameCallback(AudioFrame aframe)
        {
            aplayer.Render(aframe);
        }
    }
}
