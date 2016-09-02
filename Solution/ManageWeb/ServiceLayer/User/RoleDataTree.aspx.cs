using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WTF.Framework;

public partial class ServiceLayer_User_RoleDataTree : SupportPageBase
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
            return GetString("RoleID");

        }
    }

    public override void RenderPage()
    {

        string moduleTypeID = CurrentUserRule.Sys_Role.Where(s => s.RoleID == RoleID).First().ModuleTypeID;

        XmlDataSource.Data = CurrentModuleRule.GetModeuleTypeDataTreexXmlText(moduleTypeID, RoleID);
        tvwModule.DataSource = XmlDataSource;
        tvwModule.DataBind();
    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }
}