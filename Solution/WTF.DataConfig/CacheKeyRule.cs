using WTF.DataConfig.Entity;
using WTF.Framework;
using WTF.Logging;
using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Xml;

namespace WTF.DataConfig
{
	public class CacheKeyRule
	{
		private CacheEntities objCurrentEntities;

		public CacheEntities CurrentEntities
		{
			get
			{
				if (this.objCurrentEntities == null)
				{
					this.objCurrentEntities = new CacheEntities(EntitiesHelper.GetConnectionString<CacheEntities>());
				}
				return this.objCurrentEntities;
			}
		}

		public ObjectQuery<cache_cachekey> cache_cachekey
		{
			get
			{
				return this.CurrentEntities.cache_cachekey;
			}
		}

		public ObjectQuery<cache_cachesite> cache_cachesite
		{
			get
			{
				return this.CurrentEntities.cache_cachesite;
			}
		}

		public void Insertcachekey(cache_cachekey objcache_cachekey)
		{
			objcache_cachekey.CacheName.CheckIsNull("请输入缓存名称", "ParameterLog");
			if (objcache_cachekey.CacheKey.IsNoNull() && this.cache_cachekey.Any((cache_cachekey s) => s.CacheKey == objcache_cachekey.CacheKey))
			{
				SysAssert.ArgumentAssert<LogModuleType>("输入依懒Key已经存在", LogModuleType.LogManager);
			}
			cache_cachesite cache_cachesite = this.cache_cachesite.FirstOrDefault((cache_cachesite s) => s.CacheSiteID == objcache_cachekey.CacheSiteID);
			objcache_cachekey.IDPath = cache_cachesite.IDPath;
			this.CurrentEntities.AddTocache_cachekey(objcache_cachekey);
			this.CurrentEntities.SaveChanges();
			if (objcache_cachekey.CacheKey.IsNull())
			{
				objcache_cachekey.CacheKey = cache_cachesite.CachePrefix + "_" + objcache_cachekey.CacheKeyID;
				this.CurrentEntities.SaveChanges();
			}
		}

		public void Updatecachekey(cache_cachekey objcache_cachekey)
		{
			objcache_cachekey.CacheName.CheckIsNull("请输入缓存名称", "ParameterLog");
			if (objcache_cachekey.CacheKey.IsNoNull())
			{
				if (this.cache_cachekey.Any((cache_cachekey s) => s.CacheKeyID != objcache_cachekey.CacheKeyID && s.CacheKey == objcache_cachekey.CacheKey))
				{
					SysAssert.ArgumentAssert<LogModuleType>("输入缓存Key已经存在", LogModuleType.ParameterLog);
				}
			}
			else if (objcache_cachekey.CacheKey.IsNull())
			{
				cache_cachesite cache_cachesite = this.cache_cachesite.FirstOrDefault((cache_cachesite s) => s.CacheSiteID == objcache_cachekey.CacheSiteID);
				objcache_cachekey.CacheKey = cache_cachesite.CachePrefix + "_" + objcache_cachekey.CacheKeyID;
				this.CurrentEntities.SaveChanges();
			}
			this.CurrentEntities.SaveChanges();
		}

		public void DeletecachekeyByKey(string primaryKey)
		{
			this.CurrentEntities.cache_cachekey.DeleteDataPrimaryKey(primaryKey);
		}

		public int Deletecachesite(int cachesiteID)
		{
			cache_cachesite cache_cachesite = this.cache_cachesite.FirstOrDefault((cache_cachesite s) => s.CacheSiteID == cachesiteID);
			string iDPath = cache_cachesite.IDPath;
			this.cache_cachesite.DeleteDataSql("IDPath like '" + iDPath + "%'", new object[0]);
			return cache_cachesite.ParentID;
		}

		public void Insertcachesite(cache_cachesite objcache_cachesite)
		{
			objcache_cachesite.SiteName.CheckIsNull("请输入站点名称", LogModuleType.ParameterLog);
			objcache_cachesite.CachePrefix.CheckIsNull("请输入缓存前缀", LogModuleType.ParameterLog);
			objcache_cachesite.SortIndex = 0;
			objcache_cachesite.IDPath = "";
			objcache_cachesite.CreateDate = DateTime.Now;
			this.CurrentEntities.AddTocache_cachesite(objcache_cachesite);
			this.CurrentEntities.SaveChanges();
			cache_cachesite cache_cachesite = this.cache_cachesite.FirstOrDefault((cache_cachesite s) => s.CacheSiteID == objcache_cachesite.ParentID);
			objcache_cachesite.SortIndex = objcache_cachesite.CacheSiteID;
			objcache_cachesite.IDPath = cache_cachesite.IDPath + objcache_cachesite.CacheSiteID + ",";
			this.CurrentEntities.SaveChanges();
		}

		public void Updatecachesite(cache_cachesite objcache_cachesite)
		{
			objcache_cachesite.SiteName.CheckIsNull("请输入站点名称", LogModuleType.ParameterLog);
			objcache_cachesite.CachePrefix.CheckIsNull("请输入缓存前缀", LogModuleType.ParameterLog);
			this.CurrentEntities.SaveChanges();
		}

		private void CreateChildSiteXmlElement(XmlDocument xmlDocSource, int CacheSiteID, XmlElement objXmlElement, List<cache_cachesite> objcache_cachesiteList, string url)
		{
			foreach (cache_cachesite current in from s in objcache_cachesiteList
			where s.ParentID == CacheSiteID
			orderby s.SortIndex
			select s)
			{
				XmlElement xmlElement = xmlDocSource.CreateElement("TreeNodeMember");
				xmlElement.SetAttribute("TreeNodeID", current.CacheSiteID.ToString());
				xmlElement.SetAttribute("TreeNodeName", current.SiteName);
				xmlElement.SetAttribute("NavigateUrl", string.Format(url + "?CacheSiteID={0}", current.CacheSiteID.ToString()).EncryptModuleQuery());
				objXmlElement.AppendChild(xmlElement);
				this.CreateChildSiteXmlElement(xmlDocSource, current.CacheSiteID, xmlElement, objcache_cachesiteList, url);
			}
		}

		public string GetSiteXmlText(string url)
		{
			List<cache_cachesite> list = this.CurrentEntities.cache_cachesite.ToList<cache_cachesite>();
			cache_cachesite cache_cachesite = list.FirstOrDefault((cache_cachesite s) => s.ParentID == 0);
			XmlDocument xmlDocument = new XmlDocument();
			XmlElement xmlElement = xmlDocument.CreateElement("TreeNodeMember");
			xmlElement.SetAttribute("TreeNodeID", cache_cachesite.CacheSiteID.ToString());
			xmlElement.SetAttribute("TreeNodeName", cache_cachesite.SiteName);
			xmlElement.SetAttribute("NavigateUrl", string.Format(url + "?CacheSiteID={0}", cache_cachesite.CacheSiteID.ToString()).EncryptModuleQuery());
			xmlDocument.AppendChild(xmlElement);
			this.CreateChildSiteXmlElement(xmlDocument, cache_cachesite.CacheSiteID, xmlElement, list, url);
			return xmlDocument.InnerXml;
		}
	}
}
