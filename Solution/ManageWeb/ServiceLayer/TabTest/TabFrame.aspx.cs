using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ServiceLayer_TabTest_TabFrame : SupportPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        myTabBar.OnlyEnableCommand = "ModifyList";
    

    }
}