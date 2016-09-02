using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WTF.Power;
using WTF.Power.Entity;
using WTF.Framework;
public partial class ServiceLayer_User_UserTypeList : SupportPageBase
{
    public override string SortExpression
    {
        get
        {
            return "it.UserTypeID";
        }
    }
    UserRule objUserRule = new UserRule();
    public override void RenderPage()
    {
        this.CurrentBindData<Sys_UserType>(gdvContent, objUserRule.Sys_UserType);
    }
    protected override void CurrentTool_ItemCommand(object sender, WTF.Controls.MyCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "Create":
                Redirect("UserTypeEdit.aspx");
                break;


        }



    }
    protected override void CurrentContent_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "Modify":
                RedirectState("UserTypeEdit.aspx?UserTypeID=" + e.CommandArgument.ToString());
                break;
            case "Remove":
                objUserRule.DeleteUserTypeByKey(e.CommandArgument.ToString());
                RenderPage();
                break;

        }
    }

}