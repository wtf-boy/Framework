using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WTF.Resource;
using WTF.Framework;
using WTF.Logging;
using WTF.Logging.Entity;

public partial class ServiceLayer_Loging_ModuleTypeList : SupportPageBase
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
            return "it.ModuleTypeID";
        }
    }

    LogRule objLogRule = new LogRule();
    /// <summary>
    /// 页面初始化
    /// </summary>
    public override void RenderPage()
    {
        this.CurrentBindData<loger_moduletype>(gdvContent, objLogRule.loger_moduletype);



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
            case "UpdateModule":
                objLogRule.UpdateModuleType();
                MessageDialog("更新成功", "ModuleTypeList.aspx");
                break;
            case "Create":

                Redirect("ModuleTypeEdit.aspx");
                break;
            case "Search":
                SearchCondition();
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
                Redirect("ModuleTypeEdit.aspx?ModuleTypeID=" + e.CommandArgument.ToString());
                break;
            case "Remove":
                objLogRule.loger_moduletype.DeleteDataPrimaryKey(e.CommandArgument.ToString());
                RenderPage();
                break;


        }
    }

}