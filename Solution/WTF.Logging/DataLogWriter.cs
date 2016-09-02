namespace WTF.Logging
{
    using WTF.Framework;
    using WTF.Logging.Entity;
    using System;

    public class DataLogWriter : ILogWriter
    {
        public void WriterLog(LogMessage objLogMessage)
        {
            LogRule rule = new LogRule();
            foreach (string str in objLogMessage.LogModuleTypeList)
            {
                loger_loging _loging = new loger_loging {
                    ApplicationID = objLogMessage.ApplicationID,
                    ApplicationName = objLogMessage.ApplicationName,
                    CategoryTypeCode = objLogMessage.LogCategory,
                    ModuleTypeCode = str,
                    ApplicationHost = objLogMessage.ApplicationHost,
                    UrlReferrer = objLogMessage.UrlReferrer,
                    RawUrl = objLogMessage.RawUrl,
                    UserHostAddress = objLogMessage.UserHostAddress,
                    ProcessName = objLogMessage.ProcessName,
                    LogDate = objLogMessage.CreateDate,
                    Account = objLogMessage.UserAccount,
                    Title = objLogMessage.Title.CutText(900),
                    Message = objLogMessage.Message,
                    MessageID = objLogMessage.MessageID,
                    ResultMessage = objLogMessage.ResultMessage,
                    IDPath = objLogMessage.IDPath,
                    UserAgent = objLogMessage.UserAgent,
                    HeadersData = objLogMessage.HeadersData.JsonJsSerialize(),
                    RequestData = objLogMessage.RequestData.JsonJsSerialize()
                };
                rule.CurrentEntities.AddTologer_loging(_loging);
            }
            rule.SaveChanges();
        }
    }
}

