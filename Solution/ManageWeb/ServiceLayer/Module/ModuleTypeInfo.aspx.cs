using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using WTF.Power;
using WTF.Power.Entity;
using WTF.Framework;
public partial class ServiceLayer_Module_ModuleTypeInfo : SupportPageBase
{
    private ModuleRule objModuleRule = new ModuleRule();
    public Sys_ModuleType objModuleType;
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
        if (ModuleID.IsNoNull())
        {
            objModuleType = objModuleRule.Sys_ModuleType.Where(p => p.ModuleTypeID == ModuleID).First();

            Page.DataBind();


        }
    }
    protected override void CurrentTool_ItemCommand(object sender, WTF.Controls.MyCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "Create":
                Redirect("../../ServiceLayer/Module/ModuleEdit.aspx?ModuleType=1&ParentModuleID=" + ModuleID.ToString());
                break;
            case "CreateMvc":
                Redirect("../../ServiceLayer/Module/ModuleMvcEdit.aspx?ModuleType=1&ParentModuleID=" + ModuleID.ToString());
                break;
            case "Modify":
                Redirect("../../ServiceLayer/Module/ModuleTypeEdit.aspx?ModuleType=1&ModuleTypeID=" + ModuleID.ToString());

                break;
            case "UpdateSql":
                UpdateSql();
                break;



        }
    }

    public void UpdateSql()
    {
        DataSyncHelper objDataSyncHelper = new DataSyncHelper("OpenConnectionString");
        string updateQuery = objDataSyncHelper.CreateSyncQuery("Sys_Module", "ModuleTypeID='" + ModuleID + "'", "", true);
        string SqlFileName = "Seven" + DateTime.Now.ToString("yyyy-MM-dd-hh") + ".sql";
        Response.ContentEncoding = System.Text.Encoding.UTF8;
        Response.AppendHeader("Content-Disposition", "attachment;filename=" + SqlFileName);
        Response.Write(updateQuery);
        Response.End();
    }
}
