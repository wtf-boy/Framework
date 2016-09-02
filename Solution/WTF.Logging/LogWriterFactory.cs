namespace WTF.Logging
{
    using WTF.Framework;
    using WTF.Logging.Entity;
    using System;
    using System.Linq;

    public class LogWriterFactory
    {
        private QueuePoolHelper<LogInfo> _QueuePoolHelper = null;

        public LogWriterFactory()
        {
            this._QueuePoolHelper = new QueuePoolHelper<LogInfo>(1);
            this._QueuePoolHelper.SendMessage += new EventHandler<QueuePoolEventArgs<LogInfo>>(this._QueuePoolHelper_SendMessage);
            this._QueuePoolHelper.StartProcess(1, 0);
        }

        private void _QueuePoolHelper_SendMessage(object sender, QueuePoolEventArgs<LogInfo> e)
        {
            try
            {
                Func<loger_category, bool> predicate = null;
                LogInfo objLogInfo = e.Message;
                LogSection logSection = LogSectionHelper.GetLogSection();
                if (!string.IsNullOrWhiteSpace(logSection.Application))
                {
                    loger_application cacheApplication;
                    LogMessage objLogMessage = new LogMessage {
                        ApplicationCode = logSection.Application,
                        LogCategory = objLogInfo.LogCategory,
                        LogModuleTypeList = objLogInfo.LogModuleTypeList,
                        Title = objLogInfo.Title,
                        UserAccount = objLogInfo.UserAccount,
                        UserHostAddress = objLogInfo.UserHostAddress,
                        RawUrl = objLogInfo.RawUrl,
                        UrlReferrer = objLogInfo.UrlReferrer,
                        CreateDate = objLogInfo.CreateDate,
                        MessageID = objLogInfo.MessageID,
                        LogWriteMap = logSection.LogWriteMap,
                        Message = objLogInfo.Message.ToString(),
                        ResultMessage = objLogInfo.ResultMessage.ToString(),
                        UserAgent = objLogInfo.UserAgent,
                        HeadersData = objLogInfo.HeadersData,
                        RequestData = objLogInfo.RequestData
                    };
                    LogCategoryElement element = logSection.Categorys[objLogInfo.LogCategory];
                    if (element != null)
                    {
                        if (element.IsRecordDB)
                        {
                            LogRule rule = new LogRule();
                            cacheApplication = rule.GetCacheApplication(logSection.Application);
                            if (cacheApplication != null)
                            {
                                objLogMessage.ApplicationID = cacheApplication.ApplicationID;
                                objLogMessage.ApplicationName = cacheApplication.ApplicationName;
                                objLogMessage.IDPath = cacheApplication.IDPath;
                                this.WriteLog(objLogMessage, LogWriterType.DataLogWriter);
                            }
                        }
                        if (element.IsRecordEvent)
                        {
                            this.WriteLog(objLogMessage, LogWriterType.EventLogWriter);
                        }
                        if (objLogMessage.LogWriteMap.IsNoNull())
                        {
                            if (element.IsRecordText)
                            {
                                this.WriteLog(objLogMessage, LogWriterType.TextLogWriter);
                            }
                            if (element.IsRecordXml)
                            {
                                this.WriteLog(objLogMessage, LogWriterType.XmlLogWriter);
                            }
                        }
                    }
                    else
                    {
                        cacheApplication = new LogRule().GetCacheApplication(logSection.Application);
                        if (cacheApplication != null)
                        {
                            objLogMessage.ApplicationID = cacheApplication.ApplicationID;
                            objLogMessage.ApplicationName = cacheApplication.ApplicationName;
                            objLogMessage.IDPath = cacheApplication.IDPath;
                            if (predicate == null)
                            {
                                predicate = s => s.CategoryTypeCode == objLogInfo.LogCategory;
                            }
                            loger_category _category = cacheApplication.loger_category.FirstOrDefault<loger_category>(predicate);
                            if (_category != null)
                            {
                                foreach (int num in _category.LogWriteType.ConvertListInt())
                                {
                                    LogWriterType objLogWriterType = (LogWriterType) num;
                                    if ((objLogWriterType == LogWriterType.DataLogWriter) || (objLogWriterType == LogWriterType.EventLogWriter))
                                    {
                                        this.WriteLog(objLogMessage, objLogWriterType);
                                    }
                                    else if (objLogMessage.LogWriteMap.IsNoNull())
                                    {
                                        this.WriteLog(objLogMessage, objLogWriterType);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                EventLogWriter.WriterLog(exception);
            }
        }

        public ILogWriter CreateLogWriter(LogWriterType objLogWriterType)
        {
            switch (objLogWriterType)
            {
                case LogWriterType.DataLogWriter:
                    return new DataLogWriter();

                case LogWriterType.TextLogWriter:
                    return new TextLogWriter();

                case LogWriterType.EventLogWriter:
                    return new EventLogWriter();

                case LogWriterType.XmlLogWriter:
                    return new XmlLogWriter();
            }
            return new EventLogWriter();
        }

        public void EnqueueLog(LogInfo objLogInfo)
        {
            this._QueuePoolHelper.EnqueueInvokePool(objLogInfo);
        }

        public void WriteLog(LogMessage objLogMessage, LogWriterType objLogWriterType)
        {
            this.CreateLogWriter(objLogWriterType).WriterLog(objLogMessage);
        }

        public QueuePoolHelper<LogInfo> QueuePool
        {
            get
            {
                return this._QueuePoolHelper;
            }
        }
    }
}

