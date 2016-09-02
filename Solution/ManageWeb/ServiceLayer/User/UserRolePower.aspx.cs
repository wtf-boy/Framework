using WTF.Power;
using WTF.Power.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WTF.Framework;
public partial class ServiceLayer_User_UserRolePower : SupportPageBase
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
            return Guid.Empty.ToString();
        }
    }
    public PowerRule objPowerRule = new PowerRule();
    public UserRule objUserRule = new UserRule();
    public override void InitDataPage()
    {


        foreach (Sys_ModuleType objSys_ModuleType in objUserRule.GetUserModuleType(UserID))
        {
            dropModuleTypeID.Items.Add(new ListItem(objSys_ModuleType.ModuleTypeName, objSys_ModuleType.ModuleTypeID));
        }
    }


    public override void RenderPage()
    {

        try
        {

            if (dropModuleTypeID.Items.Count == 0)
            {
                MessageDialog("没有此用户类型绑定的模块类型", "SuperUserList.aspx");
                return;
            }

            hidPowerModuleTypeID.Value = dropModuleTypeID.SelectedValue;
            litUserName.Text = objUserRule.Sys_User.FirstOrDefault(s => s.UserID == UserID).UserName;
            XmlDataSource.Data = objPowerRule.GetPowerTreexXmlText(dropModuleTypeID.SelectedValue, CurrentUser.IsSuper);
            if (UserID.IsNoNull())
            {
                // 取得角色权限
                tvwPower.SetSelectValue(objUserRule.GetUserKeyPower(UserID, dropModuleTypeID.SelectedValue, AuthorizeGroupID), false);
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

        MessageDialog("设置成功", "SuperUserList.aspx");

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
                Redirect("SuperUserList.aspx");
                break;
        }
    }
    protected void dropModuleTypeID_SelectedIndexChanged(object sender, EventArgs e)
    {
        RenderPage();
    }
}