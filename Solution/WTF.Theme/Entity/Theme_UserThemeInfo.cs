namespace WTF.Theme.Entity
{
    using System;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;

    [Serializable, EdmEntityType(NamespaceName = "UserThemeModel", Name = "theme_userthemeinfo"), DataContract(IsReference = true)]
    public class Theme_UserThemeInfo : EntityObject
    {
        private string _LayoutPath;
        private string _ModuleThemeID;
        private string _ModuleTypeID;
        private int _OperateStyle;
        private string _Theme;
        private string _ThemeConfigID;
        private string _ThemeConfigName;
        private string _UserID;
        private string _UserThemeID;

        public static Theme_UserThemeInfo CreateTheme_UserThemeInfo(int operateStyle, string userID, string userThemeID, string moduleTypeID, string layoutPath, string theme, string themeConfigName, string moduleThemeID, string themeConfigID)
        {
            return new Theme_UserThemeInfo { OperateStyle = operateStyle, UserID = userID, UserThemeID = userThemeID, ModuleTypeID = moduleTypeID, LayoutPath = layoutPath, Theme = theme, ThemeConfigName = themeConfigName, ModuleThemeID = moduleThemeID, ThemeConfigID = themeConfigID };
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
        public int OperateStyle
        {
            get
            {
                return this._OperateStyle;
            }
            set
            {
                if (this._OperateStyle != value)
                {
                    this.ReportPropertyChanging("OperateStyle");
                    this._OperateStyle = StructuralObject.SetValidValue(value);
                    this.ReportPropertyChanged("OperateStyle");
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

        [DataMember, EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
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

        [DataMember, EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
        public string UserID
        {
            get
            {
                return this._UserID;
            }
            set
            {
                if (this._UserID != value)
                {
                    this.ReportPropertyChanging("UserID");
                    this._UserID = StructuralObject.SetValidValue(value, false);
                    this.ReportPropertyChanged("UserID");
                }
            }
        }

        [EdmScalarProperty(EntityKeyProperty = true, IsNullable = false), DataMember]
        public string UserThemeID
        {
            get
            {
                return this._UserThemeID;
            }
            set
            {
                if (this._UserThemeID != value)
                {
                    this.ReportPropertyChanging("UserThemeID");
                    this._UserThemeID = StructuralObject.SetValidValue(value, false);
                    this.ReportPropertyChanged("UserThemeID");
                }
            }
        }
    }
}

