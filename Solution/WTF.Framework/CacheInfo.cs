namespace WTF.Framework
{
    using System;

    [Serializable]
    public class CacheInfo
    {
        private string _CacheType;
        private string _ChildKey;
        private string _Key;

        public CacheInfo()
        {
            this._Key = "";
            this._CacheType = "";
        }

        public CacheInfo(string cacheType, string childKey, string cacheKey)
        {
            this._Key = "";
            this._CacheType = "";
            this._CacheType = cacheType;
            this._ChildKey = childKey;
            this._Key = cacheKey;
        }

        public string CacheType
        {
            get
            {
                return this._CacheType;
            }
            set
            {
                this._CacheType = value;
            }
        }

        public string ChildKey
        {
            get
            {
                return this._ChildKey;
            }
            set
            {
                this._ChildKey = value;
            }
        }

        public string Key
        {
            get
            {
                return this._Key;
            }
            set
            {
                this._Key = value;
            }
        }
    }
}

