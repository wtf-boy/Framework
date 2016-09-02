namespace WTF.Resource.Entity
{
    using System;
    using System.ComponentModel;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    [Serializable, DataContract(IsReference=true), EdmEntityType(NamespaceName="ResourceModel", Name="Sys_ResourceData")]
    public class Sys_ResourceData : EntityObject
    {
        private string _FileExtension;
        private byte[] _ResourceData;
        private string _ResourceVerID;

        public static Sys_ResourceData CreateSys_ResourceData(string resourceVerID, byte[] resourceData, string fileExtension)
        {
            return new Sys_ResourceData { ResourceVerID = resourceVerID, ResourceData = resourceData, FileExtension = fileExtension };
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=false), DataMember]
        public string FileExtension
        {
            get
            {
                return this._FileExtension;
            }
            set
            {
                this.ReportPropertyChanging("FileExtension");
                this._FileExtension = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("FileExtension");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=false)]
        public byte[] ResourceData
        {
            get
            {
                return StructuralObject.GetValidValue(this._ResourceData);
            }
            set
            {
                this.ReportPropertyChanging("ResourceData");
                this._ResourceData = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("ResourceData");
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

        [SoapIgnore, XmlIgnore, EdmRelationshipNavigationProperty("ResourceModel", "FK_sys_resourcedata", "sys_resourcever"), DataMember]
        public WTF.Resource.Entity.Sys_ResourceVer Sys_ResourceVer
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<WTF.Resource.Entity.Sys_ResourceVer>("ResourceModel.FK_sys_resourcedata", "sys_resourcever").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<WTF.Resource.Entity.Sys_ResourceVer>("ResourceModel.FK_sys_resourcedata", "sys_resourcever").Value = value;
            }
        }

        [Browsable(false), DataMember]
        public EntityReference<WTF.Resource.Entity.Sys_ResourceVer> Sys_ResourceVerReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<WTF.Resource.Entity.Sys_ResourceVer>("ResourceModel.FK_sys_resourcedata", "sys_resourcever");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<WTF.Resource.Entity.Sys_ResourceVer>("ResourceModel.FK_sys_resourcedata", "sys_resourcever", value);
                }
            }
        }
    }
}

