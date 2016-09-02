using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WTF.Work;
using WTF.Work.Entity;
using WTF.Framework;
public partial class ServiceLayer_Work_NotifyAddressEdit : SupportPageBase
{
    /// <summary>
    /// 获取通知地址标识
    /// </summary>
    public int NotifyAddressID
    {
        get
        {
            return GetInt("NotifyAddressID");

        }

    }
    /// <summary>
    /// 变量
    /// </summary>
    public Work_NotifyAddress objWork_NotifyAddress = new Work_NotifyAddress();
    WorkRule objWorkRule = new WorkRule();
    /// <summary>
    /// 页面加载
    /// </summary>
    public override void RenderPage()
    {

        if (NotifyAddressID.IsNoNull())
        {
            objWork_NotifyAddress = objWorkRule.Work_NotifyAddress.FirstOrDefault(s => s.NotifyAddressID == NotifyAddressID);
            if (CheckEditObjectIsNull(objWork_NotifyAddress)) return;
            ///地址类型1邮件2手机
            radAddressType.SelectedValue = objWork_NotifyAddress.AddressType.ToString();

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
        if (NotifyAddressID.IsNull())
        {
            ///地址
            objWork_NotifyAddress.Address = txtAddress.TextCut(500);
            ///地址名称
            objWork_NotifyAddress.AddressName = txtAddressName.TextCutWord(500);
            ///地址类型1邮件2手机
            objWork_NotifyAddress.AddressType = radAddressType.SelectValueInt;
            objWorkRule.InsertNotifyAddress(objWork_NotifyAddress);
            MessageDialog("新增成功", "NotifyAddressList.aspx");
        }
        else
        {
            objWork_NotifyAddress = objWorkRule.Work_NotifyAddress.FirstOrDefault(p => p.NotifyAddressID == NotifyAddressID);
            if (CheckEditObjectIsNull(objWork_NotifyAddress)) return;
            ///地址
            objWork_NotifyAddress.Address = txtAddress.TextCut(500);
            ///地址名称
            objWork_NotifyAddress.AddressName = txtAddressName.TextCutWord(500);
            ///地址类型1邮件2手机
            objWork_NotifyAddress.AddressType = radAddressType.SelectValueInt;
            objWorkRule.UpdateNotifyAddress(objWork_NotifyAddress);
            MessageDialog("修改成功", "NotifyAddressList.aspx");
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
                Redirect("NotifyAddressList.aspx");
                break;
        }

    }

}