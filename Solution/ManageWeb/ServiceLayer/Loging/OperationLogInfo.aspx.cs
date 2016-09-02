using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using WTF.Framework;
using WTF.Logging;
using WTF.Logging.Entity;
public partial class ServiceLayer_Loging_OperationLogInfo : SupportPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public int OperationID
    {
        get
        {
            return GetInt("OperationID");
        }
    }


    /// <summary>
    /// 获取程序标识
    /// </summary>
    public int ApplicationID
    {
        get
        {
            return GetInt("ApplicationID");

        }

    }
    public LogRule objLogRule = new LogRule();


    public loger_operationloging objloger_operationloging = new loger_operationloging();
    public override void RenderPage()
    {
        objloger_operationloging = objLogRule.loger_operationloging.FirstOrDefault(p => p.OperationID == OperationID);
        Page.DataBind();
    }
    protected override void CurrentTool_ItemCommand(object sender, WTF.Controls.MyCommandEventArgs e)
    {

        switch (e.CommandName)
        {

            case "Back":
                Redirect("OperationLogList.aspx?ApplicationID=" + ApplicationID.ToString());

                break;
        }
    }
}
