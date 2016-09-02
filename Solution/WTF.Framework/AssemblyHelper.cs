using System;
using System.Reflection;
namespace WTF.Framework
{


    public static class AssemblyHelper
    {
        public static Assembly FindAssembly(string assemblyName)
        {
            return Assembly.Load(assemblyName);
        }

        public static Assembly FindAssemblyPath(string assemblyName)
        {
            string str = ConfigHelper.GetValue(assemblyName);
            if (string.IsNullOrEmpty(str))
            {
                return Assembly.Load(assemblyName);
            }
            return Assembly.LoadFile(str);
        }
    }
}

