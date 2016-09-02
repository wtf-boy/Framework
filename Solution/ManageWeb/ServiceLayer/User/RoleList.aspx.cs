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
using WTF.Framework;
using WTF.Power;
using WTF.Power.Entity;
using WTF.Pages;
using System.Collections.Generic;
public partial class ServiceLayer_User_RoleList : SupportPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    public UserRule objUserRule = new UserRule();
    public override string SortExpression
    {
        get
        {
            return "it.IsSystem  desc , it.AuthorizeGroupID,it.RoleName";
        }
    }

    public override string Condition
    {
        get
        {

            return "it.ModuleTypeID='" + CoteID + "'";
        }
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


    public override void InitDataPage()
    {

        ModuleRule objModuleRule = new ModuleRule();

        AuthorizeGroupID.Items.Clear();
        AuthorizeGroupID.Items.Add(new ListItem("--全部--", ""));
        AuthorizeGroupID.Items.Add(new ListItem("平台虚拟授权组", Guid.Empty.ToString()));
        foreach (sys_authorizegroup objsys_authorizegroup in objUserRule.sys_authorizegroup.Where(s => s.ModuleTypeID == CoteID))
        {
            AuthorizeGroupID.Items.Add(new ListItem(objsys_authorizegroup.AuthorizeGroupName, objsys_authorizegroup.AuthorizeGroupID));
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
                objUserRule.DeleteRole(e.CommandArgument.ToString());
                RenderPage();
                break;
            case "Modify":
                RedirectState("RoleEdit.aspx?RoleID=" + e.CommandArgument.ToString());
                break;
            case "RoleUser":
                Redirect("RoleUser.aspx?RoleID=" + e.CommandArgument.ToString());
                break;
            case "Power":
                Redirect("RolePower.aspx?RoleID=" + e.CommandArgument.ToString());
                break;
            case "DataAllSet":
                if (!objUserRule.Sys_RolePower.Any(s => s.RoleID == e.CommandArgument))
                {
                    MessageDialog("请先设置操作权限，才能设置数据权限");
                    return;
                }
                Redirect("RoleDataPowerFrame.aspx?RoleID=" + e.CommandArgument.ToString());
                break;
            case "CotePower":

                if (!objUserRule.Sys_RolePower.Any(s => s.RoleID == e.CommandArgument))
                {
                    MessageDialog("请先设置操作权限，才能设置栏目权限");
                    return;
                }
                Redirect("RoleCotePowerFrame.aspx?RoleID=" + e.CommandArgument.ToString());
                break;



        }

    }
}
