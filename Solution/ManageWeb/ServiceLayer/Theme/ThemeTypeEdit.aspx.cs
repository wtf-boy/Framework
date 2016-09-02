using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WTF.Framework;
using WTF.DataConfig;
using WTF.DataConfig.Entity;
public partial class ServiceLayer_Theme_ThemeTypeEdit : SupportPageBase
{
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
    public Sys_ThemeType objSys_ThemeType = new Sys_ThemeType();
    ThemeRule objThemeRule = new ThemeRule();
    /// <summary>
    /// 页面加载
    /// </summary>
    public override void RenderPage()
    {

        if (ThemeTypeID.IsNoNull())
        {
            objSys_ThemeType = objThemeRule.Sys_ThemeType.First(s => s.ThemeTypeID == ThemeTypeID);

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
        if (ThemeTypeID.IsNull())
        {
            objSys_ThemeType.ThemeTypeID = Guid.NewGuid().ToString();
            ///主题类型名称
            objSys_ThemeType.ThemeTypeName = txtThemeTypeName.TextCutWord(50);
            ///主题类型代码
            objSys_ThemeType.ThemeTypeCode = txtThemeTypeCode.TextCut(100);
            ///主题类型备注
            objSys_ThemeType.Remark = txtRemark.TextCutWord(100);
            objThemeRule.InsertThemeType(objSys_ThemeType);
            MessageDialog("新增成功", "ThemeTypeList.aspx");
        }
        else
        {
            objSys_ThemeType = objThemeRule.Sys_ThemeType.First(p => p.ThemeTypeID == ThemeTypeID);
            ///主题类型名称
            objSys_ThemeType.ThemeTypeName = txtThemeTypeName.TextCutWord(50);
            ///主题类型代码
            objSys_ThemeType.ThemeTypeCode = txtThemeTypeCode.TextCut(100);
            ///主题类型备注
            objSys_ThemeType.Remark = txtRemark.TextCutWord(100);
            objThemeRule.UpdateThemeType(objSys_ThemeType);
            MessageDialog("修改成功", "ThemeTypeList.aspx");
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
                Redirect("ThemeTypeList.aspx");
                break;
        }

    }

}