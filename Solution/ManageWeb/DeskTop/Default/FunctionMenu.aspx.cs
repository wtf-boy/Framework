using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WTF.Power;
using WTF.Framework;
public partial class DeskTop_Default_FunctionMenu : SupportPageBase
{

    private ModuleRule objModuleRule = new ModuleRule();

    public override PowerType CheckPowerType
    {
        get
        {

            return PowerType.FramePower;
        }
    }

    public override void RenderPage()
    {

        rptMainNavbar.DataSource = objModuleRule.GetPowerFunctionModule(ModuleTypeID, ModuleCode, false);
        rptMainNavbar.DataBind();
    }

    public Guid ModuleID
    {
        get
        {
            return GetGuid("ParentModuleId");

        }
    }

    public string ModuleCode
    {
        get
        {
            return RequestHelper.GetString("ModuleCode");

        }
    }
}