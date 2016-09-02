using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WTF.Work;
using WTF.Work.Entity;
using WTF.Framework;
public partial class ServiceLayer_Work_PlanStepEdit : SupportPageBase
{
    /// <summary>
    /// 获取计划作业标识
    /// </summary>
    public int PlanStepID
    {
        get
        {
            return GetInt("PlanStepID");

        }

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


    public int WorkInfoID
    {
        get
        {
            return GetInt("WorkInfoID");

        }

    }

    public override void InitDataPage()
    {

        dropProcessID.Items.Clear();
        dropProcessID.Items.Add(new ListItem("--请选择--", ""));
        foreach (Work_Process objWork_Process in objWorkRule.Work_Process.Where(s => s.WorkInfoID == WorkInfoID))
        {
            dropProcessID.Items.Add(new ListItem(objWork_Process.ProcessName, objWork_Process.ProcessID.ToString()));

        }
    }
    /// <summary>
    /// 变量
    /// </summary>
    public Work_PlanStep objWork_PlanStep = new Work_PlanStep();
    WorkRule objWorkRule = new WorkRule();
    /// <summary>
    /// 页面加载
    /// </summary>
    public override void RenderPage()
    {

        if (PlanStepID.IsNoNull())
        {
            objWork_PlanStep = objWorkRule.Work_PlanStep.FirstOrDefault(s => s.PlanStepID == PlanStepID);
            if (CheckEditObjectIsNull(objWork_PlanStep)) return;
            ///功能
            dropProcessID.SelectedValue = objWork_PlanStep.ProcessID.ToString();
            ///成功时要执行的操作
            radSucessProcessType.SelectedValue = objWork_PlanStep.SucessProcessType.ToString();
            ///失败时要执行的操作
            radFailProcessType.SelectedValue = objWork_PlanStep.FailProcessType.ToString();

            Page.DataBind();
        }
        else
        {
            txtRunCount.Text = "0";
            txtRunInterval.Text = "0";
        }

    }
    /// <summary>
    /// 保存信息
    /// </summary>
    public void SaveInfo()
    {
        if (PlanStepID.IsNull())
        {
            objWork_PlanStep.PlanID = PlanID;
            ///步骤名称
            objWork_PlanStep.StepName = txtStepName.TextCutWord(100);
            ///功能
            objWork_PlanStep.ProcessID = dropProcessID.SelectValueInt;
            ///重试次数
            objWork_PlanStep.RunCount = txtRunCount.TextInt;
            ///重试间隔
            objWork_PlanStep.RunInterval = txtRunInterval.TextInt;
            ///成功时要执行的操作
            objWork_PlanStep.SucessProcessType = radSucessProcessType.SelectValueInt;
            ///失败时要执行的操作
            objWork_PlanStep.FailProcessType = radFailProcessType.SelectValueInt;
            ///执行顺序
            objWork_PlanStep.SortIndex = 0;
            objWorkRule.InsertPlanStep(objWork_PlanStep);
            MessageDialog("新增成功", "PlanStepList.aspx?PlanID=" + PlanID + "&WorkInfoID=" + WorkInfoID);
        }
        else
        {
            objWork_PlanStep = objWorkRule.Work_PlanStep.FirstOrDefault(p => p.PlanStepID == PlanStepID);
            if (CheckEditObjectIsNull(objWork_PlanStep)) return;
            ///步骤名称
            objWork_PlanStep.StepName = txtStepName.TextCutWord(100);
            ///功能
            objWork_PlanStep.ProcessID = dropProcessID.SelectValueInt;
            ///重试次数
            objWork_PlanStep.RunCount = txtRunCount.TextInt;
            ///重试间隔
            objWork_PlanStep.RunInterval = txtRunInterval.TextInt;
            ///成功时要执行的操作
            objWork_PlanStep.SucessProcessType = radSucessProcessType.SelectValueInt;
            ///失败时要执行的操作
            objWork_PlanStep.FailProcessType = radFailProcessType.SelectValueInt;
            objWorkRule.UpdatePlanStep(objWork_PlanStep);
            MessageDialog("修改成功", "PlanStepList.aspx?PlanID=" + PlanID + "&WorkInfoID=" + WorkInfoID);
        }
    }

    /// <summary>
    /// 工具栏操作
    /// </summary>
    protected override void CurrentTool_ItemCommand(object sender, WTF.Controls.MyCommandEventArgs e)
    {

        switch (e.CommandName)
        {
            case "Save":
                SaveInfo();
                break;
            case "Back":
                Redirect("PlanStepList.aspx?PlanID=" + PlanID + "&WorkInfoID=" + WorkInfoID);
                break;
        }

    }

}