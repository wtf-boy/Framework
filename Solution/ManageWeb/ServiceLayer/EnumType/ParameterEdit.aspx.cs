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
using WTF.DataConfig;
using WTF.Framework;
using WTF.DataConfig.Entity;
public partial class ServiceLayer_EnumType_ParameterEdit : SupportPageBase
{
    public int ParameterID
    {
        get
        {

            return GetInt("ParameterID");
        }
    }

    public Sys_Parameter objParameter;
    public ParameterRule objParameterRule = new ParameterRule();
    public int ParameterTypeID
    {
        get
        {
            return GetInt("ParameterTypeID");
        }
    }

    public void SaveInfo()
    {


        if (ParameterID.IsNull())
        {
            objParameter = new Sys_Parameter();
            objParameter.ParameterTypeID = ParameterTypeID;
            AutoObjectSetValue(objParameter);
            objParameterRule.InsertParameter(objParameter);

            MessageDialog("添加成功", "../../ServiceLayer/EnumType/ParameterList.aspx?ParameterTypeID=" + ParameterTypeID.ToString());
        }
        else
        {
            objParameter = objParameterRule.Sys_Parameter.First(p => p.ParameterID == ParameterID);
            AutoObjectSetValue(objParameter);
            objParameterRule.SaveChanges();
            MessageDialog("保存成功", "../../ServiceLayer/EnumType/ParameterList.aspx?ParameterTypeID=" + ParameterTypeID.ToString());
        }

    }

    public override void RenderPage()
    {
        if (ParameterID.IsNoNull())
        {
            objParameter = objParameterRule.Sys_Parameter.FirstOrDefault(p => p.ParameterID == ParameterID);
            if (CheckEditObjectIsNull(objParameter)) return;
            Page.DataBind();

        }
        else
        {

            IsEnable.Checked = true;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected override void CurrentTool_ItemCommand(object sender, WTF.Controls.MyCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "Save":
                SaveInfo();
                break;
            case "Back":

                Redirect("../../ServiceLayer/EnumType/ParameterList.aspx?ParameterTypeID=" + ParameterTypeID.ToString());

                break;

        }

    }
}
