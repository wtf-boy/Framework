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
public partial class ServiceLayer_Module_ModuleSort : SupportPageBase
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

        lboxSort.DataSource = objModuleRule.Sys_Module.Where(p => p.ParentModuleID == ModuleID).OrderBy(p => p.SortIndex);
        lboxSort.DataTextField = "ModuleName";
        lboxSort.DataValueField = "ModuleID";
        lboxSort.DataBind();


    }
    protected void btnSave_Click(object sender, EventArgs e)
    {

        if (hidSortID.Value.Length > 0)
        {
            objModuleRule.UpdateModuleSort(hidSortID.Value);
            MessageDialog("保存成功");
            RenderPage();

        }
        else
        {
            MessageDialog("排序未改变无须保存");
        }

    }
}
