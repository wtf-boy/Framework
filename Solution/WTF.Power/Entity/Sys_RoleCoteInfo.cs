namespace WTF.Power.Entity
{
    using System;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;

    [Serializable, DataContract(IsReference=true), EdmEntityType(NamespaceName="UserModel", Name="Sys_RoleCoteInfo")]
    public class Sys_RoleCoteInfo : EntityObject
    {
        private string _CoteID;
        private string _CoteModuleID;
        private string _ModuleID;
        private string _RoleCotePowerID;
        private string _RoleID;

        public static Sys_RoleCoteInfo CreateSys_RoleCoteInfo(string roleCotePowerID, string moduleID, string roleID, string coteID, string coteModuleID)
        {
            return new Sys_RoleCoteInfo { RoleCotePowerID = roleCotePowerID, ModuleID = moduleID, RoleID = roleID, CoteID = coteID, CoteModuleID = coteModuleID };
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=true, IsNullable=false)]
        public string CoteID
        {
            get
            {
                return this._CoteID;
            }
            set
            {
                if (this._CoteID != value)
                {
                    this.ReportPropertyChanging("CoteID");
                    this._CoteID = StructuralObject.SetValidValue(value, false);
                    this.ReportPropertyChanged("CoteID");
                }
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=true, IsNullable=false)]
        public string CoteModuleID
        {
            get
            {
                return this._CoteModuleID;
            }
            set
            {
                if (this._CoteModuleID != value)
                {
                    this.ReportPropertyChanging("CoteModuleID");
                    this._CoteModuleID = StructuralObject.SetValidValue(value, false);
                    this.ReportPropertyChanged("CoteModuleID");
                }
            }
        }

        [EdmScalarProperty(EntityKeyProperty=true, IsNullable=false), DataMember]
        public string ModuleID
        {
            get
            {
                return this._ModuleID;
            }
            set
            {
                if (this._ModuleID != value)
                {
                    this.ReportPropertyChanging("ModuleID");
                    this._ModuleID = StructuralObject.SetValidValue(value, false);
                    this.ReportPropertyChanged("ModuleID");
                }
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
    }
}

