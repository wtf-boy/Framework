using WTF.Logging;
using WTF.Logging.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WTF.Framework;
public partial class ServiceLayer_Loging_ApplicationEdit : SupportPageBase
{
    /// <summary>
    /// 获取程序标识
    /// </summary>
    public int ApplicationID
    {
        get
        {
            return GetInt("ApplicationID");

        }

    }

    public int ParentID
    {
        get
        {
            return GetInt("ParentID");

        }

    }
    /// <summary>
    /// 变量
    /// </summary>
    public loger_application objloger_application = new loger_application();
    LogRule objLogRule = new LogRule();

    public override void InitDataPage()
    {
        dropIntervalMinutes.Items.Clear();
        for (int i = 2; i < 30; i++)
        {
            dropIntervalMinutes.Items.Add(new ListItem(i.ToString(), i.ToString()));

        }
        if (ApplicationID.IsNoNull())
        {
            foreach (loger_category objloger_category in objLogRule.loger_category.Where(s => s.ApplicationID == ApplicationID))
            {
                chkNoticeCategory.Items.Add(new ListItem(objloger_category.CategoryName, objloger_category.CategoryTypeCode));
            }
        }
        else
        {
            List<EnumParameter> objCategoryParameterList = EnumHelper.GetEnumMembers(typeof(LogCategory));
            foreach (EnumParameter objEnumParameter in objCategoryParameterList)
            {
                chkNoticeCategory.Items.Add(new ListItem(objEnumParameter.Description, objEnumParameter.Key));
            }
        }
    }
    /// <summary>
    /// 页面加载
    /// </summary>
    public override void RenderPage()
    {

        if (ApplicationID.IsNoNull())
        {
            objloger_application = objLogRule.loger_application.FirstOrDefault(s => s.ApplicationID == ApplicationID);
            if (CheckEditObjectIsNull(objloger_application)) return;
            ///是否自动释放
            chkIsDispose.Checked = objloger_application.IsDispose;
            chkIsNotice.Checked = objloger_application.IsNotice;
            dropIntervalMinutes.SetSelectValue(objloger_application.IntervalMinutes);
            chkNoticeCategory.SetSelectValue(objloger_application.NoticeCategory);
            Page.DataBind();
        }
        else
        {
            txtLogerCount.Text = "10";
            txtNoticeInterval.Text = "3";
            txtNoticeSleep.Text = "10";
            dropIntervalMinutes.SelectedValue = "5";
            txtMinutesMaxCount.Text = "30";
            chkNoticeCategory.SetSelectValue("ExceptionError");
        }

    }
    /// <summary>
    /// 保存信息
    /// </summary>
    public void SaveInfo()
    {
        if (ApplicationID.IsNull())
        {
            ///程序代码
            objloger_application.ApplicationCode = txtApplicationCode.TextCutWord(100);
            objloger_application.ParentID = ParentID;
            ///程序名称
            objloger_application.ApplicationName = txtApplicationName.TextCutWord(100);
            ///程序备注
            objloger_application.Remark = txtRemark.TextCutWord(200);
            objloger_application.SortIndex = txtSortIndex.TextInt;
            ///是否自动释放
            objloger_application.IsDispose = chkIsDispose.Checked;
            objloger_application.IsNotice = chkIsNotice.Checked;
            objloger_application.NoticePhone = txtNoticePhone.Text;
            objloger_application.NoticeEmail = txtNoticeEmail.Text;
            objloger_application.LogerCount = txtLogerCount.TextInt;
            objloger_application.NoticeInterval = txtNoticeInterval.TextInt;
            objloger_application.NoticeSleep = txtNoticeSleep.TextInt;
            objloger_application.NoticeCategory = chkNoticeCategory.SelectValueString;
            objloger_application.IntervalMinutes = dropIntervalMinutes.SelectValueInt;
            objloger_application.MinutesMaxCount = txtMinutesMaxCount.TextInt;
            objloger_application.HeaderKey = txtHeaderKey.TextTrim;
            objloger_application.RequestKey = txtRequestKey.TextTrim;
            if (objloger_application.IsNotice)
            {
                if (objloger_application.NoticeCategory.IsNull())
                {
                    MessageDialog("不好意思,选择日志通知必须选择通知日志类型");
                    return;
                }

                if (objloger_application.NoticeEmail.IsNull() && objloger_application.NoticePhone.IsNull())
                {
                    MessageDialog("不好意思,选择日志通知必须输入邮箱或手机号码其中一个");
                    return;
                }
            }
            objLogRule.InsertApplication(objloger_application);

            RefreshFrame("frmApplicationTree", "ApplicationTree.aspx?ApplicationID=" + objloger_application.ApplicationID.ToString(), "新增成功", "ApplicationInfo.aspx?ApplicationID=" + objloger_application.ApplicationID.ToString());
        }
        else
        {
            objloger_application = objLogRule.loger_application.FirstOrDefault(p => p.ApplicationID == ApplicationID);
            if (CheckEditObjectIsNull(objloger_application)) return;
            ///程序代码
            objloger_application.ApplicationCode = txtApplicationCode.TextCutWord(100);

            ///程序名称
            objloger_application.ApplicationName = txtApplicationName.TextCutWord(100);
            ///程序备注
            objloger_application.Remark = txtRemark.TextCutWord(200);
            objloger_application.SortIndex = txtSortIndex.TextInt;
            ///是否自动释放
            objloger_application.IsDispose = chkIsDispose.Checked;
            objloger_application.IsNotice = chkIsNotice.Checked;
            objloger_application.NoticePhone = txtNoticePhone.Text;
            objloger_application.NoticeEmail = txtNoticeEmail.Text;
            objloger_application.LogerCount = txtLogerCount.TextInt;
            objloger_application.NoticeInterval = txtNoticeInterval.TextInt;
            objloger_application.NoticeSleep = txtNoticeSleep.TextInt;
            objloger_application.NoticeCategory = chkNoticeCategory.SelectValueString;
            objloger_application.IntervalMinutes = dropIntervalMinutes.SelectValueInt;
            objloger_application.MinutesMaxCount = txtMinutesMaxCount.TextInt;
            objloger_application.HeaderKey = txtHeaderKey.TextTrim;
            objloger_application.RequestKey = txtRequestKey.TextTrim;
            if (objloger_application.IsNotice)
            {
                if (objloger_application.NoticeCategory.IsNull())
                {
                    MessageDialog("不好意思,选择日志通知必须选择通知日志类型");
                    return;
                }

                if (objloger_application.NoticeEmail.IsNull() && objloger_application.NoticePhone.IsNull())
                {
                    MessageDialog("不好意思,选择日志通知必须输入邮箱或手机号码其中一个");
                    return;
                }
            }
            objLogRule.UpdateApplication(objloger_application);
            RefreshFrame("frmApplicationTree", "ApplicationTree.aspx?ApplicationID=" + objloger_application.ApplicationID.ToString(), "修改成功", "ApplicationInfo.aspx?ApplicationID=" + objloger_application.ApplicationID.ToString());
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
                Redirect("ApplicationInfo.aspx?ApplicationID=" + (ParentID.IsNoNull() ? ParentID : ApplicationID));
                break;
        }

    }

}