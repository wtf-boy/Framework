using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WTF.Framework;
using WTF.Logging;
using WTF.Logging.Entity;
public partial class ServiceLayer_Loging_OperationLogClear : SupportPageBase
{

    LogRule objLogRule = new LogRule();


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
        ModuleTypeCode.Items.Clear();
        ModuleTypeCode.Items.Add(new ListItem("--全部--", ""));
        foreach (loger_moduletype objloger_moduletype in objLogRule.loger_moduletype)
        {
            ModuleTypeCode.Items.Add(new ListItem(objloger_moduletype.ModuleTypeName, objloger_moduletype.ModuleTypeCode));
        }

        OperationTypeID.Items.Add(new ListItem("--全部--", ""));

        foreach (EnumParameter objEnumParameter in typeof(OperationType).GetEnumMembers())
        {
            OperationTypeID.Items.Add(new ListItem(objEnumParameter.Description, objEnumParameter.Value.ToString()));
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
        string Condition = QueryModel.ToConditionSql<loger_loging>();

        List<int> objApplicationIDList = new List<int>();
        if (chkChild.Checked)
        {
            string IDPath = objLogRule.loger_application.FirstOrDefault(s => s.ApplicationID == ApplicationID).IDPath;
            objApplicationIDList = objLogRule.loger_application.Where("it.IDPath like '" + IDPath + "%'").Select(s => s.ApplicationID).ToList();
        }
        else
        {
            objApplicationIDList.Add(ApplicationID);
        }
        Condition = " ApplicationID in (" + objApplicationIDList.ConvertListToString() + ")" + (Condition.IsNoNull() ? " and  " : "") + Condition;

        objLogRule.loger_loging.DeleteDataSql(Condition);
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