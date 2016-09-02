<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/EditPage.master" AutoEventWireup="true" CodeFile="CacheKeyEdit.aspx.cs" Inherits="ServiceLayer_CacheKey_CacheKeyEdit" %>

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
            <td colspan="2"></td>
        </tr>
        <tr>
            <td>缓存Key:
            </td>
            <td>
                <WTF:MyTextBox ID="txtCacheKey" ValidationGroup="SaveGroup" Width="300" MaxLength="100" runat="server" Text="<%# objcache_cachekey.CacheKey %>"></WTF:MyTextBox>没有输入键值系统自动生成
            </td>
        </tr>
        <tr>
            <td>
                <span class="txtNoNull">*</span>缓存名称:
            </td>
            <td>
                <WTF:MyTextBox ID="txtCacheName" ValidationGroup="SaveGroup" CheckValueEmpty="true" Width="300" MaxLength="100" ErrorMessage="请输入缓存名称" runat="server" Text="<%# objcache_cachekey.CacheName %>"></WTF:MyTextBox>
            </td>
        </tr>
        <tr>
            <td>备注:
            </td>
            <td>
                <WTF:MyTextBox ID="txtRemark" ValidationGroup="SaveGroup" TextMode="MultiLine" Width="500" Height="200" MaxLength="500" ErrorMessage="请输入备注" runat="server" Text="<%# objcache_cachekey.Remark %>"></WTF:MyTextBox>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphScriptcbar" runat="Server">
</asp:Content>

