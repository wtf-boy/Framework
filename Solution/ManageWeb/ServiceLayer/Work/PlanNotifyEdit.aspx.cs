using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WTF.Framework;
using WTF.Work;
using WTF.Work.Entity;
public partial class ServiceLayer_Work_PlanNotifyEdit : SupportPageBase
{
    /// <summary>
    /// 获取计划通知标识
    /// </summary>
    public int PlanNotifyID
    {
        get
        {
            return GetInt("PlanNotifyID");

        }

    }

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
    /// 变量
    /// </summary>
    public Work_PlanNotify objWork_PlanNotify = new Work_PlanNotify();
    WorkRule objWorkRule = new WorkRule();
    void LoadAddress(int NotifyType)
    {

        radNotifyAddressID.Items.Clear();
        foreach (Work_NotifyAddress objWork_NotifyAddress in objWorkRule.Work_NotifyAddress.Where(s => s.AddressType == NotifyType))
        {
            radNotifyAddressID.Items.Add(new ListItem(objWork_NotifyAddress.AddressName + "   " + objWork_NotifyAddress.Address, objWork_NotifyAddress.NotifyAddressID.ToString()));
        }
        if (radNotifyAddressID.Items.Count == 0)
        {
            radNotifyAddressID.Items.Add(new ListItem("对不起暂无此类型的通知地址，你先配置通知地址", ""));
        }
    }

    /// <summary>
    /// 页面加载
    /// </summary>
    public override void RenderPage()
    {

        if (PlanNotifyID.IsNoNull())
        {
            objWork_PlanNotify = objWorkRule.Work_PlanNotify.FirstOrDefault(s => s.PlanNotifyID == PlanNotifyID);
            if (CheckEditObjectIsNull(objWork_PlanNotify)) return;

            LoadAddress(objWork_PlanNotify.NotifyType);
            ///通知联系人
            radNotifyAddressID.SelectedValue = objWork_PlanNotify.NotifyAddressID.ToString();
            ///通知类型
            radPlanResult.SelectedValue = objWork_PlanNotify.PlanResult.ToString();
            ///通知方式
            radNotifyType.SelectedValue = objWork_PlanNotify.NotifyType.ToString();

            Page.DataBind();
        }
        else
        {
            LoadAddress(1);
        }

    }
    /// <summary>
    /// 保存信息
    /// </summary>
    public void SaveInfo()
    {

        if (radNotifyAddressID.SelectValueInt == 0)
        {
            MessageDialog("对不起选择通知联系人有误");
            return;
        }
        if (PlanNotifyID.IsNull())
        {
            objWork_PlanNotify.PlanID = PlanID;
            ///通知联系人
            objWork_PlanNotify.NotifyAddressID = radNotifyAddressID.SelectValueInt;
            ///通知类型
            objWork_PlanNotify.PlanResult = radPlanResult.SelectValueInt;
            ///通知方式
            objWork_PlanNotify.NotifyType = radNotifyType.SelectValueInt;
            objWorkRule.InsertPlanNotify(objWork_PlanNotify);
            MessageDialog("新增成功", "PlanNotifyList.aspx?PlanID=" + PlanID + "&WorkInfoID=" + WorkInfoID);
        }
        else
        {
            objWork_PlanNotify = objWorkRule.Work_PlanNotify.FirstOrDefault(p => p.PlanNotifyID == PlanNotifyID);
            if (CheckEditObjectIsNull(objWork_PlanNotify)) return;
            ///通知联系人
            objWork_PlanNotify.NotifyAddressID = radNotifyAddressID.SelectValueInt;
            ///通知类型
            objWork_PlanNotify.PlanResult = radPlanResult.SelectValueInt;
            ///通知方式
            objWork_PlanNotify.NotifyType = radNotifyType.SelectValueInt;
            objWorkRule.UpdatePlanNotify(objWork_PlanNotify);
            MessageDialog("修改成功", "PlanNotifyList.aspx?PlanID=" + PlanID + "&WorkInfoID=" + WorkInfoID);
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
                Redirect("PlanNotifyList.aspx?PlanID=" + PlanID + "&WorkInfoID=" + WorkInfoID);
                break;
        }

    }

    protected void radNotifyType_SelectedIndexChanged(object sender, EventArgs e)
    {

        LoadAddress(radNotifyType.SelectValueInt);
    }
}