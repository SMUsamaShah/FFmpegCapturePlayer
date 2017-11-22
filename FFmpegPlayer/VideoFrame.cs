using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdDetectVideoPlayer
{
    class VideoFrame
    {
        private uint width;
        private uint height;
        private ImageFormat format;
        private byte[] data;
        private uint timestamp;
        private ulong sequence_number;

        VideoFrame(byte[] data, uint width, uint height, uint timestamp, ulong seq_num, 
            ImageFormat format=ImageFormat.RGB)
        {
            this.width = width;
            this.height = height;
            this.format = format;
            this.data = data;
            this.timestamp = timestamp;
            this.sequence_number = seq_num;
        }

        public uint Width { get => width; }

        public uint Height { get => height; }

        public uint Size { get => width * height; }
        /// <summary>
        /// Color format of frame e.g. RGB, BGR, YUV etc
        /// </summary>
        public ImageFormat Format { get => format; }
        
        /// <summary>
        /// Image data
        /// </summary>
        public byte[] Data { get => data; }
        
        public uint Timestamp { get => timestamp; }

        public ulong SequenceNumber { get => sequence_number; }

        public enum ImageFormat
        {
            RGB, BGR, YUV
        }
    }
}
