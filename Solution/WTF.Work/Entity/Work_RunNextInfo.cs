using System;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;

namespace WTF.Work.Entity
{
	[EdmEntityType(NamespaceName = "WorkModel", Name = "Work_RunNextInfo"), DataContract(IsReference = true)]
	[Serializable]
	public class Work_RunNextInfo : EntityObject
	{
		private int _PlanID;

		private DateTime _RunDate;

		private string _RunIP;

		private bool _IsEnable;

		private string _WorkInfoName;

		private int _PlanRunID;

		private int _WorkInfoID;

		private bool _IsRun;

		private string _PlanName;

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
		public DateTime RunDate
		{
			get
			{
				return this._RunDate;
			}
			set
			{
				if (this._RunDate != value)
				{
					this.ReportPropertyChanging("RunDate");
					this._RunDate = StructuralObject.SetValidValue(value);
					this.ReportPropertyChanged("RunDate");
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
		public bool IsEnable
		{
			get
			{
				return this._IsEnable;
			}
			set
			{
				if (this._IsEnable != value)
				{
					this.ReportPropertyChanging("IsEnable");
					this._IsEnable = StructuralObject.SetValidValue(value);
					this.ReportPropertyChanged("IsEnable");
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
		public int PlanRunID
		{
			get
			{
				return this._PlanRunID;
			}
			set
			{
				if (this._PlanRunID != value)
				{
					this.ReportPropertyChanging("PlanRunID");
					this._PlanRunID = StructuralObject.SetValidValue(value);
					this.ReportPropertyChanged("PlanRunID");
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
		public bool IsRun
		{
			get
			{
				return this._IsRun;
			}
			set
			{
				if (this._IsRun != value)
				{
					this.ReportPropertyChanging("IsRun");
					this._IsRun = StructuralObject.SetValidValue(value);
					this.ReportPropertyChanged("IsRun");
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

		public static Work_RunNextInfo CreateWork_RunNextInfo(int planID, DateTime runDate, string runIP, bool isEnable, string workInfoName, int planRunID, int workInfoID, bool isRun)
		{
			return new Work_RunNextInfo
			{
				PlanID = planID,
				RunDate = runDate,
				RunIP = runIP,
				IsEnable = isEnable,
				WorkInfoName = workInfoName,
				PlanRunID = planRunID,
				WorkInfoID = workInfoID,
				IsRun = isRun
			};
		}
	}
}
