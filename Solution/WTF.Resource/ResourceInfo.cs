namespace WTF.Resource
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class ResourceInfo
    {
        private string _FilePath;
        private string _Md5Value;
        private string _OriginalName;
        private string _ResourceID;
        private string _ResourcePath;
        private string _ResourceVerID;
        private int _VerNo;

        public ResourceInfo()
        {
            this._ResourceID = "";
            this._ResourceVerID = "";
            this._VerNo = 0;
            this._ResourcePath = "";
            this._FilePath = "";
            this._OriginalName = "";
            this._Md5Value = "";
        }

        public ResourceInfo(string resourceID, string resourceVerID, int verNo, string resourcePath, string filePath, string originalName = "")
        {
            this._ResourceID = "";
            this._ResourceVerID = "";
            this._VerNo = 0;
            this._ResourcePath = "";
            this._FilePath = "";
            this._OriginalName = "";
            this._Md5Value = "";
            this._ResourceID = resourceID;
            this._ResourceVerID = resourceVerID;
            this._VerNo = verNo;
            this._ResourcePath = resourcePath;
            this._FilePath = filePath;
            this._OriginalName = originalName;
        }

        public string FilePath
        {
            get
            {
                return this._FilePath;
            }
            set
            {
                this._FilePath = value;
            }
        }

        public int ImageHeight { get; set; }

        public int ImageWidth { get; set; }

        public string Md5Value
        {
            get
            {
                return this._Md5Value;
            }
            set
            {
                this._Md5Value = value;
            }
        }

        public string OriginalName
        {
            get
            {
                return this._OriginalName;
            }
            set
            {
                this._OriginalName = value;
            }
        }

        public string ResourceID
        {
            get
            {
                return this._ResourceID;
            }
            set
            {
                this._ResourceID = value;
            }
        }

        public string ResourcePath
        {
            get
            {
                return this._ResourcePath;
            }
            set
            {
                this._ResourcePath = value;
            }
        }

        public string ResourceVerID
        {
            get
            {
                return this._ResourceVerID;
            }
            set
            {
                this._ResourceVerID = value;
            }
        }

        public int VerNo
        {
            get
            {
                return this._VerNo;
            }
            set
            {
                this._VerNo = value;
            }
        }
    }
}

