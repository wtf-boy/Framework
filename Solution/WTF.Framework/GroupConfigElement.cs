namespace WTF.Framework
{
    using System;
    using System.Configuration;

    public class GroupConfigElement : ConfigurationElement
    {
        [ConfigurationProperty("", IsDefaultCollection=true)]
        public GroupValueCollection GroupValues
        {
            get
            {
                return (GroupValueCollection) base[""];
            }
        }

        [ConfigurationProperty("name", IsRequired=true, IsKey=true)]
        public string name
        {
            get
            {
                return (string) base["name"];
            }
            set
            {
                base["name"] = value;
            }
        }
    }
}

