namespace WTF.Power.Entity
{
    using System;
    using System.ComponentModel;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    [Serializable, EdmEntityType(NamespaceName="UserModel", Name="Sys_RoleCotePower"), DataContract(IsReference=true)]
    public class Sys_RoleCotePower : EntityObject
    {
        private string _ModuleID;
        private string _RoleCoteID;
        private string _RoleCotePowerID;

        public static Sys_RoleCotePower CreateSys_RoleCotePower(string roleCotePowerID, string roleCoteID, string moduleID)
        {
            return new Sys_RoleCotePower { RoleCotePowerID = roleCotePowerID, RoleCoteID = roleCoteID, ModuleID = moduleID };
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=false), DataMember]
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
        public string RoleCoteID
        {
            get
            {
                return this._RoleCoteID;
            }
            set
            {
                this.ReportPropertyChanging("RoleCoteID");
                this._RoleCoteID = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("RoleCoteID");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=true, IsNullable=false)]
        public string RoleCotePowerID
        {
            get
            {
                return this._RoleCotePowerID;
            }
            set
            {
                if (this._RoleCotePowerID != value)
                {
                    this.ReportPropertyChanging("RoleCotePowerID");
                    this._RoleCotePowerID = StructuralObject.SetValidValue(value, false);
                    this.ReportPropertyChanged("RoleCotePowerID");
                }
            }
        }

        [SoapIgnore, XmlIgnore, EdmRelationshipNavigationProperty("UserModel", "FK_RoleCotePower_Ref_RoleCote", "sys_rolecote"), DataMember]
        public WTF.Power.Entity.Sys_RoleCote Sys_RoleCote
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<WTF.Power.Entity.Sys_RoleCote>("UserModel.FK_RoleCotePower_Ref_RoleCote", "sys_rolecote").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<WTF.Power.Entity.Sys_RoleCote>("UserModel.FK_RoleCotePower_Ref_RoleCote", "sys_rolecote").Value = value;
            }
        }

        [DataMember, Browsable(false)]
        public EntityReference<WTF.Power.Entity.Sys_RoleCote> Sys_RoleCoteReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<WTF.Power.Entity.Sys_RoleCote>("UserModel.FK_RoleCotePower_Ref_RoleCote", "sys_rolecote");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<WTF.Power.Entity.Sys_RoleCote>("UserModel.FK_RoleCotePower_Ref_RoleCote", "sys_rolecote", value);
                }
            }
        }
    }
}

