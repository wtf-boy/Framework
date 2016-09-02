namespace WTF.Framework
{
    using System;
    using System.Runtime.CompilerServices;

    public class QueuePoolEventArgs<T> : EventArgs where T: class
    {
        public QueuePoolEventArgs(QueuePoolHelper<T> queuePool, T message)
        {
            this.QueuePool = queuePool;
            this.Message = message;
        }

        public T Message { get; set; }

        public QueuePoolHelper<T> QueuePool { get; set; }
    }
}

