using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WTF.Framework;
using WTF.Logging;
using WTF.Logging.Entity;
public partial class ServiceLayer_Loging_WriterTypeEdit : SupportPageBase
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


    LogRule objLogRule = new LogRule();
    public override void RenderPage()
    {




        litCaption.Text = objLogRule.loger_application.First(s => s.ApplicationID == ApplicationID).ApplicationName + "日志分类存储设置";
        dropCategoryType.Items.Add(new ListItem("所有分类设置", ""));
        foreach (loger_category objloger_category in objLogRule.loger_category.Where(s => s.ApplicationID == ApplicationID).OrderBy(s => s.CategoryTypeCode))
        {
            dropCategoryType.Items.Add(new ListItem(objloger_category.CategoryName, objloger_category.CategoryTypeCode));
        }
        dropCategoryType.SelectedIndex = 0;

    }


    public void SaveInfo()
    {
        List<int> objApplicationIDList = new List<int>();
        if (chkChild.Checked)
        {
            string IDPath = objLogRule.loger_application.FirstOrDefault(s => s.ApplicationID == ApplicationID).IDPath;
            objApplicationIDList = objLogRule.loger_application.Where("it.IDPath like '" + IDPath + "%'").Select(s => s.ApplicationID).ToList();
        }
        else
        {
            objApplicationIDList.Add(ApplicationID);
        }
        if (dropCategoryType.SelectedValue == "")
        {
            foreach (loger_category objLog_Category in objLogRule.loger_category.Where(s => objApplicationIDList.Contains(s.ApplicationID)))
            {

                objLog_Category.LogWriteType = chkLogWriteType.SelectValueString;
            }
        }
        else
        {
            string categoryTypeCode = dropCategoryType.SelectedValue;
            foreach (loger_category objLog_Category in objLogRule.loger_category.Where(s => objApplicationIDList.Contains(s.ApplicationID) && s.CategoryTypeCode == categoryTypeCode))
            {

                objLog_Category.LogWriteType = chkLogWriteType.SelectValueString;
            }

        }

        objLogRule.SaveChanges();
        MessageDialog("设置成功");

    }

    protected override void CurrentTool_ItemCommand(object sender, WTF.Controls.MyCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "Save":
                SaveInfo();
                break;
            case "Back":

                Redirect("LogList.aspx?ApplicationID=" + ApplicationID);
                break;
        }
    }

    protected void dropCategoryType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (dropCategoryType.SelectedValue.IsNoNull())
        {
            chkLogWriteType.SetSelectValue(objLogRule.loger_category.FirstOrDefault(s => s.ApplicationID == ApplicationID && s.CategoryTypeCode == dropCategoryType.SelectedValue).LogWriteType);
        }
    }
}