using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WTF.Work;
using WTF.Work.Entity;

public partial class ServiceLayer_Work_PlanRun : SupportPageBase
{


    public int WorkInfoID
    {
        get
        {
            return GetInt("WorkInfoID");

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


    /// <summary>
    /// 页面加载
    /// </summary>
    public override void RenderPage()
    {

        txtRunDateTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

    }


    /// <summary>
    /// 保存信息
    /// </summary>
    public void SaveInfo()
    {
        WorkRule objWorkRule = new WorkRule();
        Work_PlanRun objWork_PlanRun = new Work_PlanRun();
        objWork_PlanRun.WorkInfoID = WorkInfoID;
        objWork_PlanRun.PlanID = PlanID;
        objWork_PlanRun.RunDate = txtRunDateTime.TextDateTime;
        objWork_PlanRun.IsRun = false;
        objWorkRule.InsertPlanRun(objWork_PlanRun);
        MessageDialog("设置成功", "PlanList.aspx?WorkInfoID=" + WorkInfoID);

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
                Redirect("PlanList.aspx?WorkInfoID=" + WorkInfoID);
                break;
        }

    }


}