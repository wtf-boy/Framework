namespace WTF.Framework
{
    using System;
    using System.Configuration;

    public class EncryptConnectionSection : ConfigurationSection
    {
        [ConfigurationProperty("", IsDefaultCollection=true)]
        public EncryptConnectionCollection ConnectionStrings
        {
            get
            {
                return (EncryptConnectionCollection) base[""];
            }
        }

        [ConfigurationProperty("EncryptKey", IsRequired=true)]
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

        [ConfigurationProperty("IsEncrypt", IsRequired=false, DefaultValue=true)]
        public bool IsEncrypt
        {
            get
            {
                return (bool) base["IsEncrypt"];
            }
            set
            {
                base["IsEncrypt"] = value;
            }
        }
    }
}

