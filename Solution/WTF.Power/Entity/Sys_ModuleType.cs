namespace WTF.Power.Entity
{
    using System;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    [Serializable, DataContract(IsReference=true), EdmEntityType(NamespaceName="ModuleModel", Name="Sys_ModuleType")]
    public class Sys_ModuleType : EntityObject
    {
        private bool _IsDispose;
        private bool _IsSystem;
        private int _LogCategoryID;
        private string _ModuleTypeCode;
        private string _ModuleTypeID;
        private string _ModuleTypeName;
        private string _UserType;

        public static Sys_ModuleType CreateSys_ModuleType(string moduleTypeID, string moduleTypeCode, string moduleTypeName, string userType, int logCategoryID, bool isDispose, bool isSystem)
        {
            return new Sys_ModuleType { ModuleTypeID = moduleTypeID, ModuleTypeCode = moduleTypeCode, ModuleTypeName = moduleTypeName, UserType = userType, LogCategoryID = logCategoryID, IsDispose = isDispose, IsSystem = isSystem };
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=false), DataMember]
        public bool IsDispose
        {
            get
            {
                return this._IsDispose;
            }
            set
            {
                this.ReportPropertyChanging("IsDispose");
                this._IsDispose = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("IsDispose");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=false), DataMember]
        public bool IsSystem
        {
            get
            {
                return this._IsSystem;
            }
            set
            {
                this.ReportPropertyChanging("IsSystem");
                this._IsSystem = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("IsSystem");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=false), DataMember]
        public int LogCategoryID
        {
            get
            {
                return this._LogCategoryID;
            }
            set
            {
                this.ReportPropertyChanging("LogCategoryID");
                this._LogCategoryID = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("LogCategoryID");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=false), DataMember]
        public string ModuleTypeCode
        {
            get
            {
                return this._ModuleTypeCode;
            }
            set
            {
                this.ReportPropertyChanging("ModuleTypeCode");
                this._ModuleTypeCode = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("ModuleTypeCode");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=true, IsNullable=false)]
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

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=false)]
        public string ModuleTypeName
        {
            get
            {
                return this._ModuleTypeName;
            }
            set
            {
                this.ReportPropertyChanging("ModuleTypeName");
                this._ModuleTypeName = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("ModuleTypeName");
            }
        }

        [XmlIgnore, SoapIgnore, DataMember, EdmRelationshipNavigationProperty("ModuleModel", "FK_module_moduletype", "sys_module")]
        public EntityCollection<WTF.Power.Entity.Sys_Module> Sys_Module
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<WTF.Power.Entity.Sys_Module>("ModuleModel.FK_module_moduletype", "sys_module");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<WTF.Power.Entity.Sys_Module>("ModuleModel.FK_module_moduletype", "sys_module", value);
                }
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=false)]
        public string UserType
        {
            get
            {
                return this._UserType;
            }
            set
            {
                this.ReportPropertyChanging("UserType");
                this._UserType = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("UserType");
            }
        }
    }
}

