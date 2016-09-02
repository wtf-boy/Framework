using WTF.Power;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WTF.Framework;
public partial class SystemSafe_User_UserPowerInfo : SupportPageBase
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
            XmlDataSource.Data = objPowerRule.GetPowerTreexXmlText(ModuleTypeID, CurrentUser.IsSuper);
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
                Redirect("UserList.aspx");
                break;
        }
    }
}