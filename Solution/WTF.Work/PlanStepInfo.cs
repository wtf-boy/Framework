using WTF.Framework;
using WTF.Logging;
using WTF.Work.Entity;
using System;

namespace WTF.Work
{
    public class PlanStepInfo
    {
        private WorkRule objWorkRule = new WorkRule();

        public int ProcessID
        {
            get;
            set;
        }

        public string WorkLogID
        {
            get;
            set;
        }

        public int PlanID
        {
            get;
            set;
        }

        public int PlanStepID
        {
            get;
            set;
        }

        public DateTime RunDateTime
        {
            get;
            set;
        }

        public string ProcessConfig
        {
            get;
            set;
        }

        public string PlanConfigInfo
        {
            get;
            set;
        }

        public int SortIndex
        {
            get;
            set;
        }

        public int RunCount
        {
            get;
            set;
        }

        public int RunCurrentCount
        {
            get;
            set;
        }

        public int RunInterval
        {
            get;
            set;
        }

        public int FailProcessType
        {
            get;
            set;
        }

        public int SucessProcessType
        {
            get;
            set;
        }

        public string ProcessName
        {
            get;
            set;
        }

        public string StepName
        {
            get;
            set;
        }

        public string PlanName
        {
            get;
            set;
        }

        public string WorkInfoName
        {
            get;
            set;
        }

        public string FullName
        {
            get
            {
                return string.Concat(new object[]
				{
					"作业名称:",
					this.WorkInfoName,
					" 计划名称:",
					this.PlanName,
					" 步骤名称:",
					this.StepName,
					" 第",
					this.RunCurrentCount,
					"次"
				});
            }
        }

        public string AssemblyName
        {
            get;
            set;
        }

        public string TypeName
        {
            get;
            set;
        }

        public void WriteWorkLog(object message)
        {
            Work_WorkProcessLog work_WorkProcessLog = new Work_WorkProcessLog();
            work_WorkProcessLog.WorkLogID = this.WorkLogID;
            work_WorkProcessLog.PlanID = this.PlanID;
            work_WorkProcessLog.PlanStepID = this.PlanStepID;
            work_WorkProcessLog.CreateDate = DateTime.Now;
            work_WorkProcessLog.StartDate = DateTime.Now;
            work_WorkProcessLog.EndDate = DateTime.Now;
            work_WorkProcessLog.Message = JobProcess.ConvertMessage(message);
            this.objWorkRule.InsertWorkProcessLog(work_WorkProcessLog);
        }

        public virtual InvokeResult Process(InvokeResult previousStepResult)
        {
            Console.WriteLine(string.Concat(new string[]
			{
				this.FullName,
				"开始进行反射",
				this.AssemblyName,
				",",
				this.TypeName
			}));
            InvokeResult invokeResult = new InvokeResult
            {
                ResultCode = "-1",
                ResultMessage = "调用失败"
            };
            try
            {
                DateTime now = DateTime.Now;
                JobProcess jobProcess = this.AssemblyName.CreateInstace<JobProcess>(this.TypeName, new object[0]);
                Console.WriteLine(this.FullName + "开始执行自定义处理");
                jobProcess.WorkLogID = this.WorkLogID;
                jobProcess.ProcessID = this.ProcessID;
                jobProcess.PlanID = this.PlanID;
                jobProcess.PlanStepID = this.PlanStepID;
                jobProcess.ProcessConfig = this.ProcessConfig;
                invokeResult = jobProcess.Execute(this.PlanConfigInfo, this.RunDateTime, previousStepResult);
                DateTime now2 = DateTime.Now;
                Work_WorkProcessLog work_WorkProcessLog = new Work_WorkProcessLog();
                work_WorkProcessLog.WorkLogID = this.WorkLogID;
                work_WorkProcessLog.PlanID = this.PlanID;
                work_WorkProcessLog.PlanStepID = this.PlanStepID;
                work_WorkProcessLog.CreateDate = DateTime.Now;
                work_WorkProcessLog.StartDate = now;
                work_WorkProcessLog.EndDate = now2;
                work_WorkProcessLog.Message = this.FullName + "执行完成,处理结果:" + ((invokeResult.ResultCode == "0") ? "成功" : "失败");
                this.objWorkRule.InsertWorkProcessLog(work_WorkProcessLog);
                Console.WriteLine(this.FullName + "开始自定义处理结束,处理结果:" + ((invokeResult.ResultCode == "0") ? "成功" : "失败"));
            }
            catch (Exception ex)
            {
                Console.WriteLine(this.FullName + "执行自定义出现异常" + ex.Message);
                this.WriteWorkLog(ex);
                LogHelper.Write("WorkLog", this.FullName + "执行异常", ex, "");
                invokeResult = new InvokeResult
                {
                    ResultCode = "-1",
                    ResultMessage = "调用失败"
                };
            }
            return invokeResult;
        }
    }
}
