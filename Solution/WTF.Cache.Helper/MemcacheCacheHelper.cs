namespace WTF.Cache.Helper
{
    using WTF.Framework;
    using System;
    using System.Runtime.CompilerServices;

    public static class MemcacheCacheHelper
    {
        private static MemcachedHelper _MemcachedHelper = null;

        static MemcacheCacheHelper()
        {
            _MemcachedHelper = new MemcachedHelper(ConfigHelper.GetValue("MemcachedInstanceName", "Gao7Cache"));
        }

        public static bool AddCacheMemcached(this object value, string cacheKey)
        {
            return _MemcachedHelper.AddCache(cacheKey, value);
        }

        public static bool AddCacheMemcached(this object value, string cacheKey, DateTime expiry)
        {
            return _MemcachedHelper.AddCache(cacheKey, value, expiry);
        }

        public static bool AddCacheMemcached(this object value, string cacheKey, TimeSpan expiry)
        {
            return _MemcachedHelper.AddCache(cacheKey, value, expiry);
        }

        public static bool AddDependencyCache(this object value, string dependencyKey, string cacheKey, TimeSpan expiry)
        {
            return _MemcachedHelper.AddDependencyCache(dependencyKey, cacheKey, value, expiry);
        }

        public static string CreateDependencyCacheKey(string dependencyKey, string cacheKey)
        {
            return _MemcachedHelper.CreateDependencyCacheKey(dependencyKey, cacheKey);
        }

        public static bool DeleteCacheMemcached(this string cacheKey)
        {
            return _MemcachedHelper.DeleteCache(cacheKey);
        }

        public static object GetCacheMemcached(this string cacheKey)
        {
            return _MemcachedHelper.GetCache(cacheKey);
        }

        public static T GetCacheMemcached<T>(this string cacheKey) where T: class
        {
            return _MemcachedHelper.GetCache<T>(cacheKey);
        }

        [Obsolete("请使用不带ts调用")]
        public static object GetCacheMemcached(this string cacheKey, TimeSpan ts)
        {
            return _MemcachedHelper.GetCache(cacheKey);
        }

        [Obsolete("请使用不带ts调用")]
        public static T GetCacheMemcached<T>(this string cacheKey, TimeSpan ts) where T: class
        {
            return _MemcachedHelper.GetCache<T>(cacheKey);
        }

        public static object GetDependencyCache(this string cacheKey, string dependencyKey)
        {
            return _MemcachedHelper.GetDependencyCache(dependencyKey, cacheKey);
        }

        public static T GetDependencyCache<T>(this string cacheKey, string dependencyKey) where T: class
        {
            return _MemcachedHelper.GetDependencyCache<T>(dependencyKey, cacheKey);
        }

        [Obsolete("请使用不带expiry调用")]
        public static object GetDependencyCache(this string cacheKey, string dependencyKey, TimeSpan expiry)
        {
            return _MemcachedHelper.GetDependencyCache(dependencyKey, cacheKey);
        }

        [Obsolete("请使用不带expiry调用")]
        public static T GetDependencyCache<T>(this string cacheKey, string dependencyKey, TimeSpan expiry) where T: class
        {
            return _MemcachedHelper.GetDependencyCache<T>(dependencyKey, cacheKey);
        }

        public static int GetIncrementMemcached(this string cacheKey)
        {
            return _MemcachedHelper.GetIncrement(cacheKey);
        }

        public static bool IncrementMemcached(this string cacheKey, int maxValue, int stepValue)
        {
            return _MemcachedHelper.Increment(cacheKey, maxValue, stepValue);
        }

        public static object ReadMemcachedNoExpireCache(this string cacheKey)
        {
            return _MemcachedHelper.ReadNoExpireCache(cacheKey);
        }

        public static bool SetCacheMemcached(this object value, string cacheKey)
        {
            return _MemcachedHelper.SetCache(cacheKey, value);
        }

        public static bool SetCacheMemcached(this object value, string cacheKey, DateTime expiry)
        {
            return _MemcachedHelper.SetCache(cacheKey, value, expiry);
        }

        public static bool SetCacheMemcached(this object value, string cacheKey, TimeSpan expiry)
        {
            return _MemcachedHelper.SetCache(cacheKey, value, expiry);
        }

        public static bool SetDependencyCache(this object value, string dependencyKey, string cacheKey, TimeSpan expiry)
        {
            return _MemcachedHelper.SetDependencyCache(dependencyKey, cacheKey, value, expiry);
        }
    }
}

