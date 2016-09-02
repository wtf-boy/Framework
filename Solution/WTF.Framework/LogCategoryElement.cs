namespace WTF.Framework
{
    using System;
    using System.Configuration;

    public class LogCategoryElement : ConfigurationElement
    {
        [ConfigurationProperty("CategoryCode", IsRequired=true, IsKey=true)]
        public string CategoryCode
        {
            get
            {
                return (string) base["CategoryCode"];
            }
            set
            {
                base["CategoryCode"] = value;
            }
        }

        [ConfigurationProperty("IsRecordDB", IsRequired=false, DefaultValue=false)]
        public bool IsRecordDB
        {
            get
            {
                return (bool) base["IsRecordDB"];
            }
            set
            {
                base["IsRecordDB"] = value;
            }
        }

        [ConfigurationProperty("IsRecordEvent", IsRequired=false, DefaultValue=false)]
        public bool IsRecordEvent
        {
            get
            {
                return (bool) base["IsRecordEvent"];
            }
            set
            {
                base["IsRecordEvent"] = value;
            }
        }

        [ConfigurationProperty("IsRecordText", IsRequired=false, DefaultValue=false)]
        public bool IsRecordText
        {
            get
            {
                return (bool) base["IsRecordText"];
            }
            set
            {
                base["IsRecordText"] = value;
            }
        }

        [ConfigurationProperty("IsRecordXml", IsRequired=false, DefaultValue=false)]
        public bool IsRecordXml
        {
            get
            {
                return (bool) base["IsRecordXml"];
            }
            set
            {
                base["IsRecordXml"] = value;
            }
        }
    }
}

