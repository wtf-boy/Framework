using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WTF.Power;
using WTF.Framework;
using WTF.Power.Entity;
public partial class SystemSafe_Power_UserEdit : SupportPageBase
{


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

    public Sys_User objUser;
    public UserRule objUserRule = new UserRule();

    public override void InitDataPage()
    {
        dropUserTypeCID.Items.Clear();

        foreach (Sys_UserType objSys_UserType in objUserRule.Sys_UserType.Where(s => UserTypeList.Contains(s.UserTypeID)))
        {
            dropUserTypeCID.Items.Add(new ListItem(objSys_UserType.UserTypeName, objSys_UserType.UserTypeID.ToString()));

        }
        List<Sys_Role> objSys_RoleList = objUserRule.Sys_Role.WhereCondition("it.AuthorizeGroupID='" + AuthorizeGroupID + "'  and it.IsSystem=false and it.IsUserRole=false").ToList();
        if (UserID.IsNoNull())
        {
            List<string> RoleIDList = objUserRule.Sys_RoleUser.Where(s => s.UserID == UserID).Select(s => s.RoleID).ToList();
            if (RoleIDList.Count > 0)
            {
                foreach (Sys_Role objSys_Role in objSys_RoleList.Where(s => RoleIDList.Contains(s.RoleID)).OrderByDescending(s => s.CreateDate))
                {
                    chkRoleList.Items.Add(new ListItem() { Text = objSys_Role.RoleName, Value = objSys_Role.RoleID, Selected = true });
                }
                foreach (Sys_Role objSys_Role in objSys_RoleList.Where(s => !RoleIDList.Contains(s.RoleID)).OrderByDescending(s => s.CreateDate))
                {
                    chkRoleList.Items.Add(new ListItem() { Text = objSys_Role.RoleName, Value = objSys_Role.RoleID });
                }
            }
            else
            {
                foreach (Sys_Role objSys_Role in objSys_RoleList)
                {
                    chkRoleList.Items.Add(new ListItem() { Text = objSys_Role.RoleName, Value = objSys_Role.RoleID });
                }
            }
        }
        else
        {
            foreach (Sys_Role objSys_Role in objSys_RoleList)
            {
                chkRoleList.Items.Add(new ListItem() { Text = objSys_Role.RoleName, Value = objSys_Role.RoleID });
            }
        }
    }
    public override void RenderPage()
    {



        if (UserID.IsNoNull())
        {
            trPassword.Visible = false;
            trtTruePassword.Visible = false;
            objUser = objUserRule.Sys_User.FirstOrDefault(p => p.UserID == UserID);
            chkRoleList.SetSelectValue(objUserRule.GetUserRole(UserID));
            txtPassword.Enabled = false;
            txtPassword.CheckValueEmpty = false;
            Page.DataBind();
        }
        else
        {
            trPassword.Visible = true;
            trtTruePassword.Visible = true;
        }

    }

    public void SaveInfo()
    {

        if (UserID.IsNull())
        {
            string Password = txtPassword.Text.IsNullOrWhiteSpace() ? "7777777" : txtPassword.Text;
            string userID = objUserRule.InsertUser(dropUserTypeCID.SelectValueInt, CurrentUser.AccountTypeID, false, txtNickName.Text, txtJobNo.Text, txtUserName.Text, txtAccount.Text, Password, txtEmail.Text);
            objUserRule.UpdateUserRole(userID, chkRoleList.SelectValueString, chkRoleList.SelectNoValueString);
            MessageDialog("新增成功", "UserList.aspx");

        }
        else
        {
            objUserRule.UpdateSuppportUser(txtNickName.Text, txtJobNo.Text, txtUserName.Text, txtAccount.Text, true, txtEmail.Text, UserID);
            objUserRule.UpdateUserRole(UserID, chkRoleList.SelectValueString, chkRoleList.SelectNoValueString);
            MessageDialog("修改成功", "UserList.aspx");
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
                Redirect("UserList.aspx");
                break;
        }

    }
}