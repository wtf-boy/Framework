using System;
using System.Collections.Concurrent;
using System.Reflection;

namespace WTF.Framework
{

    public static class AssemblyManager
    {
        private static ConcurrentDictionary<string, Assembly> _AssemblyList = new ConcurrentDictionary<string, Assembly>();
        private static object AssemblyLock = new object();

        public static Assembly CacheAssembly(string assemblyName)
        {
            Assembly assembly = null;
            if (!_AssemblyList.TryGetValue(assemblyName, out assembly))
            {
                assembly = AssemblyHelper.FindAssembly(assemblyName);
                _AssemblyList.TryAdd(assemblyName, assembly);
            }
            return assembly;
        }
    }
}

