using WTF.Theme;
using WTF.Theme.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WTF.Framework;
public partial class ServiceLayer_Module_ThemeEdit : SupportPageBase
{
    /// <summary>
    /// 获取模块主题标识
    /// </summary>
    public string ModuleThemeID
    {
        get
        {
            return GetString("ModuleThemeID");

        }

    }
    /// <summary>
    /// 获取模块类型标识
    /// </summary>
    public string ThemeModuleTypeID
    {
        get
        {
            return GetString("ThemeModuleTypeID");

        }

    }
    /// <summary>
    /// 变量
    /// </summary>
    public Theme_ModuleTheme objTheme_ModuleTheme = new Theme_ModuleTheme();
    public Theme_ModuleThemeInfo objTheme_ModuleThemeInfo = new Theme_ModuleThemeInfo();

    UserThemeRule objUserThemeRule = new UserThemeRule();
    /// <summary>
    /// 页面加载
    /// </summary>
    public override void RenderPage()
    {

        if (ModuleThemeID.IsNoNull())
        {
            objTheme_ModuleThemeInfo = objUserThemeRule.theme_modulethemeinfo.FirstOrDefault(s => s.ModuleThemeID == ModuleThemeID);
            if (CheckEditObjectIsNull(objTheme_ModuleTheme)) return;
          
            Page.DataBind();
        }
        else
        {
        }

    }
    /// <summary>
    /// 保存信息
    /// </summary>
    public void SaveInfo()
    {
        if (ModuleThemeID.IsNull())
        {
            objTheme_ModuleTheme.ModuleThemeID = Guid.NewGuid().ToString();
            objTheme_ModuleTheme.ModuleTypeID = ModuleTypeID;

            ///预览图标
            objTheme_ModuleTheme.PreviewIco = txtPreviewIco.TextCut(500);
            objUserThemeRule.InsertModuleTheme(objTheme_ModuleTheme);
            MessageDialog("新增成功", "ThemeList.aspx?ThemeModuleTypeID=" + ThemeModuleTypeID);
        }
        else
        {
            objTheme_ModuleTheme = objUserThemeRule.theme_moduletheme.FirstOrDefault(p => p.ModuleThemeID == ModuleThemeID);
            if (CheckEditObjectIsNull(objTheme_ModuleTheme)) return;

       
            ///预览图标
            objTheme_ModuleTheme.PreviewIco = txtPreviewIco.TextCut(500);
            objUserThemeRule.UpdateModuleTheme(objTheme_ModuleTheme);
            MessageDialog("修改成功", "ThemeList.aspx?ThemeModuleTypeID=" + ThemeModuleTypeID);
        }
    }

    /// <summary>
    /// 工具栏操作
    /// </summary>
    protected override void CurrentTool_ItemCommand(object sender, WTF.Controls.MyCommandEventArgs e)
    {

        switch (e.CommandName)
        {
            case "Save":
                SaveInfo();
                break;
            case "Back":
                Redirect("ThemeList.aspx?ThemeModuleTypeID=" + ThemeModuleTypeID);
                break;
        }

    }

}