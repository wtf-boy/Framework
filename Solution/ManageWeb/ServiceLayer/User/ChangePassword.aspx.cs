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
using WTF.Power;

public partial class ServiceLayer_User_ChangePassword : SupportPageBase
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
        objUserRule.ReSetPassword(CurrentUser.UserID, txtPassword.Text);
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
