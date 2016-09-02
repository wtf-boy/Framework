namespace WTF.Power.Entity
{
    using System;
    using System.ComponentModel;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    [Serializable, DataContract(IsReference=true), EdmEntityType(NamespaceName="UserModel", Name="Sys_RolePower")]
    public class Sys_RolePower : EntityObject
    {
        private string _CoteID;
        private string _CoteModuleID;
        private bool _IsCoteSupper;
        private bool _IsShare;
        private string _ModuleID;
        private string _RoleID;
        private string _RolePowerID;

        public static Sys_RolePower CreateSys_RolePower(string rolePowerID, string roleID, string moduleID, string coteID, string coteModuleID, bool isShare, bool isCoteSupper)
        {
            return new Sys_RolePower { RolePowerID = rolePowerID, RoleID = roleID, ModuleID = moduleID, CoteID = coteID, CoteModuleID = coteModuleID, IsShare = isShare, IsCoteSupper = isCoteSupper };
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=false), DataMember]
        public string CoteID
        {
            get
            {
                return this._CoteID;
            }
            set
            {
                this.ReportPropertyChanging("CoteID");
                this._CoteID = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("CoteID");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=false)]
        public string CoteModuleID
        {
            get
            {
                return this._CoteModuleID;
            }
            set
            {
                this.ReportPropertyChanging("CoteModuleID");
                this._CoteModuleID = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("CoteModuleID");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=false), DataMember]
        public bool IsCoteSupper
        {
            get
            {
                return this._IsCoteSupper;
            }
            set
            {
                this.ReportPropertyChanging("IsCoteSupper");
                this._IsCoteSupper = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("IsCoteSupper");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=false)]
        public bool IsShare
        {
            get
            {
                return this._IsShare;
            }
            set
            {
                this.ReportPropertyChanging("IsShare");
                this._IsShare = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("IsShare");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=false)]
        public string ModuleID
        {
            get
            {
                return this._ModuleID;
            }
            set
            {
                this.ReportPropertyChanging("ModuleID");
                this._ModuleID = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("ModuleID");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=false)]
        public string RoleID
        {
            get
            {
                return this._RoleID;
            }
            set
            {
                this.ReportPropertyChanging("RoleID");
                this._RoleID = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("RoleID");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=true, IsNullable=false)]
        public string RolePowerID
        {
            get
            {
                return this._RolePowerID;
            }
            set
            {
                if (this._RolePowerID != value)
                {
                    this.ReportPropertyChanging("RolePowerID");
                    this._RolePowerID = StructuralObject.SetValidValue(value, false);
                    this.ReportPropertyChanged("RolePowerID");
                }
            }
        }

        [SoapIgnore, EdmRelationshipNavigationProperty("UserModel", "FK_SYS_ROLE_ROLE_REF__SYS_ROLE", "Sys_Role"), XmlIgnore, DataMember]
        public WTF.Power.Entity.Sys_Role Sys_Role
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<WTF.Power.Entity.Sys_Role>("UserModel.FK_SYS_ROLE_ROLE_REF__SYS_ROLE", "Sys_Role").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<WTF.Power.Entity.Sys_Role>("UserModel.FK_SYS_ROLE_ROLE_REF__SYS_ROLE", "Sys_Role").Value = value;
            }
        }

        [Browsable(false), DataMember]
        public EntityReference<WTF.Power.Entity.Sys_Role> Sys_RoleReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<WTF.Power.Entity.Sys_Role>("UserModel.FK_SYS_ROLE_ROLE_REF__SYS_ROLE", "Sys_Role");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<WTF.Power.Entity.Sys_Role>("UserModel.FK_SYS_ROLE_ROLE_REF__SYS_ROLE", "Sys_Role", value);
                }
            }
        }
    }
}

