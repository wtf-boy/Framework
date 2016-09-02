using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WTF.Power;
using WTF.Power.Entity;
using WTF.Framework;
using WTF.Resource;
using WTF.Logging;

using WTF.Logging.Entity;
public partial class ServiceLayer_Module_ModuleDataList : SupportPageBase
{
    public string ModuleID
    {
        get
        {
            return GetString("ModuleID");

        }

    }

    public int IsModuleType
    {
        get
        {
            return GetInt("IsModuleType",2);
        }
    }
    public override string Condition
    {
        get
        {
            return "it.ModuleID='" + ModuleID + "'";
        }
    }

    public override string SortExpression
    {
        get
        {
            return "it.DataName";
        }
    }
    ModuleRule objModuleRule = new ModuleRule();
    public override void RenderPage()
    {

        this.CurrentBindData<Sys_ModuleData>(gdvContent, objModuleRule.Sys_ModuleData);
    }
    protected override void CurrentTool_ItemCommand(object sender, WTF.Controls.MyCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "Create":
                Redirect("ModuleDataEdit.aspx?ModuleID=" + ModuleID + "&IsModuleType=" + IsModuleType);
                break;
            case "Remove":
                objModuleRule.DeleteModuleDataByKey(gdvContent.SelectedRowDataKeys);
                RenderPage();
                break;
            case "Back":

                if (IsModuleType == 1)
                {
                    Redirect("ModuleTypeList.aspx");
                }
                else
                {
                    Redirect("ModuleInfo.aspx?ModuleID=" + ModuleID);
                }
                break;


        }


    }
    protected override void CurrentContent_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "Modify":
                RedirectState("ModuleDataEdit.aspx?ModuleID=" + ModuleID + "&ModuleDataID=" + e.CommandArgument.ToString() + "&IsModuleType=" + IsModuleType);
                break;
            case "Remove":
                objModuleRule.DeleteModuleDataByKey(e.CommandArgument.ToString());
                RenderPage();
                break;

        }
    }

}