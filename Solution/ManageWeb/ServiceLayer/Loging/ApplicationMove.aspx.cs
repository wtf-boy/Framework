using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WTF.Framework;
using WTF.Logging;
public partial class ServiceLayer_Loging_ApplicationMove : SupportPageBase
{

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
    LogRule objLogRule = new LogRule();
    public override void RenderPage()
    {

        XmlDataSource.Data = objLogRule.GetApplicationMoveTreexXml(ApplicationID);
        tvwModule.DataBind();

    }

    public void SaveModule()
    {

        if (tvwModule.SelectListIntValue.Count == 0)
        {
            XmlDataSource.Data = objLogRule.GetApplicationMoveTreexXml(ApplicationID);
            tvwModule.DataBind();
            MessageDialog("请选择目标节点");
            return;
        }
        int tagApplicationID = tvwModule.SelectListIntValue.First();
        objLogRule.ApplicationMove(ApplicationID, tagApplicationID);
        RefreshFrame("frmApplicationTree", "ApplicationTree.aspx?ApplicationID=" + ApplicationID.ToString(), "移动成功", "ApplicationInfo.aspx?ApplicationID=" + ApplicationID.ToString());
    }
    protected override void CurrentTool_ItemCommand(object sender, WTF.Controls.MyCommandEventArgs e)
    {
        switch (e.CommandName)
        {

            case "Save":
                SaveModule();

                break;
            case "Back":
                Redirect("ApplicationInfo.aspx?ApplicationID=" + ApplicationID.ToString());
                break;
        }
    }
}