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
using WTF.Resource;
using WTF.Framework;
public partial class ServiceLayer_Resource_ResourceVerEdit : SupportPageBase
{

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public ResourceRule objResourceRule = new ResourceRule();
    public int ResourceTypeID
    {
        get
        {
            return GetInt("ResourceTypeID");
        }
    }
    public string ResourceID
    {
        get
        {
            return GetString("ResourceID");

        }
    }

    public int VerNo
    {
        get
        {

            return GetInt("VerNo"); ;
        }
    }
    public override void RenderPage()
    {
        fupFile.ResourceID = ResourceID;
        if (VerNo.IsNoNull())
        {
            fupFile.VerNo = VerNo;

        }
    }

    public void SaveInfo()
    {
        if (fupFile.HasFile)
        {
            fupFile.Save();
        }
        else
        {
            MessageDialog("请选择要上传的文件");
            return;
        }
        Redirect("../../ServiceLayer/Resource/ResourceVerList.aspx?ResourceTypeID=" + ResourceTypeID.ToString() + "&ResourceID=" + ResourceID.ToString());

    }


   protected override void CurrentTool_ItemCommand(object sender, WTF.Controls.MyCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "Save":
                SaveInfo();

                break;
            case "Back":
                Redirect("../../ServiceLayer/Resource/ResourceVerList.aspx?ResourceTypeID=" + ResourceTypeID.ToString() + "&ResourceID=" + ResourceID.ToString());
                break;
        }
    }
}
