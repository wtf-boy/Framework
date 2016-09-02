namespace WTF.Framework
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public class TypeAccessor
    {
        private static Dictionary<System.Type, TypeAccessor> accessors = new Dictionary<System.Type, TypeAccessor>();
        private Dictionary<string, Action<object, object>> cloneFieldDic;
        private Action<object, object, Func<object, object>> cloneFields;
        private Dictionary<string, Action<object, object>> clonePropertyDic;
        private IList<Action<object, object>> fieldCloners;
        private Dictionary<string, Func<object, object>> fieldGetterDic;
        private Dictionary<string, Action<object, object>> fieldSetterDic;
        private Dictionary<string, Func<object, object>> propertyGetterDic;
        private Dictionary<string, Action<object, object>> propertySetterDic;
        private IList<PropertyInfo> readWriteProperties;
        private IList<Func<object, object>> readWritePropertyGetters;
        private IList<Action<object, object>> readWritePropertySetters;
        private IList<Action<object, object>> readWriterPropertyCloners;
        private System.Type type;

        private TypeAccessor(System.Type type)
        {
            Func<PropertyInfo, Func<object, object>> selector = null;
            Func<PropertyInfo, Action<object, object>> func2 = null;
            Func<PropertyInfo, Action<object, object>> func3 = null;
            Func<FieldInfo, Action<object, object>> func4 = null;
            Func<FieldInfo, Func<object, object>> func5 = null;
            Func<FieldInfo, Action<object, object>> func6 = null;
            this.type = type;
            this.propertyGetterDic = new Dictionary<string, Func<object, object>>();
            this.propertySetterDic = new Dictionary<string, Action<object, object>>();
            List<PropertyInfo> list = (from o in type.GetProperties(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance)
                where o.GetIndexParameters().Length == 0
                select o).ToList<PropertyInfo>();
            this.propertyGetterDic = (from o in list
                where o.CanRead
                select o).ToDictionary<PropertyInfo, string, Func<object, object>>(o => o.Name, o => DelegateAccessor.CreatePropertyGetter(o));
            this.propertySetterDic = (from o in list
                where o.CanWrite
                select o).ToDictionary<PropertyInfo, string, Action<object, object>>(o => o.Name, o => DelegateAccessor.CreatePropertySetter(o));
            this.readWriteProperties = (from o in list
                where o.CanRead && o.CanWrite
                select o).ToList<PropertyInfo>();
            if (selector == null)
            {
                selector = o => this.propertyGetterDic[o.Name];
            }
            this.readWritePropertyGetters = this.readWriteProperties.Select<PropertyInfo, Func<object, object>>(selector).ToList<Func<object, object>>();
            if (func2 == null)
            {
                func2 = o => this.propertySetterDic[o.Name];
            }
            this.readWritePropertySetters = this.readWriteProperties.Select<PropertyInfo, Action<object, object>>(func2).ToList<Action<object, object>>();
            this.clonePropertyDic = this.readWriteProperties.ToDictionary<PropertyInfo, string, Action<object, object>>(o => o.Name, o => DelegateAccessor.CreatePropertyCloner(o));
            if (func3 == null)
            {
                func3 = o => this.clonePropertyDic[o.Name];
            }
            this.readWriterPropertyCloners = this.readWriteProperties.Select<PropertyInfo, Action<object, object>>(func3).ToList<Action<object, object>>();
            List<FieldInfo> source = type.GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance).ToList<FieldInfo>();
            this.fieldGetterDic = source.ToDictionary<FieldInfo, string, Func<object, object>>(o => o.Name, o => DelegateAccessor.CreateFieldGetter(o));
            this.fieldSetterDic = source.ToDictionary<FieldInfo, string, Action<object, object>>(o => o.Name, o => DelegateAccessor.CreateFieldSetter(o));
            this.cloneFieldDic = (from o in source
                where !o.IsInitOnly
                select o).ToDictionary<FieldInfo, string, Action<object, object>>(o => o.Name, o => DelegateAccessor.CreateFieldCloner(o));
            if (func4 == null)
            {
                func4 = o => this.cloneFieldDic[o.Name];
            }
            this.fieldCloners = (from o in source
                where !o.IsInitOnly
                select o).Select<FieldInfo, Action<object, object>>(func4).ToList<Action<object, object>>();
            if (func5 == null)
            {
                func5 = o => this.fieldGetterDic[o.Name];
            }
            Func<object, object>[] cloneGetters = source.Select<FieldInfo, Func<object, object>>(func5).ToArray<Func<object, object>>();
            if (func6 == null)
            {
                func6 = o => this.fieldSetterDic[o.Name];
            }
            Action<object, object>[] cloneSetters = source.Select<FieldInfo, Action<object, object>>(func6).ToArray<Action<object, object>>();
            this.cloneFields = delegate (object delegatesource, object target, Func<object, object> fieldHandler) {
                for (int j = 0; j < cloneGetters.Length; j++)
                {
                    object obj2 = fieldHandler(cloneGetters[j](source));
                    cloneSetters[j](target, obj2);
                }
            };
        }

        public void CloneByFields(object source, object target, Func<object, object> fieldValueHandler)
        {
            this.cloneFields(source, target, fieldValueHandler);
        }

        public object Create()
        {
            return FastActivator.Create(this.type);
        }

        public static TypeAccessor GetAccessor(System.Type type)
        {
            TypeAccessor accessor;
            if (!accessors.TryGetValue(type, out accessor))
            {
                lock (accessors)
                {
                    if (!accessors.TryGetValue(type, out accessor))
                    {
                        accessor = new TypeAccessor(type);
                        accessors.Add(type, accessor);
                    }
                }
            }
            return accessor;
        }

        public object GetField(string fieldName, object instance)
        {
            Func<object, object> func;
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            if (!this.fieldGetterDic.TryGetValue(fieldName, out func))
            {
                throw new ArgumentOutOfRangeException("fieldName", string.Format("对象 {0} 没有命名为 {1} 的字段", instance.GetType().FullName, fieldName));
            }
            return func(instance);
        }

        public Action<object, object> GetFieldClone(string fieldName)
        {
            return this.cloneFieldDic[fieldName];
        }

        public Func<object, object> GetFieldGetter(string propertyName)
        {
            return this.fieldGetterDic[propertyName];
        }

        public Action<object, object> GetFieldSetter(string propertyName)
        {
            return this.fieldSetterDic[propertyName];
        }

        public IDictionary<string, object> GetFieldValueDictionary(object instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            return this.fieldGetterDic.ToDictionary<KeyValuePair<string, Func<object, object>>, string, object>(o => o.Key, o => o.Value(instance));
        }

        public object GetProperty(string propertyName, object instance)
        {
            Func<object, object> func;
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            if (!this.propertyGetterDic.TryGetValue(propertyName, out func))
            {
                throw new ArgumentOutOfRangeException("propertyName", string.Format("对象 {0} 没有命名为 {1} 的属性", instance.GetType().FullName, propertyName));
            }
            return func(instance);
        }

        public Action<object, object> GetPropertyClone(string propertyName)
        {
            return this.clonePropertyDic[propertyName];
        }

        public Func<object, object> GetPropertyGetter(string propertyName)
        {
            return this.propertyGetterDic[propertyName];
        }

        public Action<object, object> GetPropertySetter(string propertyName)
        {
            return this.propertySetterDic[propertyName];
        }

        public object[] GetReadWritePropertyValues(object entity)
        {
            return (from o in this.readWritePropertyGetters select o(entity)).ToArray<object>();
        }

        public void SetField(string fieldName, object instance, object value)
        {
            Action<object, object> action;
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            if (!this.fieldSetterDic.TryGetValue(fieldName, out action))
            {
                throw new ArgumentOutOfRangeException("fieldName", string.Format("对象 {0} 没有命名为 {1} 的字段", instance.GetType().FullName, fieldName));
            }
            action(instance, value);
        }

        public void SetProperty(string propertyName, object instance, object value)
        {
            Action<object, object> action;
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            if (!this.propertySetterDic.TryGetValue(propertyName, out action))
            {
                throw new ArgumentOutOfRangeException("propertyName", string.Format("对象 {0} 没有命名为 {1} 的属性", instance.GetType().FullName, propertyName));
            }
            action(instance, value);
        }

        public void SetReadWritePropertyValues(object entity, object[] values)
        {
            if (values.Length != this.readWriteProperties.Count)
            {
                throw new ArgumentException(string.Format("给定的值个数 {0} 与属性的个数 {1} 不一致，对象类型为 {2}。", values.Length, this.readWriteProperties.Count, this.type.FullName));
            }
            for (int i = 0; i < values.Length; i++)
            {
                this.readWritePropertySetters[i](entity, values[i]);
            }
        }

        public IList<PropertyInfo> ReadWriteProperties
        {
            get
            {
                return this.readWriteProperties;
            }
        }

        public System.Type Type
        {
            get
            {
                return this.type;
            }
        }
    }
}

