using System;
using System.ComponentModel;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace WTF.Work.Entity
{
	[EdmEntityType(NamespaceName = "WorkModel", Name = "Work_PlanStep"), DataContract(IsReference = true)]
	[Serializable]
	public class Work_PlanStep : EntityObject
	{
		private int _PlanStepID;

		private int _PlanID;

		private string _StepName;

		private int _ProcessID;

		private int _RunCount;

		private int _RunInterval;

		private int _SucessProcessType;

		private int _FailProcessType;

		private int _SortIndex;

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
		public string StepName
		{
			get
			{
				return this._StepName;
			}
			set
			{
				this.ReportPropertyChanging("StepName");
				this._StepName = StructuralObject.SetValidValue(value, false);
				this.ReportPropertyChanged("StepName");
			}
		}

		[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false), DataMember]
		public int ProcessID
		{
			get
			{
				return this._ProcessID;
			}
			set
			{
				this.ReportPropertyChanging("ProcessID");
				this._ProcessID = StructuralObject.SetValidValue(value);
				this.ReportPropertyChanged("ProcessID");
			}
		}

		[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false), DataMember]
		public int RunCount
		{
			get
			{
				return this._RunCount;
			}
			set
			{
				this.ReportPropertyChanging("RunCount");
				this._RunCount = StructuralObject.SetValidValue(value);
				this.ReportPropertyChanged("RunCount");
			}
		}

		[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false), DataMember]
		public int RunInterval
		{
			get
			{
				return this._RunInterval;
			}
			set
			{
				this.ReportPropertyChanging("RunInterval");
				this._RunInterval = StructuralObject.SetValidValue(value);
				this.ReportPropertyChanged("RunInterval");
			}
		}

		[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false), DataMember]
		public int SucessProcessType
		{
			get
			{
				return this._SucessProcessType;
			}
			set
			{
				this.ReportPropertyChanging("SucessProcessType");
				this._SucessProcessType = StructuralObject.SetValidValue(value);
				this.ReportPropertyChanged("SucessProcessType");
			}
		}

		[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false), DataMember]
		public int FailProcessType
		{
			get
			{
				return this._FailProcessType;
			}
			set
			{
				this.ReportPropertyChanging("FailProcessType");
				this._FailProcessType = StructuralObject.SetValidValue(value);
				this.ReportPropertyChanged("FailProcessType");
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

		[EdmRelationshipNavigationProperty("WorkModel", "FK_Plan_Ref_PlanStep", "work_plan"), DataMember, SoapIgnore, XmlIgnore]
		public Work_Plan Work_Plan
		{
			get
			{
				return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Work_Plan>("WorkModel.FK_Plan_Ref_PlanStep", "work_plan").Value;
			}
			set
			{
				((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Work_Plan>("WorkModel.FK_Plan_Ref_PlanStep", "work_plan").Value = value;
			}
		}

		[Browsable(false), DataMember]
		public EntityReference<Work_Plan> Work_PlanReference
		{
			get
			{
				return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Work_Plan>("WorkModel.FK_Plan_Ref_PlanStep", "work_plan");
			}
			set
			{
				if (value != null)
				{
					((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<Work_Plan>("WorkModel.FK_Plan_Ref_PlanStep", "work_plan", value);
				}
			}
		}

		public static Work_PlanStep CreateWork_PlanStep(int planStepID, int planID, string stepName, int processID, int runCount, int runInterval, int sucessProcessType, int failProcessType, int sortIndex)
		{
			return new Work_PlanStep
			{
				PlanStepID = planStepID,
				PlanID = planID,
				StepName = stepName,
				ProcessID = processID,
				RunCount = runCount,
				RunInterval = runInterval,
				SucessProcessType = sucessProcessType,
				FailProcessType = failProcessType,
				SortIndex = sortIndex
			};
		}
	}
}
