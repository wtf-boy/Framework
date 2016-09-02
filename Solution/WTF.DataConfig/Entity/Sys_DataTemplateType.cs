using System;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace WTF.DataConfig.Entity
{
	[EdmEntityType(NamespaceName = "DataTemplateModel", Name = "Sys_DataTemplateType"), DataContract(IsReference = true)]
	[Serializable]
	public class Sys_DataTemplateType : EntityObject
	{
		private int _DataTemplateTypeID;

		private string _DataTemplateTypeName;

		private string _DataTemplateTypeCode;

		private string _Remark;

		[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false), DataMember]
		public int DataTemplateTypeID
		{
			get
			{
				return this._DataTemplateTypeID;
			}
			set
			{
				if (this._DataTemplateTypeID != value)
				{
					this.ReportPropertyChanging("DataTemplateTypeID");
					this._DataTemplateTypeID = StructuralObject.SetValidValue(value);
					this.ReportPropertyChanged("DataTemplateTypeID");
				}
			}
		}

		[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false), DataMember]
		public string DataTemplateTypeName
		{
			get
			{
				return this._DataTemplateTypeName;
			}
			set
			{
				this.ReportPropertyChanging("DataTemplateTypeName");
				this._DataTemplateTypeName = StructuralObject.SetValidValue(value, false);
				this.ReportPropertyChanged("DataTemplateTypeName");
			}
		}

		[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false), DataMember]
		public string DataTemplateTypeCode
		{
			get
			{
				return this._DataTemplateTypeCode;
			}
			set
			{
				this.ReportPropertyChanging("DataTemplateTypeCode");
				this._DataTemplateTypeCode = StructuralObject.SetValidValue(value, false);
				this.ReportPropertyChanged("DataTemplateTypeCode");
			}
		}

		[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false), DataMember]
		public string Remark
		{
			get
			{
				return this._Remark;
			}
			set
			{
				this.ReportPropertyChanging("Remark");
				this._Remark = StructuralObject.SetValidValue(value, false);
				this.ReportPropertyChanged("Remark");
			}
		}

		[EdmRelationshipNavigationProperty("DataTemplateModel", "FK_sys_datatemplate", "sys_datatemplate"), DataMember, SoapIgnore, XmlIgnore]
		public EntityCollection<Sys_DataTemplate> Sys_DataTemplate
		{
			get
			{
				return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<Sys_DataTemplate>("DataTemplateModel.FK_sys_datatemplate", "sys_datatemplate");
			}
			set
			{
				if (value != null)
				{
					((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<Sys_DataTemplate>("DataTemplateModel.FK_sys_datatemplate", "sys_datatemplate", value);
				}
			}
		}

		public static Sys_DataTemplateType CreateSys_DataTemplateType(int dataTemplateTypeID, string dataTemplateTypeName, string dataTemplateTypeCode, string remark)
		{
			return new Sys_DataTemplateType
			{
				DataTemplateTypeID = dataTemplateTypeID,
				DataTemplateTypeName = dataTemplateTypeName,
				DataTemplateTypeCode = dataTemplateTypeCode,
				Remark = remark
			};
		}
	}
}
