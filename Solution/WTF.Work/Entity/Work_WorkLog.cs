using System;
using System.ComponentModel;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace WTF.Work.Entity
{
	[EdmEntityType(NamespaceName = "WorkModel", Name = "Work_WorkLog"), DataContract(IsReference = true)]
	[Serializable]
	public class Work_WorkLog : EntityObject
	{
		private string _WorkLogID;

		private int _WorkInfoID;

		private int _PlanID;

		private string _RunIP;

		private string _HostName;

		private DateTime _CreateDate;

		private DateTime _StartDate;

		private DateTime _EndDate;

		private int _ResultType;

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

		[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false), DataMember]
		public int WorkInfoID
		{
			get
			{
				return this._WorkInfoID;
			}
			set
			{
				this.ReportPropertyChanging("WorkInfoID");
				this._WorkInfoID = StructuralObject.SetValidValue(value);
				this.ReportPropertyChanged("WorkInfoID");
			}
		}

		[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false), DataMember]
		public int PlanID
		{
			get
			{
				return this._PlanID;
			}
			set
			{
				this.ReportPropertyChanging("PlanID");
				this._PlanID = StructuralObject.SetValidValue(value);
				this.ReportPropertyChanged("PlanID");
			}
		}

		[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false), DataMember]
		public string RunIP
		{
			get
			{
				return this._RunIP;
			}
			set
			{
				this.ReportPropertyChanging("RunIP");
				this._RunIP = StructuralObject.SetValidValue(value, false);
				this.ReportPropertyChanged("RunIP");
			}
		}

		[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false), DataMember]
		public string HostName
		{
			get
			{
				return this._HostName;
			}
			set
			{
				this.ReportPropertyChanging("HostName");
				this._HostName = StructuralObject.SetValidValue(value, false);
				this.ReportPropertyChanged("HostName");
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
		public DateTime StartDate
		{
			get
			{
				return this._StartDate;
			}
			set
			{
				this.ReportPropertyChanging("StartDate");
				this._StartDate = StructuralObject.SetValidValue(value);
				this.ReportPropertyChanged("StartDate");
			}
		}

		[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false), DataMember]
		public DateTime EndDate
		{
			get
			{
				return this._EndDate;
			}
			set
			{
				this.ReportPropertyChanging("EndDate");
				this._EndDate = StructuralObject.SetValidValue(value);
				this.ReportPropertyChanged("EndDate");
			}
		}

		[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false), DataMember]
		public int ResultType
		{
			get
			{
				return this._ResultType;
			}
			set
			{
				this.ReportPropertyChanging("ResultType");
				this._ResultType = StructuralObject.SetValidValue(value);
				this.ReportPropertyChanged("ResultType");
			}
		}

		[EdmRelationshipNavigationProperty("WorkModel", "FK_WorkInfo_Ref_WorkLog", "work_workinfo"), DataMember, SoapIgnore, XmlIgnore]
		public Work_WorkInfo Work_WorkInfo
		{
			get
			{
				return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Work_WorkInfo>("WorkModel.FK_WorkInfo_Ref_WorkLog", "work_workinfo").Value;
			}
			set
			{
				((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Work_WorkInfo>("WorkModel.FK_WorkInfo_Ref_WorkLog", "work_workinfo").Value = value;
			}
		}

		[Browsable(false), DataMember]
		public EntityReference<Work_WorkInfo> Work_WorkInfoReference
		{
			get
			{
				return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Work_WorkInfo>("WorkModel.FK_WorkInfo_Ref_WorkLog", "work_workinfo");
			}
			set
			{
				if (value != null)
				{
					((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<Work_WorkInfo>("WorkModel.FK_WorkInfo_Ref_WorkLog", "work_workinfo", value);
				}
			}
		}

		[EdmRelationshipNavigationProperty("WorkModel", "FK_WorkLog_Ref_ProcessLog", "work_workprocesslog"), DataMember, SoapIgnore, XmlIgnore]
		public EntityCollection<Work_WorkProcessLog> Work_WorkProcessLog
		{
			get
			{
				return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<Work_WorkProcessLog>("WorkModel.FK_WorkLog_Ref_ProcessLog", "work_workprocesslog");
			}
			set
			{
				if (value != null)
				{
					((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<Work_WorkProcessLog>("WorkModel.FK_WorkLog_Ref_ProcessLog", "work_workprocesslog", value);
				}
			}
		}

		public static Work_WorkLog CreateWork_WorkLog(string workLogID, int workInfoID, int planID, string runIP, string hostName, DateTime createDate, DateTime startDate, DateTime endDate, int resultType)
		{
			return new Work_WorkLog
			{
				WorkLogID = workLogID,
				WorkInfoID = workInfoID,
				PlanID = planID,
				RunIP = runIP,
				HostName = hostName,
				CreateDate = createDate,
				StartDate = startDate,
				EndDate = endDate,
				ResultType = resultType
			};
		}
	}
}
