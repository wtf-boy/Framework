using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WTF.Framework;
using WTF.Logging;
using WTF.Logging.Entity;
public partial class ServiceLayer_Loging_ModuleTypeEdit : SupportPageBase
{


    /// <summary>
    /// 标识
    /// </summary>
    public int LogModuleTypeID
    {
        get
        {
            return GetInt("ModuleTypeID");
        }
    }

    LogRule objLogRule = new LogRule();

    public loger_moduletype objLog_ModuleType = new loger_moduletype();
    /// <summary>
    /// 页面初始化
    /// </summary>
    public override void RenderPage()
    {


        if (LogModuleTypeID.IsNoNull())
        {
            objLog_ModuleType = objLogRule.loger_moduletype.First(s => s.ModuleTypeID == LogModuleTypeID);
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
        if (LogModuleTypeID.IsNull())
        {

            objLog_ModuleType.ModuleTypeCode = txLogModuleTypeCode.Text;
            objLog_ModuleType.ModuleTypeName = txtLogModuleTypeName.Text;
            objLog_ModuleType.ApplicationID = 1;
            objLogRule.Insertmoduletype(objLog_ModuleType);
            MessageDialog("新增成功", "ModuleTypeList.aspx");

        }
        else
        {

            objLog_ModuleType = objLogRule.loger_moduletype.First(s => s.ModuleTypeID == LogModuleTypeID);
            objLog_ModuleType.ModuleTypeCode = txLogModuleTypeCode.Text;
            objLog_ModuleType.ModuleTypeName = txtLogModuleTypeName.Text;
            objLogRule.Updatemoduletype(objLog_ModuleType);
            MessageDialog("修改成功", "ModuleTypeList.aspx");

        }

    }

    /// <summary>
    /// 操作栏
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected override void CurrentTool_ItemCommand(object sender, WTF.Controls.MyCommandEventArgs e)
    {

        switch (e.CommandName)
        {
            case "Save":
                SaveInfo();
                break;
            case "Back":
                Redirect("ModuleTypeList.aspx");
                break;
        }
    }

}