using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WTF.Framework;
using WTF.Power.Entity;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //List<string> aa = new List<string>() { "1", "2", "3", "4"};
            //ConsoleHelper.WriteLineYellow(aa.ConvertStringID());

            ModuleEntities db = new ModuleEntities();
            List<Sys_Module> objsss = db.sys_module.ToList();


        }
    }
}
