using System;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;

namespace WTF.Theme.Entity
{
	[EdmEntityType(NamespaceName = "UserThemeModel", Name = "Theme_ModuleThemeInfo"), DataContract(IsReference = true)]
	[Serializable]
	public class Theme_ModuleThemeInfo : EntityObject
	{
		private string _PreviewIco;

		private string _ThemeConfigID;

		private string _ModuleTypeID;

		private string _ModuleThemeID;

		private string _ThemeConfigName;

		private string _Theme;

		private string _LayoutPath;

		[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false), DataMember]
		public string PreviewIco
		{
			get
			{
				return this._PreviewIco;
			}
			set
			{
				if (this._PreviewIco != value)
				{
					this.ReportPropertyChanging("PreviewIco");
					this._PreviewIco = StructuralObject.SetValidValue(value, false);
					this.ReportPropertyChanged("PreviewIco");
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

		[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false), DataMember]
		public string ModuleTypeID
		{
			get
			{
				return this._ModuleTypeID;
			}
			set
			{
				if (this._ModuleTypeID != value)
				{
					this.ReportPropertyChanging("ModuleTypeID");
					this._ModuleTypeID = StructuralObject.SetValidValue(value, false);
					this.ReportPropertyChanged("ModuleTypeID");
				}
			}
		}

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

		[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false), DataMember]
		public string ThemeConfigName
		{
			get
			{
				return this._ThemeConfigName;
			}
			set
			{
				if (this._ThemeConfigName != value)
				{
					this.ReportPropertyChanging("ThemeConfigName");
					this._ThemeConfigName = StructuralObject.SetValidValue(value, false);
					this.ReportPropertyChanged("ThemeConfigName");
				}
			}
		}

		[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false), DataMember]
		public string Theme
		{
			get
			{
				return this._Theme;
			}
			set
			{
				if (this._Theme != value)
				{
					this.ReportPropertyChanging("Theme");
					this._Theme = StructuralObject.SetValidValue(value, false);
					this.ReportPropertyChanged("Theme");
				}
			}
		}

		[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false), DataMember]
		public string LayoutPath
		{
			get
			{
				return this._LayoutPath;
			}
			set
			{
				if (this._LayoutPath != value)
				{
					this.ReportPropertyChanging("LayoutPath");
					this._LayoutPath = StructuralObject.SetValidValue(value, false);
					this.ReportPropertyChanged("LayoutPath");
				}
			}
		}

		public static Theme_ModuleThemeInfo CreateTheme_ModuleThemeInfo(string previewIco, string themeConfigID, string moduleTypeID, string moduleThemeID, string themeConfigName, string theme, string layoutPath)
		{
			return new Theme_ModuleThemeInfo
			{
				PreviewIco = previewIco,
				ThemeConfigID = themeConfigID,
				ModuleTypeID = moduleTypeID,
				ModuleThemeID = moduleThemeID,
				ThemeConfigName = themeConfigName,
				Theme = theme,
				LayoutPath = layoutPath
			};
		}
	}
}
