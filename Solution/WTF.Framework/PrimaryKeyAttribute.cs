namespace WTF.Framework
{
    using System;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    public class PrimaryKeyAttribute : Attribute
    {
        public PrimaryKeyAttribute()
        {
            this.PrimaryProperty = null;
            this.Identity = true;
        }

        public PrimaryKeyAttribute(bool identity)
        {
            this.PrimaryProperty = null;
            this.Identity = identity;
        }

        public bool Identity { get; set; }

        public string KeyName
        {
            get
            {
                return ((this.PrimaryProperty == null) ? "" : this.PrimaryProperty.Name);
            }
        }

        public PropertyInfo PrimaryProperty { get; set; }
    }
}

