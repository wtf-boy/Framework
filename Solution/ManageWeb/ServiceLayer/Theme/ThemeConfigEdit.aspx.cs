using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WTF.Framework;
using WTF.DataConfig;
using WTF.DataConfig.Entity;
public partial class ServiceLayer_Theme_ThemeConfigEdit : SupportPageBase
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
    /// 获取主题标识
    /// </summary>
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
    /// <summary>
    /// 变量
    /// </summary>
    public Sys_ThemeConfig objSys_ThemeConfig = new Sys_ThemeConfig();
    ThemeRule objThemeRule = new ThemeRule();
    /// <summary>
    /// 页面加载
    /// </summary>
    public override void RenderPage()
    {

        if (ThemeConfigID.IsNoNull())
        {
            objSys_ThemeConfig = objThemeRule.Sys_ThemeConfig.First(s => s.ThemeConfigID == ThemeConfigID);

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
            objSys_ThemeConfig.ThemeConfigID = Guid.NewGuid().ToString();
            objSys_ThemeConfig.ThemeID = ThemeID;
            ///主题配置标识
            objSys_ThemeConfig.ThemeTypeConfigID ="";
            ///配置名称
            objSys_ThemeConfig.ConfigName = txtConfigName.TextCutWord(50);
            ///配置键
            objSys_ThemeConfig.ConfigKey = txtConfigKey.TextCut(256);
            ///配置数据
            objSys_ThemeConfig.ConfigValue = txtConfigValue.TextCut(3000);
            ///配置备注
            objSys_ThemeConfig.ConfigRemark = txtConfigRemark.TextCutWord(200);
            objThemeRule.InsertThemeConfig(objSys_ThemeConfig);
            MessageDialog("新增成功", "ThemeConfigList.aspx?ThemeTypeID=" + ThemeTypeID + "&ThemeID=" + ThemeID);
        }
        else
        {
            objSys_ThemeConfig = objThemeRule.Sys_ThemeConfig.First(p => p.ThemeConfigID == ThemeConfigID);

            ///配置名称
            objSys_ThemeConfig.ConfigName = txtConfigName.TextCutWord(50);
            ///配置键
            objSys_ThemeConfig.ConfigKey = txtConfigKey.TextCut(256);
            ///配置数据
            objSys_ThemeConfig.ConfigValue = txtConfigValue.TextCut(3000);
            ///配置备注
            objSys_ThemeConfig.ConfigRemark = txtConfigRemark.TextCutWord(200);
            objThemeRule.UpdateThemeConfig(objSys_ThemeConfig);
            MessageDialog("修改成功", "ThemeConfigList.aspx?ThemeTypeID=" + ThemeTypeID + "&ThemeID=" + ThemeID);
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
                Redirect("ThemeConfigList.aspx?ThemeTypeID=" + ThemeTypeID + "&ThemeID=" + ThemeID);
                break;
        }

    }

}