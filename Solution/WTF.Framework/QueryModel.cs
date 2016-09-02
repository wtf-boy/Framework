namespace WTF.Framework
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Text.RegularExpressions;

    public class QueryModel
    {
        private StringBuilder _Condition = new StringBuilder();
        private List<ConditionItem> _Items = new List<ConditionItem>();
        private static readonly Dictionary<QueryMethod, Func<Expression, Expression, Expression>> ExpressionDict;
        private static List<ITransformProvider> TransformProviders { get; set; }
        static QueryModel()
        {
            Dictionary<QueryMethod, Func<Expression, Expression, Expression>> dictionary = new Dictionary<QueryMethod, Func<Expression, Expression, Expression>>();
            dictionary.Add(QueryMethod.Equal, (left, right) => Expression.Equal(left, right));
            dictionary.Add(QueryMethod.GreaterThan, (left, right) => Expression.GreaterThan(left, right));
            dictionary.Add(QueryMethod.GreaterThanOrEqual, (left, right) => Expression.GreaterThanOrEqual(left, right));
            dictionary.Add(QueryMethod.LessThan, (left, right) => Expression.LessThan(left, right));
            dictionary.Add(QueryMethod.LessThanOrEqual, (left, right) => Expression.LessThanOrEqual(left, right));
            dictionary.Add(QueryMethod.Contains, delegate (Expression left, Expression right) {
                if (left.Type != typeof(string))
                {
                    return null;
                }
                return Expression.Call(left, typeof(string).GetMethod("Contains"), new Expression[] { right });
            });
            dictionary.Add(QueryMethod.StdIn, delegate (Expression left, Expression right) {
                if (!right.Type.IsArray)
                {
                    return null;
                }
                return Expression.Call(typeof(Enumerable), "Contains", new Type[] { left.Type }, new Expression[] { right, left });
            });
            dictionary.Add(QueryMethod.NotEqual, (left, right) => Expression.NotEqual(left, right));
            dictionary.Add(QueryMethod.StartsWith, delegate (Expression left, Expression right) {
                if (left.Type != typeof(string))
                {
                    return null;
                }
                return Expression.Call(left, typeof(string).GetMethod("StartsWith", new Type[] { typeof(string) }), new Expression[] { right });
            });
            dictionary.Add(QueryMethod.EndsWith, delegate (Expression left, Expression right) {
                if (left.Type != typeof(string))
                {
                    return null;
                }
                return Expression.Call(left, typeof(string).GetMethod("EndsWith", new Type[] { typeof(string) }), new Expression[] { right });
            });
            ExpressionDict = dictionary;
            TransformProviders = new List<ITransformProvider> { new InTransformProvider() };
        }

        public void AddConditionItem(string field, QueryMethod method, QueryUnite queryUnite, QueryDataType queryDataType, object val)
        {
            ConditionItem item = new ConditionItem {
                Field = field,
                Method = method,
                Value = val,
                QueryUnite = queryUnite,
                QueryDataType = queryDataType
            };
            this.Items.Add(item);
        }

        public static object ChangeType(object value, Type conversionType)
        {
            if (value == null)
            {
                return null;
            }
            if (conversionType == typeof(Guid))
            {
                return new Guid(value.ToString());
            }
            return Convert.ChangeType(value, TypeUtil.GetUnNullableType(conversionType));
        }

        public static Expression ChangeTypeToExpression(ConditionItem item, Type conversionType)
        {
            if (item.Value == null)
            {
                return Expression.Constant(item.Value, conversionType);
            }
            if (item.Method == QueryMethod.StdIn)
            {
                Array array = item.Value as Array;
                List<Expression> initializers = new List<Expression>();
                if (array != null)
                {
                    for (int i = 0; i < array.Length; i++)
                    {
                        object obj2 = ChangeType(array.GetValue(i), conversionType);
                        initializers.Add(Expression.Constant(obj2, conversionType));
                    }
                }
                return Expression.NewArrayInit(conversionType, initializers);
            }
            Type unNullableType = TypeUtil.GetUnNullableType(conversionType);
            if (unNullableType == typeof(Guid))
            {
                Guid guid = new Guid(item.Value.ToString());
                return Expression.Constant(guid, conversionType);
            }
            return Expression.Constant(Convert.ChangeType(item.Value, unNullableType), conversionType);
        }

        private Expression GetExpression<T>(ParameterExpression param, ConditionItem item)
        {
            LambdaExpression propertyLambdaExpression = this.GetPropertyLambdaExpression<T>(item, param);
            foreach (ITransformProvider provider in TransformProviders)
            {
                if (provider.Match(item, propertyLambdaExpression.Body.Type))
                {
                    return this.GetGroupExpression<T>(param, provider.Transform(item, propertyLambdaExpression.Body.Type), new Func<Expression, Expression, Expression>(Expression.AndAlso));
                }
            }
            Expression expression2 = ChangeTypeToExpression(item, propertyLambdaExpression.Body.Type);
            return ExpressionDict[item.Method](propertyLambdaExpression.Body, expression2);
        }

        private Expression GetExpressoinBody<T>(ParameterExpression param, IEnumerable<ConditionItem> items)
        {
            List<Expression> source = new List<Expression>();
            IEnumerable<ConditionItem> enumerable = from c in items
                where string.IsNullOrEmpty(c.OrGroup)
                select c;
            if (enumerable.Count<ConditionItem>() != 0)
            {
                source.Add(this.GetGroupExpression<T>(param, enumerable, new Func<Expression, Expression, Expression>(Expression.AndAlso)));
            }
            IEnumerable<IGrouping<string, ConditionItem>> enumerable2 = from c in items
                where !string.IsNullOrEmpty(c.OrGroup)
                group c by c.OrGroup;
            foreach (IGrouping<string, ConditionItem> grouping in enumerable2)
            {
                if (grouping.Count<ConditionItem>() != 0)
                {
                    source.Add(this.GetGroupExpression<T>(param, grouping, new Func<Expression, Expression, Expression>(Expression.OrElse)));
                }
            }
            return source.Aggregate<Expression>(new Func<Expression, Expression, Expression>(Expression.AndAlso));
        }

        private Expression GetGroupExpression<T>(ParameterExpression param, IEnumerable<ConditionItem> items, Func<Expression, Expression, Expression> func)
        {
            return (from item in items select this.GetExpression<T>(param, item)).Aggregate<Expression>(func);
        }

        private LambdaExpression GetPropertyLambdaExpression<T>(ConditionItem item, ParameterExpression param)
        {
            string[] strArray = item.Field.Split(new char[] { '.' });
            Expression expression = param;
            Type propertyType = typeof(T);
            int index = 0;
            do
            {
                PropertyInfo property = propertyType.GetProperty(strArray[index]);
                if (property == null)
                {
                    return null;
                }
                propertyType = property.PropertyType;
                expression = Expression.MakeMemberAccess(expression, property);
                index++;
            }
            while (index < strArray.Length);
            return Expression.Lambda(expression, new ParameterExpression[] { param });
        }

        public Expression<Func<T, bool>> ToConditioExpressionLinq<T>()
        {
            ParameterExpression expression;
            if (this.Items.Count == 0)
            {
                return null;
            }
            return Expression.Lambda<Func<T, bool>>(this.GetExpressoinBody<T>(expression = Expression.Parameter(typeof(T), "c"), this.Items), new ParameterExpression[] { expression });
        }

        public string ToConditionESearch()
        {
            if (this.Items.Count == 0)
            {
                return "";
            }
            foreach (ConditionItem item in this.Items)
            {
                if (item.QueryDataType == QueryDataType.ObjectT)
                {
                    item.QueryDataType = item.DefaultQueryDataType;
                }
                this._Condition.Append(item.ToConditionESearch());
            }
            return this._Condition.ToString().Replace("^OR", "", RegexOptions.IgnoreCase).Replace("^AND", "", RegexOptions.IgnoreCase);
        }

        public string ToConditionLinq()
        {
            if (this.Items.Count == 0)
            {
                return "";
            }
            foreach (ConditionItem item in this.Items)
            {
                this._Condition.Append(item.ToConditionLinq());
            }
            return this._Condition.ToString().Replace("^OR", "", RegexOptions.IgnoreCase).Replace("^AND", "", RegexOptions.IgnoreCase);
        }

        public string ToConditionLinq<T>()
        {
            if (this.Items.Count == 0)
            {
                return "";
            }
            foreach (ConditionItem item in this.Items)
            {
                this._Condition.Append(item.ToConditionLinq<T>());
            }
            return this._Condition.ToString().Replace("^OR", "", RegexOptions.IgnoreCase).Replace("^AND", "", RegexOptions.IgnoreCase);
        }

        public string ToConditionSolrSql()
        {
            if (this.Items.Count == 0)
            {
                return "";
            }
            foreach (ConditionItem item in this.Items)
            {
                if (item.QueryDataType == QueryDataType.ObjectT)
                {
                    item.QueryDataType = item.DefaultQueryDataType;
                }
                this._Condition.Append(item.ToConditionSolrSql());
            }
            return this._Condition.ToString().Replace("^OR", "", RegexOptions.IgnoreCase).Replace("^AND", "", RegexOptions.IgnoreCase);
        }

        public string ToConditionSql()
        {
            if (this.Items.Count == 0)
            {
                return "";
            }
            foreach (ConditionItem item in this.Items)
            {
                if (item.QueryDataType == QueryDataType.ObjectT)
                {
                    item.QueryDataType = item.DefaultQueryDataType;
                }
                this._Condition.Append(item.ToConditionSql());
            }
            return this._Condition.ToString().Replace("^OR", "", RegexOptions.IgnoreCase).Replace("^AND", "", RegexOptions.IgnoreCase);
        }

        public string ToConditionSql<T>()
        {
            if (this.Items.Count == 0)
            {
                return "";
            }
            foreach (ConditionItem item in this.Items)
            {
                this._Condition.Append(item.ToConditionSql<T>());
            }
            return this._Condition.ToString().Replace("^OR", "", RegexOptions.IgnoreCase).Replace("^AND", "", RegexOptions.IgnoreCase);
        }

        public List<ConditionItem> Items
        {
            get
            {
                return this._Items;
            }
        }

        //private static List<ITransformProvider> TransformProviders
        //{
        //    [CompilerGenerated]
        //    get
        //    {
        //        return <TransformProviders>k__BackingField;
        //    }
        //    [CompilerGenerated]
        //    set
        //    {
        //        <TransformProviders>k__BackingField = value;
        //    }
        //}
    }
}

