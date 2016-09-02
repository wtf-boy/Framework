﻿<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/EditPage.master" AutoEventWireup="true"
    CodeFile="UserTypeEdit.aspx.cs" Inherits="ServiceLayer_User_UserTypeEdit" %>

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
                用户类型基本信息
            </td>
        </tr>
        <tr>
            <td>
                <span class="txtNoNull">*</span>用户类型标识:
            </td>
            <td>
                <WTF:MyTextBox ID="txtUserTypeID" ValidationGroup="SaveGroup" CheckValueEmpty="true"
                    MaxLength="50" ErrorMessage="请输入用户类型标识" runat="server" Text="<%# objSys_UserType.UserTypeID %>"></WTF:MyTextBox>
            </td>
        </tr>
        <tr>
            <td>
                <span class="txtNoNull">*</span>用户类型名称:
            </td>
            <td>
                <WTF:MyTextBox ID="txtUserTypeName" ValidationGroup="SaveGroup" CheckValueEmpty="true"
                    MaxLength="50" ErrorMessage="请输入用户类型名称" runat="server" Text="<%# objSys_UserType.UserTypeName %>"></WTF:MyTextBox>
            </td>
        </tr>
        <tr>
            <td>
                <span class="txtNoNull">*</span>备注:
            </td>
            <td>
                <WTF:MyTextBox ID="txtRemark" ValidationGroup="SaveGroup" CheckValueEmpty="true"
                    MaxLength="100" ErrorMessage="请输入备注" runat="server" Text="<%# objSys_UserType.Remark %>"></WTF:MyTextBox>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphScriptcbar" runat="Server">
</asp:Content>
