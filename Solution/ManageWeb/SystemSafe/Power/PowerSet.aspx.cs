using WTF.Power;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WTF.Framework;
using WTF.Power.Entity;
public partial class SystemSafe_Power_PowerSet : SupportPageBase
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
    public PowerRule objPowerRule = new PowerRule();
    public UserRule objUserRule = new UserRule();

    public override void InitDataPage()
    {

    }
    public override void RenderPage()
    {

        try
        {
            UserRule objUserRule = new UserRule();
            litRoleName.Text = objUserRule.Sys_Role.FirstOrDefault(s => s.RoleID == RoleID).RoleName;
            sys_authorizegroup objsys_authorizegroup = objUserRule.sys_authorizegroup.First(s => s.AuthorizeGroupID == AuthorizeGroupID);
            if (objsys_authorizegroup.IsSupertGroup)
            {
                XmlDataSource.Data = objPowerRule.GetPowerTreexXmlText(objsys_authorizegroup.ModuleTypeID, CurrentUser.IsSuper);
            }
            else
            {
                XmlDataSource.Data = objPowerRule.GetPowerTreexXmlText(objsys_authorizegroup.ModuleTypeID, objsys_authorizegroup.AuthorizeGroupID);
            }
            if (RoleID.IsNoNull())
            {
                // 取得角色权限
                tvwPower.SetSelectValue(objUserRule.GetRoleKeyPower(RoleID), false);
            }
            tvwPower.DataBind();
        }
        catch
        {

        }

    }

    #region 保存信息
    /// <summary>
    /// 保存信息
    /// </summary>
    private void SaveInfo()
    {

        MessageDialog("设置成功", "RoleList.aspx");

    }
    #endregion


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