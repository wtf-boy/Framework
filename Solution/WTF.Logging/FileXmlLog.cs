namespace WTF.Logging
{
    using System;

    public class FileXmlLog : FileLog
    {
        public FileXmlLog(string logWriteMap, string applicationCode, string category, LogWriterType logWriterType) : base(logWriteMap, applicationCode, category, logWriterType)
        {
        }

        public override string FileBegin
        {
            get
            {
                return "<LogMessageList>";
            }
        }

        public override string FileEnd
        {
            get
            {
                return "</LogMessageList>";
            }
        }
    }
}

