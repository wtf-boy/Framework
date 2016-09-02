namespace WTF.Framework
{
    using System;
    using System.Configuration;

    public class ModuleStyleElement : ConfigurationElement
    {
        [ConfigurationProperty("LayoutTheme", IsRequired = false, DefaultValue = "Default")]
        public string LayoutTheme
        {
            get
            {
                return (string)base["LayoutTheme"];
            }
            set
            {
                base["LayoutTheme"] = value;
            }
        }

        [ConfigurationProperty("OperateStyle", IsRequired = false, DefaultValue = OperateStyle.RightOperate)]
        public WTF.Framework.OperateStyle OperateStyle
        {
            get
            {
                return (WTF.Framework.OperateStyle)base["OperateStyle"];
            }
            set
            {
                base["OperateStyle"] = value;
            }
        }

        [ConfigurationProperty("StyleTheme", IsRequired = false, DefaultValue = "Default")]
        public string StyleTheme
        {
            get
            {
                return (string)base["StyleTheme"];
            }
            set
            {
                base["StyleTheme"] = value;
            }
        }
    }
}

