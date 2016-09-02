using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WTF.Framework;
using WTF.Power;
using WTF.Power.Entity;
public partial class ServiceLayer_User_UserRole : SupportPageBase
{
    public string UserID
    {
        get
        {
            return GetString("UserID");
        }
    }
    public UserRule objUserRule = new UserRule();
    public override string SortExpression
    {
        get
        {
            return "it.IsSystem desc , it.AuthorizeGroupID,it.RoleName";
        }
    }
    public override void InitDataPage()
    {
        AuthorizeGroupID.Items.Clear();
        AuthorizeGroupID.Items.Add(new ListItem("--全部--", ""));
        AuthorizeGroupID.Items.Add(new ListItem("平台虚拟授权组", Guid.Empty.ToString()));

        dropModuleTypeID.Items.Clear();
        dropModuleTypeID.Items.Add(new ListItem("--全部--", ""));
        List<Sys_ModuleType> ModuleTypeList = objUserRule.GetUserModuleType(UserID).ToList();

        foreach (Sys_ModuleType objSys_ModuleType in ModuleTypeList)
        {
            dropModuleTypeID.Items.Add(new ListItem(objSys_ModuleType.ModuleTypeName, objSys_ModuleType.ModuleTypeID));
        }
        List<string> ModuleTypeIDList = ModuleTypeList.Select(s => s.ModuleTypeID).ToList();
        foreach (sys_authorizegroup objsys_authorizegroup in objUserRule.sys_authorizegroup.Where(s => ModuleTypeIDList.Contains(s.ModuleTypeID)))
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
    public override void RenderPage()
    {
        gdvContent.AlreadySelectedRowKeys = objUserRule.GetUserRole(UserID);
        this.CurrentBindData<Sys_Role>(gdvContent, objUserRule.GetUserModuleTypeRole(UserID));
    }
    public void SaveInfo()
    {
        objUserRule.UpdateUserRole(UserID, gdvContent.SelectedRowDataKeys, gdvContent.SelectedNoRowDataKeys);
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
                Redirect("SuperUserList.aspx");
                break;
            case "Search":
                SearchCondition();
                break;
        }

    }
}