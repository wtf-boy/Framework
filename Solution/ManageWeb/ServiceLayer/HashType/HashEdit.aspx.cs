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
public partial class ServiceLayer_HashType_HashEdit : SupportPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public int HashID
    {
        get
        {

            return GetInt("HashID");
        }
    }

    public Sys_Hash objHash;
    public HashRule objHashRule = new HashRule();

    public int HashTypeID
    {
        get
        {
            return GetInt("HashTypeID");
        }
    }


    public void SaveInfo()
    {
        if (HashID.IsNull())
        {
            objHash = new Sys_Hash();
            objHash.HashTypeID = HashTypeID;
            objHash.HashValue = txtHashValue.Text;
            objHash.HashKey = txtHashKey.Text;
            objHash.Remark = txtRemark.Text;
            objHashRule.InsertHash(objHash);

            MessageDialogContinue("添加成功", "../../ServiceLayer/HashType/HashList.aspx?HashTypeID=" + HashTypeID.ToString());
        }
        else
        {
            objHash = objHashRule.Sys_Hash.First(p => p.HashID == HashID);
            objHash.HashValue = txtHashValue.Text;
            objHash.HashKey = txtHashKey.Text;
            objHash.Remark = txtRemark.Text;
            objHashRule.SaveChanges();
            MessageDialog("保存成功", "../../ServiceLayer/HashType/HashList.aspx?HashTypeID=" + HashTypeID.ToString());
        }

    }


    public override void RenderPage()
    {
        if (HashID > 0)
        {
            objHash = objHashRule.Sys_Hash.First(p => p.HashID == HashID);
            Page.DataBind();

        }
    }

    protected override void CurrentTool_ItemCommand(object sender, WTF.Controls.MyCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "Save":
                SaveInfo();
                break;
            case "Back":
                Redirect("../../ServiceLayer/HashType/HashList.aspx?HashTypeID=" + HashTypeID.ToString());
                break;

        }

    }


}
