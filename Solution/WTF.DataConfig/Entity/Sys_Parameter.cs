using System;
using System.ComponentModel;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace WTF.DataConfig.Entity
{
	[EdmEntityType(NamespaceName = "ParameterModel", Name = "Sys_Parameter"), DataContract(IsReference = true)]
	[Serializable]
	public class Sys_Parameter : EntityObject
	{
		private int _ParameterID;

		private int _ParameterTypeID;

		private string _ParameterName;

		private string _ParameterCode;

		private int _ParameterCodeID;

		private bool _IsEnable;

		private int _SortIndex;

		private string _Remark;

		[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false), DataMember]
		public int ParameterID
		{
			get
			{
				return this._ParameterID;
			}
			set
			{
				if (this._ParameterID != value)
				{
					this.ReportPropertyChanging("ParameterID");
					this._ParameterID = StructuralObject.SetValidValue(value);
					this.ReportPropertyChanged("ParameterID");
				}
			}
		}

		[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false), DataMember]
		public int ParameterTypeID
		{
			get
			{
				return this._ParameterTypeID;
			}
			set
			{
				this.ReportPropertyChanging("ParameterTypeID");
				this._ParameterTypeID = StructuralObject.SetValidValue(value);
				this.ReportPropertyChanged("ParameterTypeID");
			}
		}

		[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false), DataMember]
		public string ParameterName
		{
			get
			{
				return this._ParameterName;
			}
			set
			{
				this.ReportPropertyChanging("ParameterName");
				this._ParameterName = StructuralObject.SetValidValue(value, false);
				this.ReportPropertyChanged("ParameterName");
			}
		}

		[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false), DataMember]
		public string ParameterCode
		{
			get
			{
				return this._ParameterCode;
			}
			set
			{
				this.ReportPropertyChanging("ParameterCode");
				this._ParameterCode = StructuralObject.SetValidValue(value, false);
				this.ReportPropertyChanged("ParameterCode");
			}
		}

		[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false), DataMember]
		public int ParameterCodeID
		{
			get
			{
				return this._ParameterCodeID;
			}
			set
			{
				this.ReportPropertyChanging("ParameterCodeID");
				this._ParameterCodeID = StructuralObject.SetValidValue(value);
				this.ReportPropertyChanged("ParameterCodeID");
			}
		}

		[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false), DataMember]
		public bool IsEnable
		{
			get
			{
				return this._IsEnable;
			}
			set
			{
				this.ReportPropertyChanging("IsEnable");
				this._IsEnable = StructuralObject.SetValidValue(value);
				this.ReportPropertyChanged("IsEnable");
			}
		}

		[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false), DataMember]
		public int SortIndex
		{
			get
			{
				return this._SortIndex;
			}
			set
			{
				this.ReportPropertyChanging("SortIndex");
				this._SortIndex = StructuralObject.SetValidValue(value);
				this.ReportPropertyChanged("SortIndex");
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

		[EdmRelationshipNavigationProperty("ParameterModel", "FK_Sys_Parameter_Sys_ParameterType", "sys_parametertype"), DataMember, SoapIgnore, XmlIgnore]
		public Sys_ParameterType Sys_ParameterType
		{
			get
			{
				return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Sys_ParameterType>("ParameterModel.FK_Sys_Parameter_Sys_ParameterType", "sys_parametertype").Value;
			}
			set
			{
				((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Sys_ParameterType>("ParameterModel.FK_Sys_Parameter_Sys_ParameterType", "sys_parametertype").Value = value;
			}
		}

		[Browsable(false), DataMember]
		public EntityReference<Sys_ParameterType> Sys_ParameterTypeReference
		{
			get
			{
				return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Sys_ParameterType>("ParameterModel.FK_Sys_Parameter_Sys_ParameterType", "sys_parametertype");
			}
			set
			{
				if (value != null)
				{
					((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<Sys_ParameterType>("ParameterModel.FK_Sys_Parameter_Sys_ParameterType", "sys_parametertype", value);
				}
			}
		}

		public static Sys_Parameter CreateSys_Parameter(int parameterID, int parameterTypeID, string parameterName, string parameterCode, int parameterCodeID, bool isEnable, int sortIndex, string remark)
		{
			return new Sys_Parameter
			{
				ParameterID = parameterID,
				ParameterTypeID = parameterTypeID,
				ParameterName = parameterName,
				ParameterCode = parameterCode,
				ParameterCodeID = parameterCodeID,
				IsEnable = isEnable,
				SortIndex = sortIndex,
				Remark = remark
			};
		}
	}
}
