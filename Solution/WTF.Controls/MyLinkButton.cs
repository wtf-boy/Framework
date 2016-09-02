namespace WTF.Controls
{
    using WTF.Framework;
    using WTF.Logging;
    using System;
    using System.ComponentModel;
    using System.Threading;
    using System.Web.UI.WebControls;

    public class MyLinkButton : LinkButton
    {
        protected override void OnClick(EventArgs e)
        {
            try
            {
                base.OnClick(e);
            }
            catch (Exception exception)
            {
                Type type = exception.GetType();
                LogModuleInfo logModuleInfo = ((ModulePage) this.Page).GetLogModuleInfo();
                if (((type != typeof(ThreadAbortException)) && (type != typeof(InfoHintException))) && !(type == typeof(ArgumentInputNullException)))
                {
                    if (!logModuleInfo.IsDispose)
                    {
                        throw exception;
                    }
                    LogHelper.DisposeException(logModuleInfo.ModuleTypeCode, exception);
                }
                else
                {
                    LogHelper.DisposeException(logModuleInfo.ModuleTypeCode, exception);
                }
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if (this.Command.IsNoNull())
            {
                this.Enabled = ((ModulePage) this.Page).GetPowerOperateButton(this.ModuleTypeID, this.ModuleCode, this.Command);
            }
        }

        public new string Command
        {
            get
            {
                if (this.ViewState["Command"] != null)
                {
                    return (string) this.ViewState["Command"];
                }
                return (string) this.ViewState["Command"];
            }
            set
            {
                this.ViewState["Command"] = value;
            }
        }

        [Browsable(true), Category("Seven：操作按钮"), Description("模块代码")]
        public string ModuleCode
        {
            get
            {
                if ((this.ViewState["ModuleCode"] == null) || string.IsNullOrEmpty((string) this.ViewState["ModuleCode"]))
                {
                    this.ViewState["ModuleCode"] = ((ModulePage) this.Page).PowerPageCode;
                }
                return (string) this.ViewState["ModuleCode"];
            }
            set
            {
                this.ViewState["ModuleCode"] = value;
            }
        }

        public string ModuleTypeID
        {
            get
            {
                if (this.ViewState["ModuleTypeID"] == null)
                {
                    this.ViewState["ModuleTypeID"] = ((ModulePage) this.Page).ModuleTypeID;
                }
                return (string) this.ViewState["ModuleTypeID"];
            }
            set
            {
                this.ViewState["ModuleTypeID"] = value;
            }
        }
    }
}

