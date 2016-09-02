using WTF.Power;
using WTF.Power.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WTF.Framework;
public partial class SystemSafe_Power_UserList : SupportPageBase
{
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
            string condition = "it.UserTypeCID in {" + UserTypeList.ConvertListToString() + "}  and it.IsSuper=false";
            if (objUserRule.sys_authorizegroup.Any(s => s.AuthorizeGroupID == AuthorizeGroupID && s.IsAllowPowerSelf == false))
            {
                condition += " and  it.UserID!='" + CurrentUser.UserID + "'";
            }
            return condition;
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

    public override void InitDataPage()
    {
        UserTypeCID.Items.Clear();
        UserTypeCID.Items.Add(new ListItem("--全部--", ""));
        foreach (Sys_UserType objSys_UserType in objUserRule.Sys_UserType.Where(s => UserTypeList.Contains(s.UserTypeID)))
        {

            UserTypeCID.Items.Add(new ListItem(objSys_UserType.UserTypeName, objSys_UserType.UserTypeID.ToString()));

        }
    }
    /// <summary>
    /// 页面初始化
    /// </summary>
    public override void RenderPage()
    {
        this.CurrentBindData<Sys_User>(gdvContent, objUserRule.Sys_User);
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
            case "ViewPower":
                RedirectState("UserPowerInfo.aspx?UserID=" + e.CommandArgument.ToString());
                break;
            case "Modify":
                RedirectState("UserEdit.aspx?UserID=" + e.CommandArgument.ToString());
                break;
            case "UserRole":
                RedirectState("UserRole.aspx?UserID=" + e.CommandArgument.ToString());
                break;
            case "RevertUserPower":
                objUserRule.RevertUserPower(e.CommandArgument.ToString(), AuthorizeGroupID);
                MessageDialog("收回授权成功");
                break;
            case "CopyPower":
                RedirectState("CopyPowerList.aspx?UserID=" + e.CommandArgument.ToString());
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
            case "SetPower":
                RedirectState("UserPowerSet.aspx?UserID=" + e.CommandArgument.ToString());
                break;

        }
    }
}