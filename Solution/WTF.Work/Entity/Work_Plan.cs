using System;
using System.ComponentModel;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace WTF.Work.Entity
{
	[EdmEntityType(NamespaceName = "WorkModel", Name = "Work_Plan"), DataContract(IsReference = true)]
	[Serializable]
	public class Work_Plan : EntityObject
	{
		private int _PlanID;

		private int _WorkInfoID;

		private bool _IsEnable;

		private DateTime? _StartDate;

		private DateTime? _EndDate;

		private DateTime _LastRunDate;

		private string _PlanName;

		private string _PlanConfig;

		private string _PlanRemark;

		private string _ConfigInfo;

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

		[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true), DataMember]
		public DateTime? StartDate
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

		[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true), DataMember]
		public DateTime? EndDate
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
		public DateTime LastRunDate
		{
			get
			{
				return this._LastRunDate;
			}
			set
			{
				this.ReportPropertyChanging("LastRunDate");
				this._LastRunDate = StructuralObject.SetValidValue(value);
				this.ReportPropertyChanged("LastRunDate");
			}
		}

		[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false), DataMember]
		public string PlanName
		{
			get
			{
				return this._PlanName;
			}
			set
			{
				this.ReportPropertyChanging("PlanName");
				this._PlanName = StructuralObject.SetValidValue(value, false);
				this.ReportPropertyChanged("PlanName");
			}
		}

		[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false), DataMember]
		public string PlanConfig
		{
			get
			{
				return this._PlanConfig;
			}
			set
			{
				this.ReportPropertyChanging("PlanConfig");
				this._PlanConfig = StructuralObject.SetValidValue(value, false);
				this.ReportPropertyChanged("PlanConfig");
			}
		}

		[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false), DataMember]
		public string PlanRemark
		{
			get
			{
				return this._PlanRemark;
			}
			set
			{
				this.ReportPropertyChanging("PlanRemark");
				this._PlanRemark = StructuralObject.SetValidValue(value, false);
				this.ReportPropertyChanged("PlanRemark");
			}
		}

		[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false), DataMember]
		public string ConfigInfo
		{
			get
			{
				return this._ConfigInfo;
			}
			set
			{
				this.ReportPropertyChanging("ConfigInfo");
				this._ConfigInfo = StructuralObject.SetValidValue(value, false);
				this.ReportPropertyChanged("ConfigInfo");
			}
		}

		[EdmRelationshipNavigationProperty("WorkModel", "FK_Plan_Ref_PlanNotify", "work_plannotify"), DataMember, SoapIgnore, XmlIgnore]
		public EntityCollection<Work_PlanNotify> Work_PlanNotify
		{
			get
			{
				return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<Work_PlanNotify>("WorkModel.FK_Plan_Ref_PlanNotify", "work_plannotify");
			}
			set
			{
				if (value != null)
				{
					((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<Work_PlanNotify>("WorkModel.FK_Plan_Ref_PlanNotify", "work_plannotify", value);
				}
			}
		}

		[EdmRelationshipNavigationProperty("WorkModel", "FK_Plan_Ref_PlanStep", "work_planstep"), DataMember, SoapIgnore, XmlIgnore]
		public EntityCollection<Work_PlanStep> Work_PlanStep
		{
			get
			{
				return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<Work_PlanStep>("WorkModel.FK_Plan_Ref_PlanStep", "work_planstep");
			}
			set
			{
				if (value != null)
				{
					((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<Work_PlanStep>("WorkModel.FK_Plan_Ref_PlanStep", "work_planstep", value);
				}
			}
		}

		[EdmRelationshipNavigationProperty("WorkModel", "FK_WorkInfo_Ref_Plan", "work_workinfo"), DataMember, SoapIgnore, XmlIgnore]
		public Work_WorkInfo Work_WorkInfo
		{
			get
			{
				return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Work_WorkInfo>("WorkModel.FK_WorkInfo_Ref_Plan", "work_workinfo").Value;
			}
			set
			{
				((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Work_WorkInfo>("WorkModel.FK_WorkInfo_Ref_Plan", "work_workinfo").Value = value;
			}
		}

		[Browsable(false), DataMember]
		public EntityReference<Work_WorkInfo> Work_WorkInfoReference
		{
			get
			{
				return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Work_WorkInfo>("WorkModel.FK_WorkInfo_Ref_Plan", "work_workinfo");
			}
			set
			{
				if (value != null)
				{
					((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<Work_WorkInfo>("WorkModel.FK_WorkInfo_Ref_Plan", "work_workinfo", value);
				}
			}
		}

		public static Work_Plan CreateWork_Plan(int planID, int workInfoID, bool isEnable, DateTime lastRunDate, string planName, string planConfig, string planRemark, string configInfo)
		{
			return new Work_Plan
			{
				PlanID = planID,
				WorkInfoID = workInfoID,
				IsEnable = isEnable,
				LastRunDate = lastRunDate,
				PlanName = planName,
				PlanConfig = planConfig,
				PlanRemark = planRemark,
				ConfigInfo = configInfo
			};
		}
	}
}
