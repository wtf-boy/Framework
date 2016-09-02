namespace WTF.Power.Entity
{
    using System;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    [Serializable, DataContract(IsReference=true), EdmEntityType(NamespaceName="UserModel", Name="sys_authorizegroup")]
    public class sys_authorizegroup : EntityObject
    {
        private string _AuthorizeGroupID;
        private string _AuthorizeGroupName;
        private bool _IsAllowPowerSelf;
        private bool _IsRevertPower;
        private bool _IsSupertGroup;
        private string _ModuleTypeID;
        private string _Remark;

        public static sys_authorizegroup Createsys_authorizegroup(string authorizeGroupID, string authorizeGroupName, string moduleTypeID, bool isSupertGroup, string remark, bool isAllowPowerSelf, bool isRevertPower)
        {
            return new sys_authorizegroup { AuthorizeGroupID = authorizeGroupID, AuthorizeGroupName = authorizeGroupName, ModuleTypeID = moduleTypeID, IsSupertGroup = isSupertGroup, Remark = remark, IsAllowPowerSelf = isAllowPowerSelf, IsRevertPower = isRevertPower };
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

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=false)]
        public string AuthorizeGroupName
        {
            get
            {
                return this._AuthorizeGroupName;
            }
            set
            {
                this.ReportPropertyChanging("AuthorizeGroupName");
                this._AuthorizeGroupName = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("AuthorizeGroupName");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=false), DataMember]
        public bool IsAllowPowerSelf
        {
            get
            {
                return this._IsAllowPowerSelf;
            }
            set
            {
                this.ReportPropertyChanging("IsAllowPowerSelf");
                this._IsAllowPowerSelf = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("IsAllowPowerSelf");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=false)]
        public bool IsRevertPower
        {
            get
            {
                return this._IsRevertPower;
            }
            set
            {
                this.ReportPropertyChanging("IsRevertPower");
                this._IsRevertPower = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("IsRevertPower");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=false)]
        public bool IsSupertGroup
        {
            get
            {
                return this._IsSupertGroup;
            }
            set
            {
                this.ReportPropertyChanging("IsSupertGroup");
                this._IsSupertGroup = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("IsSupertGroup");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=false), DataMember]
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

        [SoapIgnore, DataMember, EdmRelationshipNavigationProperty("UserModel", "sys_authorizegrouppower_ibfk_1", "sys_authorizegrouppower"), XmlIgnore]
        public EntityCollection<WTF.Power.Entity.sys_authorizegrouppower> sys_authorizegrouppower
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<WTF.Power.Entity.sys_authorizegrouppower>("UserModel.sys_authorizegrouppower_ibfk_1", "sys_authorizegrouppower");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<WTF.Power.Entity.sys_authorizegrouppower>("UserModel.sys_authorizegrouppower_ibfk_1", "sys_authorizegrouppower", value);
                }
            }
        }
    }
}

