using WTF.Power;
using WTF.Power.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WTF.Framework;
public partial class ServiceLayer_Authorize_GroupEdit : SupportPageBase
{
    /// <summary>
    /// 获取授权组标识
    /// </summary>
    public string AuthorizeGroupID
    {
        get
        {
            return GetString("AuthorizeGroupID");

        }

    }
    /// <summary>
    /// 变量
    /// </summary>
    public sys_authorizegroup objsys_authorizegroup = new sys_authorizegroup();
    UserRule objUserRule = new UserRule();

    public override void InitDataPage()
    {
        ModuleRule objModuleRule = new ModuleRule();
        dropModuleTypeID.DataSource = objModuleRule.Sys_ModuleType.Where(s => s.IsSystem == true);
        dropModuleTypeID.DataTextField = "ModuleTypeName";
        dropModuleTypeID.DataValueField = "ModuleTypeID";
        dropModuleTypeID.DataBind();
    }
    /// <summary>
    /// 页面加载
    /// </summary>
    public override void RenderPage()
    {

        if (AuthorizeGroupID.IsNoNull())
        {
            objsys_authorizegroup = objUserRule.sys_authorizegroup.FirstOrDefault(s => s.AuthorizeGroupID == AuthorizeGroupID);
            if (CheckEditObjectIsNull(objsys_authorizegroup)) return;
            ///平台类型
            dropModuleTypeID.SelectedValue = objsys_authorizegroup.ModuleTypeID.ToString();
            ///是否是超管组
            chkIsSupertGroup.Checked = objsys_authorizegroup.IsSupertGroup;
            chkIsRevertPower.Checked = objsys_authorizegroup.IsRevertPower;
            chkIsAllowPowerSelf.Checked = objsys_authorizegroup.IsAllowPowerSelf;
            Page.DataBind();
        }
        else
        {
        }

    }
    /// <summary>
    /// 保存界面上的值
    /// </summary>
    /// <param name="objsys_authorizegroup"></param>
    public void SaveValue(sys_authorizegroup objsys_authorizegroup)
    {
        ///授权组名
        objsys_authorizegroup.AuthorizeGroupName = txtAuthorizeGroupName.TextCutWord(50);
        ///平台类型
        objsys_authorizegroup.ModuleTypeID = dropModuleTypeID.SelectedValue;
        ///是否是超管组
        objsys_authorizegroup.IsSupertGroup = chkIsSupertGroup.Checked;
        ///备注
        objsys_authorizegroup.Remark = txtRemark.TextCutWord(100);
        objsys_authorizegroup.IsRevertPower = chkIsRevertPower.Checked;
        objsys_authorizegroup.IsAllowPowerSelf = chkIsAllowPowerSelf.Checked;
    }
    /// <summary>
    /// 保存信息
    /// </summary>
    public void SaveInfo()
    {
        if (AuthorizeGroupID.IsNull())
        {

            objsys_authorizegroup.AuthorizeGroupID = Guid.NewGuid().ToString();
            SaveValue(objsys_authorizegroup);
            objUserRule.Insertauthorizegroup(objsys_authorizegroup);
            MessageDialog("新增成功", "GroupList.aspx");
        }
        else
        {
            objsys_authorizegroup = objUserRule.sys_authorizegroup.FirstOrDefault(p => p.AuthorizeGroupID == AuthorizeGroupID);
            if (CheckEditObjectIsNull(objsys_authorizegroup)) return;
            SaveValue(objsys_authorizegroup);
            objUserRule.Updateauthorizegroup(objsys_authorizegroup);
            MessageDialog("修改成功", "GroupList.aspx");
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
                Redirect("GroupList.aspx");
                break;
        }

    }

}