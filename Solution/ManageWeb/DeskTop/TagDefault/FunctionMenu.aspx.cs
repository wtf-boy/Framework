using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WTF.Power;
using WTF.Framework;
public partial class DeskTop_TagDefault_FunctionMenu : SupportPageBase
{

    private PowerRule objPowerRule = new PowerRule();
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
            return GetString("ModuleCode");

        }
    }
    public override PowerType CheckPowerType
    {
        get
        {

            return PowerType.FramePower;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {

        Response.Cache.SetCacheability(HttpCacheability.NoCache);

    }

    public override void RenderPage()
    {


        XmlDataSource objXmlDataSource = new XmlDataSource();
        objXmlDataSource.Data = objPowerRule.GetPowerCoteTreeFunctionModuleXml(ModuleTypeID, ModuleCode);

        objXmlDataSource.EnableCaching = false;
        objXmlDataSource.XPath = "//Module[@ModuleLevel=2]";
        i++;
        objXmlDataSource.DataBind();
        tvwModule.DataSource = objXmlDataSource;
        tvwModule.DataBind();
    }
    public int i = 0;


}