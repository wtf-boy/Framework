using System.Web.UI.WebControls;
using WTF.Project.Business;


public partial class Manage_Topwx_DictList : SupportPageBaseSql
{
    public override string SortExpression
    {
        get
        {
            return "TableName, FieldName";
        }
    }
    BizDict objBizDict = new BizDict();
    public override void RenderPage()
    {
        this.CurrentBindData(gdvContent, objBizDict.GetPage);
    }
    protected override void CurrentTool_ItemCommand(object sender, WTF.Controls.MyCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "Create":
                Redirect("DictEdit.aspx");
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
                RedirectState("DictEdit.aspx?ID=" + e.CommandArgument.ToString());
                break;
            case "Remove":
                objBizDict.DeleteIDString(gdvContent.SelectedRowDataKeys.ToString());
                RenderPage();
                break;
        }
    }
}