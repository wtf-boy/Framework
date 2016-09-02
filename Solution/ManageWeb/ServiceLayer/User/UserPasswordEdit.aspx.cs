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
using WTF.Power;
using WTF.Framework;
public partial class ServiceLayer_User_UserPasswordEdit : SupportPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    public string UserID
    {
        get
        {
            return GetString("UserID");
        }
    }

    public void SaveInfo()
    {
        UserRule objUser = new UserRule();
        objUser.ReSetPassword(UserID, txtPassword.Text);
        MessageDialog("修改成功", "../../ServiceLayer/User/SuperUserList.aspx");
        

    }

   protected override void CurrentTool_ItemCommand(object sender, WTF.Controls.MyCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "Save":
                SaveInfo();
                break;
            case "Back":
                Redirect("../../ServiceLayer/User/SuperUserList.aspx");
                break;
        }

    }
}
