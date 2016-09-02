using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WTF.Framework;
using WTF.Power.Entity;
using WTF.Power;
public partial class SystemSafe_Role_RoleEdit : SupportPageBase
{

    public string RoleID
    {
        get
        {

            return GetString("RoleID");
        }
    }
    public UserRule objUserRule = new UserRule();
    public Sys_Role objRole = new Sys_Role();

    public override void InitDataPage()
    {
        dropAuthorizeGroupID.Items.Clear();
        foreach (sys_authorizegroup objsys_authorizegroup in objUserRule.sys_authorizegroup.Where(s => s.ModuleTypeID == ModuleTypeID))
        {
            dropAuthorizeGroupID.Items.Add(new ListItem(objsys_authorizegroup.AuthorizeGroupName, objsys_authorizegroup.AuthorizeGroupID.ToString()));
        }
    }
    public override void RenderPage()
    {


        if (RoleID.IsNoNull())
        {
            objRole = objUserRule.Sys_Role.First(p => p.RoleID == RoleID);
            if (CheckEditObjectIsNull(objRole)) return;
            dropAuthorizeGroupID.SelectedValue = objRole.AuthorizeGroupID;
            string userIDString = objUserRule.GetRoleUser(RoleID);
            if (!string.IsNullOrWhiteSpace(userIDString))
            {
                foreach (Sys_User objSys_User in objUserRule.Sys_User.WhereCondition("it.UserID in {" + userIDString.ConvertStringID() + "}").ToList())
                {
                    chkRoleUser.Items.Add(new ListItem() { Text = objSys_User.UserName, Value = objSys_User.UserID, Selected = true });
                }
            }
            dropAuthorizeGroupID.Enabled = false;
            Page.DataBind();
        }

    }

    public void SaveInfo()
    {

        if (RoleID.IsNull())
        {

            objRole.RoleID = Guid.NewGuid().ToString();
            objRole.UserID = CurrentUser.UserID;
            objRole.RoleName = txtRoleName.Text.Trim();
            objRole.RoleCode = objRole.RoleName.ConvertChineseSpell(false);
            objRole.Remark = txtRemark.Text;
            objRole.IsLockOut = false;
            objRole.ModuleTypeID = ModuleTypeID;
            objRole.RoleGroupID = "";
            objRole.IsSystem = false;
            objRole.AccountTypeID = CurrentUser.AccountTypeID;
            objRole.IsUserRole = false;
            objRole.AuthorizeGroupID = dropAuthorizeGroupID.SelectedValue;
            objRole.RefUserID = "";
            objUserRule.InsertRole(objRole);
            MessageDialog("新增成功", "RoleList.aspx");
        }
        else
        {

            objRole = objUserRule.Sys_Role.First(p => p.RoleID == RoleID);
            objRole.RoleName = txtRoleName.Text.Trim();
            objRole.RoleCode = objRole.RoleName.ConvertChineseSpell(false);
            objRole.Remark = txtRemark.Text;
            objRole.AuthorizeGroupID = dropAuthorizeGroupID.SelectedValue;
            objUserRule.SaveChanges();
            objUserRule.UpdateRoleUser(RoleID, chkRoleUser.SelectValueString, chkRoleUser.SelectNoValueString);
            MessageDialog("修改成功", "RoleList.aspx");
        }



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
        }

    }
}