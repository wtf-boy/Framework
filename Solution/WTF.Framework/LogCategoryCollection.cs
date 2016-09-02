namespace WTF.Framework
{
    using System;
    using System.Configuration;
    using System.Reflection;

    [ConfigurationCollection(typeof(LogCategoryElement), AddItemName="Category")]
    public class LogCategoryCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new LogCategoryElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((LogCategoryElement) element).CategoryCode;
        }

        protected override string ElementName
        {
            get
            {
                return "Category";
            }
        }

        public LogCategoryElement this[string name]
        {
            get
            {
                return (LogCategoryElement) base.BaseGet(name);
            }
        }
    }
}

