using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WTF.Framework;
using WTF.Work.Entity;
using WTF.Work;

public partial class ServiceLayer_Work_WorkInfoEdit : SupportPageBase
{
    /// <summary>
    /// 获取作业标识
    /// </summary>
    public int WorkInfoID
    {
        get
        {
            return GetInt("WorkInfoID");

        }

    }
    /// <summary>
    /// 变量
    /// </summary>
    public Work_WorkInfo objWork_WorkInfo = new Work_WorkInfo();
    WorkRule objWorkRule = new WorkRule();
    /// <summary>
    /// 页面加载
    /// </summary>
    public override void RenderPage()
    {

        if (WorkInfoID.IsNoNull())
        {
            objWork_WorkInfo = objWorkRule.Work_WorkInfo.First(s => s.WorkInfoID == WorkInfoID);

            Page.DataBind();
        }


    }
    /// <summary>
    /// 保存信息
    /// </summary>
    public void SaveInfo()
    {
        if (WorkInfoID.IsNull())
        {
            //作业名称
            objWork_WorkInfo.WorkInfoName = txtWorkInfoName.TextCutWord(50);
            ///作业说明
            objWork_WorkInfo.WorkInfoRemark = txtWorkInfoRemark.TextCutWord(500);
            ///是否启用
            objWork_WorkInfo.IsEnable = false;
            ///创建时间
            objWork_WorkInfo.CreateDate = DateTime.Now;
            ///修改时间
            objWork_WorkInfo.ModifyDate = DateTime.Now;
          
            objWork_WorkInfo.RunIP = txtRunIP.Text;
            ///上次执行时间
            objWork_WorkInfo.LastProcessDate = DateTime.Now;
            objWorkRule.InsertWorkInfo(objWork_WorkInfo);
            MessageDialog("新增成功", "WorkInfoList.aspx");
        }
        else
        {
            objWork_WorkInfo = objWorkRule.Work_WorkInfo.First(p => p.WorkInfoID == WorkInfoID);
            ///作业名称
            objWork_WorkInfo.WorkInfoName = txtWorkInfoName.TextCutWord(50);
            ///作业说明
            objWork_WorkInfo.WorkInfoRemark = txtWorkInfoRemark.TextCutWord(500);
            //运行IP
            objWork_WorkInfo.RunIP = txtRunIP.Text;
            ///修改时间
            objWork_WorkInfo.ModifyDate = DateTime.Now;

            objWorkRule.UpdateWorkInfo(objWork_WorkInfo);
            MessageDialog("修改成功", "WorkInfoList.aspx");
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
                Redirect("WorkInfoList.aspx");
                break;
        }

    }

    protected void btnsave_Click(object sender, EventArgs e)
    {

    }
}