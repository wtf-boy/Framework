namespace WTF.Framework
{
    using System;

    public class ModuleSectionHelper
    {
        public static ModuleElement GetModule(string typeCode)
        {
            return GetModulesSection().Modules[typeCode];
        }

        private static ModuleSection GetModulesSection()
        {
            try
            {
                return (ModuleSection) ConfigHelper.GetSection("Modules", "WTFConfig");
            }
            catch
            {
                return new ModuleSection();
            }
        }
    }
}

