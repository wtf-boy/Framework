using System;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;

namespace WTF.Work.Entity
{
	[EdmEntityType(NamespaceName = "WorkModel", Name = "Work_NotifyAddress"), DataContract(IsReference = true)]
	[Serializable]
	public class Work_NotifyAddress : EntityObject
	{
		private int _NotifyAddressID;

		private string _Address;

		private string _AddressName;

		private int _AddressType;

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

		[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false), DataMember]
		public string Address
		{
			get
			{
				return this._Address;
			}
			set
			{
				this.ReportPropertyChanging("Address");
				this._Address = StructuralObject.SetValidValue(value, false);
				this.ReportPropertyChanged("Address");
			}
		}

		[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false), DataMember]
		public string AddressName
		{
			get
			{
				return this._AddressName;
			}
			set
			{
				this.ReportPropertyChanging("AddressName");
				this._AddressName = StructuralObject.SetValidValue(value, false);
				this.ReportPropertyChanged("AddressName");
			}
		}

		[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false), DataMember]
		public int AddressType
		{
			get
			{
				return this._AddressType;
			}
			set
			{
				this.ReportPropertyChanging("AddressType");
				this._AddressType = StructuralObject.SetValidValue(value);
				this.ReportPropertyChanged("AddressType");
			}
		}

		public static Work_NotifyAddress CreateWork_NotifyAddress(int notifyAddressID, string address, string addressName, int addressType)
		{
			return new Work_NotifyAddress
			{
				NotifyAddressID = notifyAddressID,
				Address = address,
				AddressName = addressName,
				AddressType = addressType
			};
		}
	}
}
