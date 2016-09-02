<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/EditPage.master" AutoEventWireup="true"
    CodeFile="DataTypeEdit.aspx.cs" Inherits="ServiceLayer_Data_DataTypeEdit" %>

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
                数据验证类型信息
            </td>
        </tr>
        <tr>
            <td>
                <span class="txtNoNull">*</span>数据验证编码:
            </td>
            <td>
                <WTF:MyTextBox ID="txtDataCode" ValidationGroup="SaveGroup" CheckValueEmpty="true"
                    MaxLength="50" ErrorMessage="请输入数据验证编码" runat="server" Text="<%# objSys_DataType.DataCode %>"></WTF:MyTextBox>
            </td>
        </tr>
        <tr>
            <td>
                <span class="txtNoNull">*</span>数据验证名称:
            </td>
            <td>
                <WTF:MyTextBox ID="txtDataName" ValidationGroup="SaveGroup" CheckValueEmpty="true"
                    MaxCharLength="20" ErrorMessage="请输入数据验证名称" runat="server" Text="<%# objSys_DataType.DataName %>"></WTF:MyTextBox>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphScriptcbar" runat="Server">
</asp:Content>
