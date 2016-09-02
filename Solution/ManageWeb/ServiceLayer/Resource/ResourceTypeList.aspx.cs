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
using WTF.Resource;
using WTF.Framework;
using WTF.Resource.Entity;

public partial class ServiceLayer_Resource_ResourceTypeList : SupportPageBase
{

    public ResourceRule objResourceRule = new ResourceRule();
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    public override string SortExpression
    {
        get
        {
            return "it.ResourceTypeID";
        }
    }

    public override void RenderPage()
    {

        this.CurrentBindData<Sys_ResourceType>(this.gdvContent, objResourceRule.Sys_ResourceType);

    }
    protected override void CurrentTool_ItemCommand(object sender, WTF.Controls.MyCommandEventArgs e)
    {

        switch (e.CommandName)
        {
            case "Create":
                Redirect("../../ServiceLayer/Resource/ResourceTypeEdit.aspx");
                break;
            case "Remove":
                objResourceRule.DeleteResourceType(gdvContent.SelectedRowDataKeys);
                RenderPage();
                break;
            case "ResourcePath":
                Redirect("../../ServiceLayer/Resource/ResourcePathList.aspx");
                break;
            case "WaterImage":
                Redirect("../../ServiceLayer/Resource/WaterImageList.aspx");
                break;

            case "Search":
                SearchCondition();
                break;
            case "SyncSql":
                SyncSql();
                break;

        }

    }
    public void SyncSql()
    {
        MyDataSyncHelper objDataSyncHelper = new MyDataSyncHelper("SevenConnectionString");

        string ModuleSyncTableSchema = ConfigHelper.GetValue("ModuleSyncTableSchema", "openData");
        System.Text.StringBuilder objStringBuilder = new System.Text.StringBuilder();
        objStringBuilder.AppendLine(DataSyncHelper.HeaderQuery);
        objStringBuilder.AppendLine(DataSyncHelper.BeginTransaction);
        objStringBuilder.AppendLine(objDataSyncHelper.CreateSyncQuery(ModuleSyncTableSchema,"Sys_ResourceType", "", "", false, false));
        objStringBuilder.AppendLine(objDataSyncHelper.CreateSyncQuery(ModuleSyncTableSchema,"Sys_ResourceRestrict", "", "", false, false));
        objStringBuilder.AppendLine(objDataSyncHelper.CreateSyncQuery(ModuleSyncTableSchema,"Sys_ResourceRestrictPic", "", "", false, false));
        objStringBuilder.AppendLine(DataSyncHelper.EndTransaction);
        string SqlFileName = "Resource" + DateTime.Now.ToString("yyyy-MM-dd-hh") + ".sql";
        Response.ContentEncoding = System.Text.Encoding.UTF8;
        Response.AppendHeader("Content-Disposition", "attachment;filename=" + SqlFileName);
        Response.Write(objStringBuilder.ToString());
        Response.End();
    }
    protected override void CurrentContent_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "Create":
                Redirect("../../ServiceLayer/Resource/ResourceTypeEdit.aspx");
                break;
            case "Remove":
                objResourceRule.DeleteResourceType(e.CommandArgument.ToString());
                RenderPage();
                RenderPage();
                break;
            case "Modify":
                RedirectState("../../ServiceLayer/Resource/ResourceTypeEdit.aspx?ResourceTypeID=" + e.CommandArgument.ToString());
                break;
            case "Restrict":
                RedirectState("../../ServiceLayer/Resource/ResourceRestrictList.aspx?ResourceTypeID=" + e.CommandArgument.ToString());
                break;

        }
    }
}
