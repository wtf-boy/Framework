using WTF.DataConfig.Entity;
using WTF.Framework;
using WTF.Logging;
using System;
using System.Data.Objects;
using System.Linq;

namespace WTF.DataConfig
{
	public class ThemeRule
	{
		private ThemeEntities objCurrentEntities;

		public ThemeEntities CurrentEntities
		{
			get
			{
				if (this.objCurrentEntities == null)
				{
					this.objCurrentEntities = new ThemeEntities(EntitiesHelper.GetConnectionString<ThemeEntities>());
				}
				return this.objCurrentEntities;
			}
		}

		public ObjectQuery<Sys_Theme> Sys_Theme
		{
			get
			{
				return this.CurrentEntities.sys_theme;
			}
		}

		public ObjectQuery<Sys_ThemeConfig> Sys_ThemeConfig
		{
			get
			{
				return this.CurrentEntities.sys_themeconfig;
			}
		}

		public ObjectQuery<Sys_ThemeType> Sys_ThemeType
		{
			get
			{
				return this.CurrentEntities.sys_themetype;
			}
		}

		public ObjectQuery<Sys_ThemeTypeConfig> Sys_ThemeTypeConfig
		{
			get
			{
				return this.CurrentEntities.sys_themetypeconfig;
			}
		}

		public void InsertTheme(Sys_Theme objSys_Theme)
		{
			objSys_Theme.ThemeName.CheckIsNull("请输入主题名称", "ParameterLog");
			this.CurrentEntities.AddTosys_theme(objSys_Theme);
			foreach (Sys_ThemeTypeConfig current in from s in this.Sys_ThemeTypeConfig
			where s.ThemeTypeID == objSys_Theme.ThemeTypeID
			select s)
			{
				this.CurrentEntities.AddTosys_themeconfig(new Sys_ThemeConfig
				{
					ConfigKey = current.ConfigKey,
					ConfigName = current.ConfigName,
					ConfigRemark = current.ConfigRemark,
					ConfigValue = current.ConfigValue,
					ThemeID = objSys_Theme.ThemeID,
					ThemeConfigID = Guid.NewGuid().ToString(),
					ThemeTypeConfigID = current.ThemeTypeConfigID
				});
			}
			this.CurrentEntities.SaveChanges();
		}

		public void UpdateTheme(Sys_Theme objSys_Theme)
		{
			objSys_Theme.ThemeName.CheckIsNull("请输入主题名称", "ParameterLog");
			this.CurrentEntities.SaveChanges();
		}

		public void StartTheme(string ThemeTypeID, string themdID)
		{
			foreach (Sys_Theme current in from S in this.CurrentEntities.sys_theme
			where S.ThemeTypeID == ThemeTypeID
			select S)
			{
				current.IsEnable = (current.ThemeID == themdID);
			}
			this.CurrentEntities.SaveChanges();
		}

		public void DeleteThemeByKey(string primaryKey)
		{
			this.CurrentEntities.sys_theme.DeleteDataPrimaryKey(primaryKey);
		}

		public void DeleteTheme(string condition)
		{
			this.CurrentEntities.sys_theme.DeleteData(condition, new ObjectParameter[0]);
		}

		public void InsertThemeConfig(Sys_ThemeConfig objSys_ThemeConfig)
		{
			objSys_ThemeConfig.ConfigName.CheckIsNull("请输入配置名称", "ParameterLog");
			objSys_ThemeConfig.ConfigKey.CheckIsNull("请输入配置键", "ParameterLog");
			objSys_ThemeConfig.ConfigValue.CheckIsNull("请输入配置数据", "ParameterLog");
			this.CurrentEntities.AddTosys_themeconfig(objSys_ThemeConfig);
			this.CurrentEntities.SaveChanges();
		}

		public void UpdateThemeConfig(Sys_ThemeConfig objSys_ThemeConfig)
		{
			objSys_ThemeConfig.ConfigName.CheckIsNull("请输入配置名称", "ParameterLog");
			objSys_ThemeConfig.ConfigKey.CheckIsNull("请输入配置键", "ParameterLog");
			objSys_ThemeConfig.ConfigValue.CheckIsNull("请输入配置数据", "ParameterLog");
			this.CurrentEntities.SaveChanges();
		}

		public void DeleteThemeConfigByKey(string primaryKey)
		{
			this.CurrentEntities.sys_themeconfig.DeleteDataPrimaryKey(primaryKey);
		}

		public void DeleteThemeConfig(string condition)
		{
			this.CurrentEntities.sys_themeconfig.DeleteData(condition, new ObjectParameter[0]);
		}

		public void InsertThemeType(Sys_ThemeType objSys_ThemeType)
		{
			objSys_ThemeType.ThemeTypeName.CheckIsNull("请输入主题类型名称", "ParameterLog");
			objSys_ThemeType.ThemeTypeCode.CheckIsNull("请输入主题类型代码", "ParameterLog");
			this.CurrentEntities.AddTosys_themetype(objSys_ThemeType);
			this.CurrentEntities.SaveChanges();
		}

		public void UpdateThemeType(Sys_ThemeType objSys_ThemeType)
		{
			objSys_ThemeType.ThemeTypeName.CheckIsNull("请输入主题类型名称", "ParameterLog");
			objSys_ThemeType.ThemeTypeCode.CheckIsNull("请输入主题类型代码", "ParameterLog");
			this.CurrentEntities.SaveChanges();
		}

		public void DeleteThemeTypeByKey(string primaryKey)
		{
			this.CurrentEntities.sys_themetype.DeleteDataPrimaryKey(primaryKey);
		}

		public void DeleteThemeType(string condition)
		{
			this.CurrentEntities.sys_themetype.DeleteData(condition, new ObjectParameter[0]);
		}

		public void InsertThemeTypeConfig(Sys_ThemeTypeConfig objSys_ThemeTypeConfig, bool IsApplyConfig)
		{
			objSys_ThemeTypeConfig.ConfigName.CheckIsNull("请输入配置名称", "ParameterLog");
			objSys_ThemeTypeConfig.ConfigKey.CheckIsNull("请输入配置键", "ParameterLog");
			objSys_ThemeTypeConfig.ConfigValue.CheckIsNull("请输入配置数据", "ParameterLog");
			this.CurrentEntities.AddTosys_themetypeconfig(objSys_ThemeTypeConfig);
			if (IsApplyConfig)
			{
				foreach (Sys_Theme current in from s in this.Sys_Theme
				where s.ThemeTypeID == objSys_ThemeTypeConfig.ThemeTypeID
				select s)
				{
					Sys_ThemeConfig sys_ThemeConfig = new Sys_ThemeConfig();
					sys_ThemeConfig.ThemeConfigID = Guid.NewGuid().ToString();
					sys_ThemeConfig.ThemeID = current.ThemeID;
					sys_ThemeConfig.ThemeTypeConfigID = objSys_ThemeTypeConfig.ThemeTypeConfigID;
					sys_ThemeConfig.ConfigName = objSys_ThemeTypeConfig.ConfigName;
					sys_ThemeConfig.ConfigKey = objSys_ThemeTypeConfig.ConfigKey;
					sys_ThemeConfig.ConfigValue = objSys_ThemeTypeConfig.ConfigValue;
					sys_ThemeConfig.ConfigRemark = objSys_ThemeTypeConfig.ConfigRemark;
					this.CurrentEntities.AddTosys_themeconfig(sys_ThemeConfig);
				}
			}
			this.CurrentEntities.SaveChanges();
		}

		public void UpdateThemeTypeConfig(Sys_ThemeTypeConfig objSys_ThemeTypeConfig, bool IsApplyConfig)
		{
			objSys_ThemeTypeConfig.ConfigName.CheckIsNull("请输入配置名称", "ParameterLog");
			objSys_ThemeTypeConfig.ConfigKey.CheckIsNull("请输入配置键", "ParameterLog");
			objSys_ThemeTypeConfig.ConfigValue.CheckIsNull("请输入配置数据", "ParameterLog");
			if (IsApplyConfig)
			{
				foreach (Sys_ThemeConfig current in from s in this.Sys_ThemeConfig
				where s.ThemeTypeConfigID == objSys_ThemeTypeConfig.ThemeTypeConfigID
				select s)
				{
					current.ConfigName = objSys_ThemeTypeConfig.ConfigName;
					current.ConfigKey = objSys_ThemeTypeConfig.ConfigKey;
					current.ConfigRemark = objSys_ThemeTypeConfig.ConfigRemark;
				}
			}
			this.CurrentEntities.SaveChanges();
		}

		public void DeleteThemeTypeConfigByKey(string ThemeTypeConfigID)
		{
			foreach (Sys_ThemeConfig current in from s in this.Sys_ThemeConfig
			where s.ThemeTypeConfigID == ThemeTypeConfigID
			select s)
			{
				this.CurrentEntities.DeleteObject(current);
			}
			this.CurrentEntities.sys_themetypeconfig.DeleteDataPrimaryKey(ThemeTypeConfigID.ToString());
		}

		public void DeleteThemeTypeConfig(string condition)
		{
			this.CurrentEntities.sys_themetypeconfig.DeleteData(condition, new ObjectParameter[0]);
		}
	}
}
