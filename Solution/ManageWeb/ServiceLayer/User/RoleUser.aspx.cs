using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using WTF.Framework;

using WTF.Power;
using WTF.Power.Entity;
public partial class ServiceLayer_User_RoleUser : SupportPageBase
{
    public string RoleID
    {
        get
        {
            return GetString("RoleID");
        }
    }
    public UserRule objUserRule = new UserRule();

    public override string Condition
    {
        get
        {
            return "it.IsSuper == false";
        }
    }
    public override string SortExpression
    {
        get
        {
            return "it.Account";
        }
    }
    public override void RenderPage()
    {
        gdvContent.AlreadySelectedRowKeys = objUserRule.GetRoleUser(RoleID);
        ModuleRule objModuleRule = new ModuleRule();
        string userTypeID = objUserRule.GetRoleModuleTypeUserType(RoleID);
        if (string.IsNullOrWhiteSpace(userTypeID))
        {
            MessageDialog("此角色平台框架未设置用户类型", "RoleList.aspx");
            return;
        }
        this.CurrentBindData<Sys_User>(gdvContent, objUserRule.GetUserTypeUser(userTypeID));
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
                Redirect("RoleList.aspx");
                break;
            case "Search":
                SearchCondition();
                break;
        }

    }
}