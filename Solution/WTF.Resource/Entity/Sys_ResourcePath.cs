namespace WTF.Resource.Entity
{
    using System;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;

    [Serializable, EdmEntityType(NamespaceName="ResourceModel", Name="Sys_ResourcePath"), DataContract(IsReference=true)]
    public class Sys_ResourcePath : EntityObject
    {
        private string _ResourcePathID;
        private string _ResourcePathName;
        private string _StoragePath;
        private string _VirtualName;

        public static Sys_ResourcePath CreateSys_ResourcePath(string resourcePathID, string resourcePathName, string virtualName, string storagePath)
        {
            return new Sys_ResourcePath { ResourcePathID = resourcePathID, ResourcePathName = resourcePathName, VirtualName = virtualName, StoragePath = storagePath };
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=true, IsNullable=false)]
        public string ResourcePathID
        {
            get
            {
                return this._ResourcePathID;
            }
            set
            {
                if (this._ResourcePathID != value)
                {
                    this.ReportPropertyChanging("ResourcePathID");
                    this._ResourcePathID = StructuralObject.SetValidValue(value, false);
                    this.ReportPropertyChanged("ResourcePathID");
                }
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=false)]
        public string ResourcePathName
        {
            get
            {
                return this._ResourcePathName;
            }
            set
            {
                this.ReportPropertyChanging("ResourcePathName");
                this._ResourcePathName = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("ResourcePathName");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=false)]
        public string StoragePath
        {
            get
            {
                return this._StoragePath;
            }
            set
            {
                this.ReportPropertyChanging("StoragePath");
                this._StoragePath = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("StoragePath");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=false), DataMember]
        public string VirtualName
        {
            get
            {
                return this._VirtualName;
            }
            set
            {
                this.ReportPropertyChanging("VirtualName");
                this._VirtualName = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("VirtualName");
            }
        }
    }
}

