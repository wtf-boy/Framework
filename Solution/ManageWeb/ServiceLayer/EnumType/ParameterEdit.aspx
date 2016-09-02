<%@ Page Language="C#" MasterPageFile="~/App_Master/EditPage.master" AutoEventWireup="true"
    CodeFile="ParameterEdit.aspx.cs" Inherits="ServiceLayer_EnumType_ParameterEdit"
    Title="无标题页" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphTopToolbar" runat="Server">
    <WTF:MyToolbar ID="MyTopToolBar" runat="server" ModuleCode="ServiceLayer_EnumType_ParameterEdit"
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
                参数 基本信息
            </td>
        </tr>
        <tr>
            <td>
                <span class="txtNoNull">*</span>参数名称
            </td>
            <td>
                <WTF:MyTextBox ID="ParameterName" runat="server" Text="<%# objParameter.ParameterName %>"
                    CheckValueEmpty="true" ValidationGroup="SaveGroup" ErrorMessage="请输入参数名称"></WTF:MyTextBox>
            </td>
        </tr>
        <tr>
            <td>
                <span class="txtNoNull">*</span>参数代码
            </td>
            <td>
                <WTF:MyTextBox ID="ParameterCode" CheckValueEmpty="true" ValidationGroup="SaveGroup"
                    ErrorMessage="请输入参数代码" runat="server" Text="<%# objParameter.ParameterCode %>"></WTF:MyTextBox>
            </td>
        </tr>
        <tr>
            <td>
                <span class="txtNoNull">*</span>参数标识
            </td>
            <td>
                <WTF:MyTextBox ID="ParameterCodeID" CheckValueEmpty="true" ValidationGroup="SaveGroup"
                    ErrorMessage="请输入0~100000之间的标识" ValidationExpression="\d+" runat="server" Text="<%# objParameter.ParameterCodeID %>"></WTF:MyTextBox>
            </td>
        </tr>
        <tr>
            <td>
                <span class="txtNoNull">*</span>是否启用
            </td>
            <td>
                <asp:CheckBox ID="IsEnable" runat="server" Checked="<%# objParameter.IsEnable %>" />
            </td>
        </tr>
        <tr>
            <td>
                备注
            </td>
            <td>
                <WTF:MyTextBox ID="Remark" runat="server" Text="<%# objParameter.Remark %>"></WTF:MyTextBox>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphScriptcbar" runat="Server">
</asp:Content>
