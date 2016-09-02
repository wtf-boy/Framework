using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WTF.Framework;
using WTF.Power;
using WTF.Power.Entity;
public partial class SystemSafe_Power_UserPowerSet : SupportPageBase
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
            litUserName.Text = objUserRule.Sys_User.FirstOrDefault(s => s.UserID == UserID).UserName;
            sys_authorizegroup objsys_authorizegroup = objUserRule.sys_authorizegroup.First(s => s.AuthorizeGroupID == AuthorizeGroupID);
            hidPowerModuleTypeID.Value = objsys_authorizegroup.ModuleTypeID;
            if (objsys_authorizegroup.IsSupertGroup)
            {
                XmlDataSource.Data = objPowerRule.GetPowerTreexXmlText(objsys_authorizegroup.ModuleTypeID, CurrentUser.IsSuper);
            }
            else
            {
                XmlDataSource.Data = objPowerRule.GetPowerTreexXmlText(objsys_authorizegroup.ModuleTypeID, objsys_authorizegroup.AuthorizeGroupID);
            }
            if (UserID.IsNoNull())
            {
                // 取得角色权限
                tvwPower.SetSelectValue(objUserRule.GetUserKeyPower(UserID, objsys_authorizegroup.ModuleTypeID, AuthorizeGroupID), false);
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

        MessageDialog("设置成功", "UserList.aspx");

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
                Redirect("UserList.aspx");
                break;
        }
    }
}