using System;
using System.ComponentModel;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace WTF.DataConfig.Entity
{
	[EdmEntityType(NamespaceName = "CacheModel", Name = "cache_cachekey"), DataContract(IsReference = true)]
	[Serializable]
	public class cache_cachekey : EntityObject
	{
		private int _CacheKeyID;

		private int _CacheSiteID;

		private string _IDPath;

		private string _CacheKey;

		private string _CacheName;

		private string _Remark;

		[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false), DataMember]
		public int CacheKeyID
		{
			get
			{
				return this._CacheKeyID;
			}
			set
			{
				if (this._CacheKeyID != value)
				{
					this.ReportPropertyChanging("CacheKeyID");
					this._CacheKeyID = StructuralObject.SetValidValue(value);
					this.ReportPropertyChanged("CacheKeyID");
				}
			}
		}

		[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false), DataMember]
		public int CacheSiteID
		{
			get
			{
				return this._CacheSiteID;
			}
			set
			{
				this.ReportPropertyChanging("CacheSiteID");
				this._CacheSiteID = StructuralObject.SetValidValue(value);
				this.ReportPropertyChanged("CacheSiteID");
			}
		}

		[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false), DataMember]
		public string IDPath
		{
			get
			{
				return this._IDPath;
			}
			set
			{
				this.ReportPropertyChanging("IDPath");
				this._IDPath = StructuralObject.SetValidValue(value, false);
				this.ReportPropertyChanged("IDPath");
			}
		}

		[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false), DataMember]
		public string CacheKey
		{
			get
			{
				return this._CacheKey;
			}
			set
			{
				this.ReportPropertyChanging("CacheKey");
				this._CacheKey = StructuralObject.SetValidValue(value, false);
				this.ReportPropertyChanged("CacheKey");
			}
		}

		[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false), DataMember]
		public string CacheName
		{
			get
			{
				return this._CacheName;
			}
			set
			{
				this.ReportPropertyChanging("CacheName");
				this._CacheName = StructuralObject.SetValidValue(value, false);
				this.ReportPropertyChanged("CacheName");
			}
		}

		[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false), DataMember]
		public string Remark
		{
			get
			{
				return this._Remark;
			}
			set
			{
				this.ReportPropertyChanging("Remark");
				this._Remark = StructuralObject.SetValidValue(value, false);
				this.ReportPropertyChanged("Remark");
			}
		}

		[EdmRelationshipNavigationProperty("CacheModel", "cache_cachekey_ibfk_1", "cache_cachesite"), DataMember, SoapIgnore, XmlIgnore]
		public cache_cachesite cache_cachesite
		{
			get
			{
				return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<cache_cachesite>("CacheModel.cache_cachekey_ibfk_1", "cache_cachesite").Value;
			}
			set
			{
				((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<cache_cachesite>("CacheModel.cache_cachekey_ibfk_1", "cache_cachesite").Value = value;
			}
		}

		[Browsable(false), DataMember]
		public EntityReference<cache_cachesite> cache_cachesiteReference
		{
			get
			{
				return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<cache_cachesite>("CacheModel.cache_cachekey_ibfk_1", "cache_cachesite");
			}
			set
			{
				if (value != null)
				{
					((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<cache_cachesite>("CacheModel.cache_cachekey_ibfk_1", "cache_cachesite", value);
				}
			}
		}

		public static cache_cachekey Createcache_cachekey(int cacheKeyID, int cacheSiteID, string iDPath, string cacheKey, string cacheName, string remark)
		{
			return new cache_cachekey
			{
				CacheKeyID = cacheKeyID,
				CacheSiteID = cacheSiteID,
				IDPath = iDPath,
				CacheKey = cacheKey,
				CacheName = cacheName,
				Remark = remark
			};
		}
	}
}
