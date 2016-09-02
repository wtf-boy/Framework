using System;
using System.ComponentModel;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace WTF.Logging.Entity
{
	[EdmEntityType(NamespaceName = "LogModel", Name = "loger_category"), DataContract(IsReference = true)]
	[Serializable]
	public class loger_category : EntityObject
	{
		private int _CategoryID;

		private int _ApplicationID;

		private string _CategoryTypeCode;

		private string _CategoryName;

		private string _LogWriteType;

		[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false), DataMember]
		public int CategoryID
		{
			get
			{
				return this._CategoryID;
			}
			set
			{
				if (this._CategoryID != value)
				{
					this.ReportPropertyChanging("CategoryID");
					this._CategoryID = StructuralObject.SetValidValue(value);
					this.ReportPropertyChanged("CategoryID");
				}
			}
		}

		[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false), DataMember]
		public int ApplicationID
		{
			get
			{
				return this._ApplicationID;
			}
			set
			{
				this.ReportPropertyChanging("ApplicationID");
				this._ApplicationID = StructuralObject.SetValidValue(value);
				this.ReportPropertyChanged("ApplicationID");
			}
		}

		[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false), DataMember]
		public string CategoryTypeCode
		{
			get
			{
				return this._CategoryTypeCode;
			}
			set
			{
				this.ReportPropertyChanging("CategoryTypeCode");
				this._CategoryTypeCode = StructuralObject.SetValidValue(value, false);
				this.ReportPropertyChanged("CategoryTypeCode");
			}
		}

		[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false), DataMember]
		public string CategoryName
		{
			get
			{
				return this._CategoryName;
			}
			set
			{
				this.ReportPropertyChanging("CategoryName");
				this._CategoryName = StructuralObject.SetValidValue(value, false);
				this.ReportPropertyChanged("CategoryName");
			}
		}

		[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false), DataMember]
		public string LogWriteType
		{
			get
			{
				return this._LogWriteType;
			}
			set
			{
				this.ReportPropertyChanging("LogWriteType");
				this._LogWriteType = StructuralObject.SetValidValue(value, false);
				this.ReportPropertyChanged("LogWriteType");
			}
		}

		[EdmRelationshipNavigationProperty("LogModel", "Fk_Application_Category", "loger_application"), DataMember, SoapIgnore, XmlIgnore]
		public loger_application loger_application
		{
			get
			{
				return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<loger_application>("LogModel.Fk_Application_Category", "loger_application").Value;
			}
			set
			{
				((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<loger_application>("LogModel.Fk_Application_Category", "loger_application").Value = value;
			}
		}

		[Browsable(false), DataMember]
		public EntityReference<loger_application> loger_applicationReference
		{
			get
			{
				return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<loger_application>("LogModel.Fk_Application_Category", "loger_application");
			}
			set
			{
				if (value != null)
				{
					((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<loger_application>("LogModel.Fk_Application_Category", "loger_application", value);
				}
			}
		}

		public static loger_category Createloger_category(int categoryID, int applicationID, string categoryTypeCode, string categoryName, string logWriteType)
		{
			return new loger_category
			{
				CategoryID = categoryID,
				ApplicationID = applicationID,
				CategoryTypeCode = categoryTypeCode,
				CategoryName = categoryName,
				LogWriteType = logWriteType
			};
		}
	}
}
