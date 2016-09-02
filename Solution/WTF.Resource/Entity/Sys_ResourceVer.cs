namespace WTF.Resource.Entity
{
    using System;
    using System.ComponentModel;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    [Serializable, DataContract(IsReference=true), EdmEntityType(NamespaceName="ResourceModel", Name="Sys_ResourceVer")]
    public class Sys_ResourceVer : EntityObject
    {
        private string _Account;
        private string _ContentType;
        private DateTime _CreateDateTime;
        private string _DictionaryPath;
        private int? _RefCount;
        private string _Remark;
        private string _ResourceFileName;
        private string _ResourceGUIDFileName;
        private string _ResourceID;
        private string _ResourcePath;
        private int _ResourceSize;
        private string _ResourceVerID;
        private DateTime _UpdateDateTime;
        private int _VerNo;

        public static Sys_ResourceVer CreateSys_ResourceVer(string resourceVerID, string resourceID, string resourceFileName, int resourceSize, string contentType, DateTime createDateTime, DateTime updateDateTime, string account, string dictionaryPath, string resourcePath, int verNo)
        {
            return new Sys_ResourceVer { ResourceVerID = resourceVerID, ResourceID = resourceID, ResourceFileName = resourceFileName, ResourceSize = resourceSize, ContentType = contentType, CreateDateTime = createDateTime, UpdateDateTime = updateDateTime, Account = account, DictionaryPath = dictionaryPath, ResourcePath = resourcePath, VerNo = verNo };
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

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=false), DataMember]
        public string ContentType
        {
            get
            {
                return this._ContentType;
            }
            set
            {
                this.ReportPropertyChanging("ContentType");
                this._ContentType = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("ContentType");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=false)]
        public DateTime CreateDateTime
        {
            get
            {
                return this._CreateDateTime;
            }
            set
            {
                this.ReportPropertyChanging("CreateDateTime");
                this._CreateDateTime = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("CreateDateTime");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=false)]
        public string DictionaryPath
        {
            get
            {
                return this._DictionaryPath;
            }
            set
            {
                this.ReportPropertyChanging("DictionaryPath");
                this._DictionaryPath = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("DictionaryPath");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public int? RefCount
        {
            get
            {
                return this._RefCount;
            }
            set
            {
                this.ReportPropertyChanging("RefCount");
                this._RefCount = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("RefCount");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public string Remark
        {
            get
            {
                return this._Remark;
            }
            set
            {
                this.ReportPropertyChanging("Remark");
                this._Remark = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("Remark");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=false)]
        public string ResourceFileName
        {
            get
            {
                return this._ResourceFileName;
            }
            set
            {
                this.ReportPropertyChanging("ResourceFileName");
                this._ResourceFileName = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("ResourceFileName");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public string ResourceGUIDFileName
        {
            get
            {
                return this._ResourceGUIDFileName;
            }
            set
            {
                this.ReportPropertyChanging("ResourceGUIDFileName");
                this._ResourceGUIDFileName = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("ResourceGUIDFileName");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=false), DataMember]
        public string ResourceID
        {
            get
            {
                return this._ResourceID;
            }
            set
            {
                this.ReportPropertyChanging("ResourceID");
                this._ResourceID = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("ResourceID");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=false), DataMember]
        public string ResourcePath
        {
            get
            {
                return this._ResourcePath;
            }
            set
            {
                this.ReportPropertyChanging("ResourcePath");
                this._ResourcePath = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("ResourcePath");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=false)]
        public int ResourceSize
        {
            get
            {
                return this._ResourceSize;
            }
            set
            {
                this.ReportPropertyChanging("ResourceSize");
                this._ResourceSize = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("ResourceSize");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=true, IsNullable=false), DataMember]
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

        [EdmRelationshipNavigationProperty("ResourceModel", "FK_sys_resourcever", "sys_resource"), XmlIgnore, SoapIgnore, DataMember]
        public WTF.Resource.Entity.Sys_Resource Sys_Resource
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<WTF.Resource.Entity.Sys_Resource>("ResourceModel.FK_sys_resourcever", "sys_resource").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<WTF.Resource.Entity.Sys_Resource>("ResourceModel.FK_sys_resourcever", "sys_resource").Value = value;
            }
        }

        [XmlIgnore, SoapIgnore, EdmRelationshipNavigationProperty("ResourceModel", "FK_sys_resourcedata", "sys_resourcedata"), DataMember]
        public WTF.Resource.Entity.Sys_ResourceData Sys_ResourceData
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<WTF.Resource.Entity.Sys_ResourceData>("ResourceModel.FK_sys_resourcedata", "sys_resourcedata").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<WTF.Resource.Entity.Sys_ResourceData>("ResourceModel.FK_sys_resourcedata", "sys_resourcedata").Value = value;
            }
        }

        [Browsable(false), DataMember]
        public EntityReference<WTF.Resource.Entity.Sys_ResourceData> Sys_ResourceDataReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<WTF.Resource.Entity.Sys_ResourceData>("ResourceModel.FK_sys_resourcedata", "sys_resourcedata");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<WTF.Resource.Entity.Sys_ResourceData>("ResourceModel.FK_sys_resourcedata", "sys_resourcedata", value);
                }
            }
        }

        [Browsable(false), DataMember]
        public EntityReference<WTF.Resource.Entity.Sys_Resource> Sys_ResourceReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<WTF.Resource.Entity.Sys_Resource>("ResourceModel.FK_sys_resourcever", "sys_resource");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<WTF.Resource.Entity.Sys_Resource>("ResourceModel.FK_sys_resourcever", "sys_resource", value);
                }
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=false), DataMember]
        public DateTime UpdateDateTime
        {
            get
            {
                return this._UpdateDateTime;
            }
            set
            {
                this.ReportPropertyChanging("UpdateDateTime");
                this._UpdateDateTime = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("UpdateDateTime");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=false)]
        public int VerNo
        {
            get
            {
                return this._VerNo;
            }
            set
            {
                this.ReportPropertyChanging("VerNo");
                this._VerNo = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("VerNo");
            }
        }
    }
}

