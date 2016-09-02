namespace WTF.Framework
{
    using System;
    using System.Configuration;

    public class ModuleElement : ConfigurationElement
    {
        [ConfigurationProperty("Error", IsRequired=false)]
        public ErrorElement Error
        {
            get
            {
                return (ErrorElement) base["Error"];
            }
            set
            {
                base["Error"] = value;
            }
        }

        [ConfigurationProperty("IsCacheLog", IsRequired=false, DefaultValue=false)]
        public bool IsCacheLog
        {
            get
            {
                return (bool) base["IsCacheLog"];
            }
            set
            {
                base["IsCacheLog"] = value;
            }
        }

        [ConfigurationProperty("ModuleStyle", IsRequired=false)]
        public ModuleStyleElement ModuleStyle
        {
            get
            {
                return (ModuleStyleElement) base["ModuleStyle"];
            }
            set
            {
                base["ModuleStyle"] = value;
            }
        }

        [ConfigurationProperty("ModuleTypeCode", IsRequired=true, IsKey=true)]
        public string ModuleTypeCode
        {
            get
            {
                return (string) base["ModuleTypeCode"];
            }
            set
            {
                base["ModuleTypeCode"] = value;
            }
        }

        [ConfigurationProperty("ModuleTypeID", IsRequired=false, DefaultValue="")]
        public string ModuleTypeID
        {
            get
            {
                return (string) base["ModuleTypeID"];
            }
            set
            {
                base["ModuleTypeID"] = value;
            }
        }

        [ConfigurationProperty("Page", IsRequired=false)]
        public PageElement Page
        {
            get
            {
                return (PageElement) base["Page"];
            }
            set
            {
                base["Page"] = value;
            }
        }

        [ConfigurationProperty("Power", IsRequired=false)]
        public PowerElement Power
        {
            get
            {
                return (PowerElement) base["Power"];
            }
            set
            {
                base["Power"] = value;
            }
        }

        [ConfigurationProperty("SystemName", IsRequired=false, DefaultValue="管理系统")]
        public string SystemName
        {
            get
            {
                return (string) base["SystemName"];
            }
            set
            {
                base["SystemName"] = value;
            }
        }
    }
}

