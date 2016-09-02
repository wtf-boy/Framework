namespace WTF.Logging
{
    using System;

    public interface ILogWriter
    {
        void WriterLog(LogMessage objLogMessage);
    }
}

