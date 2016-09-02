using System;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace WTF.Work.Entity
{
	[EdmEntityType(NamespaceName = "WorkModel", Name = "Work_WorkInfo"), DataContract(IsReference = true)]
	[Serializable]
	public class Work_WorkInfo : EntityObject
	{
		private int _WorkInfoID;

		private string _WorkInfoName;

		private string _RunIP;

		private string _WorkInfoRemark;

		private bool _IsEnable;

		private DateTime _CreateDate;

		private DateTime _ModifyDate;

		private DateTime _LastProcessDate;

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

		[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false), DataMember]
		public string WorkInfoName
		{
			get
			{
				return this._WorkInfoName;
			}
			set
			{
				this.ReportPropertyChanging("WorkInfoName");
				this._WorkInfoName = StructuralObject.SetValidValue(value, false);
				this.ReportPropertyChanged("WorkInfoName");
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
		public string WorkInfoRemark
		{
			get
			{
				return this._WorkInfoRemark;
			}
			set
			{
				this.ReportPropertyChanging("WorkInfoRemark");
				this._WorkInfoRemark = StructuralObject.SetValidValue(value, false);
				this.ReportPropertyChanged("WorkInfoRemark");
			}
		}

		[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false), DataMember]
		public bool IsEnable
		{
			get
			{
				return this._IsEnable;
			}
			set
			{
				this.ReportPropertyChanging("IsEnable");
				this._IsEnable = StructuralObject.SetValidValue(value);
				this.ReportPropertyChanged("IsEnable");
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
		public DateTime ModifyDate
		{
			get
			{
				return this._ModifyDate;
			}
			set
			{
				this.ReportPropertyChanging("ModifyDate");
				this._ModifyDate = StructuralObject.SetValidValue(value);
				this.ReportPropertyChanged("ModifyDate");
			}
		}

		[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false), DataMember]
		public DateTime LastProcessDate
		{
			get
			{
				return this._LastProcessDate;
			}
			set
			{
				this.ReportPropertyChanging("LastProcessDate");
				this._LastProcessDate = StructuralObject.SetValidValue(value);
				this.ReportPropertyChanged("LastProcessDate");
			}
		}

		[EdmRelationshipNavigationProperty("WorkModel", "FK_WorkInfo_Ref_Plan", "work_plan"), DataMember, SoapIgnore, XmlIgnore]
		public EntityCollection<Work_Plan> Work_Plan
		{
			get
			{
				return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<Work_Plan>("WorkModel.FK_WorkInfo_Ref_Plan", "work_plan");
			}
			set
			{
				if (value != null)
				{
					((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<Work_Plan>("WorkModel.FK_WorkInfo_Ref_Plan", "work_plan", value);
				}
			}
		}

		[EdmRelationshipNavigationProperty("WorkModel", "FK_WorkInfo_Ref_PlanRun", "work_planrun"), DataMember, SoapIgnore, XmlIgnore]
		public EntityCollection<Work_PlanRun> Work_PlanRun
		{
			get
			{
				return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<Work_PlanRun>("WorkModel.FK_WorkInfo_Ref_PlanRun", "work_planrun");
			}
			set
			{
				if (value != null)
				{
					((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<Work_PlanRun>("WorkModel.FK_WorkInfo_Ref_PlanRun", "work_planrun", value);
				}
			}
		}

		[EdmRelationshipNavigationProperty("WorkModel", "FK_WorkInfo_Ref_Process", "work_process"), DataMember, SoapIgnore, XmlIgnore]
		public EntityCollection<Work_Process> Work_Process
		{
			get
			{
				return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<Work_Process>("WorkModel.FK_WorkInfo_Ref_Process", "work_process");
			}
			set
			{
				if (value != null)
				{
					((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<Work_Process>("WorkModel.FK_WorkInfo_Ref_Process", "work_process", value);
				}
			}
		}

		[EdmRelationshipNavigationProperty("WorkModel", "FK_WorkInfo_Ref_WorkLog", "work_worklog"), DataMember, SoapIgnore, XmlIgnore]
		public EntityCollection<Work_WorkLog> Work_WorkLg
		{
			get
			{
				return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<Work_WorkLog>("WorkModel.FK_WorkInfo_Ref_WorkLog", "work_worklog");
			}
			set
			{
				if (value != null)
				{
					((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<Work_WorkLog>("WorkModel.FK_WorkInfo_Ref_WorkLog", "work_worklog", value);
				}
			}
		}

		public static Work_WorkInfo CreateWork_WorkInfo(int workInfoID, string workInfoName, string runIP, string workInfoRemark, bool isEnable, DateTime createDate, DateTime modifyDate, DateTime lastProcessDate)
		{
			return new Work_WorkInfo
			{
				WorkInfoID = workInfoID,
				WorkInfoName = workInfoName,
				RunIP = runIP,
				WorkInfoRemark = workInfoRemark,
				IsEnable = isEnable,
				CreateDate = createDate,
				ModifyDate = modifyDate,
				LastProcessDate = lastProcessDate
			};
		}
	}
}
