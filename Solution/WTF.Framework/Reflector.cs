namespace WTF.Framework
{
    using System;
    using System.Collections;
    using System.Reflection;
    using System.Resources;

    [Obsolete("请使用ReflectorHelper调用")]
    public class Reflector
    {
        public static object CreateInstace(string type, string assemblyName)
        {
            return assemblyName.CreateInstace(type, new object[0]);
        }

        public static T CreateInstace<T>(string type, string assemblyName) where T: class
        {
            return assemblyName.CreateInstace<T>(type, new object[0]);
        }

        public static object CreateInstace(string type, string assemblyName, object[] args)
        {
            return assemblyName.CreateInstace(type, args);
        }

        public static T CreateInstace<T>(string type, string assemblyName, object[] args) where T: class
        {
            return assemblyName.CreateInstace<T>(type, args);
        }

        public static object ExecuteMethod(string assemblyName, string type, string methodname, object[] args)
        {
            return ReflectorHelper.ExecuteMethod(assemblyName, type, methodname, args);
        }

        public static Attribute FindAttribute(Enum instance, Type attributeType)
        {
            MemberInfo[] member = instance.GetType().GetMember(instance.ToString());
            if ((member != null) && (member.Length > 0))
            {
                MemberInfo memberInfo = member[0];
                IList list = FindAttributes(false, memberInfo, new Type[] { attributeType });
                if ((list != null) && (list.Count > 0))
                {
                    return (list[0] as Attribute);
                }
            }
            return null;
        }

        public static IList FindAttributes(bool allowDuplicates, MemberInfo memberInfo, params Type[] attributeTypes)
        {
            IList list = new ArrayList();
            foreach (Type type in attributeTypes)
            {
                object[] customAttributes = memberInfo.GetCustomAttributes(type, true);
                int num = allowDuplicates ? customAttributes.Length : 1;
                if ((customAttributes != null) && (customAttributes.Length > 0))
                {
                    for (int i = 0; i < num; i++)
                    {
                        list.Add(customAttributes[i]);
                    }
                }
            }
            return list;
        }

        public static PropertyInfo[] FindPropertys(Type type)
        {
            return type.FindPropertys();
        }

        public static PropertyInfo[] FindPropertys(string assemblyName, string typeName)
        {
            return ReflectorHelper.FindPropertys(assemblyName, typeName);
        }

        public static PropertyInfo[] FindPropertys(Type type, BindingFlags bindingAttr)
        {
            return type.FindPropertys(bindingAttr);
        }

        public static PropertyInfo[] FindPropertys(string assemblyName, string typeName, BindingFlags bindingAttr)
        {
            return ReflectorHelper.FindPropertys(assemblyName, typeName, bindingAttr);
        }

        public static ResourceSet FindResourceSet(Assembly assembly, string resourceName)
        {
            return assembly.FindResourceSet(resourceName);
        }

        public static Type FindType(Assembly assembly, string typeName)
        {
            return assembly.FindType(typeName);
        }

        public static Type FindType(string assemblyName, string typeName)
        {
            return assemblyName.FindType(typeName);
        }

        public static object GetPropertyValue(object instance, string propertyName)
        {
            return ReflectorHelper.GetPropertyValue(instance, propertyName);
        }

        public static void SetPropertyValue(object instance, string propertyName, object value)
        {
            instance.SetPropertyValue(propertyName, value);
        }
    }
}

