namespace WTF.Business
{
    using System;
    using System.Data;
    using System.Runtime.CompilerServices;

    [Serializable]
    public class PageCache
    {
        public DataSet Data { get; set; }

        public int RecordCount { get; set; }
    }
}

