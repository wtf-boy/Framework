using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using WTF.Power;
using WTF.Framework;
using WTF.Power.Entity;
public partial class ServiceLayer_Module_ModuleCoteList : SupportPageBase
{
    public override string SortExpression
    {
        get
        {
            return "it.ModuleCoteID";
        }
    }
    ModuleRule objModuleRule = new ModuleRule();
    public override void RenderPage()
    {

        this.CurrentBindData<Sys_ModuleCote>(gdvContent, objModuleRule.Sys_ModuleCote);
    }
    protected override void CurrentTool_ItemCommand(object sender, WTF.Controls.MyCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "Create":
                Redirect("ModuleCoteEdit.aspx");
                break;
            case "Back":
                Redirect("ModuleTypeList.aspx");
                break;


        }



    }
    protected override void CurrentContent_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "Modify":
                Redirect("ModuleCoteEdit.aspx?ModuleCoteID=" + e.CommandArgument.ToString());
                break;
            case "Remove":
                objModuleRule.DeleteModuleCoteByKey(e.CommandArgument.ToString());
                RenderPage();
                break;

        }
    }

}