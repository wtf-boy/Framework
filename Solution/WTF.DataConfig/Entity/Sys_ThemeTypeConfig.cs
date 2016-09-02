using System;
using System.ComponentModel;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace WTF.DataConfig.Entity
{
	[EdmEntityType(NamespaceName = "ThemeModel", Name = "Sys_ThemeTypeConfig"), DataContract(IsReference = true)]
	[Serializable]
	public class Sys_ThemeTypeConfig : EntityObject
	{
		private string _ThemeTypeConfigID;

		private string _ThemeTypeID;

		private string _ConfigName;

		private string _ConfigKey;

		private string _ConfigValue;

		private string _ConfigRemark;

		[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false), DataMember]
		public string ThemeTypeConfigID
		{
			get
			{
				return this._ThemeTypeConfigID;
			}
			set
			{
				if (this._ThemeTypeConfigID != value)
				{
					this.ReportPropertyChanging("ThemeTypeConfigID");
					this._ThemeTypeConfigID = StructuralObject.SetValidValue(value, false);
					this.ReportPropertyChanged("ThemeTypeConfigID");
				}
			}
		}

		[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false), DataMember]
		public string ThemeTypeID
		{
			get
			{
				return this._ThemeTypeID;
			}
			set
			{
				this.ReportPropertyChanging("ThemeTypeID");
				this._ThemeTypeID = StructuralObject.SetValidValue(value, false);
				this.ReportPropertyChanged("ThemeTypeID");
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

		[EdmRelationshipNavigationProperty("ThemeModel", "FK_sys_themetypeconfig", "sys_themetype"), DataMember, SoapIgnore, XmlIgnore]
		public Sys_ThemeType Sys_ThemeType
		{
			get
			{
				return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Sys_ThemeType>("ThemeModel.FK_sys_themetypeconfig", "sys_themetype").Value;
			}
			set
			{
				((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Sys_ThemeType>("ThemeModel.FK_sys_themetypeconfig", "sys_themetype").Value = value;
			}
		}

		[Browsable(false), DataMember]
		public EntityReference<Sys_ThemeType> Sys_ThemeTypeReference
		{
			get
			{
				return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Sys_ThemeType>("ThemeModel.FK_sys_themetypeconfig", "sys_themetype");
			}
			set
			{
				if (value != null)
				{
					((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<Sys_ThemeType>("ThemeModel.FK_sys_themetypeconfig", "sys_themetype", value);
				}
			}
		}

		public static Sys_ThemeTypeConfig CreateSys_ThemeTypeConfig(string themeTypeConfigID, string themeTypeID, string configName, string configKey, string configValue, string configRemark)
		{
			return new Sys_ThemeTypeConfig
			{
				ThemeTypeConfigID = themeTypeConfigID,
				ThemeTypeID = themeTypeID,
				ConfigName = configName,
				ConfigKey = configKey,
				ConfigValue = configValue,
				ConfigRemark = configRemark
			};
		}
	}
}
