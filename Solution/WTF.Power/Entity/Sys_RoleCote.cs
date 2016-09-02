namespace WTF.Power.Entity
{
    using System;
    using System.ComponentModel;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    [Serializable, DataContract(IsReference=true), EdmEntityType(NamespaceName="UserModel", Name="Sys_RoleCote")]
    public class Sys_RoleCote : EntityObject
    {
        private string _CoteID;
        private string _ModuleID;
        private string _RoleCoteID;
        private string _RoleID;

        public static Sys_RoleCote CreateSys_RoleCote(string roleCoteID, string roleID, string coteID, string moduleID)
        {
            return new Sys_RoleCote { RoleCoteID = roleCoteID, RoleID = roleID, CoteID = coteID, ModuleID = moduleID };
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

        [EdmScalarProperty(EntityKeyProperty=true, IsNullable=false), DataMember]
        public string RoleCoteID
        {
            get
            {
                return this._RoleCoteID;
            }
            set
            {
                if (this._RoleCoteID != value)
                {
                    this.ReportPropertyChanging("RoleCoteID");
                    this._RoleCoteID = StructuralObject.SetValidValue(value, false);
                    this.ReportPropertyChanged("RoleCoteID");
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

        [EdmRelationshipNavigationProperty("UserModel", "FK_RoleCote_Ref_Role", "sys_role"), XmlIgnore, DataMember, SoapIgnore]
        public WTF.Power.Entity.Sys_Role Sys_Role
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<WTF.Power.Entity.Sys_Role>("UserModel.FK_RoleCote_Ref_Role", "sys_role").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<WTF.Power.Entity.Sys_Role>("UserModel.FK_RoleCote_Ref_Role", "sys_role").Value = value;
            }
        }

        [XmlIgnore, DataMember, EdmRelationshipNavigationProperty("UserModel", "FK_RoleCotePower_Ref_RoleCote", "sys_rolecotepower"), SoapIgnore]
        public EntityCollection<WTF.Power.Entity.Sys_RoleCotePower> Sys_RoleCotePower
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<WTF.Power.Entity.Sys_RoleCotePower>("UserModel.FK_RoleCotePower_Ref_RoleCote", "sys_rolecotepower");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<WTF.Power.Entity.Sys_RoleCotePower>("UserModel.FK_RoleCotePower_Ref_RoleCote", "sys_rolecotepower", value);
                }
            }
        }

        [Browsable(false), DataMember]
        public EntityReference<WTF.Power.Entity.Sys_Role> Sys_RoleReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<WTF.Power.Entity.Sys_Role>("UserModel.FK_RoleCote_Ref_Role", "sys_role");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<WTF.Power.Entity.Sys_Role>("UserModel.FK_RoleCote_Ref_Role", "sys_role", value);
                }
            }
        }
    }
}

