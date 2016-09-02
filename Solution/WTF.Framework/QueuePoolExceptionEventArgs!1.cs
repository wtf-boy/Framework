namespace WTF.Framework
{
    using System;
    using System.Runtime.CompilerServices;

    public class QueuePoolExceptionEventArgs<T> : EventArgs where T: class
    {
        public QueuePoolExceptionEventArgs(QueuePoolHelper<T> queuePool, T message, System.Exception exception)
        {
            this.QueuePool = queuePool;
            this.Message = message;
            this.Exception = exception;
        }

        public System.Exception Exception { get; set; }

        public T Message { get; set; }

        public QueuePoolHelper<T> QueuePool { get; set; }
    }
}

