﻿namespace WTF.Logging
{
    using WTF.Framework;
    using System;
    using System.Runtime.InteropServices;

    public class Logger
    {
        private string _LogModuleType = "Application";

        public Logger(string logModuleType = "Application")
        {
            if (logModuleType.IsNoNullOrWhiteSpace())
            {
                this._LogModuleType = logModuleType;
            }
        }

        public void WriteLog(string logTitle, Exception objExp)
        {
            if (!string.IsNullOrWhiteSpace(this.LogModuleType))
            {
                LogHelper.Write(this.LogModuleType, logTitle, objExp, "");
            }
        }

        public void WriteLog(LogCategory logCategory, string logTitle, object message)
        {
            this.WriteLog(logCategory.ToString(), logTitle, message);
        }

        public void WriteLog(string logCategory, string logTitle, object message)
        {
            if (!string.IsNullOrWhiteSpace(this.LogModuleType))
            {
                LogHelper.Write(this.LogModuleType, logCategory, logTitle, message, "");
            }
        }

        public void WriteLog(LogCategory logCategory, string logTitle, object message, object resultMessage)
        {
            this.WriteLog(logCategory.ToString(), logTitle, message, resultMessage);
        }

        public void WriteLog(string logCategory, string logTitle, object message, object resultMessage)
        {
            if (!string.IsNullOrWhiteSpace(this.LogModuleType))
            {
                LogHelper.Write(this.LogModuleType, logCategory, logTitle, message, resultMessage, "");
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

