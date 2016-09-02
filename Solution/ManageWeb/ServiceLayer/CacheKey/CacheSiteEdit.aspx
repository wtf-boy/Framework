<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/EditPage.master" AutoEventWireup="true" CodeFile="CacheSiteEdit.aspx.cs" Inherits="ServiceLayer_CacheKey_CacheSiteEdit" %>

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
            <td>
                <span class="txtNoNull">*</span>站点名称:
            </td>
            <td>
                <WTF:MyTextBox ID="txtSiteName" ValidationGroup="SaveGroup" CheckValueEmpty="true" MaxLength="100" ErrorMessage="请输入站点名称" runat="server" Text="<%# objcache_cachesite.SiteName %>"></WTF:MyTextBox>
            </td>
        </tr>
               <tr>
            <td>
                <span class="txtNoNull">*</span>缓存前缀:
            </td>
            <td>
                <WTF:MyTextBox ID="txtCachePrefix" ValidationGroup="SaveGroup" CheckValueEmpty="true" MaxLength="50" ErrorMessage="请输入缓存前缀" runat="server" Text="<%# objcache_cachesite.CachePrefix %>"></WTF:MyTextBox>
            </td>
        </tr>
       
        <tr>
            <td>备注:
            </td>
            <td>
                <WTF:MyTextBox ID="txtRemark" ValidationGroup="SaveGroup" TextMode="MultiLine" Width="400" Height="100" MaxLength="200" ErrorMessage="请输入备注" runat="server" Text="<%# objcache_cachesite.Remark %>"></WTF:MyTextBox>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphScriptcbar" runat="Server">
</asp:Content>

