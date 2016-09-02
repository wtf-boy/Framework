namespace WTF.Resource.Entity
{
    using System;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;

    [Serializable, DataContract(IsReference=true), EdmEntityType(NamespaceName="FileResourceModel", Name="resource_filehistory")]
    public class resource_filehistory : EntityObject
    {
        private DateTime _CreateDate;
        private int _CreateUserID;
        private int _FileHistoryID;
        private string _FileResourceID;
        private int _FileStatus;
        private int _FileType;
        private string _PicTitle;
        private string _PicUrl;
        private string _UserName;

        public static resource_filehistory Createresource_filehistory(int fileHistoryID, string fileResourceID, int fileType, string picTitle, string picUrl, int fileStatus, DateTime createDate, int createUserID, string userName)
        {
            return new resource_filehistory { FileHistoryID = fileHistoryID, FileResourceID = fileResourceID, FileType = fileType, PicTitle = picTitle, PicUrl = picUrl, FileStatus = fileStatus, CreateDate = createDate, CreateUserID = createUserID, UserName = userName };
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=false)]
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
        public int CreateUserID
        {
            get
            {
                return this._CreateUserID;
            }
            set
            {
                this.ReportPropertyChanging("CreateUserID");
                this._CreateUserID = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("CreateUserID");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=true, IsNullable=false)]
        public int FileHistoryID
        {
            get
            {
                return this._FileHistoryID;
            }
            set
            {
                if (this._FileHistoryID != value)
                {
                    this.ReportPropertyChanging("FileHistoryID");
                    this._FileHistoryID = StructuralObject.SetValidValue(value);
                    this.ReportPropertyChanged("FileHistoryID");
                }
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

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=false)]
        public int FileStatus
        {
            get
            {
                return this._FileStatus;
            }
            set
            {
                this.ReportPropertyChanging("FileStatus");
                this._FileStatus = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("FileStatus");
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

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=false), DataMember]
        public string PicTitle
        {
            get
            {
                return this._PicTitle;
            }
            set
            {
                this.ReportPropertyChanging("PicTitle");
                this._PicTitle = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("PicTitle");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=false)]
        public string PicUrl
        {
            get
            {
                return this._PicUrl;
            }
            set
            {
                this.ReportPropertyChanging("PicUrl");
                this._PicUrl = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("PicUrl");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=false)]
        public string UserName
        {
            get
            {
                return this._UserName;
            }
            set
            {
                this.ReportPropertyChanging("UserName");
                this._UserName = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("UserName");
            }
        }
    }
}

