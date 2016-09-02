using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WTF.Framework;
using WTF.Resource.Entity;
using WTF.Resource;
public partial class ServiceLayer_FileConfig_FileStoragePathEdit : SupportPageBase
{
    /// <summary>
    /// 获取存储标识
    /// </summary>
    public string FileStoragePathID
    {
        get
        {
           
            return GetString("FileStoragePathID");

        }

    }
    /// <summary>
    /// 变量
    /// </summary>
    public resource_filestoragepath objresource_filestoragepath = new resource_filestoragepath();
    FileResourceRule objFileResourceRule = new FileResourceRule();
    /// <summary>
    /// 页面加载
    /// </summary>
    public override void RenderPage()
    {

        if (FileStoragePathID.IsNoNull())
        {
            objresource_filestoragepath = objFileResourceRule.resource_filestoragepath.FirstOrDefault(s => s.FileStoragePathID == FileStoragePathID);
            if (CheckEditObjectIsNull(objresource_filestoragepath)) return;
            ///存储类型1本地存储2FTP3文件系统
            radStorageTypeID.SelectedValue = objresource_filestoragepath.StorageTypeID.ToString();

            Page.DataBind();
        }
        else
        {
        }

    }
    /// <summary>
    /// 保存信息
    /// </summary>
    public void SaveInfo()
    {
        if (FileStoragePathID.IsNull())
        {

            objresource_filestoragepath.FileStoragePathID = Guid.NewGuid().ToString();
            ///存储名称
            objresource_filestoragepath.StoragePathName = txtStoragePathName.TextCutWord(256);
            ///存储类型1本地存储2FTP3文件系统
            objresource_filestoragepath.StorageTypeID = radStorageTypeID.SelectValueInt;
            ///虚目录
            objresource_filestoragepath.VirtualName = txtVirtualName.TextCutWord(1000);
            ///存储地址
            objresource_filestoragepath.StoragePath = txtStoragePath.TextCutWord(1000);
            ///IP
            objresource_filestoragepath.IPAddress = txtIPAddress.TextCutWord(100);
            ///帐号
            objresource_filestoragepath.Account = txtAccount.TextCutWord(100);
            ///密码
            objresource_filestoragepath.Password = txtPassword.TextCutWord(100);
            ///端口
            objresource_filestoragepath.Port = txtPort.TextCutWord(100);
            objresource_filestoragepath.StorageConfig = txtStorageConfig.Text;
            objFileResourceRule.Insertfilestoragepath(objresource_filestoragepath);
            MessageDialog("新增成功", "FileStoragePathList.aspx");
        }
        else
        {
            objresource_filestoragepath = objFileResourceRule.resource_filestoragepath.FirstOrDefault(p => p.FileStoragePathID == FileStoragePathID);
            if (CheckEditObjectIsNull(objresource_filestoragepath)) return;
            ///存储名称
            objresource_filestoragepath.StoragePathName = txtStoragePathName.TextCutWord(256);
            ///存储类型1本地存储2FTP3文件系统
            objresource_filestoragepath.StorageTypeID = radStorageTypeID.SelectValueInt;
            ///虚目录
            objresource_filestoragepath.VirtualName = txtVirtualName.TextCutWord(1000);
            ///存储地址
            objresource_filestoragepath.StoragePath = txtStoragePath.TextCutWord(1000);
            ///IP
            objresource_filestoragepath.IPAddress = txtIPAddress.TextCutWord(100);
            ///帐号
            objresource_filestoragepath.Account = txtAccount.TextCutWord(100);
            ///密码
            objresource_filestoragepath.Password = txtPassword.TextCutWord(100);
            ///端口
            objresource_filestoragepath.Port = txtPort.TextCutWord(100);
            objresource_filestoragepath.StorageConfig = txtStorageConfig.Text;
            objFileResourceRule.Updatefilestoragepath(objresource_filestoragepath);
            MessageDialog("修改成功", "FileStoragePathList.aspx");
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
                Redirect("FileStoragePathList.aspx");
                break;
        }

    }

}