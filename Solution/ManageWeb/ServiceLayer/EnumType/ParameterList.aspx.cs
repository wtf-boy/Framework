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
using WTF.Framework;
using WTF.DataConfig;
using WTF.DataConfig.Entity;
using System.Linq.Expressions;
public partial class ServiceLayer_EnumType_ParameterList : SupportPageBase<Sys_Parameter>
{
    public int ParameterTypeID
    {
        get
        {
            return GetInt("ParameterTypeID");
        }
    }

    public override Expression<Func<Sys_Parameter, bool>> Condition
    {
        get
        {

            return S => S.ParameterTypeID == ParameterTypeID;
        }
    }
    public override void RenderPage()
    {

        DataBindInfo();


    }

    public override string SortExpression
    {
        get
        {
            return "it.SortIndex";
        }
    }
    public ParameterRule objParameterRule = new ParameterRule();
    public void DataBindInfo()
    {
        this.CurrentBindData(gdvContent, objParameterRule.Sys_Parameter.Include("Sys_ParameterType").OrderBy(s => s.ParameterCodeID));
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected override void CurrentContent_RowCommand(object sender, GridViewCommandEventArgs e)
    {
       
        switch (e.CommandName)
        {
            case "Create":
                Redirect("../../ServiceLayer/EnumType/ParameterEdit.aspx?ParameterTypeID=" + ParameterTypeID.ToString());
                break;
            case "Remove":

                objParameterRule.DeleteParameter(e.CommandArgument.ToString());
                RenderPage();

                break;
            case "Modify":

                Redirect("../../ServiceLayer/EnumType/ParameterEdit.aspx?ParameterTypeID=" + ParameterTypeID.ToString() + "&ParameterID=" + e.CommandArgument.ToString());

                break;
            case "ModifyEnum":

                objParameterRule.UpdateParameter(ParameterTypeID);
                RenderPage();

                break;
            case "Back":
                Redirect("../../ServiceLayer/EnumType/ParameterTypeList.aspx");
                break;
        }

    }
    protected override void CurrentTool_ItemCommand(object sender, WTF.Controls.MyCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "Create":
                Redirect("../../ServiceLayer/EnumType/ParameterEdit.aspx?ParameterTypeID=" + ParameterTypeID.ToString());
                break;
            case "CreateQuick":
                Redirect("../../ServiceLayer/EnumType/QuickParameterEdit.aspx?ParameterTypeID=" + ParameterTypeID.ToString());
                break;

            case "Remove":

                objParameterRule.DeleteParameter(gdvContent.SelectedRowDataKeys);
                RenderPage();

                break;
            case "Modify":

                Redirect("../../ServiceLayer/EnumType/ParameterEdit.aspx?ParameterTypeID=" + ParameterTypeID.ToString() + "&ParameterID=" + gdvContent.SelectedRowFirstKey);

                break;
            case "ModifyEnum":

                objParameterRule.UpdateParameter(ParameterTypeID);
                RenderPage();

                break;
            case "Back":
                Redirect("../../ServiceLayer/EnumType/ParameterTypeList.aspx");
                break;

            case "Search":
                SearchCondition();
                break;
        }
    }
}
