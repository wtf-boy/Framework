using System;
using System.ComponentModel;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace WTF.DataConfig.Entity
{
	[EdmEntityType(NamespaceName = "ThemeModel", Name = "Sys_ThemeConfig"), DataContract(IsReference = true)]
	[Serializable]
	public class Sys_ThemeConfig : EntityObject
	{
		private string _ThemeConfigID;

		private string _ThemeTypeConfigID;

		private string _ThemeID;

		private string _ConfigName;

		private string _ConfigKey;

		private string _ConfigValue;

		private string _ConfigRemark;

		[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false), DataMember]
		public string ThemeConfigID
		{
			get
			{
				return this._ThemeConfigID;
			}
			set
			{
				if (this._ThemeConfigID != value)
				{
					this.ReportPropertyChanging("ThemeConfigID");
					this._ThemeConfigID = StructuralObject.SetValidValue(value, false);
					this.ReportPropertyChanged("ThemeConfigID");
				}
			}
		}

		[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false), DataMember]
		public string ThemeTypeConfigID
		{
			get
			{
				return this._ThemeTypeConfigID;
			}
			set
			{
				this.ReportPropertyChanging("ThemeTypeConfigID");
				this._ThemeTypeConfigID = StructuralObject.SetValidValue(value, false);
				this.ReportPropertyChanged("ThemeTypeConfigID");
			}
		}

		[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false), DataMember]
		public string ThemeID
		{
			get
			{
				return this._ThemeID;
			}
			set
			{
				this.ReportPropertyChanging("ThemeID");
				this._ThemeID = StructuralObject.SetValidValue(value, false);
				this.ReportPropertyChanged("ThemeID");
			}
		}

		[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false), DataMember]
		public string ConfigName
		{
			get
			{
				return this._ConfigName;
			}
			set
			{
				this.ReportPropertyChanging("ConfigName");
				this._ConfigName = StructuralObject.SetValidValue(value, false);
				this.ReportPropertyChanged("ConfigName");
			}
		}

		[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false), DataMember]
		public string ConfigKey
		{
			get
			{
				return this._ConfigKey;
			}
			set
			{
				this.ReportPropertyChanging("ConfigKey");
				this._ConfigKey = StructuralObject.SetValidValue(value, false);
				this.ReportPropertyChanged("ConfigKey");
			}
		}

		[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false), DataMember]
		public string ConfigValue
		{
			get
			{
				return this._ConfigValue;
			}
			set
			{
				this.ReportPropertyChanging("ConfigValue");
				this._ConfigValue = StructuralObject.SetValidValue(value, false);
				this.ReportPropertyChanged("ConfigValue");
			}
		}

		[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false), DataMember]
		public string ConfigRemark
		{
			get
			{
				return this._ConfigRemark;
			}
			set
			{
				this.ReportPropertyChanging("ConfigRemark");
				this._ConfigRemark = StructuralObject.SetValidValue(value, false);
				this.ReportPropertyChanged("ConfigRemark");
			}
		}

		[EdmRelationshipNavigationProperty("ThemeModel", "FK_sys_themeconfig", "sys_theme"), DataMember, SoapIgnore, XmlIgnore]
		public Sys_Theme Sys_Theme
		{
			get
			{
				return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Sys_Theme>("ThemeModel.FK_sys_themeconfig", "sys_theme").Value;
			}
			set
			{
				((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Sys_Theme>("ThemeModel.FK_sys_themeconfig", "sys_theme").Value = value;
			}
		}

		[Browsable(false), DataMember]
		public EntityReference<Sys_Theme> Sys_ThemeReference
		{
			get
			{
				return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Sys_Theme>("ThemeModel.FK_sys_themeconfig", "sys_theme");
			}
			set
			{
				if (value != null)
				{
					((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<Sys_Theme>("ThemeModel.FK_sys_themeconfig", "sys_theme", value);
				}
			}
		}

		public static Sys_ThemeConfig CreateSys_ThemeConfig(string themeConfigID, string themeTypeConfigID, string themeID, string configName, string configKey, string configValue, string configRemark)
		{
			return new Sys_ThemeConfig
			{
				ThemeConfigID = themeConfigID,
				ThemeTypeConfigID = themeTypeConfigID,
				ThemeID = themeID,
				ConfigName = configName,
				ConfigKey = configKey,
				ConfigValue = configValue,
				ConfigRemark = configRemark
			};
		}
	}
}
