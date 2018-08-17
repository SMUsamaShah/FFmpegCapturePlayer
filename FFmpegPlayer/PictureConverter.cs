using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AdDetectVideoPlayer
{
    class PictureConverter
    {
        [DllImport("kernel32.dll", EntryPoint = "CopyMemory", SetLastError = false)]
        public static extern void CopyMemory(IntPtr dest, IntPtr src, uint count);

        public static Bitmap RgbToBitmap(IntPtr pData, int width, int height)
        {
            var pixelFormat = PixelFormat.Format24bppRgb;

            // create new bitmap on which data will be copied
            Bitmap bitmap = new Bitmap(width, height, pixelFormat);

            // lock bits
            BitmapData bitmapData = bitmap.LockBits(
                 new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                 ImageLockMode.ReadWrite,
                 pixelFormat);


            int numBytes = width * height * 3; // 3 is for [R][G][B]

            // 1- copy frame data to RGB array, IntPtr -> byte[]
            // array to hold RGB data
            //var rgbValues = new byte[numBytes];
            //Marshal.Copy(pData, rgbValues, 0, width * height * 3);
            // 2- copy from RGB array to bitmap, byte[] -> IntPtr
            //Marshal.Copy(rgbValues, 0, bitmapData.Scan0, Math.Abs(bitmapData.Stride) * bitmap.Height);

            // copy data on bitmap from frame data in single call, IntPtr -> IntPtr
            CopyMemory(bitmapData.Scan0, pData, (uint)(numBytes));

            // unlock bitmap
            bitmap.UnlockBits(bitmapData);

            return bitmap;
        }

        public static Bitmap YuvToBitmap(byte[] yuvData, int width, int height)
        {
            byte[] rgbArray = new byte[width * height * 3];

            FFmpegPlayerCLI.FFWrapper.YUV2RGB(yuvData, rgbArray, width, height);

            var pixelFormat = PixelFormat.Format24bppRgb;
            Bitmap bitmap = new Bitmap(width, height, pixelFormat);
            BitmapData bitmapData = bitmap.LockBits(
                    new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                    ImageLockMode.ReadWrite,
                    pixelFormat);
            Marshal.Copy(rgbArray, 0, bitmapData.Scan0, Math.Abs(bitmapData.Stride) * bitmap.Height);
            bitmap.UnlockBits(bitmapData);

            return bitmap;
        }
    }
}
