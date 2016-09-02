namespace WTF.Framework
{
    using System.Configuration;

    public class ModuleSection : ConfigurationSection
    {
        [ConfigurationProperty("", IsDefaultCollection=true)]
        public ModuleElementCollection Modules
        {
            get
            {
                return (ModuleElementCollection) base[""];
            }
        }
    }
}

