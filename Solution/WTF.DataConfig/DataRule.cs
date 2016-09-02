using WTF.DataConfig.Entity;
using WTF.Framework;
using WTF.Logging;
using System;
using System.Data.Objects;

namespace WTF.DataConfig
{
	public class DataRule
	{
		private DataEntities objCurrentEntities;

		public DataEntities CurrentEntities
		{
			get
			{
				if (this.objCurrentEntities == null)
				{
					this.objCurrentEntities = new DataEntities(EntitiesHelper.GetConnectionString<DataEntities>());
				}
				return this.objCurrentEntities;
			}
		}

		public ObjectQuery<Sys_DataField> Sys_DataField
		{
			get
			{
				return this.CurrentEntities.sys_datafield;
			}
		}

		public ObjectQuery<Sys_DataType> Sys_DataType
		{
			get
			{
				return this.CurrentEntities.sys_datatype;
			}
		}

		public void InsertDataField(Sys_DataField objSys_DataField)
		{
			objSys_DataField.DataValue.CheckIsNull("请输入数据值", "ParameterLog");
			objSys_DataField.DataTitle.CheckIsNull("请输入数据名称", "ParameterLog");
			this.CurrentEntities.AddTosys_datafield(objSys_DataField);
			this.CurrentEntities.SaveChanges();
		}

		public void UpdateDataField(Sys_DataField objSys_DataField)
		{
			objSys_DataField.DataValue.CheckIsNull("请输入数据值", "ParameterLog");
			objSys_DataField.DataTitle.CheckIsNull("请输入数据名称", "ParameterLog");
			this.CurrentEntities.SaveChanges();
		}

		public void DeleteDataFieldByKey(string primaryKey)
		{
			this.CurrentEntities.sys_datafield.DeleteDataPrimaryKey(primaryKey);
		}

		public void DeleteDataField(string condition)
		{
			this.CurrentEntities.sys_datafield.DeleteData(condition, new ObjectParameter[0]);
		}

		public void InsertDataType(Sys_DataType objSys_DataType)
		{
			objSys_DataType.DataCode.CheckIsNull("请输入数据验证编码", "ParameterLog");
			objSys_DataType.DataName.CheckIsNull("请输入数据验证名称", "ParameterLog");
			this.CurrentEntities.AddTosys_datatype(objSys_DataType);
			this.CurrentEntities.SaveChanges();
		}

		public void UpdateDataType(Sys_DataType objSys_DataType)
		{
			objSys_DataType.DataCode.CheckIsNull("请输入数据验证编码", "ParameterLog");
			objSys_DataType.DataName.CheckIsNull("请输入数据验证名称", "ParameterLog");
			this.CurrentEntities.SaveChanges();
		}

		public void DeleteDataTypeByKey(string primaryKey)
		{
			this.CurrentEntities.sys_datatype.DeleteDataPrimaryKey(primaryKey);
		}

		public void DeleteDataType(string condition)
		{
			this.CurrentEntities.sys_datatype.DeleteData(condition, new ObjectParameter[0]);
		}
	}
}
