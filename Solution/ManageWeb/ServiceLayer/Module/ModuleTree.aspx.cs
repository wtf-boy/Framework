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
using WTF.Framework;
using WTF.Power.Entity;
using System.Collections.Generic;
using System.Diagnostics;
public partial class ServiceLayer_Module_ModuleTree : SupportPageBase
{
    private ModuleRule objModuleRule = new ModuleRule();

    public string ModuleID
    {
        get
        {
            return GetString("ModuleId");

        }
    }
    public override string PowerPageCode
    {
        get
        {
            return "ServiceLayer_Module_ModuleInfo";
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public override void RenderPage()
    {
        if (ModuleID.IsNoNull())
        {


            List<string> objModuleIDPathList = objModuleRule.Sys_Module.Where(s => s.ModuleID == ModuleID).Select(s => s.ModuleIDPath).ToList<string>();

            if (objModuleIDPathList.Count > 0)
            {
               
                tvwModule.ExpandPath = objModuleIDPathList.First();

            }
        }
        XmlDataSource.Data = objModuleRule.GetModeuleManageTreeXmlText();

        tvwModule.DataSource = XmlDataSource;
        tvwModule.DataBind();
    }


}
