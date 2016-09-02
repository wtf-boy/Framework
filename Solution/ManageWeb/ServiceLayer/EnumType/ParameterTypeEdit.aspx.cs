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
public partial class ServiceLayer_EnumType_ParameterTypeEdit : SupportPageBase
{
    public Sys_ParameterType objParameterType;
    public ParameterRule objParameterRule = new ParameterRule();
    protected void Page_Load(object sender, EventArgs e)
    {
    }
    public int ParameterTypeID
    {
        get
        {
            return GetInt("ParameterTypeID");
        }
    }

    public override void RenderPage()
    {
        if (ParameterTypeID.IsNoNull())
        {
            objParameterType = objParameterRule.Sys_ParameterType.First(p => p.ParameterTypeID == ParameterTypeID);

          
            Page.DataBind();

        }
    }

    public void SaveInfo()
    {
        if (ParameterTypeID.IsNoNull())
        {
            objParameterType = objParameterRule.Sys_ParameterType.First(p => p.ParameterTypeID == ParameterTypeID);
           


            if (objParameterRule.Sys_ParameterType.Any(s => s.ParameterTypeID != ParameterTypeID && s.ParameterTypeCode == txtParameterTypeCode.Text))
            {
                MessageDialog("代码已经存在无法修改");
                return;
            }
            objParameterType.ParameterTypeCode = txtParameterTypeCode.Text;
            objParameterType.Remark = txtRemark.Text;
            objParameterType.ParameterTypeName = txtParameterTypeName.Text;
            objParameterType.TypeName = txtTypeName.Text;
            objParameterType.AssemblyName = txtAssemblyName.Text;
            objParameterRule.SaveChanges();
            MessageDialog("修改成功", "../../ServiceLayer/EnumType/ParameterTypeList.aspx");

        }
        else
        {
            objParameterType = new Sys_ParameterType();
            objParameterType.ParameterTypeCode = txtParameterTypeCode.Text;
            objParameterType.Remark = txtRemark.Text;
            objParameterType.ParameterTypeName = txtParameterTypeName.Text;
            objParameterType.TypeName = txtTypeName.Text;
            objParameterType.AssemblyName = txtAssemblyName.Text;
            objParameterRule.InsertParameterType(objParameterType);
            MessageDialog("新增成功", "../../ServiceLayer/EnumType/ParameterTypeList.aspx");

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
                Redirect("../../ServiceLayer/EnumType/ParameterTypeList.aspx");
                break;
        }

    }
}
