using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WTF.Framework;
using WTF.Logging;
using WTF.Logging.Entity;
public partial class ServiceLayer_Loging_OperatorHistoryList : SupportPageBase
{

    public override int PageSize
    {
        get
        {
            return 15;
        }
    }
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
    public override string Condition
    {
        get
        {
            return "it.MenuPowerID='" + HistoryID + "'";
        }
    }
    public override string SortExpression
    {
        get
        {
            return "it.OperationHistoryID desc ";
        }
    }


    public override void InitDataPage()
    {


        OperationTypeID.Items.Clear();
        OperationTypeID.Items.Add(new ListItem("--全部--", ""));
        foreach (EnumParameter objEnumParameter in typeof(OperationType).GetEnumMembers())
        {
            OperationTypeID.Items.Add(new ListItem(objEnumParameter.Description, objEnumParameter.Value.ToString()));
        }
    }
    LogRule objLogRule = new LogRule();
    public override void RenderPage()
    {

        this.CurrentBindData<loger_operationhistory>(gdvContent, objLogRule.loger_operationhistory);
    }
    protected override void CurrentTool_ItemCommand(object sender, WTF.Controls.MyCommandEventArgs e)
    {
        switch (e.CommandName)
        {

            case "Search":

                SearchCondition();
                break;


        }


    }
    protected override void CurrentContent_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {

            case "Detail":
                RedirectState("OperatorHistoryInfo.aspx?HistoryID=" + HistoryID + "&OperationHistoryID=" + e.CommandArgument);
                break;

        }
    }

}