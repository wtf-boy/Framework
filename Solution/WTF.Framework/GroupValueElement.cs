namespace WTF.Framework
{
    using System;
    using System.Configuration;

    public class GroupValueElement : ConfigurationElement
    {
        [ConfigurationProperty("key", IsRequired=true, IsKey=true)]
        public string Key
        {
            get
            {
                return (string) base["key"];
            }
            set
            {
                base["key"] = value;
            }
        }

        [ConfigurationProperty("value", DefaultValue="")]
        public string Value
        {
            get
            {
                return base["value"].ToString();
            }
            set
            {
                base["value"] = value;
            }
        }
    }
}

