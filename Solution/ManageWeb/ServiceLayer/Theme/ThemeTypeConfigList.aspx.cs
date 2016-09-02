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
public partial class ServiceLayer_Theme_ThemeTypeConfigList : SupportPageBase
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
            return "it.ConfigName";
        }
    }
    ThemeRule objThemeRule = new ThemeRule();
    public override void RenderPage()
    {

        this.CurrentBindData<Sys_ThemeTypeConfig>(gdvContent, objThemeRule.Sys_ThemeTypeConfig);
    }
    protected override void CurrentTool_ItemCommand(object sender, WTF.Controls.MyCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "Create":
                Redirect("ThemeTypeConfigEdit.aspx?ThemeTypeID=" + ThemeTypeID);
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
                RedirectState("ThemeTypeConfigEdit.aspx?ThemeTypeID=" + ThemeTypeID + "&ThemeTypeConfigID=" + e.CommandArgument.ToString());
                break;
            case "Remove":
                objThemeRule.DeleteThemeTypeConfigByKey(e.CommandArgument.ToString());
                RenderPage();
                break;

        }
    }

}