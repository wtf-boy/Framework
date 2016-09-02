namespace WTF.Framework
{
    using System;
    using System.Runtime.CompilerServices;

    public abstract class JobWork
    {
        public JobWork()
        {
            this.IsStopWork = false;
        }

        public abstract InvokeResult Execute(string config);

        public bool IsStopWork { get; set; }

        public string JobWorkName { get; set; }
    }
}

