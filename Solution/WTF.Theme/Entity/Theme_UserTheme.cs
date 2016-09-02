namespace WTF.Theme.Entity
{
    using System;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;

    [Serializable, DataContract(IsReference = true), EdmEntityType(NamespaceName = "UserThemeModel", Name = "theme_usertheme")]
    public class Theme_UserTheme : EntityObject
    {
        private string _ModuleThemeID;
        private string _ModuleTypeID;
        private int _OperateStyle;
        private string _UserID;
        private string _UserThemeID;

        public static Theme_UserTheme CreateTheme_UserTheme(string userThemeID, string userID, string moduleTypeID, string moduleThemeID, int operateStyle)
        {
            return new Theme_UserTheme { UserThemeID = userThemeID, UserID = userID, ModuleTypeID = moduleTypeID, ModuleThemeID = moduleThemeID, OperateStyle = operateStyle };
        }

        [EdmScalarProperty(EntityKeyProperty = false, IsNullable = false), DataMember]
        public string ModuleThemeID
        {
            get
            {
                return this._ModuleThemeID;
            }
            set
            {
                this.ReportPropertyChanging("ModuleThemeID");
                this._ModuleThemeID = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("ModuleThemeID");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
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

        [DataMember, EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
        public int OperateStyle
        {
            get
            {
                return this._OperateStyle;
            }
            set
            {
                this.ReportPropertyChanging("OperateStyle");
                this._OperateStyle = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("OperateStyle");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
        public string UserID
        {
            get
            {
                return this._UserID;
            }
            set
            {
                this.ReportPropertyChanging("UserID");
                this._UserID = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("UserID");
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

