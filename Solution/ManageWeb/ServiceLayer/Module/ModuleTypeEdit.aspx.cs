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
using WTF.Logging;
using System.Collections.Generic;
using WTF.Logging.Entity;

public partial class ServiceLayer_Module_ModuleTypeEdit : SupportPageBase
{
    private ModuleRule objModuleRule = new ModuleRule();
    public Sys_ModuleType objModuleType;

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public string ModuleModuleTypeID
    {
        get
        {
            return GetString("ModuleTypeID");

        }
    }


    public override void RenderPage()
    {

        if (!Page.IsPostBack)
        {


            UserRule objUserRule = new UserRule();


            foreach (Sys_UserType objSys_UserType in objUserRule.Sys_UserType.OrderBy(s => s.UserTypeID))
            {
                chkUserType.Items.Add(new ListItem(objSys_UserType.UserTypeName, objSys_UserType.UserTypeID.ToString()));

            }

        }

        if (ModuleModuleTypeID.IsNoNull())
        {
            objModuleType = objModuleRule.Sys_ModuleType.Where(p => p.ModuleTypeID == ModuleModuleTypeID).First();
            chkUserType.SetSelectValue(objModuleType.UserType);

            chkSystem.Checked = objModuleType.IsSystem;
            Page.DataBind();

        }
    }

    public void SaveModule()
    {
        if (ModuleModuleTypeID.IsNull())
        {
            objModuleType = new Sys_ModuleType() { IsDispose = true, IsSystem = chkSystem.Checked, LogCategoryID = 0, ModuleTypeID = Guid.NewGuid().ToString(), ModuleTypeCode = txtModuleCode.Text, ModuleTypeName = txtModuleName.Text, UserType = chkUserType.SelectValueString };
            objModuleRule.InsertModuleType(objModuleType);

        }
        else
        {

            objModuleType = objModuleRule.Sys_ModuleType.Where(p => p.ModuleTypeID == ModuleModuleTypeID).First();
            objModuleType.ModuleTypeName = txtModuleName.Text;
            objModuleType.ModuleTypeCode = txtModuleCode.Text;
            objModuleType.UserType = chkUserType.SelectValueString;
            objModuleType.IsSystem = chkSystem.Checked;
            objModuleRule.SaveChanges();

        }

        MessageDialog("保存成功");
        RefreshFrame("frmModuleTree");
    }
    protected override void CurrentTool_ItemCommand(object sender, WTF.Controls.MyCommandEventArgs e)
    {
        switch (e.CommandName)
        {

            case "Save":
                SaveModule();

                break;
            case "Back":
                if (GetInt("ModuleType") != 1)
                {
                    Redirect("../../ServiceLayer/Module/ModuleTypeList.aspx");
                }
                else
                {
                    Redirect("../../ServiceLayer/Module/ModuleTypeInfo.aspx?ModuleID=" + ModuleModuleTypeID);
                }
                break;
        }
    }
}
