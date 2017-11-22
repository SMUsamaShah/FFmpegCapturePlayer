using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdDetectVideoPlayer
{
    class VideoFrame
    {
        private int width;
        private int height;
        private ImageFormat format;
        private byte[] data;
        private Int64 timestamp;
        private long sequence_number;

        private IntPtr pdata;
        private IntPtr linesize;

        public VideoFrame(IntPtr pdata, IntPtr linesize, int width, int height, Int64 timestamp)
        {
            this.width = width;
            this.height = height;
            this.pdata = pdata;
            this.linesize = linesize;
            this.timestamp = timestamp;
        }

        public VideoFrame(byte[] data, int width, int height, Int64 timestamp, long sequence_number, 
            ImageFormat format=ImageFormat.RGB)
        {
            this.width = width;
            this.height = height;
            this.format = format;
            this.data = data;
            this.timestamp = timestamp;
            this.sequence_number = sequence_number;
        }

        public int Width { get => width; }
        public int Height { get => height; }
        public int Size { get => width * height; }
        /// <summary>
        /// Color format of frame e.g. RGB, BGR, YUV etc
        /// </summary>
        public ImageFormat Format { get => format; }
        
        /// <summary>
        /// Image data
        /// </summary>
        public byte[] Data { get => data; }
        public Int64 Timestamp { get => timestamp; }
        public long SequenceNumber { get => sequence_number; }

        public IntPtr PData { get => pdata; }
        public IntPtr LineSize { get => linesize; }

        public enum ImageFormat
        {
            RGB, BGR, YUV
        }
    }
}
