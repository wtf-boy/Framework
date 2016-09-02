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
using WTF.Power.Entity;
public partial class ServiceLayer_User_SupportUserEdit : SupportPageBase
{


    public string UserID
    {
        get
        {
            return GetString("UserID");

        }

    }
    public int UserTypeCID
    {
        get
        {
            return GetInt("CoteID");

        }

    }


    public Sys_User objUser;
    public UserRule objUserRule = new UserRule();



    public override void RenderPage()
    {

        chkIsSuper.Enabled = CurrentUser.IsSuper;
        if (UserID.IsNoNull())
        {
            objUser = objUserRule.Sys_User.First(p => p.UserID == UserID);
            txtPassword.Enabled = false;
            txtPassword.CheckValueEmpty = false;
            Page.DataBind();
        }

        else
        {
            chkIsApprove.Checked = true;
        }
    }

    public void SaveInfo()
    {

        if (UserID.IsNull())
        {
            objUserRule.InsertUser(UserTypeCID, CurrentUser.AccountTypeID, chkIsSuper.Checked, txtNickName.Text, txtJobNo.Text, txtUserName.Text, txtAccount.Text, txtPassword.Text, txtEmail.Text, chkIsSuper.Checked ? false : chkIsAdmin.Checked);
            MessageDialog("新增成功", "../../ServiceLayer/User/SuperUserList.aspx");

        }
        else
        {
            objUserRule.UpdateSuppportUser(txtNickName.Text, txtJobNo.Text, txtUserName.Text, txtAccount.Text, chkIsApprove.Checked, txtEmail.Text, UserID);
            MessageDialog("修改成功", "../../ServiceLayer/User/SuperUserList.aspx");
        }

    }
    protected void Page_Load(object sender, EventArgs e)
    {

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
