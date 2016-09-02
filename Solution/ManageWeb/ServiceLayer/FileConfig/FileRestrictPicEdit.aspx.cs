using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WTF.Framework;
using WTF.Resource.Entity;
using WTF.Resource;
using WTF.Controls;
public partial class ServiceLayer_FileConfig_FileRestrictPicEdit : SupportPageBase
{
    /// <summary>
    /// 获取文件系统图片标识
    /// </summary>
    public string SystemFilePicID
    {
        get
        {
            return GetString("SystemFilePicID");

        }

    }

    public string FileResourceID
    {
        get
        {
            return GetString("FileResourceID");

        }
    }
    /// <summary>
    /// 获取存储路经标识
    /// </summary>
    public string FileRestrictID
    {
        get
        {
            return GetString("FileRestrictID");

        }

    }


    public override void InitDataPage()
    {
        ResourceRule objResourceRule = new ResourceRule();
        radWaterImageID.DataSource = objResourceRule.Sys_WaterImage;
        radWaterImageID.DataTextField = "WaterName";
        radWaterImageID.DataValueField = "WaterImageID";
        radWaterImageID.DataBind();
        radWatermarkType.Items.Clear();
        foreach (var EnumMember in typeof(WatermarkType).GetEnumMembers())
        {
            radWatermarkType.Items.Add(new ListItem(EnumMember.Description, EnumMember.Value.ToString()));
        }

    }
    /// <summary>
    /// 变量
    /// </summary>
    public resource_filerestrictpic objresource_filerestrictpic = new resource_filerestrictpic();
    FileResourceRule objFileResourceRule = new FileResourceRule();
    /// <summary>
    /// 页面加载
    /// </summary>
    public override void RenderPage()
    {

        if (SystemFilePicID.IsNoNull())
        {
            objresource_filerestrictpic = objFileResourceRule.resource_filerestrictpic.FirstOrDefault(s => s.SystemFilePicID == SystemFilePicID);
            if (CheckEditObjectIsNull(objresource_filerestrictpic)) return;
            ///是否创建水印
            chkIsCreateWaterMark.Checked = objresource_filerestrictpic.IsCreateWaterMark;
            ///水平位置
            radHorizontalAlign.SelectedValue = objresource_filerestrictpic.HorizontalAlign.ToString();
            ///垂直位置
            radVerticalAlign.SelectedValue = objresource_filerestrictpic.VerticalAlign.ToString();
            ///水印类型
            radWatermarkType.SelectedValue = objresource_filerestrictpic.WatermarkType.ToString();


            if ((WatermarkType)objresource_filerestrictpic.WatermarkType == WatermarkType.WaterImage && objresource_filerestrictpic.IsCreateWaterMark)
            {
                ///水印图标
                radWaterImageID.SelectedValue = objresource_filerestrictpic.WaterImageID.ToString();
            }

            Page.DataBind();
        }
        else
        {

            chkIsCreateWaterMark.Checked = false;

            radWatermarkType.SelectedValue = "1";
            radHorizontalAlign.SelectedValue = "3";
            radVerticalAlign.SelectedValue = "3";
        }

    }
    /// <summary>
    /// 保存信息
    /// </summary>
    public void SaveInfo()
    {
        if (SystemFilePicID.IsNull())
        {
            objresource_filerestrictpic.SystemFilePicID = Guid.NewGuid().ToString();
            objresource_filerestrictpic.FileRestrictID = FileRestrictID;
            ///创建排序号
            objresource_filerestrictpic.SortIndex = txtSortIndex.TextInt;
            ///是否创建水印
            objresource_filerestrictpic.IsCreateWaterMark = chkIsCreateWaterMark.Checked;
            ///水印文字
            objresource_filerestrictpic.WatermarkText = txtWatermarkText.TextCutWord(250);
            ///水平位置
            objresource_filerestrictpic.HorizontalAlign = radHorizontalAlign.SelectValueInt;
            ///垂直位置
            objresource_filerestrictpic.VerticalAlign = radVerticalAlign.SelectValueInt;
            ///图片宽度
            objresource_filerestrictpic.ImageWidth = txtImageWidth.TextInt;
            ///图片高度
            objresource_filerestrictpic.ImageHeight = txtImageHeight.TextInt;
            ///水印类型
            objresource_filerestrictpic.WatermarkType = radWatermarkType.SelectValueInt;


            if ((WatermarkType)objresource_filerestrictpic.WatermarkType == WatermarkType.WaterImage && objresource_filerestrictpic.IsCreateWaterMark)
            {
                if (radWaterImageID.SelectedValue.IsNull())
                {
                    MessageDialog("未选择水印图标");
                    return;

                }
                ///水印图标
                objresource_filerestrictpic.WaterImageID = radWaterImageID.SelectedValue;
            }
            else
            {
                objresource_filerestrictpic.WaterImageID = "";
            }

            objFileResourceRule.Insertfilerestrictpic(objresource_filerestrictpic);
            MessageDialog("新增成功", "FileRestrictPicList.aspx?FileResourceID=" + FileResourceID + "&FileRestrictID=" + FileRestrictID);
        }
        else
        {
            objresource_filerestrictpic = objFileResourceRule.resource_filerestrictpic.FirstOrDefault(p => p.SystemFilePicID == SystemFilePicID);
            if (CheckEditObjectIsNull(objresource_filerestrictpic)) return;
            objresource_filerestrictpic.FileRestrictID = FileRestrictID;
            ///创建排序号
            objresource_filerestrictpic.SortIndex = txtSortIndex.TextInt;
            ///是否创建水印
            objresource_filerestrictpic.IsCreateWaterMark = chkIsCreateWaterMark.Checked;
            ///水印文字
            objresource_filerestrictpic.WatermarkText = txtWatermarkText.TextCutWord(250);
            ///水平位置
            objresource_filerestrictpic.HorizontalAlign = radHorizontalAlign.SelectValueInt;
            ///垂直位置
            objresource_filerestrictpic.VerticalAlign = radVerticalAlign.SelectValueInt;
            ///图片宽度
            objresource_filerestrictpic.ImageWidth = txtImageWidth.TextInt;
            ///图片高度
            objresource_filerestrictpic.ImageHeight = txtImageHeight.TextInt;
            ///水印类型
            objresource_filerestrictpic.WatermarkType = radWatermarkType.SelectValueInt;
            ///水印图标
            objresource_filerestrictpic.WaterImageID = radWaterImageID.SelectedValue;
            objFileResourceRule.Updatefilerestrictpic(objresource_filerestrictpic);
            MessageDialog("修改成功", "FileRestrictPicList.aspx?FileResourceID=" + FileResourceID + "&FileRestrictID=" + FileRestrictID);
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
                Redirect("FileRestrictPicList.aspx?FileResourceID=" + FileResourceID + "&FileRestrictID=" + FileRestrictID);
                break;
        }

    }

}