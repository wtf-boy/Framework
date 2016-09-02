namespace WTF.Resource.Entity
{
    using System;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    [Serializable, DataContract(IsReference=true), EdmEntityType(NamespaceName="FileResourceModel", Name="resource_fileresource")]
    public class resource_fileresource : EntityObject
    {
        private DateTime _CreateDate;
        private string _FileResourceCode;
        private string _FileResourceID;
        private string _FileResourceName;

        public static resource_fileresource Createresource_fileresource(string fileResourceID, string fileResourceName, string fileResourceCode, DateTime createDate)
        {
            return new resource_fileresource { FileResourceID = fileResourceID, FileResourceName = fileResourceName, FileResourceCode = fileResourceCode, CreateDate = createDate };
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

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=false)]
        public string FileResourceCode
        {
            get
            {
                return this._FileResourceCode;
            }
            set
            {
                this.ReportPropertyChanging("FileResourceCode");
                this._FileResourceCode = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("FileResourceCode");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=true, IsNullable=false)]
        public string FileResourceID
        {
            get
            {
                return this._FileResourceID;
            }
            set
            {
                if (this._FileResourceID != value)
                {
                    this.ReportPropertyChanging("FileResourceID");
                    this._FileResourceID = StructuralObject.SetValidValue(value, false);
                    this.ReportPropertyChanged("FileResourceID");
                }
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=false)]
        public string FileResourceName
        {
            get
            {
                return this._FileResourceName;
            }
            set
            {
                this.ReportPropertyChanging("FileResourceName");
                this._FileResourceName = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("FileResourceName");
            }
        }

        [XmlIgnore, SoapIgnore, EdmRelationshipNavigationProperty("FileResourceModel", "FK_FileRestrict_Ref_FileResource", "resource_filerestrict"), DataMember]
        public EntityCollection<WTF.Resource.Entity.resource_filerestrict> resource_filerestrict
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<WTF.Resource.Entity.resource_filerestrict>("FileResourceModel.FK_FileRestrict_Ref_FileResource", "resource_filerestrict");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<WTF.Resource.Entity.resource_filerestrict>("FileResourceModel.FK_FileRestrict_Ref_FileResource", "resource_filerestrict", value);
                }
            }
        }
    }
}

