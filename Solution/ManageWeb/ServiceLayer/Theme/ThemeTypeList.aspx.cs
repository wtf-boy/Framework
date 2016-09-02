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
public partial class ServiceLayer_Theme_ThemeTypeList : SupportPageBase
{
    public override string SortExpression
    {
        get
        {
            return "it.ThemeTypeName";
        }
    }
    ThemeRule objThemeRule = new ThemeRule();
    public override void RenderPage()
    {

        this.CurrentBindData<Sys_ThemeType>(gdvContent, objThemeRule.Sys_ThemeType);
    }
    protected override void CurrentTool_ItemCommand(object sender, WTF.Controls.MyCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "Create":
                Redirect("ThemeTypeEdit.aspx");
                break;


        }



    }
    protected override void CurrentContent_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "Modify":
                RedirectState("ThemeTypeEdit.aspx?ThemeTypeID=" + e.CommandArgument.ToString());
                break;
            case "Remove":
                objThemeRule.DeleteThemeTypeByKey(e.CommandArgument.ToString());
                RenderPage();
                break;
            case "Theme":
                Redirect("ThemeList.aspx?ThemeTypeID=" + e.CommandArgument.ToString());
                break;
            case "Config":
                Redirect("ThemeTypeConfigList.aspx?ThemeTypeID=" + e.CommandArgument.ToString());
                break;

        }
    }

}