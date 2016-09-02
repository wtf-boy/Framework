using System;
using System.ComponentModel;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace WTF.Logging.Entity
{
	[EdmEntityType(NamespaceName = "LogModel", Name = "loger_loging"), DataContract(IsReference = true)]
	[Serializable]
	public class loger_loging : EntityObject
	{
		private int _LogID;

		private int _ApplicationID;

		private string _MessageID;

		private string _ApplicationHost;

		private string _UrlReferrer;

		private string _RawUrl;

		private string _Account;

		private string _UserHostAddress;

		private string _ProcessName;

		private DateTime _LogDate;

		private string _Title;

		private string _Message;

		private string _ResultMessage;

		private string _ModuleTypeCode;

		private string _CategoryTypeCode;

		private string _ApplicationName;

		private string _IDPath;

		private string _HeadersData;

		private string _RequestData;

		private string _UserAgent;

		[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false), DataMember]
		public int LogID
		{
			get
			{
				return this._LogID;
			}
			set
			{
				if (this._LogID != value)
				{
					this.ReportPropertyChanging("LogID");
					this._LogID = StructuralObject.SetValidValue(value);
					this.ReportPropertyChanged("LogID");
				}
			}
		}

		[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false), DataMember]
		public int ApplicationID
		{
			get
			{
				return this._ApplicationID;
			}
			set
			{
				this.ReportPropertyChanging("ApplicationID");
				this._ApplicationID = StructuralObject.SetValidValue(value);
				this.ReportPropertyChanged("ApplicationID");
			}
		}

		[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false), DataMember]
		public string MessageID
		{
			get
			{
				return this._MessageID;
			}
			set
			{
				this.ReportPropertyChanging("MessageID");
				this._MessageID = StructuralObject.SetValidValue(value, false);
				this.ReportPropertyChanged("MessageID");
			}
		}

		[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false), DataMember]
		public string ApplicationHost
		{
			get
			{
				return this._ApplicationHost;
			}
			set
			{
				this.ReportPropertyChanging("ApplicationHost");
				this._ApplicationHost = StructuralObject.SetValidValue(value, false);
				this.ReportPropertyChanged("ApplicationHost");
			}
		}

		[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false), DataMember]
		public string UrlReferrer
		{
			get
			{
				return this._UrlReferrer;
			}
			set
			{
				this.ReportPropertyChanging("UrlReferrer");
				this._UrlReferrer = StructuralObject.SetValidValue(value, false);
				this.ReportPropertyChanged("UrlReferrer");
			}
		}

		[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false), DataMember]
		public string RawUrl
		{
			get
			{
				return this._RawUrl;
			}
			set
			{
				this.ReportPropertyChanging("RawUrl");
				this._RawUrl = StructuralObject.SetValidValue(value, false);
				this.ReportPropertyChanged("RawUrl");
			}
		}

		[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false), DataMember]
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

		[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false), DataMember]
		public string UserHostAddress
		{
			get
			{
				return this._UserHostAddress;
			}
			set
			{
				this.ReportPropertyChanging("UserHostAddress");
				this._UserHostAddress = StructuralObject.SetValidValue(value, false);
				this.ReportPropertyChanged("UserHostAddress");
			}
		}

		[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false), DataMember]
		public string ProcessName
		{
			get
			{
				return this._ProcessName;
			}
			set
			{
				this.ReportPropertyChanging("ProcessName");
				this._ProcessName = StructuralObject.SetValidValue(value, false);
				this.ReportPropertyChanged("ProcessName");
			}
		}

		[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false), DataMember]
		public DateTime LogDate
		{
			get
			{
				return this._LogDate;
			}
			set
			{
				this.ReportPropertyChanging("LogDate");
				this._LogDate = StructuralObject.SetValidValue(value);
				this.ReportPropertyChanged("LogDate");
			}
		}

		[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false), DataMember]
		public string Title
		{
			get
			{
				return this._Title;
			}
			set
			{
				this.ReportPropertyChanging("Title");
				this._Title = StructuralObject.SetValidValue(value, false);
				this.ReportPropertyChanged("Title");
			}
		}

		[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false), DataMember]
		public string Message
		{
			get
			{
				return this._Message;
			}
			set
			{
				this.ReportPropertyChanging("Message");
				this._Message = StructuralObject.SetValidValue(value, false);
				this.ReportPropertyChanged("Message");
			}
		}

		[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false), DataMember]
		public string ResultMessage
		{
			get
			{
				return this._ResultMessage;
			}
			set
			{
				this.ReportPropertyChanging("ResultMessage");
				this._ResultMessage = StructuralObject.SetValidValue(value, false);
				this.ReportPropertyChanged("ResultMessage");
			}
		}

		[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false), DataMember]
		public string ModuleTypeCode
		{
			get
			{
				return this._ModuleTypeCode;
			}
			set
			{
				this.ReportPropertyChanging("ModuleTypeCode");
				this._ModuleTypeCode = StructuralObject.SetValidValue(value, false);
				this.ReportPropertyChanged("ModuleTypeCode");
			}
		}

		[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false), DataMember]
		public string CategoryTypeCode
		{
			get
			{
				return this._CategoryTypeCode;
			}
			set
			{
				this.ReportPropertyChanging("CategoryTypeCode");
				this._CategoryTypeCode = StructuralObject.SetValidValue(value, false);
				this.ReportPropertyChanged("CategoryTypeCode");
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
		public string HeadersData
		{
			get
			{
				return this._HeadersData;
			}
			set
			{
				this.ReportPropertyChanging("HeadersData");
				this._HeadersData = StructuralObject.SetValidValue(value, false);
				this.ReportPropertyChanged("HeadersData");
			}
		}

		[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false), DataMember]
		public string RequestData
		{
			get
			{
				return this._RequestData;
			}
			set
			{
				this.ReportPropertyChanging("RequestData");
				this._RequestData = StructuralObject.SetValidValue(value, false);
				this.ReportPropertyChanged("RequestData");
			}
		}

		[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false), DataMember]
		public string UserAgent
		{
			get
			{
				return this._UserAgent;
			}
			set
			{
				this.ReportPropertyChanging("UserAgent");
				this._UserAgent = StructuralObject.SetValidValue(value, false);
				this.ReportPropertyChanged("UserAgent");
			}
		}

		[EdmRelationshipNavigationProperty("LogModel", "FK_Application_Loging", "loger_application"), DataMember, SoapIgnore, XmlIgnore]
		public loger_application loger_application
		{
			get
			{
				return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<loger_application>("LogModel.FK_Application_Loging", "loger_application").Value;
			}
			set
			{
				((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<loger_application>("LogModel.FK_Application_Loging", "loger_application").Value = value;
			}
		}

		[Browsable(false), DataMember]
		public EntityReference<loger_application> loger_applicationReference
		{
			get
			{
				return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<loger_application>("LogModel.FK_Application_Loging", "loger_application");
			}
			set
			{
				if (value != null)
				{
					((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<loger_application>("LogModel.FK_Application_Loging", "loger_application", value);
				}
			}
		}

		public static loger_loging Createloger_loging(int logID, int applicationID, string messageID, string applicationHost, string urlReferrer, string rawUrl, string account, string userHostAddress, string processName, DateTime logDate, string title, string message, string resultMessage, string moduleTypeCode, string categoryTypeCode, string applicationName, string iDPath, string headersData, string requestData, string userAgent)
		{
			return new loger_loging
			{
				LogID = logID,
				ApplicationID = applicationID,
				MessageID = messageID,
				ApplicationHost = applicationHost,
				UrlReferrer = urlReferrer,
				RawUrl = rawUrl,
				Account = account,
				UserHostAddress = userHostAddress,
				ProcessName = processName,
				LogDate = logDate,
				Title = title,
				Message = message,
				ResultMessage = resultMessage,
				ModuleTypeCode = moduleTypeCode,
				CategoryTypeCode = categoryTypeCode,
				ApplicationName = applicationName,
				IDPath = iDPath,
				HeadersData = headersData,
				RequestData = requestData,
				UserAgent = userAgent
			};
		}
	}
}
