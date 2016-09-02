using WTF.Framework;
using WTF.Logging;
using WTF.Work.Entity;
using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;

namespace WTF.Work
{
	public class WorkRule
	{
		private WorkEntities objCurrentEntities = null;

		public WorkEntities CurrentEntities
		{
			get
			{
				if (this.objCurrentEntities == null)
				{
					this.objCurrentEntities = new WorkEntities(EntitiesHelper.GetConnectionString<WorkEntities>());
				}
				return this.objCurrentEntities;
			}
		}

		public ObjectQuery<Work_Plan> Work_Plan
		{
			get
			{
				return this.CurrentEntities.work_plan;
			}
		}

		public ObjectQuery<Work_Process> Work_Process
		{
			get
			{
				return this.CurrentEntities.work_process;
			}
		}

		public ObjectQuery<Work_PlanStepInfo> Work_PlanStepInfo
		{
			get
			{
				return this.CurrentEntities.work_planstepinfo;
			}
		}

		public ObjectQuery<Work_PlanNotify> Work_PlanNotify
		{
			get
			{
				return this.CurrentEntities.work_plannotify;
			}
		}

		public ObjectQuery<Work_PlanNotifyInfo> Work_PlanNotifyInfo
		{
			get
			{
				return this.CurrentEntities.work_plannotifyinfo;
			}
		}

		public ObjectQuery<Work_NotifyAddress> Work_NotifyAddress
		{
			get
			{
				return this.CurrentEntities.work_notifyaddress;
			}
		}

		public ObjectQuery<Work_WorkInfo> Work_WorkInfo
		{
			get
			{
				return this.CurrentEntities.work_workinfo;
			}
		}

		public ObjectQuery<Work_RunProcessInfo> Work_RunProcessInfo
		{
			get
			{
				return this.CurrentEntities.work_runprocessinfo;
			}
		}

		public ObjectQuery<Work_RunNextInfo> Work_RunNextInfo
		{
			get
			{
				return this.CurrentEntities.work_runnextinfo;
			}
		}

		public ObjectQuery<Work_PlanRun> Work_PlanRun
		{
			get
			{
				return this.CurrentEntities.work_planrun;
			}
		}

		public ObjectQuery<Work_WorkLogInfo> Work_WorkLogInfo
		{
			get
			{
				return this.CurrentEntities.work_workloginfo;
			}
		}

		public ObjectQuery<Work_WorkProcessLogInfo> Work_WorkProcessLogInfo
		{
			get
			{
				return this.CurrentEntities.work_workprocessloginfo;
			}
		}

		public ObjectQuery<Work_WorkLog> Work_WorkLog
		{
			get
			{
				return this.CurrentEntities.work_worklog;
			}
		}

		public ObjectQuery<Work_WorkProcessLog> Work_WorkProcessLog
		{
			get
			{
				return this.CurrentEntities.work_workprocesslog;
			}
		}

		public ObjectQuery<Work_PlanStep> Work_PlanStep
		{
			get
			{
				return this.CurrentEntities.work_planstep;
			}
		}

		public void InsertPlan(Work_Plan objWork_Plan)
		{
			objWork_Plan.PlanName.CheckIsNull("请输入计划名称", "WorkLog");
			this.CurrentEntities.AddTowork_plan(objWork_Plan);
			this.CurrentEntities.SaveChanges();
		}

		public void UpdatePlan(Work_Plan objWork_Plan)
		{
			objWork_Plan.PlanName.CheckIsNull("请输入计划名称", "WorkLog");
			this.DeletePlanRunByPlanID(objWork_Plan.PlanID);
			this.CurrentEntities.SaveChanges();
		}

		public List<Work_Plan> GetCreateNextPlanDate(string host)
		{
			string format = "\r\n                            select  Work_Plan.*  from Work_Plan ,Work_WorkInfo\r\n                            where   Work_Plan.WorkInfoID=Work_WorkInfo.WorkInfoID \r\n                            and  StartDate<'{0}' and EndDate>'{0}' and  Work_WorkInfo.RunIP='{1}'\r\n                            and  Work_Plan.IsEnable=1\r\n                            and  Work_WorkInfo.IsEnable=1\r\n                            and  Work_Plan.PlanID not in (select PlanID from Work_PlanRun)";
			return this.CurrentEntities.ExecuteStoreQuery<Work_Plan>(string.Format(format, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), host), new object[0]).ToList<Work_Plan>();
		}

		public List<Work_RunProcessInfo> GetCreateRunProcess(string host)
		{
			DateTime now = DateTime.Now;
			List<Work_RunProcessInfo> list = this.CurrentEntities.ExecuteStoreQuery<Work_RunProcessInfo>(string.Format("select  * from Work_RunProcessInfo where RunDate<='{0}' and  IsRun=0 and RunIP='{1}'", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), host), new object[0]).ToList<Work_RunProcessInfo>();
			List<int> PlanRunIDList = (from s in list
			select s.PlanRunID).Distinct<int>().ToList<int>();
			foreach (Work_PlanRun current in from s in this.CurrentEntities.work_planrun
			where PlanRunIDList.Contains(s.PlanRunID)
			select s)
			{
				current.IsRun = true;
			}
			this.CurrentEntities.SaveChanges();
			return list;
		}

		public void DeletePlanByKey(string primaryKey)
		{
			this.CurrentEntities.work_plan.DeleteDataPrimaryKey(primaryKey);
			this.CurrentEntities.work_planrun.DeleteData("it.PlanID in {" + primaryKey + "}", new ObjectParameter[0]);
		}

		public void InsertProcess(Work_Process objWork_Process)
		{
			objWork_Process.ProcessName.CheckIsNull("请输入处理名称", "WorkLog");
			objWork_Process.AssemblyName.CheckIsNull("请输入程序集", "WorkLog");
			objWork_Process.TypeName.CheckIsNull("请输入类全名", "WorkLog");
			this.CurrentEntities.AddTowork_process(objWork_Process);
			this.CurrentEntities.SaveChanges();
		}

		public void UpdateProcess(Work_Process objWork_Process)
		{
			objWork_Process.ProcessName.CheckIsNull("请输入处理名称", "WorkLog");
			objWork_Process.AssemblyName.CheckIsNull("请输入程序集", "WorkLog");
			objWork_Process.TypeName.CheckIsNull("请输入类全名", "WorkLog");
			this.CurrentEntities.SaveChanges();
		}

		public void DeleteProcessByKey(string primaryKey)
		{
			this.CurrentEntities.work_process.DeleteDataPrimaryKey(primaryKey);
		}

		public void InsertWorkInfo(Work_WorkInfo objWork_WorkInfo)
		{
			objWork_WorkInfo.WorkInfoName.CheckIsNull("请输入作业名称", "WorkLog");
			objWork_WorkInfo.WorkInfoRemark.CheckIsNull("请输入作业说明", "WorkLog");
			this.CurrentEntities.AddTowork_workinfo(objWork_WorkInfo);
			this.CurrentEntities.SaveChanges();
		}

		public void UpdateWorkInfo(Work_WorkInfo objWork_WorkInfo)
		{
			objWork_WorkInfo.WorkInfoName.CheckIsNull("请输入作业名称", "WorkLog");
			objWork_WorkInfo.WorkInfoRemark.CheckIsNull("请输入作业说明", "WorkLog");
			this.CurrentEntities.SaveChanges();
		}

		public void DeleteWorkInfoByKey(string primaryKey)
		{
			this.CurrentEntities.work_workinfo.DeleteDataPrimaryKey(primaryKey);
		}

		public void ChangeWorkInfoEnable(string WorkInfoIDString, bool Enable)
		{
			foreach (Work_WorkInfo current in ((IEnumerable<Work_WorkInfo>)this.Work_WorkInfo.WhereKey(WorkInfoIDString)))
			{
				current.IsEnable = Enable;
			}
			this.CurrentEntities.SaveChanges();
		}

		public void ChangePlanEnable(int PlanID, bool isEnable)
		{
			if (isEnable)
			{
				if (!this.Work_PlanStep.Any((Work_PlanStep s) => s.PlanID == PlanID))
				{
					SysAssert.InfoHintAssert("未设置步骤无法设置启动");
				}
			}
			foreach (Work_Plan current in ((IEnumerable<Work_Plan>)this.Work_Plan.WhereKey(PlanID.ToString())))
			{
				current.IsEnable = isEnable;
			}
			if (!isEnable)
			{
				this.DeletePlanRunByPlanID(PlanID);
			}
			this.CurrentEntities.SaveChanges();
		}

		public void InsertPlanRun(Work_PlanRun objWork_PlanRun)
		{
			this.CurrentEntities.AddTowork_planrun(objWork_PlanRun);
			this.CurrentEntities.SaveChanges();
		}

		public void UpdatePlanRun(Work_PlanRun objWork_PlanRun)
		{
			this.CurrentEntities.SaveChanges();
		}

		public void DeletePlanRunByKey(string primaryKey)
		{
			this.CurrentEntities.work_planrun.DeleteDataPrimaryKey(primaryKey);
		}

		public void DeletePlanRunByPlanID(int PlanID)
		{
			this.CurrentEntities.work_planrun.DeleteData("it.PlanID=" + PlanID, new ObjectParameter[0]);
		}

		public void DeletePlanRunByWorkInfoID(int WorkInfoID)
		{
			this.CurrentEntities.work_planrun.DeleteData("it.WorkInfoID=" + WorkInfoID, new ObjectParameter[0]);
		}

		public void InsertWorkLog(Work_WorkLog objWork_WorkLog)
		{
			this.CurrentEntities.AddTowork_worklog(objWork_WorkLog);
			this.CurrentEntities.SaveChanges();
		}

		public void UpdateWorkLog(Work_WorkLog objWork_WorkLog)
		{
			this.CurrentEntities.SaveChanges();
		}

		public void DeleteWorkLogByKey(string primaryKey)
		{
			this.CurrentEntities.work_worklog.DeleteDataPrimaryKey(primaryKey);
		}

		public void InsertWorkProcessLog(Work_WorkProcessLog objWork_WorkProcessLog)
		{
			objWork_WorkProcessLog.Message.CheckIsNull("请输入日志消息", "WorkLog");
			this.CurrentEntities.AddTowork_workprocesslog(objWork_WorkProcessLog);
			this.CurrentEntities.SaveChanges();
		}

		public void UpdateWorkProcessLog(Work_WorkProcessLog objWork_WorkProcessLog)
		{
			objWork_WorkProcessLog.Message.CheckIsNull("请输入日志消息", "WorkLog");
			this.CurrentEntities.SaveChanges();
		}

		public void DeleteWorkProcessLogByKey(string primaryKey)
		{
			this.CurrentEntities.work_workprocesslog.DeleteDataPrimaryKey(primaryKey);
		}

		public void InsertNotifyAddress(Work_NotifyAddress objWork_NotifyAddress)
		{
			objWork_NotifyAddress.Address.CheckIsNull("请输入通信地址", "WorkLog");
			objWork_NotifyAddress.AddressName.CheckIsNull("请输入通知联系人", "WorkLog");
			this.CurrentEntities.AddTowork_notifyaddress(objWork_NotifyAddress);
			this.CurrentEntities.SaveChanges();
		}

		public void UpdateNotifyAddress(Work_NotifyAddress objWork_NotifyAddress)
		{
			objWork_NotifyAddress.Address.CheckIsNull("请输入通信地址", "WorkLog");
			objWork_NotifyAddress.AddressName.CheckIsNull("请输入通知联系人", "WorkLog");
			this.CurrentEntities.SaveChanges();
		}

		public void DeleteNotifyAddressByKey(int NotifyAddressID)
		{
			if (this.CurrentEntities.work_plannotify.Any((Work_PlanNotify s) => s.NotifyAddressID == NotifyAddressID))
			{
				SysAssert.InfoHintAssert("此地址正在使用，无法删除");
			}
			else
			{
				this.CurrentEntities.work_notifyaddress.DeleteDataPrimaryKey(NotifyAddressID.ToString());
			}
		}

		public void UpdatePlanStepSort(string PlanStepIDstring)
		{
			PlanStepIDstring.CheckIsNull("请选择要排序的类型", "ContentLog");
			List<int> objList = PlanStepIDstring.ConvertListInt();
			foreach (Work_PlanStep current in from s in this.Work_PlanStep
			where objList.Contains(s.PlanStepID)
			select s)
			{
				current.SortIndex = objList.IndexOf(current.PlanStepID) + 1;
			}
			this.CurrentEntities.SaveChanges();
		}

		public void InsertPlanStep(Work_PlanStep objWork_PlanStep)
		{
			objWork_PlanStep.StepName.CheckIsNull("请输入步骤名称", "WorkLog");
			objWork_PlanStep.SortIndex = 1;
			if ((from s in this.CurrentEntities.work_planstep
			where s.PlanID == objWork_PlanStep.PlanID
			select s).Count<Work_PlanStep>() > 0)
			{
				objWork_PlanStep.SortIndex = (from s in this.CurrentEntities.work_planstep
				where s.PlanID == objWork_PlanStep.PlanID
				select s).Max((Work_PlanStep s) => s.SortIndex) + 1;
			}
			this.CurrentEntities.AddTowork_planstep(objWork_PlanStep);
			this.CurrentEntities.SaveChanges();
		}

		public void UpdatePlanStep(Work_PlanStep objWork_PlanStep)
		{
			objWork_PlanStep.StepName.CheckIsNull("请输入步骤名称", "WorkLog");
			this.CurrentEntities.SaveChanges();
		}

		public void DeletePlanStepByKey(string primaryKey)
		{
			this.CurrentEntities.work_planstep.DeleteDataPrimaryKey(primaryKey);
		}

		public void InsertPlanNotify(Work_PlanNotify objWork_PlanNotify)
		{
			this.CurrentEntities.AddTowork_plannotify(objWork_PlanNotify);
			this.CurrentEntities.SaveChanges();
		}

		public void UpdatePlanNotify(Work_PlanNotify objWork_PlanNotify)
		{
			this.CurrentEntities.SaveChanges();
		}

		public void DeletePlanNotifyByKey(string primaryKey)
		{
			this.CurrentEntities.work_plannotify.DeleteDataPrimaryKey(primaryKey);
		}
	}
}
