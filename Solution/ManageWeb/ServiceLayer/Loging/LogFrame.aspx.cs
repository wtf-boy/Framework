using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class ServiceLayer_Loging_LogFrame : SupportPageBase
{

    public override string PowerPageCode
    {
        get
        {
            return "ServiceLayer_Loging_LogList";
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }
}
