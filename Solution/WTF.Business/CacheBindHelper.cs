namespace WTF.Business
{
    using MySql.Data.MySqlClient;
    using WTF.Cache.Helper;
    using WTF.DAL;
    using WTF.Framework;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public static class CacheBindHelper
    {
        private static string DataSql(object value)
        {
            string str = "";
            if (value == null)
            {
                str = "null";
            }
            else if (value is bool)
            {
                str = ((bool) value) ? "1" : "0";
            }
            else
            {
                str = value.ToString().Replace("'", "''");
            }
            if ((value.GetType() == typeof(string)) || (value.GetType() == typeof(DateTime)))
            {
                str = "'" + str + "'";
            }
            return str;
        }

        public static bool DeleteCache<D, T, Key>(this BizBase<D, T, Key> objBizBase, string guidOridKey) where D: DalBase<T, Key> where T: class, new()
        {
            return string.Format(objBizBase.Dal.TableName.ToLower() + "_" + guidOridKey.ToLower(), new object[0]).DeleteCacheMemcached();
        }

        public static bool DeleteCache<D, T, Key>(this BizBase<D, T, Key> objBizBase, string tableName, string key) where D: DalBase<T, Key> where T: class, new()
        {
            return string.Format(tableName.ToLower() + "_" + key.ToLower(), new object[0]).DeleteCacheMemcached();
        }

        public static bool DeleteCacheID<D, T, Key>(this BizBase<D, T, Key> objBizBase, Key ID) where D: DalBase<T, Key> where T: class, new()
        {
            return string.Format(objBizBase.Dal.TableName.ToLower() + "_" + ID.ToString(), new object[0]).DeleteCacheMemcached();
        }

        public static void DeleteCacheIDString<D, T, Key>(this BizBase<D, T, Key> objBizBase, string IDString) where D: DalBase<T, Key> where T: class, new()
        {
            foreach (string str in IDString.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                objBizBase.DeleteCache<D, T, Key>(str);
            }
        }

        private static bool DeleteCacheKey(string cacheKey)
        {
            return cacheKey.DeleteCacheMemcached();
        }

        public static bool DeleteCacheKey<D, T, Key>(this BizBase<D, T, Key> objBizBase, string cacheKey) where D: DalBase<T, Key> where T: class, new()
        {
            return cacheKey.DeleteCacheMemcached();
        }

        public static bool DeleteDependencyKey<D, T, Key>(this BizBase<D, T, Key> objBizBase, string dependencyKey = "") where D: DalBase<T, Key> where T: class, new()
        {
            return objBizBase.GetDependencyKey<D, T, Key>(dependencyKey).DeleteCacheMemcached();
        }

        private static string GetCondtiionParam(string condition, MySqlParameter[] parms)
        {
            if (parms != null)
            {
                foreach (MySqlParameter parameter in parms)
                {
                    condition = condition.Replace(parameter.ParameterName, DataSql(parameter.Value));
                }
            }
            return condition;
        }

        public static DataSet GetDataSetCache<D, T, Key>(this BizBase<D, T, Key> objBizBase, string dependencyKey, bool isCache, string condition, string sortExpression, string fields = "*") where D: DalBase<T, Key> where T: class, new()
        {
            TimeSpan expiry = new TimeSpan(0, ConfigHelper.GetIntValue("PageCacheMinutes", 60), 0);
            return objBizBase.GetDataSetCache<D, T, Key>(dependencyKey, isCache, expiry, condition, sortExpression, fields);
        }

        public static DataSet GetDataSetCache<D, T, Key>(this BizBase<D, T, Key> objBizBase, string dependencyKey, bool isCache, string condition, MySqlParameter[] parms, string sortExpression, string fields = "*") where D: DalBase<T, Key> where T: class, new()
        {
            TimeSpan expiry = new TimeSpan(0, ConfigHelper.GetIntValue("PageCacheMinutes", 60), 0);
            return objBizBase.GetDataSetCache<D, T, Key>(dependencyKey, isCache, expiry, condition, parms, sortExpression, fields);
        }

        public static DataSet GetDataSetCache<D, T, Key>(this BizBase<D, T, Key> objBizBase, string dependencyKey, bool isCache, TimeSpan expiry, string condition, string sortExpression, string fields = "*") where D: DalBase<T, Key> where T: class, new()
        {
            return objBizBase.GetDataSetCache<D, T, Key>(dependencyKey, isCache, expiry, condition, null, sortExpression, fields);
        }

        public static DataSet GetDataSetCache<D, T, Key>(this BizBase<D, T, Key> objBizBase, string dependencyKey, bool isCache, TimeSpan expiry, string condition, MySqlParameter[] parms, string sortExpression, string fields = "*") where D: DalBase<T, Key> where T: class, new()
        {
            dependencyKey = objBizBase.GetDependencyKey<D, T, Key>(dependencyKey);
            DataSet dependencyCache = null;
            string cacheKey = (objBizBase.Dal.TableName.ToLower() + "DataSet" + GetCondtiionParam(condition, parms) + sortExpression + fields).Replace(" ", "").ToLower();
            if (isCache)
            {
                dependencyCache = cacheKey.GetDependencyCache<DataSet>(dependencyKey);
                if (dependencyCache == null)
                {
                    dependencyCache = objBizBase.GetDataSet(condition, parms, sortExpression, fields);
                    dependencyCache.SetDependencyCache(dependencyKey, cacheKey, expiry);
                }
                return dependencyCache;
            }
            DeleteCacheKey(MemcacheCacheHelper.CreateDependencyCacheKey(dependencyKey, cacheKey));
            return objBizBase.GetDataSet(condition, parms, sortExpression, fields);
        }

        public static DataSet GetDataSetLimitCache<D, T, Key>(this BizBase<D, T, Key> objBizBase, string dependencyKey, bool isCache, string condition, string sortExpression, int offset, int limit, string fields = "*") where D: DalBase<T, Key> where T: class, new()
        {
            TimeSpan expiry = new TimeSpan(0, ConfigHelper.GetIntValue("PageCacheMinutes", 60), 0);
            return objBizBase.GetDataSetLimitCache<D, T, Key>(dependencyKey, isCache, expiry, condition, sortExpression, offset, limit, fields);
        }

        public static DataSet GetDataSetLimitCache<D, T, Key>(this BizBase<D, T, Key> objBizBase, string dependencyKey, bool isCache, string condition, MySqlParameter[] parms, string sortExpression, int offset, int limit, string fields = "*") where D: DalBase<T, Key> where T: class, new()
        {
            TimeSpan expiry = new TimeSpan(0, ConfigHelper.GetIntValue("PageCacheMinutes", 60), 0);
            return objBizBase.GetDataSetLimitCache<D, T, Key>(dependencyKey, isCache, expiry, condition, parms, sortExpression, offset, limit, fields);
        }

        public static DataSet GetDataSetLimitCache<D, T, Key>(this BizBase<D, T, Key> objBizBase, string dependencyKey, bool isCache, TimeSpan expiry, string condition, string sortExpression, int offset, int limit, string fields = "*") where D: DalBase<T, Key> where T: class, new()
        {
            return objBizBase.GetDataSetLimitCache<D, T, Key>(dependencyKey, isCache, expiry, condition, null, sortExpression, offset, limit, fields);
        }

        public static DataSet GetDataSetLimitCache<D, T, Key>(this BizBase<D, T, Key> objBizBase, string dependencyKey, bool isCache, TimeSpan expiry, string condition, MySqlParameter[] parms, string sortExpression, int offset, int limit, string fields = "*") where D: DalBase<T, Key> where T: class, new()
        {
            dependencyKey = objBizBase.GetDependencyKey<D, T, Key>(dependencyKey);
            DataSet dependencyCache = null;
            string cacheKey = string.Concat(new object[] { objBizBase.Dal.TableName.ToLower(), "DataSetLimit", GetCondtiionParam(condition, parms), sortExpression, offset, limit, fields }).Replace(" ", "").ToLower();
            if (isCache)
            {
                dependencyCache = cacheKey.GetDependencyCache<DataSet>(dependencyKey);
                if (dependencyCache == null)
                {
                    dependencyCache = objBizBase.GetDataSetLimit(condition, parms, sortExpression, offset, limit, fields);
                    dependencyCache.SetDependencyCache(dependencyKey, cacheKey, expiry);
                }
                return dependencyCache;
            }
            DeleteCacheKey(MemcacheCacheHelper.CreateDependencyCacheKey(dependencyKey, cacheKey));
            return objBizBase.GetDataSetLimit(condition, parms, sortExpression, offset, limit, fields);
        }

        public static DataSet GetDataSetTextCache<D, T, Key>(this BizBase<D, T, Key> objBizBase, string dependencyKey, bool isCache, string commandText, params MySqlParameter[] parms) where D: DalBase<T, Key> where T: class, new()
        {
            TimeSpan expiry = new TimeSpan(0, ConfigHelper.GetIntValue("PageCacheMinutes", 60), 0);
            return objBizBase.GetDataSetTextCache<D, T, Key>(dependencyKey, isCache, expiry, commandText, parms);
        }

        public static DataSet GetDataSetTextCache<D, T, Key>(this BizBase<D, T, Key> objBizBase, string dependencyKey, bool isCache, TimeSpan expiry, string commandText, params MySqlParameter[] parms) where D: DalBase<T, Key> where T: class, new()
        {
            dependencyKey = objBizBase.GetDependencyKey<D, T, Key>(dependencyKey);
            DataSet dependencyCache = null;
            string cacheKey = (objBizBase.Dal.TableName.ToLower() + "DataSet" + GetCondtiionParam(commandText, parms)).Replace(" ", "").ToLower();
            if (isCache)
            {
                dependencyCache = cacheKey.GetDependencyCache<DataSet>(dependencyKey);
                if (dependencyCache == null)
                {
                    dependencyCache = objBizBase.Dal.ExecuteDataSet(commandText, parms);
                    dependencyCache.SetDependencyCache(dependencyKey, cacheKey, expiry);
                }
                return dependencyCache;
            }
            DeleteCacheKey(MemcacheCacheHelper.CreateDependencyCacheKey(dependencyKey, cacheKey));
            return objBizBase.Dal.ExecuteDataSet(commandText, parms);
        }

        public static DataSet GetDataSetViewCache<D, T, Key>(this BizBase<D, T, Key> objBizBase, string dependencyKey, bool isCache, string condition, string sortExpression, string fields = "*") where D: DalBase<T, Key> where T: class, new()
        {
            TimeSpan expiry = new TimeSpan(0, ConfigHelper.GetIntValue("PageCacheMinutes", 60), 0);
            return objBizBase.GetDataSetViewCache<D, T, Key>(dependencyKey, isCache, expiry, condition, sortExpression, fields);
        }

        public static DataSet GetDataSetViewCache<D, T, Key>(this BizBase<D, T, Key> objBizBase, string dependencyKey, bool isCache, string condition, MySqlParameter[] parms, string sortExpression, string fields = "*") where D: DalBase<T, Key> where T: class, new()
        {
            TimeSpan expiry = new TimeSpan(0, ConfigHelper.GetIntValue("PageCacheMinutes", 60), 0);
            return objBizBase.GetDataSetViewCache<D, T, Key>(dependencyKey, isCache, expiry, condition, parms, sortExpression, fields);
        }

        public static DataSet GetDataSetViewCache<D, T, Key>(this BizBase<D, T, Key> objBizBase, string dependencyKey, bool isCache, TimeSpan expiry, string condition, string sortExpression, string fields = "*") where D: DalBase<T, Key> where T: class, new()
        {
            return objBizBase.GetDataSetViewCache<D, T, Key>(dependencyKey, isCache, expiry, condition, null, sortExpression, fields);
        }

        public static DataSet GetDataSetViewCache<D, T, Key>(this BizBase<D, T, Key> objBizBase, string dependencyKey, bool isCache, TimeSpan expiry, string condition, MySqlParameter[] parms, string sortExpression, string fields = "*") where D: DalBase<T, Key> where T: class, new()
        {
            dependencyKey = objBizBase.GetDependencyKey<D, T, Key>(dependencyKey);
            DataSet dependencyCache = null;
            string cacheKey = (objBizBase.Dal.TableViewName.ToLower() + "DataSet" + GetCondtiionParam(condition, parms) + sortExpression + fields).Replace(" ", "").ToLower();
            if (isCache)
            {
                dependencyCache = cacheKey.GetDependencyCache<DataSet>(dependencyKey);
                if (dependencyCache == null)
                {
                    dependencyCache = objBizBase.GetDataSetView(condition, parms, sortExpression, fields);
                    dependencyCache.SetDependencyCache(dependencyKey, cacheKey, expiry);
                }
                return dependencyCache;
            }
            DeleteCacheKey(MemcacheCacheHelper.CreateDependencyCacheKey(dependencyKey, cacheKey));
            return objBizBase.GetDataSetView(condition, parms, sortExpression, fields);
        }

        public static DataSet GetDataSetViewLimitCache<D, T, Key>(this BizBase<D, T, Key> objBizBase, string dependencyKey, bool isCache, string condition, string sortExpression, int offset, int limit, string fields = "*") where D: DalBase<T, Key> where T: class, new()
        {
            TimeSpan expiry = new TimeSpan(0, ConfigHelper.GetIntValue("PageCacheMinutes", 60), 0);
            return objBizBase.GetDataSetViewLimitCache<D, T, Key>(dependencyKey, isCache, expiry, condition, sortExpression, offset, limit, fields);
        }

        public static DataSet GetDataSetViewLimitCache<D, T, Key>(this BizBase<D, T, Key> objBizBase, string dependencyKey, bool isCache, string condition, MySqlParameter[] parms, string sortExpression, int offset, int limit, string fields = "*") where D: DalBase<T, Key> where T: class, new()
        {
            TimeSpan expiry = new TimeSpan(0, ConfigHelper.GetIntValue("PageCacheMinutes", 60), 0);
            return objBizBase.GetDataSetViewLimitCache<D, T, Key>(dependencyKey, isCache, expiry, condition, parms, sortExpression, offset, limit, fields);
        }

        public static DataSet GetDataSetViewLimitCache<D, T, Key>(this BizBase<D, T, Key> objBizBase, string dependencyKey, bool isCache, TimeSpan expiry, string condition, string sortExpression, int offset, int limit, string fields = "*") where D: DalBase<T, Key> where T: class, new()
        {
            return objBizBase.GetDataSetViewLimitCache<D, T, Key>(dependencyKey, isCache, expiry, condition, null, sortExpression, offset, limit, fields);
        }

        public static DataSet GetDataSetViewLimitCache<D, T, Key>(this BizBase<D, T, Key> objBizBase, string dependencyKey, bool isCache, TimeSpan expiry, string condition, MySqlParameter[] parms, string sortExpression, int offset, int limit, string fields = "*") where D: DalBase<T, Key> where T: class, new()
        {
            dependencyKey = objBizBase.GetDependencyKey<D, T, Key>(dependencyKey);
            DataSet dependencyCache = null;
            string cacheKey = string.Concat(new object[] { objBizBase.Dal.TableViewName.ToLower(), "DataSetLimit", GetCondtiionParam(condition, parms), sortExpression, offset, limit, fields }).Replace(" ", "").ToLower();
            if (isCache)
            {
                dependencyCache = cacheKey.GetDependencyCache<DataSet>(dependencyKey);
                if (dependencyCache == null)
                {
                    dependencyCache = objBizBase.GetDataSetViewLimit(condition, parms, sortExpression, offset, limit, fields);
                    dependencyCache.SetDependencyCache(dependencyKey, cacheKey, expiry);
                }
                return dependencyCache;
            }
            DeleteCacheKey(MemcacheCacheHelper.CreateDependencyCacheKey(dependencyKey, cacheKey));
            return objBizBase.GetDataSetViewLimit(condition, parms, sortExpression, offset, limit, fields);
        }

        private static string GetDependencyKey<D, T, Key>(this BizBase<D, T, Key> objBizBase, string dependencyKey = "") where D: DalBase<T, Key> where T: class, new()
        {
            if (string.IsNullOrWhiteSpace(dependencyKey))
            {
                dependencyKey = "Dependency_" + objBizBase.Dal.TableName.ToLower();
            }
            return dependencyKey;
        }

        public static IList<T> GetListCache<D, T, Key>(this BizBase<D, T, Key> objBizBase, string dependencyKey, bool isCache, string condition, string sortExpression = "") where D: DalBase<T, Key> where T: class, new()
        {
            TimeSpan expiry = new TimeSpan(0, ConfigHelper.GetIntValue("PageCacheMinutes", 60), 0);
            return objBizBase.GetListCache<D, T, Key>(dependencyKey, isCache, expiry, condition, sortExpression);
        }

        public static IList<T> GetListCache<D, T, Key>(this BizBase<D, T, Key> objBizBase, string dependencyKey, bool isCache, string condition, MySqlParameter[] parms, string sortExpression = "") where D: DalBase<T, Key> where T: class, new()
        {
            TimeSpan expiry = new TimeSpan(0, ConfigHelper.GetIntValue("PageCacheMinutes", 60), 0);
            return objBizBase.GetListCache<D, T, Key>(dependencyKey, isCache, expiry, condition, parms, sortExpression);
        }

        public static IList<T> GetListCache<D, T, Key>(this BizBase<D, T, Key> objBizBase, string dependencyKey, bool isCache, TimeSpan expiry, string condition, string sortExpression = "") where D: DalBase<T, Key> where T: class, new()
        {
            return objBizBase.GetListCache<D, T, Key>(dependencyKey, isCache, expiry, condition, null, sortExpression);
        }

        public static IList<T> GetListCache<D, T, Key>(this BizBase<D, T, Key> objBizBase, string dependencyKey, bool isCache, TimeSpan expiry, string condition, MySqlParameter[] parms, string sortExpression = "") where D: DalBase<T, Key> where T: class, new()
        {
            dependencyKey = objBizBase.GetDependencyKey<D, T, Key>(dependencyKey);
            string cacheKey = (objBizBase.Dal.TableName.ToLower() + "List" + GetCondtiionParam(condition, parms) + sortExpression).Replace(" ", "").ToLower();
            if (isCache)
            {
                IList<T> dependencyCache = cacheKey.GetDependencyCache<IList<T>>(dependencyKey);
                if (dependencyCache == null)
                {
                    dependencyCache = objBizBase.GetList(condition, parms, sortExpression);
                    dependencyCache.SetDependencyCache(dependencyKey, cacheKey, expiry);
                }
                return dependencyCache;
            }
            DeleteCacheKey(MemcacheCacheHelper.CreateDependencyCacheKey(dependencyKey, cacheKey));
            return objBizBase.GetList(condition, parms, sortExpression);
        }

        public static IList<T> GetListFieldsCache<D, T, Key>(this BizBase<D, T, Key> objBizBase, string dependencyKey, bool isCache, string condition, MySqlParameter[] parms, string fields, string sortExpression = "") where D: DalBase<T, Key> where T: class, new()
        {
            TimeSpan expiry = new TimeSpan(0, ConfigHelper.GetIntValue("PageCacheMinutes", 60), 0);
            return objBizBase.GetListFieldsCache<D, T, Key>(dependencyKey, isCache, expiry, condition, parms, fields, sortExpression);
        }

        public static IList<T> GetListFieldsCache<D, T, Key>(this BizBase<D, T, Key> objBizBase, string dependencyKey, bool isCache, TimeSpan expiry, string condition, MySqlParameter[] parms, string fields, string sortExpression = "") where D: DalBase<T, Key> where T: class, new()
        {
            dependencyKey = objBizBase.GetDependencyKey<D, T, Key>(dependencyKey);
            string cacheKey = (objBizBase.Dal.TableName.ToLower() + "List" + GetCondtiionParam(condition, parms) + sortExpression + (string.IsNullOrWhiteSpace(fields) ? "" : fields)).Replace(" ", "").ToLower();
            if (isCache)
            {
                IList<T> dependencyCache = cacheKey.GetDependencyCache<IList<T>>(dependencyKey);
                if (dependencyCache == null)
                {
                    dependencyCache = objBizBase.GetListFields(condition, parms, sortExpression, fields);
                    dependencyCache.SetDependencyCache(dependencyKey, cacheKey, expiry);
                }
                return dependencyCache;
            }
            DeleteCacheKey(MemcacheCacheHelper.CreateDependencyCacheKey(dependencyKey, cacheKey));
            return objBizBase.GetListFields(condition, parms, sortExpression, fields);
        }

        public static DataSet GetPageCache<D, T, Key>(this BizBase<D, T, Key> objBizBase, string dependencyKey, bool isCache, string condition, string sortExpression, int pageSize, int pageIndex, string fields = "*") where D: DalBase<T, Key> where T: class, new()
        {
            int recordCount = 0;
            return objBizBase.GetPageCache<D, T, Key>(dependencyKey, isCache, condition, sortExpression, pageSize, pageIndex, out recordCount, fields);
        }

        public static DataSet GetPageCache<D, T, Key>(this BizBase<D, T, Key> objBizBase, string dependencyKey, bool isCache, string condition, string sortExpression, int pageSize, int pageIndex, out int recordCount, string fields = "*") where D: DalBase<T, Key> where T: class, new()
        {
            TimeSpan expiry = new TimeSpan(0, ConfigHelper.GetIntValue("PageCacheMinutes", 60), 0);
            return objBizBase.GetPageCache<D, T, Key>(dependencyKey, isCache, expiry, condition, sortExpression, pageSize, pageIndex, out recordCount, fields);
        }

        public static DataSet GetPageCache<D, T, Key>(this BizBase<D, T, Key> objBizBase, string dependencyKey, bool isCache, string condition, MySqlParameter[] parms, string sortExpression, int pageSize, int pageIndex, string fields = "*") where D: DalBase<T, Key> where T: class, new()
        {
            int recordCount = 0;
            return objBizBase.GetPageCache<D, T, Key>(dependencyKey, isCache, condition, parms, sortExpression, pageSize, pageIndex, out recordCount, fields);
        }

        public static DataSet GetPageCache<D, T, Key>(this BizBase<D, T, Key> objBizBase, string dependencyKey, bool isCache, TimeSpan expiry, string condition, string sortExpression, int pageSize, int pageIndex, string fields = "*") where D: DalBase<T, Key> where T: class, new()
        {
            int recordCount = 0;
            return objBizBase.GetPageCache<D, T, Key>(dependencyKey, isCache, expiry, condition, sortExpression, pageSize, pageIndex, out recordCount, fields);
        }

        public static DataSet GetPageCache<D, T, Key>(this BizBase<D, T, Key> objBizBase, string dependencyKey, bool isCache, string condition, MySqlParameter[] parms, string sortExpression, int pageSize, int pageIndex, out int recordCount, string fields = "*") where D: DalBase<T, Key> where T: class, new()
        {
            TimeSpan expiry = new TimeSpan(0, ConfigHelper.GetIntValue("PageCacheMinutes", 60), 0);
            return objBizBase.GetPageCache<D, T, Key>(dependencyKey, isCache, expiry, condition, parms, sortExpression, pageSize, pageIndex, out recordCount, fields);
        }

        public static DataSet GetPageCache<D, T, Key>(this BizBase<D, T, Key> objBizBase, string dependencyKey, bool isCache, TimeSpan expiry, string condition, string sortExpression, int pageSize, int pageIndex, out int recordCount, string fields = "*") where D: DalBase<T, Key> where T: class, new()
        {
            return objBizBase.GetPageCache<D, T, Key>(dependencyKey, isCache, expiry, condition, null, sortExpression, pageSize, pageIndex, out recordCount, fields);
        }

        public static DataSet GetPageCache<D, T, Key>(this BizBase<D, T, Key> objBizBase, string dependencyKey, bool isCache, TimeSpan expiry, string condition, MySqlParameter[] parms, string sortExpression, int pageSize, int pageIndex, out int recordCount, string fields = "*") where D: DalBase<T, Key> where T: class, new()
        {
            dependencyKey = objBizBase.GetDependencyKey<D, T, Key>(dependencyKey);
            string cacheKey = string.Concat(new object[] { objBizBase.Dal.TableName.ToLower(), "Page", GetCondtiionParam(condition, parms), sortExpression, pageSize, pageIndex, fields }).Replace(" ", "").ToLower();
            if (isCache)
            {
                PageCache dependencyCache = cacheKey.GetDependencyCache<PageCache>(dependencyKey);
                if (dependencyCache != null)
                {
                    recordCount = dependencyCache.RecordCount;
                    return dependencyCache.Data;
                }
                DataSet set = objBizBase.GetPage(condition, parms, sortExpression, pageSize, pageIndex, out recordCount, fields);
                new PageCache { RecordCount = recordCount, Data = set }.SetDependencyCache(dependencyKey, cacheKey, expiry);
                return set;
            }
            DeleteCacheKey(MemcacheCacheHelper.CreateDependencyCacheKey(dependencyKey, cacheKey));
            return objBizBase.GetPage(condition, parms, sortExpression, pageSize, pageIndex, out recordCount, fields);
        }

        public static IList<T> GetPageListCache<D, T, Key>(this BizBase<D, T, Key> objBizBase, string dependencyKey, bool isCache, string condition, int pageSize, int pageIndex, string sortExpression = "") where D: DalBase<T, Key> where T: class, new()
        {
            TimeSpan expiry = new TimeSpan(0, ConfigHelper.GetIntValue("PageCacheMinutes", 60), 0);
            return objBizBase.GetPageListCache<D, T, Key>(dependencyKey, isCache, expiry, condition, pageSize, pageIndex, sortExpression);
        }

        public static IList<T> GetPageListCache<D, T, Key>(this BizBase<D, T, Key> objBizBase, string dependencyKey, bool isCache, string condition, MySqlParameter[] parms, int pageSize, int pageIndex, string sortExpression = "") where D: DalBase<T, Key> where T: class, new()
        {
            TimeSpan expiry = new TimeSpan(0, ConfigHelper.GetIntValue("PageCacheMinutes", 60), 0);
            return objBizBase.GetPageListCache<D, T, Key>(dependencyKey, isCache, expiry, condition, parms, pageSize, pageIndex, sortExpression);
        }

        public static IList<T> GetPageListCache<D, T, Key>(this BizBase<D, T, Key> objBizBase, string dependencyKey, bool isCache, string condition, int pageSize, int pageIndex, out int recordCount, string sortExpression = "") where D: DalBase<T, Key> where T: class, new()
        {
            return objBizBase.GetPageListCache<D, T, Key>(dependencyKey, isCache, condition, null, pageSize, pageIndex, out recordCount, sortExpression);
        }

        public static IList<T> GetPageListCache<D, T, Key>(this BizBase<D, T, Key> objBizBase, string dependencyKey, bool isCache, TimeSpan expiry, string condition, int pageSize, int pageIndex, string sortExpression = "") where D: DalBase<T, Key> where T: class, new()
        {
            return objBizBase.GetPageListCache<D, T, Key>(dependencyKey, isCache, expiry, condition, null, pageSize, pageIndex, sortExpression);
        }

        public static IList<T> GetPageListCache<D, T, Key>(this BizBase<D, T, Key> objBizBase, string dependencyKey, bool isCache, string condition, MySqlParameter[] parms, int pageSize, int pageIndex, out int recordCount, string sortExpression = "") where D: DalBase<T, Key> where T: class, new()
        {
            TimeSpan expiry = new TimeSpan(0, ConfigHelper.GetIntValue("PageCacheMinutes", 60), 0);
            return objBizBase.GetPageListCache<D, T, Key>(dependencyKey, isCache, expiry, condition, parms, pageSize, pageIndex, out recordCount, sortExpression);
        }

        public static IList<T> GetPageListCache<D, T, Key>(this BizBase<D, T, Key> objBizBase, string dependencyKey, bool isCache, TimeSpan expiry, string condition, MySqlParameter[] parms, int pageSize, int pageIndex, string sortExpression = "") where D: DalBase<T, Key> where T: class, new()
        {
            dependencyKey = objBizBase.GetDependencyKey<D, T, Key>(dependencyKey);
            string cacheKey = string.Concat(new object[] { objBizBase.Dal.TableName.ToLower(), "PageList", GetCondtiionParam(condition, parms), sortExpression, pageSize, pageIndex }).Replace(" ", "").ToLower();
            if (isCache)
            {
                IList<T> dependencyCache = cacheKey.GetDependencyCache<IList<T>>(dependencyKey);
                if (dependencyCache == null)
                {
                    dependencyCache = objBizBase.GetPageList(condition, parms, sortExpression, pageSize, pageIndex);
                    dependencyCache.SetDependencyCache(dependencyKey, cacheKey, expiry);
                }
                return dependencyCache;
            }
            DeleteCacheKey(MemcacheCacheHelper.CreateDependencyCacheKey(dependencyKey, cacheKey));
            return objBizBase.GetPageList(condition, parms, sortExpression, pageSize, pageIndex);
        }

        public static IList<T> GetPageListCache<D, T, Key>(this BizBase<D, T, Key> objBizBase, string dependencyKey, bool isCache, TimeSpan expiry, string condition, MySqlParameter[] parms, int pageSize, int pageIndex, out int recordCount, string sortExpression = "") where D: DalBase<T, Key> where T: class, new()
        {
            dependencyKey = objBizBase.GetDependencyKey<D, T, Key>(dependencyKey);
            string cacheKey = string.Concat(new object[] { objBizBase.Dal.TableName.ToLower(), "PageListCount", GetCondtiionParam(condition, parms), sortExpression, pageSize, pageIndex }).Replace(" ", "").ToLower();
            if (isCache)
            {
                PageCache<T> dependencyCache = cacheKey.GetDependencyCache<PageCache<T>>(dependencyKey);
                if (dependencyCache != null)
                {
                    recordCount = dependencyCache.RecordCount;
                    return dependencyCache.Data;
                }
                dependencyCache = new PageCache<T> {
                    Data = objBizBase.GetPageList(condition, parms, sortExpression, pageSize, pageIndex, out recordCount),
                    RecordCount = recordCount
                };
                dependencyCache.SetDependencyCache(dependencyKey, cacheKey, expiry);
                return dependencyCache.Data;
            }
            DeleteCacheKey(MemcacheCacheHelper.CreateDependencyCacheKey(dependencyKey, cacheKey));
            return objBizBase.GetPageList(condition, parms, sortExpression, pageSize, pageIndex, out recordCount);
        }

        public static IList<T> GetPageListFieldsCache<D, T, Key>(this BizBase<D, T, Key> objBizBase, string dependencyKey, bool isCache, string condition, int pageSize, int pageIndex, string fields, string sortExpression = "") where D: DalBase<T, Key> where T: class, new()
        {
            return objBizBase.GetPageListFieldsCache<D, T, Key>(dependencyKey, isCache, condition, null, pageSize, pageIndex, fields, sortExpression);
        }

        public static IList<T> GetPageListFieldsCache<D, T, Key>(this BizBase<D, T, Key> objBizBase, string dependencyKey, bool isCache, string condition, MySqlParameter[] parms, int pageSize, int pageIndex, string fields, string sortExpression = "") where D: DalBase<T, Key> where T: class, new()
        {
            TimeSpan expiry = new TimeSpan(0, ConfigHelper.GetIntValue("PageCacheMinutes", 60), 0);
            return objBizBase.GetPageListFieldsCache<D, T, Key>(dependencyKey, isCache, expiry, condition, parms, pageSize, pageIndex, fields, sortExpression);
        }

        public static IList<T> GetPageListFieldsCache<D, T, Key>(this BizBase<D, T, Key> objBizBase, string dependencyKey, bool isCache, string condition, int pageSize, int pageIndex, out int recordCount, string fields, string sortExpression = "") where D: DalBase<T, Key> where T: class, new()
        {
            return objBizBase.GetPageListFieldsCache<D, T, Key>(dependencyKey, isCache, condition, null, pageSize, pageIndex, out recordCount, fields, sortExpression);
        }

        public static IList<T> GetPageListFieldsCache<D, T, Key>(this BizBase<D, T, Key> objBizBase, string dependencyKey, bool isCache, string condition, MySqlParameter[] parms, int pageSize, int pageIndex, out int recordCount, string fields, string sortExpression = "") where D: DalBase<T, Key> where T: class, new()
        {
            TimeSpan expiry = new TimeSpan(0, ConfigHelper.GetIntValue("PageCacheMinutes", 60), 0);
            return objBizBase.GetPageListFieldsCache<D, T, Key>(dependencyKey, isCache, expiry, condition, parms, pageSize, pageIndex, out recordCount, fields, sortExpression);
        }

        public static IList<T> GetPageListFieldsCache<D, T, Key>(this BizBase<D, T, Key> objBizBase, string dependencyKey, bool isCache, TimeSpan expiry, string condition, MySqlParameter[] parms, int pageSize, int pageIndex, string fields, string sortExpression = "") where D: DalBase<T, Key> where T: class, new()
        {
            dependencyKey = objBizBase.GetDependencyKey<D, T, Key>(dependencyKey);
            string cacheKey = string.Concat(new object[] { objBizBase.Dal.TableName.ToLower(), "PageList", GetCondtiionParam(condition, parms), sortExpression, pageSize, pageIndex, string.IsNullOrWhiteSpace(fields) ? "" : fields }).Replace(" ", "").ToLower();
            if (isCache)
            {
                IList<T> dependencyCache = cacheKey.GetDependencyCache<IList<T>>(dependencyKey);
                if (dependencyCache == null)
                {
                    dependencyCache = objBizBase.GetPageListFields(condition, parms, sortExpression, pageSize, pageIndex, fields);
                    dependencyCache.SetDependencyCache(dependencyKey, cacheKey, expiry);
                }
                return dependencyCache;
            }
            DeleteCacheKey(MemcacheCacheHelper.CreateDependencyCacheKey(dependencyKey, cacheKey));
            return objBizBase.GetPageListFields(condition, parms, sortExpression, pageSize, pageIndex, fields);
        }

        public static IList<T> GetPageListFieldsCache<D, T, Key>(this BizBase<D, T, Key> objBizBase, string dependencyKey, bool isCache, TimeSpan expiry, string condition, MySqlParameter[] parms, int pageSize, int pageIndex, out int recordCount, string fields, string sortExpression = "") where D: DalBase<T, Key> where T: class, new()
        {
            dependencyKey = objBizBase.GetDependencyKey<D, T, Key>(dependencyKey);
            string cacheKey = string.Concat(new object[] { objBizBase.Dal.TableName.ToLower(), "PageListCount", GetCondtiionParam(condition, parms), sortExpression, pageSize, pageIndex, string.IsNullOrWhiteSpace(fields) ? "" : fields }).Replace(" ", "").ToLower();
            if (isCache)
            {
                PageCache<T> dependencyCache = cacheKey.GetDependencyCache<PageCache<T>>(dependencyKey);
                if (dependencyCache != null)
                {
                    recordCount = dependencyCache.RecordCount;
                    return dependencyCache.Data;
                }
                dependencyCache = new PageCache<T> {
                    Data = objBizBase.GetPageListFields(condition, parms, sortExpression, pageSize, pageIndex, out recordCount, fields),
                    RecordCount = recordCount
                };
                dependencyCache.SetDependencyCache(dependencyKey, cacheKey, expiry);
                return dependencyCache.Data;
            }
            DeleteCacheKey(MemcacheCacheHelper.CreateDependencyCacheKey(dependencyKey, cacheKey));
            return objBizBase.GetPageListFields(condition, parms, sortExpression, pageSize, pageIndex, out recordCount, fields);
        }

        public static DataSet GetPageViewCache<D, T, Key>(this BizBase<D, T, Key> objBizBase, string dependencyKey, bool isCache, string condition, string sortExpression, int pageSize, int pageIndex, string fields = "*") where D: DalBase<T, Key> where T: class, new()
        {
            int recordCount = 0;
            return objBizBase.GetPageViewCache<D, T, Key>(dependencyKey, isCache, condition, sortExpression, pageSize, pageIndex, out recordCount, fields);
        }

        public static DataSet GetPageViewCache<D, T, Key>(this BizBase<D, T, Key> objBizBase, string dependencyKey, bool isCache, string condition, string sortExpression, int pageSize, int pageIndex, out int recordCount, string fields = "*") where D: DalBase<T, Key> where T: class, new()
        {
            TimeSpan expiry = new TimeSpan(0, ConfigHelper.GetIntValue("PageCacheMinutes", 60), 0);
            return objBizBase.GetPageViewCache<D, T, Key>(dependencyKey, isCache, expiry, condition, sortExpression, pageSize, pageIndex, out recordCount, fields);
        }

        public static DataSet GetPageViewCache<D, T, Key>(this BizBase<D, T, Key> objBizBase, string dependencyKey, bool isCache, string condition, MySqlParameter[] parms, string sortExpression, int pageSize, int pageIndex, string fields = "*") where D: DalBase<T, Key> where T: class, new()
        {
            int recordCount = 0;
            return objBizBase.GetPageViewCache<D, T, Key>(dependencyKey, isCache, condition, parms, sortExpression, pageSize, pageIndex, out recordCount, fields);
        }

        public static DataSet GetPageViewCache<D, T, Key>(this BizBase<D, T, Key> objBizBase, string dependencyKey, bool isCache, TimeSpan expiry, string condition, string sortExpression, int pageSize, int pageIndex, string fields = "*") where D: DalBase<T, Key> where T: class, new()
        {
            int recordCount = 0;
            return objBizBase.GetPageViewCache<D, T, Key>(dependencyKey, isCache, expiry, condition, sortExpression, pageSize, pageIndex, out recordCount, fields);
        }

        public static DataSet GetPageViewCache<D, T, Key>(this BizBase<D, T, Key> objBizBase, string dependencyKey, bool isCache, string condition, MySqlParameter[] parms, string sortExpression, int pageSize, int pageIndex, out int recordCount, string fields = "*") where D: DalBase<T, Key> where T: class, new()
        {
            TimeSpan expiry = new TimeSpan(0, ConfigHelper.GetIntValue("PageCacheMinutes", 60), 0);
            return objBizBase.GetPageViewCache<D, T, Key>(dependencyKey, isCache, expiry, condition, parms, sortExpression, pageSize, pageIndex, out recordCount, fields);
        }

        public static DataSet GetPageViewCache<D, T, Key>(this BizBase<D, T, Key> objBizBase, string dependencyKey, bool isCache, TimeSpan expiry, string condition, string sortExpression, int pageSize, int pageIndex, out int recordCount, string fields = "*") where D: DalBase<T, Key> where T: class, new()
        {
            return objBizBase.GetPageViewCache<D, T, Key>(dependencyKey, isCache, expiry, condition, null, sortExpression, pageSize, pageIndex, out recordCount, fields);
        }

        public static DataSet GetPageViewCache<D, T, Key>(this BizBase<D, T, Key> objBizBase, string dependencyKey, bool isCache, TimeSpan expiry, string condition, MySqlParameter[] parms, string sortExpression, int pageSize, int pageIndex, out int recordCount, string fields = "*") where D: DalBase<T, Key> where T: class, new()
        {
            dependencyKey = objBizBase.GetDependencyKey<D, T, Key>(dependencyKey);
            string cacheKey = string.Concat(new object[] { objBizBase.Dal.TableViewName.ToLower(), "Page", GetCondtiionParam(condition, parms), sortExpression, pageSize, pageIndex, fields }).Replace(" ", "").ToLower();
            if (isCache)
            {
                PageCache dependencyCache = cacheKey.GetDependencyCache<PageCache>(dependencyKey);
                if (dependencyCache != null)
                {
                    recordCount = dependencyCache.RecordCount;
                    return dependencyCache.Data;
                }
                DataSet set = objBizBase.GetPageView(condition, parms, sortExpression, pageSize, pageIndex, out recordCount, fields);
                new PageCache { RecordCount = recordCount, Data = set }.SetDependencyCache(dependencyKey, cacheKey, expiry);
                return set;
            }
            DeleteCacheKey(MemcacheCacheHelper.CreateDependencyCacheKey(dependencyKey, cacheKey));
            return objBizBase.GetPageView(condition, parms, sortExpression, pageSize, pageIndex, out recordCount, fields);
        }

        public static T GetRecordCache<D, T, Key>(this BizBase<D, T, Key> objBizBase, Key ID, bool isCache = true) where D: DalBase<T, Key> where T: class, new()
        {
            TimeSpan timeSpan = new TimeSpan(0, ConfigHelper.GetIntValue("RecordCacheMinutes", 60), 0);
            return objBizBase.GetRecordCache<D, T, Key>(ID, timeSpan, isCache);
        }

        public static T GetRecordCache<D, T, Key>(this BizBase<D, T, Key> objBizBase, Key ID, TimeSpan timeSpan, bool isCache = true) where D: DalBase<T, Key> where T: class, new()
        {
            string cacheKey = string.Format(objBizBase.Dal.TableName.ToLower() + "_" + ID.ToString(), new object[0]);
            if (!isCache)
            {
                cacheKey.DeleteCacheMemcached();
                return objBizBase.GetRecord(ID);
            }
            string str2 = ConfigHelper.GetValue("Memcache_ForeverTB", "");
            if (!(string.IsNullOrWhiteSpace(str2) || !str2.ToLower().Contains(objBizBase.Dal.TableName.ToLower())))
            {
                timeSpan = new TimeSpan(ConfigHelper.GetIntValue("RecordCacheForeverHours", 0x30), 0, 0);
            }
            T cacheMemcached = cacheKey.GetCacheMemcached<T>();
            if (cacheMemcached == null)
            {
                cacheMemcached = objBizBase.GetRecord(ID);
                if (cacheMemcached == null)
                {
                    return cacheMemcached;
                }
                cacheMemcached.SetCacheMemcached(cacheKey, timeSpan);
            }
            return cacheMemcached;
        }

        public static T GetRecordCacheGuid<D, T, Key>(this BizBase<D, T, Key> objBizBase, string guid, bool isCache = true) where D: DalBase<T, Key> where T: class, new()
        {
            return objBizBase.GetRecordCacheGuid<D, T, Key>(guid, "Guid", isCache);
        }

        public static T GetRecordCacheGuid<D, T, Key>(this BizBase<D, T, Key> objBizBase, string guid, string guidFileName, bool isCache = true) where D: DalBase<T, Key> where T: class, new()
        {
            TimeSpan timeSpan = new TimeSpan(0, ConfigHelper.GetIntValue("RecordCacheMinutes", 60), 0);
            return objBizBase.GetRecordCacheGuid<D, T, Key>(guid, guidFileName, timeSpan, isCache);
        }

        public static T GetRecordCacheGuid<D, T, Key>(this BizBase<D, T, Key> objBizBase, string guid, string guidFileName, TimeSpan timeSpan, bool isCache = true) where D: DalBase<T, Key> where T: class, new()
        {
            string cacheKey = string.Format(objBizBase.Dal.TableName.ToLower() + "_" + guid.ToLower(), new object[0]);
            if (!isCache)
            {
                cacheKey.DeleteCacheMemcached();
                return objBizBase.GetRecord(guidFileName + "=?Guid", "Guid".CreateSqlParameter(new object[] { guid }), "");
            }
            T cacheMemcached = cacheKey.GetCacheMemcached<T>();
            if (cacheMemcached == null)
            {
                cacheMemcached = objBizBase.GetRecord(guidFileName + "=?Guid", "Guid".CreateSqlParameter(new object[] { guid }), "");
                if (cacheMemcached == null)
                {
                    return cacheMemcached;
                }
                cacheMemcached.SetCacheMemcached(cacheKey, timeSpan);
            }
            return cacheMemcached;
        }

        public static T GetRecordConditionCache<D, T, Key>(this BizBase<D, T, Key> objBizBase, bool isCache, string idKey, string condition, string sortExpression = "") where D: DalBase<T, Key> where T: class, new()
        {
            TimeSpan expiry = new TimeSpan(0, ConfigHelper.GetIntValue("PageCacheMinutes", 60), 0);
            return objBizBase.GetRecordConditionCache<D, T, Key>(isCache, expiry, idKey, condition, sortExpression);
        }

        public static T GetRecordConditionCache<D, T, Key>(this BizBase<D, T, Key> objBizBase, bool isCache, string idKey, string condition, MySqlParameter[] parms, string sortExpression = "") where D: DalBase<T, Key> where T: class, new()
        {
            TimeSpan expiry = new TimeSpan(0, ConfigHelper.GetIntValue("PageCacheMinutes", 60), 0);
            return objBizBase.GetRecordConditionCache<D, T, Key>(isCache, expiry, idKey, condition, parms, sortExpression);
        }

        public static T GetRecordConditionCache<D, T, Key>(this BizBase<D, T, Key> objBizBase, bool isCache, TimeSpan expiry, string idKey, string condition, string sortExpression = "") where D: DalBase<T, Key> where T: class, new()
        {
            return objBizBase.GetRecordConditionCache<D, T, Key>(isCache, expiry, idKey, condition, null, sortExpression);
        }

        public static T GetRecordConditionCache<D, T, Key>(this BizBase<D, T, Key> objBizBase, bool isCache, TimeSpan expiry, string idKey, string condition, MySqlParameter[] parms, string sortExpression = "") where D: DalBase<T, Key> where T: class, new()
        {
            T cacheMemcached;
            string cacheKey = "";
            if (string.IsNullOrWhiteSpace(idKey))
            {
                string dependencyKey = objBizBase.GetDependencyKey<D, T, Key>("");
                cacheKey = (objBizBase.Dal.TableName.ToLower() + "_IDKey_" + GetCondtiionParam(condition, parms) + sortExpression).Replace(" ", "").ToLower();
                if (!isCache)
                {
                    DeleteCacheKey(MemcacheCacheHelper.CreateDependencyCacheKey(dependencyKey, cacheKey));
                    return objBizBase.GetRecord(condition, parms, sortExpression);
                }
                cacheMemcached = cacheKey.GetDependencyCache<T>(dependencyKey);
                if (cacheMemcached == null)
                {
                    cacheMemcached = objBizBase.GetRecord(condition, parms, sortExpression);
                    if (cacheMemcached == null)
                    {
                        return cacheMemcached;
                    }
                    cacheMemcached.SetDependencyCache(dependencyKey, cacheKey, expiry);
                }
                return cacheMemcached;
            }
            cacheKey = string.Format(objBizBase.Dal.TableName.ToLower() + "_" + idKey.ToLower(), new object[0]);
            if (!isCache)
            {
                cacheKey.DeleteCacheMemcached();
                return objBizBase.GetRecord(condition, parms, sortExpression);
            }
            cacheMemcached = cacheKey.GetCacheMemcached<T>();
            if (cacheMemcached == null)
            {
                cacheMemcached = objBizBase.GetRecord(condition, parms, sortExpression);
                if (cacheMemcached == null)
                {
                    return cacheMemcached;
                }
                cacheMemcached.SetCacheMemcached(cacheKey, expiry);
            }
            return cacheMemcached;
        }

        public static object GetScalarCache<D, T, Key>(this BizBase<D, T, Key> objBizBase, string dependencyKey, bool isCache, TimeSpan expiry, string fieldName, string condition, MySqlParameter[] parms) where D: DalBase<T, Key> where T: class, new()
        {
            dependencyKey = objBizBase.GetDependencyKey<D, T, Key>(dependencyKey);
            object dependencyCache = null;
            string cacheKey = (objBizBase.Dal.TableName.ToLower() + "Scalar" + GetCondtiionParam(condition, parms) + fieldName).Replace(" ", "").ToLower();
            if (isCache)
            {
                dependencyCache = cacheKey.GetDependencyCache<object>(dependencyKey);
                if (dependencyCache == null)
                {
                    dependencyCache = objBizBase.GetScalar(fieldName, condition, parms);
                    dependencyCache.SetDependencyCache(dependencyKey, cacheKey, expiry);
                }
                return dependencyCache;
            }
            DeleteCacheKey(MemcacheCacheHelper.CreateDependencyCacheKey(dependencyKey, cacheKey));
            return objBizBase.GetScalar(fieldName, condition, parms);
        }
    }
}

