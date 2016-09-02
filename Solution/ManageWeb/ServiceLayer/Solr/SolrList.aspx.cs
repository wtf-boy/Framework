using WTF.Framework;
using WTF.Solr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ServiceLayer_Solr_SolrList : SupportPageBaseSql
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }


    public void SolrSearch()
    {
        try
        {
            SolrSelect<UserInfo> objSolrQuery = new SolrSelect<UserInfo>(dropTableName.SelectedValue, dropServiceUrl.SelectedValue);
            var options = objSolrQuery.CreateCommon(txtField.Text, txtSort.Text, txtStart.Text.ConvertInt(), txtRows.Text.ConvertInt(), txtFiterQuery.Text.CreateCollection());
            options.AddDismax(txtQF.Text, txtBF.Text);
            options.AddHL(txtHlField.Text);
            if (chkDebugQuery.Checked)
            {
                options.AddOptions("debugQuery", "true");
            }
            txtResult.Text = objSolrQuery.QueryString(txtQuery.Text, options, SolrSelect<UserInfo>.QueryResultType.json);
        }
        catch (Exception objExp)
        {
            txtResult.Text = objExp.Message;
        }
    }
    protected override void CurrentTool_ItemCommand(object sender, WTF.Controls.MyCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "Search":
                SolrSearch();
                break;

        }



    }
}