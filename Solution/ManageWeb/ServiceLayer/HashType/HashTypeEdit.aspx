<%@ Page Language="C#" MasterPageFile="~/App_Master/EditPage.master" AutoEventWireup="true"
    CodeFile="HashTypeEdit.aspx.cs" Inherits="ServiceLayer_HashType_HashTypeEdit"
    Title="无标题页" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphTopToolbar" runat="Server">
    <WTF:MyToolbar ID="MyTopToolBar" runat="server" ModuleCode="ServiceLayer_HashType_HashTypeEdit"
        OperatePlaceTypeValue="OperateTopBar" OnItemCommand="CurrentTool_ItemCommand" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphContent" runat="Server">
    <table class="tblContent">
        <colgroup>
            <col class="colTitle" />
            <col class="colContent" />

        </colgroup>
        <tr class="trCaption">
            <td colspan="2">参数类型基本信息
            </td>
        </tr>
        <tr>
            <td>
                <span class="txtNoNull">*</span>哈希类型名称
            </td>
            <td>
                <WTF:MyTextBox ID="HashTypeName" CheckValueEmpty="true" ValidationGroup="SaveGroup"
                    ErrorMessage="请输入类型名称" runat="server" Text="<%# objHashType.HashTypeName %>"></WTF:MyTextBox>
            </td>
        </tr>
        <tr>
            <td>
                <span class="txtNoNull">*</span>哈希类型代码
            </td>
            <td>
                <WTF:MyTextBox ID="HashTypeCode" CheckValueEmpty="true" ValidationGroup="SaveGroup"
                    ErrorMessage="请输入类型代码" runat="server" Text="<%# objHashType.HashTypeCode %>"></WTF:MyTextBox>
            </td>
        </tr>
        <tr>
            <td>备注
            </td>
            <td>
                <WTF:MyTextBox ID="Remark" runat="server" Text="<%# objHashType.Remark %>"></WTF:MyTextBox>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphScriptcbar" runat="Server">
</asp:Content>
