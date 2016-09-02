<%@ Page Language="C#" MasterPageFile="~/App_Master/EditPage.master" AutoEventWireup="true"
    CodeFile="SupportUserEdit.aspx.cs" Inherits="ServiceLayer_User_SupportUserEdit"
    Title="无标题页" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphTopToolbar" runat="Server">
    <WTF:MyToolbar ID="MyTopToolBar" runat="server" ModuleCode="ServiceLayer_User_SupportUserEdit"
        OperatePlaceTypeValue="OperateTopBar" OnItemCommand="CurrentTool_ItemCommand" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphContent" runat="Server">
    <table class="tblContent">
        <colgroup>
            <col class="colTitle" />
            <col class="colContent" />
        </colgroup>
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
            <td>
                <span class="txtNoNull">*</span>用户帐号：
            </td>
            <td>

                <WTF:MyTextBox ID="txtAccount" CheckValueEmpty="true" ValidationGroup="SaveGroup"
                    ValidationExpression="[a-zA-Z0-9_]{2,15}" HintMessage="3~15位英文,数字,下划线组成" ErrorMessage="请输入正确用户帐号(3~15位英文,数字,下划线组成)" runat="server" Text="<%# objUser.Account %>"></WTF:MyTextBox>
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

        <tr>
            <td>初始密码：
            </td>
            <td>
                <WTF:MyTextBox ID="txtPassword" TextMode="Password" runat="server" CheckValueEmpty="true"
                    ValidationGroup="SaveGroup" MinCharLength="6" MaxCharLength="20" HintMessage="6~20位字符组成" ErrorMessage="请输入正确密码(6~20位字符组成)"></WTF:MyTextBox>
            </td>
        </tr>
        <tr>
            <td>Email：
            </td>
            <td>
                <WTF:MyTextBox ID="txtEmail" runat="server" ValidationGroup="SaveGroup" ErrorMessage="请输入正确Email"
                    Text="<%# objUser.Email  %>" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></WTF:MyTextBox>
            </td>
        </tr>
        <tr>
            <td>是否审核：
            </td>
            <td>
                <asp:CheckBox ID="chkIsApprove" runat="server" Checked="<%# objUser.IsApproved  %>" />
            </td>
        </tr>
        <tr>
            <td>是否主帐号：
            </td>
            <td>
                <asp:CheckBox ID="chkIsAdmin" runat="server" Checked="<%# objUser.IsAdmin  %>" />
            </td>
        </tr>

        <tr>
            <td>是否超管：
            </td>
            <td>
                <asp:CheckBox ID="chkIsSuper" runat="server" Checked="<%# objUser.IsSuper  %>" />
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphScriptcbar" runat="Server">
</asp:Content>
