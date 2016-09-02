namespace WTF.Theme
{
    using WTF.Framework;
    using WTF.Logging;
    using WTF.Theme.Entity;
    using System;
    using System.Data.Objects;
    using System.Linq;

    public class UserThemeRule
    {
        private UserThemeEntities objCurrentEntities;

        public void DeleteModuleThemeByKey(Guid primaryKey)
        {
            this.CurrentEntities.theme_moduletheme.DeleteDataPrimaryKey<WTF.Theme.Entity.Theme_ModuleTheme>(primaryKey.ToString());
            this.CurrentEntities.theme_usertheme.DeleteDataSql<WTF.Theme.Entity.Theme_UserTheme>("ModuleThemeID='" + primaryKey.ToString() + "'", new object[0]);
        }

        public void DeleteThemeConfigByKey(string primaryKey)
        {
            if (this.CurrentEntities.theme_moduletheme.Any<WTF.Theme.Entity.Theme_ModuleTheme>(s => s.ThemeConfigID == primaryKey))
            {
                SysAssert.InfoHintAssert("对不起系统模块已经引用此主题，因此无法删除");
            }
            this.CurrentEntities.theme_themeconfig.DeleteDataPrimaryKey<WTF.Theme.Entity.Theme_ThemeConfig>(primaryKey.ToString());
        }

        public void DeleteUserThemeByKey(string primaryKey)
        {
            this.CurrentEntities.theme_usertheme.DeleteDataPrimaryKey<WTF.Theme.Entity.Theme_UserTheme>(primaryKey);
        }

        public UserThemeInfo GetUserTheme(string ModuleTypeID, string UserID)
        {
            WTF.Theme.Entity.Theme_UserThemeInfo info = null;
            if (UserID.IsNoNull())
            {
                info = this.theme_userthemeinfo.FirstOrDefault<WTF.Theme.Entity.Theme_UserThemeInfo>(s => (s.UserID == UserID) && (s.ModuleTypeID == ModuleTypeID));
                if (info != null)
                {
                    return new UserThemeInfo { LayoutPath = info.LayoutPath, Theme = info.Theme, OperateStyle = (OperateStyle) info.OperateStyle };
                }
            }
            return null;
        }

        public void InsertModuleTheme(WTF.Theme.Entity.Theme_ModuleTheme objTheme_ModuleTheme)
        {
            this.CurrentEntities.AddTotheme_moduletheme(objTheme_ModuleTheme);
            this.CurrentEntities.SaveChanges();
        }

        public void InsertThemeConfig(WTF.Theme.Entity.Theme_ThemeConfig objTheme_ThemeConfig)
        {
            objTheme_ThemeConfig.ThemeConfigName.CheckIsNull<string>("请输入主题名称", "UserLog");
            objTheme_ThemeConfig.Theme.CheckIsNull<string>("请输入主题样式", "UserLog");
            objTheme_ThemeConfig.LayoutPath.CheckIsNull<string>("请输入布局样式", "UserLog");
            this.CurrentEntities.AddTotheme_themeconfig(objTheme_ThemeConfig);
            this.CurrentEntities.SaveChanges();
        }

        public void InsertUserTheme(WTF.Theme.Entity.Theme_UserTheme objTheme_UserTheme)
        {
            this.CurrentEntities.AddTotheme_usertheme(objTheme_UserTheme);
            this.CurrentEntities.SaveChanges();
        }

        public void RevertUserTheme(string UserID, string ModuleTypeID)
        {
            WTF.Theme.Entity.Theme_UserTheme entity = this.CurrentEntities.theme_usertheme.FirstOrDefault<WTF.Theme.Entity.Theme_UserTheme>(s => (s.UserID == UserID) && (s.ModuleTypeID == ModuleTypeID));
            if (entity != null)
            {
                this.CurrentEntities.DeleteObject(entity);
                this.CurrentEntities.SaveChanges();
            }
        }

        public void UpdateModuleTheme(WTF.Theme.Entity.Theme_ModuleTheme objTheme_ModuleTheme)
        {
            this.CurrentEntities.SaveChanges();
        }

        public void UpdateThemeConfig(WTF.Theme.Entity.Theme_ThemeConfig objTheme_ThemeConfig)
        {
            objTheme_ThemeConfig.ThemeConfigName.CheckIsNull<string>("请输入主题名称", "UserLog");
            objTheme_ThemeConfig.Theme.CheckIsNull<string>("请输入主题样式", "UserLog");
            objTheme_ThemeConfig.LayoutPath.CheckIsNull<string>("请输入布局样式", "UserLog");
            this.CurrentEntities.SaveChanges();
        }

        public void UpdateUserTheme(WTF.Theme.Entity.Theme_UserTheme objTheme_UserTheme)
        {
            this.CurrentEntities.SaveChanges();
        }

        public UserThemeEntities CurrentEntities
        {
            get
            {
                if (this.objCurrentEntities == null)
                {
                    this.objCurrentEntities = new UserThemeEntities(EntitiesHelper.GetConnectionString<UserThemeEntities>());
                }
                return this.objCurrentEntities;
            }
        }

        public ObjectQuery<WTF.Theme.Entity.Theme_ModuleTheme> theme_moduletheme
        {
            get
            {
                return this.CurrentEntities.theme_moduletheme;
            }
        }

        public ObjectQuery<WTF.Theme.Entity.Theme_ModuleThemeInfo> theme_modulethemeinfo
        {
            get
            {
                return this.CurrentEntities.theme_modulethemeinfo;
            }
        }

        public ObjectQuery<WTF.Theme.Entity.Theme_ThemeConfig> theme_themeconfig
        {
            get
            {
                return this.CurrentEntities.theme_themeconfig;
            }
        }

        public ObjectQuery<WTF.Theme.Entity.Theme_UserTheme> theme_usertheme
        {
            get
            {
                return this.CurrentEntities.theme_usertheme;
            }
        }

        public ObjectQuery<WTF.Theme.Entity.Theme_UserThemeInfo> theme_userthemeinfo
        {
            get
            {
                return this.CurrentEntities.theme_userthemeinfo;
            }
        }
    }
}

