using WTF.Power;
using WTF.Power.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SystemSafe_Power_PowerInfo :SupportPageBaseSql
{
    public string AuthorizeGroupID
    {
        get
        {
            return GetString("CoteID");
        }
    }
    public PowerRule objPowerRule = new PowerRule();
    public UserRule objUserRule = new UserRule();

    public override void InitDataPage()
    {

    }
    public override void RenderPage()
    {

        try
        {
            UserRule objUserRule = new UserRule();

            sys_authorizegroup objsys_authorizegroup = objUserRule.sys_authorizegroup.First(s => s.AuthorizeGroupID == AuthorizeGroupID);
            if (objsys_authorizegroup.IsSupertGroup)
            {
                XmlDataSource.Data = objPowerRule.GetPowerTreexXmlText(objsys_authorizegroup.ModuleTypeID, CurrentUser.IsSuper);
            }
            else
            {
                XmlDataSource.Data = objPowerRule.GetPowerTreexXmlText(objsys_authorizegroup.ModuleTypeID, objsys_authorizegroup.AuthorizeGroupID);
            }

            tvwPower.DataBind();
        }
        catch
        {
        }

    }




    protected override void CurrentTool_ItemCommand(object sender, WTF.Controls.MyCommandEventArgs e)
    {
        switch (e.CommandName)
        {

            case "Back":
                Redirect("RoleList.aspx");
                break;
        }
    }
}