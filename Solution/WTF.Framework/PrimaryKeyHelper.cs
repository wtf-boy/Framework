namespace WTF.Framework
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    public static class PrimaryKeyHelper
    {
        public static PrimaryKeyAttribute GetPrimaryKeyAttribute<T>() where T: class, new()
        {
            return GetPrimaryKeyProperty<T>().GetPrimaryKeyAttribute();
        }

        public static PrimaryKeyAttribute GetPrimaryKeyAttribute(this PropertyInfo value)
        {
            if (value != null)
            {
                object obj2 = value.GetCustomAttributes(typeof(PrimaryKeyAttribute), false).FirstOrDefault<object>();
                if (obj2 != null)
                {
                    PrimaryKeyAttribute attribute = (PrimaryKeyAttribute) obj2;
                    attribute.PrimaryProperty = value;
                    return attribute;
                }
            }
            return null;
        }

        public static PrimaryKeyAttribute GetPrimaryKeyAttribute<T>(this T value) where T: class, new()
        {
            foreach (PropertyInfo info in typeof(T).GetProperties())
            {
                PrimaryKeyAttribute primaryKeyAttribute = info.GetPrimaryKeyAttribute();
                if (primaryKeyAttribute != null)
                {
                    return primaryKeyAttribute;
                }
            }
            return null;
        }

        public static string GetPrimaryKeyName<T>() where T: class, new()
        {
            PropertyInfo primaryKeyProperty = GetPrimaryKeyProperty<T>();
            if (primaryKeyProperty == null)
            {
                return "";
            }
            return primaryKeyProperty.Name;
        }

        public static string GetPrimaryKeyName<T>(this T value) where T: class, new()
        {
            return GetPrimaryKeyName<T>();
        }

        public static PropertyInfo GetPrimaryKeyProperty<T>() where T: class, new()
        {
            foreach (PropertyInfo info in typeof(T).GetProperties())
            {
                if (info.GetCustomAttributes(typeof(PrimaryKeyAttribute), false).Any<object>())
                {
                    return info;
                }
            }
            return null;
        }

        public static PropertyInfo GetPrimaryKeyProperty<T>(this T value) where T: class, new()
        {
            return GetPrimaryKeyProperty<T>();
        }

        public static object GetPrimaryKeyValue<T>(this T value) where T: class, new()
        {
            PropertyInfo primaryKeyProperty = GetPrimaryKeyProperty<T>();
            if (primaryKeyProperty == null)
            {
                return null;
            }
            return primaryKeyProperty.GetValue(value, null);
        }
    }
}

