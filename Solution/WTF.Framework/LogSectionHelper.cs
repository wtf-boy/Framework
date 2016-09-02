namespace WTF.Framework
{
    using System;

    public class LogSectionHelper
    {
        public static LogCategoryElement GetCategory(string CategoryCode)
        {
            return Categorys[CategoryCode];
        }

        public static LogSection GetLogSection()
        {
            return (LogSection) ConfigHelper.GetSection("Log", "LogConfig");
        }

        public static string ApplicationCode
        {
            get
            {
                string application = GetLogSection().Application;
                if (string.IsNullOrWhiteSpace(application))
                {
                    throw new ArgumentNullException("LogConfig 未配置Application值");
                }
                return application;
            }
        }

        public static LogCategoryCollection Categorys
        {
            get
            {
                LogSection logSection = GetLogSection();
                LogCategoryCollection categorys = logSection.Categorys;
                return logSection.Categorys;
            }
        }

        public static string Host
        {
            get
            {
                LogSection logSection = GetLogSection();
                string str = (logSection != null) ? logSection.Host : "";
                if (str.IsNull())
                {
                    str = SysVariable.GetLocalIp().ToString().ToString();
                }
                return str;
            }
        }

        public static bool IsDispose
        {
            get
            {
                return GetLogSection().IsDispose;
            }
        }

        public static bool IsRecordESearch
        {
            get
            {
                return GetLogSection().IsRecordESearch;
            }
        }

        public static bool IsRecordOperation
        {
            get
            {
                return GetLogSection().IsRecordOperation;
            }
        }

        public static bool IsRecordSolrSql
        {
            get
            {
                return GetLogSection().IsRecordSolrSql;
            }
        }

        public static bool IsRecordSql
        {
            get
            {
                return GetLogSection().IsRecordSql;
            }
        }

        public static string LogWriteMap
        {
            get
            {
                return GetLogSection().LogWriteMap;
            }
        }
    }
}

