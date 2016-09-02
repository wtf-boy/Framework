using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WTF.Power;
using WTF.Power.Entity;
using WTF.Framework;
using WTF.Logging;

using WTF.Logging.Entity;
public partial class ServiceLayer_Module_ModuleCoteEdit : SupportPageBase
{
    /// <summary>
    /// 获取栏目标识
    /// </summary>
    public int ModuleCoteID
    {
        get
        {
            return GetInt("ModuleCoteID");

        }

    }
    /// <summary>
    /// 变量
    /// </summary>
    public Sys_ModuleCote objSys_ModuleCote = new Sys_ModuleCote();
    ModuleRule objModuleRule = new ModuleRule();
    /// <summary>
    /// 页面加载
    /// </summary>
    public override void RenderPage()
    {

        if (ModuleCoteID.IsNoNull())
        {
            objSys_ModuleCote = objModuleRule.Sys_ModuleCote.First(s => s.ModuleCoteID == ModuleCoteID);
            chkIsParentUrl.Checked = objSys_ModuleCote.IsParentUrl;
            radIDDataType.SelectedValue = objSys_ModuleCote.IDDataType.ToString();
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
        if (ModuleCoteID.IsNull())
        {
            ///栏目名称
            objSys_ModuleCote.CoteTitle = txtCoteTitle.TextCutWord(50);

            objSys_ModuleCote.IsParentUrl = chkIsParentUrl.Checked;
            objSys_ModuleCote.Name = txtName.TextCut(100);
            ///栏目表名
            objSys_ModuleCote.CoteTableName = txtCoteTableName.TextCut(100);
            ///ID字段名
            objSys_ModuleCote.IDName = txtIDName.TextCut(100);
            ///父节点字段名
            objSys_ModuleCote.ParentIDName = txtParentIDName.TextCut(100);
            ///ID路经名称
            objSys_ModuleCote.IDPathName = txtIDPathName.TextCut(100);
            ///连接字符串名
            objSys_ModuleCote.ConnectionStringName = txtConnectionStringName.TextCut(200);
            ///根节点ID值
            objSys_ModuleCote.RootIDValue = txtRootIDValue.TextCut(200);
            ///ID类型1整型2字符串
            objSys_ModuleCote.IDDataType = radIDDataType.SelectValueInt;

            objSys_ModuleCote.Condtion = txtCondition.TextCutWord(200);
            objSys_ModuleCote.SortExpression = txtSortExpression.TextCutWord(200);
            objModuleRule.InsertModuleCote(objSys_ModuleCote);
            MessageDialog("新增成功", "ModuleCoteList.aspx");
        }
        else
        {
            objSys_ModuleCote = objModuleRule.Sys_ModuleCote.First(p => p.ModuleCoteID == ModuleCoteID);
            ///栏目名称
            objSys_ModuleCote.CoteTitle = txtCoteTitle.TextCutWord(50);
            objSys_ModuleCote.IsParentUrl = chkIsParentUrl.Checked;
            objSys_ModuleCote.Name = txtName.TextCut(100);

            ///栏目表名
            objSys_ModuleCote.CoteTableName = txtCoteTableName.TextCut(100);
            ///ID字段名
            objSys_ModuleCote.IDName = txtIDName.TextCut(100);
            ///父节点字段名
            objSys_ModuleCote.ParentIDName = txtParentIDName.TextCut(100);
            ///ID路经名称
            objSys_ModuleCote.IDPathName = txtIDPathName.TextCut(100);
            ///连接字符串名
            objSys_ModuleCote.ConnectionStringName = txtConnectionStringName.TextCut(200);
            ///根节点ID值
            objSys_ModuleCote.RootIDValue = txtRootIDValue.TextCut(200);
            ///ID类型1整型2字符串
            objSys_ModuleCote.IDDataType = radIDDataType.SelectValueInt;

            objSys_ModuleCote.Condtion = txtCondition.TextCutWord(200);
            objSys_ModuleCote.SortExpression = txtSortExpression.TextCutWord(200);

            objModuleRule.UpdateModuleCote(objSys_ModuleCote);
            MessageDialog("修改成功", "ModuleCoteList.aspx");
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
                Redirect("ModuleCoteList.aspx");
                break;
        }

    }

}