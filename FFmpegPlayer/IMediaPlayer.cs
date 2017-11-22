using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdDetectVideoPlayer
{
    interface IMediaPlayer
    {
        void Play();
        void Pause();
        void Stop();
        void Seek(int seconds);
    }
}
