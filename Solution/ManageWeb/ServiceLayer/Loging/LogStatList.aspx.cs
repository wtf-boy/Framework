using WTF.DAL;
using WTF.Logging;
using WTF.Logging.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WTF.Framework;
public partial class ServiceLayer_Loging_LogStatList : SupportPageBaseSql
{



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
    public string GetApplicationName(int ApplicationID)
    {
        loger_application objloger_application = objloger_applicationList.FirstOrDefault(s => s.ApplicationID == ApplicationID);
        if (objloger_application != null)
        {
            return objloger_application.ApplicationName;
        }
        return "未知程序";
    }
    public LogRule objLogRule = new LogRule();
    DalBase objDalBase = new DalBase("SevenConnectionString");
    List<loger_application> objloger_applicationList = new List<loger_application>();


    public override void RenderPage()
    {
        objloger_applicationList = objLogRule.loger_application.ToList();

        DataSet objDataSet = objDalBase.ExecuteDataSet("SELECT   ApplicationID,   COUNT(0) AS LogCount FROM  loger_loging GROUP BY ApplicationID   Order by  LogCount desc");
        if (ApplicationID != 1)
        {
            loger_application objloger_application = objloger_applicationList.FirstOrDefault(s => s.ApplicationID == ApplicationID);
            string ApplicationIDString = objloger_applicationList.Where(s => s.IDPath.StartsWith(objloger_application.IDPath)).Select(s => s.ApplicationID).ConvertListToString();
            DataTable cloneTable = objDataSet.Tables[0].Clone();
            foreach (DataRow objSelectDataRow in objDataSet.Tables[0].Select(" ApplicationID in (" + ApplicationIDString + ")"))
            {
                cloneTable.Rows.Add(objSelectDataRow.ItemArray);
            }
            DataView view = new DataView();
            view = cloneTable.DefaultView;
            view.Sort = "LogCount desc";
            gdvContent.DataSource = view;
        }
        else
        {
            gdvContent.DataSource = objDataSet;
        }
        gdvContent.RecordCount = 10000;
        gdvContent.PageSize = 10000;
        gdvContent.DataBind();
    }

    protected override void CurrentContent_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "LogClear":
                objDalBase.ExecuteNonQuery("delete from loger_loging where ApplicationID=" + e.CommandArgument.ToString());
                WriteOperatorLog(OperationType.Delete, MenuPowerID + e.CommandArgument.ToString(), "程序日志查看", "清理日志", "日志清理", "在日志统计中清理");
                MessageDialog("清理成功");
                RenderPage();
                break;
        }
    }
    protected override void CurrentTool_ItemCommand(object sender, WTF.Controls.MyCommandEventArgs e)
    {

        switch (e.CommandName)
        {
            case "Search":
                SearchCondition();
                break;
            case "Back":
                Redirect("LogList.aspx?ApplicationID=" + ApplicationID.ToString());

                break;
        }
    }
}