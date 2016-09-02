using WTF.Resource;
using WTF.Resource.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ServiceLayer_FileConfig_FileResourceList : SupportPageBase
{
    public override string SortExpression
    {
        get
        {
            return "it.FileResourceCode desc";
        }
    }
    FileResourceRule objFileResourceRule = new FileResourceRule();
    public override void RenderPage()
    {
        this.CurrentBindData<resource_fileresource>(gdvContent, objFileResourceRule.resource_fileresource);
    }
    protected override void CurrentTool_ItemCommand(object sender, WTF.Controls.MyCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "Create":
                Redirect("FileResourceEdit.aspx");
                break;
            case "Search":

                SearchCondition();
                break;
            case "ResourcePath":
                Redirect("FileStoragePathList.aspx");

                break;


        }


    }
    protected override void CurrentContent_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "Modify":
                RedirectState("FileResourceEdit.aspx?FileResourceID=" + e.CommandArgument.ToString());
                break;
            case "Remove":
                objFileResourceRule.DeletefileresourceByKey(e.CommandArgument.ToString());
                RenderPage();
                break;
            case "Restrict":
                TransferID = e.CommandArgument.ToString();
                RedirectState("FileRestrictList.aspx?FileResourceID=" + e.CommandArgument.ToString());
                RenderPage();
                break;

        }
    }

}