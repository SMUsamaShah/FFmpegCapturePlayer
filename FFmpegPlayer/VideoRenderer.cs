using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdDetectVideoPlayer
{
    class VideoRenderer
    {
        private PictureBox pictureBox;

        public VideoRenderer(PictureBox pictureBox)
        {
            this.pictureBox = pictureBox;
        }

        public void Render(VideoFrame frame)
        {
            if(frame.Format == VideoFrame.ImageFormat.RGB)
            {
                int size = 1;
                var ls = new int[size];
                Marshal.Copy(frame.LineSize, ls, 0, size);

                var bmp = new Bitmap(frame.Width, frame.Height, ls[0], PixelFormat.Format24bppRgb, frame.PData);
                //bmp.Save("d:\\test.bmp");
                pictureBox.Image = bmp;
            }
        }
    }
}
