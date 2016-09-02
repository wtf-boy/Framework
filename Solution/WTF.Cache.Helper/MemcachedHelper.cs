namespace WTF.Cache.Helper
{
    using BeIT.MemCached;
    using WTF.Framework;
    using System;
    using System.Linq;
    using System.Text;

    public class MemcachedHelper
    {
        private MemcachedClient _MemcachedClient;
        public string _MemcachedConnectionHost;
        private static readonly object _SyncRoot = new object();

        public MemcachedHelper() : this("Gao7Cache")
        {
        }

        public MemcachedHelper(string instanceName) : this(instanceName, "MemcachedHost")
        {
        }

        public MemcachedHelper(string instanceName, string memcachedHost)
        {
            this._MemcachedClient = null;
            this._MemcachedConnectionHost = "";
            if (string.IsNullOrWhiteSpace(memcachedHost))
            {
                throw new ArgumentNullException(memcachedHost, "连接地址配置不能为空");
            }
            if (string.IsNullOrWhiteSpace(instanceName))
            {
                throw new ArgumentNullException(instanceName, "实例名不能为空");
            }
            this._MemcachedClient = null;
            if (!MemcachedClient.Exists(instanceName))
            {
                lock (_SyncRoot)
                {
                    if (MemcachedClient.Exists(instanceName))
                    {
                        this._MemcachedClient = MemcachedClient.GetInstance(instanceName);
                    }
                    else
                    {
                        string str = "";
                        if (memcachedHost.Split(new char[] { '.' }).Count<string>() >= 4)
                        {
                            str = memcachedHost;
                        }
                        else
                        {
                            str = ConfigHelper.GetValue(memcachedHost);
                            if (string.IsNullOrWhiteSpace(str))
                            {
                                throw new ArgumentNullException(memcachedHost, "配置文件找不到此" + memcachedHost + "节点或此节点值不能为空");
                            }
                        }
                        this._MemcachedConnectionHost = str;
                        MemcachedClient.Setup(instanceName, str.Split(new char[] { ',' }));
                        this._MemcachedClient = MemcachedClient.GetInstance(instanceName);
                        this._MemcachedClient.SendReceiveTimeout = 0x1388;
                        this._MemcachedClient.MinPoolSize = 10;
                        this._MemcachedClient.MaxPoolSize = 0xf4240;
                    }
                }
            }
            else
            {
                this._MemcachedClient = MemcachedClient.GetInstance(instanceName);
            }
        }

        public bool AddCache(string cacheKey, object value)
        {
            CheckCacheKey(cacheKey);
            this._MemcachedClient.Add(cacheKey + "_cachetime", DateTime.Now.AddYears(10));
            return this._MemcachedClient.Add(cacheKey, value);
        }

        public bool AddCache(string cacheKey, object value, DateTime expiry)
        {
            CheckCacheKey(cacheKey);
            this._MemcachedClient.Add(cacheKey + "_cachetime", expiry);
            return this._MemcachedClient.Add(cacheKey, value, expiry);
        }

        public bool AddCache(string cacheKey, object value, TimeSpan expiry)
        {
            CheckCacheKey(cacheKey);
            this._MemcachedClient.Add(cacheKey + "_cachetime", DateTime.Now.Add(expiry));
            return this._MemcachedClient.Add(cacheKey, value, expiry);
        }

        public bool AddDependencyCache(string dependencyKey, string cacheKey, object value, TimeSpan expiry)
        {
            CheckDependencyKey(dependencyKey);
            CheckCacheKey(cacheKey);
            TimeSpan span = new TimeSpan(0, ConfigHelper.GetIntValue("DependencyCacheMinutes", 60), 0);
            if (this.GetCache(dependencyKey) == null)
            {
                this.AddCache(dependencyKey, DateTime.Now, span);
            }
            string str = this.CreateDependencyCacheKey(dependencyKey, cacheKey);
            this._MemcachedClient.Add(str + "_savetime", DateTime.Now);
            return this.AddCache(str, value, expiry);
        }

        private static void CheckCacheKey(string cacheKey)
        {
            if (string.IsNullOrWhiteSpace(cacheKey))
            {
                throw new ArgumentNullException("cacheKey", "参数cacheKey不能为空");
            }
        }

        private static void CheckDependencyKey(string dependencyKey)
        {
            if (string.IsNullOrWhiteSpace(dependencyKey))
            {
                throw new ArgumentNullException("dependencyKey", "参数dependencyKey不能为空");
            }
        }

        public string CreateDependencyCacheKey(string dependencyKey, string cacheKey)
        {
            return cacheKey.MD5Encrypt(dependencyKey, Encoding.UTF8);
        }

        public bool DeleteCache(string cacheKey)
        {
            if (string.IsNullOrWhiteSpace(cacheKey))
            {
                throw new ArgumentNullException(cacheKey, "参数" + cacheKey + "不能为空");
            }
            this._MemcachedClient.Delete(cacheKey + "_cachetime");
            this._MemcachedClient.Delete(cacheKey + "_savetime");
            return this._MemcachedClient.Delete(cacheKey);
        }

        public object GetCache(string cacheKey)
        {
            CheckCacheKey(cacheKey);
            string key = cacheKey + "_cachetime";
            object obj2 = this._MemcachedClient.Get(key);
            if (obj2 != null)
            {
                if (Convert.ToDateTime(obj2) <= DateTime.Now)
                {
                    this.DeleteCache(cacheKey);
                    return null;
                }
            }
            else
            {
                this.DeleteCache(cacheKey);
                return null;
            }
            return this._MemcachedClient.Get(cacheKey);
        }

        public T GetCache<T>(string cacheKey) where T: class
        {
            object cache = this.GetCache(cacheKey);
            if (cache == null)
            {
                return default(T);
            }
            return (cache as T);
        }

        [Obsolete("请使用不带ts调用")]
        public object GetCache(string cacheKey, TimeSpan ts)
        {
            return this.GetCache(cacheKey);
        }

        [Obsolete("请使用不带ts调用")]
        public T GetCache<T>(string cacheKey, TimeSpan ts) where T: class
        {
            return this.GetCache<T>(cacheKey);
        }

        public object GetDependencyCache(string dependencyKey, string cacheKey)
        {
            CheckDependencyKey(dependencyKey);
            CheckCacheKey(cacheKey);
            object cache = this.GetCache(dependencyKey);
            string str = this.CreateDependencyCacheKey(dependencyKey, cacheKey);
            if (cache == null)
            {
                this.DeleteCache(str);
                return null;
            }
            DateTime time = Convert.ToDateTime(cache);
            string key = str + "_savetime";
            object obj3 = this._MemcachedClient.Get(key);
            if (obj3 != null)
            {
                if (Convert.ToDateTime(obj3) < time)
                {
                    this.DeleteCache(str);
                    return null;
                }
            }
            else
            {
                this.DeleteCache(str);
                return null;
            }
            return this.GetCache(str);
        }

        public T GetDependencyCache<T>(string dependencyKey, string cacheKey) where T: class
        {
            object dependencyCache = this.GetDependencyCache(dependencyKey, cacheKey);
            if (dependencyCache == null)
            {
                return default(T);
            }
            return (dependencyCache as T);
        }

        [Obsolete("请使用不带expiry调用")]
        public object GetDependencyCache(string dependencyKey, string cacheKey, TimeSpan expiry)
        {
            return this.GetDependencyCache(dependencyKey, cacheKey);
        }

        [Obsolete("请使用不带expiry调用")]
        public T GetDependencyCache<T>(string dependencyKey, string cacheKey, TimeSpan expiry) where T: class
        {
            return this.GetDependencyCache<T>(dependencyKey, cacheKey);
        }

        public int GetIncrement(string cacheKey)
        {
            int num = 0;
            CheckCacheKey(cacheKey);
            try
            {
                object obj2 = this._MemcachedClient.Get(cacheKey);
                if (obj2 != null)
                {
                    num = Convert.ToInt32(obj2);
                }
            }
            catch (Exception)
            {
            }
            return num;
        }

        public bool Increment(string cacheKey, int maxValue, int stepValue)
        {
            CheckCacheKey(cacheKey);
            bool flag = false;
            TimeSpan expiry = new TimeSpan(7, 0, 0);
            int num = 0;
            try
            {
                object obj2 = this._MemcachedClient.Get(cacheKey);
                if (obj2 != null)
                {
                    num = Convert.ToInt32(obj2);
                }
                this._MemcachedClient.Set(cacheKey, num + stepValue, expiry);
            }
            catch
            {
            }
            if (num >= maxValue)
            {
                flag = this._MemcachedClient.Set(cacheKey, stepValue, expiry);
            }
            return flag;
        }

        public object ReadNoExpireCache(string cacheKey)
        {
            CheckCacheKey(cacheKey);
            return this._MemcachedClient.Get(cacheKey);
        }

        public bool SetCache(string cacheKey, object value)
        {
            CheckCacheKey(cacheKey);
            this._MemcachedClient.Add(cacheKey + "_cachetime", DateTime.Now.AddYears(10));
            return this._MemcachedClient.Set(cacheKey, value);
        }

        public bool SetCache(string cacheKey, object value, DateTime expiry)
        {
            CheckCacheKey(cacheKey);
            this._MemcachedClient.Set(cacheKey + "_cachetime", expiry);
            return this._MemcachedClient.Set(cacheKey, value, expiry);
        }

        public bool SetCache(string cacheKey, object value, TimeSpan expiry)
        {
            CheckCacheKey(cacheKey);
            this._MemcachedClient.Set(cacheKey + "_cachetime", DateTime.Now.Add(expiry));
            return this._MemcachedClient.Set(cacheKey, value, expiry);
        }

        public bool SetDependencyCache(string dependencyKey, string cacheKey, object value, TimeSpan expiry)
        {
            CheckDependencyKey(dependencyKey);
            CheckCacheKey(cacheKey);
            TimeSpan span = new TimeSpan(0, ConfigHelper.GetIntValue("DependencyCacheMinutes", 60), 0);
            if (this.GetCache(dependencyKey) == null)
            {
                this.AddCache(dependencyKey, DateTime.Now, span);
            }
            string str = this.CreateDependencyCacheKey(dependencyKey, cacheKey);
            this._MemcachedClient.Set(str + "_savetime", DateTime.Now);
            return this.SetCache(str, value, expiry);
        }
    }
}

