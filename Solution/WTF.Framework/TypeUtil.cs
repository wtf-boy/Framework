namespace WTF.Framework
{
    using System;
    using System.ComponentModel;

    public class TypeUtil
    {
        public static Type GetUnNullableType(Type conversionType)
        {
            if (conversionType.IsGenericType && conversionType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                NullableConverter converter = new NullableConverter(conversionType);
                conversionType = converter.UnderlyingType;
            }
            return conversionType;
        }
    }
}

