using System;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;

namespace WTF.Logging.Entity
{
	[EdmEntityType(NamespaceName = "LogModel", Name = "loger_moduletype"), DataContract(IsReference = true)]
	[Serializable]
	public class loger_moduletype : EntityObject
	{
		private int _ModuleTypeID;

		private string _ModuleTypeCode;

		private string _ModuleTypeName;

		private int _ApplicationID;

		[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false), DataMember]
		public int ModuleTypeID
		{
			get
			{
				return this._ModuleTypeID;
			}
			set
			{
				if (this._ModuleTypeID != value)
				{
					this.ReportPropertyChanging("ModuleTypeID");
					this._ModuleTypeID = StructuralObject.SetValidValue(value);
					this.ReportPropertyChanged("ModuleTypeID");
				}
			}
		}

		[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false), DataMember]
		public string ModuleTypeCode
		{
			get
			{
				return this._ModuleTypeCode;
			}
			set
			{
				this.ReportPropertyChanging("ModuleTypeCode");
				this._ModuleTypeCode = StructuralObject.SetValidValue(value, false);
				this.ReportPropertyChanged("ModuleTypeCode");
			}
		}

		[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false), DataMember]
		public string ModuleTypeName
		{
			get
			{
				return this._ModuleTypeName;
			}
			set
			{
				this.ReportPropertyChanging("ModuleTypeName");
				this._ModuleTypeName = StructuralObject.SetValidValue(value, false);
				this.ReportPropertyChanged("ModuleTypeName");
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

		public static loger_moduletype Createloger_moduletype(int moduleTypeID, string moduleTypeCode, string moduleTypeName, int applicationID)
		{
			return new loger_moduletype
			{
				ModuleTypeID = moduleTypeID,
				ModuleTypeCode = moduleTypeCode,
				ModuleTypeName = moduleTypeName,
				ApplicationID = applicationID
			};
		}
	}
}
