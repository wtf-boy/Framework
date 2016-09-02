using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WTF.Framework;
using WTF.Power;
using WTF.Power.Entity;
public partial class SystemSafe_Role_RoleUser : SupportPageBase
{
    public string RoleID
    {
        get
        {
            return GetString("RoleID");
        }
    }
    public override string Condition
    {
        get
        {
            string condition = "it.UserTypeCID in {" + UserTypeList.ConvertListToString() + "}  and it.IsSuper=false ";
            string AuthorizeGroupID = objUserRule.Sys_Role.FirstOrDefault(s => s.RoleID == RoleID).AuthorizeGroupID;
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
            return "it.Account";
        }
    }
    UserRule objUserRule = new UserRule();

    public override void RenderPage()
    {
        gdvContent.AlreadySelectedRowKeys = objUserRule.GetRoleUser(RoleID);
        this.CurrentBindData<Sys_User>(gdvContent, objUserRule.Sys_User);
    }
    public void SaveInfo()
    {
        objUserRule.UpdateRoleUser(RoleID, gdvContent.SelectedRowDataKeys, gdvContent.SelectedNoRowDataKeys);
        RenderPage();
        MessageDialog("设置成功");
    }
    protected override void CurrentTool_ItemCommand(object sender, WTF.Controls.MyCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "Save":
                SaveInfo();
                break;
            case "Back":
                ResponseHelper.Redirect("RoleList.aspx");
                break;
            case "Search":
                SearchCondition();
                break;
        }

    }
}