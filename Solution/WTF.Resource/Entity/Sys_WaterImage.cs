namespace WTF.Resource.Entity
{
    using System;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;

    [Serializable, DataContract(IsReference=true), EdmEntityType(NamespaceName="ResourceModel", Name="Sys_WaterImage")]
    public class Sys_WaterImage : EntityObject
    {
        private string _WaterImageID;
        private string _WaterImagePath;
        private string _WaterName;

        public static Sys_WaterImage CreateSys_WaterImage(string waterImageID, string waterImagePath, string waterName)
        {
            return new Sys_WaterImage { WaterImageID = waterImageID, WaterImagePath = waterImagePath, WaterName = waterName };
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=true, IsNullable=false)]
        public string WaterImageID
        {
            get
            {
                return this._WaterImageID;
            }
            set
            {
                if (this._WaterImageID != value)
                {
                    this.ReportPropertyChanging("WaterImageID");
                    this._WaterImageID = StructuralObject.SetValidValue(value, false);
                    this.ReportPropertyChanged("WaterImageID");
                }
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=false), DataMember]
        public string WaterImagePath
        {
            get
            {
                return this._WaterImagePath;
            }
            set
            {
                this.ReportPropertyChanging("WaterImagePath");
                this._WaterImagePath = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("WaterImagePath");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=false)]
        public string WaterName
        {
            get
            {
                return this._WaterName;
            }
            set
            {
                this.ReportPropertyChanging("WaterName");
                this._WaterName = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("WaterName");
            }
        }
    }
}

