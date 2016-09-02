using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WTF.Power;
using WTF.Power.Entity;
using WTF.Framework;
using WTF.Resource;
using WTF.Logging;

using WTF.Logging.Entity;
using WTF.DataConfig;
using WTF.DataConfig.Entity;
public partial class ServiceLayer_Module_ModuleDataEdit : SupportPageBase
{
    /// <summary>
    /// 获取模块数据标识
    /// </summary>
    public string ModuleDataID
    {
        get
        {
            return GetString("ModuleDataID");

        }

    }


    public int IsModuleType
    {
        get
        {
            return GetInt("IsModuleType");
        }
    }
    /// <summary>
    /// 获取模块标识
    /// </summary>
    public string ModuleID
    {
        get
        {
            return GetString("ModuleID");

        }

    }
    /// <summary>
    /// 变量
    /// </summary>
    public Sys_ModuleData objSys_ModuleData = new Sys_ModuleData();
    ModuleRule objModuleRule = new ModuleRule();

    public override void InitDataPage()
    {

        HashRule objHashRule = new HashRule();
        dropConnectionKey.Items.Clear();
        dropConnectionKey.Items.Add(new ListItem("--请选择--", ""));
        foreach (var objSys_Hash in objHashRule.Sys_Hash.Where(s => s.Sys_HashType.HashTypeCode == "ConnectionType"))
        {
            dropConnectionKey.Items.Add(new ListItem(objSys_Hash.HashKey, objSys_Hash.HashValue));
        }
    }
    /// <summary>
    /// 页面加载
    /// </summary>
    public override void RenderPage()
    {



        if (ModuleDataID.IsNoNull())
        {
            objSys_ModuleData = objModuleRule.Sys_ModuleData.First(s => s.ModuleDataID == ModuleDataID);
            ///连接串Key值
            dropConnectionKey.SelectedValue = objSys_ModuleData.ConnectionKey.ToString();
            radFieldSourceType.SelectedValue = objSys_ModuleData.FieldSourceType.ToString();
            ///1int2字符串3bit4全球唯一码
            radFieldType.SelectedValue = objSys_ModuleData.FieldType.ToString();
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
        if (ModuleDataID.IsNull())
        {
            objSys_ModuleData.ModuleDataID = Guid.NewGuid().ToString();
            objSys_ModuleData.ModuleID = ModuleID;
            ///连接串Key值
            objSys_ModuleData.ConnectionKey = dropConnectionKey.SelectedValue;
            ///数据名称
            objSys_ModuleData.DataName = txtDataName.TextCutWord(50);
            ///字段名
            objSys_ModuleData.FieldName = txtFieldName.TextCut(50);
            ///数据查询
            objSys_ModuleData.DataSelect = txtDataSelect.TextCut(1000);

            ///1全局2列表
            objSys_ModuleData.PowerType = IsModuleType;
            ///1int2字符串3bit4全球唯一码
            objSys_ModuleData.FieldType = radFieldType.SelectValueInt;

            objSys_ModuleData.FieldSourceType = radFieldSourceType.SelectValueInt;
            objModuleRule.InsertModuleData(objSys_ModuleData);
            MessageDialog("新增成功", "ModuleDataList.aspx?ModuleID=" + ModuleID);
        }
        else
        {
            objSys_ModuleData = objModuleRule.Sys_ModuleData.First(p => p.ModuleDataID == ModuleDataID);
            ///连接串Key值
            objSys_ModuleData.ConnectionKey = dropConnectionKey.SelectedValue;
            ///数据名称
            objSys_ModuleData.DataName = txtDataName.TextCutWord(50);
            ///字段名
            objSys_ModuleData.FieldName = txtFieldName.TextCut(50);
            ///数据查询
            objSys_ModuleData.DataSelect = txtDataSelect.TextCut(1000);

            ///1int2字符串3bit4全球唯一码
            objSys_ModuleData.FieldType = radFieldType.SelectValueInt;
            objSys_ModuleData.FieldSourceType = radFieldSourceType.SelectValueInt;
            objModuleRule.UpdateModuleData(objSys_ModuleData);
            MessageDialog("修改成功", "ModuleDataList.aspx?ModuleID=" + ModuleID);
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
                Redirect("ModuleDataList.aspx?ModuleID=" + ModuleID);
                break;
        }

    }

}