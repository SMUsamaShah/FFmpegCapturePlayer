using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdDetectVideoPlayer
{
    /// <summary>
    /// Fixed size queue of items. Oldest item is removed before 
    /// adding a new item if size is going to increase than the limit
    /// </summary>
    class FixedSizeQueue<T> : IFixedSizeQueue<T>
    {
        private int limit;
        private int currentIndex = 0;
        private List<T> itemList;

        public event EventHandler OnItemAdded;
        public event EventHandler OnItemRemoved;

        private object _lockObject = new object();

        public FixedSizeQueue(int size)
        {
            this.limit = size;
            itemList = new List<T>(size);
        }

        public int Push(T item)
        {
            lock (_lockObject)
            {
                // size will increase on add, remove oldest item first
                if (itemList.Count > limit)
                {
                    RemoveOldest();
                }

                // add new item
                itemList.Add(item);


                OnItemAdded?.Invoke(item, EventArgs.Empty);

                return itemList.Count;
            }
        }

        private void RemoveOldest()
        {
            lock (_lockObject)
            {
                if (itemList.Count > 0)
                {
                    var item = itemList[0];
                    itemList.RemoveAt(0);

                    OnItemRemoved?.Invoke(item, EventArgs.Empty);

                    Move(-1);
                }
            }
        }

        
        public T Current()
        {
            return Get(currentIndex);
        }

        public T Next()
        {
            var item = Get(currentIndex);

            Move(1);

            return item;
        }

        public void Move(int num)
        {
            if(num >= 0)
            {
                if (itemList.Count > 0)
                {
                    if (currentIndex < (itemList.Count - 1))
                    {
                        currentIndex = currentIndex + num;
                    }
                    else
                    {
                        currentIndex = (itemList.Count - 1);
                    }
                }
            }
            else
            {
                if (currentIndex > 0)
                {
                    currentIndex = currentIndex + num;
                }
                else
                {
                    currentIndex = 0;
                }
            }
        }        

        public T Get(int index)
        {
            if(itemList.Count > 0 
                && index > 0 && index < itemList.Count)
            {
                return itemList[index];
            }
            return default(T);
        }

        public void DisplayAll(Action<T> display/*Func<T, T> display*/)
        {
            foreach (var item in itemList)
            {
                display(item);
            }
        }

        public int MaxSize { get => limit; }
        public int Count { get => itemList.Count; }
        public int CurrentIndex { get => currentIndex; }
    }

    class VideoFrameQueue : FixedSizeQueue<VideoFrame>
    {
        public VideoFrameQueue(int size) : base(size)
        {
        }
    }

    class VideoFrameViewerQueue : FixedSizeQueue<Components.VideoFrameViewer>
    {
        public VideoFrameViewerQueue(int size) : base(size)
        {
        }
    }
}
