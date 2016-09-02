using WTF.Framework;
using WTF.Logging;
using WTF.Work.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace WTF.Work
{
    public class WorkProcess
    {
        private string _Work_Host;

        private string _RunWorkIP;

        private int _NextWorkDateSleep = 3000;

        private int _NextWorkRunSleep = 3000;

        private static IPAddress GetLocalIp()
        {
            IPHostEntry iPHostEntry = new IPHostEntry();
            iPHostEntry.AddressList = new IPAddress[1];
            for (int i = 0; i < Dns.GetHostEntry(Dns.GetHostName()).AddressList.Length; i++)
            {
                if (Dns.GetHostEntry(Dns.GetHostName()).AddressList[i].AddressFamily == AddressFamily.InterNetwork)
                {
                    iPHostEntry.AddressList[0] = Dns.GetHostEntry(Dns.GetHostName()).AddressList[i];
                    break;
                }
            }
            return iPHostEntry.AddressList[0];
        }

        public WorkProcess()
        {
            this._NextWorkDateSleep = ConfigHelper.GetIntValue("Work_NextWorkDateSleep", 3000);
            this._NextWorkRunSleep = ConfigHelper.GetIntValue("Work_NextWorkRunSleep", 3000);
            this._Work_Host = ConfigHelper.GetValue("Work_Host");
            this._RunWorkIP = WorkProcess.GetLocalIp().ToString();
        }

        public void ProcessErrorStop()
        {
            try
            {
                WorkRule workRule = new WorkRule();
                List<int> list = workRule.CurrentEntities.ExecuteStoreQuery<int>("select PlanRunID from Work_RunProcessInfo where IsRun=1 and  RunIP='" + this._Work_Host + "'", new object[0]).ToList<int>();
                if (list != null && list.Count > 0)
                {
                    workRule.CurrentEntities.ExecuteStoreCommand("update Work_PlanRun set IsRun=0 where PlanRunID in (" + list.ConvertListToString<int>() + ")", new object[0]);
                }
            }
            catch (Exception ex)
            {
                ("还原异常停止服务没有执行失败" + ex.Message).WriteLineRed("");
                LogHelper.Write("WorkLog", "还原异常停止服务没有执行失败", ex, "");
            }
        }

        public void StartProcess()
        {
            try
            {
                "作业初始化".WriteLine("");
                "开始运行:还原异常停止服务没有执行完的计划".WriteLineYellow("");
                this.ProcessErrorStop();
                "结束检查:还原异常停止服务没有执行完的计划".WriteLineYellow("");
                "开始运行:循环检创建各作业下次执行时间".WriteLine("");
                Thread thread = new Thread(new ThreadStart(this.ProcessNextWorkDate));
                thread.Start();
                "开始运行:循环检查各作业的计划处理".WriteLine("");
                Thread thread2 = new Thread(new ThreadStart(this.ProcessNextWorkRun));
                thread2.Start();
                "作业初始化完成".WriteLine("");
            }
            catch (Exception ex)
            {
                ("启动作动服务失败" + ex.Message).WriteLineRed("");
            }
        }

        private InvokeResult RunPlanStep(PlanStepInfo objProcessInfo, InvokeResult previousStepResult)
        {
            objProcessInfo.RunCurrentCount++;
            InvokeResult invokeResult = new InvokeResult();
            try
            {
                (objProcessInfo.FullName + "开始执行").WriteLine("");
                invokeResult = objProcessInfo.Process(previousStepResult);
                (objProcessInfo.FullName + "执行结束,处理结果:" + ((invokeResult.ResultCode == "0") ? "成功" : "失败")).WriteLine("");
            }
            catch (Exception ex)
            {
                invokeResult = new InvokeResult
                {
                    ResultCode = "-1",
                    ResultMessage = "调用失败"
                };
                (objProcessInfo.FullName + "执行异常" + ex.Message).WriteLineRed("");
                LogHelper.Write("WorkLog", objProcessInfo.FullName + "执行异常", ex, "");
            }
            return invokeResult;
        }

        private void ProcessWorkRun(object state)
        {
            PlanInfo objPlanInfo = (PlanInfo)state;
            WorkRule workRule = new WorkRule();
            (objPlanInfo.FullName + " 开始执行").WriteLineYellow("");
            try
            {
                DateTime now = DateTime.Now;
                Work_WorkLog work_WorkLog = new Work_WorkLog();
                work_WorkLog.WorkInfoID = objPlanInfo.WorkInfoID;
                work_WorkLog.WorkLogID = objPlanInfo.WorkLogID;
                work_WorkLog.RunIP = this._RunWorkIP;
                work_WorkLog.PlanID = objPlanInfo.PlanID;
                work_WorkLog.HostName = Environment.MachineName;
                work_WorkLog.CreateDate = DateTime.Now;
                work_WorkLog.StartDate = now;
                work_WorkLog.EndDate = DateTime.Parse(DateTime.MaxValue.ToString("yyyy-MM-dd HH:mm:ss"));
                work_WorkLog.ResultType = 0;
                workRule.InsertWorkLog(work_WorkLog);
                bool flag = true;
                InvokeResult invokeResult = new InvokeResult();
                InvokeResult previousStepResult = null;
                foreach (PlanStepInfo current in from s in objPlanInfo.ProcessInfoList
                                                 orderby s.SortIndex
                                                 select s)
                {
                    current.RunCurrentCount = 0;
                    invokeResult = this.RunPlanStep(current, previousStepResult);
                    if (invokeResult.ResultCode != "0")
                    {
                        for (int i = 0; i < current.RunCount; i++)
                        {
                            Thread.Sleep(current.RunInterval * 60 * 1000);
                            invokeResult = this.RunPlanStep(current, previousStepResult);
                            if (invokeResult.ResultCode == "0")
                            {
                                break;
                            }
                        }
                    }
                    previousStepResult = new InvokeResult
                    {
                        ResultCode = invokeResult.ResultCode,
                        ResultMessage = invokeResult.ResultMessage,
                        Data = invokeResult.Data
                    };
                    if (invokeResult.ResultCode == "0")
                    {
                        if (current.SucessProcessType == 1)
                        {
                            invokeResult.ResultCode = "-1";
                            break;
                        }
                        if (current.SucessProcessType != 2)
                        {
                            if (current.SucessProcessType == 3)
                            {
                                break;
                            }
                        }
                    }
                    else
                    {
                        if (current.FailProcessType == 1)
                        {
                            break;
                        }
                        if (current.FailProcessType == 2)
                        {
                            invokeResult.ResultCode = "0";
                        }
                        else if (current.FailProcessType == 3)
                        {
                            invokeResult.ResultCode = "0";
                            break;
                        }
                    }
                    flag = (flag && invokeResult.ResultCode == "0");
                }
                flag = (flag && invokeResult.ResultCode == "0");
                Work_Plan work_Plan = workRule.Work_Plan.FirstOrDefault((Work_Plan s) => s.PlanID == objPlanInfo.PlanID);
                if (work_Plan != null)
                {
                    work_Plan.LastRunDate = objPlanInfo.RunDateTime;
                    workRule.CurrentEntities.SaveChanges();
                }
                DateTime now2 = DateTime.Now;
                string.Concat(new string[]
				{
					objPlanInfo.FullName,
					"执行结束，",
					now.ToString("yyyy-MM-dd HH:mm:ss"),
					"~",
					now2.ToString("yyyy-MM-dd HH:mm:ss")
				}).WriteLineYellow("");
                Work_WorkInfo work_WorkInfo = workRule.Work_WorkInfo.FirstOrDefault((Work_WorkInfo s) => s.WorkInfoID == objPlanInfo.WorkInfoID);
                work_WorkLog.EndDate = now2;
                work_WorkLog.ResultType = (flag ? 1 : -1);
                if (work_WorkInfo != null)
                {
                    work_WorkInfo.LastProcessDate = objPlanInfo.RunDateTime;
                }
                foreach (Work_PlanNotifyInfo current2 in from s in workRule.Work_PlanNotifyInfo
                                                         where s.PlanID == objPlanInfo.PlanID
                                                         select s)
                {
                    if ((current2.PlanResult == -1 && !flag) || (current2.PlanResult == 1 && flag) || current2.PlanResult == 2)
                    {
                        string text = objPlanInfo.FullName + "在" + DateTime.Now.ToString("yyyy年MM月dd日 HH:mm:ss") + (flag ? "执行成功" : "执行失败");
                        switch (current2.NotifyType)
                        {
                            case 1:
                                //MailQueueHelper.SendMail(SendPriority.High, current2.Address, text, text, false, true);
                                break;
                            case 2:
                                {
                                    //InvokeResult message = NotifyHelper.SendSMS(current2.Address, text, PriorityLevelType.Normal, 0, null);
                                    //message.WriteLineRed(objPlanInfo.FullName + " 短信通知完成");
                                    ConsoleHelper.WriteLineRed(objPlanInfo.FullName + " 短信通知完成");
                                    break;
                                }
                        }
                    }
                }
                workRule.CurrentEntities.SaveChanges();
                workRule.DeletePlanRunByKey(objPlanInfo.PlanRunID.ToString());
                (objPlanInfo.FullName + " 执行结束").WriteLineYellow("");
            }
            catch (Exception ex)
            {
                (objPlanInfo.FullName + "执行异常" + ex.Message).WriteLineRed("");
                LogHelper.Write("WorkLog", objPlanInfo.FullName + "执行异常", ex, "");
            }
        }

        private void ProcessNextWorkRun()
        {
            while (true)
            {
                try
                {
                    WorkRule workRule = new WorkRule();
                    List<Work_RunProcessInfo> createRunProcess = workRule.GetCreateRunProcess(this._Work_Host);
                    if (createRunProcess.Count > 0)
                    {
                        List<int> PlanIDList = (from s in createRunProcess
                                                select s.PlanID).Distinct<int>().ToList<int>();
                        List<Work_PlanStepInfo> source = (from s in workRule.Work_PlanStepInfo
                                                          where PlanIDList.Contains(s.PlanID)
                                                          select s).ToList<Work_PlanStepInfo>();
                        using (List<Work_RunProcessInfo>.Enumerator enumerator = createRunProcess.GetEnumerator())
                        {
                            while (enumerator.MoveNext())
                            {
                                Work_RunProcessInfo objWork_RunProcessInfo = enumerator.Current;
                                PlanInfo planInfo = new PlanInfo
                                {
                                    PlanRunID = objWork_RunProcessInfo.PlanRunID,
                                    PlanID = objWork_RunProcessInfo.PlanID,
                                    RunDateTime = objWork_RunProcessInfo.RunDate,
                                    WorkInfoID = objWork_RunProcessInfo.WorkInfoID,
                                    WorkInfoName = objWork_RunProcessInfo.WorkInfoName,
                                    PlanName = objWork_RunProcessInfo.PlanName,
                                    PlanConfigInfo = objWork_RunProcessInfo.ConfigInfo
                                };
                                planInfo.ProcessInfoList = new List<PlanStepInfo>();
                                foreach (Work_PlanStepInfo current in from s in
                                                                          (from s in source
                                                                           where s.PlanID == objWork_RunProcessInfo.PlanID
                                                                           select s).Distinct<Work_PlanStepInfo>()
                                                                      orderby s.SortIndex
                                                                      select s)
                                {
                                    PlanStepInfo item = new PlanStepInfo
                                    {
                                        WorkInfoName = planInfo.WorkInfoName,
                                        RunDateTime = objWork_RunProcessInfo.RunDate,
                                        WorkLogID = planInfo.WorkLogID,
                                        ProcessConfig = current.ProcessConfig,
                                        PlanConfigInfo = planInfo.PlanConfigInfo,
                                        PlanID = objWork_RunProcessInfo.PlanID,
                                        ProcessID = current.ProcessID,
                                        ProcessName = current.ProcessName,
                                        AssemblyName = current.AssemblyName,
                                        SortIndex = current.SortIndex,
                                        TypeName = current.TypeName,
                                        FailProcessType = current.FailProcessType,
                                        SucessProcessType = current.SucessProcessType,
                                        PlanStepID = current.PlanStepID,
                                        RunCount = current.RunCount,
                                        RunInterval = current.RunInterval,
                                        StepName = current.StepName,
                                        PlanName = objWork_RunProcessInfo.PlanName
                                    };
                                    planInfo.ProcessInfoList.Add(item);
                                }
                                ThreadPool.QueueUserWorkItem(new WaitCallback(this.ProcessWorkRun), planInfo);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    ("循环检查各作业的计划处理出现异常:" + ex.Message).WriteLineRed("");
                    try
                    {
                        LogHelper.Write("WorkLog", "循环检查各作业的计划处理出现异常", ex, "");
                    }
                    catch (Exception exception)
                    {
                        EventLogWriter.WriterLog(exception);
                    }
                }
                Thread.Sleep(this._NextWorkRunSleep);
            }
        }

        private void ProcessNextWorkDate()
        {
            while (true)
            {
                try
                {
                    WorkRule workRule = new WorkRule();
                    bool flag = false;
                    foreach (Work_Plan current in workRule.GetCreateNextPlanDate(this._Work_Host))
                    {
                        try
                        {
                            DateTime dateTime = PlanHelper.CreateNextPlanDate(current.PlanConfig, current.LastRunDate);
                            if (dateTime != DateTime.MaxValue)
                            {
                                Work_PlanRun work_PlanRun = new Work_PlanRun();
                                work_PlanRun.WorkInfoID = current.WorkInfoID;
                                work_PlanRun.PlanID = current.PlanID;
                                work_PlanRun.RunDate = dateTime;
                                work_PlanRun.IsRun = false;
                                flag = true;
                                workRule.CurrentEntities.AddTowork_planrun(work_PlanRun);
                            }
                        }
                        catch (Exception ex)
                        {
                            ("循环检查创建" + current.PlanName + "下次执行时间出现异常:" + ex.Message).WriteLineRed("");
                            LogHelper.Write("WorkLog", "循环检创建" + current.PlanName + "下次执行时间出现异常", ex, "");
                        }
                    }
                    if (flag)
                    {
                        "提交下一次计划更新".WriteLineYellow("");
                        workRule.CurrentEntities.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    ("循环检查创建各作业下次执行时间出现异常:" + ex.Message).WriteLineRed("");
                    try
                    {
                        LogHelper.Write("WorkLog", "循环检查创建各作业下次执行时间出现异常", ex, "");
                    }
                    catch (Exception exception)
                    {
                        EventLogWriter.WriterLog(exception);
                    }
                }
                Thread.Sleep(this._NextWorkDateSleep);
            }
        }
    }
}
