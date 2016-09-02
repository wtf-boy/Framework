using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WTF.Framework;
using WTF.Power;
using WTF.Power.Entity;
public partial class SystemSafe_Role_RoleList : SupportPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    public UserRule objUserRule = new UserRule();
    public override string Condition
    {
        get
        {
            return "it.IsSystem=false and it.IsUserRole=false  and it.ModuleTypeID='" + ModuleTypeID.ToString() + "'";
        }
    }
    public override string SortExpression
    {
        get
        {
            return "it.CreateDate desc ,it.RoleName";
        }
    }

    public override void InitDataPage()
    {
        AuthorizeGroupID.Items.Clear();
        AuthorizeGroupID.Items.Add(new ListItem("--全部--", ""));
        foreach (sys_authorizegroup objsys_authorizegroup in objUserRule.sys_authorizegroup.Where(s => s.ModuleTypeID == ModuleTypeID))
        {
            AuthorizeGroupID.Items.Add(new ListItem(objsys_authorizegroup.AuthorizeGroupName, objsys_authorizegroup.AuthorizeGroupID));
        }
    }

    public override void RenderPage()
    {

        this.CurrentBindData<Sys_Role>(gdvContent, objUserRule.Sys_Role);
    }

    public string GetAuthorizeGroupName(string authorizeGroupID)
    {
        ListItem objListItem = AuthorizeGroupID.Items.FindByValue(authorizeGroupID);
        if (objListItem != null)
        {
            return objListItem.Text;
        }
        else
        {
            return "找不到此授权组";
        }
    }
    protected override void CurrentTool_ItemCommand(object sender, WTF.Controls.MyCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "Create":
                Redirect("RoleEdit.aspx");
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
            case "Power":
                RedirectState("RolePower.aspx?RoleID=" + e.CommandArgument.ToString());
                break;

        }

    }
}