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
public partial class ServiceLayer_Resource_ResourceVerList : SupportPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
   
    public ResourceRule objResourceRule = new ResourceRule();

    public int ResourceTypeID
    {
        get
        {
            return GetInt("ResourceTypeID");
        }
    }
    public string ResourceID
    {
        get
        {
            return GetString("ResourceID");
        }
    }

    public override string SortExpression
    {
        get
        {
            return "it.VerNo";
        }
    }
    public override void RenderPage()
    {
        this.CurrentBindData<Sys_ResourceVer>(this.gdvContent, objResourceRule.Sys_Resource.First(p => p.ResourceID == ResourceID).Sys_ResourceVer.CreateSourceQuery());
    }

    protected override void CurrentContent_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {

            case "Create":
                Redirect("../../ServiceLayer/Resource/ResourceVerEdit.aspx?ResourceTypeID=" + ResourceTypeID.ToString() + "&ResourceID=" + ResourceID.ToString());
                break;
            case "Remove":
                objResourceRule.DeleteResource(ResourceID, e.CommandArgument.ToString());
                RenderPage();
                break;
            case "Modify":
                Redirect("../../ServiceLayer/Resource/ResourceVerEdit.aspx?ResourceTypeID=" + ResourceTypeID.ToString() + "&ResourceID=" + ResourceID.ToString() + "&VerNo=" + e.CommandArgument.ToString());
                break;
            case "Back":
                Redirect("../../ServiceLayer/Resource/ResourceList.aspx?ResourceTypeID=" + ResourceTypeID.ToString());
                break;
        }
    }

    protected override void CurrentTool_ItemCommand(object sender, WTF.Controls.MyCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "Create":
                Redirect("../../ServiceLayer/Resource/ResourceVerEdit.aspx?ResourceTypeID=" + ResourceTypeID.ToString() + "&ResourceID=" + ResourceID.ToString());
                break;
            case "Remove":
                objResourceRule.DeleteResource(ResourceID, gdvContent.SelectedRowDataKeys);
                RenderPage();
                break;
            case "Modify":
                Redirect("../../ServiceLayer/Resource/ResourceVerEdit.aspx?ResourceTypeID=" + ResourceTypeID.ToString() + "&ResourceID=" + ResourceID.ToString() + "&VerNo=" + gdvContent.SelectedRowFirstKey);
                break;
            case "Back":
                Redirect("../../ServiceLayer/Resource/ResourceList.aspx?ResourceTypeID=" + ResourceTypeID.ToString());
                break;
        }

    }



}
