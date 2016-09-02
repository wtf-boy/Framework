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
public partial class ServiceLayer_DataTemplate_DataTemplateList : SupportPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public int DataTemplateTypeID
    {
        get
        {
            return GetInt("DataTemplateTypeID");
        }
    }
    public DataTemplateRule objDataTemplateRule = new DataTemplateRule();

    public override string SortExpression
    {
        get
        {
            return "it.DataTemplateID";
        }
    }
    public override void RenderPage()
    {

         

        this.CurrentBindData<Sys_DataTemplate>(gdvContent, objDataTemplateRule.Sys_DataTemplateType.First(p => p.DataTemplateTypeID == DataTemplateTypeID).Sys_DataTemplate.CreateSourceQuery());


    }

    protected override void CurrentTool_ItemCommand(object sender, WTF.Controls.MyCommandEventArgs e)
    {

        switch (e.CommandName)
        {
            case "Create":

                Redirect("../../ServiceLayer/DataTemplate/DataTemplateEdit.aspx?DataTemplateTypeID=" + DataTemplateTypeID.ToString());
                break;
            case "Remove":


                objDataTemplateRule.DeleteDataTemplate(gdvContent.SelectedRowDataKeys);
                RenderPage();

                break;
            case "Modify":

                Response.Redirect("../../ServiceLayer/DataTemplate/DataTemplateEdit.aspx?DataTemplateTypeID=" + DataTemplateTypeID.ToString() + "&DataTemplateID=" + gdvContent.SelectedRowFirstKey);

                break;
            case "Back":
                Redirect("../../ServiceLayer/DataTemplate/DataTemplateTypeList.aspx");
                break;
        }

    }
    protected override void CurrentContent_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        switch (e.CommandName)
        {
            case "Create":
                Redirect("../../ServiceLayer/DataTemplate/DataTemplateEdit.aspx?DataTemplateTypeID=" + DataTemplateTypeID.ToString());
                break;
            case "Remove":

                objDataTemplateRule.DeleteDataTemplate(e.CommandArgument.ToString());
                RenderPage();

                break;
            case "Modify":

                Redirect("../../ServiceLayer/DataTemplate/DataTemplateEdit.aspx?DataTemplateTypeID=" + DataTemplateTypeID.ToString() + "&DataTemplateID=" + e.CommandArgument);

                break;
            case "Back":
                Redirect("../../ServiceLayer/DataTemplate/DataTemplateTypeList.aspx");
                break;
        }


    }
}
