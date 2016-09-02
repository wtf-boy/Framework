using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WTF.Framework;
using WTF.Resource;
using WTF.Resource.Entity;
public partial class ServiceLayer_Resource_ResourceRestrictList : SupportPageBase
{

    public override string Condition
    {
        get
        {
            return "it.ResourceTypeID=" + ResourceTypeID;
        }
    }

    public override string SortExpression
    {
        get
        {
            return "it.RestrictCode";
        }
    }

    public int ResourceTypeID
    {
        get
        {
            return GetInt("ResourceTypeID");
        }
    }
    ResourceRule objResourceRule = new ResourceRule();
    /// <summary>
    /// 页面初始化
    /// </summary>
    public override void RenderPage()
    {
        this.CurrentBindData<Sys_ResourceRestrict>(gdvContent, objResourceRule.Sys_ResourceRestrict);
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
                Redirect("ResourceRestrictEdit.aspx?ResourceTypeID=" + ResourceTypeID);
                break;
            case "Remove":
                objResourceRule.Sys_ResourceRestrict.DeleteDataPrimaryKey(gdvContent.SelectedRowDataKeys);
                MessageDialog("删除成功");
                RenderPage();
                break;
            case "Search":
                SearchCondition();
                break;
            case "Back":
                Redirect("ResourceTypeList.aspx");
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
                Redirect("ResourceRestrictEdit.aspx?ResourceTypeID=" + ResourceTypeID + "&ResourceRestrictID=" + e.CommandArgument.ToString());
                break;
            case "Remove":
                objResourceRule.Sys_ResourceRestrict.DeleteDataPrimaryKey(e.CommandArgument.ToString());
                MessageDialog("删除成功");
                RenderPage();
                break;
            case "RestrictPic":
                Redirect("ResourceRestrictPicList.aspx?ResourceTypeID=" + ResourceTypeID + "&ResourceRestrictID=" + e.CommandArgument.ToString());
                break;
                

        }
    }

    public string GetRestrictType(int RestrictType)
    {
        switch (RestrictType)
        {
            case 1:
                return "文件";
                break;
            case 2:
                return "图片";
                break;
            case 3:
                return "Flash";
                break;
            case 4:
                return "多媒体";
                break;
        }
        return "";

    }

}