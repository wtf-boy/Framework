using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WTF.Framework;
using WTF.Resource;
using WTF.Resource.Entity;
public partial class ServiceLayer_Resource_ResourceRestrictPicList : SupportPageBase
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

    public override string Condition
    {
        get
        {
            return "it.ResourceRestrictID=" + ResourceRestrictID;
        }
    }

    public override string SortExpression
    {
        get
        {
            return "it.VerNo";
        }
    }
    ResourceRule objResourceRule = new ResourceRule();
    /// <summary>
    /// 页面初始化
    /// </summary>
    public override void RenderPage()
    {
        this.CurrentBindData<Sys_ResourceRestrictPic>(gdvContent, objResourceRule.Sys_ResourceRestrictPic);
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
                Redirect("ResourceRestrictPicEdit.aspx?ResourceRestrictID=" + ResourceRestrictID + "&ResourceTypeID=" + ResourceTypeID);
                break;
            case "Remove":
                objResourceRule.Sys_ResourceRestrictPic.DeleteDataPrimaryKey(gdvContent.SelectedRowDataKeys);
                MessageDialog("删除成功");
                RenderPage();
                break;
            case "Search":
                SearchCondition();
                break;
            case "Back":
                Redirect("ResourceRestrictList.aspx?ResourceTypeID=" + ResourceTypeID);
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
                Redirect("ResourceRestrictPicEdit.aspx?ResourceRestrictID=" + ResourceRestrictID + "&ResourceTypeID=" + ResourceTypeID+"&ResourceRestrictPicID=" + e.CommandArgument.ToString());
                break;
            case "Remove":
                objResourceRule.Sys_ResourceRestrictPic.DeleteDataPrimaryKey(e.CommandArgument.ToString());
                MessageDialog("删除成功");
                RenderPage();
                break;

        }
    }

}