using WTF.DataConfig.Entity;
using WTF.Framework;
using System;
using System.Linq;

namespace WTF.DataConfig
{
	public static class ThemeHelper
	{
		public static string GetThemeData(string ThemeTypeCode, string ConfigKey, string defaultValue, bool IsCache = true)
		{
			if (!IsCache)
			{
				ThemeRule themeRule = new ThemeRule();
				Sys_ThemeConfigInfo sys_ThemeConfigInfo = themeRule.CurrentEntities.sys_themeconfiginfo.FirstOrDefault((Sys_ThemeConfigInfo s) => s.ThemeTypeCode == ThemeTypeCode && s.ConfigKey == ConfigKey);
				if (sys_ThemeConfigInfo == null)
				{
					return defaultValue;
				}
				return sys_ThemeConfigInfo.ConfigValue;
			}
			else
			{
				string childKey = "Theme" + ThemeTypeCode + ConfigKey;
				object fromCache = CacheHelper.GetFromCache(CacheType.Framework.ToString(), childKey);
				if (fromCache != null)
				{
					return (string)fromCache;
				}
				ThemeRule themeRule2 = new ThemeRule();
				Sys_ThemeConfigInfo sys_ThemeConfigInfo2 = themeRule2.CurrentEntities.sys_themeconfiginfo.FirstOrDefault((Sys_ThemeConfigInfo s) => s.ThemeTypeCode == ThemeTypeCode && s.ConfigKey == ConfigKey);
				if (sys_ThemeConfigInfo2 == null)
				{
					return defaultValue;
				}
				sys_ThemeConfigInfo2.ConfigValue.AddToCache(CacheType.Framework.ToString(), childKey, 5);
				return sys_ThemeConfigInfo2.ConfigValue;
			}
		}

		public static string GetThemeData(string ThemeTypeCode, string ConfigKey)
		{
			return ThemeHelper.GetThemeData(ThemeTypeCode, ConfigKey, string.Empty, true);
		}
	}
}
