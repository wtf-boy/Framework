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


public partial class ServiceLayer_Work_WorkLogClear : SupportPageBase
{
    /// <summary>
    /// 获取作业日志标识
    /// </summary>
    public string WorkLogID
    {
        get
        {
            return GetString("WorkLogID");

        }

    }
    public int WorkInfoReturnID
    {
        get
        {
            return GetInt("WorkInfoReturnID");

        }

    }

    /// <summary>
    /// 变量
    /// </summary>
    public Work_WorkLog objWork_WorkLog = new Work_WorkLog();
    WorkRule objWorkRule = new WorkRule();
    /// <summary>
    /// 页面加载
    /// </summary>
    public override void RenderPage()
    {


        //绑定接入商
        WorkInfoID.DataSource = objWorkRule.Work_WorkInfo;
        WorkInfoID.DataTextField = "WorkInfoName";
        WorkInfoID.DataValueField = "WorkInfoID";
        WorkInfoID.DataBind();
        WorkInfoID.Items.Insert(0, new ListItem("--全部--", ""));
        WorkInfoID.SelectedValue = WorkInfoReturnID.ToString();

    }
    /// <summary>
    /// 保存信息
    /// </summary>
    public void SaveInfo()
    {
        objWorkRule.Work_WorkLog.DeleteDataSql(QueryModel.ToConditionSql<Work_WorkLog>());
        MessageDialog("清理成功");
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
                Redirect("WorkLogList.aspx?WorkInfoID=" + WorkInfoReturnID);
                break;
        }

    }

}