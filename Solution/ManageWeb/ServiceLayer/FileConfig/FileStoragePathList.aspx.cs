using WTF.Resource;
using WTF.Resource.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ServiceLayer_FileConfig_FileStoragePathList : SupportPageBase
{
    public override string SortExpression
    {
        get
        {
            return "it.StoragePathName";
        }
    }
    FileResourceRule objFileResourceRule = new FileResourceRule();
    public override void RenderPage()
    {

        this.CurrentBindData<resource_filestoragepath>(gdvContent, objFileResourceRule.resource_filestoragepath);
    }
    protected override void CurrentTool_ItemCommand(object sender, WTF.Controls.MyCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "Create":
                Redirect("FileStoragePathEdit.aspx");
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
                RedirectState("FileStoragePathEdit.aspx?FileStoragePathID=" + e.CommandArgument.ToString());
                break;
            case "Remove":
                string FileStoragePathID = e.CommandArgument.ToString();
                if (objFileResourceRule.resource_filerestrict.Any(s => s.FileStoragePathID == FileStoragePathID))
                {
                    MessageDialog("对不起此存储已经被使用，因此无法删除！");
                    return;
                }
                objFileResourceRule.DeletefilestoragepathByKey(FileStoragePathID);
                RenderPage();
                break;

        }
    }

}