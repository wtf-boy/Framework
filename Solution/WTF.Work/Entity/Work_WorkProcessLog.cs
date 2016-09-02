using System;
using System.ComponentModel;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace WTF.Work.Entity
{
	[EdmEntityType(NamespaceName = "WorkModel", Name = "Work_WorkProcessLog"), DataContract(IsReference = true)]
	[Serializable]
	public class Work_WorkProcessLog : EntityObject
	{
		private int _WorkProcessLogID;

		private int _PlanStepID;

		private string _WorkLogID;

		private int _PlanID;

		private DateTime _CreateDate;

		private DateTime _StartDate;

		private DateTime _EndDate;

		private string _Message;

		[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false), DataMember]
		public int WorkProcessLogID
		{
			get
			{
				return this._WorkProcessLogID;
			}
			set
			{
				if (this._WorkProcessLogID != value)
				{
					this.ReportPropertyChanging("WorkProcessLogID");
					this._WorkProcessLogID = StructuralObject.SetValidValue(value);
					this.ReportPropertyChanged("WorkProcessLogID");
				}
			}
		}

		[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false), DataMember]
		public int PlanStepID
		{
			get
			{
				return this._PlanStepID;
			}
			set
			{
				this.ReportPropertyChanging("PlanStepID");
				this._PlanStepID = StructuralObject.SetValidValue(value);
				this.ReportPropertyChanged("PlanStepID");
			}
		}

		[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false), DataMember]
		public string WorkLogID
		{
			get
			{
				return this._WorkLogID;
			}
			set
			{
				this.ReportPropertyChanging("WorkLogID");
				this._WorkLogID = StructuralObject.SetValidValue(value, false);
				this.ReportPropertyChanged("WorkLogID");
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

		[EdmRelationshipNavigationProperty("WorkModel", "FK_WorkLog_Ref_ProcessLog", "work_worklog"), DataMember, SoapIgnore, XmlIgnore]
		public Work_WorkLog Work_WorkLog
		{
			get
			{
				return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Work_WorkLog>("WorkModel.FK_WorkLog_Ref_ProcessLog", "work_worklog").Value;
			}
			set
			{
				((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Work_WorkLog>("WorkModel.FK_WorkLog_Ref_ProcessLog", "work_worklog").Value = value;
			}
		}

		[Browsable(false), DataMember]
		public EntityReference<Work_WorkLog> Work_WorkLogReference
		{
			get
			{
				return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Work_WorkLog>("WorkModel.FK_WorkLog_Ref_ProcessLog", "work_worklog");
			}
			set
			{
				if (value != null)
				{
					((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<Work_WorkLog>("WorkModel.FK_WorkLog_Ref_ProcessLog", "work_worklog", value);
				}
			}
		}

		public static Work_WorkProcessLog CreateWork_WorkProcessLog(int workProcessLogID, int planStepID, string workLogID, int planID, DateTime createDate, DateTime startDate, DateTime endDate, string message)
		{
			return new Work_WorkProcessLog
			{
				WorkProcessLogID = workProcessLogID,
				PlanStepID = planStepID,
				WorkLogID = workLogID,
				PlanID = planID,
				CreateDate = createDate,
				StartDate = startDate,
				EndDate = endDate,
				Message = message
			};
		}
	}
}
