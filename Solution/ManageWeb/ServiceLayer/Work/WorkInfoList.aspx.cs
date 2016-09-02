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
using WTF.Controls;
public partial class ServiceLayer_Work_WorkInfoList : SupportPageBase
{
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

        this.CurrentBindData<Work_WorkInfo>(gdvContent, objWorkRule.Work_WorkInfo);
    }
    protected override void CurrentTool_ItemCommand(object sender, WTF.Controls.MyCommandEventArgs e)
    {
       
        switch (e.CommandName)
        {
            case "Create":
                Redirect("WorkInfoEdit.aspx");
                break;
            case "NextRun":
                Redirect("WorkNextList.aspx");
                break;
            case "NotifyAddress":
                Redirect("NotifyAddressList.aspx");
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
                
                RedirectState("WorkInfoEdit.aspx?WorkInfoID=" + e.CommandArgument.ToString());
                break;
            case "Remove":
                objWorkRule.DeleteWorkInfoByKey(e.CommandArgument.ToString());
                RenderPage();
                break;
            case "OnEnable":
                objWorkRule.ChangeWorkInfoEnable(e.CommandArgument.ToString(), true);
                RenderPage();
                break;
            case "UnEnable":
                objWorkRule.ChangeWorkInfoEnable(e.CommandArgument.ToString(), false);
                objWorkRule.DeletePlanRunByWorkInfoID(e.CommandArgument.ConvertInt());
                RenderPage();
                break;
            case "ShowProcess":
                RedirectState("ProcessList.aspx?WorkInfoID=" + e.CommandArgument.ToString());
                break;
            case "ShowPlan":
                RedirectState("PlanList.aspx?WorkInfoID=" + e.CommandArgument.ToString());
                break;
            case "ViewLog":
                RedirectState("WorkLogList.aspx?WorkInfoID=" + e.CommandArgument.ToString());
                break;
         




        }
    }



}