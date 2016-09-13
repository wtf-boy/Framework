using System;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;

namespace WTF.Theme.Entity
{
	[EdmEntityType(NamespaceName = "UserThemeModel", Name = "Theme_ThemeConfig"), DataContract(IsReference = true)]
	[Serializable]
	public class Theme_ThemeConfig : EntityObject
	{
		private string _ThemeConfigID;

		private string _ThemeConfigName;

		private string _Theme;

		private string _LayoutPath;

		private string _PreviewIco;

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
		public string ThemeConfigName
		{
			get
			{
				return this._ThemeConfigName;
			}
			set
			{
				this.ReportPropertyChanging("ThemeConfigName");
				this._ThemeConfigName = StructuralObject.SetValidValue(value, false);
				this.ReportPropertyChanged("ThemeConfigName");
			}
		}

		[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false), DataMember]
		public string Theme
		{
			get
			{
				return this._Theme;
			}
			set
			{
				this.ReportPropertyChanging("Theme");
				this._Theme = StructuralObject.SetValidValue(value, false);
				this.ReportPropertyChanged("Theme");
			}
		}

		[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false), DataMember]
		public string LayoutPath
		{
			get
			{
				return this._LayoutPath;
			}
			set
			{
				this.ReportPropertyChanging("LayoutPath");
				this._LayoutPath = StructuralObject.SetValidValue(value, false);
				this.ReportPropertyChanged("LayoutPath");
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

		public static Theme_ThemeConfig CreateTheme_ThemeConfig(string themeConfigID, string themeConfigName, string theme, string layoutPath, string previewIco)
		{
			return new Theme_ThemeConfig
			{
				ThemeConfigID = themeConfigID,
				ThemeConfigName = themeConfigName,
				Theme = theme,
				LayoutPath = layoutPath,
				PreviewIco = previewIco
			};
		}
	}
}
