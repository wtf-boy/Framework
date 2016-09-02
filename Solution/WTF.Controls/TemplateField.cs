namespace WTF.Controls
{
    using System;
    using System.Web.UI.WebControls;

    public class TemplateField : System.Web.UI.WebControls.TemplateField
    {
        public string DataField
        {
            get
            {
                if (base.ViewState["DataField"] == null)
                {
                    return string.Empty;
                }
                return (string) base.ViewState["DataField"];
            }
            set
            {
                base.ViewState["DataField"] = value;
            }
        }

        public string StatField
        {
            get
            {
                if (base.ViewState["StatField"] == null)
                {
                    return "";
                }
                return (string) base.ViewState["StatField"];
            }
            set
            {
                base.ViewState["StatField"] = value;
            }
        }
    }
}

