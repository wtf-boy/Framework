using WTF.Resource;
using WTF.Resource.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WTF.Framework;
using FastDFS.Client;
public partial class ServiceLayer_FileConfig_FileHistoryList : SupportPageBase
{
    public override int PageSize
    {
        get
        {
            return (base.PageSize / 5) * 9;
        }
    }

    public override string SortExpression
    {
        get
        {
            return "it.FileHistoryID desc";
        }
    }
    FileResourceRule objFileResourceRule = new FileResourceRule();
    public override void InitDataPage()
    {
        FileResourceID.Items.Clear();
        FileResourceID.Items.Add(new ListItem("--请选择--", ""));
        foreach (resource_fileresource objresource_fileresource in objFileResourceRule.resource_fileresource)
        {
            FileResourceID.Items.Add(new ListItem(objresource_fileresource.FileResourceName, objresource_fileresource.FileResourceID));
        }

    }
    public override void RenderPage()
    {
        int recordCount = 0;

        rtpPicHistoryList.DataSource = objFileResourceRule.resource_filehistory.GetPage(SearchCondition<resource_filehistory>(), SearchSortExpression(), PageSize, PageIndex, out recordCount);
        rtpPicHistoryList.DataBind();
        myPager.RecordCount = recordCount;
        myPager.PageSize = PageSize;
        myPager.CurrentPageIndex = PageIndex;

    }
    protected override void CurrentTool_ItemCommand(object sender, WTF.Controls.MyCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "Search":

                SearchCondition();
                break;

        }


    }


    protected void rtpPicHistoryList_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        try
        {
            int FileHistoryID = e.CommandArgument.ToString().ConvertInt();
            resource_filehistory objPicHistory = objFileResourceRule.resource_filehistory.FirstOrDefault(s => s.FileHistoryID == FileHistoryID);
            string PicUrl = objPicHistory.PicUrl;
            if (PicUrl.IndexOf("M00") >= 0)
            {
                resource_filestoragepath objresource_filestoragepath = objFileResourceRule.resource_filestoragepath.FirstOrDefault(s => s.FileStoragePathID == "590e546a-4695-48ef-af7f-c8f6de287bc7");
                PicUrl = PicUrl.Substring(PicUrl.IndexOf("M00"));
                int i = PicUrl.IndexOf('?');
                if (i > 0)
                {
                    PicUrl = PicUrl.Substring(0, i);
                }
                FastDFSClient objFastDFSClient = new FastDFSClient(objresource_filestoragepath.StorageConfig, string.IsNullOrWhiteSpace(objresource_filestoragepath.StorageConfig));
                objFastDFSClient.RemoveFile("g1", PicUrl);
                objFileResourceRule.resource_filehistory.DeleteDataPrimaryKey(FileHistoryID.ToString());
                MessageDialog("删除成功");
            }
            else
            {
                MessageDialog("没有找到相应的删除处理方法，请与管理员联系");
            }

        }
        catch (Exception objExp)
        {

            Log.WriteLog("删除文件失败", objExp);
            MessageDialog("删除文件失败");

        }

    }
}