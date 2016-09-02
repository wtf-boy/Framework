using System;
using System.Collections;
using System.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Linq;

using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.Objects;
using System.Data.Objects.ELinq;
using System.Data.Query;
using WTF.Power;
using WTF.Power.Entity;
using WTF.Framework;
using System.IO;
using WTF.Logging;
using WTF.Logging.Entity;
public partial class ServiceLayer_Module_ModuleInfo : SupportPageBase
{


    public Sys_Module objModule;

    protected void Page_Load(object sender, EventArgs e)
    {


    }

    public string ModuleID
    {
        get
        {

            return GetString("ModuleId");
        }
    }
    public override void RenderPage()
    {
        string ModuleIDPath = CurrentModuleRule.Sys_Module.First(s => s.ModuleID == ModuleID).ModuleIDPath;
        rptPath.DataSource = CurrentModuleRule.Sys_Module.Where("it.ModuleID  in {" + ModuleIDPath.ConvertStringID() + "  }").Select("it.ModuleID,it.ModuleName,it.ModuleLevel").OrderBy("it.ModuleLevel");
        rptPath.DataBind();
        List<Sys_Module> objModuleList = CurrentModuleRule.Sys_ModuleType.FirstOrDefault(s => s.ModuleTypeCode == "ModuleTemplate").Sys_Module.CreateSourceQuery().OrderBy(w => w.SortIndex).ToList<Sys_Module>();
        XmlDataSource.Data = CurrentModuleRule.GetQuickModuleTreexXml();
        tvwQuickModule.DataBind();


        txtModuleID.Text = ModuleID;
    }



    public void SyncSql()
    {
        string ModuleSyncTableSchema = ConfigHelper.GetValue("ModuleSyncTableSchema", "openData");

        string ModuleIDPath = CurrentModuleRule.Sys_Module.First(s => s.ModuleID == ModuleID).ModuleIDPath;
        MyDataSyncHelper objDataSyncHelper = new MyDataSyncHelper("SevenConnectionString");
        string updateQuery = objDataSyncHelper.CreateSyncQuery(ModuleSyncTableSchema, "Sys_Module", "ModuleIDPath like '" + ModuleIDPath + "%'", "", true);
        string SqlFileName = "OpenData" + DateTime.Now.ToString("yyyy-MM-dd-hh") + ".sql";
        Response.ContentEncoding = System.Text.Encoding.UTF8;
        Response.AppendHeader("Content-Disposition", "attachment;filename=" + SqlFileName);
        Response.Write(updateQuery);
        Response.End();
    }
    public void InsertSql()
    {
        string ModuleSyncTableSchema = ConfigHelper.GetValue("ModuleSyncTableSchema", "openData");

        string ModuleIDPath = CurrentModuleRule.Sys_Module.First(s => s.ModuleID == ModuleID).ModuleIDPath;
        MyDataSyncHelper objDataSyncHelper = new MyDataSyncHelper("SevenConnectionString");
        string updateQuery = objDataSyncHelper.CreateSyncInsertQuery(ModuleSyncTableSchema, "Sys_Module", "ModuleIDPath like '" + ModuleIDPath + "%'", true);
        string SqlFileName = "OpenData" + DateTime.Now.ToString("yyyy-MM-dd-hh") + ".sql";
        Response.ContentEncoding = System.Text.Encoding.UTF8;
        Response.AppendHeader("Content-Disposition", "attachment;filename=" + SqlFileName);
        Response.Write(updateQuery);
        Response.End();
    }
    public void UpdateSql()
    {
        string ModuleSyncTableSchema = ConfigHelper.GetValue("ModuleSyncTableSchema", "openData");
        string ModuleIDPath = CurrentModuleRule.Sys_Module.First(s => s.ModuleID == ModuleID).ModuleIDPath;
        MyDataSyncHelper objDataSyncHelper = new MyDataSyncHelper("SevenConnectionString");
        string updateQuery = objDataSyncHelper.CreateSyncUpdateQuery(ModuleSyncTableSchema, "Sys_Module", "ModuleIDPath like '" + ModuleIDPath + "%'", "", true);
        string SqlFileName = "OpenData" + DateTime.Now.ToString("yyyy-MM-dd-hh") + ".sql";
        Response.ContentEncoding = System.Text.Encoding.UTF8;
        Response.AppendHeader("Content-Disposition", "attachment;filename=" + SqlFileName);
        Response.Write(updateQuery);
        Response.End();
    }
    protected override void CurrentTool_ItemCommand(object sender, WTF.Controls.MyCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "Create":
                Redirect("../../ServiceLayer/Module/ModuleEdit.aspx?ParentModuleID=" + ModuleID.ToString());
                break;
            case "Modify":
                Redirect("../../ServiceLayer/Module/ModuleEdit.aspx?ModuleID=" + ModuleID.ToString());
                break;
            case "Remove":
                string ParentModuleID = CurrentModuleRule.Sys_Module.First(s => s.ModuleID == ModuleID).ParentModuleID;
                bool isModule = CurrentModuleRule.Sys_Module.Any(s => s.ModuleID == ParentModuleID);
                CurrentModuleRule.DeleteModule(ModuleID);
                if (isModule)
                {
                    RefreshFrame("frmModuleTree", "../../ServiceLayer/Module/ModuleTree.aspx?ModuleID=" + ParentModuleID.ToString(), "删除成功", "../../ServiceLayer/Module/ModuleInfo.aspx?ModuleID=" + ParentModuleID.ToString());

                }
                else
                {
                    RefreshFrame("frmModuleTree", "../../ServiceLayer/Module/ModuleTree.aspx?ModuleID=" + ParentModuleID.ToString(), "删除成功", "../../ServiceLayer/Module/ModuleTypeInfo.aspx?ModuleID=" + ParentModuleID.ToString());
                }
                break;
            case "ModifyIco":
                Redirect("../../ServiceLayer/Module/ModuleIcoEdit.aspx?ModuleID=" + ModuleID.ToString());
                break;
            case "ModifyHelp":
                Redirect("../../ServiceLayer/Module/ModuleHelpEdit.aspx?ModuleID=" + ModuleID.ToString());
                break;

            case "AddModule":

                if (tvwQuickModule.SelectListStringValue.Count == 0)
                {
                    MessageDialog("请选择节点");
                }
                CurrentModuleRule.ModeuleQuickCopy(txtModuleName.Text, txtModuleCode.Text.Trim(), -1, ModuleID, tvwQuickModule.SelectListStringValue);
                RefreshFrame("frmModuleTree", "../../ServiceLayer/Module/ModuleTree.aspx?ModuleID=" + ModuleID.ToString(), "新增成功", "../../ServiceLayer/Module/ModuleInfo.aspx?ModuleID=" + ModuleID.ToString());
                break;
            case "SyncSql":
                SyncSql();
                break;
            case "InsertSql":
                InsertSql();
                break;
            case "UpdateSql":
                UpdateSql();
                break;

            case "ModuleMove":
                Redirect("../../ServiceLayer/Module/ModuleMove.aspx?ModuleID=" + ModuleID.ToString());
                break;
            case "CopyModule":
                Redirect("../../ServiceLayer/Module/ModuleCopy.aspx?ModuleID=" + ModuleID.ToString());
                break;
            case "DataSet":
                Redirect("../../ServiceLayer/Module/ModuleDataList.aspx?ModuleID=" + ModuleID.ToString());
                break;

        }
    }
}
