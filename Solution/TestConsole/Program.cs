using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WTF.Framework;
using WTF.Theme;
using WTF.Logging;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //List<string> aa = new List<string>() { "1", "2", "3", "4"};
            //ConsoleHelper.WriteLineYellow(aa.ConvertStringID());
            Logger objLogger = new Logger();
            objLogger.WriteLog(LogCategory.RecordInfo, "这是测试日志记录", null);
            ConsoleHelper.WriteLineYellow("写入日志成功");
            
        }
    }
}
