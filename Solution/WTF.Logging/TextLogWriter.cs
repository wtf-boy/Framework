namespace WTF.Logging
{
    using System;
    using System.Collections.Concurrent;

    public class TextLogWriter : ILogWriter
    {
        private static ConcurrentDictionary<string, FileTextLog> _FileLogList = new ConcurrentDictionary<string, FileTextLog>();

        public void WriterLog(LogMessage objLogMessage)
        {
            string key = objLogMessage.ApplicationCode.ToString() + objLogMessage.LogCategory;
            FileTextLog log = null;
            if (!_FileLogList.TryGetValue(key, out log))
            {
                log = new FileTextLog(objLogMessage.LogWriteMap, objLogMessage.ApplicationCode, objLogMessage.LogCategory, LogWriterType.TextLogWriter);
                _FileLogList.TryAdd(key, log);
            }
            log.LogWriteMap = objLogMessage.LogWriteMap;
            log.WriterLog(objLogMessage);
        }
    }
}

