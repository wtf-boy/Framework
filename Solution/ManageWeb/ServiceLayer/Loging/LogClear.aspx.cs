using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WTF.Framework;
using WTF.Logging;
using WTF.Logging.Entity;
public partial class ServiceLayer_Loging_LogClear : SupportPageBase
{

    LogRule objLogRule = new LogRule();

    public override string MenuPowerID
    {
        get
        {
            return base.MenuPowerID + ApplicationID;
        }
    }
    /// <summary>
    /// 获取程序标识
    /// </summary>
    public int ApplicationID
    {
        get
        {
            return GetInt("ApplicationID");

        }

    }
    public override void InitDataPage()
    {

        dropCategoryTypeCode.Items.Add(new ListItem("--全部--", ""));
        List<EnumParameter> _CategoryList = typeof(LogCategory).GetEnumMembers();
        foreach (EnumParameter objEnumParameter in typeof(LogCategory).GetEnumMembers())
        {
            dropCategoryTypeCode.Items.Add(new ListItem(objEnumParameter.Description, objEnumParameter.Key));
        }

    }

    /// <summary>
    /// 页面初始化
    /// </summary>
    public override void RenderPage()
    {


    }

    /// <summary>
    /// 保存信息
    /// </summary>
    public void SaveInfo()
    {
        string condition = QueryModel.ToConditionSql<loger_loging>();
        condition = " ApplicationID=" + ApplicationID + (condition.IsNoNull() ? " and  " : "") + condition;
        objLogRule.loger_loging.DeleteDataSql(condition);
        WriteOperatorLog(OperationType.Delete, "清理日志", "日志清理" + LogDate.Text, condition);
        MessageDialog("清理成功");
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

                Redirect("LogList.aspx?ApplicationID=" + ApplicationID);

                break;
        }
    }


}