namespace WTF.Resource.Entity
{
    using System;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    [Serializable, DataContract(IsReference=true), EdmEntityType(NamespaceName="ResourceModel", Name="Sys_ResourceType")]
    public class Sys_ResourceType : EntityObject
    {
        private int _AccessModeCodeType;
        private int _PathFormatCodeType;
        private string _ResourcePathID;
        private string _ResourceTypeCode;
        private int _ResourceTypeID;
        private string _ResourceTypeName;
        private int _StorageType;

        public static Sys_ResourceType CreateSys_ResourceType(int resourceTypeID, string resourceTypeName, string resourceTypeCode, int accessModeCodeType, int pathFormatCodeType, string resourcePathID, int storageType)
        {
            return new Sys_ResourceType { ResourceTypeID = resourceTypeID, ResourceTypeName = resourceTypeName, ResourceTypeCode = resourceTypeCode, AccessModeCodeType = accessModeCodeType, PathFormatCodeType = pathFormatCodeType, ResourcePathID = resourcePathID, StorageType = storageType };
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=false), DataMember]
        public int AccessModeCodeType
        {
            get
            {
                return this._AccessModeCodeType;
            }
            set
            {
                this.ReportPropertyChanging("AccessModeCodeType");
                this._AccessModeCodeType = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("AccessModeCodeType");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=false), DataMember]
        public int PathFormatCodeType
        {
            get
            {
                return this._PathFormatCodeType;
            }
            set
            {
                this.ReportPropertyChanging("PathFormatCodeType");
                this._PathFormatCodeType = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("PathFormatCodeType");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=false)]
        public string ResourcePathID
        {
            get
            {
                return this._ResourcePathID;
            }
            set
            {
                this.ReportPropertyChanging("ResourcePathID");
                this._ResourcePathID = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("ResourcePathID");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=false)]
        public string ResourceTypeCode
        {
            get
            {
                return this._ResourceTypeCode;
            }
            set
            {
                this.ReportPropertyChanging("ResourceTypeCode");
                this._ResourceTypeCode = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("ResourceTypeCode");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=true, IsNullable=false)]
        public int ResourceTypeID
        {
            get
            {
                return this._ResourceTypeID;
            }
            set
            {
                if (this._ResourceTypeID != value)
                {
                    this.ReportPropertyChanging("ResourceTypeID");
                    this._ResourceTypeID = StructuralObject.SetValidValue(value);
                    this.ReportPropertyChanged("ResourceTypeID");
                }
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=false)]
        public string ResourceTypeName
        {
            get
            {
                return this._ResourceTypeName;
            }
            set
            {
                this.ReportPropertyChanging("ResourceTypeName");
                this._ResourceTypeName = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("ResourceTypeName");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=false), DataMember]
        public int StorageType
        {
            get
            {
                return this._StorageType;
            }
            set
            {
                this.ReportPropertyChanging("StorageType");
                this._StorageType = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("StorageType");
            }
        }

        [DataMember, EdmRelationshipNavigationProperty("ResourceModel", "FK_sys_resource", "sys_resource"), XmlIgnore, SoapIgnore]
        public EntityCollection<WTF.Resource.Entity.Sys_Resource> Sys_Resource
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<WTF.Resource.Entity.Sys_Resource>("ResourceModel.FK_sys_resource", "sys_resource");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<WTF.Resource.Entity.Sys_Resource>("ResourceModel.FK_sys_resource", "sys_resource", value);
                }
            }
        }

        [XmlIgnore, SoapIgnore, DataMember, EdmRelationshipNavigationProperty("ResourceModel", "FK_sys_resourcerestrict", "sys_resourcerestrict")]
        public EntityCollection<WTF.Resource.Entity.Sys_ResourceRestrict> Sys_ResourceRestrict
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<WTF.Resource.Entity.Sys_ResourceRestrict>("ResourceModel.FK_sys_resourcerestrict", "sys_resourcerestrict");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<WTF.Resource.Entity.Sys_ResourceRestrict>("ResourceModel.FK_sys_resourcerestrict", "sys_resourcerestrict", value);
                }
            }
        }
    }
}

