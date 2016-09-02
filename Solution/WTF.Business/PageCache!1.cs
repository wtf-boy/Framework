namespace WTF.Business
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    [Serializable]
    public class PageCache<T>
    {
        public IList<T> Data { get; set; }

        public int RecordCount { get; set; }
    }
}

