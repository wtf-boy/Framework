using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WTF.Framework;
using WTF.Resource;
using WTF.Resource.Entity;
public partial class ServiceLayer_Resource_ResourcePathEdit : SupportPageBase
{


    /// <summary>
    /// 标识
    /// </summary>
    public string ResourcePathID
    {
        get
        {
            return GetString("ResourcePathID");
        }
    }
    ResourceRule objResourceRule = new ResourceRule();
    public Sys_ResourcePath objSys_ResourcePath = new Sys_ResourcePath();
    /// <summary>
    /// 页面初始化
    /// </summary>
    public override void RenderPage()
    {
        if (ResourcePathID.IsNoNull())
        {
        
            objSys_ResourcePath = objResourceRule.Sys_ResourcePath.First(s => s.ResourcePathID == ResourcePathID);
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
        if (ResourcePathID.IsNull())
        {

            objSys_ResourcePath.ResourcePathID = Guid.NewGuid().ToString();
            objSys_ResourcePath.ResourcePathName = txtResourcePathName.Text;
            objSys_ResourcePath.VirtualName = txtVirtualName.Text;
            objSys_ResourcePath.StoragePath = txtStoragePath.Text;
            objResourceRule.CurrentEntities.AddTosys_resourcepath(objSys_ResourcePath);
            objResourceRule.SaveChanges();
            MessageDialog("新增成功", "ResourcePathList.aspx");

        }
        else
        {
            objSys_ResourcePath = objResourceRule.Sys_ResourcePath.First(s => s.ResourcePathID == ResourcePathID);
            
            objSys_ResourcePath.ResourcePathName = txtResourcePathName.Text;
            objSys_ResourcePath.VirtualName = txtVirtualName.Text;
            objSys_ResourcePath.StoragePath = txtStoragePath.Text;
            objResourceRule.SaveChanges();
            MessageDialog("修改成功", "ResourcePathList.aspx");
        }

    }

    /// <summary>
    /// 操作栏
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected override void CurrentTool_ItemCommand(object sender, WTF.Controls.MyCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "Save":
                SaveInfo();
                break;
            case "Back":
                Redirect("ResourcePathList.aspx");
                break;
        }
    }

}