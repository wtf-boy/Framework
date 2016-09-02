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
using WTF.Power.Entity;
public partial class ServiceLayer_User_RoleEdit : SupportPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }


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

        dropAuthorizeGroupID.Items.Add(new ListItem("--请选择--", ""));
        dropAuthorizeGroupID.Items.Add(new ListItem("平台虚拟授权组", Guid.Empty.ToString()));
        foreach (sys_authorizegroup objsys_authorizegroup in objUserRule.sys_authorizegroup.Where(s => s.ModuleTypeID == CoteID))
        {
            dropAuthorizeGroupID.Items.Add(new ListItem(objsys_authorizegroup.AuthorizeGroupName, objsys_authorizegroup.AuthorizeGroupID));
        }

    }
    public override void RenderPage()
    {

        if (RoleID.IsNoNull())
        {
            objRole = objUserRule.Sys_Role.First(p => p.RoleID == RoleID);
            if (CheckEditObjectIsNull(objRole)) return;
            litRoleID.Text = " " + objRole.RoleID;

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
        else
        {

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
            objRole.ModuleTypeID = CoteID;
            objRole.RoleGroupID = "";
            objRole.AccountTypeID = CurrentUser.AccountTypeID;
            objRole.AuthorizeGroupID = dropAuthorizeGroupID.SelectedValue;
            objRole.IsSystem = objRole.AuthorizeGroupID == Guid.Empty.ToString();
            objRole.IsUserRole = false;
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
            objRole.IsSystem = objRole.AuthorizeGroupID == Guid.Empty.ToString();
            objUserRule.SaveChanges();
            if (chkRoleUser.Items.Count > 0)
            {
                objUserRule.UpdateRoleUser(RoleID, chkRoleUser.SelectValueString, chkRoleUser.SelectNoValueString);
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
                Redirect("../../ServiceLayer/User/RoleList.aspx");
                break;
        }

    }


}
