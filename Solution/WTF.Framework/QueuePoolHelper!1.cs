namespace WTF.Framework
{
    using System;
    using System.Collections.Concurrent;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Threading;

    public class QueuePoolHelper<T> where T: class
    {
        private ConcurrentQueue<T>[] _MessageList;
        private Thread _ProcessThread;
        private Random _ReceivedRandom;

        public event EventHandler<QueuePoolExceptionEventArgs<T>> SendException;

        public event EventHandler<QueuePoolEventArgs<T>> SendMessage;

        public QueuePoolHelper(int poolCount = 1)
        {
            this._ProcessThread = null;
            this._ReceivedRandom = new Random();
            this.PoolCount = poolCount;
            this.OneSleepMilliseconds = 0;
            this._MessageList = new ConcurrentQueue<T>[poolCount];
            for (int i = 0; i < poolCount; i++)
            {
                this._MessageList[i] = new ConcurrentQueue<T>();
            }
        }

        [Obsolete("请使用QueuePoolHelper(int poolCount = 1)构造函数实例化启动调用StartProcess")]
        public QueuePoolHelper(int poolCount, int parallelCount, int sleepMilliseconds = 1)
        {
            this._ProcessThread = null;
            this._ReceivedRandom = new Random();
            this.PoolCount = poolCount;
            this.SleepMilliseconds = sleepMilliseconds;
            this.OneSleepMilliseconds = 0;
            this._MessageList = new ConcurrentQueue<T>[poolCount];
            for (int i = 0; i < poolCount; i++)
            {
                this._MessageList[i] = new ConcurrentQueue<T>();
            }
            if (this._ProcessThread == null)
            {
                this._ProcessThread = new Thread(new ThreadStart(this.ProcessSendMessage));
                this._ProcessThread.Start();
            }
        }

        private T Dequeue()
        {
            T result = default(T);
            for (int i = 0; i < this.MessageList.Length; i++)
            {
                if (this.MessageList[i].TryDequeue(out result))
                {
                    return result;
                }
            }
            return result;
        }

        public virtual void Enqueue(T invokeMessage)
        {
            int index = this._ReceivedRandom.Next(0, this.PoolCount);
            this.MessageList[index].Enqueue(invokeMessage);
        }

        public virtual void Enqueue(T invokeMessage, int PoolIndex)
        {
            if (PoolIndex >= this.MessageList.Length)
            {
                throw new ArgumentNullException("超过消息池索引请传入0-" + this.MessageList.Length + "值");
            }
            this.MessageList[PoolIndex].Enqueue(invokeMessage);
        }

        public virtual void EnqueueInvokePool(T invokeMessage)
        {
            int index = this._ReceivedRandom.Next(0, this.PoolCount);
            this.MessageList[index].Enqueue(invokeMessage);
        }

        public virtual void EnqueueInvokePool(T invokeMessage, int PoolIndex)
        {
            if (PoolIndex >= this.MessageList.Length)
            {
                throw new ArgumentNullException("超过消息池索引请传入0-" + this.MessageList.Length + "值");
            }
            this.MessageList[PoolIndex].Enqueue(invokeMessage);
        }

        private void OnSendMessage(T message)
        {
            if (this.SendMessage != null)
            {
                try
                {
                    this.SendMessage(this, new QueuePoolEventArgs<T>((QueuePoolHelper<T>) this, message));
                }
                catch (Exception exception)
                {
                    if (this.SendException != null)
                    {
                        try
                        {
                            this.SendException(this, new QueuePoolExceptionEventArgs<T>((QueuePoolHelper<T>) this, message, exception));
                        }
                        catch (Exception exception2)
                        {
                            EventLogHelper.WriterLog("队列池处理SendException发送异常", exception2);
                        }
                    }
                    else
                    {
                        EventLogHelper.WriterLog("队列池SendMessage发送消息异常", exception);
                    }
                }
            }
        }

        public virtual void ProcessSendMessage()
        {
            while (true)
            {
                T message = this.Dequeue();
                if (message != null)
                {
                    this.OnSendMessage(message);
                    if (this.OneSleepMilliseconds > 0)
                    {
                        Thread.Sleep(this.OneSleepMilliseconds);
                    }
                }
                else
                {
                    Thread.Sleep((this.SleepMilliseconds <= 0) ? 1 : this.SleepMilliseconds);
                }
            }
        }

        public void StartProcess(int sleepMilliseconds = 1, int oneSleepMilliseconds = 0)
        {
            this.SleepMilliseconds = sleepMilliseconds;
            this.OneSleepMilliseconds = oneSleepMilliseconds;
            if (this.SendMessage == null)
            {
                throw new ArgumentNullException("未注册SendMessage事件，因此无法处理");
            }
            if (this._ProcessThread == null)
            {
                this._ProcessThread = new Thread(new ThreadStart(this.ProcessSendMessage));
                this._ProcessThread.Start();
            }
        }

        [Obsolete("请使用StartProcess方法")]
        public void StartProcessMessage(int parallelCount = 1, int sleepMilliseconds = 1, int oneSleepMilliseconds = 0)
        {
            if (parallelCount <= 0)
            {
                throw new ArgumentNullException("ParallelCount必须大小0");
            }
            this.SleepMilliseconds = sleepMilliseconds;
            this.OneSleepMilliseconds = oneSleepMilliseconds;
            if (this.SendMessage == null)
            {
                throw new ArgumentNullException("未注册SendMessage事件，因此无法处理");
            }
            if (this._ProcessThread == null)
            {
                this._ProcessThread = new Thread(new ThreadStart(this.ProcessSendMessage));
                this._ProcessThread.Start();
            }
        }

        public ConcurrentQueue<T>[] MessageList
        {
            get
            {
                return this._MessageList;
            }
        }

        public int MessageQueueCount
        {
            get
            {
                int num = 0;
                for (int i = 0; i < this.MessageList.Length; i++)
                {
                    num += this.MessageList[i].Count;
                }
                return num;
            }
        }

        public int OneSleepMilliseconds { get; set; }

        public int PoolCount { get; set; }

        public int SleepMilliseconds { get; set; }
    }
}

