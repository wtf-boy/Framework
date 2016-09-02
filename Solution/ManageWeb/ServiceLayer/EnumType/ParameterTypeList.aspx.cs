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
using WTF.DataConfig;
using WTF.DataConfig.Entity;
using WTF.Framework;
using WTF.Logging;
using WTF.Controls;
public partial class ServiceLayer_EnumType_ParameterTypeList : SupportPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    public ParameterRule objParameterRule = new ParameterRule();

    public override string SortExpression
    {
        get
        {
            return "it.ParameterTypeID";
        }
    }
    public override void RenderPage()
    {

        this.CurrentBindData<Sys_ParameterType>(gdvContent, objParameterRule.Sys_ParameterType);

    }

    protected override void CurrentTool_ItemCommand(object sender, WTF.Controls.MyCommandEventArgs e)
    {

        switch (e.CommandName)
        {
            case "Create":
                Redirect("../../ServiceLayer/EnumType/ParameterTypeEdit.aspx");
                break;
            case "Search":
                SearchCondition();
                break;
        }

    }

    protected override void CurrentContent_RowCommand(object sender, GridViewCommandEventArgs e)
    {
      
        switch (e.CommandName)
        {
            case "Modify":
                RedirectState("../../ServiceLayer/EnumType/ParameterTypeEdit.aspx?ParameterTypeID=" + e.CommandArgument.ToString());
                break;
            case "Remove":
                objParameterRule.DeleteParameterType(e.CommandArgument.ToString());
                RenderPage();
                break;
            case "ViewParameter":
                RedirectState("../../ServiceLayer/EnumType/ParameterList.aspx?ParameterTypeID=" + e.CommandArgument.ToString());

                break;

        }

    }
 
}
