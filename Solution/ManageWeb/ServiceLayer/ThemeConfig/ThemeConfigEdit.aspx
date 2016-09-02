<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/EditPage.master" AutoEventWireup="true" CodeFile="ThemeConfigEdit.aspx.cs" Inherits="ServiceLayer_ThemeConfig_ThemeConfigEdit" %>

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
            <td colspan="2">主题信息
            </td>
        </tr>
        <tr>
            <td>
                <span class="txtNoNull">*</span>主题名称:
            </td>
            <td>
                <WTF:MyTextBox ID="txtThemeConfigName" ValidationGroup="SaveGroup" CheckValueEmpty="true" MaxLength="50" ErrorMessage="请输入主题名称" runat="server" Text="<%# objTheme_ThemeConfig.ThemeConfigName %>"></WTF:MyTextBox>
            </td>
        </tr>
        <tr>
            <td>
                <span class="txtNoNull">*</span>主题样式:
            </td>
            <td>
                <WTF:MyTextBox ID="txtTheme" ValidationGroup="SaveGroup" CheckValueEmpty="true" MaxCharLength="100" ErrorMessage="请输入主题样式" runat="server" Text="<%# objTheme_ThemeConfig.Theme %>"></WTF:MyTextBox>
            </td>
        </tr>
        <tr>
            <td>
                <span class="txtNoNull">*</span>布局样式:
            </td>
            <td>
                <WTF:MyTextBox ID="txtLayoutPath" ValidationGroup="SaveGroup" CheckValueEmpty="true" MaxCharLength="100" ErrorMessage="请输入布局样式" runat="server" Text="<%# objTheme_ThemeConfig.LayoutPath %>"></WTF:MyTextBox>
            </td>
        </tr>
        <tr>
            <td>预览图标:
            </td>
            <td>
                <WTF:MyTextBox ID="txtPreviewIco" MaxCharLength="500" ValidationGroup="SaveGroup" CheckValueEmpty="false" runat="server" Text="<%# objTheme_ThemeConfig.PreviewIco %>"></WTF:MyTextBox>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphScriptcbar" runat="Server">
</asp:Content>

