//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace WTF.Theme.Entity
{
    using System;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;

    [Serializable, EdmEntityType(NamespaceName = "UserThemeModel", Name = "theme_moduletheme"), DataContract(IsReference = true)]
    public class Theme_ModuleTheme : EntityObject
    {
        private string _ModuleThemeID;
        private string _ModuleTypeID;
        private string _PreviewIco;
        private string _ThemeConfigID;

        public static Theme_ModuleTheme CreateTheme_ModuleTheme(string moduleThemeID, string moduleTypeID, string themeConfigID, string previewIco)
        {
            return new Theme_ModuleTheme { ModuleThemeID = moduleThemeID, ModuleTypeID = moduleTypeID, ThemeConfigID = themeConfigID, PreviewIco = previewIco };
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
    }
}