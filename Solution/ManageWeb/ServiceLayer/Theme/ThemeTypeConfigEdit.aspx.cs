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
public partial class ServiceLayer_Theme_ThemeTypeConfigEdit : SupportPageBase
{
    /// <summary>
    /// 获取主题配置标识
    /// </summary>
    public string ThemeTypeConfigID
    {
        get
        {
            return GetString("ThemeTypeConfigID");

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
    public Sys_ThemeTypeConfig objSys_ThemeTypeConfig = new Sys_ThemeTypeConfig();
    ThemeRule objThemeRule = new ThemeRule();
    /// <summary>
    /// 页面加载
    /// </summary>
    public override void RenderPage()
    {

        if (ThemeTypeConfigID.IsNoNull())
        {
            objSys_ThemeTypeConfig = objThemeRule.Sys_ThemeTypeConfig.First(s => s.ThemeTypeConfigID == ThemeTypeConfigID);

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
        if (ThemeTypeConfigID.IsNull())
        {
            objSys_ThemeTypeConfig.ThemeTypeConfigID = Guid.NewGuid().ToString();
            objSys_ThemeTypeConfig.ThemeTypeID = ThemeTypeID;
            ///配置名称
            objSys_ThemeTypeConfig.ConfigName = txtConfigName.TextCutWord(50);
            ///配置键
            objSys_ThemeTypeConfig.ConfigKey = txtConfigKey.TextCut(256);
            ///配置数据
            objSys_ThemeTypeConfig.ConfigValue = txtConfigValue.TextCut(3000);
            ///配置备注
            objSys_ThemeTypeConfig.ConfigRemark = txtConfigRemark.TextCutWord(200);
            objThemeRule.InsertThemeTypeConfig(objSys_ThemeTypeConfig, chkConfig.Checked);
            MessageDialog("新增成功", "ThemeTypeConfigList.aspx?ThemeTypeID=" + ThemeTypeID);
        }
        else
        {
            objSys_ThemeTypeConfig = objThemeRule.Sys_ThemeTypeConfig.First(p => p.ThemeTypeConfigID == ThemeTypeConfigID);
            ///配置名称
            objSys_ThemeTypeConfig.ConfigName = txtConfigName.TextCutWord(50);
            ///配置键
            objSys_ThemeTypeConfig.ConfigKey = txtConfigKey.TextCut(256);
            ///配置数据
            objSys_ThemeTypeConfig.ConfigValue = txtConfigValue.TextCut(3000);
            ///配置备注
            objSys_ThemeTypeConfig.ConfigRemark = txtConfigRemark.TextCutWord(200);
            objThemeRule.UpdateThemeTypeConfig(objSys_ThemeTypeConfig, chkConfig.Checked);
            MessageDialog("修改成功", "ThemeTypeConfigList.aspx?ThemeTypeID=" + ThemeTypeID);
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
                Redirect("ThemeTypeConfigList.aspx?ThemeTypeID=" + ThemeTypeID);
                break;
        }

    }

}