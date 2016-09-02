using System;
using System.ComponentModel;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace WTF.DataConfig.Entity
{
	[EdmEntityType(NamespaceName = "HashModel", Name = "Sys_Hash"), DataContract(IsReference = true)]
	[Serializable]
	public class Sys_Hash : EntityObject
	{
		private int _HashID;

		private int _HashTypeID;

		private string _HashKey;

		private string _HashValue;

		private string _Remark;

		[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false), DataMember]
		public int HashID
		{
			get
			{
				return this._HashID;
			}
			set
			{
				if (this._HashID != value)
				{
					this.ReportPropertyChanging("HashID");
					this._HashID = StructuralObject.SetValidValue(value);
					this.ReportPropertyChanged("HashID");
				}
			}
		}

		[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false), DataMember]
		public int HashTypeID
		{
			get
			{
				return this._HashTypeID;
			}
			set
			{
				this.ReportPropertyChanging("HashTypeID");
				this._HashTypeID = StructuralObject.SetValidValue(value);
				this.ReportPropertyChanged("HashTypeID");
			}
		}

		[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false), DataMember]
		public string HashKey
		{
			get
			{
				return this._HashKey;
			}
			set
			{
				this.ReportPropertyChanging("HashKey");
				this._HashKey = StructuralObject.SetValidValue(value, false);
				this.ReportPropertyChanged("HashKey");
			}
		}

		[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false), DataMember]
		public string HashValue
		{
			get
			{
				return this._HashValue;
			}
			set
			{
				this.ReportPropertyChanging("HashValue");
				this._HashValue = StructuralObject.SetValidValue(value, false);
				this.ReportPropertyChanged("HashValue");
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

		[EdmRelationshipNavigationProperty("HashModel", "sys_hashsys_hashtype", "sys_hashtype"), DataMember, SoapIgnore, XmlIgnore]
		public Sys_HashType Sys_HashType
		{
			get
			{
				return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Sys_HashType>("HashModel.sys_hashsys_hashtype", "sys_hashtype").Value;
			}
			set
			{
				((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Sys_HashType>("HashModel.sys_hashsys_hashtype", "sys_hashtype").Value = value;
			}
		}

		[Browsable(false), DataMember]
		public EntityReference<Sys_HashType> Sys_HashTypeReference
		{
			get
			{
				return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Sys_HashType>("HashModel.sys_hashsys_hashtype", "sys_hashtype");
			}
			set
			{
				if (value != null)
				{
					((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<Sys_HashType>("HashModel.sys_hashsys_hashtype", "sys_hashtype", value);
				}
			}
		}

		public static Sys_Hash CreateSys_Hash(int hashID, int hashTypeID, string hashKey, string hashValue, string remark)
		{
			return new Sys_Hash
			{
				HashID = hashID,
				HashTypeID = hashTypeID,
				HashKey = hashKey,
				HashValue = hashValue,
				Remark = remark
			};
		}
	}
}
