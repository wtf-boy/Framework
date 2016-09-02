using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WTF.Framework;
using WTF.DataConfig;
using WTF.DataConfig.Entity;
public partial class ServiceLayer_Theme_ThemeConfigList : SupportPageBase
{
    public string ThemeID
    {
        get
        {
            return GetString("ThemeID");

        }

    }

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
            return "it.ThemeID='" + ThemeID + "'";
        }
    }
    public override string SortExpression
    {
        get
        {
            return "it.ConfigName";
        }
    }
    ThemeRule objThemeRule = new ThemeRule();
    public override void RenderPage()
    {

        this.CurrentBindData<Sys_ThemeConfig>(gdvContent, objThemeRule.Sys_ThemeConfig);
    }
    protected override void CurrentTool_ItemCommand(object sender, WTF.Controls.MyCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "Create":
                Redirect("ThemeConfigEdit.aspx?ThemeTypeID=" + ThemeTypeID + "&ThemeID=" + ThemeID);
                break;
            case "Back":
                Redirect("ThemeList.aspx?ThemeTypeID=" + ThemeTypeID);
                break;


        }



    }
    protected override void CurrentContent_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "Modify":
                RedirectState("ThemeConfigEdit.aspx?ThemeTypeID=" + ThemeTypeID + "&ThemeID=" + ThemeID + "&ThemeConfigID=" + e.CommandArgument.ToString());
                break;
            case "Remove":
                objThemeRule.DeleteThemeConfigByKey(e.CommandArgument.ToString());
                RenderPage();
                break;

        }
    }

}