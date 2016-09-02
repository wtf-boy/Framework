using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WTF.Power.Entity;
using WTF.Power;
using WTF.Framework;
public partial class ServiceLayer_User_UserTypeEdit : SupportPageBase
{
    /// <summary>
    /// 获取用户类型标识
    /// </summary>
    public int UserTypeID
    {
        get
        {
            return GetInt("UserTypeID");

        }

    }
    /// <summary>
    /// 变量
    /// </summary>
    public Sys_UserType objSys_UserType = new Sys_UserType();
    UserRule objUserRule = new UserRule();
    /// <summary>
    /// 页面加载
    /// </summary>
    public override void RenderPage()
    {

        if (UserTypeID.IsNoNull())
        {
            objSys_UserType = objUserRule.Sys_UserType.First(s => s.UserTypeID == UserTypeID);
            txtUserTypeID.Enabled = false;
            Page.DataBind();
        }
        else
        {
            txtUserTypeID.Enabled = true;
        }

    }
    /// <summary>
    /// 保存信息
    /// </summary>
    public void SaveInfo()
    {
        if (UserTypeID.IsNull())
        {
            objSys_UserType.UserTypeID = txtUserTypeID.TextInt;
            ///用户类型名称
            objSys_UserType.UserTypeName = txtUserTypeName.TextCutWord(50);
            ///备注
            objSys_UserType.Remark = txtRemark.TextCutWord(100);
            objUserRule.InsertUserType(objSys_UserType);
            MessageDialog("新增成功", "UserTypeList.aspx");
        }
        else
        {
            objSys_UserType = objUserRule.Sys_UserType.First(p => p.UserTypeID == UserTypeID);
            ///用户类型名称
            objSys_UserType.UserTypeName = txtUserTypeName.TextCutWord(50);
            ///备注
            objSys_UserType.Remark = txtRemark.TextCutWord(100);
            objUserRule.UpdateUserType(objSys_UserType);
            MessageDialog("修改成功", "UserTypeList.aspx");
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
                Redirect("UserTypeList.aspx");
                break;
        }

    }

}