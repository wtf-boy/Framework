namespace WTF.Framework
{
    using System;
    using System.Configuration;
    using System.Reflection;

    [ConfigurationCollection(typeof(ModuleElement), AddItemName="Module")]
    public class ModuleElementCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new ModuleElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ModuleElement) element).ModuleTypeCode;
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
                return "Module";
            }
        }

        public ModuleElement this[string TypeCode]
        {
            get
            {
                return (ModuleElement) base.BaseGet(TypeCode);
            }
        }
    }
}

