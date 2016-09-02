namespace WTF.Framework
{
    using System;

    public sealed class CodeAttribute : Attribute
    {
        private string _CodeValue = "";
        private string _Description;

        public CodeAttribute(string CodeValue, string description)
        {
            this._Description = description;
            this._CodeValue = CodeValue;
        }

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
                return this._Description;
            }
            set
            {
                this._Description = value;
            }
        }
    }
}

