using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using WTF.Logging;
using WTF.Framework;
using WTF.Logging.Entity;
using System.Collections.Generic;
public partial class ServiceLayer_Loging_LogList : SupportPageBase
{
    public LogRule objLogRule = new LogRule();
    public override SearchRow SearchRow
    {
        get
        {
            return SearchRow.Two;
        }
    }

    public override string MenuPowerID
    {
        get
        {
            return base.MenuPowerID + ApplicationID;
        }
    }
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
    public override string SortExpression
    {
        get
        {
            return "it.LogID desc";
        }
    }


    public override string Condition
    {
        get
        {


            if (ApplicationID != 1)
            {
                return "it.ApplicationID=" + ApplicationID.ToString();

            }

            else
            {
                return "";
            }
        }
    }

    public override void InitDataPage()
    {
        ModuleTypeCode.Items.Clear();
        ModuleTypeCode.Items.Add(new ListItem("--全部--", ""));
        foreach (loger_moduletype objloger_moduletype in objLogRule.loger_moduletype)
        {

            ModuleTypeCode.Items.Add(new ListItem(objloger_moduletype.ModuleTypeName, objloger_moduletype.ModuleTypeCode));
        }
        dropCategoryTypeCode.Items.Add(new ListItem("--全部--", ""));

        foreach (loger_category objloger_category in objLogRule.loger_category.Where(s => s.ApplicationID == ApplicationID))
        {
            dropCategoryTypeCode.Items.Add(new ListItem(objloger_category.CategoryName, objloger_category.CategoryTypeCode));
        }
    }
    public override void RenderPage()
    {

        this.CurrentBindData<loger_loging>(gdvContent, objLogRule.loger_loging);

    }
    public string GetLogCategoryName(string categoryCode)
    {
        ListItem objcategoryCode = dropCategoryTypeCode.Items.FindByValue(categoryCode);
        if (objcategoryCode != null)
        {
            return objcategoryCode.Text;
        }
        else
        {
            return categoryCode;
        }

    }
    public string GetLogModuleTypeName(string moduleTypeCode)
    {
        ListItem objListItem = ModuleTypeCode.Items.FindByValue(moduleTypeCode);
        if (objListItem != null)
        {
            return objListItem.Text;
        }
        else
        {
            return moduleTypeCode;
        }

    }
    protected override void CurrentTool_ItemCommand(object sender, WTF.Controls.MyCommandEventArgs e)
    {

        switch (e.CommandName)
        {
            case "Remove":
                objLogRule.loger_loging.DeleteDataPrimaryKey(gdvContent.SelectedRowDataKeys);
                RenderPage();
                break;
            case "LogClear":

                Redirect("LogClear.aspx?ApplicationID=" + ApplicationID.ToString());
                break;
            case "SaveSet":
                Redirect("WriterTypeEdit.aspx?ApplicationID=" + ApplicationID.ToString());
                break;
            case "LogStat":
                Redirect("LogStatList.aspx?ApplicationID=" + ApplicationID.ToString());
                break;
            case "Search":
                SearchCondition();
                break;

        }

    }
    protected override void CurrentContent_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {

            case "Remove":
                objLogRule.loger_loging.DeleteDataPrimaryKey(e.CommandArgument.ToString());
                RenderPage();
                break;
            case "Detail":
                RedirectState("LogInfo.aspx?LogID=" + e.CommandArgument.ToString() + "&ApplicationID=" + ApplicationID.ToString());
                break;
        }
    }
}
