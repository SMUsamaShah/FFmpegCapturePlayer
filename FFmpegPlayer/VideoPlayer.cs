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
        private VideoFrameViewer videoBox;
        // picturebox which shows currently selected frame by user
        private VideoFrameViewer selectionViewer;
        // selected thumbnail
        private VideoFrameViewer currentThumbnail;
        // selected frame info
        private Label info;
        // panel which contains thumbnails (VideoFrameViewer)
        private FlowLayoutPanel panel;
        public FlowLayoutPanel Container { get => panel; }

        // Video frame data source
        private VideoFrameQueue vframeQueue = null;

        // thread to continuosly dispaly current frame in video box
        private Thread playerThread;
        private bool stop = false;
        private const int PLAY_FPS = 30;
        private const int THUMB_COUNT = 10;
        private long current_playing_frame = 0;
        private State current_state = State.STOPPED;
        public HScrollBar ScrollBar { set; get; }

        private System.Drawing.Size THUMBNAIL_SIZE = new System.Drawing.Size(80, 80);

        public VideoPlayer(
            VideoFrameViewer vframeViewer,
            VideoFrameViewer selectedVFrameViewer,
            Label selectedFrameInfo,
            FlowLayoutPanel thumbnailsContainer)
        {
            this.videoBox = vframeViewer;
            this.selectionViewer = selectedVFrameViewer;
            this.info = selectedFrameInfo;
            this.panel = thumbnailsContainer;


            //Init();
        }

        public void Init()
        {
            for (int i = 0; i < THUMB_COUNT; ++i)
            {
                var vframeViewer = new VideoFrameViewer();
                vframeViewer.SizeMode = PictureBoxSizeMode.StretchImage;
                vframeViewer.Size = THUMBNAIL_SIZE;
                vframeViewer.Click += VframeViewer_Click;
                AddControlToPanel(panel, vframeViewer);
            }

            ScrollBar.Value = 0;
            ScrollBar.Maximum = vframeQueue.MaxSize - THUMB_COUNT;

            //ScrollBar.ValueChanged += ScrollBar_ValueChanged;
            ScrollBar.Scroll += ScrollBar_Scroll;
        }

        private void ScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            //var scroll = sender as HScrollBar;
            // populate thumbnails from queue
            for (int i = 0; i < THUMB_COUNT; ++i)
            {
                var vf = vframeQueue.Get(i + e.NewValue);
                if(vf != null)
                    ((VideoFrameViewer)panel.Controls[i]).SetVideoFrame(vf);
            }
        }

        public void SetSourceQueue(VideoFrameQueue queue)
        {
            this.vframeQueue = queue;

            queue.OnItemAdded += OnVframeAdded;
            queue.OnItemRemoved += OnVframeRemoved;
        }

        private void OnVframeAdded(object sender, EventArgs e)
        {
            var vframe = sender as VideoFrame;

            var vframeViewer = new VideoFrameViewer(vframe);
            vframeViewer.SizeMode = PictureBoxSizeMode.StretchImage;
            vframeViewer.Size = THUMBNAIL_SIZE;

            vframeViewer.Click += VframeViewer_Click;

            //AddControlToPanel(panel, vframeViewer);
        }

        private void VframeViewer_Click(object sender, EventArgs e)
        {
            var viewer = sender as VideoFrameViewer;
            Select(viewer);
        }

        private void OnVframeRemoved(object sender, EventArgs e)
        {
            //RemoveControlFromPanel(panel, 0);
            if (ScrollBar.Value > 0)
            {
                Common.InvokeUI(ScrollBar, () => { ScrollBar.Value--; });
            }
        }

        private void AddControlToPanel(Panel panel, Control ctrl)
        {
            Common.InvokeUI(panel, () => { panel.Controls.Add(ctrl); });
        }

        private void RemoveControlFromPanel(Panel panel, int index)
        {
            Common.InvokeUI(panel, () => { panel.Controls.RemoveAt(index); });
        }

        private void RemoveControlFromPanel(Panel panel, Control ctrl)
        {
            Common.InvokeUI(panel, () => { panel.Controls.Remove(ctrl); });
        }

        public void Render(VideoFrame vframe)
        {
            if (vframe != null && vframe.Format == VideoFrame.ImageFormat.RGB)
            {
                videoBox.SetVideoFrame(vframe);
            }
        }

        public void MarkAdStart()
        {

        }

        public void MarkAdEnd()
        {

        }

        private void Select(VideoFrameViewer selectedThumbnail)
        {
            //reset previous selection
            if(this.currentThumbnail != null)
                this.currentThumbnail.BorderStyle = BorderStyle.None;

            //
            selectedThumbnail.BorderStyle = BorderStyle.Fixed3D;
            selectionViewer.SetVideoFrame(selectedThumbnail.VideoFrame);
            info.Text = selectedThumbnail.VideoFrame.SequenceNumber + "-" + selectedThumbnail.VideoFrame.Timestamp;

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
                                current_playing_frame = vframe.SequenceNumber;
                                videoBox.SetVideoFrame(vframe);
                            }

                            // control playback speed
                            Thread.Sleep(1000 / PLAY_FPS);
                        }
                    });
                    playerThread.Start();
                    

                    break;
                case State.PAUSED:

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
            //currentThumbnail = ((VideoFrameViewer)panel.Controls[selected]);
            //Select(currentThumbnail);

            //selected = selected + seconds * 30;
        }

        enum State
        {
            PLAYING,
            PAUSED,
            STOPPED
        }
    }
}
