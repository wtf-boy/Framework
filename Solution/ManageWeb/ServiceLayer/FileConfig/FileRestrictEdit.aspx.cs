using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WTF.Framework;
using WTF.Resource.Entity;
using WTF.Resource;
public partial class ServiceLayer_FileConfig_FileRestrictEdit : SupportPageBase
{
    /// <summary>
    /// 获取文件限制标识
    /// </summary>
    public string FileRestrictID
    {
        get
        {
            return GetString("FileRestrictID");

        }

    }
    /// <summary>
    /// 获取文件资源标识
    /// </summary>
    public string FileResourceID
    {
        get
        {
            return GetString("FileResourceID");

        }

    }

    public override void InitDataPage()
    {

        radFileStoragePathID.Items.Clear();
        foreach (var filestoragepathin in objFileResourceRule.resource_filestoragepath)
        {
            radFileStoragePathID.Items.Add(new ListItem(filestoragepathin.StoragePathName, filestoragepathin.FileStoragePathID));
        }


        radAccessModeCodeType.Items.Clear();
        foreach (var EnumMember in typeof(AccessModeCodeType).GetEnumMembers())
        {
            radAccessModeCodeType.Items.Add(new ListItem(EnumMember.Description, EnumMember.Value.ToString()));
        }

        radPathFormatCodeType.Items.Clear();
        foreach (var EnumMember in typeof(PathFormatCodeType).GetEnumMembers())
        {
            radPathFormatCodeType.Items.Add(new ListItem(EnumMember.Description, EnumMember.Value.ToString()));
        }
    }
    /// <summary>
    /// 变量
    /// </summary>
    public resource_filerestrict objresource_filerestrict = new resource_filerestrict();
    FileResourceRule objFileResourceRule = new FileResourceRule();
    /// <summary>
    /// 页面加载
    /// </summary>
    public override void RenderPage()
    {

        if (FileRestrictID.IsNoNull())
        {
            objresource_filerestrict = objFileResourceRule.resource_filerestrict.FirstOrDefault(s => s.FileRestrictID == FileRestrictID);
            if (CheckEditObjectIsNull(objresource_filerestrict)) return;
            ///文件访问方式
            radAccessModeCodeType.SelectedValue = objresource_filerestrict.AccessModeCodeType.ToString();
            ///目录存储格式
            radPathFormatCodeType.SelectedValue = objresource_filerestrict.PathFormatCodeType.ToString();
            ///1文件2图片3flash4mid
            radFileType.SelectedValue = objresource_filerestrict.FileType.ToString();
            radFileStoragePathID.SelectedValue = objresource_filerestrict.FileStoragePathID;
            chkIsReturnSize.Checked = objresource_filerestrict.IsReturnSize == 1;
            chkIsHistory.Checked = objresource_filerestrict.IsHistory == 1;
            chkIsMd5.Checked = objresource_filerestrict.IsMd5 == 1;
            Page.DataBind();
        }
        else
        {
            radAccessModeCodeType.SelectedIndex = 0;
            radPathFormatCodeType.SelectedIndex = 0;

        }

    }
    /// <summary>
    /// 保存信息
    /// </summary>
    public void SaveInfo()
    {
        if (FileRestrictID.IsNull())
        {
            objresource_filerestrict.FileRestrictID = Guid.NewGuid().ToString();
            objresource_filerestrict.FileResourceID = FileResourceID;
            ///存储路经标识
            objresource_filerestrict.FileStoragePathID = radFileStoragePathID.SelectedValue;
            ///文件访问方式
            objresource_filerestrict.AccessModeCodeType = radAccessModeCodeType.SelectValueInt;
            ///目录存储格式
            objresource_filerestrict.PathFormatCodeType = radPathFormatCodeType.SelectValueInt;
            ///限制名称
            objresource_filerestrict.RestrictName = txtRestrictName.TextCutWord(100);
            ///限制码
            objresource_filerestrict.RestrictCode = txtRestrictCode.TextCutWord(1000);
            ///1文件2图片3flash4mid
            objresource_filerestrict.FileType = radFileType.SelectValueInt;
            ///文件扩展名
            objresource_filerestrict.FileExtension = txtFileExtension.TextCutWord(256);
            ///大小限制单位(K)0不限制
            objresource_filerestrict.FileMaxSize = txtFileMaxSize.TextInt;

            objresource_filerestrict.IsReturnSize = chkIsReturnSize.Checked ? 1 : 0;
            objresource_filerestrict.IsHistory = chkIsHistory.Checked ? 1 : 0;
            objresource_filerestrict.IsMd5 = chkIsMd5.Checked ? 1 : 0;
            objFileResourceRule.Insertfilerestrict(objresource_filerestrict);
            MessageDialog("新增成功", "FileRestrictList.aspx?FileResourceID=" + FileResourceID);
        }
        else
        {
            objresource_filerestrict = objFileResourceRule.resource_filerestrict.FirstOrDefault(p => p.FileRestrictID == FileRestrictID);
            if (CheckEditObjectIsNull(objresource_filerestrict)) return;
            ///存储路经标识
            objresource_filerestrict.FileStoragePathID = radFileStoragePathID.SelectedValue;
            ///文件访问方式
            objresource_filerestrict.AccessModeCodeType = radAccessModeCodeType.SelectValueInt;
            ///目录存储格式
            objresource_filerestrict.PathFormatCodeType = radPathFormatCodeType.SelectValueInt;
            ///限制名称
            objresource_filerestrict.RestrictName = txtRestrictName.TextCutWord(100);
            ///限制码
            objresource_filerestrict.RestrictCode = txtRestrictCode.TextCutWord(1000);
            ///1文件2图片3flash4mid
            objresource_filerestrict.FileType = radFileType.SelectValueInt;
            ///文件扩展名
            objresource_filerestrict.FileExtension = txtFileExtension.TextCutWord(256);
            ///大小限制单位(K)0不限制
            objresource_filerestrict.FileMaxSize = txtFileMaxSize.TextInt;
            objresource_filerestrict.IsReturnSize = chkIsReturnSize.Checked ? 1 : 0;
            objresource_filerestrict.IsHistory = chkIsHistory.Checked ? 1 : 0;
            objresource_filerestrict.IsMd5 = chkIsMd5.Checked ? 1 : 0;
            objFileResourceRule.Updatefilerestrict(objresource_filerestrict);
            MessageDialog("修改成功", "FileRestrictList.aspx?FileResourceID=" + FileResourceID);
        }
    }

    /// <summary>
    /// 工具栏操作
    /// </summary>
    protected override void CurrentTool_ItemCommand(object sender, WTF.Controls.MyCommandEventArgs e)
    {

        switch (e.CommandName)
        {
            case "Save":
                SaveInfo();
                break;
            case "Back":
                Redirect("FileRestrictList.aspx?FileResourceID=" + FileResourceID);
                break;
        }

    }

}