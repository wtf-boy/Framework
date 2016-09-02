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
using WTF.Pages;
using WTF.Power;
using WTF.Power.Entity;
public partial class ServiceLayer_User_SuperUserList : SupportPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {


    }
    public override string SortExpression
    {
        get
        {
            return "it.IsSuper desc ,it.IsAdmin desc ,it.UserTypeCID";
        }
    }


    public override string Condition
    {
        get
        {
            return "it.UserTypeCID=" + CoteID;
        }
    }

    public UserRule objUserRule = new UserRule();
    public override void RenderPage()
    {

        this.CurrentBindData<Sys_User>(gdvContent, objUserRule.Sys_User.Where("it.Account!='" + CurrentUser.Account + "'" + (!CurrentUser.IsSuper ? "and it.IsSuper==False" : "")));
    }
    protected override void CurrentTool_ItemCommand(object sender, WTF.Controls.MyCommandEventArgs e)
    {

        switch (e.CommandName)
        {
            case "Create":
                Redirect("SupportUserEdit.aspx");
                break;
            case "Remove":
                objUserRule.DeleteUser(gdvContent.SelectedRowDataKeys);
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

            case "UserRole":
                RedirectState("UserRole.aspx?UserID=" + e.CommandArgument.ToString());
                break;
            case "ResetPassword":
                RedirectState("UserPasswordEdit.aspx?UserID=" + e.CommandArgument.ToString());
                break;
            case "ViewPower":
                RedirectState("UserPowerInfo.aspx?UserID=" + e.CommandArgument.ToString());
                break;
            case "Modify":
                RedirectState("SupportUserEdit.aspx?UserID=" + e.CommandArgument.ToString());
                break;
            case "Power":
                Redirect("UserRolePower.aspx?UserID=" + e.CommandArgument.ToString());
                break;
            case "Remove":
                objUserRule.DeleteUser(e.CommandArgument.ToString());
                RenderPage();
                break;
            case "RevertUserPower":
                objUserRule.RevertUserPower(e.CommandArgument.ToString(), "");
                MessageDialog("收回授权成功");
                break;
            case "Lock":
                objUserRule.SetUserLock(e.CommandArgument.ToString(), true);
                RenderPage();
                break;
            case "NoLock":
                objUserRule.SetUserLock(e.CommandArgument.ToString(), false);
                RenderPage();
                break;
            case "NoActivation":
                objUserRule.SetUserActivation(e.CommandArgument.ToString(), false);
                RenderPage();
                break;
            case "Activation":
                objUserRule.SetUserActivation(e.CommandArgument.ToString(), true);
                RenderPage();

                break;
        }

    }
}
