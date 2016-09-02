namespace WTF.Framework
{
    using System;
    using System.Collections.Concurrent;
    using System.IO;
    using System.Reflection;
    using System.Resources;
    using System.Runtime.CompilerServices;

    public static class ReflectorHelper
    {
        private static readonly ConcurrentDictionary<string, Assembly> _AssemblyCacheList = new ConcurrentDictionary<string, Assembly>();
        private static readonly ConcurrentDictionary<string, Type> _AssemblyTypeList = new ConcurrentDictionary<string, Type>();

        public static object CreateInstace(this string assemblyName, string type, params object[] args)
        {
            return Activator.CreateInstance(assemblyName.FindType(type), args);
        }

        public static T CreateInstace<T>(this string assemblyName, string type, params object[] args) where T : class
        {
            return (T)assemblyName.CreateInstace(type, args);
        }

        public static object ExecuteMethod(string assemblyName, string type, string methodname, object[] args)
        {
            object obj2 = assemblyName.CreateInstace(type, new object[0]);
            return obj2.GetType().GetMethod(methodname).Invoke(obj2, args);
        }

        public static Assembly FindAssembly(this string assemblyName)
        {
            return _AssemblyCacheList.GetOrAdd(assemblyName, assembly => Assembly.Load(assembly));
        }

        public static PropertyInfo[] FindPropertys(this Type type)
        {
            return type.GetProperties();
        }

        public static PropertyInfo[] FindPropertys(string assemblyName, string typeName)
        {
            return assemblyName.FindType(typeName).FindPropertys();
        }

        public static PropertyInfo[] FindPropertys(this Type type, BindingFlags bindingAttr)
        {
            return type.GetProperties(bindingAttr);
        }

        public static PropertyInfo[] FindPropertys(string assemblyName, string typeName, BindingFlags bindingAttr)
        {
            return assemblyName.FindType(typeName).FindPropertys(bindingAttr);
        }

        public static ResourceSet FindResourceSet(this Assembly assembly, string resourceName)
        {
            Stream manifestResourceStream = assembly.GetManifestResourceStream(resourceName);
            if (manifestResourceStream == null)
            {
                throw new ArgumentNullException("stream");
            }
            ResourceSet set = new ResourceSet(manifestResourceStream);
            manifestResourceStream.Close();
            return set;
        }

        public static Type FindType(this Assembly assembly, string typeName)
        {
            Type type = null;
            if (!_AssemblyTypeList.TryGetValue(typeName, out type))
            {
                type = assembly.GetType(typeName);
                if (type == null)
                {
                    throw new ArgumentNullException("程序集" + assembly.FullName + "找不到类型" + typeName);
                }
                _AssemblyTypeList.TryAdd(typeName, type);
            }
            return type;
        }

        public static Type FindType(this string assemblyName, string typeName)
        {
            Type type = null;
            if (!_AssemblyTypeList.TryGetValue(typeName, out type))
            {
                type = assemblyName.FindAssembly().GetType(typeName);
                if (type == null)
                {
                    throw new ArgumentNullException("程序集" + assemblyName + "找不到类型" + typeName);
                }
                _AssemblyTypeList.TryAdd(typeName, type);
            }
            return type;
        }

        private static MemberAdapter GetMemberAdapter(object instance, string propertyName)
        {
            if ((instance != null) && !string.IsNullOrEmpty(propertyName))
            {
                Type type = instance.GetType();
                PropertyInfo property = type.GetProperty(propertyName);
                if (property != null)
                {
                    return new MemberAdapter(instance, property);
                }
                FieldInfo field = type.GetField(propertyName, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
                if (field != null)
                {
                    return new MemberAdapter(instance, field);
                }
            }
            return MemberAdapter.Empty;
        }

        public static object GetPropertyValue(object instance, string propertyName)
        {
            return GetMemberAdapter(instance, propertyName).Value;
        }

        public static void SetPropertyValue(this object instance, string propertyName, object value)
        {
            MemberAdapter ma = GetMemberAdapter(instance, propertyName);
            ma.Value = value;
        }
    }
}

