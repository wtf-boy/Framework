using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WTF.Framework;
using WTF.DataConfig;
using WTF.DataConfig.Entity;
public partial class ServiceLayer_Data_DataFieldList : SupportPageBase
{
    public int DataTypeID
    {
        get
        {
            return GetInt("DataTypeID");

        }

    }
    public override string Condition
    {
        get
        {
            return "it.DataTypeID=" + DataTypeID;
        }
    }
    public override string SortExpression
    {
        get
        {
            return "it.DataValue";
        }
    }
    DataRule objDataRule = new DataRule();
    public override void RenderPage()
    {

        this.CurrentBindData<Sys_DataField>(gdvContent, objDataRule.Sys_DataField);
    }
    protected override void CurrentTool_ItemCommand(object sender, WTF.Controls.MyCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "Create":
                Redirect("DataFieldEdit.aspx?DataTypeID=" + DataTypeID);
                break;
            case "Remove":
                objDataRule.DeleteDataFieldByKey(gdvContent.SelectedRowDataKeys);
                RenderPage();
                break;
            case "Back":
                Redirect("DataTypeList.aspx?DataTypeID=" + DataTypeID);
                break;


        }



    }
    protected override void CurrentContent_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "Modify":
                Redirect("DataFieldEdit.aspx?DataTypeID=" + DataTypeID + "&DataFieldID=" + e.CommandArgument.ToString());
                break;
            case "Remove":
                objDataRule.DeleteDataFieldByKey(e.CommandArgument.ToString());
                RenderPage();
                break;

        }
    }

}