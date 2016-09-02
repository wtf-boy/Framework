using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using WTF.Framework;
using WTF.Logging;
using WTF.Logging.Entity;
public partial class ServiceLayer_Loging_ApplicationInfo : SupportPageBase
{
    public int ApplicationID
    {
        get
        {
            return GetInt("ApplicationID");

        }

    }
    public override string Condition
    {
        get
        {
            return "it.ApplicationID=" + ApplicationID;
        }
    }
    public override string SortExpression
    {
        get
        {
            return "it.CategoryName";
        }
    }
    public loger_application objloger_application = new loger_application();
    public override void InitDataPage()
    {
        if (ApplicationID == 1)
        {
            MyTopToolBar.CommandHidden = "RemoveApplication,Move";
        }
        base.InitDataPage();
    }
    LogRule objLogRule = new LogRule();
    public override void RenderPage()
    {
        objloger_application = objLogRule.loger_application.FirstOrDefault(s => s.ApplicationID == ApplicationID);

        this.CurrentBindData<loger_category>(gdvContent, objLogRule.loger_category);
    }
    protected override void CurrentTool_ItemCommand(object sender, WTF.Controls.MyCommandEventArgs e)
    {

        switch (e.CommandName)
        {
            case "CreateApplication":
                Redirect("ApplicationEdit.aspx?ParentID=" + ApplicationID);
                break;
            case "ModifyApplication":
                Redirect("ApplicationEdit.aspx?ApplicationID=" + ApplicationID);
                break;
            case "Move":
                Redirect("ApplicationMove.aspx?ApplicationID=" + ApplicationID);
                break;

            case "RemoveApplication":
                int ParentID = objLogRule.DeleteApplication(ApplicationID); ;
                RefreshFrame("frmApplicationTree", "ApplicationTree.aspx?ApplicationID=" + ParentID, "删除成功", "ApplicationInfo.aspx?ApplicationID=" + ParentID);
                break;
            case "Create":
                Redirect("LogCategoryTypeEdit.aspx?ApplicationID=" + ApplicationID);
                break;
            case "InitCategory":
                objLogRule.UpdateApplicationCategory(ApplicationID);
                RenderPage();
                MessageDialog("初始化成功");
                break;

        }

    }
    protected override void CurrentContent_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "Remove":
                objLogRule.loger_category.DeleteDataPrimaryKey(gdvContent.SelectedRowDataKeys);
                RenderPage();
                break;
            case "Modify":
                RedirectState("LogCategoryTypeEdit.aspx?ApplicationID=" + ApplicationID + "&CategoryID=" + e.CommandArgument.ToString());
                break;

        }
    }

}