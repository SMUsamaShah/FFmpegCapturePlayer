using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AdDetectVideoPlayer.Components;

namespace AdDetectVideoPlayer
{
    public partial class Form1 : Form
    {
        private VideoPlayer videoPlayer;
        private VideoDecoder decoder;
        private int QUEUE_SIZE = 300;

        public Form1()
        {
            InitializeComponent();

            var vframeQueue = new VideoFrameQueue(QUEUE_SIZE);

            decoder = new VideoDecoder(outputVframeQueue: vframeQueue);

            videoPlayer = new VideoPlayer(
                vframeViewer: pictureBoxVideo,
                selectedVFrameViewer: selectedFrameViewer,
                thumbnailsContainer: thumbPanel
            );
            videoPlayer.SetSourceQueue(vframeQueue);

            videoPlayer.ScrollBar = queueScroller;

            videoPlayer.Init();
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            decoder.SetInputUrl(@textBoxPath.Text);
            bool success = decoder.Open();
            if (!success)
                return;

            decoder.StartDecodeThread();
            videoPlayer.Play();
            //mediaController.Input(@textBoxPath.Text);
            //mediaController.Play();
        }

        void UpdateCurrentFrameInfo(long sequenceNumber)
        {

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //mediaController.Stop();
        }
    }
}
