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
using WTF.Power;
public partial class ServiceLayer_Module_ModuleIcoEdit : SupportPageBase
{

    private ModuleRule objModuleRule = new ModuleRule();
    public string ModuleID
    {
        get
        {
            return GetString("ModuleID");
          
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public override void RenderPage()
    {
        ResourceRule objResourceRule = new ResourceRule();

        rptImglist.DataSource = objResourceRule.GetResourceVerFilePathInfo(1);
        rptImglist.DataBind();
    }

    public void SaveInfo()
    {

        objModuleRule.UpdateModuleIco(radOperateTypeID.SelectedValue.ConvertInt(), txtImageUrl.Text);
        MessageDialog("设置成功");


    }

    protected override void CurrentTool_ItemCommand(object sender, WTF.Controls.MyCommandEventArgs e)
    {
        switch (e.CommandName)
        {

            case "Save":
                SaveInfo();

                break;
            case "Back":
                if (objModuleRule.Sys_Module.First(s => s.ModuleID == ModuleID).IsMvc)
                {
                    Redirect("../../ServiceLayer/Module/ModuleMvcInfo.aspx?ModuleID=" + ModuleID.ToString());
                }
                else
                {
                    Redirect("../../ServiceLayer/Module/ModuleInfo.aspx?ModuleID=" + ModuleID.ToString());

                }
                break;
        }
    }
}
