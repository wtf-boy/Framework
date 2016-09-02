using WTF.Power;
using WTF.Power.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SystemSafe_Power_UserRole : SupportPageBase
{
    public UserRule objUserRule = new UserRule();


    public string UserID
    {
        get
        {

            return GetString("UserID");

        }

    }
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
            return "it.AuthorizeGroupID='" + AuthorizeGroupID + "' and   it.ModuleTypeID='" + ModuleTypeID.ToString() + "' and it.IsSystem=false and it.IsUserRole=false";
        }
    }
    public override string SortExpression
    {
        get
        {
            return "it.RoleName";
        }
    }
    public override void RenderPage()
    {
        gdvContent.AlreadySelectedRowKeys = objUserRule.GetUserRole(UserID);
        this.CurrentBindData<Sys_Role>(gdvContent, objUserRule.Sys_Role);


    }

    public void SaveInfo()
    {
        objUserRule.UpdateUserRole(UserID, gdvContent.SelectedRowDataKeys, gdvContent.SelectedNoRowDataKeys);

        RenderPage();
    }
    protected override void CurrentTool_ItemCommand(object sender, WTF.Controls.MyCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "Save":
                SaveInfo();
                MessageDialog("修改成功");
                break;
            case "Back":
                Redirect("UserList.aspx");
                break;
            case "Search":
                SearchCondition();
                break;
        }

    }
    protected override void CurrentContent_RowCommand(object sender, GridViewCommandEventArgs e)
    {


    }
}