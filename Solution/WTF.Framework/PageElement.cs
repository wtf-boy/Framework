namespace WTF.Framework
{
    using System;
    using System.Configuration;

    public class PageElement : ConfigurationElement
    {
        [ConfigurationProperty("AutoEncrypt", IsRequired=false, DefaultValue=false)]
        public bool AutoEncrypt
        {
            get
            {
                return (bool) base["AutoEncrypt"];
            }
            set
            {
                base["AutoEncrypt"] = value;
            }
        }

        [ConfigurationProperty("EncryptKey", IsRequired=false, DefaultValue="95254db9-5be7-48c1-ab91-34b6555d8a3c")]
        public string EncryptKey
        {
            get
            {
                return (string) base["EncryptKey"];
            }
            set
            {
                base["EncryptKey"] = value;
            }
        }

        [ConfigurationProperty("IsAutoPageSize", IsRequired=false, DefaultValue=true)]
        public bool IsAutoPageSize
        {
            get
            {
                return (bool) base["IsAutoPageSize"];
            }
            set
            {
                base["IsAutoPageSize"] = value;
            }
        }

        [ConfigurationProperty("PageSize", IsRequired=false, DefaultValue=15)]
        public int PageSize
        {
            get
            {
                return (int) base["PageSize"];
            }
            set
            {
                base["PageSize"] = value;
            }
        }
    }
}

