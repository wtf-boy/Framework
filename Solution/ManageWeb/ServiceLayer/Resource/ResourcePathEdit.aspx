<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/EditPage.master" AutoEventWireup="true"
    CodeFile="ResourcePathEdit.aspx.cs" Inherits="ServiceLayer_Resource_ResourcePathEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphTopToolbar" runat="Server">
    <WTF:MyToolbar ID="MyTopToolBar" runat="server" ModuleCode="ServiceLayer_Resource_ResourcePathEdit"
        OperatePlaceTypeValue="OperateTopBar" OnItemCommand="CurrentTool_ItemCommand" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphContent" runat="Server">
    <table class="tblContent">
        <colgroup>
            <col class="colTitle" />
            <col class="colContent" />
            <col class="colError" />
        </colgroup>
        <tr class="trCaption">
            <td colspan="3">
                资源目录信息
            </td>
        </tr>
        <tr>
            <td>
                <span class="txtNoNull">*</span>目录名称：
            </td>
            <td>
                <WTF:MyTextBox ID="txtResourcePathName" CheckValueEmpty="true" ValidationGroup="SaveGroup"
                    ErrorMessage="请输入目录名称" runat="server" Text="<%# objSys_ResourcePath.ResourcePathName %>"></WTF:MyTextBox>
            </td>
        </tr>
        <tr>
            <td>
                <span class="txtNoNull">*</span>虚目录名称：
            </td>
            <td>
                <WTF:MyTextBox ID="txtVirtualName" CheckValueEmpty="true" ValidationGroup="SaveGroup"
                    ValidationExpression=".{1,50}" ErrorMessage="请输入虚目录名称" runat="server" Text="<%# objSys_ResourcePath.VirtualName %>"></WTF:MyTextBox>
            </td>
        </tr>
        <tr>
            <td>
                <span class="txtNoNull">*</span>存储路经：
            </td>
            <td>
                <WTF:MyTextBox ID="txtStoragePath" CheckValueEmpty="true" ValidationGroup="SaveGroup"
                    Width="400" ErrorMessage="请输入存储路经" runat="server" Text="<%# objSys_ResourcePath.StoragePath %>"></WTF:MyTextBox>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphScriptcbar" runat="Server">
</asp:Content>
