<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/EditPage.master" AutoEventWireup="true"
    CodeFile="UserPasswordEdit.aspx.cs" Inherits="SystemSafe_User_UserPasswordEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphTopToolbar" runat="Server">
    <WTF:MyToolbar ID="MyTopToolBar" runat="server" ModuleCode="" OperatePlaceTypeValue="OperateTopBar"
        OnItemCommand="CurrentTool_ItemCommand" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphContent" runat="Server">
    <table class="tblContent">
        <colgroup>
            <col class="colTitle" />
            <col class="colContent" />
        </colgroup>
        <tr class="trCaption">
            <td colspan="2">
                修改用户密码
            </td>
        </tr>
        <tr>
            <td>
                <span class="txtNoNull">*</span>用户帐号：
            </td>
            <td>
                <WTF:MyTextBox ID="txtAccount" runat="server" Enabled="false"></WTF:MyTextBox>
            </td>
        </tr>
              <tr>
            <td>
                <span class="txtNoNull">*</span>旧密码：
            </td>
            <td>
                <WTF:MyTextBox ID="txtOldPassword" CheckValueEmpty="true" ValidationGroup="SaveGroup"
                    ErrorMessage="请输入旧密码" TextMode="Password" runat="server"></WTF:MyTextBox>
            </td>
        </tr>
        <tr>
            <td>
                <span class="txtNoNull">*</span>新密码：
            </td>
            <td>
                <WTF:MyTextBox ID="txtPassword" CheckValueEmpty="true" ValidationGroup="SaveGroup"
                   MinCharLength="6" MaxCharLength="20" HintMessage="6~20位字符组成" ErrorMessage="请输入正确新密码(6~20位字符组成)" TextMode="Password" runat="server"></WTF:MyTextBox>
            </td>
        </tr>
        <tr>
            <td>
                <span class="txtNoNull">*</span>确认新密码：
            </td>
            <td>
                <WTF:MyTextBox ID="txtTruePassword" CheckValueEmpty="true" ValidationGroup="SaveGroup"
                    ErrorMessage="对不起，确认新密码与新密码不同" TextMode="Password" runat="server" ControlToCompare="txtPassword"></WTF:MyTextBox>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphScriptcbar" runat="Server">
</asp:Content>
