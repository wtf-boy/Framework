namespace WTF.Framework
{
    using System;

    public abstract class JobTiming
    {
        protected JobTiming()
        {
        }

        public abstract void Execute(string config);
    }
}

