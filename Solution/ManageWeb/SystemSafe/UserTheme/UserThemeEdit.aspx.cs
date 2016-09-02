using WTF.Theme;
using WTF.Theme.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WTF.Framework;
public partial class SystemSafe_UserTheme_UserThemeEdit : SupportPageBase
{

    /// <summary>
    /// 变量
    /// </summary>
    public Theme_UserThemeInfo objTheme_UserThemeInfo = new Theme_UserThemeInfo();
    UserThemeRule objUserThemeRule = new UserThemeRule();


    public string GetThemePreviewIco(object PreviewIco)
    {
        Theme_ModuleThemeInfo objTheme_ModuleThemeInfo = (Theme_ModuleThemeInfo)PreviewIco;
        return SysVariable.ApplicationPath + "/DeskTop/" + objTheme_ModuleThemeInfo.LayoutPath + "/" + (objTheme_ModuleThemeInfo.PreviewIco.IsNoNull() ? objTheme_ModuleThemeInfo.PreviewIco : "Preview.jpg");
    }
    /// <summary>
    /// 页面加载
    /// </summary>
    public override void RenderPage()
    {


        rptThemeList.DataSource = objUserThemeRule.theme_modulethemeinfo.Where(s => s.ModuleTypeID == ModuleTypeID);
        rptThemeList.DataBind();

        objTheme_UserThemeInfo = objUserThemeRule.theme_userthemeinfo.FirstOrDefault(s => s.UserID == CurrentUser.UserID);

        if (objTheme_UserThemeInfo != null)
        {

            radOperateStyle.SelectedValue = objTheme_UserThemeInfo.OperateStyle.ToString();
            hidModuleThemeID.Value = objTheme_UserThemeInfo.ModuleThemeID.ToString();
            Page.DataBind();
        }
        else
        {
            MyTopToolBar.CommandHidden = "Revert";
        }


    }
    /// <summary>
    /// 保存信息
    /// </summary>
    public void SaveInfo()
    {
        Theme_UserTheme objTheme_UserTheme = objUserThemeRule.theme_usertheme.FirstOrDefault(s => s.UserID == CurrentUser.UserID);

        if (objTheme_UserTheme.IsNull())
        {
            objTheme_UserTheme = new Theme_UserTheme();
            objTheme_UserTheme.UserThemeID = Guid.NewGuid().ToString();
            objTheme_UserTheme.UserID = CurrentUser.UserID;
            ///模块类型标识
            objTheme_UserTheme.ModuleTypeID = ModuleTypeID;
            objTheme_UserTheme.ModuleThemeID = hidModuleThemeID.Value;
            ///操作方式
            objTheme_UserTheme.OperateStyle = radOperateStyle.SelectValueInt;
            objUserThemeRule.InsertUserTheme(objTheme_UserTheme);
            CurrentThemeInfo = null;
            ScriptHelper.RegisterScript("alert('设置成功'); window.top.location='" + LoginUrl + "'", RegisterType.ClientBlock);
        }
        else
        {

            ///操作方式
            objTheme_UserTheme.OperateStyle = radOperateStyle.SelectValueInt;
            objTheme_UserTheme.ModuleThemeID = hidModuleThemeID.Value;
            objUserThemeRule.UpdateUserTheme(objTheme_UserTheme);
            CurrentThemeInfo = null;
            ScriptHelper.RegisterScript("alert('设置成功'); window.top.location='" + LoginUrl + "'", RegisterType.ClientBlock);
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
            case "Revert":
                objUserThemeRule.RevertUserTheme(CurrentUser.UserID, ModuleTypeID);
                ScriptHelper.RegisterScript("alert('还原成功'); window.top.location='" + LoginUrl + "'", RegisterType.ClientBlock);
                break;



        }

    }

}