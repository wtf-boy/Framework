namespace WTF.Framework
{
    using System;
    using System.Configuration;
    using System.Reflection;

    [ConfigurationCollection(typeof(GroupValueElement), AddItemName="add")]
    public class GroupValueCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new GroupValueElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((GroupValueElement) element).Key;
        }

        public override ConfigurationElementCollectionType CollectionType
        {
            get
            {
                return ConfigurationElementCollectionType.BasicMap;
            }
        }

        protected override string ElementName
        {
            get
            {
                return "add";
            }
        }

        public GroupValueElement this[string key]
        {
            get
            {
                return (GroupValueElement) base.BaseGet(key);
            }
        }
    }
}

