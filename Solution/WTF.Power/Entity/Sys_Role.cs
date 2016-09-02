namespace WTF.Power.Entity
{
    using System;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    [Serializable, DataContract(IsReference=true), EdmEntityType(NamespaceName="UserModel", Name="Sys_Role")]
    public class Sys_Role : EntityObject
    {
        private string _AccountTypeID;
        private string _AuthorizeGroupID;
        private DateTime _CreateDate;
        private bool _IsLockOut;
        private bool _IsSystem;
        private bool _IsUserRole;
        private string _ModuleTypeID;
        private string _RefUserID;
        private string _Remark;
        private string _RoleCode;
        private string _RoleGroupID;
        private string _RoleID;
        private string _RoleName;
        private string _UserID;

        public static Sys_Role CreateSys_Role(string roleID, string roleGroupID, string accountTypeID, string moduleTypeID, string roleName, string roleCode, bool isSystem, string userID, bool isLockOut, string remark, bool isUserRole, string authorizeGroupID, string refUserID, DateTime createDate)
        {
            return new Sys_Role { RoleID = roleID, RoleGroupID = roleGroupID, AccountTypeID = accountTypeID, ModuleTypeID = moduleTypeID, RoleName = roleName, RoleCode = roleCode, IsSystem = isSystem, UserID = userID, IsLockOut = isLockOut, Remark = remark, IsUserRole = isUserRole, AuthorizeGroupID = authorizeGroupID, RefUserID = refUserID, CreateDate = createDate };
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=false)]
        public string AccountTypeID
        {
            get
            {
                return this._AccountTypeID;
            }
            set
            {
                this.ReportPropertyChanging("AccountTypeID");
                this._AccountTypeID = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("AccountTypeID");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=false)]
        public string AuthorizeGroupID
        {
            get
            {
                return this._AuthorizeGroupID;
            }
            set
            {
                this.ReportPropertyChanging("AuthorizeGroupID");
                this._AuthorizeGroupID = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("AuthorizeGroupID");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=false), DataMember]
        public DateTime CreateDate
        {
            get
            {
                return this._CreateDate;
            }
            set
            {
                this.ReportPropertyChanging("CreateDate");
                this._CreateDate = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("CreateDate");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=false)]
        public bool IsLockOut
        {
            get
            {
                return this._IsLockOut;
            }
            set
            {
                this.ReportPropertyChanging("IsLockOut");
                this._IsLockOut = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("IsLockOut");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=false)]
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
        public bool IsUserRole
        {
            get
            {
                return this._IsUserRole;
            }
            set
            {
                this.ReportPropertyChanging("IsUserRole");
                this._IsUserRole = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("IsUserRole");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=false)]
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

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=false)]
        public string RefUserID
        {
            get
            {
                return this._RefUserID;
            }
            set
            {
                this.ReportPropertyChanging("RefUserID");
                this._RefUserID = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("RefUserID");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=false), DataMember]
        public string Remark
        {
            get
            {
                return this._Remark;
            }
            set
            {
                this.ReportPropertyChanging("Remark");
                this._Remark = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("Remark");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=false)]
        public string RoleCode
        {
            get
            {
                return this._RoleCode;
            }
            set
            {
                this.ReportPropertyChanging("RoleCode");
                this._RoleCode = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("RoleCode");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=false), DataMember]
        public string RoleGroupID
        {
            get
            {
                return this._RoleGroupID;
            }
            set
            {
                this.ReportPropertyChanging("RoleGroupID");
                this._RoleGroupID = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("RoleGroupID");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=true, IsNullable=false), DataMember]
        public string RoleID
        {
            get
            {
                return this._RoleID;
            }
            set
            {
                if (this._RoleID != value)
                {
                    this.ReportPropertyChanging("RoleID");
                    this._RoleID = StructuralObject.SetValidValue(value, false);
                    this.ReportPropertyChanged("RoleID");
                }
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=false), DataMember]
        public string RoleName
        {
            get
            {
                return this._RoleName;
            }
            set
            {
                this.ReportPropertyChanging("RoleName");
                this._RoleName = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("RoleName");
            }
        }

        [DataMember, SoapIgnore, XmlIgnore, EdmRelationshipNavigationProperty("UserModel", "FK_RoleCote_Ref_Role", "sys_rolecote")]
        public EntityCollection<WTF.Power.Entity.Sys_RoleCote> Sys_RoleCote
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<WTF.Power.Entity.Sys_RoleCote>("UserModel.FK_RoleCote_Ref_Role", "sys_rolecote");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<WTF.Power.Entity.Sys_RoleCote>("UserModel.FK_RoleCote_Ref_Role", "sys_rolecote", value);
                }
            }
        }

        [DataMember, SoapIgnore, XmlIgnore, EdmRelationshipNavigationProperty("UserModel", "FK_RoleData_Ref_Role", "sys_roledata")]
        public EntityCollection<WTF.Power.Entity.Sys_RoleData> Sys_RoleData
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<WTF.Power.Entity.Sys_RoleData>("UserModel.FK_RoleData_Ref_Role", "sys_roledata");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<WTF.Power.Entity.Sys_RoleData>("UserModel.FK_RoleData_Ref_Role", "sys_roledata", value);
                }
            }
        }

        [XmlIgnore, SoapIgnore, DataMember, EdmRelationshipNavigationProperty("UserModel", "FK_SYS_ROLE_ROLE_REF__SYS_ROLE", "sys_rolepower")]
        public EntityCollection<WTF.Power.Entity.Sys_RolePower> Sys_RolePower
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<WTF.Power.Entity.Sys_RolePower>("UserModel.FK_SYS_ROLE_ROLE_REF__SYS_ROLE", "sys_rolepower");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<WTF.Power.Entity.Sys_RolePower>("UserModel.FK_SYS_ROLE_ROLE_REF__SYS_ROLE", "sys_rolepower", value);
                }
            }
        }

        [XmlIgnore, EdmRelationshipNavigationProperty("UserModel", "FK_Role_Ref_RoleUser", "sys_roleuser"), SoapIgnore, DataMember]
        public EntityCollection<WTF.Power.Entity.Sys_RoleUser> Sys_RoleUser
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<WTF.Power.Entity.Sys_RoleUser>("UserModel.FK_Role_Ref_RoleUser", "sys_roleuser");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<WTF.Power.Entity.Sys_RoleUser>("UserModel.FK_Role_Ref_RoleUser", "sys_roleuser", value);
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

