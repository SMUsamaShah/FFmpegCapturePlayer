using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdDetectVideoPlayer
{
    /// <summary>
    /// A fixed size queue. Remove last element when size increases
    /// </summary>
    /// <typeparam name="T"></typeparam>
    interface IFixedSizeQueue<T> 
    {
        int Push(T item);
        T Get(int index);

        int MaxSize { get; }
    }
}
