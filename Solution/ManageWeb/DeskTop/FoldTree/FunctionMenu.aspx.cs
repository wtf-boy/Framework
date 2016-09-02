using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WTF.Power;
using WTF.Framework;
public partial class DeskTop_FoldTree_FunctionMenu : SupportPageBase
{

    private PowerRule objPowerRule = new PowerRule();

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

        rptCoteTree.DataSource = objPowerRule.GetPowerCoteTreeFunctionModule(ModuleTypeID);
        rptCoteTree.DataBind();
    }
    public int i = 0;
    public object GetTreeData(object xml)
    {
        XmlDataSource objXmlDataSource = new XmlDataSource();
        objXmlDataSource.Data = xml.ToString();
        objXmlDataSource.ID = "XmlDataSource" + i;
        objXmlDataSource.EnableCaching = false;
        objXmlDataSource.XPath = "//Module[@ModuleLevel=2]"; 
        i++;
        objXmlDataSource.DataBind();
        return objXmlDataSource;
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