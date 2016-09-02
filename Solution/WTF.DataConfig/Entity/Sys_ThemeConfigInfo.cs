using System;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;

namespace WTF.DataConfig.Entity
{
	[EdmEntityType(NamespaceName = "ThemeModel", Name = "Sys_ThemeConfigInfo"), DataContract(IsReference = true)]
	[Serializable]
	public class Sys_ThemeConfigInfo : EntityObject
	{
		private string _ThemeTypeCode;

		private string _ConfigKey;

		private string _ConfigValue;

		private string _ThemeConfigID;

		[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false), DataMember]
		public string ThemeTypeCode
		{
			get
			{
				return this._ThemeTypeCode;
			}
			set
			{
				if (this._ThemeTypeCode != value)
				{
					this.ReportPropertyChanging("ThemeTypeCode");
					this._ThemeTypeCode = StructuralObject.SetValidValue(value, false);
					this.ReportPropertyChanged("ThemeTypeCode");
				}
			}
		}

		[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false), DataMember]
		public string ConfigKey
		{
			get
			{
				return this._ConfigKey;
			}
			set
			{
				if (this._ConfigKey != value)
				{
					this.ReportPropertyChanging("ConfigKey");
					this._ConfigKey = StructuralObject.SetValidValue(value, false);
					this.ReportPropertyChanged("ConfigKey");
				}
			}
		}

		[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false), DataMember]
		public string ConfigValue
		{
			get
			{
				return this._ConfigValue;
			}
			set
			{
				if (this._ConfigValue != value)
				{
					this.ReportPropertyChanging("ConfigValue");
					this._ConfigValue = StructuralObject.SetValidValue(value, false);
					this.ReportPropertyChanged("ConfigValue");
				}
			}
		}

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

		public static Sys_ThemeConfigInfo CreateSys_ThemeConfigInfo(string themeTypeCode, string configKey, string configValue, string themeConfigID)
		{
			return new Sys_ThemeConfigInfo
			{
				ThemeTypeCode = themeTypeCode,
				ConfigKey = configKey,
				ConfigValue = configValue,
				ThemeConfigID = themeConfigID
			};
		}
	}
}
