using System;
using System.ComponentModel;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace WTF.DataConfig.Entity
{
	[EdmEntityType(NamespaceName = "ThemeModel", Name = "Sys_Theme"), DataContract(IsReference = true)]
	[Serializable]
	public class Sys_Theme : EntityObject
	{
		private string _ThemeID;

		private string _ThemeTypeID;

		private string _ThemeName;

		private bool _IsEnable;

		[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false), DataMember]
		public string ThemeID
		{
			get
			{
				return this._ThemeID;
			}
			set
			{
				if (this._ThemeID != value)
				{
					this.ReportPropertyChanging("ThemeID");
					this._ThemeID = StructuralObject.SetValidValue(value, false);
					this.ReportPropertyChanged("ThemeID");
				}
			}
		}

		[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true), DataMember]
		public string ThemeTypeID
		{
			get
			{
				return this._ThemeTypeID;
			}
			set
			{
				this.ReportPropertyChanging("ThemeTypeID");
				this._ThemeTypeID = StructuralObject.SetValidValue(value, true);
				this.ReportPropertyChanged("ThemeTypeID");
			}
		}

		[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false), DataMember]
		public string ThemeName
		{
			get
			{
				return this._ThemeName;
			}
			set
			{
				this.ReportPropertyChanging("ThemeName");
				this._ThemeName = StructuralObject.SetValidValue(value, false);
				this.ReportPropertyChanged("ThemeName");
			}
		}

		[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false), DataMember]
		public bool IsEnable
		{
			get
			{
				return this._IsEnable;
			}
			set
			{
				this.ReportPropertyChanging("IsEnable");
				this._IsEnable = StructuralObject.SetValidValue(value);
				this.ReportPropertyChanged("IsEnable");
			}
		}

		[EdmRelationshipNavigationProperty("ThemeModel", "FK_sys_theme", "sys_themetype"), DataMember, SoapIgnore, XmlIgnore]
		public Sys_ThemeType Sys_ThemeType
		{
			get
			{
				return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Sys_ThemeType>("ThemeModel.FK_sys_theme", "sys_themetype").Value;
			}
			set
			{
				((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Sys_ThemeType>("ThemeModel.FK_sys_theme", "sys_themetype").Value = value;
			}
		}

		[Browsable(false), DataMember]
		public EntityReference<Sys_ThemeType> Sys_ThemeTypeReference
		{
			get
			{
				return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Sys_ThemeType>("ThemeModel.FK_sys_theme", "sys_themetype");
			}
			set
			{
				if (value != null)
				{
					((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<Sys_ThemeType>("ThemeModel.FK_sys_theme", "sys_themetype", value);
				}
			}
		}

		[EdmRelationshipNavigationProperty("ThemeModel", "FK_sys_themeconfig", "sys_themeconfig"), DataMember, SoapIgnore, XmlIgnore]
		public EntityCollection<Sys_ThemeConfig> Sys_ThemeConfig
		{
			get
			{
				return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<Sys_ThemeConfig>("ThemeModel.FK_sys_themeconfig", "sys_themeconfig");
			}
			set
			{
				if (value != null)
				{
					((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<Sys_ThemeConfig>("ThemeModel.FK_sys_themeconfig", "sys_themeconfig", value);
				}
			}
		}

		public static Sys_Theme CreateSys_Theme(string themeID, string themeName, bool isEnable)
		{
			return new Sys_Theme
			{
				ThemeID = themeID,
				ThemeName = themeName,
				IsEnable = isEnable
			};
		}
	}
}
