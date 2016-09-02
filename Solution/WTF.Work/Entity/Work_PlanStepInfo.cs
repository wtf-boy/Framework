using System;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;

namespace WTF.Work.Entity
{
	[EdmEntityType(NamespaceName = "WorkModel", Name = "Work_PlanStepInfo"), DataContract(IsReference = true)]
	[Serializable]
	public class Work_PlanStepInfo : EntityObject
	{
		private int _PlanStepID;

		private int _PlanID;

		private string _StepName;

		private int _ProcessID;

		private int _RunCount;

		private int _RunInterval;

		private int _FailProcessType;

		private int _SortIndex;

		private string _ProcessName;

		private string _ProcessConfig;

		private string _AssemblyName;

		private string _TypeName;

		private int _SucessProcessType;

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

		[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false), DataMember]
		public int ProcessID
		{
			get
			{
				return this._ProcessID;
			}
			set
			{
				if (this._ProcessID != value)
				{
					this.ReportPropertyChanging("ProcessID");
					this._ProcessID = StructuralObject.SetValidValue(value);
					this.ReportPropertyChanged("ProcessID");
				}
			}
		}

		[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false), DataMember]
		public int RunCount
		{
			get
			{
				return this._RunCount;
			}
			set
			{
				if (this._RunCount != value)
				{
					this.ReportPropertyChanging("RunCount");
					this._RunCount = StructuralObject.SetValidValue(value);
					this.ReportPropertyChanged("RunCount");
				}
			}
		}

		[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false), DataMember]
		public int RunInterval
		{
			get
			{
				return this._RunInterval;
			}
			set
			{
				if (this._RunInterval != value)
				{
					this.ReportPropertyChanging("RunInterval");
					this._RunInterval = StructuralObject.SetValidValue(value);
					this.ReportPropertyChanged("RunInterval");
				}
			}
		}

		[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false), DataMember]
		public int FailProcessType
		{
			get
			{
				return this._FailProcessType;
			}
			set
			{
				if (this._FailProcessType != value)
				{
					this.ReportPropertyChanging("FailProcessType");
					this._FailProcessType = StructuralObject.SetValidValue(value);
					this.ReportPropertyChanged("FailProcessType");
				}
			}
		}

		[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false), DataMember]
		public int SortIndex
		{
			get
			{
				return this._SortIndex;
			}
			set
			{
				if (this._SortIndex != value)
				{
					this.ReportPropertyChanging("SortIndex");
					this._SortIndex = StructuralObject.SetValidValue(value);
					this.ReportPropertyChanged("SortIndex");
				}
			}
		}

		[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false), DataMember]
		public string ProcessName
		{
			get
			{
				return this._ProcessName;
			}
			set
			{
				if (this._ProcessName != value)
				{
					this.ReportPropertyChanging("ProcessName");
					this._ProcessName = StructuralObject.SetValidValue(value, false);
					this.ReportPropertyChanged("ProcessName");
				}
			}
		}

		[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false), DataMember]
		public string ProcessConfig
		{
			get
			{
				return this._ProcessConfig;
			}
			set
			{
				if (this._ProcessConfig != value)
				{
					this.ReportPropertyChanging("ProcessConfig");
					this._ProcessConfig = StructuralObject.SetValidValue(value, false);
					this.ReportPropertyChanged("ProcessConfig");
				}
			}
		}

		[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false), DataMember]
		public string AssemblyName
		{
			get
			{
				return this._AssemblyName;
			}
			set
			{
				if (this._AssemblyName != value)
				{
					this.ReportPropertyChanging("AssemblyName");
					this._AssemblyName = StructuralObject.SetValidValue(value, false);
					this.ReportPropertyChanged("AssemblyName");
				}
			}
		}

		[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false), DataMember]
		public string TypeName
		{
			get
			{
				return this._TypeName;
			}
			set
			{
				if (this._TypeName != value)
				{
					this.ReportPropertyChanging("TypeName");
					this._TypeName = StructuralObject.SetValidValue(value, false);
					this.ReportPropertyChanged("TypeName");
				}
			}
		}

		[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false), DataMember]
		public int SucessProcessType
		{
			get
			{
				return this._SucessProcessType;
			}
			set
			{
				if (this._SucessProcessType != value)
				{
					this.ReportPropertyChanging("SucessProcessType");
					this._SucessProcessType = StructuralObject.SetValidValue(value);
					this.ReportPropertyChanged("SucessProcessType");
				}
			}
		}

		public static Work_PlanStepInfo CreateWork_PlanStepInfo(int planStepID, int planID, string stepName, int processID, int runCount, int runInterval, int failProcessType, int sortIndex, string processName, string processConfig, string assemblyName, string typeName, int sucessProcessType)
		{
			return new Work_PlanStepInfo
			{
				PlanStepID = planStepID,
				PlanID = planID,
				StepName = stepName,
				ProcessID = processID,
				RunCount = runCount,
				RunInterval = runInterval,
				FailProcessType = failProcessType,
				SortIndex = sortIndex,
				ProcessName = processName,
				ProcessConfig = processConfig,
				AssemblyName = assemblyName,
				TypeName = typeName,
				SucessProcessType = sucessProcessType
			};
		}
	}
}
