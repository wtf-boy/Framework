using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WTF.Framework;
using WTF.Resource;
using WTF.Resource.Entity;
public partial class ServiceLayer_Resource_WaterImageEdit : SupportPageBase
{


    /// <summary>
    /// 标识
    /// </summary>
    public string WaterImageID
    {
        get
        {
            return GetString("WaterImageID");
        }
    }

    Sys_WaterImage objSys_WaterImage = new Sys_WaterImage();
    ResourceRule objResourceRule = new ResourceRule();
    /// <summary>
    /// 页面初始化
    /// </summary>
    public override void RenderPage()
    {
        if (WaterImageID.IsNoNull())
        {
            objSys_WaterImage = objResourceRule.Sys_WaterImage.First(s => s.WaterImageID == WaterImageID);
            txtWaterName.Text = objSys_WaterImage.WaterName;
            fupFile.ResourceID = WaterImageID;
            fupFile.DownLoadUrl = objSys_WaterImage.WaterImagePath;
            txtWaterPath.Text = objSys_WaterImage.WaterImagePath;

        }
        else
        {

            fupFile.CheckValueEmpty = true;
        }
    }

    /// <summary>
    /// 保存信息
    /// </summary>
    public void SaveInfo()
    {
        if (WaterImageID.IsNull())
        {
            if (fupFile.HasFile)
            {
                fupFile.Save();
                objSys_WaterImage.WaterImagePath = fupFile.ResourcePath;
            }
            else
            {

                objSys_WaterImage.WaterImagePath = txtWaterPath.Text;
            }
            if (objSys_WaterImage.WaterImagePath.IsNull())
            {
                MessageDialog("请选择上传水印文件或输入水印地址");
            }
            objSys_WaterImage.WaterImageID = fupFile.ResourceID.IsNull() ? Guid.NewGuid().ToString() : fupFile.ResourceID;
            objSys_WaterImage.WaterName = txtWaterName.Text;
            objResourceRule.CurrentEntities.AddTosys_waterimage(objSys_WaterImage);
            objResourceRule.SaveChanges();
            MessageDialog("新增成功", "WaterImageList.aspx");

        }
        else
        {
            objSys_WaterImage = objResourceRule.Sys_WaterImage.First(s => s.WaterImageID == WaterImageID);
            objSys_WaterImage.WaterName = txtWaterName.Text;
            if (fupFile.HasFile)
            {
                fupFile.Save();
                objSys_WaterImage.WaterImagePath = fupFile.ResourcePath;
            }
            else
            {
                objSys_WaterImage.WaterImagePath = txtWaterPath.Text;
            }
            if (objSys_WaterImage.WaterImagePath.IsNull())
            {
                MessageDialog("请选择上传水印文件或输入水印地址");
            }
            objResourceRule.SaveChanges();
            MessageDialog("修改成功", "WaterImageList.aspx");

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
                ResponseHelper.Redirect("WaterImageList.aspx");
                break;
        }
    }

}