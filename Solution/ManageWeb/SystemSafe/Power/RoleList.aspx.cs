using WTF.Power;
using WTF.Power.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SystemSafe_Power_RoleList : SupportPageBase
{
    public UserRule objUserRule = new UserRule();

    public string AuthorizeGroupID
    {
        get
        {
            return GetString("CoteID");
        }
    }
    public override string Condition
    {
        get
        {
            return "it.AuthorizeGroupID='" + AuthorizeGroupID + "'  and it.IsSystem=false and it.IsUserRole=false and it.AuthorizeGroupID!='00000000-0000-0000-0000-000000000000' ";
        }
    }
    public override string SortExpression
    {
        get
        {
            return "it.CreateDate desc ,it.RoleName";
        }
    }
    public override void RenderPage()
    {
        this.CurrentBindData<Sys_Role>(gdvContent, objUserRule.Sys_Role);
    }
    protected override void CurrentTool_ItemCommand(object sender, WTF.Controls.MyCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "Create":
                Redirect("RoleEdit.aspx");
                break;
            case "PowerInfo":
                RedirectState("PowerInfo.aspx");
                break;
            case "Remove":
                objUserRule.DeleteRole(gdvContent.SelectedRowDataKeys);
                RenderPage();
                break;
            case "Search":
                SearchCondition();
                break;
        }

    }
    protected override void CurrentContent_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "Remove":
                objUserRule.DeleteRole(gdvContent.SelectedRowDataKeys);
                RenderPage();
                break;
            case "Modify":
                RedirectState("RoleEdit.aspx?RoleID=" + e.CommandArgument.ToString());
                break;
            case "RoleUser":
                RedirectState("RoleUser.aspx?RoleID=" + e.CommandArgument.ToString());
                break;
            case "SetPower":
                RedirectState("PowerSet.aspx?RoleID=" + e.CommandArgument.ToString());
                break;
        }

    }
}