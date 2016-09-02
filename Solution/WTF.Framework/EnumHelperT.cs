namespace WTF.Framework
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class EnumHelperT
    {
        private List<EnumParameter> _EnumParameterList;

        public EnumHelperT(Enum objEnum) : this(objEnum.GetType())
        {
        }

        public EnumHelperT(Type enumType)
        {
            this._EnumParameterList = null;
            this._EnumParameterList = new List<EnumParameter>();
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
                        this._EnumParameterList.Add(item);
                        break;
                    }
                }
            }
        }

        public string GetEnumDescription(int objEnumValue)
        {
            EnumParameter parameter = this._EnumParameterList.FirstOrDefault<EnumParameter>(p => p.Value == objEnumValue);
            if (parameter != null)
            {
                return parameter.Description;
            }
            return string.Empty;
        }

        public string GetEnumDescription(string objEnumKey)
        {
            EnumParameter parameter = this._EnumParameterList.FirstOrDefault<EnumParameter>(p => p.Key == objEnumKey);
            if (parameter != null)
            {
                return parameter.Description;
            }
            return string.Empty;
        }

        public List<EnumParameter> EnumMembers
        {
            get
            {
                return this._EnumParameterList;
            }
        }
    }
}

