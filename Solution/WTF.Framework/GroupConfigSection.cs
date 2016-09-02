namespace WTF.Framework
{
    using System.Configuration;

    public class GroupConfigSection : ConfigurationSection
    {
        [ConfigurationProperty("", IsDefaultCollection=true)]
        public GroupConfigCollection Groups
        {
            get
            {
                return (GroupConfigCollection) base[""];
            }
        }
    }
}

