using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
namespace WTF.Framework
{
    public static class ObjectHelper
    {
        public static object ChangeType(this object value, Type conversionType)
        {
            if (conversionType.IsGenericType && conversionType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                if (value == null)
                {
                    return null;
                }
                NullableConverter converter = new NullableConverter(conversionType);
                conversionType = converter.UnderlyingType;
            }
            return Convert.ChangeType(value, conversionType);
        }

        public static C Clone<T, C>(this T cloneValue) where C: class, new()
        {
            C instance = Activator.CreateInstance<C>();
            foreach (PropertyInfo info in typeof(T).FindPropertys())
            {
                if (info.CanRead && info.CanWrite)
                {
                    instance.SetPropertyValue(info.Name, ReflectorHelper.GetPropertyValue(cloneValue, info.Name));
                }
            }
            foreach (FieldInfo info2 in typeof(T).GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance))
            {
                instance.SetPropertyValue(info2.Name, ReflectorHelper.GetPropertyValue(cloneValue, info2.Name));
            }
            return instance;
        }

        public static void Clone<C, T>(this C clone, T value) where C: class where T: class
        {
            foreach (PropertyInfo info in typeof(C).GetProperties())
            {
                if (info.CanRead && info.CanWrite)
                {
                    value.SetPropertyValue(info.Name, ReflectorHelper.GetPropertyValue(clone, info.Name));
                }
            }
            foreach (FieldInfo info2 in typeof(C).GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance))
            {
                value.SetPropertyValue(info2.Name, ReflectorHelper.GetPropertyValue(clone, info2.Name));
            }
        }

        public static bool ConvertBool(this object value)
        {
            if (value == null)
            {
                return false;
            }
            if (value is int)
            {
                return (((int) value) != 0);
            }
            if (!(value is string) && (value is bool))
            {
                return (bool) value;
            }
            return ((value.ToString().ToLower() == "true") || (value.ToString().ToLower() == "1"));
        }

        public static string ConvertDate(this object value, string format)
        {
            if (value is DateTime)
            {
                DateTime time = (DateTime) value;
                return time.ToString(format);
            }
            return "此对象不是DateTime类型";
        }

        public static decimal ConvertDecimal(this object value)
        {
            return value.ToString().ConvertDecimal();
        }

        public static int ConvertInt(this object value)
        {
            if (value == null)
            {
                return 0;
            }
            if (value is int)
            {
                return (int) value;
            }
            if (value is bool)
            {
                return (((bool) value) ? 1 : 0);
            }
            return value.ToString().ConvertInt();
        }

        public static List<int> ConvertListInt(this IEnumerable<object> ValueList)
        {
            List<int> list = new List<int>();
            if ((ValueList != null) && (ValueList.Count<object>() != 0))
            {
                foreach (object obj2 in ValueList)
                {
                    list.Add(Convert.ToInt32(obj2));
                }
            }
            return list;
        }

        public static List<string> ConvertListString(this IEnumerable<object> ValueList)
        {
            List<string> list = new List<string>();
            if ((ValueList != null) && (ValueList.Count<object>() != 0))
            {
                foreach (object obj2 in ValueList)
                {
                    list.Add(Convert.ToString(obj2));
                }
            }
            return list;
        }

        public static long ConvertLong(this object value)
        {
            if (value == null)
            {
                return 0L;
            }
            if (value is int)
            {
                return long.Parse(value.ToString());
            }
            if (value is long)
            {
                return long.Parse(value.ToString());
            }
            if (value is bool)
            {
                return (((bool) value) ? ((long) 1) : ((long) 0));
            }
            return value.ToString().ConvertLong();
        }

        public static string ConvertString(this object value)
        {
            try
            {
                return ((value == null) ? "" : value.ToString());
            }
            catch
            {
                return "";
            }
        }

        public static string CutText(this object textData, int Length)
        {
            return textData.CutText(Length, false);
        }

        public static string CutText(this object textData, int Length, bool Flag)
        {
            return textData.ConvertString().CutText(Length, Flag);
        }

        public static string DataConvertString(this object value)
        {
            if (value == null)
            {
                return "";
            }
            if (value is bool)
            {
                return (((bool) value) ? "1" : "0");
            }
            return value.ToString();
        }

        public static bool IndexListOf(this object o, IEnumerable b)
        {
            foreach (object obj2 in b)
            {
                if (obj2 == o)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool IsDBNull(this object value)
        {
            return ((value == null) || (value == DBNull.Value));
        }

        public static bool IsNoNull(this object instance)
        {
            if (instance != null)
            {
                if (instance is string)
                {
                    return instance.ToString().IsNoNull();
                }
                if (instance is bool)
                {
                    return !instance.Equals(NullBoolean);
                }
                if (instance is Guid)
                {
                    return !instance.Equals(NullGuid);
                }
                if (instance is int)
                {
                    return !instance.Equals(NullInt);
                }
                if (instance is long)
                {
                    return !instance.Equals(NullLong);
                }
                if (instance is float)
                {
                    return !instance.Equals(NullSingle);
                }
                if (instance is DateTime)
                {
                    DateTime time = (DateTime) instance;
                    return !time.Date.Equals(NullDate.Date);
                }
                if (instance is double)
                {
                    return !instance.Equals(NullDouble);
                }
                if (instance is decimal)
                {
                    return !instance.Equals(NullDecimal);
                }
                if (instance is float)
                {
                    return !instance.Equals(NullFloat);
                }
                return true;
            }
            return false;
        }

        public static bool IsNull(this object instance)
        {
            if (instance != null)
            {
                if (instance is string)
                {
                    return instance.ToString().IsNull();
                }
                if (instance is int)
                {
                    return instance.Equals(NullInt);
                }
                if (instance is long)
                {
                    return instance.Equals(NullLong);
                }
                if (instance is Guid)
                {
                    return instance.Equals(NullGuid);
                }
                if (instance is DateTime)
                {
                    DateTime time = (DateTime) instance;
                    return time.Date.Equals(NullDate.Date);
                }
                if (instance is bool)
                {
                    return instance.Equals(NullBoolean);
                }
                if (instance is float)
                {
                    return instance.Equals(NullSingle);
                }
                if (instance is double)
                {
                    return instance.Equals(NullDouble);
                }
                if (instance is decimal)
                {
                    return instance.Equals(NullDecimal);
                }
                return ((instance is float) && instance.Equals(NullFloat));
            }
            return true;
        }

        public static bool IsNullOrEmpty(this object[] value)
        {
            return ((value == null) || (value.Length == 0));
        }

        public static bool IsTheRightType(this object value, string typeName)
        {
            if (value == null)
            {
                return false;
            }
            return (value.GetType() == Type.GetType(typeName));
        }

        public static Dictionary<string, object> PropertyDictionary<T>(this T value) where T: class
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            PropertyInfo[] properties = typeof(T).GetProperties();
            foreach (PropertyInfo info in properties)
            {
                if (info.Name.ToLower() != "_id")
                {
                    dictionary.Add(info.Name, info.GetValue(value, null));
                }
            }
            return dictionary;
        }

        public static bool NullBoolean
        {
            get
            {
                return false;
            }
        }

        public static byte NullByte
        {
            get
            {
                return 0;
            }
        }

        public static DateTime NullDate
        {
            get
            {
                return Convert.ToDateTime("1900-01-01 00:00:00");
            }
        }

        public static decimal NullDecimal
        {
            get
            {
                return -79228162514264337593543950335M;
            }
        }

        public static double NullDouble
        {
            get
            {
                return double.MinValue;
            }
        }

        public static float NullFloat
        {
            get
            {
                return float.MinValue;
            }
        }

        public static Guid NullGuid
        {
            get
            {
                return Guid.Empty;
            }
        }

        public static int NullInt
        {
            get
            {
                return -2147483648;
            }
        }

        public static long NullLong
        {
            get
            {
                return -9223372036854775808L;
            }
        }

        public static short NullShort
        {
            get
            {
                return -32768;
            }
        }

        public static float NullSingle
        {
            get
            {
                return float.MinValue;
            }
        }

        public static string NullString
        {
            get
            {
                return string.Empty;
            }
        }
    }
}

