namespace WTF.Controls
{
    using WTF.Framework;
    using System;
    using System.ComponentModel;
    using System.Web.UI.WebControls;

    public class MyHyperLink : HyperLink
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if (this.Command.IsNoNull())
            {
                this.Enabled = ((ModulePage) this.Page).GetPowerOperateButton(this.ModuleTypeID, this.ModuleCode, this.Command);
            }
        }

        public string Command
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

        [Category("Seven：操作按钮"), Description("模块代码"), Browsable(true)]
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

