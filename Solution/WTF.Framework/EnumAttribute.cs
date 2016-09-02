namespace WTF.Framework
{
    using System;

    public sealed class EnumAttribute : Attribute
    {
        private string _Description;
        private int _Value;

        public EnumAttribute(string description)
        {
            this._Value = -2147483648;
            this._Description = description;
        }

        public EnumAttribute(int value, string description)
        {
            this._Value = -2147483648;
            this._Description = description;
            this._Value = value;
        }

        public string Description
        {
            get
            {
                return this._Description;
            }
            set
            {
                this._Description = value;
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

