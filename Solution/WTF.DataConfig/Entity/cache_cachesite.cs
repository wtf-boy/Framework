using System;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace WTF.DataConfig.Entity
{
	[EdmEntityType(NamespaceName = "CacheModel", Name = "cache_cachesite"), DataContract(IsReference = true)]
	[Serializable]
	public class cache_cachesite : EntityObject
	{
		private int _CacheSiteID;

		private string _SiteName;

		private string _Remark;

		private int _ParentID;

		private string _IDPath;

		private int _SortIndex;

		private DateTime _CreateDate;

		private string _CachePrefix;

		[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false), DataMember]
		public int CacheSiteID
		{
			get
			{
				return this._CacheSiteID;
			}
			set
			{
				if (this._CacheSiteID != value)
				{
					this.ReportPropertyChanging("CacheSiteID");
					this._CacheSiteID = StructuralObject.SetValidValue(value);
					this.ReportPropertyChanged("CacheSiteID");
				}
			}
		}

		[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false), DataMember]
		public string SiteName
		{
			get
			{
				return this._SiteName;
			}
			set
			{
				this.ReportPropertyChanging("SiteName");
				this._SiteName = StructuralObject.SetValidValue(value, false);
				this.ReportPropertyChanged("SiteName");
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

		[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false), DataMember]
		public int ParentID
		{
			get
			{
				return this._ParentID;
			}
			set
			{
				this.ReportPropertyChanging("ParentID");
				this._ParentID = StructuralObject.SetValidValue(value);
				this.ReportPropertyChanged("ParentID");
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
		public int SortIndex
		{
			get
			{
				return this._SortIndex;
			}
			set
			{
				this.ReportPropertyChanging("SortIndex");
				this._SortIndex = StructuralObject.SetValidValue(value);
				this.ReportPropertyChanged("SortIndex");
			}
		}

		[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false), DataMember]
		public DateTime CreateDate
		{
			get
			{
				return this._CreateDate;
			}
			set
			{
				this.ReportPropertyChanging("CreateDate");
				this._CreateDate = StructuralObject.SetValidValue(value);
				this.ReportPropertyChanged("CreateDate");
			}
		}

		[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false), DataMember]
		public string CachePrefix
		{
			get
			{
				return this._CachePrefix;
			}
			set
			{
				this.ReportPropertyChanging("CachePrefix");
				this._CachePrefix = StructuralObject.SetValidValue(value, false);
				this.ReportPropertyChanged("CachePrefix");
			}
		}

		[EdmRelationshipNavigationProperty("CacheModel", "cache_cachekey_ibfk_1", "cache_cachekey"), DataMember, SoapIgnore, XmlIgnore]
		public EntityCollection<cache_cachekey> cache_cachekey
		{
			get
			{
				return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<cache_cachekey>("CacheModel.cache_cachekey_ibfk_1", "cache_cachekey");
			}
			set
			{
				if (value != null)
				{
					((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<cache_cachekey>("CacheModel.cache_cachekey_ibfk_1", "cache_cachekey", value);
				}
			}
		}

		public static cache_cachesite Createcache_cachesite(int cacheSiteID, string siteName, string remark, int parentID, string iDPath, int sortIndex, DateTime createDate, string cachePrefix)
		{
			return new cache_cachesite
			{
				CacheSiteID = cacheSiteID,
				SiteName = siteName,
				Remark = remark,
				ParentID = parentID,
				IDPath = iDPath,
				SortIndex = sortIndex,
				CreateDate = createDate,
				CachePrefix = cachePrefix
			};
		}
	}
}
