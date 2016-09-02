namespace WTF.Logging
{
    using WTF.Framework;
    using WTF.Logging.Entity;
    using System;
    using System.Runtime.InteropServices;

    public class OperationLoger
    {
        private string _LogModuleType = "Application";
        private static OperationLogWriterFactory _LogWriterFactory = new OperationLogWriterFactory();

        public OperationLoger(string logModuleType = "Application")
        {
            if (logModuleType.IsNoNullOrWhiteSpace())
            {
                this._LogModuleType = logModuleType;
            }
        }

        public void JudgeSqlLog(string commandText, string commandTextTemp)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(commandText))
                {
                    string str = commandText.MatchValue(@"INSERT\s*INTO\s*(?<tableName>\w+)\s*\(", "tableName");
                    if (!string.IsNullOrWhiteSpace(str))
                    {
                        this.WriteLog(OperationType.Insert, str.Trim(), commandTextTemp);
                    }
                    else
                    {
                        str = commandText.MatchValue(@"UPDATE\s*(?<tableName>\w+)\s*SET", "tableName");
                        if (!string.IsNullOrWhiteSpace(str))
                        {
                            this.WriteLog(OperationType.Update, str.Trim(), commandTextTemp);
                        }
                        else
                        {
                            str = commandText.MatchValue(@"DELETE\s*FROM\s*(?<tableName>\w+)\s*", "tableName");
                            if (!string.IsNullOrWhiteSpace(str))
                            {
                                this.WriteLog(OperationType.Delete, str.Trim(), commandTextTemp);
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                LogHelper.Write(this._LogModuleType, "分类操作日志出现异常", exception, "");
            }
        }

        public void WriteLog(OperationType operationType, string tableName, string sqlQuery)
        {
            OperationLogInfo objLogInfo = new OperationLogInfo {
                OperationType = operationType,
                LogModuleTypeCode = this._LogModuleType,
                TableName = tableName,
                SqlQuery = sqlQuery
            };
            _LogWriterFactory.EnqueueLog(objLogInfo);
        }

        public static void WriteOperatorLog(OperationType operationType, string MenuPowerID, string MenuName, int UserID, string Account, string CommandName, string Title, string Description, object OperationData)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(MenuPowerID) && !string.IsNullOrWhiteSpace(Title))
                {
                    if (string.IsNullOrWhiteSpace(Account))
                    {
                        Account = "";
                    }
                    if (string.IsNullOrWhiteSpace(CommandName))
                    {
                        CommandName = operationType.ToString();
                    }
                    if (string.IsNullOrWhiteSpace(Description))
                    {
                        Description = operationType.GetEnumDescription() + ":" + Title;
                    }
                    if (string.IsNullOrWhiteSpace(MenuName))
                    {
                        MenuName = "";
                    }
                    loger_operationhistory _operationhistory = new loger_operationhistory {
                        MenuPowerID = MenuPowerID,
                        UserID = UserID,
                        MenuName = MenuName,
                        Account = Account,
                        OperationTypeID = (int) operationType,
                        CommandName = CommandName.CutWord(10),
                        CreateDate = DateTime.Now,
                        Title = Title.CutWord(500),
                        Description = Description.CutWord(500),
                        OperationData = (OperationData == null) ? "" : OperationData.JsonJsSerialize(),
                        UserHostAddress = RequestHelper.GetRealIp()
                    };
                    LogRule rule = new LogRule();
                    rule.CurrentEntities.AddTologer_operationhistory(_operationhistory);
                    rule.CurrentEntities.SaveChanges();
                }
            }
            catch (Exception exception)
            {
                new Logger("Application").WriteLog("记录用户操作行为异常", exception);
            }
        }

        public string LogModuleType
        {
            get
            {
                return this._LogModuleType;
            }
            set
            {
                if (value.IsNoNullOrWhiteSpace())
                {
                    this._LogModuleType = value;
                }
            }
        }
    }
}

