using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WTF.Framework;
using WTF.DataConfig;
using WTF.DataConfig.Entity;
public partial class ServiceLayer_Data_DataTypeEdit : SupportPageBase
{
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
    public Sys_DataType objSys_DataType = new Sys_DataType();
    DataRule objDataRule = new DataRule();
    /// <summary>
    /// 页面加载
    /// </summary>
    public override void RenderPage()
    {

        if (DataTypeID.IsNoNull())
        {
            objSys_DataType = objDataRule.Sys_DataType.First(s => s.DataTypeID == DataTypeID);

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
        if (DataTypeID.IsNull())
        {
            ///数据验证编码
            objSys_DataType.DataCode = txtDataCode.TextCut(50);
            ///数据验证名称
            objSys_DataType.DataName = txtDataName.TextCutWord(20);
            objDataRule.InsertDataType(objSys_DataType);
            MessageDialog("新增成功", "DataTypeList.aspx");
        }
        else
        {
            objSys_DataType = objDataRule.Sys_DataType.First(p => p.DataTypeID == DataTypeID);
            ///数据验证编码
            objSys_DataType.DataCode = txtDataCode.TextCut(50);
            ///数据验证名称
            objSys_DataType.DataName = txtDataName.TextCutWord(20);
            objDataRule.UpdateDataType(objSys_DataType);
            MessageDialog("修改成功", "DataTypeList.aspx");
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
                Redirect("DataTypeList.aspx");
                break;
        }

    }

}