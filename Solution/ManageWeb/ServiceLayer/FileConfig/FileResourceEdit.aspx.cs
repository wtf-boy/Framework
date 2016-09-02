using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WTF.Framework;
using WTF.Resource.Entity;
using WTF.Resource;
public partial class ServiceLayer_FileConfig_FileResourceEdit : SupportPageBase
{
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
    /// <summary>
    /// 变量
    /// </summary>
    public resource_fileresource objresource_fileresource = new resource_fileresource();
    FileResourceRule objFileResourceRule = new FileResourceRule();
    /// <summary>
    /// 页面加载
    /// </summary>
    public override void RenderPage()
    {

        if (FileResourceID.IsNoNull())
        {
            objresource_fileresource = objFileResourceRule.resource_fileresource.FirstOrDefault(s => s.FileResourceID == FileResourceID);
            if (CheckEditObjectIsNull(objresource_fileresource)) return;

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
        if (FileResourceID.IsNull())
        {
            ///文件资源名称
         
            objresource_fileresource.FileResourceID = Guid.NewGuid().ToString();
            objresource_fileresource.FileResourceName = txtFileResourceName.TextCutWord(100);
            ///文件资源代码
            objresource_fileresource.FileResourceCode = txtFileResourceCode.TextCutWord(100);
            ///创建时间
            objresource_fileresource.CreateDate = DateTime.Now;
            objFileResourceRule.Insertfileresource(objresource_fileresource);
            MessageDialog("新增成功", "FileResourceList.aspx");
        }
        else
        {
            objresource_fileresource = objFileResourceRule.resource_fileresource.FirstOrDefault(p => p.FileResourceID == FileResourceID);
            if (CheckEditObjectIsNull(objresource_fileresource)) return;
            objresource_fileresource.FileResourceName = txtFileResourceName.TextCutWord(100);
            ///文件资源代码
            objresource_fileresource.FileResourceCode = txtFileResourceCode.TextCutWord(100);
            ///创建时间
            objresource_fileresource.CreateDate = DateTime.Now;
            objFileResourceRule.Updatefileresource(objresource_fileresource);
            MessageDialog("修改成功", "FileResourceList.aspx");
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
                Redirect("FileResourceList.aspx");
                break;
        }

    }

}