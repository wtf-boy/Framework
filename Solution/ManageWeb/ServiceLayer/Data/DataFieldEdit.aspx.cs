using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WTF.Framework;
using WTF.DataConfig;
using WTF.DataConfig.Entity;
public partial class ServiceLayer_Data_DataFieldEdit : SupportPageBase
{
    /// <summary>
    /// 获取数据验证标识
    /// </summary>
    public int DataFieldID
    {
        get
        {
            return GetInt("DataFieldID");

        }

    }
    /// <summary>
    /// 获取数据验证类型标识
    /// </summary>
    public int DataTypeID
    {
        get
        {
            return GetInt("DataTypeID");

        }

    }
    /// <summary>
    /// 变量
    /// </summary>
    public Sys_DataField objSys_DataField = new Sys_DataField();
    DataRule objDataRule = new DataRule();
    /// <summary>
    /// 页面加载
    /// </summary>
    public override void RenderPage()
    {

        if (DataFieldID.IsNoNull())
        {
            objSys_DataField = objDataRule.Sys_DataField.First(s => s.DataFieldID == DataFieldID);

            Page.DataBind();
        }
        else
        {
        }

    }
    /// <summary>
    /// 保存信息
    /// </summary>
    public void SaveInfo()
    {
        if (DataFieldID.IsNull())
        {
            objSys_DataField.DataTypeID = DataTypeID;
            ///数据值
            objSys_DataField.DataValue = txtDataValue.TextCut(36);
            ///数据名称
            objSys_DataField.DataTitle = txtDataTitle.TextCutWord(50);
            objDataRule.InsertDataField(objSys_DataField);
            MessageDialog("新增成功", "DataFieldList.aspx?DataTypeID=" + DataTypeID);
        }
        else
        {
            objSys_DataField = objDataRule.Sys_DataField.First(p => p.DataFieldID == DataFieldID);
            ///数据值
            objSys_DataField.DataValue = txtDataValue.TextCut(36);
            ///数据名称
            objSys_DataField.DataTitle = txtDataTitle.TextCutWord(50);
            objDataRule.UpdateDataField(objSys_DataField);
            MessageDialog("修改成功", "DataFieldList.aspx?DataTypeID=" + DataTypeID);
        }
    }

    /// <summary>
    /// 工具栏操作
    /// </summary>
    protected override void CurrentTool_ItemCommand(object sender, WTF.Controls.MyCommandEventArgs e)
    {

        switch (e.CommandName)
        {
            case "Save":
                SaveInfo();
                break;
            case "Back":
                Redirect("DataFieldList.aspx?DataTypeID=" + DataTypeID);
                break;
        }

    }

}