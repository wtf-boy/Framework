using WTF.Cache.Helper;
using WTF.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;

namespace WTF.Solr
{
	public static class SolrCacheBindHelper
	{
		private static string GetDependencyKey<T>(this SolrQuery<T> objSolrQuery, string dependencyKey = "") where T : class, new()
		{
			if (string.IsNullOrWhiteSpace(dependencyKey))
			{
				dependencyKey = "SolrDependency_" + objSolrQuery.TableName.ToLower();
			}
			return dependencyKey;
		}

		public static DataSet QueryDataSetCache<T>(this SolrQuery<T> objSolrQuery, string dependencyKey, bool isCache, TimeSpan expiry, string fields, string condition, string qf, int limit, string sortExpression, string bf = "") where T : class, new()
		{
			return objSolrQuery.QueryDataSetCache(dependencyKey, isCache, expiry, fields, condition, qf, "", limit, sortExpression, bf);
		}

		public static DataSet QueryDataSetCache<T>(this SolrQuery<T> objSolrQuery, string dependencyKey, bool isCache, TimeSpan expiry, string fields, string condition, string qf, string highlightFields, int limit, string sortExpression, string bf = "") where T : class, new()
		{
			long num = 0L;
			return objSolrQuery.QueryDataSetLimitCache(dependencyKey, isCache, expiry, fields, condition, qf, highlightFields, 0, limit, sortExpression, out num, bf);
		}

		public static DataSet QueryDataSetLimitCache<T>(this SolrQuery<T> objSolrQuery, string dependencyKey, bool isCache, TimeSpan expiry, string fields, string condition, string qf, string highlightFields, int offset, int limit, string sortExpression, out long recordCount, string bf = "") where T : class, new()
		{
			dependencyKey = objSolrQuery.GetDependencyKey(dependencyKey);
			string text = string.Concat(new object[]
			{
				objSolrQuery.TableName.ToLower(),
				"SolrDataSetLimit",
				fields,
				condition,
				qf,
				highlightFields,
				offset,
				limit,
				sortExpression,
				bf
			});
			text = text.Replace(" ", "").ToLower();
			DataSet result;
			if (isCache)
			{
				PageSolrCache dependencyCache = MemcacheCacheHelper.GetDependencyCache<PageSolrCache>(text, dependencyKey);
				if (dependencyCache != null)
				{
					recordCount = dependencyCache.RecordCount;
					result = dependencyCache.Data;
				}
				else
				{
					DataSet dataSet = objSolrQuery.QueryDataSetLimit(fields, condition, qf, highlightFields, offset, limit, sortExpression, out recordCount, bf);
					MemcacheCacheHelper.SetDependencyCache(new PageSolrCache
					{
						RecordCount = recordCount,
						Data = dataSet
					}, dependencyKey, text, expiry);
					result = dataSet;
				}
			}
			else
			{
				SolrCacheBindHelper.DeleteCacheKey(MemcacheCacheHelper.CreateDependencyCacheKey(dependencyKey, text));
				result = objSolrQuery.QueryDataSetLimit(fields, condition, qf, highlightFields, offset, limit, sortExpression, out recordCount, bf);
			}
			return result;
		}

		public static DataSet QueryDataSetLimitCache<T>(this SolrQuery<T> objSolrQuery, string dependencyKey, bool isCache, TimeSpan expiry, string fields, string condition, string qf, int offset, int limit, string sortExpression, string bf = "") where T : class, new()
		{
			return objSolrQuery.QueryDataSetLimitCache(dependencyKey, isCache, expiry, fields, condition, qf, "", offset, limit, sortExpression, bf);
		}

		public static DataSet QueryDataSetLimitCache<T>(this SolrQuery<T> objSolrQuery, string dependencyKey, bool isCache, TimeSpan expiry, string fields, string condition, string qf, string highlightFields, int offset, int limit, string sortExpression, string bf = "") where T : class, new()
		{
			long num = 0L;
			return objSolrQuery.QueryDataSetLimitCache(dependencyKey, isCache, expiry, fields, condition, qf, highlightFields, offset, limit, sortExpression, out num, bf);
		}

		public static bool DeleteCacheKey<T>(this SolrQuery<T> objSolrQuery, string cacheKey) where T : class, new()
		{
			return MemcacheCacheHelper.DeleteCacheMemcached(cacheKey);
		}

		public static bool DeleteDependencyKey<T>(this SolrQuery<T> objSolrQuery, string dependencyKey = "") where T : class, new()
		{
			return MemcacheCacheHelper.DeleteCacheMemcached(objSolrQuery.GetDependencyKey(dependencyKey));
		}

		private static bool DeleteCacheKey(string cacheKey)
		{
			return MemcacheCacheHelper.DeleteCacheMemcached(cacheKey);
		}

		public static DataSet QueryDataSetCache<T>(this SolrQuery<T> objSolrQuery, string dependencyKey, bool isCache, string fields, string condition, string qf, int limit, string sortExpression, string bf = "") where T : class, new()
		{
			return objSolrQuery.QueryDataSetCache(dependencyKey, isCache, fields, condition, qf, "", limit, sortExpression, bf);
		}

		public static DataSet QueryDataSetCache<T>(this SolrQuery<T> objSolrQuery, string dependencyKey, bool isCache, string fields, string condition, string qf, string highlightFields, int limit, string sortExpression, string bf = "") where T : class, new()
		{
			long num = 0L;
			TimeSpan expiry = new TimeSpan(0, ConfigHelper.GetIntValue("PageCacheMinutes", 60), 0);
			return objSolrQuery.QueryDataSetLimitCache(dependencyKey, isCache, expiry, fields, condition, qf, highlightFields, 0, limit, sortExpression, out num, bf);
		}

		public static DataSet QueryDataSetLimitCache<T>(this SolrQuery<T> objSolrQuery, string dependencyKey, bool isCache, string fields, string condition, string qf, int offset, int limit, string sortExpression, string bf = "") where T : class, new()
		{
			return objSolrQuery.QueryDataSetLimitCache(dependencyKey, isCache, fields, condition, qf, "", offset, limit, sortExpression, bf);
		}

		public static DataSet QueryDataSetLimitCache<T>(this SolrQuery<T> objSolrQuery, string dependencyKey, bool isCache, string fields, string condition, string qf, string highlightFields, int offset, int limit, string sortExpression, string bf = "") where T : class, new()
		{
			long num = 0L;
			TimeSpan expiry = new TimeSpan(0, ConfigHelper.GetIntValue("PageCacheMinutes", 60), 0);
			return objSolrQuery.QueryDataSetLimitCache(dependencyKey, isCache, expiry, fields, condition, qf, highlightFields, offset, limit, sortExpression, out num, bf);
		}

		public static DataSet QueryPageDataSetCache<T>(this SolrQuery<T> objSolrQuery, string dependencyKey, bool isCache, TimeSpan expiry, string fields, string condition, string qf, string highlightFields, int pageSize, int pageIndex, string sortExpression, out long recordCount, string bf = "") where T : class, new()
		{
			dependencyKey = objSolrQuery.GetDependencyKey(dependencyKey);
			string text = string.Concat(new object[]
			{
				objSolrQuery.TableName.ToLower(),
				"SolrPage",
				fields,
				condition,
				qf,
				highlightFields,
				pageSize,
				pageIndex,
				sortExpression,
				bf
			});
			text = text.Replace(" ", "").ToLower();
			DataSet result;
			if (isCache)
			{
				PageSolrCache dependencyCache = MemcacheCacheHelper.GetDependencyCache<PageSolrCache>(text, dependencyKey);
				if (dependencyCache != null)
				{
					recordCount = dependencyCache.RecordCount;
					result = dependencyCache.Data;
				}
				else
				{
					DataSet dataSet = objSolrQuery.QueryPageDataSet(fields, condition, qf, highlightFields, pageSize, pageIndex, sortExpression, out recordCount, bf);
					MemcacheCacheHelper.SetDependencyCache(new PageSolrCache
					{
						RecordCount = recordCount,
						Data = dataSet
					}, dependencyKey, text, expiry);
					result = dataSet;
				}
			}
			else
			{
				SolrCacheBindHelper.DeleteCacheKey(MemcacheCacheHelper.CreateDependencyCacheKey(dependencyKey, text));
				result = objSolrQuery.QueryPageDataSet(fields, condition, qf, highlightFields, pageSize, pageIndex, sortExpression, out recordCount, bf);
			}
			return result;
		}

		public static DataSet QueryPageDataSetCache<T>(this SolrQuery<T> objSolrQuery, string dependencyKey, bool isCache, TimeSpan expiry, string fields, string condition, string qf, int pageSize, int pageIndex, string sortExpression, out long recordCount, string bf = "") where T : class, new()
		{
			return objSolrQuery.QueryPageDataSetCache(dependencyKey, isCache, expiry, fields, condition, qf, "", pageSize, pageIndex, sortExpression, out recordCount, bf);
		}

		public static DataSet QueryPageDataSetCache<T>(this SolrQuery<T> objSolrQuery, string dependencyKey, bool isCache, TimeSpan expiry, string fields, string condition, string qf, int pageSize, int pageIndex, string sortExpression, string bf = "") where T : class, new()
		{
			long num = 0L;
			return objSolrQuery.QueryPageDataSetCache(dependencyKey, isCache, expiry, fields, condition, qf, "", pageSize, pageIndex, sortExpression, out num, bf);
		}

		public static DataSet QueryPageDataSetCache<T>(this SolrQuery<T> objSolrQuery, string dependencyKey, bool isCache, string fields, string condition, string qf, int pageSize, int pageIndex, string sortExpression, out long recordCount, string bf = "") where T : class, new()
		{
			TimeSpan expiry = new TimeSpan(0, ConfigHelper.GetIntValue("PageCacheMinutes", 60), 0);
			return objSolrQuery.QueryPageDataSetCache(dependencyKey, isCache, expiry, fields, condition, qf, "", pageSize, pageIndex, sortExpression, out recordCount, bf);
		}

		public static DataSet QueryPageDataSetCache<T>(this SolrQuery<T> objSolrQuery, string dependencyKey, bool isCache, string fields, string condition, string qf, int pageSize, int pageIndex, string sortExpression, string bf = "") where T : class, new()
		{
			long num = 0L;
			TimeSpan expiry = new TimeSpan(0, ConfigHelper.GetIntValue("PageCacheMinutes", 60), 0);
			return objSolrQuery.QueryPageDataSetCache(dependencyKey, isCache, expiry, fields, condition, qf, "", pageSize, pageIndex, sortExpression, out num, bf);
		}

		public static IEnumerable<T> QueryListCache<T>(this SolrQuery<T> objSolrQuery, string dependencyKey, bool isCache, TimeSpan expiry, string fields, string condition, string qf, int limit, string sortExpression, string bf = "") where T : class, new()
		{
			return objSolrQuery.QueryListCache(dependencyKey, isCache, expiry, fields, condition, qf, "", limit, sortExpression, bf);
		}

		public static IEnumerable<T> QueryListCache<T>(this SolrQuery<T> objSolrQuery, string dependencyKey, bool isCache, TimeSpan expiry, string fields, string condition, string qf, string highlightFields, int limit, string sortExpression, string bf = "") where T : class, new()
		{
			long num = 0L;
			return objSolrQuery.QueryListLimitCache(dependencyKey, isCache, expiry, fields, condition, qf, highlightFields, 0, limit, sortExpression, out num, bf);
		}

		public static IEnumerable<T> QueryListLimitCache<T>(this SolrQuery<T> objSolrQuery, string dependencyKey, bool isCache, TimeSpan expiry, string fields, string condition, string qf, int offset, int limit, string sortExpression, out long recordCount, string bf = "") where T : class, new()
		{
			return objSolrQuery.QueryListLimitCache(dependencyKey, isCache, expiry, fields, condition, qf, "", offset, limit, sortExpression, out recordCount, bf);
		}

		public static IEnumerable<T> QueryListLimitCache<T>(this SolrQuery<T> objSolrQuery, string dependencyKey, bool isCache, TimeSpan expiry, string fields, string condition, string qf, string highlightFields, int offset, int limit, string sortExpression, out long recordCount, string bf = "") where T : class, new()
		{
			dependencyKey = objSolrQuery.GetDependencyKey(dependencyKey);
			string text = string.Concat(new object[]
			{
				objSolrQuery.TableName.ToLower(),
				"SolrList",
				fields,
				condition,
				qf,
				highlightFields,
				offset,
				limit,
				sortExpression,
				bf
			});
			text = text.Replace(" ", "").ToLower();
			IEnumerable<T> result;
			if (isCache)
			{
				PageSolrCache<T> dependencyCache = MemcacheCacheHelper.GetDependencyCache<PageSolrCache<T>>(text, dependencyKey);
				if (dependencyCache != null)
				{
					recordCount = dependencyCache.RecordCount;
					result = dependencyCache.Data;
				}
				else
				{
					IEnumerable<T> enumerable = objSolrQuery.QueryListLimit(fields, condition, qf, highlightFields, offset, limit, sortExpression, out recordCount, bf);
					MemcacheCacheHelper.SetDependencyCache(new PageSolrCache<T>
					{
						RecordCount = recordCount,
						Data = enumerable
					}, dependencyKey, text, expiry);
					result = enumerable;
				}
			}
			else
			{
				SolrCacheBindHelper.DeleteCacheKey(MemcacheCacheHelper.CreateDependencyCacheKey(dependencyKey, text));
				result = objSolrQuery.QueryListLimit(fields, condition, qf, highlightFields, offset, limit, sortExpression, out recordCount, bf);
			}
			return result;
		}

		public static IEnumerable<T> QueryListLimitCache<T>(this SolrQuery<T> objSolrQuery, string dependencyKey, bool isCache, TimeSpan expiry, string fields, string condition, string qf, int offset, int limit, string sortExpression, string bf = "") where T : class, new()
		{
			return objSolrQuery.QueryListLimitCache(dependencyKey, isCache, expiry, fields, condition, qf, "", offset, limit, sortExpression, bf);
		}

		public static IEnumerable<T> QueryListLimitCache<T>(this SolrQuery<T> objSolrQuery, string dependencyKey, bool isCache, TimeSpan expiry, string fields, string condition, string qf, string highlightFields, int offset, int limit, string sortExpression, string bf = "") where T : class, new()
		{
			long num = 0L;
			return objSolrQuery.QueryListLimitCache(dependencyKey, isCache, expiry, fields, condition, qf, highlightFields, offset, limit, sortExpression, out num, bf);
		}

		public static IEnumerable<T> QueryListCache<T>(this SolrQuery<T> objSolrQuery, string dependencyKey, bool isCache, string fields, string condition, string qf, int limit, string sortExpression, string bf = "") where T : class, new()
		{
			return objSolrQuery.QueryListCache(dependencyKey, isCache, fields, condition, qf, "", limit, sortExpression, bf);
		}

		public static IEnumerable<T> QueryListCache<T>(this SolrQuery<T> objSolrQuery, string dependencyKey, bool isCache, string fields, string condition, string qf, string highlightFields, int limit, string sortExpression, string bf = "") where T : class, new()
		{
			long num = 0L;
			TimeSpan expiry = new TimeSpan(0, ConfigHelper.GetIntValue("PageCacheMinutes", 60), 0);
			return objSolrQuery.QueryListLimitCache(dependencyKey, isCache, expiry, fields, condition, qf, highlightFields, 0, limit, sortExpression, out num, bf);
		}

		public static IEnumerable<T> QueryListLimitCache<T>(this SolrQuery<T> objSolrQuery, string dependencyKey, bool isCache, string fields, string condition, string qf, int offset, int limit, string sortExpression, out long recordCount, string bf = "") where T : class, new()
		{
			TimeSpan expiry = new TimeSpan(0, ConfigHelper.GetIntValue("PageCacheMinutes", 60), 0);
			return objSolrQuery.QueryListLimitCache(dependencyKey, isCache, expiry, fields, condition, qf, "", offset, limit, sortExpression, out recordCount, bf);
		}

		public static IEnumerable<T> QueryListLimitCache<T>(this SolrQuery<T> objSolrQuery, string dependencyKey, bool isCache, string fields, string condition, string qf, int offset, int limit, string sortExpression, string bf = "") where T : class, new()
		{
			return objSolrQuery.QueryListLimitCache(dependencyKey, isCache, fields, condition, qf, "", offset, limit, sortExpression, bf);
		}

		public static IEnumerable<T> QueryListLimitCache<T>(this SolrQuery<T> objSolrQuery, string dependencyKey, bool isCache, string fields, string condition, string qf, string highlightFields, int offset, int limit, string sortExpression, string bf = "") where T : class, new()
		{
			long num = 0L;
			TimeSpan expiry = new TimeSpan(0, ConfigHelper.GetIntValue("PageCacheMinutes", 60), 0);
			return objSolrQuery.QueryListLimitCache(dependencyKey, isCache, expiry, fields, condition, qf, highlightFields, offset, limit, sortExpression, out num, bf);
		}

		public static IEnumerable<T> QueryPageCache<T>(this SolrQuery<T> objSolrQuery, string dependencyKey, bool isCache, TimeSpan expiry, string fields, string condition, string qf, int pageSize, int pageIndex, string sortExpression, out long recordCount, string bf = "") where T : class, new()
		{
			return objSolrQuery.QueryPageCache(dependencyKey, isCache, expiry, fields, condition, qf, "", pageSize, pageIndex, sortExpression, out recordCount, bf);
		}

		public static IEnumerable<T> QueryPageCache<T>(this SolrQuery<T> objSolrQuery, string dependencyKey, bool isCache, TimeSpan expiry, string fields, string condition, string qf, string highlightFields, int pageSize, int pageIndex, string sortExpression, out long recordCount, string bf = "") where T : class, new()
		{
			dependencyKey = objSolrQuery.GetDependencyKey(dependencyKey);
			string text = string.Concat(new object[]
			{
				objSolrQuery.TableName.ToLower(),
				"SolrPageList",
				fields,
				condition,
				qf,
				highlightFields,
				pageSize,
				pageIndex,
				sortExpression,
				bf
			});
			text = text.Replace(" ", "").ToLower();
			IEnumerable<T> result;
			if (isCache)
			{
				PageSolrCache<T> dependencyCache = MemcacheCacheHelper.GetDependencyCache<PageSolrCache<T>>(text, dependencyKey);
				if (dependencyCache != null)
				{
					recordCount = dependencyCache.RecordCount;
					result = dependencyCache.Data;
				}
				else
				{
					IEnumerable<T> enumerable = objSolrQuery.QueryPage(fields, condition, qf, highlightFields, pageSize, pageIndex, sortExpression, out recordCount, bf);
					MemcacheCacheHelper.SetDependencyCache(new PageSolrCache<T>
					{
						RecordCount = recordCount,
						Data = enumerable
					}, dependencyKey, text, expiry);
					result = enumerable;
				}
			}
			else
			{
				SolrCacheBindHelper.DeleteCacheKey(MemcacheCacheHelper.CreateDependencyCacheKey(dependencyKey, text));
				result = objSolrQuery.QueryPage(fields, condition, qf, highlightFields, pageSize, pageIndex, sortExpression, out recordCount, bf);
			}
			return result;
		}

		public static IEnumerable<T> QueryPageCache<T>(this SolrQuery<T> objSolrQuery, string dependencyKey, bool isCache, string fields, string condition, string qf, int pageSize, int pageIndex, string sortExpression, out long recordCount, string bf = "") where T : class, new()
		{
			return objSolrQuery.QueryPageCache(dependencyKey, isCache, fields, condition, qf, "", pageSize, pageIndex, sortExpression, out recordCount, bf);
		}

		public static IEnumerable<T> QueryPageCache<T>(this SolrQuery<T> objSolrQuery, string dependencyKey, bool isCache, string fields, string condition, string qf, string highlightFields, int pageSize, int pageIndex, string sortExpression, out long recordCount, string bf = "") where T : class, new()
		{
			TimeSpan expiry = new TimeSpan(0, ConfigHelper.GetIntValue("PageCacheMinutes", 60), 0);
			return objSolrQuery.QueryPageCache(dependencyKey, isCache, expiry, fields, condition, qf, "", pageSize, pageIndex, sortExpression, out recordCount, bf);
		}

		public static IEnumerable<T> QueryListCache<T>(this SolrQuery<T> objSolrQuery, string dependencyKey, bool isCache, string fields, Expression<Func<T, bool>> predicate, string qf, int limit, string sortExpression, string bf = "") where T : class, new()
		{
			return objSolrQuery.QueryListCache(dependencyKey, isCache, fields, predicate, qf, "", limit, sortExpression, bf);
		}

		public static IEnumerable<T> QueryListCache<T>(this SolrQuery<T> objSolrQuery, string dependencyKey, bool isCache, string fields, Expression<Func<T, bool>> predicate, string qf, string highlightFields, int limit, string sortExpression, string bf = "") where T : class, new()
		{
			long num = 0L;
			TimeSpan expiry = new TimeSpan(0, ConfigHelper.GetIntValue("PageCacheMinutes", 60), 0);
			return objSolrQuery.QueryListLimitCache(dependencyKey, isCache, expiry, fields, predicate, qf, highlightFields, 0, limit, sortExpression, out num, bf);
		}

		public static IEnumerable<T> QueryListLimitCache<T>(this SolrQuery<T> objSolrQuery, string dependencyKey, bool isCache, string fields, Expression<Func<T, bool>> predicate, string qf, int offset, int limit, string sortExpression, out long recordCount, string bf = "") where T : class, new()
		{
			TimeSpan expiry = new TimeSpan(0, ConfigHelper.GetIntValue("PageCacheMinutes", 60), 0);
			return objSolrQuery.QueryListLimitCache(dependencyKey, isCache, expiry, fields, predicate, qf, "", offset, limit, sortExpression, out recordCount, bf);
		}

		public static IEnumerable<T> QueryListLimitCache<T>(this SolrQuery<T> objSolrQuery, string dependencyKey, bool isCache, string fields, Expression<Func<T, bool>> predicate, string qf, int offset, int limit, string sortExpression, string bf = "") where T : class, new()
		{
			return objSolrQuery.QueryListLimitCache(dependencyKey, isCache, fields, predicate, qf, "", offset, limit, sortExpression, bf);
		}

		public static IEnumerable<T> QueryListLimitCache<T>(this SolrQuery<T> objSolrQuery, string dependencyKey, bool isCache, string fields, Expression<Func<T, bool>> predicate, string qf, string highlightFields, int offset, int limit, string sortExpression, string bf = "") where T : class, new()
		{
			long num = 0L;
			TimeSpan expiry = new TimeSpan(0, ConfigHelper.GetIntValue("PageCacheMinutes", 60), 0);
			return objSolrQuery.QueryListLimitCache(dependencyKey, isCache, expiry, fields, predicate, qf, highlightFields, offset, limit, sortExpression, out num, bf);
		}

		public static IEnumerable<T> QueryListCache<T>(this SolrQuery<T> objSolrQuery, string dependencyKey, bool isCache, TimeSpan expiry, string fields, Expression<Func<T, bool>> predicate, string qf, int limit, string sortExpression, string bf = "") where T : class, new()
		{
			return objSolrQuery.QueryListCache(dependencyKey, isCache, expiry, fields, predicate, qf, "", limit, sortExpression, bf);
		}

		public static IEnumerable<T> QueryListCache<T>(this SolrQuery<T> objSolrQuery, string dependencyKey, bool isCache, TimeSpan expiry, string fields, Expression<Func<T, bool>> predicate, string qf, string highlightFields, int limit, string sortExpression, string bf = "") where T : class, new()
		{
			long num = 0L;
			return objSolrQuery.QueryListLimitCache(dependencyKey, isCache, expiry, fields, predicate, qf, highlightFields, 0, limit, sortExpression, out num, bf);
		}

		public static IEnumerable<T> QueryListLimitCache<T>(this SolrQuery<T> objSolrQuery, string dependencyKey, bool isCache, TimeSpan expiry, string fields, Expression<Func<T, bool>> predicate, string qf, int offset, int limit, string sortExpression, out long recordCount, string bf = "") where T : class, new()
		{
			return objSolrQuery.QueryListLimitCache(dependencyKey, isCache, expiry, fields, predicate, qf, "", offset, limit, sortExpression, out recordCount, bf);
		}

		public static IEnumerable<T> QueryListLimitCache<T>(this SolrQuery<T> objSolrQuery, string dependencyKey, bool isCache, TimeSpan expiry, string fields, Expression<Func<T, bool>> predicate, string qf, string highlightFields, int offset, int limit, string sortExpression, out long recordCount, string bf = "") where T : class, new()
		{
			dependencyKey = objSolrQuery.GetDependencyKey(dependencyKey);
			string text = objSolrQuery.Condiition(predicate);
			string text2 = string.Concat(new object[]
			{
				objSolrQuery.TableName.ToLower(),
				"SolrList",
				fields,
				text,
				qf,
				highlightFields,
				offset,
				limit,
				sortExpression,
				bf
			});
			text2 = text2.Replace(" ", "").ToLower();
			IEnumerable<T> result;
			if (isCache)
			{
				PageSolrCache<T> dependencyCache = MemcacheCacheHelper.GetDependencyCache<PageSolrCache<T>>(text2, dependencyKey);
				if (dependencyCache != null)
				{
					recordCount = dependencyCache.RecordCount;
					result = dependencyCache.Data;
				}
				else
				{
					IEnumerable<T> enumerable = objSolrQuery.QueryListLimit(fields, text, qf, highlightFields, offset, limit, sortExpression, out recordCount, bf);
					MemcacheCacheHelper.SetDependencyCache(new PageSolrCache<T>
					{
						RecordCount = recordCount,
						Data = enumerable
					}, dependencyKey, text2, expiry);
					result = enumerable;
				}
			}
			else
			{
				SolrCacheBindHelper.DeleteCacheKey(MemcacheCacheHelper.CreateDependencyCacheKey(dependencyKey, text2));
				result = objSolrQuery.QueryListLimit(fields, text, qf, highlightFields, offset, limit, sortExpression, out recordCount, bf);
			}
			return result;
		}

		public static IEnumerable<T> QueryListLimitCache<T>(this SolrQuery<T> objSolrQuery, string dependencyKey, bool isCache, TimeSpan expiry, string fields, Expression<Func<T, bool>> predicate, string qf, int offset, int limit, string sortExpression, string bf = "") where T : class, new()
		{
			return objSolrQuery.QueryListLimitCache(dependencyKey, isCache, expiry, fields, predicate, qf, "", offset, limit, sortExpression, bf);
		}

		public static IEnumerable<T> QueryListLimitCache<T>(this SolrQuery<T> objSolrQuery, string dependencyKey, bool isCache, TimeSpan expiry, string fields, Expression<Func<T, bool>> predicate, string qf, string highlightFields, int offset, int limit, string sortExpression, string bf = "") where T : class, new()
		{
			long num = 0L;
			return objSolrQuery.QueryListLimitCache(dependencyKey, isCache, expiry, fields, predicate, qf, highlightFields, offset, limit, sortExpression, out num, bf);
		}

		public static IEnumerable<T> QueryPageCache<T>(this SolrQuery<T> objSolrQuery, string dependencyKey, bool isCache, string fields, Expression<Func<T, bool>> predicate, string qf, int pageSize, int pageIndex, string sortExpression, out long recordCount, string bf = "") where T : class, new()
		{
			return objSolrQuery.QueryPageCache(dependencyKey, isCache, fields, predicate, qf, "", pageSize, pageIndex, sortExpression, out recordCount, bf);
		}

		public static IEnumerable<T> QueryPageCache<T>(this SolrQuery<T> objSolrQuery, string dependencyKey, bool isCache, string fields, Expression<Func<T, bool>> predicate, string qf, string highlightFields, int pageSize, int pageIndex, string sortExpression, out long recordCount, string bf = "") where T : class, new()
		{
			TimeSpan expiry = new TimeSpan(0, ConfigHelper.GetIntValue("PageCacheMinutes", 60), 0);
			return objSolrQuery.QueryPageCache(dependencyKey, isCache, expiry, fields, predicate, qf, "", pageSize, pageIndex, sortExpression, out recordCount, bf);
		}

		public static IEnumerable<T> QueryPageCache<T>(this SolrQuery<T> objSolrQuery, string dependencyKey, bool isCache, TimeSpan expiry, string fields, Expression<Func<T, bool>> predicate, string qf, int pageSize, int pageIndex, string sortExpression, out long recordCount, string bf = "") where T : class, new()
		{
			return objSolrQuery.QueryPageCache(dependencyKey, isCache, expiry, fields, predicate, qf, "", pageSize, pageIndex, sortExpression, out recordCount, bf);
		}

		public static IEnumerable<T> QueryPageCache<T>(this SolrQuery<T> objSolrQuery, string dependencyKey, bool isCache, TimeSpan expiry, string fields, Expression<Func<T, bool>> predicate, string qf, string highlightFields, int pageSize, int pageIndex, string sortExpression, out long recordCount, string bf = "") where T : class, new()
		{
			dependencyKey = objSolrQuery.GetDependencyKey(dependencyKey);
			string text = objSolrQuery.Condiition(predicate);
			string text2 = string.Concat(new object[]
			{
				objSolrQuery.TableName.ToLower(),
				"SolrPageList",
				fields,
				text,
				qf,
				highlightFields,
				pageSize,
				pageIndex,
				sortExpression,
				bf
			});
			text2 = text2.Replace(" ", "").ToLower();
			IEnumerable<T> result;
			if (isCache)
			{
				PageSolrCache<T> dependencyCache = MemcacheCacheHelper.GetDependencyCache<PageSolrCache<T>>(text2, dependencyKey);
				if (dependencyCache != null)
				{
					recordCount = dependencyCache.RecordCount;
					result = dependencyCache.Data;
				}
				else
				{
					IEnumerable<T> enumerable = objSolrQuery.QueryPage(fields, text, qf, highlightFields, pageSize, pageIndex, sortExpression, out recordCount, bf);
					MemcacheCacheHelper.SetDependencyCache(new PageSolrCache<T>
					{
						RecordCount = recordCount,
						Data = enumerable
					}, dependencyKey, text2, expiry);
					result = enumerable;
				}
			}
			else
			{
				SolrCacheBindHelper.DeleteCacheKey(MemcacheCacheHelper.CreateDependencyCacheKey(dependencyKey, text2));
				result = objSolrQuery.QueryPage(fields, text, qf, highlightFields, pageSize, pageIndex, sortExpression, out recordCount, bf);
			}
			return result;
		}
	}
}
