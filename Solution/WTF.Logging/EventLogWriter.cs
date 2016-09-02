namespace WTF.Logging
{
    using WTF.Framework;
    using System;
    using System.Diagnostics;
    using System.Text;

    public class EventLogWriter : ILogWriter
    {
        private static string GetProcessName()
        {
            StringBuilder lpFilename = new StringBuilder(0x400);
            int num = NativeMethods.GetModuleFileName(NativeMethods.GetModuleHandle(null), lpFilename, lpFilename.Capacity);
            return lpFilename.ToString();
        }

        public void WriterLog(LogMessage objLogEntry)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("程序代码：").Append(objLogEntry.ApplicationCode.ToString()).Append("\r\n");
            builder.Append("模块代码：").Append(objLogEntry.LogModuleTypeList.ConvertListToString<string>().ToString()).Append("\r\n");
            builder.Append("模块分类：").Append(objLogEntry.LogCategory.ToString()).Append("\r\n");
            builder.Append("运行程序：").Append(objLogEntry.ProcessName).Append("\r\n");
            builder.Append("异常日期：").Append(objLogEntry.CreateDate.ToString()).Append("\r\n");
            builder.Append("远程主机：").Append(objLogEntry.UserHostAddress.ToString()).Append("\r\n");
            builder.Append("出错页面：").Append(objLogEntry.RawUrl.ToString()).Append("\r\n");
            builder.Append("页面来源：").Append(objLogEntry.UrlReferrer.ToString()).Append("\r\n");
            builder.Append("日志内容：").Append(objLogEntry.Message).Append("\r\n");
            builder.Append("日志结果：").Append(objLogEntry.ResultMessage).Append("\r\n");
            builder.Append("\r\n-----------------------------------------\r\n");
            EventLog.WriteEntry("Application", builder.ToString(), EventLogEntryType.Error);
        }

        public static void WriterLog(Exception exception)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("模块代码：").Append(LogModuleType.LogManager.ToString()).Append("\r\n");
            builder.Append("模块分类：").Append(LogCategory.ExceptionError.ToString()).Append("\r\n");
            builder.Append("运行程序：").Append(GetProcessName()).Append("\r\n");
            builder.Append("异常日期：").Append(DateTime.Now.ToString()).Append("\r\n");
            builder.Append("日志内容：").Append(exception.Message).Append("\r\n");
            builder.Append("调用堆栈：").Append(exception.StackTrace).Append("\r\n");
            builder.Append("\r\n-----------------------------------------\r\n");
            WriterLog(builder.ToString());
        }

        public static void WriterLog(string message)
        {
            EventLog.WriteEntry("Application", message.ToString(), EventLogEntryType.Error);
        }

        public static void WriterLog(string title, Exception exception)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("日志标题：").Append(title + "\r\n");
            builder.Append("运行程序：").Append(GetProcessName()).Append("\r\n");
            builder.Append("异常日期：").Append(DateTime.Now.ToString()).Append("\r\n");
            builder.Append("日志内容：").Append(exception.Message).Append("\r\n");
            builder.Append("调用堆栈：").Append(exception.StackTrace).Append("\r\n");
            builder.Append("\r\n-----------------------------------------\r\n");
            WriterLog(builder.ToString());
        }
    }
}

