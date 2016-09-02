using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
namespace WTF.Framework
{
    public class BatchQueueProcess<T>
    {
        private Action<string, List<T>> _batchActionAdd;
        private int _BatchCount;
        private int _ClearMinutes;
        private ConcurrentDictionary<string, List<T>> _DataGroupList;
        private DateTime _LastClearDate;
        private Thread _ProcessThread;
        private int _SleepMilliseconds;
        private ReaderWriterLockSlimHelper objConfigWriterLockSlimHelper;

        public BatchQueueProcess()
        {
            this.objConfigWriterLockSlimHelper = new ReaderWriterLockSlimHelper();
            this._DataGroupList = new ConcurrentDictionary<string, List<T>>();
            this._SleepMilliseconds = 1;
            this._BatchCount = 10;
            this._ClearMinutes = 1;
            this._batchActionAdd = null;
            this._LastClearDate = DateTime.Now;
            this._ProcessThread = null;
        }

        public void Add(T data, string groupKey = "DataBatch")
        {
            List<T> orAdd = this._DataGroupList.GetOrAdd(groupKey, new List<T>());
            ReaderWriterLockSlim slim = this.objConfigWriterLockSlimHelper.CreateLock(groupKey);
            slim.EnterWriteLock();
            try
            {
                orAdd.Add(data);
            }
            finally
            {
                slim.ExitWriteLock();
            }
        }

        private void ProcessCountData()
        {
            this._LastClearDate = DateTime.Now;
            while (true)
            {
                try
                {
                    TimeSpan span;
                    foreach (KeyValuePair<string, List<T>> pair in this._DataGroupList)
                    {
                        int count = pair.Value.Count;
                        if ((count >= this._BatchCount) || (((span = (TimeSpan) (DateTime.Now - this._LastClearDate)).Minutes >= this._ClearMinutes) && (count > 0)))
                        {
                            List<T> list = new List<T>();
                            ReaderWriterLockSlim slim = this.objConfigWriterLockSlimHelper.CreateLock(pair.Key);
                            slim.EnterWriteLock();
                            try
                            {
                                list.AddRange(pair.Value);
                                pair.Value.Clear();
                            }
                            finally
                            {
                                slim.ExitWriteLock();
                            }
                            if (list.Count > 0)
                            {
                                this._batchActionAdd(pair.Key, list);
                            }
                        }
                    }
                    span = (TimeSpan) (DateTime.Now - this._LastClearDate);
                    if (span.Minutes >= this._ClearMinutes)
                    {
                        this._LastClearDate = DateTime.Now;
                    }
                    Thread.Sleep(this._SleepMilliseconds);
                }
                catch (Exception exception)
                {
                    EventLogHelper.WriterLog("定时定量批量操作", exception);
                    Thread.Sleep(this._SleepMilliseconds);
                }
            }
        }

        public void StartProcess(Action<string, List<T>> batchAction, int batchCount = 10, int clearMinutes = 1, int sleepMilliseconds = 1)
        {
            if (batchAction == null)
            {
                throw new ArgumentException("batchAction批量新增动作不能为空");
            }
            if (sleepMilliseconds < 1)
            {
                throw new ArgumentException("sleepMilliseconds必须大于等于1");
            }
            if (batchCount < 5)
            {
                throw new ArgumentException("_BatchCount必须大于等于5");
            }
            if (clearMinutes < 1)
            {
                throw new ArgumentException("clearMinutes必须大于等于1");
            }
            this._batchActionAdd = batchAction;
            this._SleepMilliseconds = sleepMilliseconds;
            this._BatchCount = batchCount;
            this._ClearMinutes = clearMinutes;
            if (this._ProcessThread == null)
            {
                this._ProcessThread = new Thread(new ThreadStart(this.ProcessCountData));
                this._ProcessThread.Start();
            }
        }
    }
}

