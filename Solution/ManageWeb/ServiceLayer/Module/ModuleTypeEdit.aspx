﻿<%@ Page Language="C#" MasterPageFile="~/App_Master/EditPage.master" AutoEventWireup="true"
    CodeFile="ModuleTypeEdit.aspx.cs" Inherits="ServiceLayer_Module_ModuleTypeEdit"
    Title="无标题页" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphTopToolbar" runat="Server">
    <WTF:MyToolbar ID="MyTopToolBar" runat="server" ModuleCode="ServiceLayer_Module_ModuleTypeEdit"
        OperatePlaceTypeValue="OperateTopBar" OnItemCommand="CurrentTool_ItemCommand" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphContent" runat="Server">
    <table class="tblContent">
        <colgroup>
            <col class="colTitle" />
            <col class="colContent" />
        </colgroup>
        <tr class="trCaption">
            <td colspan="3">平台分类基本信息
            </td>
        </tr>
        <tr>
            <td>
                <span class="txtNoNull">*</span>平台分类名称：
            </td>
            <td>
                <WTF:MyTextBox ID="txtModuleName" CheckValueEmpty="true" ValidationGroup="SaveGroup"
                    ErrorMessage="请输入分类名称" runat="server" Text="<%# objModuleType.ModuleTypeName  %>"></WTF:MyTextBox>
            </td>
        </tr>
        <tr>
            <td>
                <span class="txtNoNull">*</span>平台分类代码：
            </td>
            <td>
                <WTF:MyTextBox ID="txtModuleCode" CheckValueEmpty="true" ValidationGroup="SaveGroup"
                    ErrorMessage="请输入分类代码" runat="server" Text="<%#  objModuleType.ModuleTypeCode %>"></WTF:MyTextBox>
            </td>
        </tr>
        <tr>
            <td>用户类型：
            </td>
            <td>
                <WTF:MyEnumCheckBoxList ID="chkUserType" runat="server" RepeatColumns="3">
                </WTF:MyEnumCheckBoxList>
            </td>
        </tr>
       
       
        <tr>
            <td>是否系统：
            </td>
            <td>
                <asp:CheckBox ID="chkSystem" runat="server" Text="是" />
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphScriptcbar" runat="Server">
</asp:Content>
