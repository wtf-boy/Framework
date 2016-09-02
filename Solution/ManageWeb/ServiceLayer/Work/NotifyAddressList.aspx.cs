using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WTF.Work;
using WTF.Work.Entity;

public partial class ServiceLayer_Work_NotifyAddressList : SupportPageBase
{
    public override string SortExpression
    {
        get
        {
            return "it.AddressName";
        }
    }
    WorkRule objWorkRule = new WorkRule();
    public override void RenderPage()
    {

        this.CurrentBindData<Work_NotifyAddress>(gdvContent, objWorkRule.Work_NotifyAddress);
    }
    protected override void CurrentTool_ItemCommand(object sender, WTF.Controls.MyCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "Create":
                Redirect("NotifyAddressEdit.aspx");
                break;
            case "Back":
                Redirect("WorkInfoList.aspx");
                break;


        }

    }
    protected override void CurrentContent_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "Modify":
                RedirectState("NotifyAddressEdit.aspx?NotifyAddressID=" + e.CommandArgument.ToString());
                break;
            case "Remove":
                objWorkRule.DeleteNotifyAddressByKey(int.Parse(e.CommandArgument.ToString()));
                RenderPage();
                break;

        }
    }

}