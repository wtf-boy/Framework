using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WTF.Framework;
using WTF.Resource;
using WTF.Resource.Entity;
using WTF.Controls;
public partial class ServiceLayer_Resource_ResourceRestrictPicEdit : SupportPageBase
{
    /// <summary>
    /// 标识
    /// </summary>
    public int ResourceRestrictID
    {
        get
        {
            return GetInt("ResourceRestrictID");
        }
    }
    public int ResourceTypeID
    {
        get
        {
            return GetInt("ResourceTypeID");
        }
    }

    /// <summary>
    /// 标识
    /// </summary>
    public int ResourceRestrictPicID
    {
        get
        {
            return GetInt("ResourceRestrictPicID");
        }
    }
    public override void InitDataPage()
    {
        dropWaterImage.DataSource = objResourceRule.Sys_WaterImage;
        dropWaterImage.DataTextField = "WaterName";
        dropWaterImage.DataValueField = "WaterImageID";
        dropWaterImage.DataBind();
    }

    ResourceRule objResourceRule = new ResourceRule();
    public Sys_ResourceRestrictPic objSys_ResourceRestrictPic = new Sys_ResourceRestrictPic();
    /// <summary>
    /// 页面初始化
    /// </summary>
    public override void RenderPage()
    {
        if (ResourceRestrictPicID.IsNoNull())
        {

            objSys_ResourceRestrictPic = objResourceRule.Sys_ResourceRestrictPic.First(s => s.ResourceRestrictPicID == ResourceRestrictPicID);
            radWatermarkType.SelectedValue = objSys_ResourceRestrictPic.WatermarkType.ToString();
            radHorizontalAlign.SelectedValue = objSys_ResourceRestrictPic.HorizontalAlign.ToString();
            radVerticalAlign.SelectedValue = objSys_ResourceRestrictPic.VerticalAlign.ToString();
            chkCreateWaterMark.Checked = objSys_ResourceRestrictPic.CreateWaterMark;
            if ((WatermarkType)objSys_ResourceRestrictPic.WatermarkType == WatermarkType.WaterImage && objSys_ResourceRestrictPic.CreateWaterMark)
            {
                dropWaterImage.SelectedValue = objSys_ResourceRestrictPic.WaterImageID.ToString();
            }
            Page.DataBind();

        }
        else
        {
            chkCreateWaterMark.Checked = false;
            radWatermarkType.SelectedValue = "1";
            radHorizontalAlign.SelectedValue = "1";
            radVerticalAlign.SelectedValue = "1";

        }
    }

    /// <summary>
    /// 保存信息
    /// </summary>
    public void SaveInfo()
    {
        if (ResourceRestrictPicID.IsNull())
        {

            objSys_ResourceRestrictPic.ResourceRestrictID = ResourceRestrictID;
            objSys_ResourceRestrictPic.CreateWaterMark = chkCreateWaterMark.Checked;
            objSys_ResourceRestrictPic.ImageHeight = txtImageHeight.TextInt;
            objSys_ResourceRestrictPic.ImageWidth = txtImageWidth.TextInt;
            objSys_ResourceRestrictPic.WatermarkType = radWatermarkType.SelectValueInt;
            objSys_ResourceRestrictPic.HorizontalAlign = radHorizontalAlign.SelectValueInt;
            objSys_ResourceRestrictPic.VerticalAlign = radVerticalAlign.SelectValueInt;
            objSys_ResourceRestrictPic.WatermarkText = txtWatermarkText.Text;
            objSys_ResourceRestrictPic.VerNo = txtVerNo.TextInt;
            if ((WatermarkType)objSys_ResourceRestrictPic.WatermarkType == WatermarkType.WaterImage && objSys_ResourceRestrictPic.CreateWaterMark)
            {
                if (dropWaterImage.SelectedValue.IsNull())
                {
                    MessageDialog("未选择水印图标");
                    return;

                }
                objSys_ResourceRestrictPic.WaterImageID = dropWaterImage.SelectedValue;
            }
            else
            {
                objSys_ResourceRestrictPic.WaterImageID = "";
            }
            objResourceRule.CurrentEntities.AddTosys_resourcerestrictpic(objSys_ResourceRestrictPic);
            objResourceRule.SaveChanges();
            MessageDialog("新增成功", "ResourceRestrictPicList.aspx?ResourceRestrictID=" + ResourceRestrictID + "&ResourceTypeID=" + ResourceTypeID);

        }
        else
        {
            objSys_ResourceRestrictPic = objResourceRule.Sys_ResourceRestrictPic.First(s => s.ResourceRestrictPicID == ResourceRestrictPicID);
            objSys_ResourceRestrictPic.CreateWaterMark = chkCreateWaterMark.Checked;
            objSys_ResourceRestrictPic.ImageHeight = txtImageHeight.TextInt;
            objSys_ResourceRestrictPic.ImageWidth = txtImageWidth.TextInt;
            objSys_ResourceRestrictPic.WatermarkType = radWatermarkType.SelectValueInt;
            objSys_ResourceRestrictPic.HorizontalAlign = radHorizontalAlign.SelectValueInt;
            objSys_ResourceRestrictPic.VerticalAlign = radVerticalAlign.SelectValueInt;
            objSys_ResourceRestrictPic.WatermarkText = txtWatermarkText.Text;
            objSys_ResourceRestrictPic.VerNo = txtVerNo.TextInt;
            if ((WatermarkType)objSys_ResourceRestrictPic.WatermarkType == WatermarkType.WaterImage && objSys_ResourceRestrictPic.CreateWaterMark)
            {
                if (dropWaterImage.SelectedValue.IsNull())
                {
                    MessageDialog("未选择水印图标");
                    return;

                }
                objSys_ResourceRestrictPic.WaterImageID = dropWaterImage.SelectedValue;
            }
            else
            {
                objSys_ResourceRestrictPic.WaterImageID="";
            }
            objResourceRule.SaveChanges();
            MessageDialog("修改成功", "ResourceRestrictPicList.aspx?ResourceRestrictID=" + ResourceRestrictID + "&ResourceTypeID=" + ResourceTypeID);

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
                Redirect("ResourceRestrictPicList.aspx?ResourceRestrictID=" + ResourceRestrictID + "&ResourceTypeID=" + ResourceTypeID);

                break;
        }
    }

}