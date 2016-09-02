using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WTF.Framework;
using WTF.Resource;
using WTF.Resource.Entity;
using WTF.Logging;
public partial class App_Control_Xheditor_xheditor_Seven_resource_DialogFile : XheditorResourceBase
{
    #region 资源表唯一标识属性
    /// <summary>
    /// 资源表唯一标识属性
    /// </summary>
    public string ResourceID
    {
        get
        {

            return hidResourceID.Value;
        }
        set
        {
            if (hidResourceID.Value.IsNull())
            {
                ScriptHelper.RegisterScript(" updateResourceClientValue('" + ResourceClientID + "','" + value.ToString() + "');", RegisterType.StartupScript);
                hidResourceID.Value = value.ToString();
            }
        }
    }
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            hidResourceID.Value = RequestHelper.GetString("ResourceID");
            fupFile.ResourceTypeID = ResourceTypeID;
            fupFile.ResourceID = ResourceID;
            fupFile.RestrictCode = RestrictCode;

            DataBindInfo();
        }

    }
    ResourceRule objResourceRule = new ResourceRule();
    public void DataBindInfo()
    {
        if (ResourceID.IsNoNull())
        {
            Sys_ResourceRestrict objSys_ResourceRestrict = objResourceRule.Sys_ResourceRestrict.Where(s => s.ResourceTypeID == ResourceTypeID && s.RestrictCode == RestrictCode).FirstOrDefault();
            if (objSys_ResourceRestrict.IsNoNull())
            {
                rptResource.DataSource = objResourceRule.Sys_ResourceVer.Where(s => s.ResourceID == ResourceID && s.VerNo >= objSys_ResourceRestrict.BeginVerNo && s.VerNo <= objSys_ResourceRestrict.EndVerNo).OrderByDescending(s => s.VerNo);

                rptResource.DataBind();
            }
        }
    }

    #region 删除附件事件
    /// <summary>
    /// 删除附件事件
    /// </summary>
    /// <param name="sender">事件对象</param>
    /// <param name="e">事件参数</param>
    protected void btnRemove_Click(object sender, EventArgs e)
    {
        objResourceRule.DeleteResource(ResourceID, ((ImageButton)sender).CommandArgument.ToString());
        DataBindInfo();

    }
    #endregion
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (fupFile.HasFile)
            {
                fupFile.Save();
                txtLink.Text = fupFile.FileName;
                ResourceID = fupFile.ResourceID;
                if (fupFile.MoveResourceInfo.IsNull())
                {
                    txtSelectUrl.Text = fupFile.ResourcePath;
                }
                else
                {
                    txtSelectUrl.Text = fupFile.MoveResourceInfo[0].ResourcePath;
                }


                DataBindInfo();
            }
            else
            {
                ScriptHelper.MessageDialog("请选择要上传的文件");
            }
        }
        catch (Exception ojbExp)
        {
            LogHelper.DisposeException(LogModuleType, ojbExp);
        }
    }
}