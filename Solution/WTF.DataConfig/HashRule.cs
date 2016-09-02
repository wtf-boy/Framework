using WTF.DataConfig.Entity;
using WTF.Framework;
using WTF.Logging;
using System;
using System.Data.Objects;
using System.Linq;

namespace WTF.DataConfig
{
	public class HashRule
	{
		private HashEntities objCurrentEntities;

		public HashEntities CurrentEntities
		{
			get
			{
				if (this.objCurrentEntities == null)
				{
					this.objCurrentEntities = new HashEntities(EntitiesHelper.GetConnectionString<HashEntities>());
				}
				return this.objCurrentEntities;
			}
		}

		public ObjectQuery<Sys_HashType> Sys_HashType
		{
			get
			{
				return this.CurrentEntities.sys_hashtype;
			}
		}

		public ObjectQuery<Sys_Hash> Sys_Hash
		{
			get
			{
				return this.CurrentEntities.sys_hash;
			}
		}

		public void SaveChanges()
		{
			this.CurrentEntities.SaveChanges();
		}

		public void InsertHashType(Sys_HashType objHashType)
		{
			objHashType.HashTypeCode.CheckIsNull("请输入哈希类型代码", LogModuleType.ParameterLog);
			objHashType.HashTypeName.CheckIsNull("请输入哈希类型名称", LogModuleType.ParameterLog);
			SysAssert.CheckCondition(this.Sys_HashType.Any((Sys_HashType s) => s.HashTypeCode == objHashType.HashTypeCode), "输入的代码已经存在", LogModuleType.ParameterLog);
			this.CurrentEntities.AddTosys_hashtype(objHashType);
			this.CurrentEntities.SaveChanges();
		}

		public void DeleteHashType(string hashTypeIDString)
		{
			this.CurrentEntities.sys_hashtype.DeleteData("it.HashTypeID in {" + hashTypeIDString + "}", new ObjectParameter[0]);
		}

		public void InsertHash(Sys_Hash objHash)
		{
			objHash.HashValue.CheckIsNull("请输入哈希值", LogModuleType.ParameterLog);
			objHash.HashKey.CheckIsNull("请输入哈希健", LogModuleType.ParameterLog);
			this.CurrentEntities.AddTosys_hash(objHash);
			this.CurrentEntities.SaveChanges();
		}

		public void DeleteHash(string hashIDString)
		{
			this.CurrentEntities.sys_hash.DeleteData("it.HashID in {" + hashIDString + "}", new ObjectParameter[0]);
		}

		public string GetHashValue(string hashTypeCode, string hashKey)
		{
			Sys_Hash sys_Hash = (from s in this.CurrentEntities.sys_hash
			where s.HashKey == hashKey && s.Sys_HashType.HashTypeCode == hashTypeCode
			select s).FirstOrDefault<Sys_Hash>();
			if (sys_Hash.IsNull())
			{
				SysAssert.ArgumentAssert<LogModuleType>(string.Concat(new string[]
				{
					"未设置哈希参数",
					hashTypeCode,
					"-",
					hashKey,
					"值"
				}), LogModuleType.Framework);
			}
			return sys_Hash.HashValue;
		}
	}
}
