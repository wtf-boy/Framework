namespace WTF.Controls
{
    using WTF.Framework;
    using System;
    using System.Web.UI.WebControls;

    public class MenuLinkButton : LinkButton
    {
        public string CommandExpandArgument
        {
            get
            {
                return this.ViewState.GetString("CommandExpandArgument");
            }
            set
            {
                this.ViewState["CommandExpandArgument"] = value;
            }
        }
    }
}

