using WTF.Resource;
using WTF.Resource.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ServiceLayer_FileConfig_FileRestrictList : SupportPageBase
{
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
            return "it.FileResourceID='" + FileResourceID + "'";
        }
    }
    public override string SortExpression
    {
        get
        {
            return "it.RestrictCode";
        }
    }

    public string GetRestrictType(int RestrictType)
    {
        switch (RestrictType)
        {
            case 1:
                return "文件";
                break;
            case 2:
                return "图片";
                break;
            case 3:
                return "Flash";
                break;
            case 4:
                return "多媒体";
                break;
        }
        return "";

    }
    FileResourceRule objFileResourceRule = new FileResourceRule();
    public override void RenderPage()
    {

        this.CurrentBindData<resource_filerestrict>(gdvContent, objFileResourceRule.resource_filerestrict.Include("resource_filestoragepath"));
    }
    protected override void CurrentTool_ItemCommand(object sender, WTF.Controls.MyCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "Create":
                Redirect("FileRestrictEdit.aspx?FileResourceID=" + FileResourceID);
                break;
            case "Remove":
                objFileResourceRule.DeletefilerestrictByKey(gdvContent.SelectedRowDataKeys);
                RenderPage();
                break;
            case "Back":
                Redirect("FileResourceList.aspx");
                break;

        }


    }
    protected override void CurrentContent_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "Modify":
                Redirect("FileRestrictEdit.aspx?FileResourceID=" + FileResourceID + "&FileRestrictID=" + e.CommandArgument.ToString());
                break;
            case "Remove":
                objFileResourceRule.DeletefilerestrictByKey(e.CommandArgument.ToString());
                RenderPage();
                break;
            case "RestrictPic":
                Redirect("FileRestrictPicList.aspx?FileResourceID=" + FileResourceID + "&FileRestrictID=" + e.CommandArgument.ToString());
                break;

        }
    }


}