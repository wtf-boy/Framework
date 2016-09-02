namespace WTF.Framework
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public class ModulePage : Page
    {
        private int _CachePowerMinute = -2147483648;
        private string _ModuleTypeID = "";
        private int _PageIndex = -1;
        private int _PageSize = -1;
        private string _PowerPageCode = string.Empty;
        private string _TransferFourID = string.Empty;
        private string _TransferID = string.Empty;
        private string _TransferThreeID = string.Empty;
        private string _TransferTwoID = string.Empty;
        private List<int> _UserTypeList = null;
        protected bool IsLoadPageStateFromToSession = false;

        public string AddExtendQueryID(string url)
        {
            if (this.PowerID.IsNoNull() && (url.IndexOf("PowerID") < 0))
            {
                if (url.IndexOf("?") >= 0)
                {
                    url = url + "&PowerID=" + this.PowerID;
                }
                else
                {
                    url = url + "?PowerID=" + this.PowerID;
                }
            }
            if (this.PowerName.IsNoNull() && (url.IndexOf("PowerName") < 0))
            {
                if (url.IndexOf("?") >= 0)
                {
                    url = url + "&PowerName=" + this.PowerName;
                }
                else
                {
                    url = url + "?PowerName=" + this.PowerName;
                }
            }
            if (this.TransferID.IsNoNull() && (url.IndexOf("TransferID") < 0))
            {
                if (url.IndexOf("?") >= 0)
                {
                    url = url + "&TransferID=" + this.TransferID;
                }
                else
                {
                    url = url + "?TransferID=" + this.TransferID;
                }
            }
            if (this.TransferTwoID.IsNoNull() && (url.IndexOf("TransferTwoID") < 0))
            {
                if (url.IndexOf("?") >= 0)
                {
                    url = url + "&TransferTwoID=" + this.TransferTwoID;
                }
                else
                {
                    url = url + "?TransferTwoID=" + this.TransferTwoID;
                }
            }
            if (this.TransferThreeID.IsNoNull() && (url.IndexOf("TransferThreeID") < 0))
            {
                if (url.IndexOf("?") >= 0)
                {
                    url = url + "&TransferThreeID=" + this.TransferThreeID;
                }
                else
                {
                    url = url + "?TransferThreeID=" + this.TransferThreeID;
                }
            }
            if (this.TransferFourID.IsNoNull() && (url.IndexOf("TransferFourID") < 0))
            {
                if (url.IndexOf("?") >= 0)
                {
                    url = url + "&TransferFourID=" + this.TransferFourID;
                }
                else
                {
                    url = url + "?TransferFourID=" + this.TransferFourID;
                }
            }
            if (this.CoteID.IsNoNull() && (url.IndexOf("CoteID") < 0))
            {
                if (url.IndexOf("?") >= 0)
                {
                    url = url + "&CoteID=" + this.CoteID;
                }
                else
                {
                    url = url + "?CoteID=" + this.CoteID;
                }
            }
            if (this.CoteKeyID.IsNoNull() && (url.IndexOf("CoteKeyID") < 0))
            {
                if (url.IndexOf("?") >= 0)
                {
                    url = url + "&CoteKeyID=" + this.CoteKeyID;
                }
                else
                {
                    url = url + "?CoteKeyID=" + this.CoteKeyID;
                }
            }
            if (this.CoteModuleID.IsNoNull() && (url.IndexOf("CoteModuleID") < 0))
            {
                if (url.IndexOf("?") >= 0)
                {
                    url = url + "&CoteModuleID=" + this.CoteModuleID;
                }
                else
                {
                    url = url + "?CoteModuleID=" + this.CoteModuleID;
                }
            }
            if (this.CoteIsParent.IsNoNull() && (url.IndexOf("CoteIsParent") < 0))
            {
                if (url.IndexOf("?") >= 0)
                {
                    url = url + "&CoteIsParent=" + this.CoteIsParent;
                    return url;
                }
                url = url + "?CoteIsParent=" + this.CoteIsParent;
            }
            return url;
        }

        public bool CheckEditObjectIsNull(object obj)
        {
            return this.CheckEditObjectIsNull(obj, "对不起此记录已经不存在");
        }

        public bool CheckEditObjectIsNull(object obj, string message)
        {
            if (obj == null)
            {
                this.MessageDialog(message, base.Request.UrlReferrer.PathAndQuery.Replace("&CheckCode=.+", "", RegexOptions.IgnoreCase).DecodeUrl());
            }
            return (obj == null);
        }

        public virtual bool CheckIsPowerData(string ModuleTypeID, string PowerPageCode)
        {
            return false;
        }

        public virtual string CheckPowerFieldData(string moduleTypeID, string pageName, string fieldNameSource, string fieldName)
        {
            return "";
        }

        public virtual bool CheckPowerOperateButton(string commandName)
        {
            return true;
        }

        public virtual bool CheckPowerOperateButton(string moduleCode, string commandName)
        {
            return true;
        }

        protected virtual bool CheckUrlQuery()
        {
            if (this.IsCheckUrl)
            {
                return RequestHelper.SignatureQueryMD5Check(0, "", this.EncryptKey, false);
            }
            return true;
        }

        public void CloseForm()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("window.parent.closeSubMainPages();");
            this.RegisterScript(builder.ToString(), RegisterType.StartupScript);
        }

        public virtual void CurrentContent_Sorting(object sender, GridViewSortEventArgs e)
        {
        }

        protected override NameValueCollection DeterminePostBackMode()
        {
            NameValueCollection second = base.DeterminePostBackMode();
            if (second == null)
            {
                if (this.Session[this.PageStateCode] == null)
                {
                    return second;
                }
                this.IsLoadPageStateFromToSession = true;
                Pair pair = this.Session[this.PageStateCode] as Pair;
                if (pair != null)
                {
                    second = pair.Second as NameValueCollection;
                }
            }
            return second;
        }

        public void DialogMessage(string message, string returnValue, bool isClose = true)
        {
            StringBuilder builder = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(message))
            {
                builder.Append("alert(\"").Append(message).Append("\");");
            }
            if (!string.IsNullOrWhiteSpace(returnValue))
            {
                builder.Append("window.parent.dialogIframeReturn(window.name.replace('frame', ''),'" + returnValue + "');");
            }
            if (isClose)
            {
                builder.Append("window.parent.closedialogIframe(window.name.replace('frame', ''));");
            }
            this.RegisterScript(builder.ToString(), RegisterType.StartupScript);
        }

        public void DialogOpenerReloadScript(bool IsClosed, string message = "")
        {
            StringBuilder builder = new StringBuilder();
            if (message.IsNoNull())
            {
                builder.Append("alert(\"").Append(message).Append("\");");
            }
            builder.Append("var pWindow=window.dialogArguments;  if(pWindow != null){  pWindow.location.href =pWindow.location.href; }else{  window.opener.location.href = window.opener.location.href; };");
            if (IsClosed)
            {
                builder.Append("window.close();").Append("\r\n");
            }
            this.RegisterScript(builder.ToString(), RegisterType.ClientBlock);
        }

        public void DialogOpenerReloadScript(string theUrl, bool IsClosed, string message = "")
        {
            theUrl = this.AddExtendQueryID(theUrl);
            theUrl = this.EncryptModuleQuery(theUrl);
            StringBuilder builder = new StringBuilder();
            if (message.IsNoNull())
            {
                builder.Append("alert(\"").Append(message).Append("\");");
            }
            builder.Append("var pWindow=window.dialogArguments;  if(pWindow != null){  pWindow.location.href = '" + theUrl + "'; }else{  window.opener.location.href = '" + theUrl + "'; };");
            if (IsClosed)
            {
                builder.Append("window.close();").Append("\r\n");
            }
            this.RegisterScript(builder.ToString(), RegisterType.ClientBlock);
        }

        public void DialogReturnValue(string returnValue, bool isClose = true)
        {
            this.DialogMessage("", returnValue, isClose);
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
            return this.GetString(key, defaultValue.ToString(), true).ConvertGuid();
        }

        public decimal GetHeadersDecimal(string key)
        {
            return this.GetHeadersDecimal(key, ObjectHelper.NullDecimal);
        }

        public decimal GetHeadersDecimal(string key, decimal defaultValue)
        {
            return this.GetHeadersString(key, defaultValue.ToString()).ConvertDecimal();
        }

        public double GetHeadersDouble(string key)
        {
            return this.GetHeadersDouble(key, ObjectHelper.NullDouble);
        }

        public double GetHeadersDouble(string key, double defaultValue)
        {
            return this.GetHeadersString(key, defaultValue.ToString()).ConvertDouble();
        }

        public double GetHeadersFloat(string key)
        {
            return (double) this.GetHeadersFloat(key, ObjectHelper.NullFloat);
        }

        public float GetHeadersFloat(string key, float defaultValue)
        {
            return this.GetHeadersString(key, defaultValue.ToString()).ConvertFloat();
        }

        public int GetHeadersInt(string key)
        {
            return this.GetHeadersInt(key, ObjectHelper.NullInt).ConvertInt();
        }

        public int GetHeadersInt(string key, int defaultValue)
        {
            return this.GetHeadersString(key, defaultValue.ToString()).ConvertInt();
        }

        public long GetHeadersLong(string key)
        {
            return this.GetHeadersLong(key, ObjectHelper.NullLong);
        }

        public long GetHeadersLong(string key, long defaultValue)
        {
            return this.GetHeadersString(key, defaultValue.ToString()).ConvertLong();
        }

        public string GetHeadersString(string key, string defaultValue = "")
        {
            string str = string.Empty;
            foreach (string str2 in key.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                str = base.Request.Headers[str2];
                if (!string.IsNullOrEmpty(str))
                {
                    break;
                }
            }
            return (str.IsNoNull() ? str : defaultValue);
        }

        public int GetInt(string key)
        {
            return this.GetInt(key, ObjectHelper.NullInt);
        }

        public int GetInt(string key, int defaultValue)
        {
            return this.GetString(key, defaultValue.ToString(), true).ConvertInt();
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
            return this.GetString(key, defaultValue.ToString(), true).ConvertLong();
        }

        public virtual bool GetPowerOperateButton(string moduleTypeID, string moduleCode, string commandName)
        {
            return true;
        }

        public virtual bool GetPowerOperateButton(string moduleTypeID, string moduleCode, string coteModuleID, string coteID, string commandName)
        {
            return true;
        }

        public virtual List<OperateModuleInfo> GetPowerOperateModule(string moduleTypeID, string moduleCode, OperatePlaceType operatePlaceType)
        {
            return new List<OperateModuleInfo>();
        }

        public virtual List<OperateModuleInfo> GetPowerOperateModule(string moduleTypeID, string moduleCode, string coteModuleID, string coteID, OperatePlaceType operatePlaceType)
        {
            return new List<OperateModuleInfo>();
        }

        public string GetString(string key, bool filterSql = true)
        {
            return this.GetString(key, string.Empty, filterSql);
        }

        public string GetString(string key, string defaultValue, bool filterSql = true)
        {
            string str = "";
            foreach (string str2 in key.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                str = base.Request[str2];
                if (!string.IsNullOrWhiteSpace(str))
                {
                    if (filterSql)
                    {
                        str = str.FilterSql();
                    }
                    break;
                }
            }
            return (str.IsNoNull() ? str : defaultValue);
        }

        public virtual bool IsCheckCotePower(string coteModuleID)
        {
            return false;
        }

        protected override object LoadPageStateFromPersistenceMedium()
        {
            if (this.IsLoadPageStateFromToSession)
            {
                Pair pair = (Pair) this.Session[this.PageStateCode];
                this.PageIndex = (int) this.Session[this.PageStateCode + "PageIndex"];
                this.ClickSortExpression = (string) this.Session[this.PageStateCode + "ClickSortExpression"];
                object first = pair.First;
                this.Session.Remove(this.PageStateCode);
                this.Session.Remove(this.PageStateCode + "PageIndex");
                this.Session.Remove(this.PageStateCode + "ClickSortExpression");
                return first;
            }
            return base.LoadPageStateFromPersistenceMedium();
        }

        public void MessageDialog(string message)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("alert(\"").Append(message).Append("\")");
            this.RegisterScript(builder.ToString(), RegisterType.StartupScript);
        }

        public void MessageDialog(string message, bool isCloseForm)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("alert(\"").Append(message).Append("\");");
            if (isCloseForm)
            {
                builder.Append("window.parent.closeSubMainPages();");
            }
            this.RegisterScript(builder.ToString(), RegisterType.StartupScript);
        }

        public void MessageDialog(string message, string navigateUrl)
        {
            navigateUrl = this.AddExtendQueryID(navigateUrl);
            navigateUrl = this.EncryptModuleQuery(navigateUrl);
            StringBuilder builder = new StringBuilder();
            builder.Append("alert(\"").Append(message).Append("\");");
            builder.Append("location.href='").Append(navigateUrl).Append("'");
            this.RegisterScript(builder.ToString(), RegisterType.ClientBlock);
        }

        public void MessageDialogContinue(string message, string navigateUrl)
        {
            navigateUrl = this.AddExtendQueryID(navigateUrl);
            navigateUrl = this.EncryptModuleQuery(navigateUrl);
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("if (window.confirm('" + message + ",是否继续新增?'))");
            builder.AppendLine("{");
            builder.Append("location.href='").Append(this.RawUrl).Append("'");
            builder.AppendLine("}");
            builder.AppendLine(" else");
            builder.AppendLine("{");
            builder.Append("location.href='").Append(navigateUrl).Append("'");
            builder.AppendLine("}");
            this.RegisterScript(builder.ToString(), RegisterType.ClientBlock);
        }

        public void MessageDialogTabEnableAll(string message)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("alert(\"").Append(message).Append("\");");
            builder.Append("window.parent.TabEnableAll();");
            this.RegisterScript(builder.ToString(), RegisterType.StartupScript);
        }

        public void MessageDialogTabNext(string message)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("alert(\"").Append(message).Append("\");");
            builder.Append("window.parent.TabNext();");
            this.RegisterScript(builder.ToString(), RegisterType.StartupScript);
        }

        public void OpenerReloadScript(string theUrl, bool IsClosed)
        {
            theUrl = this.AddExtendQueryID(theUrl);
            theUrl = this.EncryptModuleQuery(theUrl);
            StringBuilder builder = new StringBuilder();
            builder.Append("var pWindow=window.dialogArguments;  if(pWindow != null){  pWindow.location.href = '" + theUrl + "'; }else{  window.opener.location.href = '" + theUrl + "'; };");
            if (IsClosed)
            {
                builder.Append("window.close();").Append("\r\n");
            }
            this.RegisterScript(builder.ToString(), RegisterType.ClientBlock);
        }

        public void OpenerReturnValueScript(string returnVale)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" window.returnValue = '" + returnVale + "';").Append("\r\n");
            builder.Append(" window.close();").Append("\r\n");
            this.RegisterScript(builder.ToString(), RegisterType.ClientBlock);
        }

        public void OpenModalDialogScript(string pUrl, int pWidth, int pHeight)
        {
            pUrl = this.AddExtendQueryID(pUrl);
            pUrl = this.EncryptModuleQuery(pUrl);
            StringBuilder builder = new StringBuilder();
            builder.Append("var iNewX = (screen.availWidth - " + pWidth.ToString() + ")/2;").Append("\r\n");
            builder.Append("var iNewY = (screen.availHeight - " + pHeight.ToString() + ")/2;").Append("\r\n");
            builder.Append("var theDes = \"dialogWidth:" + pWidth.ToString() + "px;dialogHeight:" + pHeight.ToString() + "px;edge:sunken;help:no;status:no;scroll:no;\"").Append("\r\n");
            builder.Append("var ReturnValue = window.showModalDialog('" + pUrl + "',window,theDes);").Append("\r\n");
            this.RegisterScript(builder.ToString(), RegisterType.ClientBlock);
        }

        public void OpenModelessDialogScript(string pUrl, int pWidth, int pHeight)
        {
            pUrl = this.AddExtendQueryID(pUrl);
            pUrl = this.EncryptModuleQuery(pUrl);
            StringBuilder builder = new StringBuilder();
            builder.Append("var iNewX = (screen.availWidth - " + pWidth.ToString() + ")/2;").Append("\r\n");
            builder.Append("var iNewY = (screen.availHeight - " + pHeight.ToString() + ")/2;").Append("\r\n");
            builder.Append("var theDes = \"dialogWidth:" + pWidth.ToString() + "px;dialogHeight:" + pHeight.ToString() + "px;edge:sunken;help:no;status:no;scroll:no;\"").Append("\r\n");
            builder.Append("var ReturnValue = window.showModelessDialog('" + pUrl + "',window,theDes);").Append("\r\n");
            this.RegisterScript(builder.ToString(), RegisterType.ClientBlock);
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler sourceControl, string eventArgument)
        {
            if (!this.IsLoadPageStateFromToSession)
            {
                base.RaisePostBackEvent(sourceControl, eventArgument);
            }
        }

        public void Redirect(string url)
        {
            this.Redirect(url, false, this.AutoEncrypt);
        }

        public void Redirect(string url, bool isEncrypt)
        {
            this.Redirect(url, false, isEncrypt);
        }

        public void Redirect(string url, bool isSaveState, bool isEncrypt)
        {
            if (isSaveState)
            {
                this.SavePageStateToSession();
            }
            url = this.AddExtendQueryID(url);
            if (isEncrypt)
            {
                base.Response.Redirect(this.EncryptQuery(url), true);
            }
            else
            {
                base.Response.Redirect(url.EncodeUrlQuery(), true);
            }
        }

        public void RedirectState(string url)
        {
            this.Redirect(url, true, this.AutoEncrypt);
        }

        public void RedirectState(string url, bool isEncrypt)
        {
            this.Redirect(url, true, isEncrypt);
        }

        public void RefreshFrame(string frameName)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("parent.window.frames['").Append(frameName).Append("'].location=").Append("parent.window.frames['").Append(frameName).Append("'].location;");
            this.RegisterScript(builder.ToString(), RegisterType.ClientBlock);
        }

        public void RefreshFrame(string frameName, string frameNavigateUrl)
        {
            frameNavigateUrl = this.AddExtendQueryID(frameNavigateUrl);
            frameNavigateUrl = this.EncryptModuleQuery(frameNavigateUrl);
            StringBuilder builder = new StringBuilder();
            builder.Append("parent.window.frames['").Append(frameName).Append("'].location='").Append(frameNavigateUrl).Append("'");
            this.RegisterScript(builder.ToString(), RegisterType.ClientBlock);
        }

        public void RefreshFrame(string frameName, string message, string navigateUrl)
        {
            navigateUrl = this.AddExtendQueryID(navigateUrl);
            navigateUrl = this.EncryptModuleQuery(navigateUrl);
            StringBuilder builder = new StringBuilder();
            builder.Append("parent.window.frames['").Append(frameName).Append("'].location=").Append("parent.window.frames['").Append(frameName).Append("'].location;");
            builder.Append("alert(\"").Append(message).Append("\");");
            builder.Append("location.href='").Append(navigateUrl).Append("'");
            this.RegisterScript(builder.ToString(), RegisterType.ClientBlock);
        }

        public void RefreshFrame(string frameName, string frameNavigateUrl, string message, string navigateUrl)
        {
            frameNavigateUrl = this.AddExtendQueryID(frameNavigateUrl);
            frameNavigateUrl = this.EncryptModuleQuery(frameNavigateUrl);
            navigateUrl = this.AddExtendQueryID(navigateUrl);
            navigateUrl = this.EncryptModuleQuery(navigateUrl);
            StringBuilder builder = new StringBuilder();
            builder.Append("parent.window.frames['").Append(frameName).Append("'].location='").Append(frameNavigateUrl).Append("';");
            builder.Append(" alert(\"").Append(message).Append("\");");
            builder.Append("location.href='").Append(navigateUrl).Append("';");
            this.RegisterScript(builder.ToString(), RegisterType.ClientBlock);
        }

        public void RegisterScript(string script, RegisterType registerTypeValue)
        {
            if (registerTypeValue == RegisterType.StartupScript)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), "script", script, true);
            }
            else
            {
                base.ClientScript.RegisterClientScriptBlock(base.GetType(), "script", script, true);
            }
        }

        protected void SavePageStateToSession()
        {
            object obj2 = base.LoadPageStateFromPersistenceMedium();
            Pair pair = new Pair {
                First = obj2,
                Second = base.DeterminePostBackMode()
            };
            this.Session[this.PageStateCode] = pair;
            this.Session[this.PageStateCode + "PageIndex"] = this.PageIndex;
            this.Session[this.PageStateCode + "ClickSortExpression"] = this.ClickSortExpression;
        }

        public void TabEnableAll()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("window.parent.TabEnableAll();");
            this.RegisterScript(builder.ToString(), RegisterType.StartupScript);
        }

        public void TabNext()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("window.parent.TabNext();");
            this.RegisterScript(builder.ToString(), RegisterType.StartupScript);
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

        protected virtual string ClickSortExpression
        {
            get
            {
                return this.ViewState.GetString("_ClickSortExpression", "");
            }
            set
            {
                this.ViewState["_ClickSortExpression"] = value;
            }
        }

        public virtual string CoteID
        {
            get
            {
                return this.GetString("CoteID", true);
            }
        }

        public virtual int CoteIsParent
        {
            get
            {
                return this.GetInt("CoteIsParent");
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
                return this.GetString("CoteModuleID", true);
            }
        }

        private int DefaultPageSize
        {
            get
            {
                return ModuleSectionHelper.GetModule(this.ModuleTypeCode).Page.PageSize;
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

        public virtual bool IsAutoPageSize
        {
            get
            {
                return ModuleSectionHelper.GetModule(this.ModuleTypeCode).Page.IsAutoPageSize;
            }
        }

        public virtual bool IsCacheDataPower
        {
            get
            {
                return ModuleSectionHelper.GetModule(this.ModuleTypeCode).Power.IsCacheDataPower;
            }
        }

        public virtual bool IsCacheLog
        {
            get
            {
                return ModuleSectionHelper.GetModule(this.ModuleTypeCode).IsCacheLog;
            }
        }

        public virtual bool IsCacheOperatePower
        {
            get
            {
                return ModuleSectionHelper.GetModule(this.ModuleTypeCode).Power.IsCacheOperatePower;
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

        public virtual bool IsPowerDataCheck
        {
            get
            {
                return ModuleSectionHelper.GetModule(this.ModuleTypeCode).Power.IsPowerDataCheck;
            }
        }

        public virtual bool IsRelease
        {
            get
            {
                return ConfigHelper.IsRelease;
            }
        }

        public virtual bool IsRolePowerManage
        {
            get
            {
                return ModuleSectionHelper.GetModule(this.ModuleTypeCode).Power.IsRolePowerManage;
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

        public string ModuleQuery
        {
            get
            {
                string str = "";
                if (this.PowerID.IsNoNull())
                {
                    str = str + "&PowerID=" + this.PowerID;
                }
                if (this.PowerName.IsNoNull())
                {
                    str = str + "&PowerName=" + this.PowerName;
                }
                if (this.TransferID.IsNoNull())
                {
                    str = str + "&TransferID=" + this.TransferID;
                }
                if (this.TransferTwoID.IsNoNull())
                {
                    str = str + "&TransferTwoID=" + this.TransferTwoID;
                }
                if (this.TransferThreeID.IsNoNull())
                {
                    str = str + "&TransferThreeID=" + this.TransferThreeID;
                }
                if (this.TransferFourID.IsNoNull())
                {
                    str = str + "&TransferFourID=" + this.TransferFourID;
                }
                if (this.CoteID.IsNoNull())
                {
                    str = str + "&CoteID=" + this.CoteID;
                }
                if (this.CoteKeyID.IsNoNull())
                {
                    str = str + "&CoteKeyID=" + this.CoteKeyID;
                }
                if (this.CoteModuleID.IsNoNull())
                {
                    str = str + "&CoteModuleID=" + this.CoteModuleID;
                }
                if (this.CoteIsParent.IsNoNull())
                {
                    str = str + "&CoteIsParent=" + this.CoteIsParent;
                }
                return str.Trim(new char[] { '&' });
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

        public virtual WTF.Framework.OperateStyle OperateStyle
        {
            get
            {
                return ModuleSectionHelper.GetModule(this.ModuleTypeCode).ModuleStyle.OperateStyle;
            }
        }

        public virtual int PageIndex
        {
            get
            {
                try
                {
                    if (this._PageIndex != -1)
                    {
                        this.ViewState["PageIndex"] = this._PageIndex;
                        return this._PageIndex;
                    }
                    if ((this.ViewState["PageIndex"] != null) && (((int) this.ViewState["PageIndex"]) != -1))
                    {
                        return (int) this.ViewState["PageIndex"];
                    }
                    if ((base.Request["PageIndex"] != null) && (base.Request["PageIndex"].Length != 0))
                    {
                        return int.Parse(base.Request["PageIndex"]);
                    }
                    return 0;
                }
                catch
                {
                    return 0;
                }
            }
            set
            {
                this._PageIndex = value;
            }
        }

        public virtual int PageSize
        {
            get
            {
                if (this._PageSize != -1)
                {
                    this.ViewState["PageSize"] = this._PageSize;
                    return this._PageSize;
                }
                if ((this.ViewState["PageSize"] != null) && (((int) this.ViewState["PageSize"]) != -1))
                {
                    return (int) this.ViewState["PageSize"];
                }
                if (this.IsAutoPageSize)
                {
                    string AutoPageSize = CookieHelper.GetCookieValue("AutoPageSize");
                    if (!string.IsNullOrEmpty(AutoPageSize))
                    {
                        return int.Parse(AutoPageSize) - ((int)SearchRow);

                    }
                }
                return this.DefaultPageSize;
            }
            set
            {
                this._PageSize = value;
            }
        }

        private string PageStateCode
        {
            get
            {
                return this.Page.ToString().Replace("ASP.", "").Replace("_aspx", "");
            }
        }

        public virtual string PowerID
        {
            get
            {
                return this.GetString("PowerID", true);
            }
        }

        public virtual string PowerName
        {
            get
            {
                return this.GetString("PowerName", true);
            }
        }

        public virtual string PowerPageCode
        {
            get
            {
                if (string.IsNullOrEmpty(this._PowerPageCode))
                {
                    this._PowerPageCode = this.Page.ToString().Replace("ASP.", "").Replace("_aspx", "");
                }
                return this._PowerPageCode;
            }
        }

        public string RawUrl
        {
            get
            {
                return base.Request.RawUrl;
            }
        }

        public virtual WTF.Framework.SearchRow SearchRow
        {
            get
            {
                return WTF.Framework.SearchRow.One;
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

        public virtual string TransferFourID
        {
            get
            {
                if (!((this._TransferFourID != null) && string.IsNullOrWhiteSpace(this._TransferFourID)))
                {
                    return this._TransferFourID;
                }
                return this.GetString("TransferFourID", true);
            }
            set
            {
                this._TransferFourID = value;
            }
        }

        public virtual string TransferID
        {
            get
            {
                if (!((this._TransferID != null) && string.IsNullOrWhiteSpace(this._TransferID)))
                {
                    return this._TransferID;
                }
                return this.GetString("TransferID", true);
            }
            set
            {
                this._TransferID = value;
            }
        }

        public virtual string TransferThreeID
        {
            get
            {
                if (!((this._TransferThreeID != null) && string.IsNullOrWhiteSpace(this._TransferThreeID)))
                {
                    return this._TransferThreeID;
                }
                return this.GetString("TransferThreeID", true);
            }
            set
            {
                this._TransferThreeID = value;
            }
        }

        public virtual string TransferTwoID
        {
            get
            {
                if (!((this._TransferTwoID != null) && string.IsNullOrWhiteSpace(this._TransferTwoID)))
                {
                    return this._TransferTwoID;
                }
                return this.GetString("TransferTwoID", true);
            }
            set
            {
                this._TransferTwoID = value;
            }
        }

        public string UrlPath
        {
            get
            {
                return base.Request.Url.LocalPath;
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

