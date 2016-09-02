using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WTF.Framework;
using WTF.Power;
using WTF.Power.Entity;
public partial class SystemSafe_User_CopyPowerList : SupportPageBase
{
    public string UserID
    {
        get
        {
            return GetString("UserID");
        }
    }

    public override string Condition
    {
        get
        {
            return "it.UserTypeCID=" + CurrentUser.UserTypeCID + " and  it.IsSuper=false and it.UserID!='" + CurrentUser.UserID + "' and  it.UserID!='" + UserID + "'";

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

        this.CurrentBindData<Sys_User>(gdvContent, objUserRule.Sys_User);
    }
    public void SaveInfo()
    {
        if (string.IsNullOrWhiteSpace(gdvContent.SelectedRowFirstKey))
        {
            MessageDialog("请选择要复制的目标");
            return;
        }
        objUserRule.CopyUserPower(UserID, gdvContent.SelectedRowFirstKey, "");
        MessageDialog("复制成功", "UserList.aspx");
    }
    protected override void CurrentTool_ItemCommand(object sender, WTF.Controls.MyCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "Save":
                SaveInfo();

                break;
            case "Back":
                Redirect("UserList.aspx");
                break;
            case "Search":
                SearchCondition();
                break;
        }

    }
}