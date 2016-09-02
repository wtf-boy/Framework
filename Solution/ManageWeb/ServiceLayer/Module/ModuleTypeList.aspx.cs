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
using WTF.Power;
using WTF.Framework;
using WTF.Power.Entity;
public partial class ServiceLayer_Module_ModuleTypeList : SupportPageBase
{

    private ModuleRule objModuleRule = new ModuleRule();
    protected void Page_Load(object sender, EventArgs e)
    {

    }


    public override string SortExpression
    {
        get
        {
            return "it.ModuleTypeCode";
        }
    }

    public override void RenderPage()
    {
        this.CurrentBindData<Sys_ModuleType>(gdvContent, objModuleRule.Sys_ModuleType);

    }
    protected override void CurrentTool_ItemCommand(object sender, WTF.Controls.MyCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "Create":
                Redirect("../../ServiceLayer/Module/ModuleTypeEdit.aspx");
                break;
            case "Modify":
                Redirect("../../ServiceLayer/Module/ModuleTypeEdit.aspx?ModuleTypeID=" + gdvContent.SelectedRowFirstKey);

                break;


            case "DataField":
                Redirect("../../ServiceLayer/Data/DataTypeList.aspx");
                break;
            case "Cote":
                Redirect("ModuleCoteList.aspx");
                break;
            case "UpdateSql":
                UpdateSql();
                break;
        }

    }
    public void UpdateSql()
    {
        DataSyncHelper objDataSyncHelper = new DataSyncHelper("SanaoConnectionString");
        string updateQuery = objDataSyncHelper.CreateSyncQuery("Sys_Module", "", "", true);
        string SqlFileName = "Seven" + DateTime.Now.ToString("yyyy-MM-dd-hh") + ".sql";
        Response.ContentEncoding = System.Text.Encoding.UTF8;
        Response.AppendHeader("Content-Disposition", "attachment;filename=" + SqlFileName);
        Response.Write(updateQuery);
        Response.End();
    }
    protected override void CurrentContent_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string ModuleTypeID = e.CommandArgument.ToString();
        switch (e.CommandName)
        {
            case "Create":
                Redirect("../../ServiceLayer/Module/ModuleTypeEdit.aspx");
                break;
            case "ToDoType":
                Redirect("../../CMS/ToDo/ToDoCategoryList.aspx?ModuleTypeID=" + e.CommandArgument.ToString());
                break;
            case "ThemeList":
                Redirect("../../ServiceLayer/Module/ThemeList.aspx?ThemeModuleTypeID=" + e.CommandArgument.ToString());
                break;
            case "Modify":
                Redirect("../../ServiceLayer/Module/ModuleTypeEdit.aspx?ModuleTypeID=" + e.CommandArgument.ToString());

                break;
            case "Remove":
                objModuleRule.DeleteModuleType(e.CommandArgument.ToString());
                RenderPage();
                MessageDialog("删除成功");
                RefreshFrame("frmModuleTree");
                break;
            case "DataSet":
                Redirect("../../ServiceLayer/Module/ModuleDataList.aspx?IsModuleType=1&ModuleID=" + e.CommandArgument.ToString());
                break;


        }
    }

}
