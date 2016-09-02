﻿namespace WTF.Logging
{
    using System;
    using System.Collections.Concurrent;

    public class XmlLogWriter : ILogWriter
    {
        private static ConcurrentDictionary<string, FileXmlLog> _FileLogList = new ConcurrentDictionary<string, FileXmlLog>();

        public void WriterLog(LogMessage objLogMessage)
        {
            string key = objLogMessage.ApplicationCode.ToString() + objLogMessage.LogCategory;
            FileXmlLog log = null;
            if (!_FileLogList.TryGetValue(key, out log))
            {
                log = new FileXmlLog(objLogMessage.LogWriteMap, objLogMessage.ApplicationCode, objLogMessage.LogCategory, LogWriterType.XmlLogWriter);
                _FileLogList.TryAdd(key, log);
            }
            log.LogWriteMap = objLogMessage.LogWriteMap;
            log.WriterLog(objLogMessage);
        }
    }
}

