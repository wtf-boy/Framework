namespace WTF.Framework
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Text;

    public static class ConsoleHelper
    {
        private static string CreateMessage(object value)
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
                    Exception exception = (Exception) value;
                    builder.Append("事件信息：").Append("\r\n").AppendLine(exception.Message);
                    builder.Append("\r\n").Append("堆栈跟踪：").Append("<br>").AppendLine(exception.StackTrace);
                    if (exception.InnerException != null)
                    {
                        builder.Append("\r\n").Append("内部事件信息：").Append("\r\n").AppendLine(exception.InnerException.Message);
                        builder.Append("\r\n").Append("内部堆栈跟踪：").Append("\r\n").AppendLine(exception.InnerException.StackTrace);
                    }
                    return builder.ToString();
                }
                return value.JsonJsSerialize();
            }
            catch (Exception exception2)
            {
                return ("消息转换出现异常:" + exception2.Message);
            }
        }

        private static string FormatMessage(object message, string title)
        {
            return (DateTime.Now.ToString("[yyyy-MM-dd HH:mm:ss:fff]") + title + "\r\n" + CreateMessage(message));
        }

        public static void WriteLine(this object message, string title = "")
        {
            Console.WriteLine(FormatMessage(message, title));
        }

        public static void WriteLine(this object message, ConsoleColor objConsoleColor, string title = "")
        {
            Console.ForegroundColor = objConsoleColor;
            Console.WriteLine(FormatMessage(message, title));
            Console.ResetColor();
        }

        public static void WriteLineRed(this object message, string title = "")
        {
            message.WriteLine(ConsoleColor.Red, title);
        }

        public static void WriteLineYellow(this object message, string title = "")
        {
            message.WriteLine(ConsoleColor.Yellow, title);
        }
    }
}

