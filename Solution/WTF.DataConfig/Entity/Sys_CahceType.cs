using System;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;

namespace WTF.DataConfig.Entity
{
	[EdmEntityType(NamespaceName = "CacheModel", Name = "Sys_CahceType"), DataContract(IsReference = true)]
	[Serializable]
	public class Sys_CahceType : EntityObject
	{
		private int _CahceTypeID;

		private string _CahceTypeName;

		private string _CahceTypeCode;

		private string _Remark;

		[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false), DataMember]
		public int CahceTypeID
		{
			get
			{
				return this._CahceTypeID;
			}
			set
			{
				if (this._CahceTypeID != value)
				{
					this.ReportPropertyChanging("CahceTypeID");
					this._CahceTypeID = StructuralObject.SetValidValue(value);
					this.ReportPropertyChanged("CahceTypeID");
				}
			}
		}

		[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false), DataMember]
		public string CahceTypeName
		{
			get
			{
				return this._CahceTypeName;
			}
			set
			{
				this.ReportPropertyChanging("CahceTypeName");
				this._CahceTypeName = StructuralObject.SetValidValue(value, false);
				this.ReportPropertyChanged("CahceTypeName");
			}
		}

		[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false), DataMember]
		public string CahceTypeCode
		{
			get
			{
				return this._CahceTypeCode;
			}
			set
			{
				this.ReportPropertyChanging("CahceTypeCode");
				this._CahceTypeCode = StructuralObject.SetValidValue(value, false);
				this.ReportPropertyChanged("CahceTypeCode");
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

		public static Sys_CahceType CreateSys_CahceType(int cahceTypeID, string cahceTypeName, string cahceTypeCode, string remark)
		{
			return new Sys_CahceType
			{
				CahceTypeID = cahceTypeID,
				CahceTypeName = cahceTypeName,
				CahceTypeCode = cahceTypeCode,
				Remark = remark
			};
		}
	}
}
