namespace WTF.Power.Entity
{
    using System;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;

    [Serializable, DataContract(IsReference=true), EdmEntityType(NamespaceName="UserModel", Name="Sys_RoleData_Info")]
    public class Sys_RoleData_Info : EntityObject
    {
        private string _DataSelect;
        private string _FieldName;
        private int _FieldType;
        private string _ModuleDataID;
        private string _ModuleID;
        private string _RoleDataID;
        private string _RoleID;

        public static Sys_RoleData_Info CreateSys_RoleData_Info(string roleID, string roleDataID, string dataSelect, string moduleDataID, string fieldName, int fieldType, string moduleID)
        {
            return new Sys_RoleData_Info { RoleID = roleID, RoleDataID = roleDataID, DataSelect = dataSelect, ModuleDataID = moduleDataID, FieldName = fieldName, FieldType = fieldType, ModuleID = moduleID };
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=true, IsNullable=false)]
        public string DataSelect
        {
            get
            {
                return this._DataSelect;
            }
            set
            {
                if (this._DataSelect != value)
                {
                    this.ReportPropertyChanging("DataSelect");
                    this._DataSelect = StructuralObject.SetValidValue(value, false);
                    this.ReportPropertyChanged("DataSelect");
                }
            }
        }

        [EdmScalarProperty(EntityKeyProperty=true, IsNullable=false), DataMember]
        public string FieldName
        {
            get
            {
                return this._FieldName;
            }
            set
            {
                if (this._FieldName != value)
                {
                    this.ReportPropertyChanging("FieldName");
                    this._FieldName = StructuralObject.SetValidValue(value, false);
                    this.ReportPropertyChanged("FieldName");
                }
            }
        }

        [EdmScalarProperty(EntityKeyProperty=true, IsNullable=false), DataMember]
        public int FieldType
        {
            get
            {
                return this._FieldType;
            }
            set
            {
                if (this._FieldType != value)
                {
                    this.ReportPropertyChanging("FieldType");
                    this._FieldType = StructuralObject.SetValidValue(value);
                    this.ReportPropertyChanged("FieldType");
                }
            }
        }

        [EdmScalarProperty(EntityKeyProperty=true, IsNullable=false), DataMember]
        public string ModuleDataID
        {
            get
            {
                return this._ModuleDataID;
            }
            set
            {
                if (this._ModuleDataID != value)
                {
                    this.ReportPropertyChanging("ModuleDataID");
                    this._ModuleDataID = StructuralObject.SetValidValue(value, false);
                    this.ReportPropertyChanged("ModuleDataID");
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

        [EdmScalarProperty(EntityKeyProperty=true, IsNullable=false), DataMember]
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

