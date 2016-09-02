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
public partial class ServiceLayer_Resource_ResourceList : SupportPageBase
{
    public override SearchRow SearchRow
    {
        get
        {
            return WTF.Framework.SearchRow.None;
        }
    }
    public ResourceRule objResourceRule = new ResourceRule();
    public int ResourceTypeID
    {
        get
        {

            return GetInt("ResourceTypeID");
        }
    }
    public override string PowerPageCode
    {
        get
        {
            return "ServiceLayer_Resource_ResourceFrame";
        }
    }

    public override string SortExpression
    {
        get
        {
            return "it.CreateDate";
        }
    }

    public override void RenderPage()
    {
        this.CurrentBindData<Sys_Resource>(this.gdvContent,  objResourceRule.Sys_ResourceType.First(p => p.ResourceTypeID == ResourceTypeID).Sys_Resource.CreateSourceQuery());
    }
    protected override void CurrentTool_ItemCommand(object sender, WTF.Controls.MyCommandEventArgs e)
    {

        switch (e.CommandName)
        {
            case "Create":
                Redirect("../../ServiceLayer/Resource/ResourceEdit.aspx?ResourceTypeID=" + ResourceTypeID.ToString());
                break;
            case "CreateVer":
                Redirect("../../ServiceLayer/Resource/ResourceVerEdit.aspx?ResourceTypeID=" + ResourceTypeID.ToString() + "&ResourceID=" + gdvContent.SelectedRowFirstKey);
                break;
            case "ViewVer":
                Redirect("../../ServiceLayer/Resource/ResourceVerList.aspx?ResourceTypeID=" + ResourceTypeID.ToString() + "&ResourceID=" + gdvContent.SelectedRowFirstKey);
                break;
            case "Remove":
                objResourceRule.DeleteResource(gdvContent.SelectedRowDataKeys);
                RenderPage();
                break;
            case "Modify":
                Redirect("../../ServiceLayer/Resource/ResourceEdit.aspx?ResourceTypeID=" + gdvContent.SelectedRowDataKeys);
                break;
        }


    }

    protected override void CurrentContent_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "Create":
                Redirect("../../ServiceLayer/Resource/ResourceEdit.aspx");
                break;
            case "CreateVer":
                Redirect("../../ServiceLayer/Resource/ResourceVerEdit.aspx?ResourceTypeID=" + ResourceTypeID.ToString() + "&ResourceID=" + e.CommandArgument.ToString());
                break;
            case "Modify":
                Redirect("../../ServiceLayer/Resource/ResourceEdit.aspx?ResourceTypeID=" + e.CommandArgument.ToString());
                break;

            case "ViewVer":
                Redirect("../../ServiceLayer/Resource/ResourceVerList.aspx?ResourceTypeID=" + ResourceTypeID.ToString() + "&ResourceID=" + e.CommandArgument.ToString());
                break;

            case "Remove":
                objResourceRule.DeleteResource(e.CommandArgument.ToString());
                RenderPage();
                break;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {



    }
}
