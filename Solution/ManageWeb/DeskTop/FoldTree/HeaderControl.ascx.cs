using WTF.Logging;
using WTF.Power;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DeskTop_FoldTree_HeaderControl : SupportUserControl
{



    public override void RenderPage()
    {
        litAccount.Text = CurrentUser.UserName + "[" + CurrentUser.Account + "]";
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