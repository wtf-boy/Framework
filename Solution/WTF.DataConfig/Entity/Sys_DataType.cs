using System;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace WTF.DataConfig.Entity
{
	[EdmEntityType(NamespaceName = "DataModel", Name = "Sys_DataType"), DataContract(IsReference = true)]
	[Serializable]
	public class Sys_DataType : EntityObject
	{
		private int _DataTypeID;

		private string _DataCode;

		private string _DataName;

		[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false), DataMember]
		public int DataTypeID
		{
			get
			{
				return this._DataTypeID;
			}
			set
			{
				if (this._DataTypeID != value)
				{
					this.ReportPropertyChanging("DataTypeID");
					this._DataTypeID = StructuralObject.SetValidValue(value);
					this.ReportPropertyChanged("DataTypeID");
				}
			}
		}

		[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false), DataMember]
		public string DataCode
		{
			get
			{
				return this._DataCode;
			}
			set
			{
				this.ReportPropertyChanging("DataCode");
				this._DataCode = StructuralObject.SetValidValue(value, false);
				this.ReportPropertyChanged("DataCode");
			}
		}

		[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false), DataMember]
		public string DataName
		{
			get
			{
				return this._DataName;
			}
			set
			{
				this.ReportPropertyChanging("DataName");
				this._DataName = StructuralObject.SetValidValue(value, false);
				this.ReportPropertyChanged("DataName");
			}
		}

		[EdmRelationshipNavigationProperty("DataModel", "FK_sys_datafield", "sys_datafield"), DataMember, SoapIgnore, XmlIgnore]
		public EntityCollection<Sys_DataField> Sys_DataField
		{
			get
			{
				return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<Sys_DataField>("DataModel.FK_sys_datafield", "sys_datafield");
			}
			set
			{
				if (value != null)
				{
					((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<Sys_DataField>("DataModel.FK_sys_datafield", "sys_datafield", value);
				}
			}
		}

		public static Sys_DataType CreateSys_DataType(int dataTypeID, string dataCode, string dataName)
		{
			return new Sys_DataType
			{
				DataTypeID = dataTypeID,
				DataCode = dataCode,
				DataName = dataName
			};
		}
	}
}
