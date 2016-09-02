using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WTF.Resource;
using WTF.Resource.Entity;
using WTF.Framework;
public partial class ServiceLayer_Resource_ResourcePathList : SupportPageBase
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
            return "it.ResourcePathName";
        }
    }

    ResourceRule objResourceRule = new ResourceRule();
    /// <summary>
    /// 页面初始化
    /// </summary>
    public override void RenderPage()
    {
        this.CurrentBindData<Sys_ResourcePath>(gdvContent, objResourceRule.Sys_ResourcePath);
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
                Redirect("ResourcePathEdit.aspx");
                break;
            case "Back":
                Redirect("../../ServiceLayer/Resource/ResourceTypeList.aspx");

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
                Redirect("ResourcePathEdit.aspx?ResourcePathID=" + e.CommandArgument.ToString());
                break;
            case "Remove":

                if (objResourceRule.Sys_ResourceType.Any(s => s.ResourcePathID == e.CommandArgument.ToString()))
                {
                    MessageDialog("对不起此目录已经被资源引用无法删除");
                    return;
                }
                objResourceRule.Sys_ResourcePath.DeleteDataPrimaryKey(e.CommandArgument.ToString());
                MessageDialog("删除成功");
                RenderPage();
                break;

        }
    }


}