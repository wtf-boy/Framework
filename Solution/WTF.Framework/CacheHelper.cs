namespace WTF.Framework
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Text.RegularExpressions;
    using System.Web;
    using System.Web.Caching;

    public static class CacheHelper
    {
        private static readonly Cache _cache = HttpRuntime.Cache;
        private static object CacheLock = new object();

        public static void AddMaxCache(this object obj, string cacheType, string childKey)
        {
            AddMaxCache(obj, cacheType, childKey, null);
        }

        public static void AddMaxCache(object obj, string cacheType, string childKey, CacheDependency dep)
        {
            if (cacheType.IsNull())
            {
                throw new ArgumentNullException("cacheType", "请输入缓存类型");
            }
            if (obj != null)
            {
                lock (CacheLock)
                {
                    string key = GetKey(cacheType, childKey);
                    _cache.Remove(key);
                    _cache.Insert(key, obj, dep, DateTime.MaxValue, TimeSpan.Zero, CacheItemPriority.AboveNormal, null);
                }
            }
        }

        public static void AddToCache(this object obj, string cacheType, string childKey, int minute)
        {
            obj.AddToCache(cacheType, childKey, null, CacheFactor.Minute, minute);
        }

        public static void AddToCache(this object obj, string cacheType, string childKey, CacheFactor objCacheFactor, int numeral)
        {
            obj.AddToCache(cacheType, childKey, null, objCacheFactor, numeral);
        }

        public static void AddToCache(this object obj, string cacheType, string childKey, CacheFactor objCacheFactor, int numeral, CacheItemPriority priority)
        {
            obj.AddToCache(cacheType, childKey, null, objCacheFactor, numeral, priority, null);
        }

        public static void AddToCache(this object obj, string cacheType, string childKey, CacheFactor objCacheFactor, int numeral, CacheItemRemovedCallback cacheItemRemovedCallback)
        {
            obj.AddToCache(cacheType, childKey, null, objCacheFactor, numeral, CacheItemPriority.Normal, cacheItemRemovedCallback);
        }

        public static void AddToCache(this object obj, string cacheType, string childKey, CacheDependency dep, CacheFactor objCacheFactor, int numeral)
        {
            obj.AddToCache(cacheType, childKey, dep, objCacheFactor, numeral, CacheItemPriority.Normal, null);
        }

        public static void AddToCache(this object obj, string cacheType, string childKey, string fileName, CacheFactor objCacheFactor, int numeral, CacheItemRemovedCallback cacheItemRemovedCallback)
        {
            CacheDependency cacheDependency = new CacheDependency(fileName);
            obj.AddToCache(cacheType, childKey, cacheDependency, objCacheFactor, numeral, CacheItemPriority.Normal, cacheItemRemovedCallback);
        }

        public static void AddToCache(this object obj, string cacheType, string childKey, CacheDependency cacheDependency, CacheFactor objCacheFactor, int numeral, CacheItemPriority priority, CacheItemRemovedCallback cacheItemRemovedCallback)
        {
            if (cacheType.IsNull())
            {
                throw new ArgumentNullException("cacheType", "请输入缓存类型");
            }
            if (obj != null)
            {
                DateTime now = DateTime.Now;
                switch (objCacheFactor)
                {
                    case CacheFactor.Minute:
                        now = DateTime.Now.AddMinutes((double) numeral);
                        break;

                    case CacheFactor.Hour:
                        now = DateTime.Now.AddHours((double) numeral);
                        break;

                    case CacheFactor.Day:
                        now = DateTime.Now.AddDays((double) numeral);
                        break;

                    case CacheFactor.Max:
                        now = DateTime.MaxValue;
                        break;
                }
                lock (CacheLock)
                {
                    string key = GetKey(cacheType, childKey);
                    _cache.Remove(key);
                    _cache.Insert(key, obj, cacheDependency, now, TimeSpan.Zero, priority, cacheItemRemovedCallback);
                }
            }
        }

        public static void AddToFileCache(this object obj, string cacheType, string childKey, string fileName)
        {
            obj.AddToFileCache(cacheType, childKey, fileName, null);
        }

        public static void AddToFileCache(this object obj, string cacheType, string childKey, string fileName, CacheItemRemovedCallback cacheItemRemovedCallback)
        {
            obj.AddToCache(cacheType, childKey, fileName, CacheFactor.Max, 0, cacheItemRemovedCallback);
        }

        public static void Clear()
        {
            lock (CacheLock)
            {
                IDictionaryEnumerator enumerator = _cache.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    _cache.Remove(enumerator.Key.ToString());
                }
            }
        }

        public static List<CacheInfo> GetCache(string cacheType)
        {
            List<CacheInfo> list = new List<CacheInfo>();
            IDictionaryEnumerator enumerator = null;
            lock (CacheLock)
            {
                enumerator = _cache.GetEnumerator();
            }
            while (enumerator.MoveNext())
            {
                string input = enumerator.Key.ToString();
                if (input.IsMatch("^[^=]+=[^=]+$"))
                {
                    if (cacheType.IsNoNull())
                    {
                        if (input.IsMatch("^" + cacheType.ToString() + "=[^=]+$"))
                        {
                            list.Add(new CacheInfo(input.Replace("^(?<x>[^=]+)=[^=]+$", "$1", RegexOptions.IgnoreCase), input.Replace("^[^=]+=(?<x>[^=]+)$", "$1", RegexOptions.IgnoreCase), input));
                        }
                    }
                    else
                    {
                        list.Add(new CacheInfo(input.Replace("^(?<x>[^=]+)=[^=]+$", "$1", RegexOptions.IgnoreCase), input.Replace("^[^=]+=(?<x>[^=]+)$", "$1", RegexOptions.IgnoreCase), input));
                    }
                }
                else if (cacheType.IsNull())
                {
                    list.Add(new CacheInfo(cacheType.ToString(), input, cacheType.ToString() + "=" + input));
                }
            }
            return list;
        }

        public static object GetFromCache(string cacheType, string childKey)
        {
            if (cacheType.IsNull())
            {
                throw new ArgumentNullException("CacheType", "请输入缓存类型");
            }
            return _cache[GetKey(cacheType, childKey)];
        }

        public static T GetFromCache<T>(string cacheType, string childKey)
        {
            if (cacheType.IsNull())
            {
                throw new ArgumentNullException("CacheType", "请输入缓存类型");
            }
            object obj2 = _cache[GetKey(cacheType, childKey)];
            if (obj2 == null)
            {
                return default(T);
            }
            return (T) obj2;
        }

        public static string GetKey(string cacheType, string childKey)
        {
            if (cacheType.IsNull())
            {
                throw new ArgumentNullException("cacheType", "请输入缓存类型");
            }
            return (cacheType + "=" + childKey);
        }

        public static void Remove(string cacheType, string childkey)
        {
            lock (CacheLock)
            {
                if (childkey.IsNoNull())
                {
                    if (cacheType.IsNoNull())
                    {
                        _cache.Remove(GetKey(cacheType, childkey));
                    }
                    else
                    {
                        _cache.Remove(childkey);
                    }
                }
                else if (cacheType.IsNull())
                {
                    Clear();
                }
                else
                {
                    RemoveByPattern("^" + cacheType.ToString() + "=[^=]+$");
                }
            }
        }

        public static void RemoveByPattern(string pattern)
        {
            IDictionaryEnumerator enumerator = _cache.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (enumerator.Key.ToString().IsMatch(pattern))
                {
                    _cache.Remove(enumerator.Key.ToString());
                }
            }
        }
    }
}

