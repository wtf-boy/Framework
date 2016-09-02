using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WTF.Framework;
using WTF.Power;
using System.Xml.Linq;
public partial class SystemSafe_User_UserPasswordEdit : SupportPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    public UserRule objUserRule = new UserRule();

    public override void RenderPage()
    {
        txtAccount.Text = objUserRule.Sys_User.First(p => p.UserID == CurrentUser.UserID).Account;



    }

    public void SaveInfo()
    {
        objUserRule.ReSetPassword(CurrentUser.UserID, txtOldPassword.TextTrim, txtPassword.TextTrim);
        MessageDialog("修改成功");
    }


    protected override void CurrentTool_ItemCommand(object sender, WTF.Controls.MyCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "Save":
                SaveInfo();
                break;

        }
    }
}