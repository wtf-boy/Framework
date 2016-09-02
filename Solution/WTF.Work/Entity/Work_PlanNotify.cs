using System;
using System.ComponentModel;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace WTF.Work.Entity
{
	[EdmEntityType(NamespaceName = "WorkModel", Name = "Work_PlanNotify"), DataContract(IsReference = true)]
	[Serializable]
	public class Work_PlanNotify : EntityObject
	{
		private int _PlanNotifyID;

		private int _PlanID;

		private int _NotifyAddressID;

		private int _PlanResult;

		private int _NotifyType;

		[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false), DataMember]
		public int PlanNotifyID
		{
			get
			{
				return this._PlanNotifyID;
			}
			set
			{
				if (this._PlanNotifyID != value)
				{
					this.ReportPropertyChanging("PlanNotifyID");
					this._PlanNotifyID = StructuralObject.SetValidValue(value);
					this.ReportPropertyChanged("PlanNotifyID");
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
		public int NotifyAddressID
		{
			get
			{
				return this._NotifyAddressID;
			}
			set
			{
				this.ReportPropertyChanging("NotifyAddressID");
				this._NotifyAddressID = StructuralObject.SetValidValue(value);
				this.ReportPropertyChanged("NotifyAddressID");
			}
		}

		[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false), DataMember]
		public int PlanResult
		{
			get
			{
				return this._PlanResult;
			}
			set
			{
				this.ReportPropertyChanging("PlanResult");
				this._PlanResult = StructuralObject.SetValidValue(value);
				this.ReportPropertyChanged("PlanResult");
			}
		}

		[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false), DataMember]
		public int NotifyType
		{
			get
			{
				return this._NotifyType;
			}
			set
			{
				this.ReportPropertyChanging("NotifyType");
				this._NotifyType = StructuralObject.SetValidValue(value);
				this.ReportPropertyChanged("NotifyType");
			}
		}

		[EdmRelationshipNavigationProperty("WorkModel", "FK_Plan_Ref_PlanNotify", "work_plan"), DataMember, SoapIgnore, XmlIgnore]
		public Work_Plan Work_Plan
		{
			get
			{
				return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Work_Plan>("WorkModel.FK_Plan_Ref_PlanNotify", "work_plan").Value;
			}
			set
			{
				((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Work_Plan>("WorkModel.FK_Plan_Ref_PlanNotify", "work_plan").Value = value;
			}
		}

		[Browsable(false), DataMember]
		public EntityReference<Work_Plan> Work_PlanReference
		{
			get
			{
				return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Work_Plan>("WorkModel.FK_Plan_Ref_PlanNotify", "work_plan");
			}
			set
			{
				if (value != null)
				{
					((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<Work_Plan>("WorkModel.FK_Plan_Ref_PlanNotify", "work_plan", value);
				}
			}
		}

		public static Work_PlanNotify CreateWork_PlanNotify(int planNotifyID, int planID, int notifyAddressID, int planResult, int notifyType)
		{
			return new Work_PlanNotify
			{
				PlanNotifyID = planNotifyID,
				PlanID = planID,
				NotifyAddressID = notifyAddressID,
				PlanResult = planResult,
				NotifyType = notifyType
			};
		}
	}
}
