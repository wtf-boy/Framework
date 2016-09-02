namespace WTF.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;

    [DataObject]
    public static class CodeHelper
    {
        public static string GetCodeDescription(this Enum objEnum)
        {
            return objEnum.GetType().GetCodeParameters().Single<CodeParameter>(p => (p.Key == objEnum.ToString())).Description;
        }

        public static string GetCodeDescription<T>(string objEnumKey)
        {
            return typeof(T).GetCodeParameters().Single<CodeParameter>(p => (p.Key == objEnumKey)).Description;
        }

        public static string GetCodeDescription(this Type type, string codeValue)
        {
            return type.GetCodeParameters().Single<CodeParameter>(p => (p.CodeValue == codeValue)).Description;
        }

        public static List<CodeParameter> GetCodeParameters(this Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type", "输入的枚举类型为空");
            }
            List<CodeParameter> list = new List<CodeParameter>();
            foreach (string str in Enum.GetNames(type))
            {
                foreach (Attribute attribute in type.GetField(str).GetCustomAttributes(typeof(CodeAttribute), false))
                {
                    CodeAttribute attribute2 = attribute as CodeAttribute;
                    if (attribute2 != null)
                    {
                        CodeParameter item = new CodeParameter {
                            Key = str,
                            Description = attribute2.Description,
                            CodeValue = attribute2.CodeValue,
                            Value = Convert.ToInt32(Enum.Parse(type, str))
                        };
                        list.Add(item);
                        break;
                    }
                }
            }
            return list;
        }

        public static List<CodeParameter> GetCodeParameters(this string typeName, string assemblyName)
        {
            Type enumType = assemblyName.FindType(typeName);
            if (enumType == null)
            {
                throw new ArgumentNullException("typeName", "输入的类型系统无法找到请重新输入");
            }
            List<CodeParameter> list = new List<CodeParameter>();
            foreach (string str in Enum.GetNames(enumType))
            {
                foreach (Attribute attribute in enumType.GetField(str).GetCustomAttributes(typeof(CodeAttribute), false))
                {
                    CodeAttribute attribute2 = attribute as CodeAttribute;
                    if (attribute2 != null)
                    {
                        CodeParameter item = new CodeParameter {
                            Key = str,
                            Description = attribute2.Description,
                            CodeValue = attribute2.CodeValue,
                            Value = Convert.ToInt32(Enum.Parse(enumType, str))
                        };
                        list.Add(item);
                        break;
                    }
                }
            }
            return (from s in list
                orderby s.CodeValue
                select s).ToList<CodeParameter>();
        }

        public static string GetCodeValue(this Enum objEnum)
        {
            return objEnum.GetType().GetCodeParameters().Single<CodeParameter>(p => (p.Key == objEnum.ToString())).CodeValue;
        }

        public static T GetObject<T>(string CodeValue)
        {
            Type type = typeof(T);
            CodeParameter parameter = type.GetCodeParameters().Single<CodeParameter>(p => p.CodeValue == CodeValue);
            return (T) Enum.ToObject(type, parameter.Value);
        }

        public static T GetObject<T>(this Type enumType, string CodeValue)
        {
            CodeParameter parameter = enumType.GetType().GetCodeParameters().Single<CodeParameter>(p => p.CodeValue == CodeValue);
            return (T) Enum.ToObject(enumType.GetType(), parameter.Value);
        }
    }
}

