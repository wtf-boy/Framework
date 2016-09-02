using System;
using System.Data.EntityClient;
using System.Data.Objects;

namespace WTF.DataConfig.Entity
{
	public class CacheEntities : ObjectContext
	{
		private ObjectSet<Sys_CahceType> _sys_cahcetype;

		private ObjectSet<cache_cachekey> _cache_cachekey;

		private ObjectSet<cache_cachesite> _cache_cachesite;

		public ObjectSet<Sys_CahceType> sys_cahcetype
		{
			get
			{
				if (this._sys_cahcetype == null)
				{
					this._sys_cahcetype = base.CreateObjectSet<Sys_CahceType>("sys_cahcetype");
				}
				return this._sys_cahcetype;
			}
		}

		public ObjectSet<cache_cachekey> cache_cachekey
		{
			get
			{
				if (this._cache_cachekey == null)
				{
					this._cache_cachekey = base.CreateObjectSet<cache_cachekey>("cache_cachekey");
				}
				return this._cache_cachekey;
			}
		}

		public ObjectSet<cache_cachesite> cache_cachesite
		{
			get
			{
				if (this._cache_cachesite == null)
				{
					this._cache_cachesite = base.CreateObjectSet<cache_cachesite>("cache_cachesite");
				}
				return this._cache_cachesite;
			}
		}

		public CacheEntities() : base("name=CacheEntities", "CacheEntities")
		{
			base.ContextOptions.LazyLoadingEnabled = true;
		}

		public CacheEntities(string connectionString) : base(connectionString, "CacheEntities")
		{
			base.ContextOptions.LazyLoadingEnabled = true;
		}

		public CacheEntities(EntityConnection connection) : base(connection, "CacheEntities")
		{
			base.ContextOptions.LazyLoadingEnabled = true;
		}

		public void AddTosys_cahcetype(Sys_CahceType sys_CahceType)
		{
			base.AddObject("sys_cahcetype", sys_CahceType);
		}

		public void AddTocache_cachekey(cache_cachekey cache_cachekey)
		{
			base.AddObject("cache_cachekey", cache_cachekey);
		}

		public void AddTocache_cachesite(cache_cachesite cache_cachesite)
		{
			base.AddObject("cache_cachesite", cache_cachesite);
		}
	}
}
