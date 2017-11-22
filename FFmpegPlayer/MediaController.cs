using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdDetectVideoPlayer
{
    public class MediaController : IMediaPlayer
    {
        private String input = String.Empty;
        private int current_timestamp;
        private int current_frame_number;
        private State current_state = State.STOPPED;

        void IMediaPlayer.Pause()
        {
            throw new NotImplementedException();
        }

        void IMediaPlayer.Play()
        {
            switch(current_state)
            {
                case State.STOPPED:
                    
                    break;
                case State.PAUSED:

                    break;
                case State.PLAYING:

                    break;
            }

            throw new NotImplementedException();
        }

        void IMediaPlayer.Seek(int seconds)
        {
            throw new NotImplementedException();
        }

        void IMediaPlayer.Stop()
        {
            current_state = State.STOPPED;
            current_timestamp = 0;
            current_frame_number = 0;



            throw new NotImplementedException();
        }

        enum State
        {
            PLAYING,
            PAUSED,
            STOPPED
        }
    }
}
