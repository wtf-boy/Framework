using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WTF.Framework;
using WTF.Logging;
public partial class ServiceLayer_Loging_OperationLogTree : SupportPageBase
{
    public override string PowerPageCode
    {
        get
        {
            return "ServiceLayer_Loging_OperationLogList";
        }
    }

    public LogRule objLogRule = new LogRule();

    public override void RenderPage()
    {

        XmlDataSource.Data = objLogRule.GetApplicationXmlText("OperationLogList.aspx");
        treeContent.DataSource = XmlDataSource;
        treeContent.DataBind();
    }
}