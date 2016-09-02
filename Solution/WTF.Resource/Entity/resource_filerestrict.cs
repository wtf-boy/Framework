namespace WTF.Resource.Entity
{
    using System;
    using System.ComponentModel;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    [Serializable, EdmEntityType(NamespaceName="FileResourceModel", Name="resource_filerestrict"), DataContract(IsReference=true)]
    public class resource_filerestrict : EntityObject
    {
        private int _AccessModeCodeType;
        private string _FileExtension;
        private int _FileMaxSize;
        private string _FileResourceID;
        private string _FileRestrictID;
        private string _FileStoragePathID;
        private int _FileType;
        private int? _IsHistory;
        private int _IsMd5;
        private int _IsReturnSize;
        private int _PathFormatCodeType;
        private string _RestrictCode;
        private string _RestrictName;

        public static resource_filerestrict Createresource_filerestrict(string fileRestrictID, string fileStoragePathID, string fileResourceID, int accessModeCodeType, int pathFormatCodeType, string restrictName, string restrictCode, int fileType, string fileExtension, int fileMaxSize, int isReturnSize, int isMd5)
        {
            return new resource_filerestrict { FileRestrictID = fileRestrictID, FileStoragePathID = fileStoragePathID, FileResourceID = fileResourceID, AccessModeCodeType = accessModeCodeType, PathFormatCodeType = pathFormatCodeType, RestrictName = restrictName, RestrictCode = restrictCode, FileType = fileType, FileExtension = fileExtension, FileMaxSize = fileMaxSize, IsReturnSize = isReturnSize, IsMd5 = isMd5 };
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

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=false)]
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
        public int FileMaxSize
        {
            get
            {
                return this._FileMaxSize;
            }
            set
            {
                this.ReportPropertyChanging("FileMaxSize");
                this._FileMaxSize = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("FileMaxSize");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=false), DataMember]
        public string FileResourceID
        {
            get
            {
                return this._FileResourceID;
            }
            set
            {
                this.ReportPropertyChanging("FileResourceID");
                this._FileResourceID = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("FileResourceID");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=true, IsNullable=false), DataMember]
        public string FileRestrictID
        {
            get
            {
                return this._FileRestrictID;
            }
            set
            {
                if (this._FileRestrictID != value)
                {
                    this.ReportPropertyChanging("FileRestrictID");
                    this._FileRestrictID = StructuralObject.SetValidValue(value, false);
                    this.ReportPropertyChanged("FileRestrictID");
                }
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=false), DataMember]
        public string FileStoragePathID
        {
            get
            {
                return this._FileStoragePathID;
            }
            set
            {
                this.ReportPropertyChanging("FileStoragePathID");
                this._FileStoragePathID = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("FileStoragePathID");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=false), DataMember]
        public int FileType
        {
            get
            {
                return this._FileType;
            }
            set
            {
                this.ReportPropertyChanging("FileType");
                this._FileType = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("FileType");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public int? IsHistory
        {
            get
            {
                return this._IsHistory;
            }
            set
            {
                this.ReportPropertyChanging("IsHistory");
                this._IsHistory = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("IsHistory");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=false)]
        public int IsMd5
        {
            get
            {
                return this._IsMd5;
            }
            set
            {
                this.ReportPropertyChanging("IsMd5");
                this._IsMd5 = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("IsMd5");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=false), DataMember]
        public int IsReturnSize
        {
            get
            {
                return this._IsReturnSize;
            }
            set
            {
                this.ReportPropertyChanging("IsReturnSize");
                this._IsReturnSize = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("IsReturnSize");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=false)]
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

        [XmlIgnore, DataMember, SoapIgnore, EdmRelationshipNavigationProperty("FileResourceModel", "FK_FileRestrict_Ref_FileResource", "resource_fileresource")]
        public WTF.Resource.Entity.resource_fileresource resource_fileresource
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<WTF.Resource.Entity.resource_fileresource>("FileResourceModel.FK_FileRestrict_Ref_FileResource", "resource_fileresource").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<WTF.Resource.Entity.resource_fileresource>("FileResourceModel.FK_FileRestrict_Ref_FileResource", "resource_fileresource").Value = value;
            }
        }

        [Browsable(false), DataMember]
        public EntityReference<WTF.Resource.Entity.resource_fileresource> resource_fileresourceReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<WTF.Resource.Entity.resource_fileresource>("FileResourceModel.FK_FileRestrict_Ref_FileResource", "resource_fileresource");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<WTF.Resource.Entity.resource_fileresource>("FileResourceModel.FK_FileRestrict_Ref_FileResource", "resource_fileresource", value);
                }
            }
        }

        [SoapIgnore, DataMember, XmlIgnore, EdmRelationshipNavigationProperty("FileResourceModel", "FK_FileRestrictPic_Ref_FileRestrict", "resource_filerestrictpic")]
        public EntityCollection<WTF.Resource.Entity.resource_filerestrictpic> resource_filerestrictpic
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<WTF.Resource.Entity.resource_filerestrictpic>("FileResourceModel.FK_FileRestrictPic_Ref_FileRestrict", "resource_filerestrictpic");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<WTF.Resource.Entity.resource_filerestrictpic>("FileResourceModel.FK_FileRestrictPic_Ref_FileRestrict", "resource_filerestrictpic", value);
                }
            }
        }

        [SoapIgnore, DataMember, EdmRelationshipNavigationProperty("FileResourceModel", "FK_FileRestrict_Ref_StoragePath", "resource_filestoragepath"), XmlIgnore]
        public WTF.Resource.Entity.resource_filestoragepath resource_filestoragepath
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<WTF.Resource.Entity.resource_filestoragepath>("FileResourceModel.FK_FileRestrict_Ref_StoragePath", "resource_filestoragepath").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<WTF.Resource.Entity.resource_filestoragepath>("FileResourceModel.FK_FileRestrict_Ref_StoragePath", "resource_filestoragepath").Value = value;
            }
        }

        [Browsable(false), DataMember]
        public EntityReference<WTF.Resource.Entity.resource_filestoragepath> resource_filestoragepathReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<WTF.Resource.Entity.resource_filestoragepath>("FileResourceModel.FK_FileRestrict_Ref_StoragePath", "resource_filestoragepath");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<WTF.Resource.Entity.resource_filestoragepath>("FileResourceModel.FK_FileRestrict_Ref_StoragePath", "resource_filestoragepath", value);
                }
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=false), DataMember]
        public string RestrictCode
        {
            get
            {
                return this._RestrictCode;
            }
            set
            {
                this.ReportPropertyChanging("RestrictCode");
                this._RestrictCode = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("RestrictCode");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=false)]
        public string RestrictName
        {
            get
            {
                return this._RestrictName;
            }
            set
            {
                this.ReportPropertyChanging("RestrictName");
                this._RestrictName = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("RestrictName");
            }
        }
    }
}

