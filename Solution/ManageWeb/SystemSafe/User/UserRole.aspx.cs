using WTF.Power;
using WTF.Power.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SystemSafe_User_UserRole : SupportPageBase
{
    public UserRule objUserRule = new UserRule();


    public string UserID
    {
        get
        {

            return GetString("UserID");

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
            return "it.AuthorizeGroupID ,it.RoleName";
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