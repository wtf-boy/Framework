using WTF.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WTF.Framework;
public partial class ServiceLayer_Loging_ApplicationTree : SupportPageBase
{
    public override string PowerPageCode
    {
        get
        {
            return "ServiceLayer_Loging_ApplicationInfo";
        }
    }
    /// <summary>
    /// 获取程序标识
    /// </summary>
    public int ApplicationID
    {
        get
        {
            return GetInt("ApplicationID");

        }

    }
    public LogRule objLogRule = new LogRule();

    public override void RenderPage()
    {
        if (ApplicationID.IsNoNull())
        {


            List<string> objIDPathList = objLogRule.loger_application.Where(s => s.ApplicationID == ApplicationID).Select(s => s.IDPath).ToList<string>();

            if (objIDPathList.Count > 0)
            {

                treeContent.ExpandPath = objIDPathList.First();

            }
        }
        XmlDataSource.Data = objLogRule.GetApplicationXmlText("ApplicationInfo.aspx");
        treeContent.DataSource = XmlDataSource;
        treeContent.DataBind();
    }

}