namespace WTF.Theme.Entity
{
    using System;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;

    [Serializable, EdmEntityType(NamespaceName = "UserThemeModel", Name = "theme_modulethemeinfo"), DataContract(IsReference = true)]
    public class Theme_ModuleThemeInfo : EntityObject
    {
        private string _LayoutPath;
        private string _ModuleThemeID;
        private string _ModuleTypeID;
        private string _PreviewIco;
        private string _Theme;
        private string _ThemeConfigID;
        private string _ThemeConfigName;

        public static Theme_ModuleThemeInfo CreateTheme_ModuleThemeInfo(string previewIco, string themeConfigID, string moduleTypeID, string moduleThemeID, string themeConfigName, string theme, string layoutPath)
        {
            return new Theme_ModuleThemeInfo { PreviewIco = previewIco, ThemeConfigID = themeConfigID, ModuleTypeID = moduleTypeID, ModuleThemeID = moduleThemeID, ThemeConfigName = themeConfigName, Theme = theme, LayoutPath = layoutPath };
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
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

        [DataMember, EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
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

        [DataMember, EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
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

        [DataMember, EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
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

        [DataMember, EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
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
    }
}

