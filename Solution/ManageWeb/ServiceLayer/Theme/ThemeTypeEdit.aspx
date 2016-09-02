<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/EditPage.master" AutoEventWireup="true"
    CodeFile="ThemeTypeEdit.aspx.cs" Inherits="ServiceLayer_Theme_ThemeTypeEdit" %>

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
                主题类型
            </td>
        </tr>
        <tr>
            <td>
                <span class="txtNoNull">*</span>主题类型名称:
            </td>
            <td>
                <WTF:MyTextBox ID="txtThemeTypeName" ValidationGroup="SaveGroup" CheckValueEmpty="true"
                    MaxLength="50" ErrorMessage="请输入主题类型名称" runat="server" Text="<%# objSys_ThemeType.ThemeTypeName %>"></WTF:MyTextBox>
            </td>
        </tr>
        <tr>
            <td>
                <span class="txtNoNull">*</span>主题类型代码:
            </td>
            <td>
                <WTF:MyTextBox ID="txtThemeTypeCode" ValidationGroup="SaveGroup" CheckValueEmpty="true"
                    MaxCharLength="100" ErrorMessage="请输入主题类型代码" runat="server" Text="<%# objSys_ThemeType.ThemeTypeCode %>"></WTF:MyTextBox>
            </td>
        </tr>
        <tr>
            <td>
                主题类型备注:
            </td>
            <td>
                <WTF:MyTextBox ID="txtRemark" ValidationGroup="SaveGroup" MaxLength="100" ErrorMessage="请输入主题类型备注"
                    runat="server" Text="<%# objSys_ThemeType.Remark %>"></WTF:MyTextBox>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphScriptcbar" runat="Server">
</asp:Content>
