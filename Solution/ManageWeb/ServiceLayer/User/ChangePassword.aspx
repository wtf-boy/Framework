<%@ Page Language="C#" MasterPageFile="~/App_Master/EditPage.master" AutoEventWireup="true"
    CodeFile="ChangePassword.aspx.cs" Inherits="ServiceLayer_User_ChangePassword"
    Title="无标题页" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphTopToolbar" runat="Server">
    <WTF:MyToolbar ID="MyTopToolBar" runat="server" ModuleCode="ServiceLayer_User_ChangePassword"
        OperatePlaceTypeValue="OperateTopBar" OnItemCommand="CurrentTool_ItemCommand" />
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
                <WTF:MyTextBox ID="txtAccount" runat="server"  ></WTF:MyTextBox>
            </td>
        </tr>
        <tr>
            <td>
                <span class="txtNoNull">*</span>新密码：
            </td>
            <td>
                <WTF:MyTextBox ID="txtPassword" CheckValueEmpty="true" ValidationGroup="SaveGroup"   
                    ErrorMessage="请输入新密码" TextMode="Password" runat="server"></WTF:MyTextBox>
            </td>
        
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphScriptcbar" runat="Server">
</asp:Content>
