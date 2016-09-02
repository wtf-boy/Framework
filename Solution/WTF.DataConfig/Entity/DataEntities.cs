using System;
using System.Data.EntityClient;
using System.Data.Objects;

namespace WTF.DataConfig.Entity
{
	public class DataEntities : ObjectContext
	{
		private ObjectSet<Sys_DataField> _sys_datafield;

		private ObjectSet<Sys_DataType> _sys_datatype;

		public ObjectSet<Sys_DataField> sys_datafield
		{
			get
			{
				if (this._sys_datafield == null)
				{
					this._sys_datafield = base.CreateObjectSet<Sys_DataField>("sys_datafield");
				}
				return this._sys_datafield;
			}
		}

		public ObjectSet<Sys_DataType> sys_datatype
		{
			get
			{
				if (this._sys_datatype == null)
				{
					this._sys_datatype = base.CreateObjectSet<Sys_DataType>("sys_datatype");
				}
				return this._sys_datatype;
			}
		}

		public DataEntities() : base("name=DataEntities", "DataEntities")
		{
			base.ContextOptions.LazyLoadingEnabled = true;
		}

		public DataEntities(string connectionString) : base(connectionString, "DataEntities")
		{
			base.ContextOptions.LazyLoadingEnabled = true;
		}

		public DataEntities(EntityConnection connection) : base(connection, "DataEntities")
		{
			base.ContextOptions.LazyLoadingEnabled = true;
		}

		public void AddTosys_datafield(Sys_DataField sys_DataField)
		{
			base.AddObject("sys_datafield", sys_DataField);
		}

		public void AddTosys_datatype(Sys_DataType sys_DataType)
		{
			base.AddObject("sys_datatype", sys_DataType);
		}
	}
}
