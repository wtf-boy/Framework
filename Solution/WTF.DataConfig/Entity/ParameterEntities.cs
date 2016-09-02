using System;
using System.Data.EntityClient;
using System.Data.Objects;

namespace WTF.DataConfig.Entity
{
	public class ParameterEntities : ObjectContext
	{
		private ObjectSet<Sys_Parameter> _sys_parameter;

		private ObjectSet<Sys_ParameterType> _sys_parametertype;

		public ObjectSet<Sys_Parameter> sys_parameter
		{
			get
			{
				if (this._sys_parameter == null)
				{
					this._sys_parameter = base.CreateObjectSet<Sys_Parameter>("sys_parameter");
				}
				return this._sys_parameter;
			}
		}

		public ObjectSet<Sys_ParameterType> sys_parametertype
		{
			get
			{
				if (this._sys_parametertype == null)
				{
					this._sys_parametertype = base.CreateObjectSet<Sys_ParameterType>("sys_parametertype");
				}
				return this._sys_parametertype;
			}
		}

		public ParameterEntities() : base("name=ParameterEntities", "ParameterEntities")
		{
			base.ContextOptions.LazyLoadingEnabled = true;
		}

		public ParameterEntities(string connectionString) : base(connectionString, "ParameterEntities")
		{
			base.ContextOptions.LazyLoadingEnabled = true;
		}

		public ParameterEntities(EntityConnection connection) : base(connection, "ParameterEntities")
		{
			base.ContextOptions.LazyLoadingEnabled = true;
		}

		public void AddTosys_parameter(Sys_Parameter sys_Parameter)
		{
			base.AddObject("sys_parameter", sys_Parameter);
		}

		public void AddTosys_parametertype(Sys_ParameterType sys_ParameterType)
		{
			base.AddObject("sys_parametertype", sys_ParameterType);
		}
	}
}
