using WTF.Power;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WTF.Framework;
using WTF.Power.Entity;
public partial class ServiceLayer_Authorize_GroupPower : SupportPageBase
{

    /// <summary>
    /// 获取授权组标识
    /// </summary>
    public string AuthorizeGroupID
    {
        get
        {
            return GetString("AuthorizeGroupID");

        }

    }
    public PowerRule objPowerRule = new PowerRule();

    public UserRule objUserRule = new UserRule();



    public override void RenderPage()
    {

        try
        {

            UserRule objUserRule = new UserRule();
            sys_authorizegroup objsys_authorizegroup = objUserRule.sys_authorizegroup.First(s => s.AuthorizeGroupID == AuthorizeGroupID);
            litGroupName.Text = objsys_authorizegroup.AuthorizeGroupName;
            string moduleTypeID = objsys_authorizegroup.ModuleTypeID;
            XmlDataSource.Data = objPowerRule.GetPowerTreexXmlText(moduleTypeID, true);
            if (AuthorizeGroupID.IsNoNull())
            {
                // 取得角色权限
                tvwPower.SetSelectValue(objUserRule.GetAuthorizeGroupPower(AuthorizeGroupID), false);

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

        MessageDialog("设置成功", "GroupList.aspx");

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
                Redirect("GroupList.aspx");
                break;
        }
    }
}