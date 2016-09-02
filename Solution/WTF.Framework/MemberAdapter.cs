namespace WTF.Framework
{
    using System;
    using System.Reflection;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct MemberAdapter
    {
        private object _object;
        private PropertyInfo _PropertyInfo;
        private FieldInfo _FieldInfo;
        public static readonly MemberAdapter Empty;
        public Type MemberType
        {
            get
            {
                if (this._PropertyInfo != null)
                {
                    return this._PropertyInfo.PropertyType;
                }
                if (this._FieldInfo != null)
                {
                    return this._FieldInfo.FieldType;
                }
                return null;
            }
        }
        public object Value
        {
            get
            {
                if ((this._PropertyInfo != null) && this._PropertyInfo.CanRead)
                {
                    return this._PropertyInfo.GetValue(this._object, null);
                }
                if (this._FieldInfo != null)
                {
                    return this._FieldInfo.GetValue(this._object);
                }
                return null;
            }
            set
            {
                if ((this._PropertyInfo != null) && this._PropertyInfo.CanWrite)
                {
                    this._PropertyInfo.SetValue(this._object, value, null);
                }
                else if (this._FieldInfo != null)
                {
                    this._FieldInfo.SetValue(this._object, value);
                }
            }
        }
        public MemberAdapter(object obj, PropertyInfo propertyInfo)
        {
            this._object = obj;
            this._PropertyInfo = propertyInfo;
            this._FieldInfo = null;
        }

        public MemberAdapter(object obj, FieldInfo fieldInfoi)
        {
            this._object = obj;
            this._FieldInfo = fieldInfoi;
            this._PropertyInfo = null;
        }

        static MemberAdapter()
        {
            Empty = new MemberAdapter();
        }
    }
}

