using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
namespace WTF.Framework
{
    public static class ActionHelper
    {
        public static bool RunAction(Action objAct, TimeSpan expTimeSpan, int retryRunCount = 10, bool IsThrowExp = true, string expTitle = "")
        {
            for (int i = 0; i <= retryRunCount; i++)
            {
                try
                {
                    objAct();
                    return true;
                }
                catch (Exception exception)
                {
                    exception.WriteLineRed(expTitle);
                    if (IsThrowExp && (i == retryRunCount))
                    {
                        throw exception;
                    }
                }
                Thread.Sleep(expTimeSpan);
            }
            return false;
        }

        public static TResult RunAction<TResult>(this Func<TResult> objFunc, TimeSpan expTimeSpan, int retryRunCount = 10, bool IsThrowExp = true, string expTitle = "")
        {
            for (int i = 0; i <= retryRunCount; i++)
            {
                try
                {
                    return objFunc();
                }
                catch (Exception exception)
                {
                    exception.WriteLineRed(expTitle);
                    if (IsThrowExp && (i == retryRunCount))
                    {
                        throw exception;
                    }
                }
                Thread.Sleep(expTimeSpan);
            }
            return default(TResult);
        }

        public static void RunActionWhile(Action objAct, TimeSpan expTimeSpan, bool IsThrowExp = true, string expTitle = "")
        {
            while (true)
            {
                try
                {
                    objAct();
                    return;
                }
                catch (Exception exception)
                {
                    exception.WriteLineRed(expTitle);
                }
                Thread.Sleep(expTimeSpan);
            }
        }

        public static TResult RunActionWhile<TResult>(this Func<TResult> objFunc, TimeSpan expTimeSpan, bool IsThrowExp = true, string expTitle = "")
        {
            while (true)
            {
                try
                {
                    return objFunc();
                }
                catch (Exception exception)
                {
                    exception.WriteLineRed(expTitle);
                }
                Thread.Sleep(expTimeSpan);
            }
        }
    }
}

