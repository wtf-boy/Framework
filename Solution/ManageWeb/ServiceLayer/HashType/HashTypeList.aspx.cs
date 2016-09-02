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
public partial class ServiceLayer_HashType_HashTypeList : SupportPageBase
{

    protected void Page_Load(object sender, EventArgs e)
    {


    }
    private HashRule objHashRule = new HashRule();
    public override void InitDataPage()
    {
       
    }

    public override string SortExpression
    {
        get
        {
            return "it.HashTypeID,it.HashTypeName";
        }
    }
    public override void RenderPage()
    {


        this.CurrentBindData<Sys_HashType>(gdvContent, objHashRule.Sys_HashType);

    }
    protected override void CurrentContent_RowCommand(object sender, GridViewCommandEventArgs e)
    {
       
        switch (e.CommandName)
        {

            case "Modify":
                RedirectState("../../ServiceLayer/HashType/HashTypeEdit.aspx?HashTypeID=" + e.CommandArgument.ToString() + "&wxnt=" + DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
                break;
            case "View":
                Redirect("../../ServiceLayer/HashType/HashList.aspx?HashTypeID=" + e.CommandArgument.ToString());
                break;
            case "Remove":
                objHashRule.DeleteHashType(e.CommandArgument.ToString());
                RenderPage();
                break;
        }

    }
    protected override void CurrentTool_ItemCommand(object sender, WTF.Controls.MyCommandEventArgs e)
    {
        switch (e.CommandName)
        {

            case "Create":
                Redirect("../../ServiceLayer/HashType/HashTypeEdit.aspx");
                break;
            case "Search":
                SearchCondition();
                break;
        }


    }
}
