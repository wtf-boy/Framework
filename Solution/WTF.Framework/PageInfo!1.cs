namespace WTF.Framework
{
    using System;
    using System.Runtime.CompilerServices;

    [Serializable]
    public class PageInfo<T> where T: class, new()
    {
        public PageInfo()
        {
            this.PageIndex = 0;
            this.PageSize = 20;
            this.RecordCount = 0;
            this.Data = default(T);
        }

        public T Data { get; set; }

        public int PageCount
        {
            get
            {
                if (this.PageSize == 0)
                {
                    return 0;
                }
                if ((this.RecordCount % this.PageSize) == 0)
                {
                    return (this.RecordCount / this.PageSize);
                }
                return ((this.RecordCount / this.PageSize) + 1);
            }
        }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public int RecordCount { get; set; }
    }
}

