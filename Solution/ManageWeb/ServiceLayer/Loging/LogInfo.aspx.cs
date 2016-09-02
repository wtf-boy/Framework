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
using System.Collections.Generic;
public partial class ServiceLayer_Loging_LogInfo : SupportPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public int LogID
    {
        get
        {
            return GetInt("LogID");
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


    public loger_loging objLog_LogingInfo = new loger_loging();
    public override void RenderPage()
    {
        objLog_LogingInfo = objLogRule.loger_loging.FirstOrDefault(p => p.LogID == LogID);

        if (objLog_LogingInfo.HeadersData != "[]" && objLog_LogingInfo.HeadersData.IsNoNull())
        {
            rptHeadersData.DataSource = objLog_LogingInfo.HeadersData.JsonJsDeserialize<List<LogDataInfo>>();
        }
        if (objLog_LogingInfo.RequestData != "[]" && objLog_LogingInfo.RequestData.IsNoNull())
        {
            rptRequestData.DataSource = objLog_LogingInfo.RequestData.JsonJsDeserialize<List<LogDataInfo>>();
        }

        Page.DataBind();
    }
    protected override void CurrentTool_ItemCommand(object sender, WTF.Controls.MyCommandEventArgs e)
    {

        switch (e.CommandName)
        {

            case "Back":
                Redirect("LogList.aspx?ApplicationID=" + ApplicationID.ToString());

                break;
        }
    }
}
