namespace WTF.Power.Entity
{
    using System;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;

    [Serializable, DataContract(IsReference=true), EdmEntityType(NamespaceName="UserModel", Name="Sys_ModuleRole")]
    public class Sys_ModuleRole : EntityObject
    {
        private string _AccountTypeID;
        private string _AuthorizeGroupID;
        private bool _IsLockOut;
        private bool _IsSystem;
        private string _ModuleTypeCode;
        private string _ModuleTypeID;
        private string _ModuleTypeName;
        private string _Remark;
        private string _RoleCode;
        private string _RoleGroupID;
        private string _RoleID;
        private string _RoleName;
        private string _UserID;
        private string _UserType;

        public static Sys_ModuleRole CreateSys_ModuleRole(string moduleTypeName, string moduleTypeCode, string roleID, string roleGroupID, string moduleTypeID, string roleName, string roleCode, string userID, bool isLockOut, string remark, string userType, bool isSystem, string accountTypeID, string authorizeGroupID)
        {
            return new Sys_ModuleRole { ModuleTypeName = moduleTypeName, ModuleTypeCode = moduleTypeCode, RoleID = roleID, RoleGroupID = roleGroupID, ModuleTypeID = moduleTypeID, RoleName = roleName, RoleCode = roleCode, UserID = userID, IsLockOut = isLockOut, Remark = remark, UserType = userType, IsSystem = isSystem, AccountTypeID = accountTypeID, AuthorizeGroupID = authorizeGroupID };
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=true, IsNullable=false)]
        public string AccountTypeID
        {
            get
            {
                return this._AccountTypeID;
            }
            set
            {
                if (this._AccountTypeID != value)
                {
                    this.ReportPropertyChanging("AccountTypeID");
                    this._AccountTypeID = StructuralObject.SetValidValue(value, false);
                    this.ReportPropertyChanged("AccountTypeID");
                }
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=true, IsNullable=false)]
        public string AuthorizeGroupID
        {
            get
            {
                return this._AuthorizeGroupID;
            }
            set
            {
                if (this._AuthorizeGroupID != value)
                {
                    this.ReportPropertyChanging("AuthorizeGroupID");
                    this._AuthorizeGroupID = StructuralObject.SetValidValue(value, false);
                    this.ReportPropertyChanged("AuthorizeGroupID");
                }
            }
        }

        [EdmScalarProperty(EntityKeyProperty=true, IsNullable=false), DataMember]
        public bool IsLockOut
        {
            get
            {
                return this._IsLockOut;
            }
            set
            {
                if (this._IsLockOut != value)
                {
                    this.ReportPropertyChanging("IsLockOut");
                    this._IsLockOut = StructuralObject.SetValidValue(value);
                    this.ReportPropertyChanged("IsLockOut");
                }
            }
        }

        [EdmScalarProperty(EntityKeyProperty=true, IsNullable=false), DataMember]
        public bool IsSystem
        {
            get
            {
                return this._IsSystem;
            }
            set
            {
                if (this._IsSystem != value)
                {
                    this.ReportPropertyChanging("IsSystem");
                    this._IsSystem = StructuralObject.SetValidValue(value);
                    this.ReportPropertyChanged("IsSystem");
                }
            }
        }

        [EdmScalarProperty(EntityKeyProperty=true, IsNullable=false), DataMember]
        public string ModuleTypeCode
        {
            get
            {
                return this._ModuleTypeCode;
            }
            set
            {
                if (this._ModuleTypeCode != value)
                {
                    this.ReportPropertyChanging("ModuleTypeCode");
                    this._ModuleTypeCode = StructuralObject.SetValidValue(value, false);
                    this.ReportPropertyChanged("ModuleTypeCode");
                }
            }
        }

        [EdmScalarProperty(EntityKeyProperty=true, IsNullable=false), DataMember]
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

        [DataMember, EdmScalarProperty(EntityKeyProperty=true, IsNullable=false)]
        public string ModuleTypeName
        {
            get
            {
                return this._ModuleTypeName;
            }
            set
            {
                if (this._ModuleTypeName != value)
                {
                    this.ReportPropertyChanging("ModuleTypeName");
                    this._ModuleTypeName = StructuralObject.SetValidValue(value, false);
                    this.ReportPropertyChanged("ModuleTypeName");
                }
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=true, IsNullable=false)]
        public string Remark
        {
            get
            {
                return this._Remark;
            }
            set
            {
                if (this._Remark != value)
                {
                    this.ReportPropertyChanging("Remark");
                    this._Remark = StructuralObject.SetValidValue(value, false);
                    this.ReportPropertyChanged("Remark");
                }
            }
        }

        [EdmScalarProperty(EntityKeyProperty=true, IsNullable=false), DataMember]
        public string RoleCode
        {
            get
            {
                return this._RoleCode;
            }
            set
            {
                if (this._RoleCode != value)
                {
                    this.ReportPropertyChanging("RoleCode");
                    this._RoleCode = StructuralObject.SetValidValue(value, false);
                    this.ReportPropertyChanged("RoleCode");
                }
            }
        }

        [EdmScalarProperty(EntityKeyProperty=true, IsNullable=false), DataMember]
        public string RoleGroupID
        {
            get
            {
                return this._RoleGroupID;
            }
            set
            {
                if (this._RoleGroupID != value)
                {
                    this.ReportPropertyChanging("RoleGroupID");
                    this._RoleGroupID = StructuralObject.SetValidValue(value, false);
                    this.ReportPropertyChanged("RoleGroupID");
                }
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=true, IsNullable=false)]
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

        [DataMember, EdmScalarProperty(EntityKeyProperty=true, IsNullable=false)]
        public string RoleName
        {
            get
            {
                return this._RoleName;
            }
            set
            {
                if (this._RoleName != value)
                {
                    this.ReportPropertyChanging("RoleName");
                    this._RoleName = StructuralObject.SetValidValue(value, false);
                    this.ReportPropertyChanged("RoleName");
                }
            }
        }

        [EdmScalarProperty(EntityKeyProperty=true, IsNullable=false), DataMember]
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

        [EdmScalarProperty(EntityKeyProperty=true, IsNullable=false), DataMember]
        public string UserType
        {
            get
            {
                return this._UserType;
            }
            set
            {
                if (this._UserType != value)
                {
                    this.ReportPropertyChanging("UserType");
                    this._UserType = StructuralObject.SetValidValue(value, false);
                    this.ReportPropertyChanged("UserType");
                }
            }
        }
    }
}

