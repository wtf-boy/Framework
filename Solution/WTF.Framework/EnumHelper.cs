namespace WTF.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Web.UI.WebControls;

    [DataObject]
    public static class EnumHelper
    {
        public static void BindEnumControl(this ListControl bindControl, Enum objEnum, HeaderType headerType = 0)
        {
            bindControl.BindEnumControl(objEnum, ShowEnumType.Description, ShowEnumType.Value, headerType);
        }

        public static void BindEnumControl(this ListControl bindControl, Type enumType, HeaderType headerType = 0)
        {
            bindControl.BindEnumControl(enumType, ShowEnumType.Description, ShowEnumType.Value, headerType);
        }

        public static void BindEnumControl(this ListControl bindControl, Enum objEnum, ShowEnumType text, ShowEnumType value, HeaderType headerType = 0)
        {
            bindControl.BindEnumControl(objEnum.GetType(), text, value, headerType);
        }

        public static void BindEnumControl(this ListControl bindControl, Type enumType, ShowEnumType text, ShowEnumType value, HeaderType headerType = 0)
        {
            bindControl.Items.Clear();
            if (headerType != HeaderType.None)
            {
                bindControl.Items.Add(new ListItem(headerType.GetEnumDescription(), ""));
            }
            foreach (EnumParameter parameter in from s in enumType.GetEnumMembers()
                orderby s.Value
                select s)
            {
                bindControl.Items.Add(new ListItem(GetShowText(parameter, text), GetShowText(parameter, value)));
            }
        }

        public static string GetEnumDescription(this Enum objEnum)
        {
            EnumParameter parameter = objEnum.GetType().GetEnumMembers().FirstOrDefault<EnumParameter>(p => p.Key == objEnum.ToString());
            if (parameter != null)
            {
                return parameter.Description;
            }
            return string.Empty;
        }

        public static string GetEnumDescription(this Type type, int objEnumValue)
        {
            EnumParameter parameter = type.GetEnumMembers().FirstOrDefault<EnumParameter>(p => p.Value == objEnumValue);
            if (parameter != null)
            {
                return parameter.Description;
            }
            return string.Empty;
        }

        public static string GetEnumDescription(this Type type, string objEnumKey)
        {
            EnumParameter parameter = type.GetEnumMembers().FirstOrDefault<EnumParameter>(p => p.Key == objEnumKey);
            if (parameter != null)
            {
                return parameter.Description;
            }
            return string.Empty;
        }

        public static string GetEnumDescription(this string typeName, string assemblyName, int objEnumValue)
        {
            EnumParameter parameter = typeName.GetEnumMembers(assemblyName).FirstOrDefault<EnumParameter>(p => p.Value == objEnumValue);
            if (parameter != null)
            {
                return parameter.Description;
            }
            return string.Empty;
        }

        public static string GetEnumDescription(this string typeName, string assemblyName, string objEnumKey)
        {
            EnumParameter parameter = typeName.GetEnumMembers(assemblyName).FirstOrDefault<EnumParameter>(p => p.Key == objEnumKey);
            if (parameter != null)
            {
                return parameter.Description;
            }
            return string.Empty;
        }

        public static List<EnumParameter> GetEnumMembers(this Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type", "输入的枚举类型为空");
            }
            List<EnumParameter> list = new List<EnumParameter>();
            foreach (string str in Enum.GetNames(type))
            {
                foreach (Attribute attribute in type.GetField(str).GetCustomAttributes(typeof(EnumAttribute), false))
                {
                    EnumAttribute attribute2 = attribute as EnumAttribute;
                    if (attribute2 != null)
                    {
                        EnumParameter item = new EnumParameter {
                            Key = str,
                            Description = attribute2.Description
                        };
                        if (attribute2.Value == -2147483648)
                        {
                            item.Value = Convert.ToInt32(Enum.Parse(type, str));
                        }
                        else
                        {
                            item.Value = attribute2.Value;
                        }
                        list.Add(item);
                        break;
                    }
                }
            }
            return list;
        }

        public static List<EnumParameter> GetEnumMembers(this string typeName, string assemblyName)
        {
            Type enumType = assemblyName.FindType(typeName);
            if (enumType == null)
            {
                throw new ArgumentNullException("typeName", "输入的类型系统无法找到请重新输入");
            }
            List<EnumParameter> list = new List<EnumParameter>();
            foreach (string str in Enum.GetNames(enumType))
            {
                foreach (Attribute attribute in enumType.GetField(str).GetCustomAttributes(typeof(EnumAttribute), false))
                {
                    EnumAttribute attribute2 = attribute as EnumAttribute;
                    if (attribute2 != null)
                    {
                        EnumParameter item = new EnumParameter {
                            Key = str,
                            Description = attribute2.Description
                        };
                        if (attribute2.Value == -2147483648)
                        {
                            item.Value = Convert.ToInt32(Enum.Parse(enumType, str));
                        }
                        else
                        {
                            item.Value = attribute2.Value;
                        }
                        list.Add(item);
                        break;
                    }
                }
            }
            return (from s in list
                orderby s.Value
                select s).ToList<EnumParameter>();
        }

        public static int GetEnumValue(this string typeName, string assemblyName, string objEnumKey)
        {
            EnumParameter parameter = typeName.GetEnumMembers(assemblyName).FirstOrDefault<EnumParameter>(p => p.Key == objEnumKey);
            if (parameter != null)
            {
                return parameter.Value;
            }
            return -2147483648;
        }

        private static string GetShowText(EnumParameter objEnumParameter, ShowEnumType show)
        {
            if (show == ShowEnumType.Key)
            {
                return objEnumParameter.Key;
            }
            if (show == ShowEnumType.Description)
            {
                return objEnumParameter.Description;
            }
            return objEnumParameter.Value.ToString();
        }

        public static T ParseEnum<T>(this string value, bool ignoreCase = true)
        {
            return (T) Enum.Parse(typeof(T), value.Trim(), ignoreCase);
        }
    }
}

