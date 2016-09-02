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

public partial class ServiceLayer_Work_ProcessEdit : SupportPageBase
{
    /// <summary>
    /// 获取作业处理标识
    /// </summary>
    public int ProcessID
    {
        get
        {
            return GetInt("ProcessID");

        }

    }
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
    public Work_Process objWork_Process = new Work_Process();
    WorkRule objWorkRule = new WorkRule();
    /// <summary>
    /// 页面加载
    /// </summary>
    public override void RenderPage()
    {

        if (ProcessID.IsNoNull())
        {
            objWork_Process = objWorkRule.Work_Process.First(s => s.ProcessID == ProcessID);

            Page.DataBind();
        }
        else
        {
        }

    }
    /// <summary>
    /// 保存信息
    /// </summary>
    public void SaveInfo()
    {
        if (ProcessID.IsNull())
        {
            objWork_Process.WorkInfoID = WorkInfoID;
            ///处理名称
            objWork_Process.ProcessName = txtProcessName.TextCutWord(50);
            ///程序集
            objWork_Process.AssemblyName = txtAssemblyName.TextCut(500);
            ///类全名
            objWork_Process.TypeName = txtTypeName.TextCut(500);

            objWork_Process.ProcessConfig = txtProcessConfig.Text;
            ///创建时间
            objWork_Process.CreateDate = DateTime.Now;
            ///处理备注
            objWork_Process.ProcessRemark = txtProcessRemark.TextCutWord(500);
            objWorkRule.InsertProcess(objWork_Process);
            MessageDialog("新增成功", "ProcessList.aspx?WorkInfoID=" + WorkInfoID);
        }
        else
        {
            objWork_Process = objWorkRule.Work_Process.First(p => p.ProcessID == ProcessID);
            ///处理名称
            objWork_Process.ProcessName = txtProcessName.TextCutWord(50);
            ///程序集
            objWork_Process.AssemblyName = txtAssemblyName.TextCut(500);
            ///类全名
            objWork_Process.TypeName = txtTypeName.TextCut(500);

            objWork_Process.ProcessConfig = txtProcessConfig.Text;
            ///处理备注
            objWork_Process.ProcessRemark = txtProcessRemark.TextCutWord(500);
            objWorkRule.UpdateProcess(objWork_Process);
            MessageDialog("修改成功", "ProcessList.aspx?WorkInfoID=" + WorkInfoID);
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
                Redirect("ProcessList.aspx?WorkInfoID=" + WorkInfoID);
                break;
        }

    }

}