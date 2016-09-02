namespace WTF.Framework
{
    using System;
    using System.Collections.Generic;

    public class QueueHelper<T> : Queue<T>
    {
        private object QueueLock;

        public QueueHelper()
        {
            this.QueueLock = new object();
        }

        public T Dequeue()
        {
            lock (this.QueueLock)
            {
                return base.Dequeue();
            }
        }

        public void Enqueue(T item)
        {
            lock (this.QueueLock)
            {
                base.Enqueue(item);
            }
        }

        public int Count
        {
            get
            {
                lock (this.QueueLock)
                {
                    return base.Count;
                }
            }
        }
    }
}

