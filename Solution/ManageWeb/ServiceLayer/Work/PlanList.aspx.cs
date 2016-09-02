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
public partial class ServiceLayer_Work_PlanList : SupportPageBase
{
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
            return "it.WorkInfoID=" + WorkInfoID;
        }
    }
    public override string SortExpression
    {
        get
        {
            return "it.PlanName";
        }
    }
    WorkRule objWorkRule = new WorkRule();
    public override void RenderPage()
    {

        this.CurrentBindData<Work_Plan>(gdvContent, objWorkRule.Work_Plan);
    }
    protected override void CurrentTool_ItemCommand(object sender, WTF.Controls.MyCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "Create":
                Redirect("PlanEdit.aspx?WorkInfoID=" + WorkInfoID);
                break;
            case "Back":
                Redirect("WorkInfoList.aspx?WorkInfoID=" + WorkInfoID);
                break;
            case "Search":

                SearchCondition();
                break;


        }



    }
    protected override void CurrentContent_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "Modify":
                RedirectState("PlanEdit.aspx?WorkInfoID=" + WorkInfoID + "&PlanID=" + e.CommandArgument.ToString());
                break;
            case "PlanStep":
                RedirectState("PlanStepList.aspx?WorkInfoID=" + WorkInfoID + "&PlanID=" + e.CommandArgument.ToString());
                break;
            case "NotifyList":
                RedirectState("PlanNotifyList.aspx?WorkInfoID=" + WorkInfoID + "&PlanID=" + e.CommandArgument.ToString());
                break;

            case "Remove":
                objWorkRule.DeletePlanByKey(e.CommandArgument.ToString());
                RenderPage();
                break;
            case "StartPlan":
                objWorkRule.ChangePlanEnable(int.Parse(e.CommandArgument.ToString()), true);
                RenderPage();
                break;
            case "StopPlan":
                objWorkRule.ChangePlanEnable(int.Parse(e.CommandArgument.ToString()), false);
                RenderPage();
                break;
            case "DoPlan":
                RedirectState("PlanRun.aspx?WorkInfoID=" + WorkInfoID + "&PlanID=" + e.CommandArgument.ToString());
                break;
        }


    }


}