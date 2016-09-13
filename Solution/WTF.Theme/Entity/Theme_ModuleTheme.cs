using System;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;

namespace WTF.Theme.Entity
{
	[EdmEntityType(NamespaceName = "UserThemeModel", Name = "Theme_ModuleTheme"), DataContract(IsReference = true)]
	[Serializable]
	public class Theme_ModuleTheme : EntityObject
	{
		private string _ModuleThemeID;

		private string _ModuleTypeID;

		private string _ThemeConfigID;

		private string _PreviewIco;

		[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false), DataMember]
		public string ModuleThemeID
		{
			get
			{
				return this._ModuleThemeID;
			}
			set
			{
				if (this._ModuleThemeID != value)
				{
					this.ReportPropertyChanging("ModuleThemeID");
					this._ModuleThemeID = StructuralObject.SetValidValue(value, false);
					this.ReportPropertyChanged("ModuleThemeID");
				}
			}
		}

		[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false), DataMember]
		public string ModuleTypeID
		{
			get
			{
				return this._ModuleTypeID;
			}
			set
			{
				this.ReportPropertyChanging("ModuleTypeID");
				this._ModuleTypeID = StructuralObject.SetValidValue(value, false);
				this.ReportPropertyChanged("ModuleTypeID");
			}
		}

		[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false), DataMember]
		public string ThemeConfigID
		{
			get
			{
				return this._ThemeConfigID;
			}
			set
			{
				this.ReportPropertyChanging("ThemeConfigID");
				this._ThemeConfigID = StructuralObject.SetValidValue(value, false);
				this.ReportPropertyChanged("ThemeConfigID");
			}
		}

		[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false), DataMember]
		public string PreviewIco
		{
			get
			{
				return this._PreviewIco;
			}
			set
			{
				this.ReportPropertyChanging("PreviewIco");
				this._PreviewIco = StructuralObject.SetValidValue(value, false);
				this.ReportPropertyChanged("PreviewIco");
			}
		}

		public static Theme_ModuleTheme CreateTheme_ModuleTheme(string moduleThemeID, string moduleTypeID, string themeConfigID, string previewIco)
		{
			return new Theme_ModuleTheme
			{
				ModuleThemeID = moduleThemeID,
				ModuleTypeID = moduleTypeID,
				ThemeConfigID = themeConfigID,
				PreviewIco = previewIco
			};
		}
	}
}
