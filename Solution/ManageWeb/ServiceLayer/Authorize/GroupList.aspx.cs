using WTF.Power;
using WTF.Power.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WTF.Controls;
public partial class ServiceLayer_Authorize_GroupList : SupportPageBase
{
    public override string SortExpression
    {
        get
        {
            return "it.ModuleTypeID,it.IsSupertGroup desc";
        }
    }
    UserRule objUserRule = new UserRule();

    List<Sys_ModuleType> objSys_ModuleTypeList = new List<Sys_ModuleType>();

    public string GetModuleTypenName(string moduleTypeID)
    {
        Sys_ModuleType objSys_ModuleType = objSys_ModuleTypeList.FirstOrDefault(s => s.ModuleTypeID == moduleTypeID);
        return objSys_ModuleType == null ? "未找到平台" : objSys_ModuleType.ModuleTypeName;
    }
    public override void RenderPage()
    {
        objSys_ModuleTypeList = CurrentModuleRule.Sys_ModuleType.ToList();
        this.CurrentBindData<sys_authorizegroup>(gdvContent, objUserRule.sys_authorizegroup);
    }
    protected override void CurrentTool_ItemCommand(object sender, WTF.Controls.MyCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "Create":
                Redirect("GroupEdit.aspx");
                break;
            case "Search":
                SearchCondition();
                break;

        }
    }
    protected override void CurrentContent_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "Modify":
                RedirectState("GroupEdit.aspx?AuthorizeGroupID=" + e.CommandArgument.ToString());
                break;
            case "GroupPower":
                RedirectState("GroupPower.aspx?AuthorizeGroupID=" + e.CommandArgument.ToString());
                break;
            case "RevertPower":


                if (e.CommandExpandArgument() == "0")
                {
                    MessageDialog("系统已经配置不启用回收授权功能因此无法回收");
                    return;
                }
                objUserRule.RevertAuthorizeGroupPower(e.CommandArgument.ToString());
                RenderPage();
                MessageDialog("回收成功");
                break;

            case "Remove":
                objUserRule.DeleteauthorizegroupByKey(e.CommandArgument.ToString());
                RenderPage();
                break;

        }
    }

}