namespace WTF.Framework
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    public class FastActivator
    {
        private static Dictionary<Type, Func<object>> factoryCache = new Dictionary<Type, Func<object>>();

        public static T Create<T>()
        {
            return (T) Create(typeof(T));
        }

        public static object Create(string type)
        {
            return Create(Type.GetType(type));
        }

        public static object Create(Type type)
        {
            Func<object> func;
            if (type == null)
            {
                throw new ArgumentException("type");
            }
            if (!factoryCache.TryGetValue(type, out func))
            {
                lock (factoryCache)
                {
                    if (!factoryCache.TryGetValue(type, out func))
                    {
                        func = Expression.Lambda<Func<object>>(Expression.Convert(Expression.New(type), typeof(object)), new ParameterExpression[0]).Compile();
                        factoryCache[type] = func;
                    }
                }
            }
            return func();
        }
    }
}

