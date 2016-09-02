<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/EditPage.master" AutoEventWireup="true" CodeFile="ThemeEdit.aspx.cs" Inherits="ServiceLayer_Module_ThemeEdit" %>

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
            <td colspan="2">主题信息</td>
        </tr>
        <tr>
            <td>主题名称:
            </td>
            <td>
                <%# objTheme_ModuleThemeInfo.ThemeConfigName %>

            </td>
        </tr>
     
        <tr>
            <td>预览图标:
            </td>
            <td>
                <WTF:MyTextBox ID="txtPreviewIco" ValidationGroup="SaveGroup"  MaxCharLength="500"  runat="server" Text="<%# objTheme_ModuleThemeInfo.PreviewIco %>"></WTF:MyTextBox>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphScriptcbar" runat="Server">
</asp:Content>

