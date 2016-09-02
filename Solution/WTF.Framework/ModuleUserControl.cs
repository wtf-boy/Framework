namespace WTF.Framework
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public class ModuleUserControl : UserControl
    {
        private List<int> _UserTypeList = null;

        public virtual void CurrentContent_Sorting(object sender, GridViewSortEventArgs e)
        {
        }

        public virtual string EncryptModuleQuery(string url)
        {
            if (!this.AutoEncrypt)
            {
                return url;
            }
            return this.EncryptQuery(url);
        }

        public virtual string EncryptQuery(string url)
        {
            return url.SignatureQueryMD5("", this.EncryptKey, false);
        }

        public Guid GetGuid(string key)
        {
            return this.GetGuid(key, ObjectHelper.NullGuid);
        }

        public Guid GetGuid(string key, Guid defaultValue)
        {
            return this.GetString(key, defaultValue.ToString()).ConvertGuid();
        }

        public int GetInt(string key)
        {
            return this.GetInt(key, ObjectHelper.NullInt);
        }

        public int GetInt(string key, int defaultValue)
        {
            return this.GetString(key, defaultValue.ToString()).ConvertInt();
        }

        public virtual LogModuleInfo GetLogModuleInfo()
        {
            if (this.LogModuleTypeCode.IsNoNull())
            {
                return new LogModuleInfo(LogSectionHelper.IsDispose, this.LogModuleTypeCode);
            }
            return LogModuleInfo.CreateApplicationModuleInfo();
        }

        public long GetLong(string key)
        {
            return this.GetLong(key, ObjectHelper.NullLong);
        }

        public long GetLong(string key, long defaultValue)
        {
            return this.GetString(key, defaultValue.ToString()).ConvertLong();
        }

        public string GetString(string key)
        {
            return this.GetString(key, string.Empty);
        }

        public string GetString(string key, string defaultValue)
        {
            string str = "";
            foreach (string str2 in key.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                str = base.Request[str2];
                if (!string.IsNullOrWhiteSpace(str))
                {
                    str = str.FilterSql();
                    break;
                }
            }
            return (str.IsNoNull() ? str : defaultValue);
        }

        public virtual bool AutoEncrypt
        {
            get
            {
                return ModuleSectionHelper.GetModule(this.ModuleTypeCode).Page.AutoEncrypt;
            }
        }

        public virtual string EncryptKey
        {
            get
            {
                return ModuleSectionHelper.GetModule(this.ModuleTypeCode).Page.EncryptKey;
            }
        }

        public virtual string ErrorHint
        {
            get
            {
                return LogSectionHelper.GetLogSection().Error.ErrorHint;
            }
        }

        public virtual bool ErrorIsRedirect
        {
            get
            {
                return LogSectionHelper.GetLogSection().Error.ErrorIsRedirect;
            }
        }

        public virtual string ErrorUrl
        {
            get
            {
                return LogSectionHelper.GetLogSection().Error.ErrorUrl;
            }
        }

        public virtual bool IsPowerCheck
        {
            get
            {
                return ModuleSectionHelper.GetModule(this.ModuleTypeCode).Power.IsPowerCheck;
            }
        }

        public virtual bool IsPowerDataCheck
        {
            get
            {
                return ModuleSectionHelper.GetModule(this.ModuleTypeCode).Power.IsPowerDataCheck;
            }
        }

        public virtual string LayoutTheme
        {
            get
            {
                return ModuleSectionHelper.GetModule(this.ModuleTypeCode).ModuleStyle.LayoutTheme;
            }
        }

        public virtual string LoginUrl
        {
            get
            {
                return ModuleSectionHelper.GetModule(this.ModuleTypeCode).Power.LoginUrl;
            }
        }

        public virtual string LogModuleTypeCode { get; set; }

        public virtual string ModuleTypeCode
        {
            get
            {
                return "";
            }
        }

        public virtual string ModuleTypeID
        {
            get
            {
                return ModuleSectionHelper.GetModule(this.ModuleTypeCode).ModuleTypeID;
            }
        }

        public virtual WTF.Framework.OperateStyle OperateStyle
        {
            get
            {
                return ModuleSectionHelper.GetModule(this.ModuleTypeCode).ModuleStyle.OperateStyle;
            }
        }

        public virtual string PowerPageCode
        {
            get
            {
                return this.Page.ToString().Replace("ASP.", "").Replace("_aspx", "");
            }
        }

        public virtual string StyleTheme
        {
            get
            {
                return ModuleSectionHelper.GetModule(this.ModuleTypeCode).ModuleStyle.StyleTheme;
            }
        }

        public virtual string SystemName
        {
            get
            {
                return ModuleSectionHelper.GetModule(this.ModuleTypeCode).SystemName;
            }
        }

        public virtual List<int> UserTypeList
        {
            get
            {
                if (this._UserTypeList == null)
                {
                    this._UserTypeList = ModuleSectionHelper.GetModule(this.ModuleTypeCode).Power.UserType.ConvertListInt();
                }
                return this._UserTypeList;
            }
        }
    }
}

