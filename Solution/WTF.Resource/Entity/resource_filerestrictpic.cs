namespace WTF.Resource.Entity
{
    using System;
    using System.ComponentModel;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    [Serializable, EdmEntityType(NamespaceName="FileResourceModel", Name="resource_filerestrictpic"), DataContract(IsReference=true)]
    public class resource_filerestrictpic : EntityObject
    {
        private string _FileRestrictID;
        private int _HorizontalAlign;
        private int _ImageHeight;
        private int _ImageWidth;
        private bool _IsCreateWaterMark;
        private int _SortIndex;
        private string _SystemFilePicID;
        private int _VerticalAlign;
        private string _WaterImageID;
        private string _WatermarkText;
        private int _WatermarkType;

        public static resource_filerestrictpic Createresource_filerestrictpic(string systemFilePicID, string fileRestrictID, int sortIndex, bool isCreateWaterMark, string watermarkText, int horizontalAlign, int verticalAlign, int imageWidth, int imageHeight, int watermarkType, string waterImageID)
        {
            return new resource_filerestrictpic { SystemFilePicID = systemFilePicID, FileRestrictID = fileRestrictID, SortIndex = sortIndex, IsCreateWaterMark = isCreateWaterMark, WatermarkText = watermarkText, HorizontalAlign = horizontalAlign, VerticalAlign = verticalAlign, ImageWidth = imageWidth, ImageHeight = imageHeight, WatermarkType = watermarkType, WaterImageID = waterImageID };
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=false), DataMember]
        public string FileRestrictID
        {
            get
            {
                return this._FileRestrictID;
            }
            set
            {
                this.ReportPropertyChanging("FileRestrictID");
                this._FileRestrictID = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("FileRestrictID");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=false)]
        public int HorizontalAlign
        {
            get
            {
                return this._HorizontalAlign;
            }
            set
            {
                this.ReportPropertyChanging("HorizontalAlign");
                this._HorizontalAlign = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("HorizontalAlign");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=false), DataMember]
        public int ImageHeight
        {
            get
            {
                return this._ImageHeight;
            }
            set
            {
                this.ReportPropertyChanging("ImageHeight");
                this._ImageHeight = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("ImageHeight");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=false)]
        public int ImageWidth
        {
            get
            {
                return this._ImageWidth;
            }
            set
            {
                this.ReportPropertyChanging("ImageWidth");
                this._ImageWidth = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("ImageWidth");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=false)]
        public bool IsCreateWaterMark
        {
            get
            {
                return this._IsCreateWaterMark;
            }
            set
            {
                this.ReportPropertyChanging("IsCreateWaterMark");
                this._IsCreateWaterMark = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("IsCreateWaterMark");
            }
        }

        [XmlIgnore, DataMember, EdmRelationshipNavigationProperty("FileResourceModel", "FK_FileRestrictPic_Ref_FileRestrict", "resource_filerestrict"), SoapIgnore]
        public WTF.Resource.Entity.resource_filerestrict resource_filerestrict
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<WTF.Resource.Entity.resource_filerestrict>("FileResourceModel.FK_FileRestrictPic_Ref_FileRestrict", "resource_filerestrict").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<WTF.Resource.Entity.resource_filerestrict>("FileResourceModel.FK_FileRestrictPic_Ref_FileRestrict", "resource_filerestrict").Value = value;
            }
        }

        [Browsable(false), DataMember]
        public EntityReference<WTF.Resource.Entity.resource_filerestrict> resource_filerestrictReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<WTF.Resource.Entity.resource_filerestrict>("FileResourceModel.FK_FileRestrictPic_Ref_FileRestrict", "resource_filerestrict");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<WTF.Resource.Entity.resource_filerestrict>("FileResourceModel.FK_FileRestrictPic_Ref_FileRestrict", "resource_filerestrict", value);
                }
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=false), DataMember]
        public int SortIndex
        {
            get
            {
                return this._SortIndex;
            }
            set
            {
                this.ReportPropertyChanging("SortIndex");
                this._SortIndex = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("SortIndex");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=true, IsNullable=false), DataMember]
        public string SystemFilePicID
        {
            get
            {
                return this._SystemFilePicID;
            }
            set
            {
                if (this._SystemFilePicID != value)
                {
                    this.ReportPropertyChanging("SystemFilePicID");
                    this._SystemFilePicID = StructuralObject.SetValidValue(value, false);
                    this.ReportPropertyChanged("SystemFilePicID");
                }
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=false), DataMember]
        public int VerticalAlign
        {
            get
            {
                return this._VerticalAlign;
            }
            set
            {
                this.ReportPropertyChanging("VerticalAlign");
                this._VerticalAlign = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("VerticalAlign");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=false)]
        public string WaterImageID
        {
            get
            {
                return this._WaterImageID;
            }
            set
            {
                this.ReportPropertyChanging("WaterImageID");
                this._WaterImageID = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("WaterImageID");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=false)]
        public string WatermarkText
        {
            get
            {
                return this._WatermarkText;
            }
            set
            {
                this.ReportPropertyChanging("WatermarkText");
                this._WatermarkText = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("WatermarkText");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=false), DataMember]
        public int WatermarkType
        {
            get
            {
                return this._WatermarkType;
            }
            set
            {
                this.ReportPropertyChanging("WatermarkType");
                this._WatermarkType = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("WatermarkType");
            }
        }
    }
}

