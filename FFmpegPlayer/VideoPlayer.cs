using AdDetectVideoPlayer.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdDetectVideoPlayer
{
    /// <summary>
    /// VideoPlayer consists of a video viewer, selected frame viewer,
    /// thumbnail slider, video controls, and info view. 
    /// A VideoFrameQueue is used as a data source or input.
    /// Next frame is retrieved and displayed in video view continously
    /// Frame selected by user is displayed separately along with its info
    /// When a frame is marked, it is communicated back to data source.
    /// </summary>
    class VideoPlayer
    {
        // picturebox which continously displays next frame from queue
        private FrameViewer videoBox;
        // picturebox which shows currently selected frame by user
        private FrameViewer selectionViewer;
        // selected thumbnail
        private FrameViewer currentThumbnail;
        // panel which contains thumbnails (FrameViewer)
        private FlowLayoutPanel panel;

        // Video frame data source
        private VideoFrameQueue vframeQueue = null;

        // thread to continuosly dispaly current frame in video box
        private Thread playerThread;
        private bool stop = false;
        private const int PLAY_FPS = 30;
        private const int THUMB_COUNT = 10;
        
        private System.Drawing.Size THUMBNAIL_SIZE = new System.Drawing.Size(100, 120);
        private State current_state = State.STOPPED;

        public HScrollBar ScrollBar { set; get; }
        public FlowLayoutPanel Container { get => panel; }

        public VideoPlayer(
            FrameViewer vframeViewer,
            FrameViewer selectedVFrameViewer,
            FlowLayoutPanel thumbnailsContainer)
        {
            this.videoBox = vframeViewer;
            this.selectionViewer = selectedVFrameViewer;
            this.panel = thumbnailsContainer;
        }

        public void Init()
        {
            // add thumbnails in the panel, these will be populated later
            for (int i = 0; i < THUMB_COUNT; ++i)
            {
                var vframeViewer = new FrameViewer();
                vframeViewer.Size = THUMBNAIL_SIZE;
                vframeViewer.Click += VframeViewer_Click;
                panel.InvokeUI(() => { panel.Controls.Add(vframeViewer); });
            }

            ScrollBar.Value = 0;
            ScrollBar.Maximum = vframeQueue.MaxSize - THUMB_COUNT;

            ScrollBar.Scroll += ScrollBar_Scroll;
        }

        private void ScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            // e.Type contains different types of events from scrollbar

            // populate thumbnails from queue
            panel.InvokeUI(() => {
                for (int i = 0; i < THUMB_COUNT; ++i)
                {
                    var vf = vframeQueue.Get(i + e.NewValue);
                    if (vf != null)
                    {
                        ((FrameViewer)panel.Controls[i]).SetVideoFrame(vf);
                    }
                }
            });
        }

        public void SetSourceQueue(VideoFrameQueue queue)
        {
            this.vframeQueue = queue;

            queue.OnItemAdded += OnVframeAdded;
            queue.OnItemRemoved += OnVframeRemoved;
        }

        private void VframeViewer_Click(object sender, EventArgs e)
        {
            var viewer = sender as FrameViewer;
            Select(viewer);
        }

        private void OnVframeAdded(object sender, EventArgs e)
        {
            
        }
        static int n=0;
        private void OnVframeRemoved(object sender, EventArgs e)
        {
            /*if(n++ == 30)
            {
                n = 0;
                GC.Collect();
            }*/

            if (ScrollBar.Value > 0)
            {
                ScrollBar.InvokeUI(() => { ScrollBar.Value--; });
            }
        }

        public void MarkAdStart()
        {

        }

        public void MarkAdEnd()
        {

        }

        private void Select(FrameViewer selectedThumbnail)
        {
            //
            selectionViewer.SetVideoFrame(selectedThumbnail.VideoFrame);

            this.currentThumbnail = selectedThumbnail;
        }
        
        public void Play()
        {
            switch (current_state)
            {
                case State.STOPPED:

                    current_state = State.PLAYING;

                    if (playerThread != null && playerThread.IsAlive)
                        return;

                    playerThread = new Thread(() =>
                    {
                        while (!stop)
                        {
                            // keep playing latest frame from queue
                            var vframe = vframeQueue.Get(vframeQueue.Count - 1);//vframeQueue.Next();
                            if (vframe != null)
                            {
                                videoBox.SetVideoFrame(vframe);
                            }

                            // control playback speed
                            Thread.Sleep(1000 / PLAY_FPS);
                        }
                    });
                    playerThread.Start();


                    break;
                case State.PAUSED:
                    current_state = State.PLAYING;

                    break;
                case State.PLAYING:
                    Stop();
                    break;
            }
        }

        public void Pause()
        {
            throw new NotImplementedException();
        }

        public void Stop()
        {
            stop = true;
            playerThread.Abort();
            current_state = State.STOPPED;
            //current_timestamp = 0;
            //decoded_frame_number = 0;
        }

        public void Seek(int seconds)
        {
            
        }

        enum State
        {
            PLAYING,
            PAUSED,
            STOPPED
        }
    }
}
