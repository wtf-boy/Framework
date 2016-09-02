namespace WTF.Framework
{
    using System;
    using System.Configuration;

    public class ConnectionStringElement : ConfigurationElement
    {
        [ConfigurationProperty("connectionString", IsRequired=true)]
        public string connectionString
        {
            get
            {
                return (string) base["connectionString"];
            }
            set
            {
                base["connectionString"] = value;
            }
        }

        [ConfigurationProperty("name", IsRequired=true, IsKey=true)]
        public string name
        {
            get
            {
                return (string) base["name"];
            }
            set
            {
                base["name"] = value;
            }
        }
    }
}

