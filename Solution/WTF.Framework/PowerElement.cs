namespace WTF.Framework
{
    using System;
    using System.Configuration;

    public class PowerElement : ConfigurationElement
    {
        [ConfigurationProperty("CachePowerMinute", IsRequired=false, DefaultValue=5)]
        public int CachePowerMinute
        {
            get
            {
                return (int) base["CachePowerMinute"];
            }
            set
            {
                base["CachePowerMinute"] = value;
            }
        }

        [ConfigurationProperty("IsCacheDataPower", IsRequired=false, DefaultValue=false)]
        public bool IsCacheDataPower
        {
            get
            {
                return (bool) base["IsCacheDataPower"];
            }
            set
            {
                base["IsCacheDataPower"] = value;
            }
        }

        [ConfigurationProperty("IsCacheOperatePower", IsRequired=false, DefaultValue=false)]
        public bool IsCacheOperatePower
        {
            get
            {
                return (bool) base["IsCacheOperatePower"];
            }
            set
            {
                base["IsCacheOperatePower"] = value;
            }
        }

        [ConfigurationProperty("IsCachePagePower", IsRequired=false, DefaultValue=false)]
        public bool IsCachePagePower
        {
            get
            {
                return (bool) base["IsCachePagePower"];
            }
            set
            {
                base["IsCachePagePower"] = value;
            }
        }

        [ConfigurationProperty("IsPowerCheck", IsRequired=false, DefaultValue=false)]
        public bool IsPowerCheck
        {
            get
            {
                return (bool) base["IsPowerCheck"];
            }
            set
            {
                base["IsPowerCheck"] = value;
            }
        }

        [ConfigurationProperty("IsPowerDataCheck", IsRequired=false, DefaultValue=false)]
        public bool IsPowerDataCheck
        {
            get
            {
                return (bool) base["IsPowerDataCheck"];
            }
            set
            {
                base["IsPowerDataCheck"] = value;
            }
        }

        [ConfigurationProperty("IsRolePowerManage", IsRequired=false, DefaultValue=true)]
        public bool IsRolePowerManage
        {
            get
            {
                return (bool) base["IsRolePowerManage"];
            }
            set
            {
                base["IsRolePowerManage"] = value;
            }
        }

        [ConfigurationProperty("LoginUrl", IsRequired=false, DefaultValue="../../Default.aspx")]
        public string LoginUrl
        {
            get
            {
                return (string) base["LoginUrl"];
            }
            set
            {
                base["LoginUrl"] = value;
            }
        }

        [ConfigurationProperty("UserType", IsRequired=false, DefaultValue="")]
        public string UserType
        {
            get
            {
                return (string) base["UserType"];
            }
            set
            {
                base["UserType"] = value;
            }
        }
    }
}

