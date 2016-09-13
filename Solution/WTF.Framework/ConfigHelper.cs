namespace WTF.Framework
{
    using System;
    using System.Configuration;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Threading;

    public class ConfigHelper
    {
        private static ReaderWriterLockSlimHelper objConfigWriterLockSlimHelper = new ReaderWriterLockSlimHelper();

        public static string Application(string name, string configCode = "ApplicationConfig")
        {
            string str = string.Empty;
            foreach (string str2 in name.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                str = ConfigurationManager.AppSettings[str2];
                if (str != null)
                {
                    break;
                }
                System.Configuration.Configuration configurationCode = GetConfigurationCode(configCode);
                if (configurationCode != null)
                {
                    KeyValueConfigurationElement element = configurationCode.AppSettings.Settings[str2];
                    if (element != null)
                    {
                        str = element.Value;
                        break;
                    }
                }
            }
            if (str == null)
            {
                str = string.Empty;
            }
            return str;
        }

        public static bool ApplicationBool(string key, bool defaultValue, string configCode = "ApplicationConfig")
        {
            string str = Application(key, configCode);
            if (str.IsNull())
            {
                return defaultValue;
            }
            return Convert.ToBoolean(str);
        }

        public static bool ApplicationBoolFalse(string key, string configCode = "ApplicationConfig")
        {
            return ApplicationBool(key, false, configCode);
        }

        public static bool ApplicationBoolTrue(string key, string configCode = "ApplicationConfig")
        {
            return ApplicationBool(key, true, configCode);
        }

        public static int ApplicationInt(string key, int defaultValue, string configCode = "ApplicationConfig")
        {
            string str = Application(key, configCode);
            if (str.IsNull())
            {
                return defaultValue;
            }
            return Convert.ToInt32(str);
        }

        public static long ApplicationInt64(string key, long defaultValue, string configCode = "ApplicationConfig")
        {
            string str = Application(key, configCode);
            if (str.IsNull())
            {
                return defaultValue;
            }
            return Convert.ToInt64(str);
        }

        public static string ApplicationValue(string key, string defaultValue = "", string configCode = "ApplicationConfig")
        {
            string str = Application(key, configCode);
            if (str.IsNull())
            {
                return defaultValue;
            }
            return str;
        }

        public static string AppSettings(string name, string configCode = "WTFConfig")
        {
            string str = string.Empty;
            foreach (string str2 in name.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                str = ConfigurationManager.AppSettings[str2];
                if (str != null)
                {
                    break;
                }
                System.Configuration.Configuration configurationCode = GetConfigurationCode(configCode);
                if (configurationCode != null)
                {
                    KeyValueConfigurationElement element = configurationCode.AppSettings.Settings[str2];
                    if (element != null)
                    {
                        str = element.Value;
                        break;
                    }
                }
            }
            if (str == null)
            {
                str = string.Empty;
            }
            return str;
        }

        public static bool AppSettingsBool(string key, bool defaultValue, string configCode = "WTFConfig")
        {
            string str = AppSettings(key, configCode);
            if (str.IsNull())
            {
                return defaultValue;
            }
            return Convert.ToBoolean(str);
        }

        public static bool AppSettingsBoolFalse(string key, string configCode = "WTFConfig")
        {
            return AppSettingsBool(key, false, configCode);
        }

        public static bool AppSettingsBoolTrue(string key, string configCode = "WTFConfig")
        {
            return AppSettingsBool(key, true, configCode);
        }

        public static int AppSettingsInt(string key, int defaultValue, string configCode = "WTFConfig")
        {
            string str = AppSettings(key, configCode);
            if (str.IsNull())
            {
                return defaultValue;
            }
            return Convert.ToInt32(str);
        }

        public static long AppSettingsInt64(string key, long defaultValue, string configCode = "WTFConfig")
        {
            string str = AppSettings(key, configCode);
            if (str.IsNull())
            {
                return defaultValue;
            }
            return Convert.ToInt64(str);
        }

        public static string AppSettingsValue(string key, string defaultValue = "", string configCode = "WTFConfig")
        {
            string str = AppSettings(key, configCode);
            if (str.IsNull())
            {
                return defaultValue;
            }
            return str;
        }

        public static string ConnectionString(string name, string configCode = "WTFConfig")
        {
            ConnectionStringSettings settings = null;
            settings = ConfigurationManager.ConnectionStrings[name];
            if (settings == null)
            {
                System.Configuration.Configuration configurationCode = GetConfigurationCode(configCode);
                if (configurationCode != null)
                {
                    settings = configurationCode.ConnectionStrings.ConnectionStrings[name];
                }
            }
            if (settings == null)
            {
                throw new ConfigurationErrorsException("未配置数据连接" + name);
            }
            return settings.ConnectionString;
        }

        public static bool GetBoolFalseValue(string key)
        {
            return GetBoolValue(key, false);
        }

        public static bool GetBoolTrueValue(string key)
        {
            return GetBoolValue(key, true);
        }

        public static bool GetBoolValue(string key, bool defaultValue)
        {
            string str = AppSettings(key, "WTFConfig");
            if (str == string.Empty)
            {
                return defaultValue;
            }
            return Convert.ToBoolean(str);
        }

        public static string GetConfigPath(string ConfigCode)
        {
            string str = ConfigurationManager.AppSettings[ConfigCode];
            if (str.IsNull())
            {
                str = AppDomain.CurrentDomain.BaseDirectory.TrimEnd(new char[] { '\\' }) + @"\" + ConfigCode + ".config";
            }
            return str;
        }

        private static System.Configuration.Configuration GetConfigurationCode(string configCode)
        {
            if (string.IsNullOrWhiteSpace(configCode))
            {
                return null;
            }
            System.Configuration.Configuration fromCache = CacheHelper.GetFromCache<System.Configuration.Configuration>(WTF.Framework.CacheType.Framework.ToString(), configCode);
            if (fromCache == null)
            {
                if (CacheHelper.GetFromCache(WTF.Framework.CacheType.Framework.ToString(), configCode + "NoFile") != null)
                {
                    return null;
                }
                ReaderWriterLockSlim slim = objConfigWriterLockSlimHelper.CreateLock(configCode);
                slim.EnterWriteLock();
                try
                {
                    fromCache = CacheHelper.GetFromCache<System.Configuration.Configuration>(WTF.Framework.CacheType.Framework.ToString(), configCode);
                    if (fromCache == null)
                    {
                        if (CacheHelper.GetFromCache(WTF.Framework.CacheType.Framework.ToString(), configCode + "NoFile") != null)
                        {
                            return null;
                        }
                        string configPath = GetConfigPath(configCode);
                        if (!File.Exists(configPath))
                        {
                            true.AddToFileCache(WTF.Framework.CacheType.Framework.ToString(), configCode + "NoFile", configPath);
                            return null;
                        }
                        ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap {
                            ExeConfigFilename = configPath
                        };
                        fromCache = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
                        fromCache.AddToFileCache(WTF.Framework.CacheType.Framework.ToString(), configCode, configPath);
                    }
                    return fromCache;
                }
                finally
                {
                    slim.ExitWriteLock();
                }
            }
            return fromCache;
        }

        public static long GetInt64Value(string key, long defaultValue)
        {
            string str = AppSettings(key, "WTFConfig");
            if (str == string.Empty)
            {
                return defaultValue;
            }
            return Convert.ToInt64(str);
        }

        public static int GetIntValue(string key, int defaultValue)
        {
            string str = AppSettings(key, "WTFConfig");
            if (str == string.Empty)
            {
                return defaultValue;
            }
            return Convert.ToInt32(str);
        }

        public static object GetSection(string sectionName, string configCode = "WTFConfig")
        {
            object section = null;
            section = ConfigurationManager.GetSection(sectionName);
            if (section == null)
            {
                System.Configuration.Configuration configurationCode = GetConfigurationCode(configCode);
                if (configurationCode != null)
                {
                    section = configurationCode.GetSection(sectionName);
                }
            }
            if (section == null)
            {
                throw new ConfigurationErrorsException(configCode + ".config文件未配置节点" + sectionName);
            }
            return section;
        }

        public static string GetValue(string key)
        {
            return GetValue(key, string.Empty);
        }

        public static string GetValue(string key, string defaultVale)
        {
            string str = AppSettings(key, "WTFConfig");
            if (str.IsNull())
            {
                return defaultVale;
            }
            return str;
        }

        public static bool IsRelease
        {
            get
            {
                return GetBoolTrueValue("IsRelease");
            }
        }
    }
}

