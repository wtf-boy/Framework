using WTF.Theme;
using WTF.Theme.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WTF.Framework;
public partial class ServiceLayer_ThemeConfig_ThemeConfigEdit : SupportPageBase
{
    /// <summary>
    /// 获取主题配置标识
    /// </summary>
    public string ThemeConfigID
    {
        get
        {
            return GetString("ThemeConfigID");

        }

    }
    /// <summary>
    /// 变量
    /// </summary>
    public Theme_ThemeConfig objTheme_ThemeConfig = new Theme_ThemeConfig();
    UserThemeRule objUserThemeRule = new UserThemeRule();
    /// <summary>
    /// 页面加载
    /// </summary>
    public override void RenderPage()
    {

        if (ThemeConfigID.IsNoNull())
        {
            objTheme_ThemeConfig = objUserThemeRule.theme_themeconfig.FirstOrDefault(s => s.ThemeConfigID == ThemeConfigID);
            if (CheckEditObjectIsNull(objTheme_ThemeConfig)) return;

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
        if (ThemeConfigID.IsNull())
        {
            objTheme_ThemeConfig.ThemeConfigID = Guid.NewGuid().ToString();
            ///主题名称
            objTheme_ThemeConfig.ThemeConfigName = txtThemeConfigName.TextCutWord(50);
            ///主题样式
            objTheme_ThemeConfig.Theme = txtTheme.TextCut(100);
            ///布局样式
            objTheme_ThemeConfig.LayoutPath = txtLayoutPath.TextCut(100);
            objTheme_ThemeConfig.PreviewIco = txtPreviewIco.TextCut(500);
            objUserThemeRule.InsertThemeConfig(objTheme_ThemeConfig);
            MessageDialog("新增成功", "ThemeConfigList.aspx");
        }
        else
        {
            objTheme_ThemeConfig = objUserThemeRule.theme_themeconfig.FirstOrDefault(p => p.ThemeConfigID == ThemeConfigID);
            if (CheckEditObjectIsNull(objTheme_ThemeConfig)) return;
            ///主题名称
            objTheme_ThemeConfig.ThemeConfigName = txtThemeConfigName.TextCutWord(50);
            ///主题样式
            objTheme_ThemeConfig.Theme = txtTheme.TextCut(100);
            ///布局样式
            objTheme_ThemeConfig.LayoutPath = txtLayoutPath.TextCut(100);
            objTheme_ThemeConfig.PreviewIco = txtPreviewIco.TextCut(500);
            objUserThemeRule.UpdateThemeConfig(objTheme_ThemeConfig);
            MessageDialog("修改成功", "ThemeConfigList.aspx");
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
                Redirect("ThemeConfigList.aspx");
                break;
        }

    }

}