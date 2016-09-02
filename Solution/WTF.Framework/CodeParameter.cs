namespace WTF.Framework
{
    using System;

    public class CodeParameter
    {
        private string _CodeValue;
        private string _description;
        private string _key;
        private int _Value = 0;

        public string CodeValue
        {
            get
            {
                return this._CodeValue;
            }
            set
            {
                this._CodeValue = value;
            }
        }

        public string Description
        {
            get
            {
                return this._description;
            }
            set
            {
                this._description = value;
            }
        }

        public string Key
        {
            get
            {
                return this._key;
            }
            set
            {
                this._key = value;
            }
        }

        public int Value
        {
            get
            {
                return this._Value;
            }
            set
            {
                this._Value = value;
            }
        }
    }
}

