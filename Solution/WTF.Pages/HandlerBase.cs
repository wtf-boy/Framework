namespace WTF.Pages
{
    using WTF.Framework;
    using WTF.Logging;
    using WTF.Power;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Web;
    using System.Web.SessionState;

    public abstract class HandlerBase : IHttpHandler, IRequiresSessionState
    {
        private int _CachePowerMinute = -2147483648;
        private ModuleRule _CurrentModuleRule = null;
        private string _ModuleTypeID = "";
        private string _PowerPageCode = string.Empty;
        private UserRule _UserRule = null;
        private List<int> _UserTypeList = null;
        public HttpContext CurrentContext;

        protected HandlerBase()
        {
        }

        public virtual InvokeResult CheckHandlerPower()
        {
            InvokeResult result = new InvokeResult();
            if (this.IsPowerCheck)
            {
                int userTypeCID = this.CurrentUser.UserTypeCID;
                if (!this.CheckUrlQuery())
                {
                    result = this.CreatePowerError("对不起你访问不正确的地址");
                }
                if (!this.UserTypeList.Contains(userTypeCID))
                {
                    result = this.CreatePowerError((this.CurrentUser.UserTypeCID == -1) ? "对不起你登录超时，请重新登录" : "对不起你没有权限访问此平台");
                }
                if ((this.CheckPowerType == PowerType.PagePower) && this.PowerPageCode.IsNoNull())
                {
                    if (!this.CheckPowerPage())
                    {
                        result = this.CreatePowerError("对不起你没有权限访问此界面");
                    }
                    return result;
                }
                if (this.CheckPowerType != PowerType.FramePower)
                {
                    return result;
                }
                if (!this.CheckPowerFrame())
                {
                    result = this.CreatePowerError("对不起你没有权限访问此平台");
                }
            }
            return result;
        }

        public virtual bool CheckPowerFrame()
        {
            return (this.CurrentUser.IsSuper || this.CurrentUserRule.CheckPowerFrame(this.ModuleTypeID, this.CurrentUser.UserID));
        }

        public virtual bool CheckPowerPage()
        {
            if (this.CurrentUser.IsSuper)
            {
                return true;
            }
            if (this.CoteModuleID.IsNoNull() && this.CoteID.IsNoNull())
            {
                return this.CurrentUserRule.CheckPowerPage(this.ModuleTypeID, this.CoteModuleID, this.CoteID, this.PowerPageCode, this.CurrentUser.UserID);
            }
            return this.CurrentUserRule.CheckPowerPage(this.ModuleTypeID, this.PowerPageCode, this.CurrentUser.UserID);
        }

        protected virtual bool CheckUrlQuery()
        {
            if (this.IsCheckUrl)
            {
                return RequestHelper.SignatureQueryMD5Check(0, "", this.EncryptKey, false);
            }
            return true;
        }

        public virtual InvokeResult CreatePowerError(string resultMessage)
        {
            return new InvokeResult { ResultCode = "-1", ResultMessage = resultMessage, Data = "" };
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
            if (this.LogModuleType.IsNoNull())
            {
                return new LogModuleInfo(LogSectionHelper.IsDispose, this.LogModuleType);
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
            string str = string.Empty;
            foreach (string str2 in key.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                str = this.CurrentContext.Request[str2];
                if (!string.IsNullOrEmpty(str))
                {
                    str = str.FilterSql();
                    break;
                }
            }
            return (str.IsNoNull() ? str : defaultValue);
        }

        public abstract void ProcessRequest(HttpContext context);
        public void WriteOperatorLog(OperationType operationType, string Title, object OperationData)
        {
            this.WriteOperatorLog(operationType, Title, "", OperationData);
        }

        public void WriteOperatorLog(OperationType operationType, string Title, string Description, object OperationData)
        {
            if (string.IsNullOrWhiteSpace(this.MenuPowerID))
            {
                throw new ArgumentNullException("自动获取MenuPowerID失败请重写MenuPowerID");
            }
            this.WriteOperatorLog(operationType, this.MenuPowerID, this.PowerName, Title, Description, OperationData);
        }

        public void WriteOperatorLog(OperationType operationType, string MenuPowerID, string MenuName, string Title, string Description, object OperationData)
        {
            this.WriteOperatorLog(operationType, MenuPowerID, MenuName, "", Title, Description, OperationData);
        }

        public void WriteOperatorLog(OperationType operationType, string menuPowerID, string MenuName, string CommandName, string Title, string Description, object OperationData)
        {
            OperationLoger.WriteOperatorLog(operationType, menuPowerID, MenuName, this.CurrentUser.ID, this.CurrentUser.Account, CommandName, Title, Description, OperationData);
        }

        public virtual bool AutoEncrypt
        {
            get
            {
                return ModuleSectionHelper.GetModule(this.ModuleTypeCode).Page.AutoEncrypt;
            }
        }

        public virtual int CachePowerMinute
        {
            get
            {
                if (this._CachePowerMinute == -2147483648)
                {
                    this._CachePowerMinute = ModuleSectionHelper.GetModule(this.ModuleTypeCode).Power.CachePowerMinute;
                }
                return this._CachePowerMinute;
            }
        }

        public virtual PowerType CheckPowerType
        {
            get
            {
                return PowerType.PagePower;
            }
        }

        public virtual string CoteID
        {
            get
            {
                return this.GetString("CoteID");
            }
        }

        public virtual int CoteKeyID
        {
            get
            {
                return this.GetInt("CoteKeyID");
            }
        }

        public virtual string CoteModuleID
        {
            get
            {
                return this.GetString("CoteModuleID");
            }
        }

        public ModuleRule CurrentModuleRule
        {
            get
            {
                if (this._CurrentModuleRule == null)
                {
                    this._CurrentModuleRule = new ModuleRule();
                }
                return this._CurrentModuleRule;
            }
        }

        public UserInfo CurrentUser
        {
            get
            {
                return this.CurrentUserRule.GetCurrentUser(this.ModuleTypeID);
            }
        }

        public UserRule CurrentUserRule
        {
            get
            {
                if (this._UserRule == null)
                {
                    this._UserRule = new UserRule();
                }
                return this._UserRule;
            }
        }

        public virtual string EncryptKey
        {
            get
            {
                return ModuleSectionHelper.GetModule(this.ModuleTypeCode).Page.EncryptKey;
            }
        }

        public virtual bool IsCacheLog
        {
            get
            {
                return ModuleSectionHelper.GetModule(this.ModuleTypeCode).IsCacheLog;
            }
        }

        public virtual bool IsCachePagePower
        {
            get
            {
                return ModuleSectionHelper.GetModule(this.ModuleTypeCode).Power.IsCachePagePower;
            }
        }

        public virtual bool IsCheckUrl
        {
            get
            {
                return this.AutoEncrypt;
            }
        }

        public virtual bool IsPowerCheck
        {
            get
            {
                return ModuleSectionHelper.GetModule(this.ModuleTypeCode).Power.IsPowerCheck;
            }
        }

        public virtual bool IsReusable
        {
            get
            {
                return false;
            }
        }

        public virtual string LogModuleType { get; set; }

        public virtual string MenuPowerID
        {
            get
            {
                string powerID = this.PowerID;
                if (powerID.IsNullOrWhiteSpace())
                {
                    powerID = string.Format("{0}{1}", this.CoteModuleID, this.CoteID);
                }
                return powerID;
            }
        }

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
                if (this._ModuleTypeID == "")
                {
                    this._ModuleTypeID = ModuleSectionHelper.GetModule(this.ModuleTypeCode).ModuleTypeID;
                }
                return this._ModuleTypeID;
            }
        }

        public virtual string PowerID
        {
            get
            {
                return this.GetString("PowerID");
            }
        }

        public virtual string PowerName
        {
            get
            {
                return this.GetString("PowerName");
            }
        }

        public virtual string PowerPageCode
        {
            get
            {
                return this._PowerPageCode;
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

