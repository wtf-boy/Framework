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
using WTF.Resource;
using WTF.Framework;
public partial class ServiceLayer_Resource_ResourceTree : SupportPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    public override string PowerPageCode
    {
        get
        {
            
            return "ServiceLayer_Resource_ResourceFrame";
        }
    }

    public override void RenderPage()
    {
        ResourceRule objResourceRule = new ResourceRule();
        rptResourceType.DataSource = objResourceRule.Sys_ResourceType.OrderBy(r => r.ResourceTypeID);
        rptResourceType.DataBind();
    }
}
