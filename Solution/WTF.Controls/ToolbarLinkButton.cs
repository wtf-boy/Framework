namespace WTF.Controls
{
    using WTF.Framework;
    using System;
    using System.ComponentModel;
    using System.Web.UI.WebControls;

    public class ToolbarLinkButton : LinkButton
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if (this.UserValidationGroup.IsNoNull() && this.ClickScriptFun.IsNoNull())
            {
                this.OnClientClick = " function  " + base.CommandName + "out(){try{return " + this.ClickScriptFun + "}catch(e){ return true; }}; var " + base.CommandName + "outVal=" + base.CommandName + "out(); var  checkVal= $(this).ValidationValue('" + this.UserValidationGroup + "'); return " + base.CommandName + "outVal&&checkVal;";
            }
            else if (this.UserValidationGroup.IsNull() && this.ClickScriptFun.IsNoNull())
            {
                this.OnClientClick = "try{return " + this.ClickScriptFun + ";}catch(e){return true;}";
            }
            else if (this.UserValidationGroup.IsNoNull() && this.ClickScriptFun.IsNull())
            {
                this.OnClientClick = "return  $(this).ValidationValue('" + this.UserValidationGroup + "');";
            }
        }

        [Description("点击事件"), DefaultValue(""), Category("Seven：点击执行的脚本"), Browsable(true)]
        public string ClickScriptFun
        {
            get
            {
                if (this.ViewState["ClickScriptFun"] != null)
                {
                    return (string) this.ViewState["ClickScriptFun"];
                }
                return "";
            }
            set
            {
                this.ViewState["ClickScriptFun"] = value;
            }
        }

        [Category("Seven：验证功能"), Description("验证组"), DefaultValue(""), Browsable(true)]
        public string UserValidationGroup
        {
            get
            {
                if (this.ViewState["UserValidationGroup"] != null)
                {
                    return (string) this.ViewState["UserValidationGroup"];
                }
                return "";
            }
            set
            {
                this.ViewState["UserValidationGroup"] = value;
            }
        }
    }
}

