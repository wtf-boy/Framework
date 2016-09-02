namespace WTF.Framework
{
    using System;
    using System.Configuration;
    using System.Reflection;

    [ConfigurationCollection(typeof(GroupConfigElement), AddItemName="Group")]
    public class GroupConfigCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new GroupConfigElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((GroupConfigElement) element).name;
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
                return "Group";
            }
        }

        public GroupConfigElement this[string name]
        {
            get
            {
                return (GroupConfigElement) base.BaseGet(name);
            }
        }
    }
}

