namespace WTF.Power.Entity
{
    using System;
    using System.ComponentModel;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    [Serializable, DataContract(IsReference=true), EdmEntityType(NamespaceName="UserModel", Name="sys_authorizegrouppower")]
    public class sys_authorizegrouppower : EntityObject
    {
        private string _AuthorizeGroupID;
        private string _AuthorizeGroupPowerID;
        private string _CoteID;
        private string _CoteModuleID;
        private bool _IsCoteSupper;
        private bool _IsShare;
        private string _ModuleID;

        public static sys_authorizegrouppower Createsys_authorizegrouppower(string authorizeGroupPowerID, string authorizeGroupID, string moduleID, string coteID, string coteModuleID, bool isShare, bool isCoteSupper)
        {
            return new sys_authorizegrouppower { AuthorizeGroupPowerID = authorizeGroupPowerID, AuthorizeGroupID = authorizeGroupID, ModuleID = moduleID, CoteID = coteID, CoteModuleID = coteModuleID, IsShare = isShare, IsCoteSupper = isCoteSupper };
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

        [DataMember, EdmScalarProperty(EntityKeyProperty=true, IsNullable=false)]
        public string AuthorizeGroupPowerID
        {
            get
            {
                return this._AuthorizeGroupPowerID;
            }
            set
            {
                if (this._AuthorizeGroupPowerID != value)
                {
                    this.ReportPropertyChanging("AuthorizeGroupPowerID");
                    this._AuthorizeGroupPowerID = StructuralObject.SetValidValue(value, false);
                    this.ReportPropertyChanged("AuthorizeGroupPowerID");
                }
            }
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

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=false), DataMember]
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

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=false), DataMember]
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

        [SoapIgnore, XmlIgnore, DataMember, EdmRelationshipNavigationProperty("UserModel", "sys_authorizegrouppower_ibfk_1", "sys_authorizegroup")]
        public WTF.Power.Entity.sys_authorizegroup sys_authorizegroup
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<WTF.Power.Entity.sys_authorizegroup>("UserModel.sys_authorizegrouppower_ibfk_1", "sys_authorizegroup").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<WTF.Power.Entity.sys_authorizegroup>("UserModel.sys_authorizegrouppower_ibfk_1", "sys_authorizegroup").Value = value;
            }
        }

        [DataMember, Browsable(false)]
        public EntityReference<WTF.Power.Entity.sys_authorizegroup> sys_authorizegroupReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<WTF.Power.Entity.sys_authorizegroup>("UserModel.sys_authorizegrouppower_ibfk_1", "sys_authorizegroup");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<WTF.Power.Entity.sys_authorizegroup>("UserModel.sys_authorizegrouppower_ibfk_1", "sys_authorizegroup", value);
                }
            }
        }
    }
}

