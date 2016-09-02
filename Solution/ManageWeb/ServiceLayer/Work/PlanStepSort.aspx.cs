using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WTF.Work;

public partial class ServiceLayer_Work_PlanStepSort : SupportPageBase
{
    WorkRule objWorkRule = new WorkRule();
    public override void RenderPage()
    {

        lboxSort.DataSource = objWorkRule.Work_PlanStep.Where(s => s.PlanID == PlanID).OrderBy(p => p.SortIndex);
        lboxSort.DataTextField = "StepName";
        lboxSort.DataValueField = "PlanStepID";
        lboxSort.DataBind();


    }

    /// <summary>
    /// 获取计划标识
    /// </summary>
    public int PlanID
    {
        get
        {
            return GetInt("PlanID");

        }

    }

    protected void Page_Load(object sender, EventArgs e)
    {

        Response.Cache.SetCacheability(HttpCacheability.NoCache);

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {

        if (hidSortID.Value.Length > 0)
        {
            objWorkRule.UpdatePlanStepSort(hidSortID.Value);

            DialogOpenerReloadScript(true, "保存成功");

        }
        else
        {
            MessageDialog("排序未改变无须保存");
        }

    }
}