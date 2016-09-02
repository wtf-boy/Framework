using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WTF.Framework;
using WTF.DataConfig;
using WTF.DataConfig.Entity;
public partial class ServiceLayer_Theme_ThemeEdit : SupportPageBase
{
    /// <summary>
    /// 获取主题标识
    /// </summary>
    public string ThemeID
    {
        get
        {
            return GetString("ThemeID");

        }

    }
    /// <summary>
    /// 获取主题类型标识
    /// </summary>
    public string ThemeTypeID
    {
        get
        {
            return GetString("ThemeTypeID");

        }

    }
    /// <summary>
    /// 变量
    /// </summary>
    public Sys_Theme objSys_Theme = new Sys_Theme();
    ThemeRule objThemeRule = new ThemeRule();
    /// <summary>
    /// 页面加载
    /// </summary>
    public override void RenderPage()
    {

        if (ThemeID.IsNoNull())
        {
            objSys_Theme = objThemeRule.Sys_Theme.First(s => s.ThemeID == ThemeID);
           

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
        if (ThemeID.IsNull())
        {
            objSys_Theme.ThemeID = Guid.NewGuid().ToString();
            objSys_Theme.ThemeTypeID = ThemeTypeID;
            ///主题名称
            objSys_Theme.ThemeName = txtThemeName.TextCutWord(50);
            ///是否启用
            objSys_Theme.IsEnable =false;
            objThemeRule.InsertTheme(objSys_Theme);

            MessageDialog("新增成功", "ThemeList.aspx?ThemeTypeID=" + ThemeTypeID);
        }
        else
        {
            objSys_Theme = objThemeRule.Sys_Theme.First(p => p.ThemeID == ThemeID);
            ///主题名称
            objSys_Theme.ThemeName = txtThemeName.TextCutWord(50);
            objThemeRule.UpdateTheme(objSys_Theme);
            MessageDialog("修改成功", "ThemeList.aspx?ThemeTypeID=" + ThemeTypeID);
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
                Redirect("ThemeList.aspx?ThemeTypeID=" + ThemeTypeID);
                break;
        }

    }

}