using System;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;

namespace WTF.Work.Entity
{
	[EdmEntityType(NamespaceName = "WorkModel", Name = "Work_WorkProcessLogInfo"), DataContract(IsReference = true)]
	[Serializable]
	public class Work_WorkProcessLogInfo : EntityObject
	{
		private int _WorkProcessLogID;

		private string _WorkLogID;

		private int _PlanID;

		private DateTime _CreateDate;

		private DateTime _StartDate;

		private DateTime _EndDate;

		private string _Message;

		private string _PlanName;

		private int _PlanStepID;

		private string _StepName;

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
		public string Message
		{
			get
			{
				return this._Message;
			}
			set
			{
				if (this._Message != value)
				{
					this.ReportPropertyChanging("Message");
					this._Message = StructuralObject.SetValidValue(value, false);
					this.ReportPropertyChanged("Message");
				}
			}
		}

		[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true), DataMember]
		public string PlanName
		{
			get
			{
				return this._PlanName;
			}
			set
			{
				this.ReportPropertyChanging("PlanName");
				this._PlanName = StructuralObject.SetValidValue(value, true);
				this.ReportPropertyChanged("PlanName");
			}
		}

		[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false), DataMember]
		public int PlanStepID
		{
			get
			{
				return this._PlanStepID;
			}
			set
			{
				if (this._PlanStepID != value)
				{
					this.ReportPropertyChanging("PlanStepID");
					this._PlanStepID = StructuralObject.SetValidValue(value);
					this.ReportPropertyChanged("PlanStepID");
				}
			}
		}

		[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false), DataMember]
		public string StepName
		{
			get
			{
				return this._StepName;
			}
			set
			{
				if (this._StepName != value)
				{
					this.ReportPropertyChanging("StepName");
					this._StepName = StructuralObject.SetValidValue(value, false);
					this.ReportPropertyChanged("StepName");
				}
			}
		}

		public static Work_WorkProcessLogInfo CreateWork_WorkProcessLogInfo(int workProcessLogID, string workLogID, int planID, DateTime createDate, DateTime startDate, DateTime endDate, string message, int planStepID, string stepName)
		{
			return new Work_WorkProcessLogInfo
			{
				WorkProcessLogID = workProcessLogID,
				WorkLogID = workLogID,
				PlanID = planID,
				CreateDate = createDate,
				StartDate = startDate,
				EndDate = endDate,
				Message = message,
				PlanStepID = planStepID,
				StepName = stepName
			};
		}
	}
}
