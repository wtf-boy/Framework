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
using WTF.Framework;
using WTF.DataConfig.Entity;
public partial class ServiceLayer_DataTemplate_DataTemplateTypeList : SupportPageBase
{
    public DataTemplateRule objDataTemplateRule = new DataTemplateRule();

    public override string SortExpression
    {
        get
        {
            return "it.DataTemplateTypeID";
        }
    }
    public override void RenderPage()
    {

        this.CurrentBindData<Sys_DataTemplateType>(gdvContent, objDataTemplateRule.Sys_DataTemplateType);


    }
    protected void Page_Load(object sender, EventArgs e)
    {

      

    }

    protected override void CurrentContent_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "Remove":

                objDataTemplateRule.DeleteDataTemplateType(e.CommandArgument.ToString());

                RenderPage();

                break;
            case "Create":

                Redirect("../../ServiceLayer/DataTemplate/DataTemplateTypeEdit.aspx");

                break;
            case "Modify":


                Redirect("../../ServiceLayer/DataTemplate/DataTemplateTypeEdit.aspx?DataTemplateTypeID=" + e.CommandArgument.ToString());


                break;
            case "View":
                Redirect("../../ServiceLayer/DataTemplate/DataTemplateList.aspx?DataTemplateTypeID=" + e.CommandArgument.ToString());
                break;
        }

    }
    protected override void CurrentTool_ItemCommand(object sender, WTF.Controls.MyCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "Remove":

                objDataTemplateRule.DeleteDataTemplateType(gdvContent.SelectedRowDataKeys);
                RenderPage();

                break;
            case "Create":
                Redirect("../../ServiceLayer/DataTemplate/DataTemplateTypeEdit.aspx");

                break;
            case "Modify":

                Redirect("../../ServiceLayer/DataTemplate/DataTemplateTypeEdit.aspx?DataTemplateTypeID=" + gdvContent.SelectedRowFirstKey);


                break;
            case "View":

                Redirect("../../ServiceLayer/DataTemplate/DataTemplateList.aspx?DataTemplateTypeID=" + gdvContent.SelectedRowFirstKey);

                break;
            case "Search":
                SearchCondition();
                break;
        }


    }
}
