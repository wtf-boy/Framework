namespace WTF.Resource.Entity
{
    using System;
    using System.ComponentModel;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    [Serializable, DataContract(IsReference=true), EdmEntityType(NamespaceName="ResourceModel", Name="Sys_ResourceRestrictPic")]
    public class Sys_ResourceRestrictPic : EntityObject
    {
        private bool _CreateWaterMark;
        private int _HorizontalAlign;
        private int _ImageHeight;
        private int _ImageWidth;
        private int _ResourceRestrictID;
        private int _ResourceRestrictPicID;
        private int _VerNo;
        private int _VerticalAlign;
        private string _WaterImageID;
        private string _WatermarkText;
        private int _WatermarkType;

        public static Sys_ResourceRestrictPic CreateSys_ResourceRestrictPic(int resourceRestrictPicID, int resourceRestrictID, int verNo, bool createWaterMark, string watermarkText, string waterImageID, int horizontalAlign, int verticalAlign, int imageWidth, int imageHeight, int watermarkType)
        {
            return new Sys_ResourceRestrictPic { ResourceRestrictPicID = resourceRestrictPicID, ResourceRestrictID = resourceRestrictID, VerNo = verNo, CreateWaterMark = createWaterMark, WatermarkText = watermarkText, WaterImageID = waterImageID, HorizontalAlign = horizontalAlign, VerticalAlign = verticalAlign, ImageWidth = imageWidth, ImageHeight = imageHeight, WatermarkType = watermarkType };
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=false), DataMember]
        public bool CreateWaterMark
        {
            get
            {
                return this._CreateWaterMark;
            }
            set
            {
                this.ReportPropertyChanging("CreateWaterMark");
                this._CreateWaterMark = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("CreateWaterMark");
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

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=false), DataMember]
        public int ResourceRestrictID
        {
            get
            {
                return this._ResourceRestrictID;
            }
            set
            {
                this.ReportPropertyChanging("ResourceRestrictID");
                this._ResourceRestrictID = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("ResourceRestrictID");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=true, IsNullable=false), DataMember]
        public int ResourceRestrictPicID
        {
            get
            {
                return this._ResourceRestrictPicID;
            }
            set
            {
                if (this._ResourceRestrictPicID != value)
                {
                    this.ReportPropertyChanging("ResourceRestrictPicID");
                    this._ResourceRestrictPicID = StructuralObject.SetValidValue(value);
                    this.ReportPropertyChanged("ResourceRestrictPicID");
                }
            }
        }

        [SoapIgnore, XmlIgnore, DataMember, EdmRelationshipNavigationProperty("ResourceModel", "FK_sys_resourcerestrictpic", "sys_resourcerestrict")]
        public WTF.Resource.Entity.Sys_ResourceRestrict Sys_ResourceRestrict
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<WTF.Resource.Entity.Sys_ResourceRestrict>("ResourceModel.FK_sys_resourcerestrictpic", "sys_resourcerestrict").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<WTF.Resource.Entity.Sys_ResourceRestrict>("ResourceModel.FK_sys_resourcerestrictpic", "sys_resourcerestrict").Value = value;
            }
        }

        [Browsable(false), DataMember]
        public EntityReference<WTF.Resource.Entity.Sys_ResourceRestrict> Sys_ResourceRestrictReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<WTF.Resource.Entity.Sys_ResourceRestrict>("ResourceModel.FK_sys_resourcerestrictpic", "sys_resourcerestrict");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<WTF.Resource.Entity.Sys_ResourceRestrict>("ResourceModel.FK_sys_resourcerestrictpic", "sys_resourcerestrict", value);
                }
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

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=false), DataMember]
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

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=false), DataMember]
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

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=false)]
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

