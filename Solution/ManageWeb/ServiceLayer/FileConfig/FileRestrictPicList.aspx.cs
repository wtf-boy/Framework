using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WTF.Framework;
using WTF.Resource;
using WTF.Resource.Entity;
public partial class ServiceLayer_FileConfig_FileRestrictPicList : SupportPageBase
{
    public string FileRestrictID
    {
        get
        {
            return GetString("FileRestrictID");

        }

    }

    public string FileResourceID
    {
        get
        {
            return GetString("FileResourceID");

        }
    }
    public override string Condition
    {
        get
        {
            return "it.FileRestrictID='" + FileRestrictID + "'";
        }
    }
    public override string SortExpression
    {
        get
        {
            return "it.SortIndex";
        }
    }
    FileResourceRule objFileResourceRule = new FileResourceRule();
    public override void RenderPage()
    {
        this.CurrentBindData<resource_filerestrictpic>(gdvContent, objFileResourceRule.resource_filerestrictpic);
    }
    protected override void CurrentTool_ItemCommand(object sender, WTF.Controls.MyCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "Create":
                Redirect("FileRestrictPicEdit.aspx?FileResourceID=" + FileResourceID + "&FileRestrictID=" + FileRestrictID);
                break;
            case "Remove":
                objFileResourceRule.DeletefilerestrictpicByKey(gdvContent.SelectedRowDataKeys);
                RenderPage();
                break;
            case "Back":
                Redirect("FileRestrictList.aspx?FileResourceID=" + FileResourceID + "&FileRestrictID=" + FileRestrictID);
                break;

        }
    }
    protected override void CurrentContent_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "Modify":
                RedirectState("FileRestrictPicEdit.aspx?FileResourceID=" + FileResourceID + "&FileRestrictID=" + FileRestrictID + "&SystemFilePicID=" + e.CommandArgument.ToString());
                break;
            case "Remove":
                objFileResourceRule.DeletefilerestrictpicByKey(e.CommandArgument.ToString());
                RenderPage();
                break;

        }
    }


}