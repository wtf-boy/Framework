using System;
using System.ComponentModel;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace WTF.Work.Entity
{
	[EdmEntityType(NamespaceName = "WorkModel", Name = "Work_PlanRun"), DataContract(IsReference = true)]
	[Serializable]
	public class Work_PlanRun : EntityObject
	{
		private int _PlanRunID;

		private int _WorkInfoID;

		private int _PlanID;

		private bool _IsRun;

		private DateTime _RunDate;

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
		public bool IsRun
		{
			get
			{
				return this._IsRun;
			}
			set
			{
				this.ReportPropertyChanging("IsRun");
				this._IsRun = StructuralObject.SetValidValue(value);
				this.ReportPropertyChanged("IsRun");
			}
		}

		[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false), DataMember]
		public DateTime RunDate
		{
			get
			{
				return this._RunDate;
			}
			set
			{
				this.ReportPropertyChanging("RunDate");
				this._RunDate = StructuralObject.SetValidValue(value);
				this.ReportPropertyChanged("RunDate");
			}
		}

		[EdmRelationshipNavigationProperty("WorkModel", "FK_WorkInfo_Ref_PlanRun", "work_workinfo"), DataMember, SoapIgnore, XmlIgnore]
		public Work_WorkInfo Work_WorkInfo
		{
			get
			{
				return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Work_WorkInfo>("WorkModel.FK_WorkInfo_Ref_PlanRun", "work_workinfo").Value;
			}
			set
			{
				((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Work_WorkInfo>("WorkModel.FK_WorkInfo_Ref_PlanRun", "work_workinfo").Value = value;
			}
		}

		[Browsable(false), DataMember]
		public EntityReference<Work_WorkInfo> Work_WorkInfoReference
		{
			get
			{
				return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Work_WorkInfo>("WorkModel.FK_WorkInfo_Ref_PlanRun", "work_workinfo");
			}
			set
			{
				if (value != null)
				{
					((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<Work_WorkInfo>("WorkModel.FK_WorkInfo_Ref_PlanRun", "work_workinfo", value);
				}
			}
		}

		public static Work_PlanRun CreateWork_PlanRun(int planRunID, int workInfoID, int planID, bool isRun, DateTime runDate)
		{
			return new Work_PlanRun
			{
				PlanRunID = planRunID,
				WorkInfoID = workInfoID,
				PlanID = planID,
				IsRun = isRun,
				RunDate = runDate
			};
		}
	}
}
