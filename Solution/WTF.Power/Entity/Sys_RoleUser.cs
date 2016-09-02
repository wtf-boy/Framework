namespace WTF.Power.Entity
{
    using System;
    using System.ComponentModel;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    [Serializable, DataContract(IsReference=true), EdmEntityType(NamespaceName="UserModel", Name="Sys_RoleUser")]
    public class Sys_RoleUser : EntityObject
    {
        private string _RoleID;
        private string _RoleUserID;
        private string _UserID;

        public static Sys_RoleUser CreateSys_RoleUser(string roleUserID, string roleID, string userID)
        {
            return new Sys_RoleUser { RoleUserID = roleUserID, RoleID = roleID, UserID = userID };
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=false), DataMember]
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

        [EdmScalarProperty(EntityKeyProperty=true, IsNullable=false), DataMember]
        public string RoleUserID
        {
            get
            {
                return this._RoleUserID;
            }
            set
            {
                if (this._RoleUserID != value)
                {
                    this.ReportPropertyChanging("RoleUserID");
                    this._RoleUserID = StructuralObject.SetValidValue(value, false);
                    this.ReportPropertyChanged("RoleUserID");
                }
            }
        }

        [SoapIgnore, EdmRelationshipNavigationProperty("UserModel", "FK_Role_Ref_RoleUser", "sys_role"), XmlIgnore, DataMember]
        public WTF.Power.Entity.Sys_Role Sys_Role
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<WTF.Power.Entity.Sys_Role>("UserModel.FK_Role_Ref_RoleUser", "sys_role").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<WTF.Power.Entity.Sys_Role>("UserModel.FK_Role_Ref_RoleUser", "sys_role").Value = value;
            }
        }

        [Browsable(false), DataMember]
        public EntityReference<WTF.Power.Entity.Sys_Role> Sys_RoleReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<WTF.Power.Entity.Sys_Role>("UserModel.FK_Role_Ref_RoleUser", "sys_role");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<WTF.Power.Entity.Sys_Role>("UserModel.FK_Role_Ref_RoleUser", "sys_role", value);
                }
            }
        }

        [SoapIgnore, XmlIgnore, DataMember, EdmRelationshipNavigationProperty("UserModel", "FK_User_Ref_RoleUser", "sys_user")]
        public WTF.Power.Entity.Sys_User Sys_User
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<WTF.Power.Entity.Sys_User>("UserModel.FK_User_Ref_RoleUser", "sys_user").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<WTF.Power.Entity.Sys_User>("UserModel.FK_User_Ref_RoleUser", "sys_user").Value = value;
            }
        }

        [DataMember, Browsable(false)]
        public EntityReference<WTF.Power.Entity.Sys_User> Sys_UserReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<WTF.Power.Entity.Sys_User>("UserModel.FK_User_Ref_RoleUser", "sys_user");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<WTF.Power.Entity.Sys_User>("UserModel.FK_User_Ref_RoleUser", "sys_user", value);
                }
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=false)]
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
    }
}

