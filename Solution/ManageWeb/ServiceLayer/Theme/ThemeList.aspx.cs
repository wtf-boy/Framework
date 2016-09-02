using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using WTF.Framework;
using WTF.DataConfig;
using WTF.DataConfig.Entity;
public partial class ServiceLayer_Theme_ThemeList : SupportPageBase
{
    public string ThemeTypeID
    {
        get
        {
            return GetString("ThemeTypeID");

        }

    }
    public override string Condition
    {
        get
        {
            return "it.ThemeTypeID='" + ThemeTypeID + "'";
        }
    }
    public override string SortExpression
    {
        get
        {
            return "it.ThemeName";
        }
    }
    ThemeRule objThemeRule = new ThemeRule();
    public override void RenderPage()
    {

        this.CurrentBindData<Sys_Theme>(gdvContent, objThemeRule.Sys_Theme);
    }
    protected override void CurrentTool_ItemCommand(object sender, WTF.Controls.MyCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "Create":
                Redirect("ThemeEdit.aspx?ThemeTypeID=" + ThemeTypeID);
                break;
            case "Back":
                Redirect("ThemeTypeList.aspx?ThemeTypeID=" + ThemeTypeID);
                break;


        }



    }
    protected override void CurrentContent_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "Modify":
                RedirectState("ThemeEdit.aspx?ThemeTypeID=" + ThemeTypeID + "&ThemeID=" + e.CommandArgument.ToString());
                break;
            case "ThemeStart":
                objThemeRule.StartTheme(ThemeTypeID, e.CommandArgument.ToString());
                RenderPage();
                break;
            case "Remove":
                objThemeRule.DeleteThemeByKey(e.CommandArgument.ToString());
                RenderPage();
                break;
            case "Config":
                Redirect("ThemeConfigList.aspx?ThemeTypeID=" + ThemeTypeID + "&ThemeID=" + e.CommandArgument.ToString());
                break;

        }
    }

}