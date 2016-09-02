namespace WTF.Framework
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    public static class ESearchIDHelper
    {
        public static PropertyInfo GetESearchIDProperty<T>() where T: class, new()
        {
            PropertyInfo[] properties = typeof(T).GetProperties();
            foreach (PropertyInfo info in properties)
            {
                if (info.GetCustomAttributes(typeof(ESearchIDAttribute), false).Any<object>())
                {
                    return info;
                }
            }
            foreach (PropertyInfo info in properties)
            {
                if (info.GetCustomAttributes(typeof(PrimaryKeyAttribute), false).Any<object>())
                {
                    return info;
                }
            }
            return null;
        }

        public static object GetESearchIDValue<T>(this T value) where T: class, new()
        {
            PropertyInfo eSearchIDProperty = GetESearchIDProperty<T>();
            if (eSearchIDProperty == null)
            {
                return null;
            }
            return eSearchIDProperty.GetValue(value, null);
        }

        public static PropertyInfo GetESearchRoutingProperty<T>() where T: class, new()
        {
            PropertyInfo[] properties = typeof(T).GetProperties();
            foreach (PropertyInfo info in properties)
            {
                if (info.GetCustomAttributes(typeof(ESearchRoutingAttribute), false).Any<object>())
                {
                    return info;
                }
            }
            return null;
        }

        public static object GetESearchRoutingValue<T>(this T value) where T: class, new()
        {
            PropertyInfo eSearchRoutingProperty = GetESearchRoutingProperty<T>();
            if (eSearchRoutingProperty == null)
            {
                return null;
            }
            return eSearchRoutingProperty.GetValue(value, null);
        }
    }
}

