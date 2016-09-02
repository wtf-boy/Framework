using System;
using System.ComponentModel;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace WTF.DataConfig.Entity
{
	[EdmEntityType(NamespaceName = "DataTemplateModel", Name = "Sys_DataTemplate"), DataContract(IsReference = true)]
	[Serializable]
	public class Sys_DataTemplate : EntityObject
	{
		private int _DataTemplateID;

		private int _DataTemplateTypeID;

		private string _TemplateName;

		private string _TemplateCode;

		private string _TemplateContent;

		private string _Remark;

		[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false), DataMember]
		public int DataTemplateID
		{
			get
			{
				return this._DataTemplateID;
			}
			set
			{
				if (this._DataTemplateID != value)
				{
					this.ReportPropertyChanging("DataTemplateID");
					this._DataTemplateID = StructuralObject.SetValidValue(value);
					this.ReportPropertyChanged("DataTemplateID");
				}
			}
		}

		[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false), DataMember]
		public int DataTemplateTypeID
		{
			get
			{
				return this._DataTemplateTypeID;
			}
			set
			{
				this.ReportPropertyChanging("DataTemplateTypeID");
				this._DataTemplateTypeID = StructuralObject.SetValidValue(value);
				this.ReportPropertyChanged("DataTemplateTypeID");
			}
		}

		[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false), DataMember]
		public string TemplateName
		{
			get
			{
				return this._TemplateName;
			}
			set
			{
				this.ReportPropertyChanging("TemplateName");
				this._TemplateName = StructuralObject.SetValidValue(value, false);
				this.ReportPropertyChanged("TemplateName");
			}
		}

		[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false), DataMember]
		public string TemplateCode
		{
			get
			{
				return this._TemplateCode;
			}
			set
			{
				this.ReportPropertyChanging("TemplateCode");
				this._TemplateCode = StructuralObject.SetValidValue(value, false);
				this.ReportPropertyChanged("TemplateCode");
			}
		}

		[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false), DataMember]
		public string TemplateContent
		{
			get
			{
				return this._TemplateContent;
			}
			set
			{
				this.ReportPropertyChanging("TemplateContent");
				this._TemplateContent = StructuralObject.SetValidValue(value, false);
				this.ReportPropertyChanged("TemplateContent");
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

		[EdmRelationshipNavigationProperty("DataTemplateModel", "FK_sys_datatemplate", "sys_datatemplatetype"), DataMember, SoapIgnore, XmlIgnore]
		public Sys_DataTemplateType Sys_DataTemplateType
		{
			get
			{
				return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Sys_DataTemplateType>("DataTemplateModel.FK_sys_datatemplate", "sys_datatemplatetype").Value;
			}
			set
			{
				((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Sys_DataTemplateType>("DataTemplateModel.FK_sys_datatemplate", "sys_datatemplatetype").Value = value;
			}
		}

		[Browsable(false), DataMember]
		public EntityReference<Sys_DataTemplateType> Sys_DataTemplateTypeReference
		{
			get
			{
				return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Sys_DataTemplateType>("DataTemplateModel.FK_sys_datatemplate", "sys_datatemplatetype");
			}
			set
			{
				if (value != null)
				{
					((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<Sys_DataTemplateType>("DataTemplateModel.FK_sys_datatemplate", "sys_datatemplatetype", value);
				}
			}
		}

		public static Sys_DataTemplate CreateSys_DataTemplate(int dataTemplateID, int dataTemplateTypeID, string templateName, string templateCode, string templateContent, string remark)
		{
			return new Sys_DataTemplate
			{
				DataTemplateID = dataTemplateID,
				DataTemplateTypeID = dataTemplateTypeID,
				TemplateName = templateName,
				TemplateCode = templateCode,
				TemplateContent = templateContent,
				Remark = remark
			};
		}
	}
}
