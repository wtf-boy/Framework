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


public partial class ServiceLayer_Work_WorkProcessLogList : SupportPageBase
{
    public string WorkLogID
    {
        get
        {
            return GetString("WorkLogID");

        }

    }

    public int WorkInfoID
    {
        get
        {
            return GetInt("WorkInfoID");

        }

    }
    public override string Condition
    {
        get
        {
            return "it.WorkLogID='" + WorkLogID + "'";
        }
    }
    public override string SortExpression
    {


        get
        {
            return "it.CreateDate";
        }
    }
    WorkRule objWorkRule = new WorkRule();
    public override void RenderPage()
    {

        this.CurrentBindData<Work_WorkProcessLogInfo>(gdvContent, objWorkRule.Work_WorkProcessLogInfo);
    }
    protected override void CurrentTool_ItemCommand(object sender, WTF.Controls.MyCommandEventArgs e)
    {
        switch (e.CommandName)
        {

            case "Search":

                SearchCondition();
                break;

            case "Back":
                Redirect("WorkLogList.aspx?WorkInfoID=" + WorkInfoID);
                break;


        }



    }
    protected override void CurrentContent_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {

        }
    }

}