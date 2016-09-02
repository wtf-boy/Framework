using System;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;

namespace WTF.Logging.Entity
{
	[EdmEntityType(NamespaceName = "LogModel", Name = "loger_operationloging"), DataContract(IsReference = true)]
	[Serializable]
	public class loger_operationloging : EntityObject
	{
		private int _OperationID;

		private int _ApplicationID;

		private string _ApplicationName;

		private string _Account;

		private string _TableName;

		private int _OperationTypeID;

		private string _SqlQuery;

		private DateTime _CreateDate;

		private string _IDPath;

		private string _ModuleTypeCode;

		private string _ApplicationHost;

		private string _UserHostAddress;

		private string _UrlReferrer;

		private string _RawUrl;

		[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false), DataMember]
		public int OperationID
		{
			get
			{
				return this._OperationID;
			}
			set
			{
				if (this._OperationID != value)
				{
					this.ReportPropertyChanging("OperationID");
					this._OperationID = StructuralObject.SetValidValue(value);
					this.ReportPropertyChanged("OperationID");
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
		public string TableName
		{
			get
			{
				return this._TableName;
			}
			set
			{
				this.ReportPropertyChanging("TableName");
				this._TableName = StructuralObject.SetValidValue(value, false);
				this.ReportPropertyChanged("TableName");
			}
		}

		[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false), DataMember]
		public int OperationTypeID
		{
			get
			{
				return this._OperationTypeID;
			}
			set
			{
				this.ReportPropertyChanging("OperationTypeID");
				this._OperationTypeID = StructuralObject.SetValidValue(value);
				this.ReportPropertyChanged("OperationTypeID");
			}
		}

		[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false), DataMember]
		public string SqlQuery
		{
			get
			{
				return this._SqlQuery;
			}
			set
			{
				this.ReportPropertyChanging("SqlQuery");
				this._SqlQuery = StructuralObject.SetValidValue(value, false);
				this.ReportPropertyChanged("SqlQuery");
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

		public static loger_operationloging Createloger_operationloging(int operationID, int applicationID, string applicationName, string account, string tableName, int operationTypeID, string sqlQuery, DateTime createDate, string iDPath, string moduleTypeCode, string applicationHost, string userHostAddress, string urlReferrer, string rawUrl)
		{
			return new loger_operationloging
			{
				OperationID = operationID,
				ApplicationID = applicationID,
				ApplicationName = applicationName,
				Account = account,
				TableName = tableName,
				OperationTypeID = operationTypeID,
				SqlQuery = sqlQuery,
				CreateDate = createDate,
				IDPath = iDPath,
				ModuleTypeCode = moduleTypeCode,
				ApplicationHost = applicationHost,
				UserHostAddress = userHostAddress,
				UrlReferrer = urlReferrer,
				RawUrl = rawUrl
			};
		}
	}
}
