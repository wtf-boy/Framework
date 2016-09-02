using System;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace WTF.DataConfig.Entity
{
	[EdmEntityType(NamespaceName = "ParameterModel", Name = "Sys_ParameterType"), DataContract(IsReference = true)]
	[Serializable]
	public class Sys_ParameterType : EntityObject
	{
		private int _ParameterTypeID;

		private string _ParameterTypeName;

		private string _ParameterTypeCode;

		private string _AssemblyName;

		private string _TypeName;

		private string _Remark;

		[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false), DataMember]
		public int ParameterTypeID
		{
			get
			{
				return this._ParameterTypeID;
			}
			set
			{
				if (this._ParameterTypeID != value)
				{
					this.ReportPropertyChanging("ParameterTypeID");
					this._ParameterTypeID = StructuralObject.SetValidValue(value);
					this.ReportPropertyChanged("ParameterTypeID");
				}
			}
		}

		[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false), DataMember]
		public string ParameterTypeName
		{
			get
			{
				return this._ParameterTypeName;
			}
			set
			{
				this.ReportPropertyChanging("ParameterTypeName");
				this._ParameterTypeName = StructuralObject.SetValidValue(value, false);
				this.ReportPropertyChanged("ParameterTypeName");
			}
		}

		[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false), DataMember]
		public string ParameterTypeCode
		{
			get
			{
				return this._ParameterTypeCode;
			}
			set
			{
				this.ReportPropertyChanging("ParameterTypeCode");
				this._ParameterTypeCode = StructuralObject.SetValidValue(value, false);
				this.ReportPropertyChanged("ParameterTypeCode");
			}
		}

		[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false), DataMember]
		public string AssemblyName
		{
			get
			{
				return this._AssemblyName;
			}
			set
			{
				this.ReportPropertyChanging("AssemblyName");
				this._AssemblyName = StructuralObject.SetValidValue(value, false);
				this.ReportPropertyChanged("AssemblyName");
			}
		}

		[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false), DataMember]
		public string TypeName
		{
			get
			{
				return this._TypeName;
			}
			set
			{
				this.ReportPropertyChanging("TypeName");
				this._TypeName = StructuralObject.SetValidValue(value, false);
				this.ReportPropertyChanged("TypeName");
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

		[EdmRelationshipNavigationProperty("ParameterModel", "FK_Sys_Parameter_Sys_ParameterType", "sys_parameter"), DataMember, SoapIgnore, XmlIgnore]
		public EntityCollection<Sys_Parameter> Sys_Parameter
		{
			get
			{
				return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<Sys_Parameter>("ParameterModel.FK_Sys_Parameter_Sys_ParameterType", "sys_parameter");
			}
			set
			{
				if (value != null)
				{
					((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<Sys_Parameter>("ParameterModel.FK_Sys_Parameter_Sys_ParameterType", "sys_parameter", value);
				}
			}
		}

		public static Sys_ParameterType CreateSys_ParameterType(int parameterTypeID, string parameterTypeName, string parameterTypeCode, string assemblyName, string typeName, string remark)
		{
			return new Sys_ParameterType
			{
				ParameterTypeID = parameterTypeID,
				ParameterTypeName = parameterTypeName,
				ParameterTypeCode = parameterTypeCode,
				AssemblyName = assemblyName,
				TypeName = typeName,
				Remark = remark
			};
		}
	}
}
