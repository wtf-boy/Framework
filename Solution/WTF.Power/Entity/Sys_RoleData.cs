namespace WTF.Power.Entity
{
    using System;
    using System.ComponentModel;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    [Serializable, DataContract(IsReference=true), EdmEntityType(NamespaceName="UserModel", Name="Sys_RoleData")]
    public class Sys_RoleData : EntityObject
    {
        private string _DataSelect;
        private string _ModuleDataID;
        private string _ModuleID;
        private string _RoleDataID;
        private string _RoleID;

        public static Sys_RoleData CreateSys_RoleData(string roleDataID, string roleID, string moduleID, string moduleDataID, string dataSelect)
        {
            return new Sys_RoleData { RoleDataID = roleDataID, RoleID = roleID, ModuleID = moduleID, ModuleDataID = moduleDataID, DataSelect = dataSelect };
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=false)]
        public string DataSelect
        {
            get
            {
                return this._DataSelect;
            }
            set
            {
                this.ReportPropertyChanging("DataSelect");
                this._DataSelect = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("DataSelect");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=false), DataMember]
        public string ModuleDataID
        {
            get
            {
                return this._ModuleDataID;
            }
            set
            {
                this.ReportPropertyChanging("ModuleDataID");
                this._ModuleDataID = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("ModuleDataID");
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

        [DataMember, EdmScalarProperty(EntityKeyProperty=true, IsNullable=false)]
        public string RoleDataID
        {
            get
            {
                return this._RoleDataID;
            }
            set
            {
                if (this._RoleDataID != value)
                {
                    this.ReportPropertyChanging("RoleDataID");
                    this._RoleDataID = StructuralObject.SetValidValue(value, false);
                    this.ReportPropertyChanged("RoleDataID");
                }
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

        [SoapIgnore, EdmRelationshipNavigationProperty("UserModel", "FK_RoleData_Ref_Role", "sys_role"), DataMember, XmlIgnore]
        public WTF.Power.Entity.Sys_Role Sys_Role
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<WTF.Power.Entity.Sys_Role>("UserModel.FK_RoleData_Ref_Role", "sys_role").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<WTF.Power.Entity.Sys_Role>("UserModel.FK_RoleData_Ref_Role", "sys_role").Value = value;
            }
        }

        [DataMember, Browsable(false)]
        public EntityReference<WTF.Power.Entity.Sys_Role> Sys_RoleReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<WTF.Power.Entity.Sys_Role>("UserModel.FK_RoleData_Ref_Role", "sys_role");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<WTF.Power.Entity.Sys_Role>("UserModel.FK_RoleData_Ref_Role", "sys_role", value);
                }
            }
        }
    }
}

