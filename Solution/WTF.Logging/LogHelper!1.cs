﻿namespace WTF.Logging
{
    using WTF.Framework;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using System.Threading;

    public class LogHelper<T>
    {
        public static void DisposeException(T logModuleType, Exception exception)
        {
            LogHelper<T>.DisposeException(logModuleType, "", exception);
        }

        public static void DisposeException(T logModuleType, string logTitle, Exception exception)
        {
            Type type = exception.GetType();
            if (exception is InfoHintException)
            {
                exception.Message.MessageDialog();
            }
            else if (type != typeof(ThreadAbortException))
            {
                if (exception is ArgumentInputNullException)
                {
                    LogHelper<T>.Write(logModuleType, logTitle, (ArgumentInputNullException) exception, "");
                }
                else
                {
                    LogHelper<T>.Write(logModuleType, logTitle, exception, "");
                }
                if (exception is ArgumentInputNullException)
                {
                    exception.Message.MessageDialog();
                }
                else
                {
                    "程序出现异常请与管理员联系！！！".MessageDialog();
                }
            }
        }

        public static void WaitWriteComplete()
        {
            LogHelper.WaitWriteComplete();
        }

        public static void Write(T logModuleType, ArgumentInputNullException message, string messageID = "")
        {
            LogHelper<T>.Write(logModuleType, message.Message, message, messageID);
        }

        public static void Write(T logModuleType, Exception message, string messageID = "")
        {
            LogHelper<T>.Write(logModuleType, message.Message.CutText(30), message, messageID);
        }

        public static void Write(T logModuleType, string logTitle, ArgumentInputNullException message, string messageID = "")
        {
            List<string> logModuleTypeList = new List<string> {
                logModuleType.ToString()
            };
            if (!(!message.LogModuleType.IsNoNullOrWhiteSpace() || logModuleTypeList.Contains(message.LogModuleType)))
            {
                logModuleTypeList.Add(message.LogModuleType);
            }
            LogHelper.Write(logModuleTypeList, LogCategory.ArgumentInputError, logTitle, message, "", messageID);
        }

        public static void Write(T logModuleType, string logTitle, Exception message, string messageID = "")
        {
            List<string> logModuleTypeList = new List<string> {
                logModuleType.ToString()
            };
            if (message is ArgumentInputNullException)
            {
                string str = ((ArgumentInputNullException) message).LogModuleType;
                logTitle = ((ArgumentInputNullException) message).Message;
                if (!(!str.IsNoNullOrWhiteSpace() || logModuleTypeList.Contains(str)))
                {
                    logModuleTypeList.Add(str);
                }
            }
            LogHelper.Write(logModuleTypeList, LogCategory.ExceptionError, logTitle, message, "", messageID);
        }

        public static void Write(T logModuleType, LogCategory logCategory, string logTitle, object message, string messageID = "")
        {
            LogHelper.Write(new List<string> { logModuleType.ToString() }, logCategory, logTitle, message, "", messageID);
        }

        public static void Write(T logModuleType, LogCategory logCategory, string logTitle, string message, string messageID = "")
        {
            LogHelper.Write(new List<string> { logModuleType.ToString() }, logCategory, logTitle, message, "", messageID);
        }

        public static void Write(T logModuleType, LogCategory logCategory, string logTitle, object message, object resultMessage, string messageID = "")
        {
            LogHelper.Write(new List<string> { logModuleType.ToString() }, logCategory, logTitle, message, resultMessage, messageID);
        }
    }
}

