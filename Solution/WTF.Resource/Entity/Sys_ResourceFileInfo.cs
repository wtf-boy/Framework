namespace WTF.Resource.Entity
{
    using System;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;

    [Serializable, EdmEntityType(NamespaceName="ResourceModel", Name="Sys_ResourceFileInfo"), DataContract(IsReference=true)]
    public class Sys_ResourceFileInfo : EntityObject
    {
        private string _ResourcePath;
        private string _ResourceVerID;

        public static Sys_ResourceFileInfo CreateSys_ResourceFileInfo(string resourceVerID, string resourcePath)
        {
            return new Sys_ResourceFileInfo { ResourceVerID = resourceVerID, ResourcePath = resourcePath };
        }

        [EdmScalarProperty(EntityKeyProperty=true, IsNullable=false), DataMember]
        public string ResourcePath
        {
            get
            {
                return this._ResourcePath;
            }
            set
            {
                if (this._ResourcePath != value)
                {
                    this.ReportPropertyChanging("ResourcePath");
                    this._ResourcePath = StructuralObject.SetValidValue(value, false);
                    this.ReportPropertyChanged("ResourcePath");
                }
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=true, IsNullable=false)]
        public string ResourceVerID
        {
            get
            {
                return this._ResourceVerID;
            }
            set
            {
                if (this._ResourceVerID != value)
                {
                    this.ReportPropertyChanging("ResourceVerID");
                    this._ResourceVerID = StructuralObject.SetValidValue(value, false);
                    this.ReportPropertyChanged("ResourceVerID");
                }
            }
        }
    }
}

