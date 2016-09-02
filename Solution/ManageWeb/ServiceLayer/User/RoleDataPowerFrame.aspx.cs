using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WTF.Framework;
public partial class ServiceLayer_User_RoleDataPowerFrame : SupportPageBase
{

    public override string PowerPageCode
    {
        get
        {
            return "ServiceLayer_User_RoleDataPower";
        }
    }
    public string RoleID
    {
        get
        {
            
            return RequestHelper.GetString("RoleID");

        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }
}