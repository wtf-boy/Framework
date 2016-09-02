namespace WTF.Framework
{
    using System;
    using System.Diagnostics;
    using System.Text;

    public static class EventLogHelper
    {
        private static string GetProcessName()
        {
            StringBuilder lpFilename = new StringBuilder(0x400);
            int num = NativeMethods.GetModuleFileName(NativeMethods.GetModuleHandle(null), lpFilename, lpFilename.Capacity);
            return lpFilename.ToString();
        }

        public static void WriterLog(Exception exception)
        {
            StringBuilder builder = new StringBuilder();
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

