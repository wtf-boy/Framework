using WTF.Theme;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WTF.Framework;
using WTF.Theme.Entity;
public partial class ServiceLayer_Module_ThemeImport : SupportPageBase
{
    public string ThemeModuleTypeID
    {

        get
        {
            return GetString("ThemeModuleTypeID");

        }

    }
    UserThemeRule objUserThemeRule = new UserThemeRule();
    public override void RenderPage()
    {
        List<string> ThemeConfigIDList = objUserThemeRule.theme_moduletheme.Where(s => s.ModuleTypeID == ThemeModuleTypeID).Select(s => s.ThemeConfigID).ToList();
        lboxThemeConfig.DataSource = objUserThemeRule.theme_themeconfig.Where(s => !ThemeConfigIDList.Contains(s.ThemeConfigID));
        lboxThemeConfig.DataTextField = "ThemeConfigName";
        lboxThemeConfig.DataValueField = "ThemeConfigID";
        lboxThemeConfig.DataBind();


    }
    protected void Page_Load(object sender, EventArgs e)
    {

        Response.Cache.SetCacheability(HttpCacheability.NoCache);

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {

     
        foreach (ListItem objList in lboxThemeConfig.Items)
        {

            if (objList.Selected)
            {

                string ThemeConfigID = objList.Value;
                Theme_ThemeConfig objTheme_ThemeConfig = objUserThemeRule.theme_themeconfig.First(s => s.ThemeConfigID == ThemeConfigID);

                Theme_ModuleTheme objTheme_ModuleTheme = new Theme_ModuleTheme();
                objTheme_ModuleTheme.ModuleThemeID = Guid.NewGuid().ToString();
                objTheme_ModuleTheme.ThemeConfigID = ThemeConfigID;
                objTheme_ModuleTheme.PreviewIco = objTheme_ThemeConfig.PreviewIco;
                objTheme_ModuleTheme.ModuleTypeID = ThemeModuleTypeID;
                objUserThemeRule.CurrentEntities.AddTotheme_moduletheme(objTheme_ModuleTheme);
            }
        }

        objUserThemeRule.CurrentEntities.SaveChanges();
        DialogOpenerReloadScript(true, "保存成功");
    }
}