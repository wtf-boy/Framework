<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/EditPage.master" AutoEventWireup="true" CodeFile="ClearKeyEdit.aspx.cs" Inherits="ServiceLayer_CacheKey_ClearKeyEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphTopToolbar" runat="Server">
    <WTF:MyToolbar ID="MyTopToolBar" runat="server" ModuleCode=""
        OperatePlaceTypeValue="OperateTopBar" OnItemCommand="CurrentTool_ItemCommand" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphContent" runat="Server">
    <table class="tblContent">
        <colgroup>
            <col class="colTitle" />
            <col class="colContent" />

        </colgroup>
        <tr class="trCaption">
            <td colspan="2">缓存管理</td>
        </tr>
        <tr>
            <td><span class="txtNoNull">*</span>Key或表名:
            </td>
            <td>
                <WTF:MyTextBox ID="txtKey" Width="300" ValidationGroup="SaveGroup" CheckValueEmpty="true" ErrorMessage="请输入Key或表名" runat="server"></WTF:MyTextBox>
            </td>
        </tr>
        <tr>
            <td>表标识:
            </td>
            <td>
                <WTF:MyTextBox ID="txtID" Width="300" MaxLength="100" runat="server"></WTF:MyTextBox>
            </td>
        </tr>
        <tr>
            <td>缓存值:
            </td>
            <td>
                <WTF:MyTextBox ID="txtResult" Width="600" Height="300" TextMode="MultiLine" runat="server"></WTF:MyTextBox>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphScriptcbar" runat="Server">
</asp:Content>

