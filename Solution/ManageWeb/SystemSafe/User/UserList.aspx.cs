using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WTF.Framework;
using WTF.Power;
using WTF.Power.Entity;
public partial class SystemSafe_User_UserList : SupportPageBase
{
    public override string Condition
    {
        get
        {

            return "it.UserTypeCID in {" + UserTypeList.ConvertListToString() + "}  and it.IsSuper=false and it.UserID!='" + CurrentUser.UserID + "' ";
        }
    }

    public override string SortExpression
    {
        get
        {
            return "it.Account ";
        }
    }

    UserRule objUserRule = new UserRule();
    List<Sys_Role> _Sys_RoleList = new List<Sys_Role>();

    public override void InitDataPage()
    {
        UserTypeCID.Items.Clear();
        UserTypeCID.Items.Add(new ListItem("--全部--", ""));
        foreach (Sys_UserType objSys_UserType in objUserRule.Sys_UserType.Where(s => UserTypeList.Contains(s.UserTypeID)))
        {

            UserTypeCID.Items.Add(new ListItem(objSys_UserType.UserTypeName, objSys_UserType.UserTypeID.ToString()));

        }
    }


    public string GetUserTypeName(int UserTypeID)
    {
        ListItem objListItem = UserTypeCID.Items.FindByValue(UserTypeID.ToString());
        if (objListItem != null)
        {
            return objListItem.Text;
        }
        return "未知类型";
    }
    /// <summary>
    /// 页面初始化
    /// </summary>
    public override void RenderPage()
    {

        this.CurrentBindData<Sys_User>(gdvContent, objUserRule.Sys_User);
    }

    /// <summary>
    /// 操作栏
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected override void CurrentTool_ItemCommand(object sender, WTF.Controls.MyCommandEventArgs e)
    {

        switch (e.CommandName)
        {
            case "Create":
                Redirect("UserEdit.aspx");
                break;
            case "Search":
                SearchCondition();
                break;

        }
    }


    /// <summary>
    /// 列表命令列表
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected override void CurrentContent_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        switch (e.CommandName)
        {
            case "Modify":
                RedirectState("UserEdit.aspx?UserID=" + e.CommandArgument.ToString());
                break;
            case "ViewPower":
                RedirectState("UserPowerInfo.aspx?UserID=" + e.CommandArgument.ToString());
                break;
            case "CopyPower":
                RedirectState("CopyPowerList.aspx?UserID=" + e.CommandArgument.ToString());
                break;
            case "RevertUserPower":
                string AuthorizeGroupIDList = objUserRule.sys_authorizegroup.Where(s => s.ModuleTypeID == ModuleTypeID).Select(s => s.AuthorizeGroupID).ConvertListToString();
                if (AuthorizeGroupIDList.IsNoNullOrWhiteSpace())
                {
                    objUserRule.RevertUserPower(e.CommandArgument.ToString(), AuthorizeGroupIDList);
                }
                MessageDialog("收回授权成功");
                break;
            case "UserRole":
                Redirect("UserRole.aspx?UserID=" + e.CommandArgument.ToString());
                break;
            case "Remove":
                objUserRule.DeleteUser(e.CommandArgument.ToString());
                MessageDialog("删除成功");
                RenderPage();
                break;
            case "RePassword":
                objUserRule.ReSetPassword(e.CommandArgument.ToString(), "7777777");
                MessageDialog("初始化密码成功，密码为7777777");
                break;
            case "Lock":
                objUserRule.SetUserLock(e.CommandArgument.ToString(), true);
                MessageDialog("禁用成功");
                RenderPage();
                break;
            case "NoLock":
                objUserRule.SetUserLock(e.CommandArgument.ToString(), false);
                MessageDialog("启用成功");
                RenderPage();
                break;

        }
    }
}