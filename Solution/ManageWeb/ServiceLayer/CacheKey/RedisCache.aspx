<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/EditPage.master" AutoEventWireup="true" CodeFile="RedisCache.aspx.cs" Inherits="ServiceLayer_CacheKey_RedisCache" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphTopToolbar" runat="Server">
    <WTF:MyToolbar ID="MyTopToolBar" runat="server" ModuleCode=""
        OperatePlaceTypeValue="OperateTopBar" OnItemCommand="CurrentTool_ItemCommand">
        <Buttons>
            <WTF:ToolButton CommandName="Save" Name="读取" ImageCss="saveButton" />
            <WTF:ToolButton CommandName="Back" Name="返回" ImageCss="backButton" />
        </Buttons>
    </WTF:MyToolbar>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphContent" runat="Server">
    <table class="tblContent">
        <colgroup>
            <col class="colTitle" />
            <col class="colContent" />
        </colgroup>
        <tr class="trCaption">
            <td colspan="2"></td>
        </tr>
        <tr>
            <td>读取配置值:
            </td>
            <td>
                <WTF:MyTextBox ID="txtConfigValue" ValidationGroup="SaveGroup" Width="300" runat="server" Text="RedisWriteF1"></WTF:MyTextBox>
                RedisWriteF0,RedisReadF0,RedisWriteF1,RedisReadF1,RedisWriteF2,RedisReadF2
            </td>
        </tr>
        <tr>
            <td>缓存Key:
            </td>
            <td>
                <WTF:MyTextBox ID="txtCacheKey" ValidationGroup="SaveGroup" Width="300" runat="server"></WTF:MyTextBox>
            </td>
        </tr>
        <tr>
            <td>缓存值:
            </td>
            <td>
                <WTF:MyTextBox ID="txtValue" ValidationGroup="SaveGroup" Width="300" TextMode="MultiLine" Height="100" runat="server"></WTF:MyTextBox>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphScriptcbar" runat="Server">
</asp:Content>

