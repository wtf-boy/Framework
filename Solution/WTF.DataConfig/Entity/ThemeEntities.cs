using System;
using System.Data.EntityClient;
using System.Data.Objects;

namespace WTF.DataConfig.Entity
{
	public class ThemeEntities : ObjectContext
	{
		private ObjectSet<Sys_Theme> _sys_theme;

		private ObjectSet<Sys_ThemeConfig> _sys_themeconfig;

		private ObjectSet<Sys_ThemeType> _sys_themetype;

		private ObjectSet<Sys_ThemeTypeConfig> _sys_themetypeconfig;

		private ObjectSet<Sys_ThemeConfigInfo> _sys_themeconfiginfo;

		public ObjectSet<Sys_Theme> sys_theme
		{
			get
			{
				if (this._sys_theme == null)
				{
					this._sys_theme = base.CreateObjectSet<Sys_Theme>("sys_theme");
				}
				return this._sys_theme;
			}
		}

		public ObjectSet<Sys_ThemeConfig> sys_themeconfig
		{
			get
			{
				if (this._sys_themeconfig == null)
				{
					this._sys_themeconfig = base.CreateObjectSet<Sys_ThemeConfig>("sys_themeconfig");
				}
				return this._sys_themeconfig;
			}
		}

		public ObjectSet<Sys_ThemeType> sys_themetype
		{
			get
			{
				if (this._sys_themetype == null)
				{
					this._sys_themetype = base.CreateObjectSet<Sys_ThemeType>("sys_themetype");
				}
				return this._sys_themetype;
			}
		}

		public ObjectSet<Sys_ThemeTypeConfig> sys_themetypeconfig
		{
			get
			{
				if (this._sys_themetypeconfig == null)
				{
					this._sys_themetypeconfig = base.CreateObjectSet<Sys_ThemeTypeConfig>("sys_themetypeconfig");
				}
				return this._sys_themetypeconfig;
			}
		}

		public ObjectSet<Sys_ThemeConfigInfo> sys_themeconfiginfo
		{
			get
			{
				if (this._sys_themeconfiginfo == null)
				{
					this._sys_themeconfiginfo = base.CreateObjectSet<Sys_ThemeConfigInfo>("sys_themeconfiginfo");
				}
				return this._sys_themeconfiginfo;
			}
		}

		public ThemeEntities() : base("name=ThemeEntities", "ThemeEntities")
		{
			base.ContextOptions.LazyLoadingEnabled = true;
		}

		public ThemeEntities(string connectionString) : base(connectionString, "ThemeEntities")
		{
			base.ContextOptions.LazyLoadingEnabled = true;
		}

		public ThemeEntities(EntityConnection connection) : base(connection, "ThemeEntities")
		{
			base.ContextOptions.LazyLoadingEnabled = true;
		}

		public void AddTosys_theme(Sys_Theme sys_Theme)
		{
			base.AddObject("sys_theme", sys_Theme);
		}

		public void AddTosys_themeconfig(Sys_ThemeConfig sys_ThemeConfig)
		{
			base.AddObject("sys_themeconfig", sys_ThemeConfig);
		}

		public void AddTosys_themetype(Sys_ThemeType sys_ThemeType)
		{
			base.AddObject("sys_themetype", sys_ThemeType);
		}

		public void AddTosys_themetypeconfig(Sys_ThemeTypeConfig sys_ThemeTypeConfig)
		{
			base.AddObject("sys_themetypeconfig", sys_ThemeTypeConfig);
		}

		public void AddTosys_themeconfiginfo(Sys_ThemeConfigInfo sys_ThemeConfigInfo)
		{
			base.AddObject("sys_themeconfiginfo", sys_ThemeConfigInfo);
		}
	}
}
