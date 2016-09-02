using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Linq;
using WTF.Power;
using WTF.Framework;
using WTF.Logging;
using WTF.Power.Entity;
using System.Data.Objects;
using System.Threading;
public partial class _Default : SupportPageBase
{
    public override bool IsPowerCheck
    {
        get
        {
            return false;
        }
    }

    public string PowerExit
    {
        get
        {
            return RequestHelper.GetString("PowerExit");
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        this.Title = SystemName + "_ºóÌ¨µÇÂ¼";
        try
        {
            if (!Page.IsPostBack)
            {

                UserRule objUserRule = new UserRule();
                if (PowerExit.IsNull())
                {
                    if (objUserRule.CurrentUser.IsSuper || objUserRule.CheckPowerFrame(ModuleTypeID, objUserRule.CurrentUser.UserID))
                    {
                        Redirect(SysVariable.ApplicationPath + "/DeskTop/" + CurrentLayoutPath(objUserRule.CurrentUser.UserID) + "/Default.aspx");
                    }
                }
                else
                {
                    objUserRule.SystemExitRule();
                }
            }
        }
        catch (Exception objExp)
        {
            LogHelper<LogModuleType>.DisposeException(LogModuleType.SupportLog, objExp);
        }
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        try
        {

            UserRule objUserRule = new UserRule();
            LoginInfo objLoginInfo = objUserRule.LoginRule(txtAccount.Text, txtPassword.Text, chkLogin.Checked, UserTypeList);
            Redirect(SysVariable.ApplicationPath + "/DeskTop/" + CurrentLayoutPath(objLoginInfo.UserID) + "/Default.aspx");
        }
        catch (Exception objExp)
        {

            LogHelper<LogModuleType>.DisposeException(LogModuleType.SupportLog, objExp);
        }

    }




}

