namespace WTF.Controls
{
    using System;
    using System.Web.UI.WebControls;

    public class BoundField : System.Web.UI.WebControls.BoundField
    {
        public bool IsAutoSort
        {
            get
            {
                return ((base.ViewState["IsAutoSort"] == null) || ((bool) base.ViewState["IsAutoSort"]));
            }
            set
            {
                base.ViewState["IsAutoSort"] = value;
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

