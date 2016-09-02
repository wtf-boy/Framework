namespace WTF.Pages
{
    using WTF.Controls;
    using WTF.Framework;
    using WTF.Logging;
    using WTF.Power;
    using System;
    using System.Data.Common;
    using System.Data.Objects;
    using System.Data.Objects.DataClasses;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public class UserControlBase : ModuleUserControl
    {
        private Logger _Logger = new Logger("Application");
        private int _PageIndex = -1;
        private int _PageSize = -1;
        private UserRule _UserRule = null;

        public void CloseForm()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("window.parent.closeSubMainPages();");
            this.RegisterScript(builder.ToString(), RegisterType.StartupScript);
        }

        public void CurrentBindData(DataBoundControl objDataBoundControl, ObjectQuery<DbDataRecord> objObjectQuery)
        {
            int recordCount = 0;
            objDataBoundControl.DataSource = objObjectQuery.GetPage<DbDataRecord>(this.Condition, this.SortExpression, this.PageSize, this.PageIndex, out recordCount);
            this.RecordCount = recordCount;
            objDataBoundControl.DataBind();
        }

        public void CurrentBindData<T>(DataBoundControl objDataBoundControl, ObjectQuery<T> objObjectQuery) where T : EntityObject
        {
            int recordCount = 0;
            objDataBoundControl.DataSource = objObjectQuery.GetPage<T>(this.Condition, this.SortExpression, this.PageSize, this.PageIndex, out recordCount);
            this.RecordCount = recordCount;
            objDataBoundControl.DataBind();
        }

        public void CurrentBindData(Repeater objRepeater, ObjectQuery<DbDataRecord> objObjectQuery)
        {
            int recordCount = 0;
            objRepeater.DataSource = objObjectQuery.GetPage<DbDataRecord>(this.Condition, this.SortExpression, this.PageSize, this.PageIndex, out recordCount);
            this.RecordCount = recordCount;
            objRepeater.DataBind();
        }

        public void CurrentBindData<T>(Repeater objRepeater, ObjectQuery<T> objObjectQuery) where T : EntityObject
        {
            int recordCount = 0;
            objRepeater.DataSource = objObjectQuery.GetPage<T>(this.Condition, this.SortExpression, this.PageSize, this.PageIndex, out recordCount);
            this.RecordCount = recordCount;
            objRepeater.DataBind();
        }

        public virtual void CurrentPager_PagerChangeCommand(object sender, PagerEventArgs e)
        {
            if (e.PagerChangeType == PagerChangeType.PageIndex)
            {
                this.PageIndex = e.ChangeValue;
                this.RenderPage();
            }
            else
            {
                this.PageIndex = 0;
                this.PageSize = e.ChangeValue;
                this.RenderPage();
            }
        }

        public void DialogOpenerReloadScript(bool IsClosed)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("dialogArguments.location.href = dialogArguments.location.href;").Append("\r\n");
            if (IsClosed)
            {
                builder.Append("window.close();").Append("\r\n");
            }
            this.RegisterScript(builder.ToString(), RegisterType.ClientBlock);
        }

        public void DialogOpenerReloadScript(string theUrl, bool IsClosed)
        {
            theUrl = this.EncryptModuleQuery(theUrl);
            StringBuilder builder = new StringBuilder();
            builder.Append(" window.dialogArguments.location.href = '" + theUrl + "';").Append("\r\n");
            if (IsClosed)
            {
                builder.Append("window.close();").Append("\r\n");
            }
            this.RegisterScript(builder.ToString(), RegisterType.ClientBlock);
        }

        public virtual void LogExpDispose(Exception objExp)
        {
            LogModuleInfo logModuleInfo = this.GetLogModuleInfo();
            if (!logModuleInfo.IsDispose)
            {
                throw objExp;
            }
            LogHelper.DisposeException(logModuleInfo.ModuleTypeCode, objExp);
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
            navigateUrl = this.EncryptModuleQuery(navigateUrl);
            StringBuilder builder = new StringBuilder();
            builder.Append("alert(\"").Append(message).Append("\");");
            builder.Append("location.href='").Append(navigateUrl).Append("'");
            this.RegisterScript(builder.ToString(), RegisterType.ClientBlock);
        }

        protected override void OnLoad(EventArgs e)
        {
            if (!base.IsPostBack)
            {
                this.RenderPage();
            }
            base.OnLoad(e);
        }

        public void OpenerReloadScript(string theUrl, bool IsClosed)
        {
            theUrl = this.EncryptModuleQuery(theUrl);
            StringBuilder builder = new StringBuilder();
            builder.Append("opener.location.href = '" + theUrl + "';").Append("\r\n");
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
            pUrl = this.EncryptModuleQuery(pUrl);
            StringBuilder builder = new StringBuilder();
            builder.Append("var iNewX = (screen.availWidth - " + pWidth.ToString() + ")/2;").Append("\r\n");
            builder.Append("var iNewY = (screen.availHeight - " + pHeight.ToString() + ")/2;").Append("\r\n");
            builder.Append("var theDes = \"dialogWidth:" + pWidth.ToString() + "px;dialogHeight:" + pHeight.ToString() + "px;edge:sunken;help:no;status:no;scroll:no;\"").Append("\r\n");
            builder.Append("var ReturnValue = window.showModelessDialog('" + pUrl + "',window,theDes);").Append("\r\n");
            this.RegisterScript(builder.ToString(), RegisterType.ClientBlock);
        }

        public void Redirect(string url)
        {
            this.Redirect(url, this.AutoEncrypt);
        }

        public void Redirect(string url, bool isEncrypt)
        {
            if (isEncrypt)
            {
                base.Response.Redirect(this.EncryptModuleQuery(url), true);
            }
            else
            {
                base.Response.Redirect(url.EncodeUrlQuery(), true);
            }
        }

        public void RefreshFrame(string frameName)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("parent.window.frames['").Append(frameName).Append("'].location=").Append("parent.window.frames['").Append(frameName).Append("'].location;");
            this.RegisterScript(builder.ToString(), RegisterType.ClientBlock);
        }

        public void RefreshFrame(string frameName, string frameNavigateUrl)
        {
            frameNavigateUrl = this.EncryptModuleQuery(frameNavigateUrl);
            StringBuilder builder = new StringBuilder();
            builder.Append("parent.window.frames['").Append(frameName).Append("'].location='").Append(frameNavigateUrl).Append("'");
            this.RegisterScript(builder.ToString(), RegisterType.ClientBlock);
        }

        public void RefreshFrame(string frameName, string message, string navigateUrl)
        {
            navigateUrl = this.EncryptModuleQuery(navigateUrl);
            StringBuilder builder = new StringBuilder();
            builder.Append("parent.window.frames['").Append(frameName).Append("'].location=").Append("parent.window.frames['").Append(frameName).Append("'].location;");
            builder.Append("alert(\"").Append(message).Append("\");");
            builder.Append("location.href='").Append(navigateUrl).Append("'");
            this.RegisterScript(builder.ToString(), RegisterType.ClientBlock);
        }

        public void RefreshFrame(string frameName, string frameNavigateUrl, string message, string navigateUrl)
        {
            frameNavigateUrl = this.EncryptModuleQuery(frameNavigateUrl);
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
                ScriptManager.RegisterStartupScript((Control)SysVariable.CurrentHandler, SysVariable.CurrentHandler.GetType(), "script", script, true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock((Control)SysVariable.CurrentHandler, SysVariable.CurrentHandler.GetType(), "script", script, true);
            }
        }

        public virtual void RenderPage()
        {
        }

        public virtual void SearchCondition()
        {
            this.PageIndex = 0;
            this.RenderPage();
        }

        public virtual string Condition
        {
            get
            {
                return "";
            }
        }

        public UserInfo CurrentUser
        {
            get
            {
                return this.CurrentUserRule.CurrentUser;
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

        public Logger Log
        {
            get
            {
                return this._Logger;
            }
        }

        public int PageCount
        {
            get
            {
                if ((this.RecordCount % this.PageSize) > 0)
                {
                    return ((this.RecordCount / this.PageSize) + 1);
                }
                return (this.RecordCount / this.PageSize);
            }
        }

        public int PageIndex
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
                    if ((this.ViewState["PageIndex"] != null) && (((int)this.ViewState["PageIndex"]) != -1))
                    {
                        return (int)this.ViewState["PageIndex"];
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

        public int PageSize
        {
            get
            {
                try
                {
                    if (this._PageSize != -1)
                    {
                        this.ViewState["PageSize"] = this._PageSize;
                        return this._PageSize;
                    }
                    if ((this.ViewState["PageSize"] != null) && (((int)this.ViewState["PageSize"]) != -1))
                    {
                        return (int)this.ViewState["PageSize"];
                    }
                    if ((base.Request["PageSize"] != null) && (base.Request["PageSize"].Length != 0))
                    {
                        return int.Parse(base.Request["PageSize"]);
                    }
                    return 20;
                }
                catch
                {
                    return 20;
                }
            }
            set
            {
                this._PageSize = value;
            }
        }

        public string RawUrl
        {
            get
            {
                return base.Request.RawUrl;
            }
        }

        public int RecordCount
        {
            get
            {
                int num = this.ViewState["_RecordCount"].ConvertInt();
                return ((num == 0) ? 0 : num);
            }
            set
            {
                this.ViewState["_RecordCount"] = value;
            }
        }

        public virtual string SortExpression
        {
            get
            {
                return "";
            }
        }

        public string UrlPath
        {
            get
            {
                return base.Request.Url.LocalPath;
            }
        }
    }
}

