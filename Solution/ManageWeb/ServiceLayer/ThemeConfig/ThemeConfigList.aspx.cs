using WTF.Theme;
using WTF.Theme.Entity;
using WTF.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ServiceLayer_ThemeConfig_ThemeConfigList : SupportPageBase
{
    public override string SortExpression
    {
        get
        {
            return "it.ThemeConfigName";
        }
    }
    UserThemeRule objUserThemeRule = new UserThemeRule();
    public override void RenderPage()
    {

        this.CurrentBindData<Theme_ThemeConfig>(gdvContent, objUserThemeRule.theme_themeconfig);
    }
    protected override void CurrentTool_ItemCommand(object sender, WTF.Controls.MyCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "Create":
                Redirect("ThemeConfigEdit.aspx");
                break;
        }
    }
    protected override void CurrentContent_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "Modify":
                RedirectState("ThemeConfigEdit.aspx?ThemeConfigID=" + e.CommandArgument.ToString());
                break;
            case "Remove":
                objUserThemeRule.DeleteThemeConfigByKey(e.CommandArgument.ToString());
                RenderPage();
                break;

        }
    }

}