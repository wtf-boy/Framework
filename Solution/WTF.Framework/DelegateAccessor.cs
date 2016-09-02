namespace WTF.Framework
{
    using System;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Reflection.Emit;
    using System.Runtime.CompilerServices;

    public class DelegateAccessor
    {
        private static Type[] ParamTypes = new Type[] { typeof(object).MakeByRefType(), typeof(object) };

        public static Action<object, object> CreateFieldCloner(FieldInfo field)
        {
            ParameterExpression expression;
            ParameterExpression expression2;
            Action<object, object> action2 = null;
            if (field.ReflectedType.IsValueType)
            {
                if (action2 == null)
                {
                    action2 = delegate (object source, object target) {
                        object obj2 = CreateFieldGetter(field)(source);
                        CreateFieldSetter(field)(target, obj2);
                    };
                }
                return action2;
            }
            MemberExpression left = Expression.Field(Expression.Convert(expression2 = Expression.Parameter(typeof(object), "target"), field.ReflectedType), field);
            MemberExpression right = Expression.Field(Expression.Convert(expression = Expression.Parameter(typeof(object), "source"), field.ReflectedType), field);
            BinaryExpression body = Expression.Assign(left, right);
            return (Action<object, object>) Expression.Lambda(typeof(Action<object, object>), body, new ParameterExpression[] { expression, expression2 }).Compile();
        }

        public static Func<object, object> CreateFieldGetter(FieldInfo field)
        {
            ParameterExpression expression;
            UnaryExpression body = Expression.Convert(Expression.Field(Expression.Convert(expression = Expression.Parameter(typeof(object), "instance"), field.ReflectedType), field), typeof(object));
            return (Func<object, object>) Expression.Lambda(typeof(Func<object, object>), body, new ParameterExpression[] { expression }).Compile();
        }

        public static Action<object, object> CreateFieldSetter(FieldInfo field)
        {
            ParameterExpression expression;
            ParameterExpression expression2;
            if (field.IsInitOnly || field.ReflectedType.IsValueType)
            {
                RefSetterDelegate refHandler = CreateSetMethod(field);
                return delegate (object instance, object value) {
                    refHandler(ref instance, value);
                };
            }
            BinaryExpression body = Expression.Assign(Expression.Field(Expression.Convert(expression = Expression.Parameter(typeof(object), "instance"), field.ReflectedType), field), Expression.Convert(expression2 = Expression.Parameter(typeof(object), "value"), field.FieldType));
            return (Action<object, object>) Expression.Lambda(typeof(Action<object, object>), body, new ParameterExpression[] { expression, expression2 }).Compile();
        }

        public static Action<object, object> CreatePropertyCloner(PropertyInfo property)
        {
            ParameterExpression expression;
            ParameterExpression expression2;
            Action<object, object> action2 = null;
            if (property.ReflectedType.IsValueType)
            {
                if (action2 == null)
                {
                    action2 = delegate (object source, object target) {
                        object obj2 = CreatePropertyGetter(property)(source);
                        CreatePropertySetter(property)(target, obj2);
                    };
                }
                return action2;
            }
            MemberExpression expression3 = Expression.Property(Expression.Convert(expression = Expression.Parameter(typeof(object), "source"), property.ReflectedType), property);
            MethodCallExpression body = Expression.Call(Expression.Convert(expression2 = Expression.Parameter(typeof(object), "target"), property.ReflectedType), property.GetSetMethod(true), new Expression[] { expression3 });
            return (Action<object, object>) Expression.Lambda(typeof(Action<object, object>), body, new ParameterExpression[] { expression, expression2 }).Compile();
        }

        public static Func<object, object> CreatePropertyGetter(PropertyInfo property)
        {
            ParameterExpression expression;
            UnaryExpression body = Expression.Convert(Expression.Property(Expression.Convert(expression = Expression.Parameter(typeof(object), "instance"), property.ReflectedType), property), typeof(object));
            return (Func<object, object>) Expression.Lambda(typeof(Func<object, object>), body, new ParameterExpression[] { expression }).Compile();
        }

        public static Action<object, object> CreatePropertySetter(PropertyInfo property)
        {
            ParameterExpression expression;
            ParameterExpression expression2;
            if (property.ReflectedType.IsValueType)
            {
                RefSetterDelegate refHandler = CreateSetMethod(property);
                return delegate (object instance, object value) {
                    refHandler(ref instance, value);
                };
            }
            MethodCallExpression body = Expression.Call(Expression.Convert(expression = Expression.Parameter(typeof(object), "instance"), property.ReflectedType), property.GetSetMethod(true), new Expression[] { Expression.Convert(expression2 = Expression.Parameter(typeof(object), "value"), property.PropertyType) });
            return (Action<object, object>) Expression.Lambda(typeof(Action<object, object>), body, new ParameterExpression[] { expression, expression2 }).Compile();
        }

        public static RefSetterDelegate CreateSetMethod(MemberInfo memberInfo)
        {
            Type propertyType;
            if (memberInfo is PropertyInfo)
            {
                propertyType = ((PropertyInfo) memberInfo).PropertyType;
            }
            else
            {
                if (!(memberInfo is FieldInfo))
                {
                    throw new Exception("Can only create set methods for properties and fields.");
                }
                propertyType = ((FieldInfo) memberInfo).FieldType;
            }
            DynamicMethod method = new DynamicMethod("", typeof(void), ParamTypes, memberInfo.ReflectedType.Module, true);
            ILGenerator iLGenerator = method.GetILGenerator();
            iLGenerator.Emit(OpCodes.Ldarg_0);
            iLGenerator.Emit(OpCodes.Ldind_Ref);
            if (memberInfo.ReflectedType.IsValueType)
            {
                iLGenerator.DeclareLocal(memberInfo.ReflectedType.MakeByRefType());
                iLGenerator.Emit(OpCodes.Unbox, memberInfo.ReflectedType);
                iLGenerator.Emit(OpCodes.Stloc_0);
                iLGenerator.Emit(OpCodes.Ldloc_0);
            }
            iLGenerator.Emit(OpCodes.Ldarg_1);
            if (propertyType.IsValueType)
            {
                iLGenerator.Emit(OpCodes.Unbox_Any, propertyType);
            }
            if (memberInfo is PropertyInfo)
            {
                iLGenerator.Emit(OpCodes.Callvirt, ((PropertyInfo) memberInfo).GetSetMethod());
            }
            else if (memberInfo is FieldInfo)
            {
                iLGenerator.Emit(OpCodes.Stfld, (FieldInfo) memberInfo);
            }
            if (memberInfo.ReflectedType.IsValueType)
            {
                iLGenerator.Emit(OpCodes.Ldarg_0);
                iLGenerator.Emit(OpCodes.Ldloc_0);
                iLGenerator.Emit(OpCodes.Ldobj, memberInfo.ReflectedType);
                iLGenerator.Emit(OpCodes.Box, memberInfo.ReflectedType);
                iLGenerator.Emit(OpCodes.Stind_Ref);
            }
            iLGenerator.Emit(OpCodes.Ret);
            return (RefSetterDelegate) method.CreateDelegate(typeof(RefSetterDelegate));
        }

        public delegate void RefSetterDelegate(ref object target, object value);
    }
}

