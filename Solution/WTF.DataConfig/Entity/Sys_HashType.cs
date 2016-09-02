using System;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace WTF.DataConfig.Entity
{
	[EdmEntityType(NamespaceName = "HashModel", Name = "Sys_HashType"), DataContract(IsReference = true)]
	[Serializable]
	public class Sys_HashType : EntityObject
	{
		private int _HashTypeID;

		private string _HashTypeName;

		private string _HashTypeCode;

		private string _Remark;

		[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false), DataMember]
		public int HashTypeID
		{
			get
			{
				return this._HashTypeID;
			}
			set
			{
				if (this._HashTypeID != value)
				{
					this.ReportPropertyChanging("HashTypeID");
					this._HashTypeID = StructuralObject.SetValidValue(value);
					this.ReportPropertyChanged("HashTypeID");
				}
			}
		}

		[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false), DataMember]
		public string HashTypeName
		{
			get
			{
				return this._HashTypeName;
			}
			set
			{
				this.ReportPropertyChanging("HashTypeName");
				this._HashTypeName = StructuralObject.SetValidValue(value, false);
				this.ReportPropertyChanged("HashTypeName");
			}
		}

		[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false), DataMember]
		public string HashTypeCode
		{
			get
			{
				return this._HashTypeCode;
			}
			set
			{
				this.ReportPropertyChanging("HashTypeCode");
				this._HashTypeCode = StructuralObject.SetValidValue(value, false);
				this.ReportPropertyChanged("HashTypeCode");
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

		[EdmRelationshipNavigationProperty("HashModel", "sys_hashsys_hashtype", "sys_hash"), DataMember, SoapIgnore, XmlIgnore]
		public EntityCollection<Sys_Hash> Sys_Hash
		{
			get
			{
				return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<Sys_Hash>("HashModel.sys_hashsys_hashtype", "sys_hash");
			}
			set
			{
				if (value != null)
				{
					((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<Sys_Hash>("HashModel.sys_hashsys_hashtype", "sys_hash", value);
				}
			}
		}

		public static Sys_HashType CreateSys_HashType(int hashTypeID, string hashTypeName, string hashTypeCode, string remark)
		{
			return new Sys_HashType
			{
				HashTypeID = hashTypeID,
				HashTypeName = hashTypeName,
				HashTypeCode = hashTypeCode,
				Remark = remark
			};
		}
	}
}
