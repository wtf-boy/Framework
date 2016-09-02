namespace WTF.Framework
{
    using System;
    using System.Configuration;
    using System.Reflection;

    [ConfigurationCollection(typeof(ConnectionStringElement), AddItemName="ConnectionString")]
    public class EncryptConnectionCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new ConnectionStringElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ConnectionStringElement) element).name;
        }

        protected override string ElementName
        {
            get
            {
                return "ConnectionString";
            }
        }

        public ConnectionStringElement this[string name]
        {
            get
            {
                return (ConnectionStringElement) base.BaseGet(name);
            }
        }
    }
}

