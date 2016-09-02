using System;
using System.Data.EntityClient;
using System.Data.Objects;

namespace WTF.Logging.Entity
{
	public class LogEntities : ObjectContext
	{
		private ObjectSet<loger_category> _loger_category;

		private ObjectSet<loger_moduletype> _loger_moduletype;

		private ObjectSet<loger_operationloging> _loger_operationloging;

		private ObjectSet<loger_application> _loger_application;

		private ObjectSet<loger_operationhistory> _loger_operationhistory;

		private ObjectSet<loger_loging> _loger_loging;

		public ObjectSet<loger_category> loger_category
		{
			get
			{
				if (this._loger_category == null)
				{
					this._loger_category = base.CreateObjectSet<loger_category>("loger_category");
				}
				return this._loger_category;
			}
		}

		public ObjectSet<loger_moduletype> loger_moduletype
		{
			get
			{
				if (this._loger_moduletype == null)
				{
					this._loger_moduletype = base.CreateObjectSet<loger_moduletype>("loger_moduletype");
				}
				return this._loger_moduletype;
			}
		}

		public ObjectSet<loger_operationloging> loger_operationloging
		{
			get
			{
				if (this._loger_operationloging == null)
				{
					this._loger_operationloging = base.CreateObjectSet<loger_operationloging>("loger_operationloging");
				}
				return this._loger_operationloging;
			}
		}

		public ObjectSet<loger_application> loger_application
		{
			get
			{
				if (this._loger_application == null)
				{
					this._loger_application = base.CreateObjectSet<loger_application>("loger_application");
				}
				return this._loger_application;
			}
		}

		public ObjectSet<loger_operationhistory> loger_operationhistory
		{
			get
			{
				if (this._loger_operationhistory == null)
				{
					this._loger_operationhistory = base.CreateObjectSet<loger_operationhistory>("loger_operationhistory");
				}
				return this._loger_operationhistory;
			}
		}

		public ObjectSet<loger_loging> loger_loging
		{
			get
			{
				if (this._loger_loging == null)
				{
					this._loger_loging = base.CreateObjectSet<loger_loging>("loger_loging");
				}
				return this._loger_loging;
			}
		}

		public LogEntities() : base("name=LogEntities", "LogEntities")
		{
			base.ContextOptions.LazyLoadingEnabled = true;
		}

		public LogEntities(string connectionString) : base(connectionString, "LogEntities")
		{
			base.ContextOptions.LazyLoadingEnabled = true;
		}

		public LogEntities(EntityConnection connection) : base(connection, "LogEntities")
		{
			base.ContextOptions.LazyLoadingEnabled = true;
		}

		public void AddTologer_category(loger_category loger_category)
		{
			base.AddObject("loger_category", loger_category);
		}

		public void AddTologer_moduletype(loger_moduletype loger_moduletype)
		{
			base.AddObject("loger_moduletype", loger_moduletype);
		}

		public void AddTologer_operationloging(loger_operationloging loger_operationloging)
		{
			base.AddObject("loger_operationloging", loger_operationloging);
		}

		public void AddTologer_application(loger_application loger_application)
		{
			base.AddObject("loger_application", loger_application);
		}

		public void AddTologer_operationhistory(loger_operationhistory loger_operationhistory)
		{
			base.AddObject("loger_operationhistory", loger_operationhistory);
		}

		public void AddTologer_loging(loger_loging loger_loging)
		{
			base.AddObject("loger_loging", loger_loging);
		}
	}
}
