using System;
using System.Collections;
using System.Configuration;
using System.Collections.Generic;
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
public partial class ServiceLayer_User_RolePower : SupportPageBase
{

    public string RoleID
    {
        get
        {
            return GetString("RoleID");
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
            Sys_Role objSys_Role = objUserRule.Sys_Role.FirstOrDefault(s => s.RoleID == RoleID);
            litRoleName.Text = objSys_Role.RoleName;
            if (objSys_Role.AuthorizeGroupID == Guid.Empty.ToString())
            {
                XmlDataSource.Data = objPowerRule.GetPowerTreexXmlText(objSys_Role.ModuleTypeID, CurrentUser.IsSuper);
            }
            else
            {
                sys_authorizegroup objsys_authorizegroup = objUserRule.sys_authorizegroup.First(s => s.AuthorizeGroupID == objSys_Role.AuthorizeGroupID);
                if (objsys_authorizegroup.IsSupertGroup)
                {
                    XmlDataSource.Data = objPowerRule.GetPowerTreexXmlText(objsys_authorizegroup.ModuleTypeID, false);
                }
                else
                {
                    XmlDataSource.Data = objPowerRule.GetPowerTreexXmlText(objsys_authorizegroup.ModuleTypeID, objsys_authorizegroup.AuthorizeGroupID);
                }
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
