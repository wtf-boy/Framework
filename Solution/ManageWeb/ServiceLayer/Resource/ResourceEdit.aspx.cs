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
using WTF.Resource.Entity;
using WTF.Framework;
public partial class ServiceLayer_Resource_ResourceEdit : SupportPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    public Sys_Resource objResource = new Sys_Resource();

    public ResourceRule objResourceRule = new ResourceRule();
    public int ResourceTypeID
    {
        get
        {


            return GetInt("ResourceTypeID");
        }
    }

    public void SaveInfo()
    {

        objResourceRule.InsertResource(txtResourceName.Text, ResourceTypeID);

    }

    protected override void CurrentTool_ItemCommand(object sender, WTF.Controls.MyCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "Save":
                SaveInfo();
                Redirect("../../ServiceLayer/Resource/ResourceList.aspx?ResourceTypeID=" + ResourceTypeID.ToString());
                break;
            case "Back":
                Redirect("../../ServiceLayer/Resource/ResourceList.aspx?ResourceTypeID=" + ResourceTypeID.ToString());
                break;
        }
    }
}
