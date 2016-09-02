namespace WTF.Resource.Entity
{
    using System;
    using System.ComponentModel;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    [Serializable, DataContract(IsReference=true), EdmEntityType(NamespaceName="ResourceModel", Name="Sys_Resource")]
    public class Sys_Resource : EntityObject
    {
        private DateTime _CreateDate;
        private string _ResourceID;
        private string _ResourceName;
        private int _ResourceTypeID;
        private int _VerCount;

        public static Sys_Resource CreateSys_Resource(string resourceID, string resourceName, int resourceTypeID, DateTime createDate, int verCount)
        {
            return new Sys_Resource { ResourceID = resourceID, ResourceName = resourceName, ResourceTypeID = resourceTypeID, CreateDate = createDate, VerCount = verCount };
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=false), DataMember]
        public DateTime CreateDate
        {
            get
            {
                return this._CreateDate;
            }
            set
            {
                this.ReportPropertyChanging("CreateDate");
                this._CreateDate = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("CreateDate");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=true, IsNullable=false)]
        public string ResourceID
        {
            get
            {
                return this._ResourceID;
            }
            set
            {
                if (this._ResourceID != value)
                {
                    this.ReportPropertyChanging("ResourceID");
                    this._ResourceID = StructuralObject.SetValidValue(value, false);
                    this.ReportPropertyChanged("ResourceID");
                }
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=false)]
        public string ResourceName
        {
            get
            {
                return this._ResourceName;
            }
            set
            {
                this.ReportPropertyChanging("ResourceName");
                this._ResourceName = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("ResourceName");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=false), DataMember]
        public int ResourceTypeID
        {
            get
            {
                return this._ResourceTypeID;
            }
            set
            {
                this.ReportPropertyChanging("ResourceTypeID");
                this._ResourceTypeID = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("ResourceTypeID");
            }
        }

        [SoapIgnore, XmlIgnore, DataMember, EdmRelationshipNavigationProperty("ResourceModel", "FK_sys_resource", "sys_resourcetype")]
        public WTF.Resource.Entity.Sys_ResourceType Sys_ResourceType
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<WTF.Resource.Entity.Sys_ResourceType>("ResourceModel.FK_sys_resource", "sys_resourcetype").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<WTF.Resource.Entity.Sys_ResourceType>("ResourceModel.FK_sys_resource", "sys_resourcetype").Value = value;
            }
        }

        [DataMember, Browsable(false)]
        public EntityReference<WTF.Resource.Entity.Sys_ResourceType> Sys_ResourceTypeReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<WTF.Resource.Entity.Sys_ResourceType>("ResourceModel.FK_sys_resource", "sys_resourcetype");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<WTF.Resource.Entity.Sys_ResourceType>("ResourceModel.FK_sys_resource", "sys_resourcetype", value);
                }
            }
        }

        [SoapIgnore, EdmRelationshipNavigationProperty("ResourceModel", "FK_sys_resourcever", "sys_resourcever"), XmlIgnore, DataMember]
        public EntityCollection<WTF.Resource.Entity.Sys_ResourceVer> Sys_ResourceVer
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<WTF.Resource.Entity.Sys_ResourceVer>("ResourceModel.FK_sys_resourcever", "sys_resourcever");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<WTF.Resource.Entity.Sys_ResourceVer>("ResourceModel.FK_sys_resourcever", "sys_resourcever", value);
                }
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=false)]
        public int VerCount
        {
            get
            {
                return this._VerCount;
            }
            set
            {
                this.ReportPropertyChanging("VerCount");
                this._VerCount = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("VerCount");
            }
        }
    }
}

