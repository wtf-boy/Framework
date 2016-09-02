namespace WTF.Framework
{
    using System;
    using System.Collections;
    using System.Data.Common;
    using System.Data.EntityClient;
    using System.Data.Objects;
    using System.Data.Objects.DataClasses;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Text.RegularExpressions;

    public static class EntitiesHelper
    {
        //public static Expression<T> Compose<T>(this Expression<T> first, Expression<T> second, Func<Expression, Expression, Expression> merge)
        //{
        //    Expression expression = ParameterRebinder.ReplaceParameters(first.Parameters.Select(((Func<ParameterExpression, int, <>f__AnonymousType0<ParameterExpression, ParameterExpression>>) ((f, i) => new { f = f, s = second.Parameters[i] }))).ToDictionary(p => p.s, p => p.f), second.Body);
        //    return Expression.Lambda<T>(merge(first.Body, expression), first.Parameters);
        //}

        private static void DeleteData(this ObjectQuery objObjectQuery)
        {
            ObjectContext context = objObjectQuery.Context;
            foreach (object obj2 in (IEnumerable) objObjectQuery)
            {
                context.DeleteObject(obj2);
            }
            context.SaveChanges();
        }

        private static void DeleteData<T>(this ObjectQuery<T> objObjectQuery) where T: EntityObject
        {
            ObjectContext context = objObjectQuery.Context;
            foreach (T local in objObjectQuery)
            {
                context.DeleteObject(local);
            }
            context.SaveChanges();
        }

        public static void DeleteData<TEntity>(this ObjectSet<TEntity> objObjectSet, Expression<Func<TEntity, bool>> predicate) where TEntity: class
        {
            foreach (TEntity local in objObjectSet.Where<TEntity>(predicate))
            {
                objObjectSet.Context.DeleteObject(local);
            }
            objObjectSet.Context.SaveChanges();
        }

        public static void DeleteData<T>(this ObjectContext objContext, string queryString, params ObjectParameter[] parameters) where T: EntityObject
        {
            objContext.CreateQuery<T>("[" + typeof(T).Name + "]", new ObjectParameter[0]).Where(queryString, parameters).DeleteData<T>();
        }

        public static void DeleteData<T>(this ObjectQuery<T> objObjectQuery, string queryString, params ObjectParameter[] parameters) where T: EntityObject
        {
            objObjectQuery.Where(queryString, parameters).DeleteData<T>();
        }

        public static void DeleteDataPrimaryKey<T>(this ObjectContext objContext, string IdString) where T: EntityObject
        {
            PropertyInfo entityPrimaryKey = GetEntityPrimaryKey<T>();
            string queryString = "it." + entityPrimaryKey.Name + " in  {" + IdString + "}";
            if (entityPrimaryKey.PropertyType == typeof(Guid))
            {
                queryString = "it." + entityPrimaryKey.Name + " in {" + IdString.ConvertGuidID() + "}";
            }
            else if (entityPrimaryKey.PropertyType == typeof(string))
            {
                queryString = "it." + entityPrimaryKey.Name + " in {" + IdString.ConvertStringID() + "}";
            }
            objContext.DeleteData<T>(queryString, new ObjectParameter[0]);
        }

        public static void DeleteDataPrimaryKey<T>(this ObjectQuery<T> objObjectQuery, string primaryKey) where T: EntityObject
        {
            if (!primaryKey.IsNull())
            {
                PropertyInfo entityPrimaryKey = GetEntityPrimaryKey<T>();
                string predicate = "it." + entityPrimaryKey.Name + " in  {" + primaryKey + "}";
                if (entityPrimaryKey.PropertyType == typeof(Guid))
                {
                    predicate = "it." + entityPrimaryKey.Name + " in {" + primaryKey.ConvertGuidID() + "}";
                }
                else if (entityPrimaryKey.PropertyType == typeof(string))
                {
                    predicate = "it." + entityPrimaryKey.Name + " in {" + primaryKey.ConvertStringID() + "}";
                }
                objObjectQuery.Where(predicate, new ObjectParameter[0]).DeleteData<T>();
            }
        }

        public static void DeleteDataPrimaryKeySql<T>(this ObjectQuery<T> objObjectQuery, string primaryKey) where T: EntityObject
        {
            if (!primaryKey.IsNull())
            {
                PropertyInfo entityPrimaryKey = GetEntityPrimaryKey<T>();
                string conditon = entityPrimaryKey.Name + " in  (" + primaryKey + ")";
                if (entityPrimaryKey.PropertyType == typeof(Guid))
                {
                    conditon = entityPrimaryKey.Name + " in (" + primaryKey.ConvertStringID() + ")";
                }
                else if (entityPrimaryKey.PropertyType == typeof(string))
                {
                    conditon = entityPrimaryKey.Name + " in (" + primaryKey.ConvertStringID() + ")";
                }
                objObjectQuery.DeleteDataSql<T>(conditon, new object[0]);
            }
        }

        public static void DeleteDataSql<T>(this ObjectQuery<T> objObjectQuery, string conditon, params object[] parameters) where T: EntityObject
        {
            objObjectQuery.Context.DeleteDataSql(GetEntityTableName<T>(), conditon, parameters);
        }

        public static void DeleteDataSql(this ObjectContext objObjectContext, string tableName, string conditon, params object[] parameters)
        {
            if (string.IsNullOrEmpty(tableName))
            {
                throw new ArgumentNullException("tableName 不能为空");
            }
            string commandText = "delete from " + tableName;
            if (!string.IsNullOrEmpty(conditon))
            {
                commandText = commandText + "  where " + conditon;
            }
            objObjectContext.ExecuteStoreCommand(commandText, parameters);
        }

        public static string GetConnectionString<T>() where T: class
        {
            return GetConnectionString<T>("SevenConnectionString");
        }

        public static string GetConnectionString<T>(string connectionName) where T: class
        {
            connectionName = ConfigHelper.GetValue(connectionName, connectionName);
            string format = "metadata=res://*/Entity.{0}.csdl|res://*/Entity.{0}.ssdl|res://*/Entity.{0}.msl;provider=MySql.Data.MySqlClient;provider connection string=\"{1}\"";
            string str2 = typeof(T).Name.Replace("Entities", "Model");
            return string.Format(format, str2, EncryptConnectionHelper.ConnectionString(connectionName));
        }

        public static EntityConnection GetEntityConnection<T>() where T: ObjectContext
        {
            return new EntityConnection(GetConnectionString<T>("SevenConnectionString"));
        }

        public static EntityConnection GetEntityConnection<T>(string connectionName) where T: ObjectContext
        {
            return new EntityConnection(GetConnectionString<T>(connectionName));
        }

        public static EntityConnection GetEntityConnection<T, C>(C connectionName) where T: ObjectContext
        {
            return GetEntityConnection<T>(connectionName.ToString());
        }

        private static PropertyInfo GetEntityPrimaryKey<T>() where T: EntityObject
        {
            PropertyInfo[] properties = typeof(T).GetProperties();
            foreach (PropertyInfo info in properties)
            {
                foreach (Attribute attribute in info.GetCustomAttributes(typeof(EdmScalarPropertyAttribute), false))
                {
                    EdmScalarPropertyAttribute attribute2 = attribute as EdmScalarPropertyAttribute;
                    if ((attribute2 != null) && attribute2.EntityKeyProperty)
                    {
                        return info;
                    }
                }
            }
            return null;
        }

        public static string GetEntityTableName<T>() where T: EntityObject
        {
            EdmEntityTypeAttribute attribute = (EdmEntityTypeAttribute) Attribute.GetCustomAttribute(typeof(T), typeof(EdmEntityTypeAttribute), false);
            return ((attribute == null) ? "" : attribute.Name);
        }

        public static IQueryable<T> GetPage<T>(this IQueryable<T> objIQueryable, string sortExpression, int pageSize, int currentPageIndex, out int recordCount)
        {
            recordCount = 0;
            recordCount = objIQueryable.Count<T>();
            return objIQueryable.OrderByExpand<T>(sortExpression).Skip<T>((pageSize * currentPageIndex)).Take<T>(pageSize);
        }

        public static ObjectQuery<T> GetPage<T>(this ObjectQuery<T> objObjectQuery, string condition, string sortExpression, int pageSize, int currentPageIndex, out int recordCount)
        {
            recordCount = 0;
            if (condition.IsNoNull())
            {
                objObjectQuery = objObjectQuery.Where(condition, new ObjectParameter[0]);
            }
            recordCount = objObjectQuery.Count<T>();
            int num = pageSize * currentPageIndex;
            return objObjectQuery.Skip(sortExpression, num.ToString(), new ObjectParameter[0]).Top(pageSize.ToString(), new ObjectParameter[0]);
        }

        public static ObjectQuery<DbDataRecord> GetPage<T>(this ObjectQuery<T> objObjectQuery, string fields, string condition, string sortExpression, int pageSize, int currentPageIndex, out int recordCount)
        {
            recordCount = 0;
            if (condition.IsNoNull())
            {
                objObjectQuery = objObjectQuery.Where(condition, new ObjectParameter[0]);
            }
            recordCount = objObjectQuery.Count<T>();
            if (fields.IsNull())
            {
                throw new ArgumentNullException("请输入要选择的字段fields");
            }
            int num = pageSize * currentPageIndex;
            return objObjectQuery.Select(fields, new ObjectParameter[0]).Skip(sortExpression, num.ToString(), new ObjectParameter[0]).Top(pageSize.ToString(), new ObjectParameter[0]);
        }

        public static IQueryable<T> OrderByExpand<T>(this IQueryable<T> query, string keys)
        {
            keys = keys.Trim();
            if (!string.IsNullOrEmpty(keys))
            {
                keys = keys.Replace("it.", "");
                bool flag = true;
                foreach (string str in keys.Split(new char[] { ',' }))
                {
                    ParameterExpression expression;
                    string[] strArray = str.Trim().Replace("[ ]+", "|", RegexOptions.IgnoreCase).Split(new char[] { '|' });
                    if (string.IsNullOrEmpty(strArray[0]))
                    {
                        throw new Exception("请指定排序字段");
                    }
                    string name = strArray[0];
                    string str4 = "ASC";
                    if (strArray.Length == 2)
                    {
                        str4 = strArray[1];
                    }
                    Expression expression2 = expression = Expression.Parameter(typeof(T), "it");
                    if (Nullable.GetUnderlyingType(expression2.Type) != null)
                    {
                        expression2 = Expression.Property(expression2, "Value");
                    }
                    PropertyInfo property = typeof(T).GetProperty(name, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                    if (property == null)
                    {
                        throw new Exception("对像上不存在" + name + "的字段");
                    }
                    str4 = str4.ToLower();
                    expression2 = Expression.MakeMemberAccess(expression2, property);
                    LambdaExpression expression3 = Expression.Lambda(expression2, new ParameterExpression[] { expression });
                    string methodName = "";
                    if (flag)
                    {
                        flag = false;
                        methodName = (str4 == "desc") ? "OrderByDescending" : "OrderBy";
                    }
                    else
                    {
                        flag = false;
                        methodName = (str4 == "desc") ? "ThenByDescending" : "ThenBy";
                    }
                    query = query.Provider.CreateQuery<T>(Expression.Call(typeof(Queryable), methodName, new Type[] { typeof(T), expression2.Type }, new Expression[] { query.Expression, Expression.Quote(expression3) }));
                }
            }
            return query;
        }

        public static ObjectQuery<DbDataRecord> SelectFiledTop<T>(this ObjectQuery<T> objObjectQuery, string sortExpression, string selecFileds, int topCount) where T: EntityObject
        {
            return objObjectQuery.SelectFiledTop<T>("", sortExpression, selecFileds, topCount);
        }

        public static ObjectQuery<DbDataRecord> SelectFiledTop<T>(this ObjectQuery<T> objObjectQuery, string condition, string sortExpression, string selecFileds, int topCount) where T: EntityObject
        {
            if (condition.IsNoNull())
            {
                objObjectQuery = objObjectQuery.Where(condition, new ObjectParameter[0]);
            }
            if (selecFileds.IsNull())
            {
                throw new ArgumentNullException("请输入要选择的字段" + selecFileds);
            }
            return objObjectQuery.Skip(sortExpression, "0", new ObjectParameter[0]).Select(selecFileds, new ObjectParameter[0]).Top(topCount.ToString(), new ObjectParameter[0]);
        }

        public static ObjectQuery<T> SelectTop<T>(this ObjectQuery<T> objObjectQuery, string sortExpression, int topCount) where T: EntityObject
        {
            return objObjectQuery.SelectTop<T>("", sortExpression, topCount);
        }

        public static ObjectQuery<T> SelectTop<T>(this ObjectQuery<T> objObjectQuery, string condition, string sortExpression, int topCount) where T: EntityObject
        {
            if (condition.IsNoNull())
            {
                objObjectQuery = objObjectQuery.Where(condition, new ObjectParameter[0]);
            }
            return objObjectQuery.Skip(sortExpression, "0", new ObjectParameter[0]).Top(topCount.ToString(), new ObjectParameter[0]);
        }

        public static ObjectQuery<T> WhereCondition<T>(this ObjectQuery<T> objObjectQuery, string condition)
        {
            if (condition.IsNoNull())
            {
                objObjectQuery = objObjectQuery.Where(condition, new ObjectParameter[0]);
            }
            return objObjectQuery;
        }

        public static ObjectQuery<T> WhereKey<T>(this ObjectQuery<T> objObjectQuery, string IdString) where T: EntityObject
        {
            if (IdString.IsNull())
            {
                return objObjectQuery.Where("1=0", new ObjectParameter[0]);
            }
            PropertyInfo entityPrimaryKey = GetEntityPrimaryKey<T>();
            string predicate = "it." + entityPrimaryKey.Name + " in  {" + IdString + "}";
            if (entityPrimaryKey.PropertyType == typeof(Guid))
            {
                predicate = "it." + entityPrimaryKey.Name + " in {" + IdString.ConvertGuidID() + "}";
            }
            else if (entityPrimaryKey.PropertyType == typeof(string))
            {
                predicate = "it." + entityPrimaryKey.Name + " in {" + IdString.ConvertStringID() + "}";
            }
            return objObjectQuery.Where(predicate, new ObjectParameter[0]);
        }
    }
}

