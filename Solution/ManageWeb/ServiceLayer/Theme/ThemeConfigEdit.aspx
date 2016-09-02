<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/EditPage.master" AutoEventWireup="true"
    CodeFile="ThemeConfigEdit.aspx.cs" Inherits="ServiceLayer_Theme_ThemeConfigEdit" %>

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
                配置信息
            </td>
        </tr>
        <tr>
            <td>
                <span class="txtNoNull">*</span>配置名称:
            </td>
            <td>
                <WTF:MyTextBox ID="txtConfigName" ValidationGroup="SaveGroup" CheckValueEmpty="true"
                    MaxLength="50" ErrorMessage="请输入配置名称" runat="server" Text="<%# objSys_ThemeConfig.ConfigName %>"></WTF:MyTextBox>
            </td>
        </tr>
        <tr>
            <td>
                <span class="txtNoNull">*</span>配置键:
            </td>
            <td>
                <WTF:MyTextBox ID="txtConfigKey" ValidationGroup="SaveGroup" CheckValueEmpty="true"
                    MaxCharLength="256" ErrorMessage="请输入配置键" runat="server" Text="<%# objSys_ThemeConfig.ConfigKey %>"></WTF:MyTextBox>
            </td>
        </tr>
        <tr>
            <td>
                <span class="txtNoNull">*</span>配置数据:
            </td>
            <td>
                <WTF:MyTextBox ID="txtConfigValue" ValidationGroup="SaveGroup" CheckValueEmpty="true"
                    MaxCharLength="3000" ErrorMessage="请输入配置数据" runat="server" Text="<%# objSys_ThemeConfig.ConfigValue %>"></WTF:MyTextBox>
            </td>
        </tr>
        <tr>
            <td>
                配置备注:
            </td>
            <td>
                <WTF:MyTextBox ID="txtConfigRemark" ValidationGroup="SaveGroup" MaxLength="200"
                    ErrorMessage="请输入配置备注" runat="server" Text="<%# objSys_ThemeConfig.ConfigRemark %>"></WTF:MyTextBox>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphScriptcbar" runat="Server">
</asp:Content>
