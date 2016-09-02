namespace WTF.Theme.Entity
{
    using System;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;

    [Serializable, EdmEntityType(NamespaceName = "UserThemeModel", Name = "theme_themeconfig"), DataContract(IsReference = true)]
    public class Theme_ThemeConfig : EntityObject
    {
        private string _LayoutPath;
        private string _PreviewIco;
        private string _Theme;
        private string _ThemeConfigID;
        private string _ThemeConfigName;

        public static Theme_ThemeConfig CreateTheme_ThemeConfig(string themeConfigID, string themeConfigName, string theme, string layoutPath, string previewIco)
        {
            return new Theme_ThemeConfig { ThemeConfigID = themeConfigID, ThemeConfigName = themeConfigName, Theme = theme, LayoutPath = layoutPath, PreviewIco = previewIco };
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
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

        [DataMember, EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
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

        [DataMember, EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
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
    }
}

