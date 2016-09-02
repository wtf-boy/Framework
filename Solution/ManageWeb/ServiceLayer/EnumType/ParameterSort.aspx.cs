using WTF.DataConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ServiceLayer_EnumType_ParameterSort : SupportPageBase
{
    private ParameterRule objParameterRule = new ParameterRule();
    protected void Page_Load(object sender, EventArgs e)
    {

    }


    public int ParameterTypeID
    {
        get
        {
            return GetInt("ParameterTypeID");
        }
    }

    public override void RenderPage()
    {

        lboxSort.DataSource = objParameterRule.Sys_Parameter.Where(p => p.ParameterTypeID == ParameterTypeID).OrderBy(p => p.SortIndex);
        lboxSort.DataTextField = "ParameterName";
        lboxSort.DataValueField = "ParameterID";
        lboxSort.DataBind();


    }
    protected void btnSave_Click(object sender, EventArgs e)
    {

        if (hidSortID.Value.Length > 0)
        {
            objParameterRule.ParameterSort(hidSortID.Value);
            DialogOpenerReloadScript(true,"保存成功");
        }
        else
        {
            MessageDialog("排序未改变无须保存");
        }

    }
}