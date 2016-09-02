using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WTF.Framework;
using WTF.Resource;
using WTF.Resource.Entity;
public partial class ServiceLayer_Resource_ResourceRestrictEdit : SupportPageBase
{


    /// <summary>
    /// 标识
    /// </summary>
    public int ResourceRestrictID
    {
        get
        {
            return GetInt("ResourceRestrictID");
        }
    }
    public int ResourceTypeID
    {
        get
        {
            return GetInt("ResourceTypeID");
        }
    }

    public Sys_ResourceRestrict objSys_ResourceRestrict = new Sys_ResourceRestrict();
    ResourceRule objResourceRule = new ResourceRule();
    /// <summary>
    /// 页面初始化
    /// </summary>
    public override void RenderPage()
    {
        if (ResourceRestrictID.IsNoNull())
        {

            objSys_ResourceRestrict = objResourceRule.Sys_ResourceRestrict.First(s => s.ResourceRestrictID == ResourceRestrictID);

            radRestrictType.SelectedValue = objSys_ResourceRestrict.RestrictType.ToString();
            Page.DataBind();

        }
        else
        {

            txtVerNo.Text = "0";
            txtBeginVerNo.Text = "1";
            txtEndVerNo.Text = "5000";
          
            txtFileMaxSize.Text = "5120";
            radRestrictType.SelectedValue = "1";

        }
    }

    /// <summary>
    /// 保存信息
    /// </summary>
    public void SaveInfo()
    {
        if (ResourceRestrictID.IsNull())
        {

            objSys_ResourceRestrict.RestrictCode = txtRestrictCode.Text;
            objSys_ResourceRestrict.RestrictName = txtRestrictName.Text;
            objSys_ResourceRestrict.RestrictType = radRestrictType.SelectValueInt;
            objSys_ResourceRestrict.VerNo = txtVerNo.TextInt;
            objSys_ResourceRestrict.BeginVerNo = txtBeginVerNo.TextInt;
            objSys_ResourceRestrict.EndVerNo = txtEndVerNo.TextInt;
            objSys_ResourceRestrict.FileExtension = txtFileExtension.Text;
            objSys_ResourceRestrict.FileMaxSize = txtFileMaxSize.TextInt;
            objSys_ResourceRestrict.ResourceTypeID = ResourceTypeID;
            if (objSys_ResourceRestrict.VerNo != 0)
            {
                objSys_ResourceRestrict.BeginVerNo = 0;
                objSys_ResourceRestrict.EndVerNo = 0;
            }
            objResourceRule.CurrentEntities.AddTosys_resourcerestrict(objSys_ResourceRestrict);
            objResourceRule.SaveChanges();
            MessageDialog("新增成功", "ResourceRestrictList.aspx?ResourceTypeID=" + ResourceTypeID);

        }
        else
        {
            objSys_ResourceRestrict = objResourceRule.Sys_ResourceRestrict.First(s => s.ResourceRestrictID == ResourceRestrictID);
            objSys_ResourceRestrict.RestrictCode = txtRestrictCode.Text;
            objSys_ResourceRestrict.RestrictName = txtRestrictName.Text;
            objSys_ResourceRestrict.RestrictType = radRestrictType.SelectValueInt;
            objSys_ResourceRestrict.VerNo = txtVerNo.TextInt;
            objSys_ResourceRestrict.BeginVerNo = txtBeginVerNo.TextInt;
            objSys_ResourceRestrict.EndVerNo = txtEndVerNo.TextInt;
            objSys_ResourceRestrict.FileExtension = txtFileExtension.Text;
            objSys_ResourceRestrict.FileMaxSize = txtFileMaxSize.TextInt;
            objSys_ResourceRestrict.ResourceTypeID = ResourceTypeID;
            if (objSys_ResourceRestrict.VerNo != 0)
            {
                objSys_ResourceRestrict.BeginVerNo = 0;
                objSys_ResourceRestrict.EndVerNo = 0;
            }
            objResourceRule.SaveChanges();
            MessageDialog("修改成功", "ResourceRestrictList.aspx?ResourceTypeID=" + ResourceTypeID);

        }

    }

    /// <summary>
    /// 操作栏
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected override void CurrentTool_ItemCommand(object sender, WTF.Controls.MyCommandEventArgs e)
    {

        switch (e.CommandName)
        {
            case "Save":
                SaveInfo();
                break;
            case "Back":
                Redirect("ResourceRestrictList.aspx?ResourceTypeID=" + ResourceTypeID);
                break;
        }
    }

}