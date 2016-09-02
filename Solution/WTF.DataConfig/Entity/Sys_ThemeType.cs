using System;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace WTF.DataConfig.Entity
{
	[EdmEntityType(NamespaceName = "ThemeModel", Name = "Sys_ThemeType"), DataContract(IsReference = true)]
	[Serializable]
	public class Sys_ThemeType : EntityObject
	{
		private string _ThemeTypeID;

		private string _ThemeTypeName;

		private string _ThemeTypeCode;

		private string _Remark;

		[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false), DataMember]
		public string ThemeTypeID
		{
			get
			{
				return this._ThemeTypeID;
			}
			set
			{
				if (this._ThemeTypeID != value)
				{
					this.ReportPropertyChanging("ThemeTypeID");
					this._ThemeTypeID = StructuralObject.SetValidValue(value, false);
					this.ReportPropertyChanged("ThemeTypeID");
				}
			}
		}

		[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false), DataMember]
		public string ThemeTypeName
		{
			get
			{
				return this._ThemeTypeName;
			}
			set
			{
				this.ReportPropertyChanging("ThemeTypeName");
				this._ThemeTypeName = StructuralObject.SetValidValue(value, false);
				this.ReportPropertyChanged("ThemeTypeName");
			}
		}

		[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false), DataMember]
		public string ThemeTypeCode
		{
			get
			{
				return this._ThemeTypeCode;
			}
			set
			{
				this.ReportPropertyChanging("ThemeTypeCode");
				this._ThemeTypeCode = StructuralObject.SetValidValue(value, false);
				this.ReportPropertyChanged("ThemeTypeCode");
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

		[EdmRelationshipNavigationProperty("ThemeModel", "FK_sys_theme", "sys_theme"), DataMember, SoapIgnore, XmlIgnore]
		public EntityCollection<Sys_Theme> Sys_Theme
		{
			get
			{
				return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<Sys_Theme>("ThemeModel.FK_sys_theme", "sys_theme");
			}
			set
			{
				if (value != null)
				{
					((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<Sys_Theme>("ThemeModel.FK_sys_theme", "sys_theme", value);
				}
			}
		}

		[EdmRelationshipNavigationProperty("ThemeModel", "FK_sys_themetypeconfig", "sys_themetypeconfig"), DataMember, SoapIgnore, XmlIgnore]
		public EntityCollection<Sys_ThemeTypeConfig> Sys_ThemeTypeConfig
		{
			get
			{
				return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<Sys_ThemeTypeConfig>("ThemeModel.FK_sys_themetypeconfig", "sys_themetypeconfig");
			}
			set
			{
				if (value != null)
				{
					((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<Sys_ThemeTypeConfig>("ThemeModel.FK_sys_themetypeconfig", "sys_themetypeconfig", value);
				}
			}
		}

		public static Sys_ThemeType CreateSys_ThemeType(string themeTypeID, string themeTypeName, string themeTypeCode, string remark)
		{
			return new Sys_ThemeType
			{
				ThemeTypeID = themeTypeID,
				ThemeTypeName = themeTypeName,
				ThemeTypeCode = themeTypeCode,
				Remark = remark
			};
		}
	}
}
