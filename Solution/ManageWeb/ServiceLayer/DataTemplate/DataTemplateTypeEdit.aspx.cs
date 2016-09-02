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
public partial class ServiceLayer_DataTemplate_DataTemplateTypeEdit : SupportPageBase
{
    public int DataTemplateTypeID
    {
        get
        {
            return GetInt("DataTemplateTypeID");
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public Sys_DataTemplateType objDataTemplateType;
    public DataTemplateRule objDataTemplateRule = new DataTemplateRule();

    public override void RenderPage()
    {
        if (DataTemplateTypeID.IsNoNull())
        {
            objDataTemplateType = objDataTemplateRule.Sys_DataTemplateType.First(p => p.DataTemplateTypeID == DataTemplateTypeID);
            Page.DataBind();
        }
    }

    private void SaveInfo()
    {


        if (DataTemplateTypeID.IsNull())
        {
            objDataTemplateType = new Sys_DataTemplateType();
            objDataTemplateType.DataTemplateTypeCode = txtDataTemplateTypeCode.Text;
            objDataTemplateType.DataTemplateTypeName = txtDataTemplateTypeName.Text;
            objDataTemplateType.Remark = txtRemark.Text;
            objDataTemplateRule.InsertDataTemplateType(objDataTemplateType);
            MessageDialog("新增成功！", "../../ServiceLayer/DataTemplate/DataTemplateTypeList.aspx");

        }
        else
        {
            objDataTemplateType = objDataTemplateRule.Sys_DataTemplateType.First(p => p.DataTemplateTypeID == DataTemplateTypeID);
            if (objDataTemplateRule.Sys_DataTemplateType.Any(s => s.DataTemplateTypeID != DataTemplateTypeID && s.DataTemplateTypeCode == txtDataTemplateTypeCode.Text))
            {
                MessageDialog("代码已经存在无法修改");
                return;
            }
            objDataTemplateType.DataTemplateTypeCode = txtDataTemplateTypeCode.Text;
            objDataTemplateType.DataTemplateTypeName = txtDataTemplateTypeName.Text;
            objDataTemplateType.Remark = txtRemark.Text;
            objDataTemplateRule.SaveChanges();


            MessageDialog("修改成功！", "../../ServiceLayer/DataTemplate/DataTemplateTypeList.aspx");

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

                Redirect("../../ServiceLayer/DataTemplate/DataTemplateTypeList.aspx");
                break;
        }

    }
}
