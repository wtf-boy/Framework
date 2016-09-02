<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/EditPage.master" AutoEventWireup="true"
    CodeFile="UserEdit.aspx.cs" Inherits="SystemSafe_Power_UserEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphTopToolbar" runat="Server">
    <WTF:MyToolbar ID="MyTopToolBar" runat="server"
        OperatePlaceTypeValue="OperateTopBar" OnItemCommand="CurrentTool_ItemCommand" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphContent" runat="Server">
    <table class="tblContent">
        <colgroup>
            <col class="colTitle" />
            <col class="colContent" />
        </colgroup>
        <tr class="trCaption">
            <td colspan="2">用户基本信息
            </td>
        </tr>
        <tr>
            <td>
                <span class="txtNoNull">*</span>用户类型：
            </td>
            <td>
                <WTF:MyDropDownList ID="dropUserTypeCID" runat="server">
                </WTF:MyDropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <span class="txtNoNull">*</span>用户名：
            </td>
            <td>
                <WTF:MyTextBox ID="txtUserName" CheckValueEmpty="true" ValidationGroup="SaveGroup" MaxCharLength="100"
                    ErrorMessage="请输入用户名" runat="server" Text="<%# objUser.UserName %>"></WTF:MyTextBox>
            </td>
        </tr>
        <tr>
            <td>昵称：
            </td>
            <td>
                <WTF:MyTextBox ID="txtNickName" MaxLength="100"
                    ErrorMessage="请输入昵称" runat="server" Text="<%# objUser.NickName %>"></WTF:MyTextBox>
            </td>
        </tr>
        <tr>
            <td>工号：
            </td>
            <td>
                <WTF:MyTextBox ID="txtJobNo" MaxLength="100"
                    ErrorMessage="请输入工号" runat="server" Text="<%# objUser.JobNo %>"></WTF:MyTextBox>
            </td>
        </tr>
        <tr class="trCaption">
            <td colspan="2">帐户基本信息
            </td>
        </tr>
        <tr>
            <td>
                <span class="txtNoNull">*</span>用户帐号：
            </td>
            <td>
                <WTF:MyTextBox ID="txtAccount" CheckValueEmpty="true" ValidationGroup="SaveGroup"
                    ValidationExpression="[a-zA-Z0-9_]{2,15}" HintMessage="2~15位英文,数字,下划线组成" ErrorMessage="请输入正确用户帐号(2~15位英文,数字,下划线组成)" runat="server" Text="<%# objUser.Account %>"></WTF:MyTextBox>
            </td>
        </tr>
        <tr id="trPassword" runat="server">
            <td>初始密码：
            </td>
            <td>
                <WTF:MyTextBox ID="txtPassword" TextMode="Password" runat="server"
                    ValidationGroup="SaveGroup" MinCharLength="6" MaxCharLength="20" HintMessage="6~20位字符组成"
                    ErrorMessage="请输入正确密码(6~20位字符组成)"></WTF:MyTextBox>默认7777777
            </td>
        </tr>
        <tr id="trtTruePassword" runat="server">
            <td>确认新密码：
            </td>
            <td>
                <WTF:MyTextBox ID="txtTruePassword" ValidationGroup="SaveGroup"
                    ErrorMessage="对不起，确认新密码与新密码不同" TextMode="Password" runat="server" ControlToCompare="txtPassword"></WTF:MyTextBox>
            </td>
        </tr>
        <tr>
            <td>Email：
            </td>
            <td>
                <WTF:MyTextBox ID="txtEmail" runat="server" Text="<%# objUser.Email  %>" ValidationGroup="SaveGroup" ErrorMessage="请输入正确的电子邮件" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></WTF:MyTextBox>
            </td>
        </tr>
        <tr class="trCaption">
            <td colspan="2">帐户角色
            </td>
        </tr>
        <tr>
            <td>授权角色：
            </td>
            <td>
                <WTF:MyCheckBoxList ID="chkRoleList" runat="server" RepeatColumns="5" RepeatDirection="Horizontal" ValidationGroup="SaveGroup">
                </WTF:MyCheckBoxList>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphScriptcbar" runat="Server">
</asp:Content>
