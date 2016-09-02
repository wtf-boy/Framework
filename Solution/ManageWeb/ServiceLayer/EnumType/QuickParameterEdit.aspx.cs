using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using WTF.DataConfig;
using WTF.Framework;
using WTF.DataConfig.Entity;
public partial class ServiceLayer_EnumType_QuickParameterEdit : SupportPageBase
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

        foreach (string ParameterName in txtParameterName.Text.Split(','))
        {
            objParameter = new Sys_Parameter();
            objParameter.ParameterTypeID = ParameterTypeID;
            objParameter.ParameterName = ParameterName;
            objParameter.ParameterCode = ParameterName;
            objParameter.ParameterCodeID = 0;
            objParameter.IsEnable = true;
            objParameter.Remark = "";
            objParameterRule.InsertParameter(objParameter);
        }

        MessageDialog("添加成功", "../../ServiceLayer/EnumType/ParameterList.aspx?ParameterTypeID=" + ParameterTypeID.ToString());


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