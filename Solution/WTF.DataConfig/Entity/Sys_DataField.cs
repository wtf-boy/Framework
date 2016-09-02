using System;
using System.ComponentModel;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace WTF.DataConfig.Entity
{
	[EdmEntityType(NamespaceName = "DataModel", Name = "Sys_DataField"), DataContract(IsReference = true)]
	[Serializable]
	public class Sys_DataField : EntityObject
	{
		private int _DataFieldID;

		private int? _DataTypeID;

		private string _DataValue;

		private string _DataTitle;

		[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false), DataMember]
		public int DataFieldID
		{
			get
			{
				return this._DataFieldID;
			}
			set
			{
				if (this._DataFieldID != value)
				{
					this.ReportPropertyChanging("DataFieldID");
					this._DataFieldID = StructuralObject.SetValidValue(value);
					this.ReportPropertyChanged("DataFieldID");
				}
			}
		}

		[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true), DataMember]
		public int? DataTypeID
		{
			get
			{
				return this._DataTypeID;
			}
			set
			{
				this.ReportPropertyChanging("DataTypeID");
				this._DataTypeID = StructuralObject.SetValidValue(value);
				this.ReportPropertyChanged("DataTypeID");
			}
		}

		[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false), DataMember]
		public string DataValue
		{
			get
			{
				return this._DataValue;
			}
			set
			{
				this.ReportPropertyChanging("DataValue");
				this._DataValue = StructuralObject.SetValidValue(value, false);
				this.ReportPropertyChanged("DataValue");
			}
		}

		[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false), DataMember]
		public string DataTitle
		{
			get
			{
				return this._DataTitle;
			}
			set
			{
				this.ReportPropertyChanging("DataTitle");
				this._DataTitle = StructuralObject.SetValidValue(value, false);
				this.ReportPropertyChanged("DataTitle");
			}
		}

		[EdmRelationshipNavigationProperty("DataModel", "FK_sys_datafield", "sys_datatype"), DataMember, SoapIgnore, XmlIgnore]
		public Sys_DataType Sys_DataType
		{
			get
			{
				return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Sys_DataType>("DataModel.FK_sys_datafield", "sys_datatype").Value;
			}
			set
			{
				((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Sys_DataType>("DataModel.FK_sys_datafield", "sys_datatype").Value = value;
			}
		}

		[Browsable(false), DataMember]
		public EntityReference<Sys_DataType> Sys_DataTypeReference
		{
			get
			{
				return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Sys_DataType>("DataModel.FK_sys_datafield", "sys_datatype");
			}
			set
			{
				if (value != null)
				{
					((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<Sys_DataType>("DataModel.FK_sys_datafield", "sys_datatype", value);
				}
			}
		}

		public static Sys_DataField CreateSys_DataField(int dataFieldID, string dataValue, string dataTitle)
		{
			return new Sys_DataField
			{
				DataFieldID = dataFieldID,
				DataValue = dataValue,
				DataTitle = dataTitle
			};
		}
	}
}
