<%@ Page Language="C#" MasterPageFile="~/App_Master/EditPage.master" AutoEventWireup="true"
    CodeFile="ResourceVerEdit.aspx.cs" Inherits="ServiceLayer_Resource_ResourceVerEdit"
    Title="无标题页" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphTopToolbar" runat="Server">
    <WTF:MyToolbar ID="MyTopToolBar" runat="server" ModuleCode="ServiceLayer_Resource_ResourceVerEdit"
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
            <td colspan="2">
                请选择要上传的文件
            </td>
        </tr>
        <tr>
            <td>
                <span class="txtNoNull">*</span>请选择文件：
            </td>
            <td>
                <WTF:MyFileUpload ID="fupFile" runat="server" >
                </WTF:MyFileUpload>
            </td>
            <td>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphScriptcbar" runat="Server">
</asp:Content>
