namespace WTF.Framework
{
    using System;
    using System.Collections.Concurrent;
    using System.Threading;

    public class ReaderWriterLockSlimHelper
    {
        private ConcurrentDictionary<string, ReaderWriterLockSlim> _ReaderWriterLockSlimList = new ConcurrentDictionary<string, ReaderWriterLockSlim>();

        public ReaderWriterLockSlim CreateLock(string LockKey)
        {
            return this._ReaderWriterLockSlimList.GetOrAdd(LockKey, key => new ReaderWriterLockSlim());
        }

        [Obsolete("请使用RemoveLock方法")]
        public void DisposableLock(string LockKey)
        {
            this.RemoveLock(LockKey);
        }

        public void RemoveLock(string LockKey)
        {
            ReaderWriterLockSlim slim = null;
            this._ReaderWriterLockSlimList.TryRemove(LockKey, out slim);
        }
    }
}

