using System;
using System.Data.EntityClient;
using System.Data.Objects;

namespace WTF.Theme.Entity
{
	public class UserThemeEntities : ObjectContext
	{
		private ObjectSet<Theme_ModuleTheme> _theme_moduletheme;

		private ObjectSet<Theme_ThemeConfig> _theme_themeconfig;

		private ObjectSet<Theme_UserTheme> _theme_usertheme;

		private ObjectSet<Theme_ModuleThemeInfo> _theme_modulethemeinfo;

		private ObjectSet<Theme_UserThemeInfo> _theme_userthemeinfo;

		public ObjectSet<Theme_ModuleTheme> theme_moduletheme
		{
			get
			{
				if (this._theme_moduletheme == null)
				{
					this._theme_moduletheme = base.CreateObjectSet<Theme_ModuleTheme>("theme_moduletheme");
				}
				return this._theme_moduletheme;
			}
		}

		public ObjectSet<Theme_ThemeConfig> theme_themeconfig
		{
			get
			{
				if (this._theme_themeconfig == null)
				{
					this._theme_themeconfig = base.CreateObjectSet<Theme_ThemeConfig>("theme_themeconfig");
				}
				return this._theme_themeconfig;
			}
		}

		public ObjectSet<Theme_UserTheme> theme_usertheme
		{
			get
			{
				if (this._theme_usertheme == null)
				{
					this._theme_usertheme = base.CreateObjectSet<Theme_UserTheme>("theme_usertheme");
				}
				return this._theme_usertheme;
			}
		}

		public ObjectSet<Theme_ModuleThemeInfo> theme_modulethemeinfo
		{
			get
			{
				if (this._theme_modulethemeinfo == null)
				{
					this._theme_modulethemeinfo = base.CreateObjectSet<Theme_ModuleThemeInfo>("theme_modulethemeinfo");
				}
				return this._theme_modulethemeinfo;
			}
		}

		public ObjectSet<Theme_UserThemeInfo> theme_userthemeinfo
		{
			get
			{
				if (this._theme_userthemeinfo == null)
				{
					this._theme_userthemeinfo = base.CreateObjectSet<Theme_UserThemeInfo>("theme_userthemeinfo");
				}
				return this._theme_userthemeinfo;
			}
		}

		public UserThemeEntities() : base("name=UserThemeEntities", "UserThemeEntities")
		{
			base.ContextOptions.LazyLoadingEnabled = true;
		}

		public UserThemeEntities(string connectionString) : base(connectionString, "UserThemeEntities")
		{
			base.ContextOptions.LazyLoadingEnabled = true;
		}

		public UserThemeEntities(EntityConnection connection) : base(connection, "UserThemeEntities")
		{
			base.ContextOptions.LazyLoadingEnabled = true;
		}

		public void AddTotheme_moduletheme(Theme_ModuleTheme theme_ModuleTheme)
		{
			base.AddObject("theme_moduletheme", theme_ModuleTheme);
		}

		public void AddTotheme_themeconfig(Theme_ThemeConfig theme_ThemeConfig)
		{
			base.AddObject("theme_themeconfig", theme_ThemeConfig);
		}

		public void AddTotheme_usertheme(Theme_UserTheme theme_UserTheme)
		{
			base.AddObject("theme_usertheme", theme_UserTheme);
		}

		public void AddTotheme_modulethemeinfo(Theme_ModuleThemeInfo theme_ModuleThemeInfo)
		{
			base.AddObject("theme_modulethemeinfo", theme_ModuleThemeInfo);
		}

		public void AddTotheme_userthemeinfo(Theme_UserThemeInfo theme_UserThemeInfo)
		{
			base.AddObject("theme_userthemeinfo", theme_UserThemeInfo);
		}
	}
}
