using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFmpegPlayer
{
    class AudioFrame
    {
        private uint frequency;
        private uint sample_rate;
        private byte[] data;
        private uint timestamp;

        AudioFrame(byte[] data, uint frequency, uint sample_rate, uint timestamp)
        {
            this.frequency = frequency;
            this.data = data;
            this.timestamp = timestamp;
        }
        
        /// <summary>
        /// Decoded audio frame data
        /// </summary>
        public byte[] Data { get => data; }

        public uint Timestamp { get => timestamp; }

        public uint Frequency { get => frequency; }

        public uint SampleRate { get => sample_rate; }
    }
}
