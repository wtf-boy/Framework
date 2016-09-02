namespace WTF.Framework
{
    using System;
    using System.Diagnostics;
    using System.Runtime.InteropServices;
    using System.Threading;

    public static class CodeTimerHelper
    {
        [DllImport("kernel32.dll")]
        private static extern IntPtr GetCurrentThread();
        private static ulong GetCycleCount()
        {
            ulong cycleTime = 0L;
            QueryThreadCycleTime(GetCurrentThread(), ref cycleTime);
            return cycleTime;
        }

        public static void Initialize()
        {
            Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.High;
            Thread.CurrentThread.Priority = ThreadPriority.Highest;
            RunTime("", 1, delegate {
            }, false);
        }

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("kernel32.dll")]
        private static extern bool QueryThreadCycleTime(IntPtr threadHandle, ref ulong cycleTime);
        public static void RunTime(string name, int iteration, Action action, bool isShowOnMessage = false)
        {
            if (!string.IsNullOrEmpty(name))
            {
                int num;
                ConsoleColor foregroundColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(name);
                GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);
                int[] numArray = new int[GC.MaxGeneration + 1];
                for (num = 0; num <= GC.MaxGeneration; num++)
                {
                    numArray[num] = GC.CollectionCount(num);
                }
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                ulong cycleCount = GetCycleCount();
                Stopwatch stopwatch2 = new Stopwatch();
                for (num = 0; num < iteration; num++)
                {
                    if (isShowOnMessage)
                    {
                        stopwatch2.Start();
                        action();
                        stopwatch2.Stop();
                        Console.WriteLine(string.Concat(new object[] { "第", num + 1, "次消耗时间:\t", stopwatch.ElapsedMilliseconds.ToString("N0"), "ms" }));
                    }
                    else
                    {
                        action();
                    }
                }
                ulong num3 = GetCycleCount() - cycleCount;
                stopwatch.Stop();
                Console.ForegroundColor = foregroundColor;
                Console.WriteLine("\t消耗时间:\t" + stopwatch.ElapsedMilliseconds.ToString("N0") + "ms");
                Console.WriteLine("\t执行次数:\t" + iteration + "次");
                Console.WriteLine("\tCPU Cycles:\t" + num3.ToString("N0"));
                for (num = 0; num <= GC.MaxGeneration; num++)
                {
                    int num4 = GC.CollectionCount(num) - numArray[num];
                    Console.WriteLine(string.Concat(new object[] { "\t代回收 ", num, ": \t\t", num4 }));
                }
                Console.WriteLine();
            }
        }
    }
}

