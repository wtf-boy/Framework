using System;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace WTF.Logging.Entity
{
	[EdmEntityType(NamespaceName = "LogModel", Name = "loger_application"), DataContract(IsReference = true)]
	[Serializable]
	public class loger_application : EntityObject
	{
		private int _ApplicationID;

		private string _ApplicationCode;

		private string _ApplicationName;

		private string _Remark;

		private bool _IsDispose;

		private int _ParentID;

		private string _IDPath;

		private int _SortIndex;

		private DateTime _CreateDate;

		private bool _IsNotice;

		private int _LogerCount;

		private string _NoticeEmail;

		private string _NoticePhone;

		private string _NoticeCategory;

		private int _NoticeInterval;

		private int _NoticeSleep;

		private int _IntervalMinutes;

		private int _MinutesMaxCount;

		private string _HeaderKey;

		private string _RequestKey;

		[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false), DataMember]
		public int ApplicationID
		{
			get
			{
				return this._ApplicationID;
			}
			set
			{
				if (this._ApplicationID != value)
				{
					this.ReportPropertyChanging("ApplicationID");
					this._ApplicationID = StructuralObject.SetValidValue(value);
					this.ReportPropertyChanged("ApplicationID");
				}
			}
		}

		[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false), DataMember]
		public string ApplicationCode
		{
			get
			{
				return this._ApplicationCode;
			}
			set
			{
				this.ReportPropertyChanging("ApplicationCode");
				this._ApplicationCode = StructuralObject.SetValidValue(value, false);
				this.ReportPropertyChanged("ApplicationCode");
			}
		}

		[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false), DataMember]
		public string ApplicationName
		{
			get
			{
				return this._ApplicationName;
			}
			set
			{
				this.ReportPropertyChanging("ApplicationName");
				this._ApplicationName = StructuralObject.SetValidValue(value, false);
				this.ReportPropertyChanged("ApplicationName");
			}
		}

		[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false), DataMember]
		public string Remark
		{
			get
			{
				return this._Remark;
			}
			set
			{
				this.ReportPropertyChanging("Remark");
				this._Remark = StructuralObject.SetValidValue(value, false);
				this.ReportPropertyChanged("Remark");
			}
		}

		[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false), DataMember]
		public bool IsDispose
		{
			get
			{
				return this._IsDispose;
			}
			set
			{
				this.ReportPropertyChanging("IsDispose");
				this._IsDispose = StructuralObject.SetValidValue(value);
				this.ReportPropertyChanged("IsDispose");
			}
		}

		[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false), DataMember]
		public int ParentID
		{
			get
			{
				return this._ParentID;
			}
			set
			{
				this.ReportPropertyChanging("ParentID");
				this._ParentID = StructuralObject.SetValidValue(value);
				this.ReportPropertyChanged("ParentID");
			}
		}

		[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false), DataMember]
		public string IDPath
		{
			get
			{
				return this._IDPath;
			}
			set
			{
				this.ReportPropertyChanging("IDPath");
				this._IDPath = StructuralObject.SetValidValue(value, false);
				this.ReportPropertyChanged("IDPath");
			}
		}

		[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false), DataMember]
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

		[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false), DataMember]
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

		[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false), DataMember]
		public bool IsNotice
		{
			get
			{
				return this._IsNotice;
			}
			set
			{
				this.ReportPropertyChanging("IsNotice");
				this._IsNotice = StructuralObject.SetValidValue(value);
				this.ReportPropertyChanged("IsNotice");
			}
		}

		[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false), DataMember]
		public int LogerCount
		{
			get
			{
				return this._LogerCount;
			}
			set
			{
				this.ReportPropertyChanging("LogerCount");
				this._LogerCount = StructuralObject.SetValidValue(value);
				this.ReportPropertyChanged("LogerCount");
			}
		}

		[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false), DataMember]
		public string NoticeEmail
		{
			get
			{
				return this._NoticeEmail;
			}
			set
			{
				this.ReportPropertyChanging("NoticeEmail");
				this._NoticeEmail = StructuralObject.SetValidValue(value, false);
				this.ReportPropertyChanged("NoticeEmail");
			}
		}

		[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false), DataMember]
		public string NoticePhone
		{
			get
			{
				return this._NoticePhone;
			}
			set
			{
				this.ReportPropertyChanging("NoticePhone");
				this._NoticePhone = StructuralObject.SetValidValue(value, false);
				this.ReportPropertyChanged("NoticePhone");
			}
		}

		[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false), DataMember]
		public string NoticeCategory
		{
			get
			{
				return this._NoticeCategory;
			}
			set
			{
				this.ReportPropertyChanging("NoticeCategory");
				this._NoticeCategory = StructuralObject.SetValidValue(value, false);
				this.ReportPropertyChanged("NoticeCategory");
			}
		}

		[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false), DataMember]
		public int NoticeInterval
		{
			get
			{
				return this._NoticeInterval;
			}
			set
			{
				this.ReportPropertyChanging("NoticeInterval");
				this._NoticeInterval = StructuralObject.SetValidValue(value);
				this.ReportPropertyChanged("NoticeInterval");
			}
		}

		[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false), DataMember]
		public int NoticeSleep
		{
			get
			{
				return this._NoticeSleep;
			}
			set
			{
				this.ReportPropertyChanging("NoticeSleep");
				this._NoticeSleep = StructuralObject.SetValidValue(value);
				this.ReportPropertyChanged("NoticeSleep");
			}
		}

		[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false), DataMember]
		public int IntervalMinutes
		{
			get
			{
				return this._IntervalMinutes;
			}
			set
			{
				this.ReportPropertyChanging("IntervalMinutes");
				this._IntervalMinutes = StructuralObject.SetValidValue(value);
				this.ReportPropertyChanged("IntervalMinutes");
			}
		}

		[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false), DataMember]
		public int MinutesMaxCount
		{
			get
			{
				return this._MinutesMaxCount;
			}
			set
			{
				this.ReportPropertyChanging("MinutesMaxCount");
				this._MinutesMaxCount = StructuralObject.SetValidValue(value);
				this.ReportPropertyChanged("MinutesMaxCount");
			}
		}

		[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false), DataMember]
		public string HeaderKey
		{
			get
			{
				return this._HeaderKey;
			}
			set
			{
				this.ReportPropertyChanging("HeaderKey");
				this._HeaderKey = StructuralObject.SetValidValue(value, false);
				this.ReportPropertyChanged("HeaderKey");
			}
		}

		[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false), DataMember]
		public string RequestKey
		{
			get
			{
				return this._RequestKey;
			}
			set
			{
				this.ReportPropertyChanging("RequestKey");
				this._RequestKey = StructuralObject.SetValidValue(value, false);
				this.ReportPropertyChanged("RequestKey");
			}
		}

		[EdmRelationshipNavigationProperty("LogModel", "Fk_Application_Category", "loger_category"), DataMember, SoapIgnore, XmlIgnore]
		public EntityCollection<loger_category> loger_category
		{
			get
			{
				return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<loger_category>("LogModel.Fk_Application_Category", "loger_category");
			}
			set
			{
				if (value != null)
				{
					((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<loger_category>("LogModel.Fk_Application_Category", "loger_category", value);
				}
			}
		}

		[EdmRelationshipNavigationProperty("LogModel", "FK_Application_Loging", "loger_loging"), DataMember, SoapIgnore, XmlIgnore]
		public EntityCollection<loger_loging> loger_loging
		{
			get
			{
				return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<loger_loging>("LogModel.FK_Application_Loging", "loger_loging");
			}
			set
			{
				if (value != null)
				{
					((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<loger_loging>("LogModel.FK_Application_Loging", "loger_loging", value);
				}
			}
		}

		public static loger_application Createloger_application(int applicationID, string applicationCode, string applicationName, string remark, bool isDispose, int parentID, string iDPath, int sortIndex, DateTime createDate, bool isNotice, int logerCount, string noticeEmail, string noticePhone, string noticeCategory, int noticeInterval, int noticeSleep, int intervalMinutes, int minutesMaxCount, string headerKey, string requestKey)
		{
			return new loger_application
			{
				ApplicationID = applicationID,
				ApplicationCode = applicationCode,
				ApplicationName = applicationName,
				Remark = remark,
				IsDispose = isDispose,
				ParentID = parentID,
				IDPath = iDPath,
				SortIndex = sortIndex,
				CreateDate = createDate,
				IsNotice = isNotice,
				LogerCount = logerCount,
				NoticeEmail = noticeEmail,
				NoticePhone = noticePhone,
				NoticeCategory = noticeCategory,
				NoticeInterval = noticeInterval,
				NoticeSleep = noticeSleep,
				IntervalMinutes = intervalMinutes,
				MinutesMaxCount = minutesMaxCount,
				HeaderKey = headerKey,
				RequestKey = requestKey
			};
		}
	}
}
