namespace WTF.Resource.Entity
{
    using System;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    [Serializable, EdmEntityType(NamespaceName="FileResourceModel", Name="resource_filestoragepath"), DataContract(IsReference=true)]
    public class resource_filestoragepath : EntityObject
    {
        private string _Account;
        private string _FileStoragePathID;
        private string _IPAddress;
        private string _Password;
        private string _Port;
        private string _StorageConfig;
        private string _StoragePath;
        private string _StoragePathName;
        private int _StorageTypeID;
        private string _VirtualName;

        public static resource_filestoragepath Createresource_filestoragepath(string fileStoragePathID, string storagePathName, int storageTypeID, string virtualName, string storagePath, string iPAddress, string account, string password, string port, string storageConfig)
        {
            return new resource_filestoragepath { FileStoragePathID = fileStoragePathID, StoragePathName = storagePathName, StorageTypeID = storageTypeID, VirtualName = virtualName, StoragePath = storagePath, IPAddress = iPAddress, Account = account, Password = password, Port = port, StorageConfig = storageConfig };
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=false), DataMember]
        public string Account
        {
            get
            {
                return this._Account;
            }
            set
            {
                this.ReportPropertyChanging("Account");
                this._Account = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("Account");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=true, IsNullable=false), DataMember]
        public string FileStoragePathID
        {
            get
            {
                return this._FileStoragePathID;
            }
            set
            {
                if (this._FileStoragePathID != value)
                {
                    this.ReportPropertyChanging("FileStoragePathID");
                    this._FileStoragePathID = StructuralObject.SetValidValue(value, false);
                    this.ReportPropertyChanged("FileStoragePathID");
                }
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=false), DataMember]
        public string IPAddress
        {
            get
            {
                return this._IPAddress;
            }
            set
            {
                this.ReportPropertyChanging("IPAddress");
                this._IPAddress = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("IPAddress");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=false)]
        public string Password
        {
            get
            {
                return this._Password;
            }
            set
            {
                this.ReportPropertyChanging("Password");
                this._Password = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("Password");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=false), DataMember]
        public string Port
        {
            get
            {
                return this._Port;
            }
            set
            {
                this.ReportPropertyChanging("Port");
                this._Port = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("Port");
            }
        }

        [DataMember, EdmRelationshipNavigationProperty("FileResourceModel", "FK_FileRestrict_Ref_StoragePath", "resource_filerestrict"), XmlIgnore, SoapIgnore]
        public EntityCollection<WTF.Resource.Entity.resource_filerestrict> resource_filerestrict
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<WTF.Resource.Entity.resource_filerestrict>("FileResourceModel.FK_FileRestrict_Ref_StoragePath", "resource_filerestrict");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<WTF.Resource.Entity.resource_filerestrict>("FileResourceModel.FK_FileRestrict_Ref_StoragePath", "resource_filerestrict", value);
                }
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=false), DataMember]
        public string StorageConfig
        {
            get
            {
                return this._StorageConfig;
            }
            set
            {
                this.ReportPropertyChanging("StorageConfig");
                this._StorageConfig = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("StorageConfig");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=false), DataMember]
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
        public string StoragePathName
        {
            get
            {
                return this._StoragePathName;
            }
            set
            {
                this.ReportPropertyChanging("StoragePathName");
                this._StoragePathName = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("StoragePathName");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=false)]
        public int StorageTypeID
        {
            get
            {
                return this._StorageTypeID;
            }
            set
            {
                this.ReportPropertyChanging("StorageTypeID");
                this._StorageTypeID = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("StorageTypeID");
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

