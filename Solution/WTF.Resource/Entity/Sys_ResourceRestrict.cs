namespace WTF.Resource.Entity
{
    using System;
    using System.ComponentModel;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    [Serializable, EdmEntityType(NamespaceName="ResourceModel", Name="Sys_ResourceRestrict"), DataContract(IsReference=true)]
    public class Sys_ResourceRestrict : EntityObject
    {
        private int _BeginVerNo;
        private int _EndVerNo;
        private string _FileExtension;
        private int _FileMaxSize;
        private int _ResourceRestrictID;
        private int _ResourceTypeID;
        private string _RestrictCode;
        private string _RestrictName;
        private int _RestrictType;
        private int _VerNo;

        public static Sys_ResourceRestrict CreateSys_ResourceRestrict(int resourceRestrictID, int resourceTypeID, string restrictCode, string restrictName, int restrictType, string fileExtension, int fileMaxSize, int verNo, int beginVerNo, int endVerNo)
        {
            return new Sys_ResourceRestrict { ResourceRestrictID = resourceRestrictID, ResourceTypeID = resourceTypeID, RestrictCode = restrictCode, RestrictName = restrictName, RestrictType = restrictType, FileExtension = fileExtension, FileMaxSize = fileMaxSize, VerNo = verNo, BeginVerNo = beginVerNo, EndVerNo = endVerNo };
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=false), DataMember]
        public int BeginVerNo
        {
            get
            {
                return this._BeginVerNo;
            }
            set
            {
                this.ReportPropertyChanging("BeginVerNo");
                this._BeginVerNo = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("BeginVerNo");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=false)]
        public int EndVerNo
        {
            get
            {
                return this._EndVerNo;
            }
            set
            {
                this.ReportPropertyChanging("EndVerNo");
                this._EndVerNo = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("EndVerNo");
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

        [EdmScalarProperty(EntityKeyProperty=true, IsNullable=false), DataMember]
        public int ResourceRestrictID
        {
            get
            {
                return this._ResourceRestrictID;
            }
            set
            {
                if (this._ResourceRestrictID != value)
                {
                    this.ReportPropertyChanging("ResourceRestrictID");
                    this._ResourceRestrictID = StructuralObject.SetValidValue(value);
                    this.ReportPropertyChanged("ResourceRestrictID");
                }
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

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=false)]
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

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=false), DataMember]
        public int RestrictType
        {
            get
            {
                return this._RestrictType;
            }
            set
            {
                this.ReportPropertyChanging("RestrictType");
                this._RestrictType = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("RestrictType");
            }
        }

        [EdmRelationshipNavigationProperty("ResourceModel", "FK_sys_resourcerestrictpic", "sys_resourcerestrictpic"), DataMember, XmlIgnore, SoapIgnore]
        public EntityCollection<WTF.Resource.Entity.Sys_ResourceRestrictPic> Sys_ResourceRestrictPic
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<WTF.Resource.Entity.Sys_ResourceRestrictPic>("ResourceModel.FK_sys_resourcerestrictpic", "sys_resourcerestrictpic");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<WTF.Resource.Entity.Sys_ResourceRestrictPic>("ResourceModel.FK_sys_resourcerestrictpic", "sys_resourcerestrictpic", value);
                }
            }
        }

        [DataMember, SoapIgnore, EdmRelationshipNavigationProperty("ResourceModel", "FK_sys_resourcerestrict", "sys_resourcetype"), XmlIgnore]
        public WTF.Resource.Entity.Sys_ResourceType Sys_ResourceType
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<WTF.Resource.Entity.Sys_ResourceType>("ResourceModel.FK_sys_resourcerestrict", "sys_resourcetype").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<WTF.Resource.Entity.Sys_ResourceType>("ResourceModel.FK_sys_resourcerestrict", "sys_resourcetype").Value = value;
            }
        }

        [Browsable(false), DataMember]
        public EntityReference<WTF.Resource.Entity.Sys_ResourceType> Sys_ResourceTypeReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<WTF.Resource.Entity.Sys_ResourceType>("ResourceModel.FK_sys_resourcerestrict", "sys_resourcetype");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<WTF.Resource.Entity.Sys_ResourceType>("ResourceModel.FK_sys_resourcerestrict", "sys_resourcetype", value);
                }
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=false), DataMember]
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

