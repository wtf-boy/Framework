using WTF.Logging;
using WTF.Logging.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WTF.Framework;
public partial class ServiceLayer_Loging_OperatorHistoryInfo : SupportPageBase
{
    public override PowerType CheckPowerType
    {
        get
        {
            return PowerType.FramePower;
        }
    }

    public string HistoryID
    {
        get
        {
            return GetString("HistoryID");
        }
    }
    /// <summary>
    /// 获取操作历史标识
    /// </summary>
    public int OperationHistoryID
    {
        get
        {
            return GetInt("OperationHistoryID");

        }

    }
    /// <summary>
    /// 变量
    /// </summary>
    public loger_operationhistory objloger_operationhistory = new loger_operationhistory();
    LogRule objLogRule = new LogRule();
    /// <summary>
    /// 页面加载
    /// </summary>
    public override void RenderPage()
    {

        if (OperationHistoryID.IsNoNull())
        {
            objloger_operationhistory = objLogRule.loger_operationhistory.FirstOrDefault(s => s.OperationHistoryID == OperationHistoryID);
            if (CheckEditObjectIsNull(objloger_operationhistory)) return;

            Page.DataBind();
        }
        else
        {
        }

    }


    /// <summary>
    /// 工具栏操作
    /// </summary>
    protected override void CurrentTool_ItemCommand(object sender, WTF.Controls.MyCommandEventArgs e)
    {

        switch (e.CommandName)
        {
            case "Back":
                Redirect("OperatorHistoryList.aspx?HistoryID=" + HistoryID);
                break;
        }

    }

}