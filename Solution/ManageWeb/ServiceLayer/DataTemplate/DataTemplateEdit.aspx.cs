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
public partial class ServiceLayer_DataTemplate_DataTemplateEdit : SupportPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public int DataTemplateID
    {
        get
        {

            return GetInt("DataTemplateID");
        }
    }

    public Sys_DataTemplate objDataTemplate;
    public DataTemplateRule objDataTemplateRule = new DataTemplateRule();

    public int DataTemplateTypeID
    {
        get
        {
            return GetInt("DataTemplateTypeID");
        }
    }


    public void SaveInfo()
    {

        if ( DataTemplateID.IsNull())
        {

            objDataTemplate = new Sys_DataTemplate();

            objDataTemplate.DataTemplateTypeID = DataTemplateTypeID;
        
            objDataTemplate.TemplateContent = txtTemplateContent.Text;
            objDataTemplate.TemplateCode = txtTemplateCode.Text;
            objDataTemplate.TemplateName = txtTemplateName.Text;
            objDataTemplate.Remark = txtRemark.Text;
            objDataTemplateRule.InsertDataTemplate( objDataTemplate);
            MessageDialog("添加成功", "../../ServiceLayer/DataTemplate/DataTemplateList.aspx?DataTemplateTypeID=" + DataTemplateTypeID.ToString());
        }
        else
        {
            objDataTemplate = objDataTemplateRule.Sys_DataTemplate.First(p => p.DataTemplateID == DataTemplateID);
            objDataTemplate.TemplateContent = txtTemplateContent.Text;
            objDataTemplate.TemplateCode = txtTemplateCode.Text;
            objDataTemplate.TemplateName = txtTemplateName.Text;
            objDataTemplate.Remark = txtRemark.Text;
            objDataTemplateRule.SaveChanges();
            MessageDialog("保存成功", "../../ServiceLayer/DataTemplate/DataTemplateList.aspx?DataTemplateTypeID=" + DataTemplateTypeID.ToString());
        }

    }


    public override void RenderPage()
    {
        if (DataTemplateID.IsNoNull())
        {

            objDataTemplate = objDataTemplateRule.Sys_DataTemplate.First(p => p.DataTemplateID == DataTemplateID);
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

                Redirect("../../ServiceLayer/DataTemplate/DataTemplateList.aspx?DataTemplateTypeID=" + DataTemplateTypeID.ToString());

                break;

        }

    }
}
