using System;
using System.Data.EntityClient;
using System.Data.Objects;

namespace WTF.DataConfig.Entity
{
	public class DataTemplateEntities : ObjectContext
	{
		private ObjectSet<Sys_DataTemplate> _sys_datatemplate;

		private ObjectSet<Sys_DataTemplateType> _sys_datatemplatetype;

		public ObjectSet<Sys_DataTemplate> sys_datatemplate
		{
			get
			{
				if (this._sys_datatemplate == null)
				{
					this._sys_datatemplate = base.CreateObjectSet<Sys_DataTemplate>("sys_datatemplate");
				}
				return this._sys_datatemplate;
			}
		}

		public ObjectSet<Sys_DataTemplateType> sys_datatemplatetype
		{
			get
			{
				if (this._sys_datatemplatetype == null)
				{
					this._sys_datatemplatetype = base.CreateObjectSet<Sys_DataTemplateType>("sys_datatemplatetype");
				}
				return this._sys_datatemplatetype;
			}
		}

		public DataTemplateEntities() : base("name=DataTemplateEntities", "DataTemplateEntities")
		{
			base.ContextOptions.LazyLoadingEnabled = true;
		}

		public DataTemplateEntities(string connectionString) : base(connectionString, "DataTemplateEntities")
		{
			base.ContextOptions.LazyLoadingEnabled = true;
		}

		public DataTemplateEntities(EntityConnection connection) : base(connection, "DataTemplateEntities")
		{
			base.ContextOptions.LazyLoadingEnabled = true;
		}

		public void AddTosys_datatemplate(Sys_DataTemplate sys_DataTemplate)
		{
			base.AddObject("sys_datatemplate", sys_DataTemplate);
		}

		public void AddTosys_datatemplatetype(Sys_DataTemplateType sys_DataTemplateType)
		{
			base.AddObject("sys_datatemplatetype", sys_DataTemplateType);
		}
	}
}
