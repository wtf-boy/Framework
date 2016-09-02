using System;
using System.Data.EntityClient;
using System.Data.Objects;

namespace WTF.DataConfig.Entity
{
	public class HashEntities : ObjectContext
	{
		private ObjectSet<Sys_Hash> _sys_hash;

		private ObjectSet<Sys_HashType> _sys_hashtype;

		public ObjectSet<Sys_Hash> sys_hash
		{
			get
			{
				if (this._sys_hash == null)
				{
					this._sys_hash = base.CreateObjectSet<Sys_Hash>("sys_hash");
				}
				return this._sys_hash;
			}
		}

		public ObjectSet<Sys_HashType> sys_hashtype
		{
			get
			{
				if (this._sys_hashtype == null)
				{
					this._sys_hashtype = base.CreateObjectSet<Sys_HashType>("sys_hashtype");
				}
				return this._sys_hashtype;
			}
		}

		public HashEntities() : base("name=HashEntities", "HashEntities")
		{
			base.ContextOptions.LazyLoadingEnabled = true;
		}

		public HashEntities(string connectionString) : base(connectionString, "HashEntities")
		{
			base.ContextOptions.LazyLoadingEnabled = true;
		}

		public HashEntities(EntityConnection connection) : base(connection, "HashEntities")
		{
			base.ContextOptions.LazyLoadingEnabled = true;
		}

		public void AddTosys_hash(Sys_Hash sys_Hash)
		{
			base.AddObject("sys_hash", sys_Hash);
		}

		public void AddTosys_hashtype(Sys_HashType sys_HashType)
		{
			base.AddObject("sys_hashtype", sys_HashType);
		}
	}
}
