using System;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;

namespace WTF.Logging.Entity
{
	[EdmEntityType(NamespaceName = "LogModel", Name = "loger_operationhistory"), DataContract(IsReference = true)]
	[Serializable]
	public class loger_operationhistory : EntityObject
	{
		private int _OperationHistoryID;

		private string _MenuPowerID;

		private int _UserID;

		private string _Account;

		private int _OperationTypeID;

		private string _CommandName;

		private DateTime _CreateDate;

		private string _Title;

		private string _Description;

		private string _OperationData;

		private string _UserHostAddress;

		private string _MenuName;

		[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false), DataMember]
		public int OperationHistoryID
		{
			get
			{
				return this._OperationHistoryID;
			}
			set
			{
				if (this._OperationHistoryID != value)
				{
					this.ReportPropertyChanging("OperationHistoryID");
					this._OperationHistoryID = StructuralObject.SetValidValue(value);
					this.ReportPropertyChanged("OperationHistoryID");
				}
			}
		}

		[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false), DataMember]
		public string MenuPowerID
		{
			get
			{
				return this._MenuPowerID;
			}
			set
			{
				this.ReportPropertyChanging("MenuPowerID");
				this._MenuPowerID = StructuralObject.SetValidValue(value, false);
				this.ReportPropertyChanged("MenuPowerID");
			}
		}

		[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false), DataMember]
		public int UserID
		{
			get
			{
				return this._UserID;
			}
			set
			{
				this.ReportPropertyChanging("UserID");
				this._UserID = StructuralObject.SetValidValue(value);
				this.ReportPropertyChanged("UserID");
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
		public string CommandName
		{
			get
			{
				return this._CommandName;
			}
			set
			{
				this.ReportPropertyChanging("CommandName");
				this._CommandName = StructuralObject.SetValidValue(value, false);
				this.ReportPropertyChanged("CommandName");
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
		public string Description
		{
			get
			{
				return this._Description;
			}
			set
			{
				this.ReportPropertyChanging("Description");
				this._Description = StructuralObject.SetValidValue(value, false);
				this.ReportPropertyChanged("Description");
			}
		}

		[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false), DataMember]
		public string OperationData
		{
			get
			{
				return this._OperationData;
			}
			set
			{
				this.ReportPropertyChanging("OperationData");
				this._OperationData = StructuralObject.SetValidValue(value, false);
				this.ReportPropertyChanged("OperationData");
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
		public string MenuName
		{
			get
			{
				return this._MenuName;
			}
			set
			{
				this.ReportPropertyChanging("MenuName");
				this._MenuName = StructuralObject.SetValidValue(value, false);
				this.ReportPropertyChanged("MenuName");
			}
		}

		public static loger_operationhistory Createloger_operationhistory(int operationHistoryID, string menuPowerID, int userID, string account, int operationTypeID, string commandName, DateTime createDate, string title, string description, string operationData, string userHostAddress, string menuName)
		{
			return new loger_operationhistory
			{
				OperationHistoryID = operationHistoryID,
				MenuPowerID = menuPowerID,
				UserID = userID,
				Account = account,
				OperationTypeID = operationTypeID,
				CommandName = commandName,
				CreateDate = createDate,
				Title = title,
				Description = description,
				OperationData = operationData,
				UserHostAddress = userHostAddress,
				MenuName = menuName
			};
		}
	}
}
