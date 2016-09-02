namespace WTF.Framework
{
    using System;
    using System.Configuration;
    using System.Runtime.InteropServices;

    public class ConfigGroupHelper
    {
        public static string AppSettings(string groupName, string key, string configCode = "SevenGroup")
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException("name不能为空值");
            }
            if (string.IsNullOrWhiteSpace(groupName))
            {
                throw new ArgumentNullException("groupName不能为空值");
            }
            if (string.IsNullOrWhiteSpace(configCode))
            {
                throw new ArgumentNullException("configCode不能为空值");
            }
            GroupConfigSection section = (GroupConfigSection) ConfigHelper.GetSection("GroupConfig", configCode);
            GroupConfigElement element = section.Groups[groupName];
            if (element == null)
            {
                throw new ConfigurationErrorsException("找不到组名" + groupName + "配置节点");
            }
            string str = string.Empty;
            foreach (string str2 in key.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                GroupValueElement element2 = element.GroupValues[str2];
                if (element2 != null)
                {
                    str = element2.Value;
                    break;
                }
            }
            if (str == null)
            {
                str = string.Empty;
            }
            return str;
        }

        public static bool GetBool(string groupName, string key, bool defaultValue, string configCode = "SevenGroup")
        {
            string str = AppSettings(groupName, key, configCode);
            if (str.IsNull())
            {
                return defaultValue;
            }
            return Convert.ToBoolean(str);
        }

        public static bool GetBoolFalse(string groupName, string key, string configCode = "SevenGroup")
        {
            return GetBool(groupName, key, false, configCode);
        }

        public static bool GetBoolTrue(string groupName, string key, string configCode = "SevenGroup")
        {
            return GetBool(groupName, key, true, configCode);
        }

        public static int GetInt(string groupName, string key, int defaultValue, string configCode = "SevenGroup")
        {
            string str = AppSettings(groupName, key, configCode);
            if (str.IsNull())
            {
                return defaultValue;
            }
            return Convert.ToInt32(str);
        }

        public static long GetInt64(string groupName, string key, long defaultValue, string configCode = "SevenGroup")
        {
            string str = AppSettings(groupName, key, configCode);
            if (str.IsNull())
            {
                return defaultValue;
            }
            return Convert.ToInt64(str);
        }

        public static string GetValue(string groupName, string key, string defaultValue = "", string configCode = "SevenGroup")
        {
            string str = AppSettings(groupName, key, configCode);
            if (str.IsNull())
            {
                return defaultValue;
            }
            return str;
        }
    }
}

