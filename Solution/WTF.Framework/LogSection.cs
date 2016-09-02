namespace WTF.Framework
{
    using System;
    using System.Configuration;

    public class LogSection : ConfigurationSection
    {
        [ConfigurationProperty("Application", IsRequired=false, DefaultValue="")]
        public string Application
        {
            get
            {
                return (string) base["Application"];
            }
            set
            {
                base["Application"] = value;
            }
        }

        [ConfigurationProperty("", IsDefaultCollection=true)]
        public LogCategoryCollection Categorys
        {
            get
            {
                return (LogCategoryCollection) base[""];
            }
        }

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

        [ConfigurationProperty("Host", IsRequired=false, DefaultValue="")]
        public string Host
        {
            get
            {
                return (string) base["Host"];
            }
            set
            {
                base["Host"] = value;
            }
        }

        [ConfigurationProperty("IsDispose", IsRequired=false, DefaultValue=true)]
        public bool IsDispose
        {
            get
            {
                return (bool) base["IsDispose"];
            }
            set
            {
                base["IsDispose"] = value;
            }
        }

        [ConfigurationProperty("IsRecordESearch", IsRequired=false, DefaultValue=false)]
        public bool IsRecordESearch
        {
            get
            {
                return (bool) base["IsRecordESearch"];
            }
            set
            {
                base["IsRecordESearch"] = value;
            }
        }

        [ConfigurationProperty("IsRecordOperation", IsRequired=false, DefaultValue=false)]
        public bool IsRecordOperation
        {
            get
            {
                return (bool) base["IsRecordOperation"];
            }
            set
            {
                base["IsRecordOperation"] = value;
            }
        }

        [ConfigurationProperty("IsRecordSolrSql", IsRequired=false, DefaultValue=false)]
        public bool IsRecordSolrSql
        {
            get
            {
                return (bool) base["IsRecordSolrSql"];
            }
            set
            {
                base["IsRecordSolrSql"] = value;
            }
        }

        [ConfigurationProperty("IsRecordSql", IsRequired=false, DefaultValue=false)]
        public bool IsRecordSql
        {
            get
            {
                return (bool) base["IsRecordSql"];
            }
            set
            {
                base["IsRecordSql"] = value;
            }
        }

        [ConfigurationProperty("LogWriteMap", IsRequired=false, DefaultValue="")]
        public string LogWriteMap
        {
            get
            {
                return (string) base["LogWriteMap"];
            }
            set
            {
                base["LogWriteMap"] = value;
            }
        }
    }
}

