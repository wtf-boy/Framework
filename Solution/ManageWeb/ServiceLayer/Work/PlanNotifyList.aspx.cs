using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WTF.Work;
using WTF.Work.Entity;

public partial class ServiceLayer_Work_PlanNotifyList : SupportPageBase
{
    public int PlanID
    {
        get
        {
            return GetInt("PlanID");

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
            return "it.PlanID=" + PlanID;
        }
    }
    public override string SortExpression
    {
        get
        {
            return "it.PlanNotifyID";
        }
    }
    WorkRule objWorkRule = new WorkRule();
    public override void RenderPage()
    {

        this.CurrentBindData<Work_PlanNotifyInfo>(gdvContent, objWorkRule.Work_PlanNotifyInfo);
    }
    protected override void CurrentTool_ItemCommand(object sender, WTF.Controls.MyCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "Create":
                Redirect("PlanNotifyEdit.aspx?PlanID=" + PlanID + "&WorkInfoID=" + WorkInfoID);
                break;
            case "Back":
                Redirect("PlanList.aspx?WorkInfoID=" + WorkInfoID);
                break;

        }

    }
    protected override void CurrentContent_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "Modify":
                RedirectState("PlanNotifyEdit.aspx?PlanID=" + PlanID + "&PlanNotifyID=" + e.CommandArgument.ToString() + "&WorkInfoID=" + WorkInfoID);
                break;
            case "Remove":
                objWorkRule.DeletePlanNotifyByKey(e.CommandArgument.ToString());
                RenderPage();
                break;

        }
    }

}