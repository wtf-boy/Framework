using System;
using System.Data.EntityClient;
using System.Data.Objects;

namespace WTF.Work.Entity
{
	public class WorkEntities : ObjectContext
	{
		private ObjectSet<Work_NotifyAddress> _work_notifyaddress;

		private ObjectSet<Work_Plan> _work_plan;

		private ObjectSet<Work_PlanNotify> _work_plannotify;

		private ObjectSet<Work_PlanRun> _work_planrun;

		private ObjectSet<Work_PlanStep> _work_planstep;

		private ObjectSet<Work_Process> _work_process;

		private ObjectSet<Work_WorkInfo> _work_workinfo;

		private ObjectSet<Work_WorkLog> _work_worklog;

		private ObjectSet<Work_WorkProcessLog> _work_workprocesslog;

		private ObjectSet<Work_PlanNotifyInfo> _work_plannotifyinfo;

		private ObjectSet<Work_PlanStepInfo> _work_planstepinfo;

		private ObjectSet<Work_RunProcessInfo> _work_runprocessinfo;

		private ObjectSet<Work_WorkLogInfo> _work_workloginfo;

		private ObjectSet<Work_WorkProcessLogInfo> _work_workprocessloginfo;

		private ObjectSet<Work_RunNextInfo> _work_runnextinfo;

		public ObjectSet<Work_NotifyAddress> work_notifyaddress
		{
			get
			{
				if (this._work_notifyaddress == null)
				{
					this._work_notifyaddress = base.CreateObjectSet<Work_NotifyAddress>("work_notifyaddress");
				}
				return this._work_notifyaddress;
			}
		}

		public ObjectSet<Work_Plan> work_plan
		{
			get
			{
				if (this._work_plan == null)
				{
					this._work_plan = base.CreateObjectSet<Work_Plan>("work_plan");
				}
				return this._work_plan;
			}
		}

		public ObjectSet<Work_PlanNotify> work_plannotify
		{
			get
			{
				if (this._work_plannotify == null)
				{
					this._work_plannotify = base.CreateObjectSet<Work_PlanNotify>("work_plannotify");
				}
				return this._work_plannotify;
			}
		}

		public ObjectSet<Work_PlanRun> work_planrun
		{
			get
			{
				if (this._work_planrun == null)
				{
					this._work_planrun = base.CreateObjectSet<Work_PlanRun>("work_planrun");
				}
				return this._work_planrun;
			}
		}

		public ObjectSet<Work_PlanStep> work_planstep
		{
			get
			{
				if (this._work_planstep == null)
				{
					this._work_planstep = base.CreateObjectSet<Work_PlanStep>("work_planstep");
				}
				return this._work_planstep;
			}
		}

		public ObjectSet<Work_Process> work_process
		{
			get
			{
				if (this._work_process == null)
				{
					this._work_process = base.CreateObjectSet<Work_Process>("work_process");
				}
				return this._work_process;
			}
		}

		public ObjectSet<Work_WorkInfo> work_workinfo
		{
			get
			{
				if (this._work_workinfo == null)
				{
					this._work_workinfo = base.CreateObjectSet<Work_WorkInfo>("work_workinfo");
				}
				return this._work_workinfo;
			}
		}

		public ObjectSet<Work_WorkLog> work_worklog
		{
			get
			{
				if (this._work_worklog == null)
				{
					this._work_worklog = base.CreateObjectSet<Work_WorkLog>("work_worklog");
				}
				return this._work_worklog;
			}
		}

		public ObjectSet<Work_WorkProcessLog> work_workprocesslog
		{
			get
			{
				if (this._work_workprocesslog == null)
				{
					this._work_workprocesslog = base.CreateObjectSet<Work_WorkProcessLog>("work_workprocesslog");
				}
				return this._work_workprocesslog;
			}
		}

		public ObjectSet<Work_PlanNotifyInfo> work_plannotifyinfo
		{
			get
			{
				if (this._work_plannotifyinfo == null)
				{
					this._work_plannotifyinfo = base.CreateObjectSet<Work_PlanNotifyInfo>("work_plannotifyinfo");
				}
				return this._work_plannotifyinfo;
			}
		}

		public ObjectSet<Work_PlanStepInfo> work_planstepinfo
		{
			get
			{
				if (this._work_planstepinfo == null)
				{
					this._work_planstepinfo = base.CreateObjectSet<Work_PlanStepInfo>("work_planstepinfo");
				}
				return this._work_planstepinfo;
			}
		}

		public ObjectSet<Work_RunProcessInfo> work_runprocessinfo
		{
			get
			{
				if (this._work_runprocessinfo == null)
				{
					this._work_runprocessinfo = base.CreateObjectSet<Work_RunProcessInfo>("work_runprocessinfo");
				}
				return this._work_runprocessinfo;
			}
		}

		public ObjectSet<Work_WorkLogInfo> work_workloginfo
		{
			get
			{
				if (this._work_workloginfo == null)
				{
					this._work_workloginfo = base.CreateObjectSet<Work_WorkLogInfo>("work_workloginfo");
				}
				return this._work_workloginfo;
			}
		}

		public ObjectSet<Work_WorkProcessLogInfo> work_workprocessloginfo
		{
			get
			{
				if (this._work_workprocessloginfo == null)
				{
					this._work_workprocessloginfo = base.CreateObjectSet<Work_WorkProcessLogInfo>("work_workprocessloginfo");
				}
				return this._work_workprocessloginfo;
			}
		}

		public ObjectSet<Work_RunNextInfo> work_runnextinfo
		{
			get
			{
				if (this._work_runnextinfo == null)
				{
					this._work_runnextinfo = base.CreateObjectSet<Work_RunNextInfo>("work_runnextinfo");
				}
				return this._work_runnextinfo;
			}
		}

		public WorkEntities() : base("name=WorkEntities", "WorkEntities")
		{
			base.ContextOptions.LazyLoadingEnabled = true;
		}

		public WorkEntities(string connectionString) : base(connectionString, "WorkEntities")
		{
			base.ContextOptions.LazyLoadingEnabled = true;
		}

		public WorkEntities(EntityConnection connection) : base(connection, "WorkEntities")
		{
			base.ContextOptions.LazyLoadingEnabled = true;
		}

		public void AddTowork_notifyaddress(Work_NotifyAddress work_NotifyAddress)
		{
			base.AddObject("work_notifyaddress", work_NotifyAddress);
		}

		public void AddTowork_plan(Work_Plan work_Plan)
		{
			base.AddObject("work_plan", work_Plan);
		}

		public void AddTowork_plannotify(Work_PlanNotify work_PlanNotify)
		{
			base.AddObject("work_plannotify", work_PlanNotify);
		}

		public void AddTowork_planrun(Work_PlanRun work_PlanRun)
		{
			base.AddObject("work_planrun", work_PlanRun);
		}

		public void AddTowork_planstep(Work_PlanStep work_PlanStep)
		{
			base.AddObject("work_planstep", work_PlanStep);
		}

		public void AddTowork_process(Work_Process work_Process)
		{
			base.AddObject("work_process", work_Process);
		}

		public void AddTowork_workinfo(Work_WorkInfo work_WorkInfo)
		{
			base.AddObject("work_workinfo", work_WorkInfo);
		}

		public void AddTowork_worklog(Work_WorkLog work_WorkLog)
		{
			base.AddObject("work_worklog", work_WorkLog);
		}

		public void AddTowork_workprocesslog(Work_WorkProcessLog work_WorkProcessLog)
		{
			base.AddObject("work_workprocesslog", work_WorkProcessLog);
		}

		public void AddTowork_plannotifyinfo(Work_PlanNotifyInfo work_PlanNotifyInfo)
		{
			base.AddObject("work_plannotifyinfo", work_PlanNotifyInfo);
		}

		public void AddTowork_planstepinfo(Work_PlanStepInfo work_PlanStepInfo)
		{
			base.AddObject("work_planstepinfo", work_PlanStepInfo);
		}

		public void AddTowork_runprocessinfo(Work_RunProcessInfo work_RunProcessInfo)
		{
			base.AddObject("work_runprocessinfo", work_RunProcessInfo);
		}

		public void AddTowork_workloginfo(Work_WorkLogInfo work_WorkLogInfo)
		{
			base.AddObject("work_workloginfo", work_WorkLogInfo);
		}

		public void AddTowork_workprocessloginfo(Work_WorkProcessLogInfo work_WorkProcessLogInfo)
		{
			base.AddObject("work_workprocessloginfo", work_WorkProcessLogInfo);
		}

		public void AddTowork_runnextinfo(Work_RunNextInfo work_RunNextInfo)
		{
			base.AddObject("work_runnextinfo", work_RunNextInfo);
		}
	}
}
