<%@ Page Language="C#" MasterPageFile="~/App_Master/EditPage.master" AutoEventWireup="true"
    CodeFile="DataTemplateTypeEdit.aspx.cs" Inherits="ServiceLayer_DataTemplate_DataTemplateTypeEdit"
    Title="无标题页" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphTopToolbar" runat="Server">
    <WTF:MyToolbar ID="MyTopToolBar" runat="server" ModuleCode="ServiceLayer_DataTemplate_DataTemplateTypeEdit"
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
                数据模板类型基本信息
            </td>
        </tr>
        <tr>
            <td>
                <span class="txtNoNull">*</span>数据模板类型名称
            </td>
            <td>
                <WTF:MyTextBox ID="txtDataTemplateTypeName" CheckValueEmpty="true" ValidationGroup="SaveGroup"
                    ErrorMessage="请输入类型名称" runat="server" Text="<%# objDataTemplateType.DataTemplateTypeName %>"></WTF:MyTextBox>
            </td>
        </tr>
        <tr>
            <td>
                <span class="txtNoNull">*</span>数据模板类型代码
            </td>
            <td>
                <WTF:MyTextBox ID="txtDataTemplateTypeCode" CheckValueEmpty="true" ValidationGroup="SaveGroup"
                    ErrorMessage="请输入类型代码" runat="server" Text="<%# objDataTemplateType.DataTemplateTypeCode %>"></WTF:MyTextBox>
            </td>
        </tr>
        <tr>
            <td>
                备注
            </td>
            <td>
                <WTF:MyTextBox ID="txtRemark" runat="server" Text="<%# objDataTemplateType.Remark %>"></WTF:MyTextBox>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphScriptcbar" runat="Server">
</asp:Content>
