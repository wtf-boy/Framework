using WTF.Framework;
using WTF.Work.Entity;
using System;
using System.Text;

namespace WTF.Work
{
	public abstract class JobProcess
	{
		protected WorkRule objWorkRule = new WorkRule();

		public int ProcessID
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

		public int WorkInfoID
		{
			get;
			set;
		}

		public string WorkLogID
		{
			get;
			set;
		}

		public string ProcessConfig
		{
			get;
			set;
		}

		public void LogWrite(object message)
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
			work_WorkProcessLog.Message.WriteLine("");
		}

		public InvokeResult CreateSuccessResult(object data)
		{
			return new InvokeResult
			{
				Data = data
			};
		}

		public InvokeResult CreateFailResult(object data)
		{
			return new InvokeResult
			{
				ResultCode = "-1",
				ResultMessage = "调用失败",
				Data = data
			};
		}

		public InvokeResult CreateFailResult()
		{
			return this.CreateFailResult(null);
		}

		public InvokeResult CreateSuccessResult()
		{
			return this.CreateSuccessResult(null);
		}

		public static string ConvertMessage(object value)
		{
			string result;
			try
			{
				if (value == null)
				{
					result = "null";
				}
				else if (value is string)
				{
					result = (string)value;
				}
				else if (value is Exception)
				{
					StringBuilder stringBuilder = new StringBuilder();
					Exception ex = (Exception)value;
					stringBuilder.Append("事件信息：").Append("<br>").AppendLine(ex.Message);
					stringBuilder.Append("<br>").Append("堆栈跟踪：").Append("<br>").AppendLine(ex.StackTrace);
					if (ex.InnerException != null)
					{
						stringBuilder.Append("<br>").Append("内部事件信息：").Append("<br>").AppendLine(ex.InnerException.Message);
						stringBuilder.Append("<br>").Append("内部堆栈跟踪：").Append("<br>").AppendLine(ex.InnerException.StackTrace);
					}
					result = stringBuilder.ToString();
				}
				else
				{
					result = value.JsonJsSerialize();
				}
			}
			catch
			{
				result = "消息转换出现异常";
			}
			return result;
		}

		public abstract InvokeResult Execute(string config, DateTime runDateTime, InvokeResult previousStepResult);
	}
}
