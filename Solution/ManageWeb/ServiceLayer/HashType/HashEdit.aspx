<%@ Page Language="C#" MasterPageFile="~/App_Master/EditPage.master" AutoEventWireup="true"
    CodeFile="HashEdit.aspx.cs" Inherits="ServiceLayer_HashType_HashEdit" Title="无标题页" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphTopToolbar" runat="Server">
    <WTF:MyToolbar ID="MyTopToolBar" runat="server" ModuleCode="ServiceLayer_HashType_HashEdit"
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
                <span class="txtNoNull">*</span>哈希键名
            </td>
            <td>
                <WTF:MyTextBox ID="txtHashKey" CheckValueEmpty="true" ValidationGroup="SaveGroup"
                    ErrorMessage="请输入哈希键名" runat="server" Text="<%# objHash.HashKey %>"></WTF:MyTextBox>
            </td>
        </tr>
        <tr>
            <td>
                <span class="txtNoNull">*</span>哈希键值
            </td>
            <td>
                <WTF:MyTextBox ID="txtHashValue" Width="400" CheckValueEmpty="true" ValidationGroup="SaveGroup"
                    ErrorMessage="请输入哈希键值" runat="server" Text="<%# objHash.HashValue %>"></WTF:MyTextBox>
            </td>
        </tr>
        <tr>
            <td>
                备注
            </td>
            <td>
                <WTF:MyTextBox ID="txtRemark" runat="server" Text="<%# objHash.Remark %>"></WTF:MyTextBox>
            </td>
            <td>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphScriptcbar" runat="Server">
</asp:Content>
