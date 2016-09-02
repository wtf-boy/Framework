using WTF.Power;
using WTF.Power.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WTF.Framework;
public partial class SystemSafe_Power_RoleEdit : SupportPageBase
{
    public string RoleID
    {
        get
        {

            return GetString("RoleID");
        }
    }

    public string AuthorizeGroupID
    {
        get
        {

            return GetString("CoteID");
        }
    }
    public UserRule objUserRule = new UserRule();
    public Sys_Role objRole = new Sys_Role();
    public override void RenderPage()
    {


        if (RoleID.IsNoNull())
        {
            objRole = objUserRule.Sys_Role.First(s => s.RoleID == RoleID && s.AuthorizeGroupID == AuthorizeGroupID);
            if (CheckEditObjectIsNull(objRole)) return;
            string userIDString = objUserRule.GetRoleUser(RoleID);
            if (!string.IsNullOrWhiteSpace(userIDString))
            {
                foreach (Sys_User objSys_User in objUserRule.Sys_User.WhereCondition("it.UserID in {" + userIDString.ConvertStringID() + "}").ToList())
                {
                    chkRoleUser.Items.Add(new ListItem() { Text = objSys_User.UserName, Value = objSys_User.UserID, Selected = true });
                }
            }
            Page.DataBind();
        }
    }

    public void SaveInfo()
    {
        sys_authorizegroup objsys_authorizegroup = objUserRule.sys_authorizegroup.FirstOrDefault(p => p.AuthorizeGroupID == AuthorizeGroupID);

        if (RoleID.IsNull())
        {
         

            if (objUserRule.Sys_Role.Any(s => s.ModuleTypeID == objsys_authorizegroup.ModuleTypeID && objRole.AuthorizeGroupID == AuthorizeGroupID && s.RoleName == txtRoleName.Text))
            {
                MessageDialog("对不起此角色名称已经存在");
                return;
            }
            objRole.RoleID = Guid.NewGuid().ToString();
            objRole.AuthorizeGroupID = AuthorizeGroupID;
            objRole.RefUserID = "";
            objRole.IsUserRole = false;
            objRole.UserID = CurrentUser.UserID;
            objRole.RoleName = txtRoleName.Text.Trim();
            objRole.RoleCode = objRole.RoleName.ConvertChineseSpell(false);
            objRole.Remark = txtRemark.Text;
            objRole.IsLockOut = false;
            objRole.ModuleTypeID = objsys_authorizegroup.ModuleTypeID;
            objRole.RoleGroupID = "";
            objRole.IsSystem = false;
            objRole.AccountTypeID = CurrentUser.AccountTypeID;
            objUserRule.InsertRole(objRole);
            MessageDialog("新增成功", "RoleList.aspx");
        }
        else
        {
            if (objUserRule.Sys_Role.Any(s => s.ModuleTypeID == objsys_authorizegroup.ModuleTypeID && s.RoleID != RoleID && objRole.AuthorizeGroupID == AuthorizeGroupID && s.RoleName == txtRoleName.Text))
            {
                MessageDialog("对不起此角色名称已经存在");
                return;
            }
            objRole = objUserRule.Sys_Role.First(p => p.RoleID == RoleID);
            objRole.RoleName = txtRoleName.Text.Trim();
            objRole.RoleCode = objRole.RoleName.ConvertChineseSpell(false);
            objRole.Remark = txtRemark.Text;
            objRole.IsLockOut = false;
            objUserRule.SaveChanges();
            if (chkRoleUser.Items.Count > 0)
            {
                objUserRule.AddRoleUser(RoleID, chkRoleUser.SelectValueString);
                objUserRule.RemoveRoleUser(RoleID, chkRoleUser.SelectNoValueString);
            }
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