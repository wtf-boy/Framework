using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WTF.Framework;
using WTF.Theme;
using WTF.Theme.Entity;
public partial class ServiceLayer_Module_ThemeList : SupportPageBase
{
    public Guid ThemeModuleTypeID
    {
        get
        {
            return GetGuid("ThemeModuleTypeID");

        }

    }
    public override string Condition
    {
        get
        {
            return "it.ModuleTypeID='" + ThemeModuleTypeID + "'";
        }
    }
    public override string SortExpression
    {
        get
        {
            return "it.ThemeConfigName desc";
        }
    }
    UserThemeRule objUserThemeRule = new UserThemeRule();

    public string GetThemePreviewIco(object PreviewIco)
    {
        Theme_ModuleThemeInfo objTheme_ModuleThemeInfo = (Theme_ModuleThemeInfo)PreviewIco;
        return SysVariable.ApplicationPath + "/DeskTop/" + objTheme_ModuleThemeInfo.LayoutPath + "/" + (objTheme_ModuleThemeInfo.PreviewIco.IsNoNull() ? objTheme_ModuleThemeInfo.PreviewIco : "Preview.jpg");
    }
    public override void RenderPage()
    {

        this.CurrentBindData<Theme_ModuleThemeInfo>(gdvContent, objUserThemeRule.theme_modulethemeinfo);
    }
    protected override void CurrentTool_ItemCommand(object sender, WTF.Controls.MyCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "ThemeImport":

                RenderPage();
                break;
            case "Back":
                Redirect("../../ServiceLayer/Module/ModuleTypeList.aspx");
                break;


        }



    }
    protected override void CurrentContent_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "Modify":
                RedirectState("ThemeEdit.aspx?ThemeModuleTypeID=" + ThemeModuleTypeID + "&ModuleThemeID=" + e.CommandArgument.ToString());
                break;
            case "Remove":
                objUserThemeRule.DeleteModuleThemeByKey(e.CommandArgument.ToString().ConvertGuid());
                RenderPage();
                break;

        }
    }

}