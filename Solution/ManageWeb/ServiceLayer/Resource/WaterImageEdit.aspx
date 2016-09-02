<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/EditPage.master" AutoEventWireup="true"
    CodeFile="WaterImageEdit.aspx.cs" Inherits="ServiceLayer_Resource_WaterImageEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphTopToolbar" runat="Server">
    <WTF:MyToolbar ID="MyTopToolBar" runat="server" ModuleCode="ServiceLayer_Resource_WaterImageEdit"
        OperatePlaceTypeValue="OperateTopBar" OnItemCommand="CurrentTool_ItemCommand" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphContent" runat="Server">
    <table class="tblContent">
        <colgroup>
            <col class="colTitle" />
            <col class="colContent" />
        </colgroup>
        <tr class="trCaption">
            <td colspan="2">水印图标
            </td>
        </tr>
        <tr>
            <td>
                <span class="txtNoNull">*</span> 水印名称：
            </td>
            <td>
                <WTF:MyTextBox ID="txtWaterName" CheckValueEmpty="true" ValidationGroup="SaveGroup"
                    ErrorMessage="请输入" runat="server"></WTF:MyTextBox>
            </td>
        </tr>
        <tr>
            <td>
                <span class="txtNoNull">*</span> 水印地址：
            </td>
            <td>
                <WTF:MyTextBox ID="txtWaterPath"
                    runat="server" Width="500"></WTF:MyTextBox>
            </td>
        </tr>
        <tr>
            <td>
                <span class="txtNoNull">*</span>水印图标
            </td>
            <td>
                <WTF:MyFileRestrictUpload ID="fupFile" ResourceTypeID="20" ValidationGroup="SaveGroup"
                    VerNo="1" runat="server" RestrictCode="WaterImage" />
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphScriptcbar" runat="Server">
</asp:Content>
