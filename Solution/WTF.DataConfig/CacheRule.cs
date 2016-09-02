using WTF.DataConfig.Entity;
using WTF.Framework;
using WTF.Logging;
using System;
using System.Data.Objects;

namespace WTF.DataConfig
{
	public class CacheRule
	{
		private CacheEntities objCurrentEntities;

		public CacheEntities CurrentEntities
		{
			get
			{
				if (this.objCurrentEntities == null)
				{
					this.objCurrentEntities = new CacheEntities(EntitiesHelper.GetConnectionString<CacheEntities>());
				}
				return this.objCurrentEntities;
			}
		}

		public ObjectQuery<Sys_CahceType> Sys_CahceType
		{
			get
			{
				return this.CurrentEntities.sys_cahcetype;
			}
		}

		public void InsertCahceType(Sys_CahceType objSys_CahceType)
		{
			objSys_CahceType.CahceTypeName.CheckIsNull("请输入缓存类型名称", "ParameterLog");
			objSys_CahceType.CahceTypeCode.CheckIsNull("请输入缓存类型代码", "ParameterLog");
			objSys_CahceType.Remark.CheckIsNull("请输入缓存备注", "ParameterLog");
			this.CurrentEntities.AddTosys_cahcetype(objSys_CahceType);
			this.CurrentEntities.SaveChanges();
		}

		public void UpdateCahceType(Sys_CahceType objSys_CahceType)
		{
			objSys_CahceType.CahceTypeName.CheckIsNull("请输入缓存类型名称", "ParameterLog");
			objSys_CahceType.CahceTypeCode.CheckIsNull("请输入缓存类型代码", "ParameterLog");
			objSys_CahceType.Remark.CheckIsNull("请输入缓存备注", "ParameterLog");
			this.CurrentEntities.SaveChanges();
		}

		public void DeleteCahceTypeByKey(string primaryKey)
		{
			this.CurrentEntities.sys_cahcetype.DeleteDataPrimaryKey(primaryKey);
		}

		public void DeleteCahceType(string condition)
		{
			this.CurrentEntities.sys_cahcetype.DeleteData(condition, new ObjectParameter[0]);
		}
	}
}
