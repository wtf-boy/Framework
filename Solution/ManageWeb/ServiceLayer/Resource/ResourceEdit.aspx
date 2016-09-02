<%@ Page Language="C#" MasterPageFile="~/App_Master/EditPage.master" AutoEventWireup="true"
    CodeFile="ResourceEdit.aspx.cs" Inherits="ServiceLayer_Resource_ResourceEdit"
    Title="无标题页" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphTopToolbar" runat="Server">
    <WTF:MyToolbar ID="MyTopToolBar" runat="server" ModuleCode="ServiceLayer_Resource_ResourceEdit"
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
                资源基本信息
            </td>
        </tr>
        <tr>
            <td>
                <span class="txtNoNull">*</span>资源名称：
            </td>
            <td>
            
                <WTF:MyTextBox ID="txtResourceName" CheckValueEmpty="true" ValidationGroup="SaveGroup"
                    ErrorMessage="请输入资源名称" runat="server" Text="<%# objResource.ResourceName %>"></WTF:MyTextBox>
            </td>
     
        </tr>
    </table>
    
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphScriptcbar" runat="Server">
</asp:Content>
