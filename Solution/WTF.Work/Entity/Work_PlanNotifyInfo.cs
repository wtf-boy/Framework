using System;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;

namespace WTF.Work.Entity
{
	[EdmEntityType(NamespaceName = "WorkModel", Name = "Work_PlanNotifyInfo"), DataContract(IsReference = true)]
	[Serializable]
	public class Work_PlanNotifyInfo : EntityObject
	{
		private int _NotifyType;

		private int _PlanResult;

		private int _NotifyAddressID;

		private int _PlanID;

		private int _PlanNotifyID;

		private string _Address;

		private string _AddressName;

		[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false), DataMember]
		public int NotifyType
		{
			get
			{
				return this._NotifyType;
			}
			set
			{
				if (this._NotifyType != value)
				{
					this.ReportPropertyChanging("NotifyType");
					this._NotifyType = StructuralObject.SetValidValue(value);
					this.ReportPropertyChanged("NotifyType");
				}
			}
		}

		[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false), DataMember]
		public int PlanResult
		{
			get
			{
				return this._PlanResult;
			}
			set
			{
				if (this._PlanResult != value)
				{
					this.ReportPropertyChanging("PlanResult");
					this._PlanResult = StructuralObject.SetValidValue(value);
					this.ReportPropertyChanged("PlanResult");
				}
			}
		}

		[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false), DataMember]
		public int NotifyAddressID
		{
			get
			{
				return this._NotifyAddressID;
			}
			set
			{
				if (this._NotifyAddressID != value)
				{
					this.ReportPropertyChanging("NotifyAddressID");
					this._NotifyAddressID = StructuralObject.SetValidValue(value);
					this.ReportPropertyChanged("NotifyAddressID");
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

		[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false), DataMember]
		public string Address
		{
			get
			{
				return this._Address;
			}
			set
			{
				if (this._Address != value)
				{
					this.ReportPropertyChanging("Address");
					this._Address = StructuralObject.SetValidValue(value, false);
					this.ReportPropertyChanged("Address");
				}
			}
		}

		[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false), DataMember]
		public string AddressName
		{
			get
			{
				return this._AddressName;
			}
			set
			{
				if (this._AddressName != value)
				{
					this.ReportPropertyChanging("AddressName");
					this._AddressName = StructuralObject.SetValidValue(value, false);
					this.ReportPropertyChanged("AddressName");
				}
			}
		}

		public static Work_PlanNotifyInfo CreateWork_PlanNotifyInfo(int notifyType, int planResult, int notifyAddressID, int planID, int planNotifyID, string address, string addressName)
		{
			return new Work_PlanNotifyInfo
			{
				NotifyType = notifyType,
				PlanResult = planResult,
				NotifyAddressID = notifyAddressID,
				PlanID = planID,
				PlanNotifyID = planNotifyID,
				Address = address,
				AddressName = addressName
			};
		}
	}
}
