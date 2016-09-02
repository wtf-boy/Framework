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
public partial class ServiceLayer_HashType_HashList : SupportPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {

      
    }

    //属性  
    private HashRule objHashRule = new HashRule();
    public int HashTypeID
    {
        get
        {
            return GetInt("HashTypeID");
        }
    }

    public override string SortExpression
    {
        get
        {
            return "it.HashID";
        }
    }
    public override void RenderPage()
    {
        this.CurrentBindData<Sys_Hash>(gdvContent, objHashRule.Sys_HashType.First(p => p.HashTypeID == HashTypeID).Sys_Hash.CreateSourceQuery());

    }

    protected override void CurrentTool_ItemCommand(object sender, WTF.Controls.MyCommandEventArgs e)
    {

        switch (e.CommandName)
        {
            case "Create":

                Redirect("../../ServiceLayer/HashType/HashEdit.aspx?HashTypeID=" + HashTypeID.ToString());
                break;
            case "Remove":

                objHashRule.DeleteHash(gdvContent.SelectedRowDataKeys);
                RenderPage();

                break;
            case "Modify":

                Redirect("../../ServiceLayer/HashType/HashEdit.aspx?HashTypeID=" + HashTypeID.ToString() + "&HashID=" + gdvContent.SelectedRowFirstKey);

                break;
            case "Back":
                Redirect("../../ServiceLayer/HashType/HashTypeList.aspx");
                break;
        }

    }
    protected override void CurrentContent_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        switch (e.CommandName)
        {
            case "Create":
                Redirect("../../ServiceLayer/HashType/HashEdit.aspx?HashTypeID=" + HashTypeID.ToString());
                break;
            case "Remove":

                objHashRule.DeleteHash(e.CommandArgument.ToString());
                RenderPage();

                break;
            case "Modify":

                Redirect("../../ServiceLayer/HashType/HashEdit.aspx?HashTypeID=" + HashTypeID.ToString() + "&HashID=" + e.CommandArgument);

                break;
            case "Back":
                Redirect("../../ServiceLayer/HashType/HashTypeList.aspx");
                break;
        }


    }
}
