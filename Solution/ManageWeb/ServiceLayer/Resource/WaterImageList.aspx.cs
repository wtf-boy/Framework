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
public partial class ServiceLayer_Resource_WaterImageList : SupportPageBase
{

    public override string Condition
    {
        get
        {
            return "";
        }
    }

    public override string SortExpression
    {
        get
        {
            return "it.WaterName";
        }
    }
    ResourceRule objResourceRule = new ResourceRule();
    /// <summary>
    /// 页面初始化
    /// </summary>
    public override void RenderPage()
    {
        this.CurrentBindData<Sys_WaterImage>(gdvContent, objResourceRule.Sys_WaterImage);
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
            case "Create":
                ResponseHelper.Redirect("WaterImageEdit.aspx");
                break;
            case "Search":
                SearchCondition();
                break;

            case "Back":
                ResponseHelper.Redirect("ResourceTypeList.aspx");
                break;
        }
    }

    /// <summary>
    /// 列表命令列表
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected override void CurrentContent_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        switch (e.CommandName)
        {

            case "Modify":
                Redirect("WaterImageEdit.aspx?WaterImageID=" + e.CommandArgument.ToString());
                break;
            case "Remove":

                string WaterImageID = e.CommandArgument.ToString();
                if (objResourceRule.Sys_ResourceRestrictPic.Any(s => s.WaterImageID == WaterImageID))
                {
                    MessageDialog("此水印有存在使用，无法删除 ");
                    return;
                }

                objResourceRule.Sys_WaterImage.DeleteDataPrimaryKey(WaterImageID.ToString());
                objResourceRule.DeleteResource(WaterImageID);
                MessageDialog("删除成功");
                RenderPage();
                break;

        }
    }

}