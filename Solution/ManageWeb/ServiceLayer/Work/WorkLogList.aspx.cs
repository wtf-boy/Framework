using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WTF.Framework;
using WTF.Work.Entity;
using WTF.Work;
using WTF.Power;

public partial class ServiceLayer_Work_WorkLogList : SupportPageBase
{
    public int WorkInfoID
    {
        get
        {
            return GetInt("WorkInfoID");

        }

    }

    public override string SortExpression
    {
        get
        {
            return "it.CreateDate desc";
        }
    }

    public override void InitDataPage()
    {
        dropWorkInfoID.Items.Clear();
        dropWorkInfoID.Items.Add(new ListItem("--全部--", ""));
        foreach (Work_WorkInfo objWork_WorkInfo in objWorkRule.Work_WorkInfo)
        {
            dropWorkInfoID.Items.Add(new ListItem(objWork_WorkInfo.WorkInfoName, objWork_WorkInfo.WorkInfoID.ToString()));
        }
        dropWorkInfoID.SelectedValue = WorkInfoID.ToString();

    }
    WorkRule objWorkRule = new WorkRule();
    public override void RenderPage()
    {

        this.CurrentBindData<Work_WorkLogInfo>(gdvContent, objWorkRule.Work_WorkLogInfo);
    }
    protected override void CurrentTool_ItemCommand(object sender, WTF.Controls.MyCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "Back":
                Redirect("WorkInfoList.aspx");
                break;

            case "Search":

                SearchCondition();
                break;

            case "LogClear":

                RedirectState("WorkLogClear.aspx?WorkInfoReturnID=" + WorkInfoID);
                break;


        }



    }
    protected override void CurrentContent_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "Remove":
                objWorkRule.DeleteWorkLogByKey(e.CommandArgument.ToString());
                RenderPage();
                break;

            case "ShowProcessLog":

                RedirectState("WorkProcessLogList.aspx?WorkInfoID=" + WorkInfoID + "&WorkLogID=" + e.CommandArgument.ToString());
                break;

        }
    }

}