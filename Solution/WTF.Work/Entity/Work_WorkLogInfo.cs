using System;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;

namespace WTF.Work.Entity
{
	[EdmEntityType(NamespaceName = "WorkModel", Name = "Work_WorkLogInfo"), DataContract(IsReference = true)]
	[Serializable]
	public class Work_WorkLogInfo : EntityObject
	{
		private string _WorkLogID;

		private int _WorkInfoID;

		private string _RunIP;

		private string _HostName;

		private DateTime _CreateDate;

		private DateTime _StartDate;

		private DateTime _EndDate;

		private string _WorkInfoName;

		private int _ResultType;

		private int _PlanID;

		private string _PlanName;

		[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false), DataMember]
		public string WorkLogID
		{
			get
			{
				return this._WorkLogID;
			}
			set
			{
				if (this._WorkLogID != value)
				{
					this.ReportPropertyChanging("WorkLogID");
					this._WorkLogID = StructuralObject.SetValidValue(value, false);
					this.ReportPropertyChanged("WorkLogID");
				}
			}
		}

		[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false), DataMember]
		public int WorkInfoID
		{
			get
			{
				return this._WorkInfoID;
			}
			set
			{
				if (this._WorkInfoID != value)
				{
					this.ReportPropertyChanging("WorkInfoID");
					this._WorkInfoID = StructuralObject.SetValidValue(value);
					this.ReportPropertyChanged("WorkInfoID");
				}
			}
		}

		[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false), DataMember]
		public string RunIP
		{
			get
			{
				return this._RunIP;
			}
			set
			{
				if (this._RunIP != value)
				{
					this.ReportPropertyChanging("RunIP");
					this._RunIP = StructuralObject.SetValidValue(value, false);
					this.ReportPropertyChanged("RunIP");
				}
			}
		}

		[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false), DataMember]
		public string HostName
		{
			get
			{
				return this._HostName;
			}
			set
			{
				if (this._HostName != value)
				{
					this.ReportPropertyChanging("HostName");
					this._HostName = StructuralObject.SetValidValue(value, false);
					this.ReportPropertyChanged("HostName");
				}
			}
		}

		[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false), DataMember]
		public DateTime CreateDate
		{
			get
			{
				return this._CreateDate;
			}
			set
			{
				if (this._CreateDate != value)
				{
					this.ReportPropertyChanging("CreateDate");
					this._CreateDate = StructuralObject.SetValidValue(value);
					this.ReportPropertyChanged("CreateDate");
				}
			}
		}

		[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false), DataMember]
		public DateTime StartDate
		{
			get
			{
				return this._StartDate;
			}
			set
			{
				if (this._StartDate != value)
				{
					this.ReportPropertyChanging("StartDate");
					this._StartDate = StructuralObject.SetValidValue(value);
					this.ReportPropertyChanged("StartDate");
				}
			}
		}

		[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false), DataMember]
		public DateTime EndDate
		{
			get
			{
				return this._EndDate;
			}
			set
			{
				if (this._EndDate != value)
				{
					this.ReportPropertyChanging("EndDate");
					this._EndDate = StructuralObject.SetValidValue(value);
					this.ReportPropertyChanged("EndDate");
				}
			}
		}

		[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false), DataMember]
		public string WorkInfoName
		{
			get
			{
				return this._WorkInfoName;
			}
			set
			{
				if (this._WorkInfoName != value)
				{
					this.ReportPropertyChanging("WorkInfoName");
					this._WorkInfoName = StructuralObject.SetValidValue(value, false);
					this.ReportPropertyChanged("WorkInfoName");
				}
			}
		}

		[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false), DataMember]
		public int ResultType
		{
			get
			{
				return this._ResultType;
			}
			set
			{
				if (this._ResultType != value)
				{
					this.ReportPropertyChanging("ResultType");
					this._ResultType = StructuralObject.SetValidValue(value);
					this.ReportPropertyChanged("ResultType");
				}
			}
		}

		[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false), DataMember]
		public int PlanID
		{
			get
			{
				return this._PlanID;
			}
			set
			{
				if (this._PlanID != value)
				{
					this.ReportPropertyChanging("PlanID");
					this._PlanID = StructuralObject.SetValidValue(value);
					this.ReportPropertyChanged("PlanID");
				}
			}
		}

		[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false), DataMember]
		public string PlanName
		{
			get
			{
				return this._PlanName;
			}
			set
			{
				if (this._PlanName != value)
				{
					this.ReportPropertyChanging("PlanName");
					this._PlanName = StructuralObject.SetValidValue(value, false);
					this.ReportPropertyChanged("PlanName");
				}
			}
		}

		public static Work_WorkLogInfo CreateWork_WorkLogInfo(string workLogID, int workInfoID, string runIP, string hostName, DateTime createDate, DateTime startDate, DateTime endDate, string workInfoName, int resultType, int planID, string planName)
		{
			return new Work_WorkLogInfo
			{
				WorkLogID = workLogID,
				WorkInfoID = workInfoID,
				RunIP = runIP,
				HostName = hostName,
				CreateDate = createDate,
				StartDate = startDate,
				EndDate = endDate,
				WorkInfoName = workInfoName,
				ResultType = resultType,
				PlanID = planID,
				PlanName = planName
			};
		}
	}
}
