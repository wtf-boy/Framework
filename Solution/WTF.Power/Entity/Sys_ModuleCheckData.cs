namespace WTF.Power.Entity
{
    using System;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;

    [Serializable, EdmEntityType(NamespaceName="ModuleModel", Name="Sys_ModuleCheckData"), DataContract(IsReference=true)]
    public class Sys_ModuleCheckData : EntityObject
    {
        private string _ModuleCheckDataID;
        private string _ModuleDataID;
        private string _ModuleID;

        public static Sys_ModuleCheckData CreateSys_ModuleCheckData(string moduleCheckDataID, string moduleID, string moduleDataID)
        {
            return new Sys_ModuleCheckData { ModuleCheckDataID = moduleCheckDataID, ModuleID = moduleID, ModuleDataID = moduleDataID };
        }

        [EdmScalarProperty(EntityKeyProperty=true, IsNullable=false), DataMember]
        public string ModuleCheckDataID
        {
            get
            {
                return this._ModuleCheckDataID;
            }
            set
            {
                if (this._ModuleCheckDataID != value)
                {
                    this.ReportPropertyChanging("ModuleCheckDataID");
                    this._ModuleCheckDataID = StructuralObject.SetValidValue(value, false);
                    this.ReportPropertyChanged("ModuleCheckDataID");
                }
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=false)]
        public string ModuleDataID
        {
            get
            {
                return this._ModuleDataID;
            }
            set
            {
                this.ReportPropertyChanging("ModuleDataID");
                this._ModuleDataID = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("ModuleDataID");
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
    }
}

