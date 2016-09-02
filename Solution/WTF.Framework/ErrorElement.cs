namespace WTF.Framework
{
    using System;
    using System.Configuration;

    public class ErrorElement : ConfigurationElement
    {
        [ConfigurationProperty("ErrorHint", IsRequired=false, DefaultValue="对不起页面出错，请与管理员联系")]
        public string ErrorHint
        {
            get
            {
                return (string) base["ErrorHint"];
            }
            set
            {
                base["ErrorHint"] = value;
            }
        }

        [ConfigurationProperty("ErrorIsRedirect", IsRequired=false, DefaultValue=false)]
        public bool ErrorIsRedirect
        {
            get
            {
                return (bool) base["ErrorIsRedirect"];
            }
            set
            {
                base["ErrorIsRedirect"] = value;
            }
        }

        [ConfigurationProperty("ErrorNoFoundUrl", IsRequired=false, DefaultValue="404.htm")]
        public string ErrorNoFoundUrl
        {
            get
            {
                return (string) base["ErrorNoFoundUrl"];
            }
            set
            {
                base["ErrorNoFoundUrl"] = value;
            }
        }

        [ConfigurationProperty("ErrorUrl", IsRequired=false, DefaultValue="Error.htm")]
        public string ErrorUrl
        {
            get
            {
                return (string) base["ErrorUrl"];
            }
            set
            {
                base["ErrorUrl"] = value;
            }
        }
    }
}

