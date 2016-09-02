namespace WTF.Power.Entity
{
    using System;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;

    [Serializable, DataContract(IsReference=true), EdmEntityType(NamespaceName="ModuleModel", Name="Sys_ModuleData")]
    public class Sys_ModuleData : EntityObject
    {
        private string _ConnectionKey;
        private string _DataName;
        private string _DataSelect;
        private string _FieldName;
        private int _FieldSourceType;
        private int _FieldType;
        private string _ModuleDataID;
        private string _ModuleID;
        private int _PowerType;

        public static Sys_ModuleData CreateSys_ModuleData(string moduleDataID, string moduleID, string connectionKey, string dataName, string fieldName, string dataSelect, int powerType, int fieldType, int fieldSourceType)
        {
            return new Sys_ModuleData { ModuleDataID = moduleDataID, ModuleID = moduleID, ConnectionKey = connectionKey, DataName = dataName, FieldName = fieldName, DataSelect = dataSelect, PowerType = powerType, FieldType = fieldType, FieldSourceType = fieldSourceType };
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=false), DataMember]
        public string ConnectionKey
        {
            get
            {
                return this._ConnectionKey;
            }
            set
            {
                this.ReportPropertyChanging("ConnectionKey");
                this._ConnectionKey = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("ConnectionKey");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=false)]
        public string DataName
        {
            get
            {
                return this._DataName;
            }
            set
            {
                this.ReportPropertyChanging("DataName");
                this._DataName = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("DataName");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=false), DataMember]
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

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=false)]
        public string FieldName
        {
            get
            {
                return this._FieldName;
            }
            set
            {
                this.ReportPropertyChanging("FieldName");
                this._FieldName = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("FieldName");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=false)]
        public int FieldSourceType
        {
            get
            {
                return this._FieldSourceType;
            }
            set
            {
                this.ReportPropertyChanging("FieldSourceType");
                this._FieldSourceType = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("FieldSourceType");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=false)]
        public int FieldType
        {
            get
            {
                return this._FieldType;
            }
            set
            {
                this.ReportPropertyChanging("FieldType");
                this._FieldType = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("FieldType");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=true, IsNullable=false)]
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

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=false), DataMember]
        public int PowerType
        {
            get
            {
                return this._PowerType;
            }
            set
            {
                this.ReportPropertyChanging("PowerType");
                this._PowerType = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("PowerType");
            }
        }
    }
}

