using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WTF.Framework;
using WTF.Power;
using WTF.Logging;
public partial class WorkFrame_Default_HeaderControl : SupportUserControl
{

    private ModuleRule CurrentModuleRule = new ModuleRule();

    public override void RenderPage()
    {
        if (CurrentUser.UserID != Guid.Empty.ToString())
        {
            var moduleTable = CurrentModuleRule.GetPowerFunctionModule(ModuleTypeID, "", false);
            rptModule.DataSource = moduleTable;
            rptModule.DataBind();
        }
    }

    protected void rptModule_ItemCommand(object source, RepeaterCommandEventArgs e)
    {

    }
    protected void lbtnOut_Click(object sender, EventArgs e)
    {
        try
        {
            UserRule objUser = new UserRule();
            objUser.SystemExitRule();
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "logout", "<script>window.location='../../Default.aspx'</script>");


        }
        catch (Exception objExp)
        {

            LogHelper.DisposeException(LogModuleType.SupportLog.ToString(), objExp);
        }
    }
}