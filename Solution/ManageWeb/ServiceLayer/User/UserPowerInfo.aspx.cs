using WTF.Power;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WTF.Framework;
public partial class ServiceLayer_User_UserPowerInfo : SupportPageBase
{
    public string UserID
    {
        get
        {
            return GetString("UserID");
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

            List<string> ModuleTypeIDList = objUserRule.GetUserModuleTypeID(UserID);
            if (ModuleTypeIDList.Count() == 0)
            {
                MessageDialog("没有此用户类型绑定的模块类型", "SuperUserList.aspx");
                return;
            }
            else if (ModuleTypeIDList.Count() > 1)
            {
                MessageDialog("此用户有多个模块类型绑定请到具体授权组授权查看", "SuperUserList.aspx");
                return;
            }
            XmlDataSource.Data = objPowerRule.GetPowerTreexXmlText(ModuleTypeIDList.FirstOrDefault(), CurrentUser.IsSuper);
            if (UserID.IsNoNull())
            {
                // 取得角色权限
                tvwPower.SetSelectValue(objUserRule.GetUserAllKeyPower(UserID), false);
            }
            tvwPower.DataBind();
        }
        catch
        {
        }

    }




    protected override void CurrentTool_ItemCommand(object sender, WTF.Controls.MyCommandEventArgs e)
    {
        switch (e.CommandName)
        {

            case "Back":
                Redirect("SuperUserList.aspx");
                break;
        }
    }
}