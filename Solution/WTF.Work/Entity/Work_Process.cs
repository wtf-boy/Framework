using System;
using System.ComponentModel;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace WTF.Work.Entity
{
	[EdmEntityType(NamespaceName = "WorkModel", Name = "Work_Process"), DataContract(IsReference = true)]
	[Serializable]
	public class Work_Process : EntityObject
	{
		private int _ProcessID;

		private int _WorkInfoID;

		private string _ProcessName;

		private string _ProcessConfig;

		private string _AssemblyName;

		private string _TypeName;

		private DateTime _CreateDate;

		private string _ProcessRemark;

		[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false), DataMember]
		public int ProcessID
		{
			get
			{
				return this._ProcessID;
			}
			set
			{
				if (this._ProcessID != value)
				{
					this.ReportPropertyChanging("ProcessID");
					this._ProcessID = StructuralObject.SetValidValue(value);
					this.ReportPropertyChanged("ProcessID");
				}
			}
		}

		[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false), DataMember]
		public int WorkInfoID
		{
			get
			{
				return this._WorkInfoID;
			}
			set
			{
				this.ReportPropertyChanging("WorkInfoID");
				this._WorkInfoID = StructuralObject.SetValidValue(value);
				this.ReportPropertyChanged("WorkInfoID");
			}
		}

		[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false), DataMember]
		public string ProcessName
		{
			get
			{
				return this._ProcessName;
			}
			set
			{
				this.ReportPropertyChanging("ProcessName");
				this._ProcessName = StructuralObject.SetValidValue(value, false);
				this.ReportPropertyChanged("ProcessName");
			}
		}

		[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false), DataMember]
		public string ProcessConfig
		{
			get
			{
				return this._ProcessConfig;
			}
			set
			{
				this.ReportPropertyChanging("ProcessConfig");
				this._ProcessConfig = StructuralObject.SetValidValue(value, false);
				this.ReportPropertyChanged("ProcessConfig");
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
		public DateTime CreateDate
		{
			get
			{
				return this._CreateDate;
			}
			set
			{
				this.ReportPropertyChanging("CreateDate");
				this._CreateDate = StructuralObject.SetValidValue(value);
				this.ReportPropertyChanged("CreateDate");
			}
		}

		[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false), DataMember]
		public string ProcessRemark
		{
			get
			{
				return this._ProcessRemark;
			}
			set
			{
				this.ReportPropertyChanging("ProcessRemark");
				this._ProcessRemark = StructuralObject.SetValidValue(value, false);
				this.ReportPropertyChanged("ProcessRemark");
			}
		}

		[EdmRelationshipNavigationProperty("WorkModel", "FK_WorkInfo_Ref_Process", "work_workinfo"), DataMember, SoapIgnore, XmlIgnore]
		public Work_WorkInfo Work_WorkInfo
		{
			get
			{
				return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Work_WorkInfo>("WorkModel.FK_WorkInfo_Ref_Process", "work_workinfo").Value;
			}
			set
			{
				((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Work_WorkInfo>("WorkModel.FK_WorkInfo_Ref_Process", "work_workinfo").Value = value;
			}
		}

		[Browsable(false), DataMember]
		public EntityReference<Work_WorkInfo> Work_WorkInfoReference
		{
			get
			{
				return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Work_WorkInfo>("WorkModel.FK_WorkInfo_Ref_Process", "work_workinfo");
			}
			set
			{
				if (value != null)
				{
					((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<Work_WorkInfo>("WorkModel.FK_WorkInfo_Ref_Process", "work_workinfo", value);
				}
			}
		}

		public static Work_Process CreateWork_Process(int processID, int workInfoID, string processName, string processConfig, string assemblyName, string typeName, DateTime createDate, string processRemark)
		{
			return new Work_Process
			{
				ProcessID = processID,
				WorkInfoID = workInfoID,
				ProcessName = processName,
				ProcessConfig = processConfig,
				AssemblyName = assemblyName,
				TypeName = typeName,
				CreateDate = createDate,
				ProcessRemark = processRemark
			};
		}
	}
}
