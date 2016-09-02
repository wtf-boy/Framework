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
using WTF.Resource.Entity;
using WTF.Resource;
using WTF.Framework;
public partial class ServiceLayer_Resource_ResourceTypeEdit : SupportPageBase
{

    public Sys_ResourceType objResourceType;
    private ResourceRule objResourceRule = new ResourceRule();
   
    public int ResourceTypeID
    {
        get
        {
            return GetInt("ResourceTypeID");
        }
    }

    public override void RenderPage()
    {
        dropStorageModeCodeType.Items.Clear();

        foreach (Sys_ResourcePath objSys_ResourcePath in objResourceRule.Sys_ResourcePath)
        {
            dropStorageModeCodeType.Items.Add(new ListItem(objSys_ResourcePath.ResourcePathName, objSys_ResourcePath.ResourcePathID.ToString()));
        }
        if (ResourceTypeID.IsNoNull())
        {
            objResourceType = objResourceRule.Sys_ResourceType.First(r => r.ResourceTypeID == ResourceTypeID);
            dropStorageModeCodeType.SelectedValue = objResourceType.ResourcePathID.ToString();
           
            radAccessModeCodeType.SelectedValue = objResourceType.AccessModeCodeType.ToString();
            radPathFormatCodeType.SelectedValue = objResourceType.PathFormatCodeType.ToString();
            radStorageTypeList.SelectedValue = objResourceType.StorageType.ToString();
            Page.DataBind();
        }

    }

    public void SaveInfo()
    {
        if (ResourceTypeID.IsNull())
        {

            objResourceType = new Sys_ResourceType();
          
            objResourceType.ResourceTypeCode = txtResourceTypeCode.Text;
            objResourceType.ResourceTypeName = txtResourceTypeName.Text;
            objResourceType.ResourcePathID = dropStorageModeCodeType.SelectedValue;
            objResourceType.AccessModeCodeType = radAccessModeCodeType.SelectedValue.ConvertInt();
            objResourceType.PathFormatCodeType = radPathFormatCodeType.SelectedValue.ConvertInt();
            objResourceType.StorageType = radStorageTypeList.SelectedValue.ConvertInt();
            objResourceRule.InsertResourceType(objResourceType);
            MessageDialog("新增成功", "../../ServiceLayer/Resource/ResourceTypeList.aspx");
        }
        else
        {
            objResourceType = objResourceRule.Sys_ResourceType.First(r => r.ResourceTypeID == ResourceTypeID);
            objResourceType.ResourceTypeCode = txtResourceTypeCode.Text;
            objResourceType.StorageType = radStorageTypeList.SelectedValue.ConvertInt();
            objResourceType.ResourceTypeName = txtResourceTypeName.Text;
            objResourceType.ResourcePathID = dropStorageModeCodeType.SelectedValue;
            objResourceType.AccessModeCodeType = radAccessModeCodeType.SelectedValue.ConvertInt();
            objResourceType.PathFormatCodeType = radPathFormatCodeType.SelectedValue.ConvertInt();
            objResourceRule.SaveChanges();
            MessageDialog("修改成功", "../../ServiceLayer/Resource/ResourceTypeList.aspx");

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
                Redirect("../../ServiceLayer/Resource/ResourceTypeList.aspx");

                break;
        }
    }
}
