using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdDetectVideoPlayer.Components
{
    public partial class FrameViewer : UserControl
    {
        private VideoFrame vframe;

        public VideoFrame VideoFrame { get => vframe; }
        public long SequenceNumber => vframe.SequenceNumber;

        public event EventHandler Click;

        public const string InfoFormat = "Index: {0}, Timestamp: {1}";

        public FrameViewer()
        {
            InitializeComponent();
        }

        public FrameViewer(VideoFrame vframe) : this()
        {
            this.vframe = vframe;
            pictureBox.InvokeUI(() => {
                pictureBox.Image = vframe.Bitmap;

                label1.Text = String.Format(
                    InfoFormat,
                    vframe.SequenceNumber,
                    vframe.Timestamp);
            });
        }

        public void SetVideoFrame(VideoFrame vframe)
        {
            this.vframe = vframe;
            pictureBox.InvokeUI(() => {
                pictureBox.Image = vframe.Bitmap;

                label1.Text = String.Format(
                    InfoFormat,
                    vframe.SequenceNumber,
                    vframe.Timestamp);
            });
            
        }

        private void pictureBox_Click(object sender, EventArgs e)
        {
            if (this.Click != null)
                this.Click(this, e);
        }
    }
}
