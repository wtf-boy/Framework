﻿namespace WTF.Logging
{
    using WTF.Framework;
    using WTF.Logging.Entity;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Text;

    public class LogInfo
    {
        private static LogRule _LogRule = new LogRule();
        private object _Message = string.Empty;
        private object _ResultMessage = string.Empty;

        public LogInfo()
        {
            this.CreateDate = DateTime.Now;
            this.HeadersData = new List<LogDataInfo>();
            this.RequestData = new List<LogDataInfo>();
            try
            {
                if ((SysVariable.CurrentContext != null) && (SysVariable.CurrentContext.Request != null))
                {
                    this.UrlReferrer = (SysVariable.CurrentContext.Request.UrlReferrer == null) ? "" : SysVariable.CurrentContext.Request.UrlReferrer.ToString();
                    this.RawUrl = (SysVariable.CurrentContext.Request.Url == null) ? "" : SysVariable.CurrentContext.Request.Url.ToString();
                    this.UserHostAddress = RequestHelper.GetRealIp();
                    this.UserAgent = (SysVariable.CurrentContext.Request.UserAgent == null) ? "" : SysVariable.CurrentContext.Request.UserAgent;
                    loger_application application = GetApplication();
                    if (application != null)
                    {
                        LogDataInfo info;
                        if ((SysVariable.CurrentContext.Request.Headers != null) && application.HeaderKey.IsNoNullOrWhiteSpace())
                        {
                            if (application.HeaderKey != "*")
                            {
                                foreach (string str in application.HeaderKey.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                                {
                                    info = new LogDataInfo {
                                        DataKey = str,
                                        DataValue = SysVariable.CurrentContext.Request.Headers[str]
                                    };
                                    this.HeadersData.Add(info);
                                }
                            }
                            else
                            {
                                foreach (string str in SysVariable.CurrentContext.Request.Headers.Keys)
                                {
                                    info = new LogDataInfo {
                                        DataKey = str,
                                        DataValue = SysVariable.CurrentContext.Request.Headers[str]
                                    };
                                    this.HeadersData.Add(info);
                                }
                            }
                        }
                        if (application.RequestKey.IsNoNullOrWhiteSpace())
                        {
                            if (application.RequestKey != "*")
                            {
                                foreach (string str2 in application.RequestKey.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                                {
                                    info = new LogDataInfo {
                                        DataKey = str2,
                                        DataValue = SysVariable.CurrentContext.Request[str2]
                                    };
                                    this.RequestData.Add(info);
                                }
                            }
                            else
                            {
                                foreach (string str2 in SysVariable.CurrentContext.Request.Form.Keys)
                                {
                                    info = new LogDataInfo {
                                        DataKey = str2,
                                        DataValue = SysVariable.CurrentContext.Request.Form[str2]
                                    };
                                    this.RequestData.Add(info);
                                }
                                LogDataInfo item = new LogDataInfo {
                                    DataKey = "--Cookies--",
                                    DataValue = "---Cookies--"
                                };
                                this.RequestData.Add(item);
                                foreach (string str2 in SysVariable.CurrentContext.Request.Cookies.Keys)
                                {
                                    info = new LogDataInfo {
                                        DataKey = str2,
                                        DataValue = SysVariable.CurrentContext.Request.Cookies[str2].Value
                                    };
                                    this.RequestData.Add(info);
                                }
                            }
                        }
                    }
                }
                else
                {
                    this.UrlReferrer = "";
                    this.RawUrl = "";
                    this.UserHostAddress = "";
                    this.UserAccount = "";
                    this.UserAgent = "";
                }
                this.UserAccount = this.CurrentAccount();
            }
            catch (Exception)
            {
                this.UrlReferrer = "";
                this.RawUrl = "";
                this.UserHostAddress = "";
                this.UserAccount = "";
                this.UserAgent = "";
            }
        }

        private string CreateMessage(object value)
        {
            try
            {
                if (value == null)
                {
                    return "null";
                }
                if (value is string)
                {
                    return (string) value;
                }
                if (value is Exception)
                {
                    StringBuilder builder = new StringBuilder();
                    Exception innerException = (Exception) value;
                    builder.Append("事件信息：").Append("<br>").AppendLine(innerException.Message);
                    builder.Append("<br>").Append("堆栈跟踪：").Append("<br>").AppendLine(innerException.StackTrace);
                    while (innerException.InnerException != null)
                    {
                        builder.Append("<br>").Append("内部事件信息：").Append("<br>").AppendLine(innerException.InnerException.Message);
                        builder.Append("<br>").Append("内部堆栈跟踪：").Append("<br>").AppendLine(innerException.InnerException.StackTrace);
                        innerException = innerException.InnerException;
                    }
                    return builder.ToString();
                }
                return value.JsonJsSerialize();
            }
            catch (Exception exception2)
            {
                EventLogWriter.WriterLog(exception2);
                return "日志消息转换出现异常";
            }
        }

        public string CurrentAccount()
        {
            try
            {
                if (((SysVariable.CurrentContext != null) && (SysVariable.CurrentContext.Session != null)) && (SysVariable.CurrentContext.Session["CurrentUser"] != null))
                {
                    return ((UserInfo) SysVariable.CurrentContext.Session["CurrentUser"]).Account;
                }
                return "";
            }
            catch
            {
                return "";
            }
        }

        private static loger_application GetApplication()
        {
            try
            {
                return _LogRule.GetCacheApplication(LogSectionHelper.ApplicationCode);
            }
            catch (Exception exception)
            {
                EventLogHelper.WriterLog("读取日志程序代码", exception);
                return null;
            }
        }

        public DateTime CreateDate { get; set; }

        public List<LogDataInfo> HeadersData { get; set; }

        public string LogCategory { get; set; }

        public List<string> LogModuleTypeList { get; set; }

        public object Message
        {
            get
            {
                return this._Message;
            }
            set
            {
                this._Message = this.CreateMessage(value);
            }
        }

        public string MessageID { get; set; }

        public string RawUrl { get; set; }

        public List<LogDataInfo> RequestData { get; set; }

        public object ResultMessage
        {
            get
            {
                return this._ResultMessage;
            }
            set
            {
                this._ResultMessage = this.CreateMessage(value);
            }
        }

        public string Title { get; set; }

        public string UrlReferrer { get; set; }

        public string UserAccount { get; set; }

        public string UserAgent { get; set; }

        public string UserHostAddress { get; set; }
    }
}

