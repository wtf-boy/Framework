using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using WTF.Power;
using WTF.Power.Entity;
using WTF.Framework;
using WTF.Resource;
using WTF.Logging;

public partial class ServiceLayer_Module_ModuleMove : SupportPageBase
{
    private ModuleRule objModuleRule = new ModuleRule();

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

        XmlDataSource.Data = objModuleRule.GetModeuleMoveTreexXml(ModuleID);
        tvwModule.DataBind();

    }

    public void SaveModule()
    {

        if (tvwModule.SelectListGuidValue.Count == 0)
        {
            XmlDataSource.Data = objModuleRule.GetModeuleMoveTreexXml(ModuleID);
            tvwModule.DataBind();
            MessageDialog("请选择目标节点");
            return;
        }
        string tagModuleID = tvwModule.SelectListStringValue.First();
        objModuleRule.ModeuleMove(ModuleID, tagModuleID);
        RefreshFrame("frmModuleTree", "../../ServiceLayer/Module/ModuleTree.aspx?ModuleID=" + ModuleID.ToString(), "移动成功", "../../ServiceLayer/Module/ModuleInfo.aspx?ModuleID=" + ModuleID.ToString());
    }
    protected override void CurrentTool_ItemCommand(object sender, WTF.Controls.MyCommandEventArgs e)
    {
        switch (e.CommandName)
        {

            case "Save":
                SaveModule();

                break;
            case "Back":

                Redirect("../../ServiceLayer/Module/ModuleInfo.aspx?ModuleID=" + ModuleID.ToString());

                break;
        }
    }
}