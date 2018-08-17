using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;

namespace AdDetectVideoPlayer
{
    public class VideoFrame
    {
        public int Width { get => width; }
        public int Height { get => height; }
        public int Size { get => width * height; }
        /// <summary>
        /// Color format of frame e.g. RGB, BGR, YUV etc
        /// </summary>
        public ImageFormat Format { get => dataFormat; }

        public Int64 Timestamp { get => timestamp; }
        public long SequenceNumber { get => sequence_number; }

        public AdMarker AdMark { get => adMark; set => adMark = value; }

        public byte[] DataArray { get => data; set => data = value; }
        public IntPtr DataPtr { get => pData; }
        public int DataSize { get => size; }

        public Bitmap Bitmap
        {
            get {
                // create bitmap from yuv data on runtime
                if (bitmap == null && dataFormat == ImageFormat.YUV420P)
                {
                    return PictureConverter.YuvToBitmap(data, width, height);
                }
                return bitmap;
            }
        }

        private VideoFrame(IntPtr pData, int size, int width, int height, Int64 timestamp, ImageFormat format, long sequence_number)
        {
            this.width = width;
            this.height = height;
            this.pData = pData;
            this.size = size;
            this.timestamp = timestamp;
            this.sequence_number = sequence_number;
            this.dataFormat = format;
        }

        /// <summary>
        /// Create and return VideoFrame object. Created video frame will contain 
        /// either yuv data or a bitmap depending on recieved frame format.
        /// </summary>
        /// <param name="pData"></param>
        /// <param name="size"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="timestamp"></param>
        /// <param name="format"></param>
        /// <param name="sequence_number"></param>
        /// <returns></returns>
        public static VideoFrame CreateVideoFrame(IntPtr pData, int size, int width, int height, Int64 timestamp, int format, long sequence_number)
        {
            VideoFrame vframe = new VideoFrame(pData, size, width, height, timestamp, (ImageFormat)format, sequence_number);
            if (format == (int)VideoFrame.ImageFormat.YUV420P)
            {
                vframe.SaveYuv();
            }
            if (format == (int)VideoFrame.ImageFormat.BGR24)
            {
                vframe.SaveBitmap();
            }
            return vframe;
        }

        /// <summary>
        /// Make bitmap and save in VideoFrame
        /// </summary>
        public void SaveBitmap()
        {
            this.bitmap = PictureConverter.RgbToBitmap(this.DataPtr, this.Width, this.Height);
        }

        /// <summary>
        /// Save yuv data in VideoFrame
        /// </summary>
        public void SaveYuv()
        {
            this.DataArray = new byte[size];
            Marshal.Copy(this.DataPtr, this.DataArray, 0, size);
        }

        private int width;
        private int height;

        private Bitmap bitmap = null;
        private Int64 timestamp;
        private long sequence_number;

        private AdMarker adMark;

        private ImageFormat dataFormat;
        private byte[] data;
        private IntPtr pData;
        private int size;

        /// <summary>
        /// Enum corresponding to only supported AVPixelFormat values
        /// </summary>
        public enum ImageFormat
        {
            YUV420P = 0,   ///< planar YUV 4:2:0, 12bpp, (1 Cr & Cb sample per 2x2 Y samples)
            //YUYV422 = 1,   ///< packed YUV 4:2:2, 16bpp, Y0 Cb Y1 Cr
            //RGB24 = 2,     ///< packed RGB 8:8:8, 24bpp, RGBRGB...
            BGR24 = 3,     ///< packed RGB 8:8:8, 24bpp, BGRBGR..
        }

        public enum AdMarker
        {
            START, END
        }
    }
}
