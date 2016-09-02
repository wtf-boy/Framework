using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WTF.Framework;
using WTF.DataConfig;
using WTF.DataConfig.Entity;
public partial class ServiceLayer_Data_DataTypeList : SupportPageBase
{
    public override string SortExpression
    {
        get
        {
            return "it.DataCode";
        }
    }
    DataRule objDataRule = new DataRule();
    public override void RenderPage()
    {

        this.CurrentBindData<Sys_DataType>(gdvContent, objDataRule.Sys_DataType);
    }
    protected override void CurrentTool_ItemCommand(object sender, WTF.Controls.MyCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "Create":
                Redirect("DataTypeEdit.aspx");
                break;
            case "Back":
                Redirect("../../ServiceLayer/Module/ModuleTypeList.aspx");
                break;

        }



    }
    protected override void CurrentContent_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "Modify":
                Redirect("DataTypeEdit.aspx?DataTypeID=" + e.CommandArgument.ToString());
                break;
            case "Remove":
                objDataRule.DeleteDataTypeByKey(e.CommandArgument.ToString());
                RenderPage();
                break;
            case "ViewData":
                Redirect("DataFieldList.aspx?DataTypeID=" + e.CommandArgument.ToString());
                break;

        }
    }

}