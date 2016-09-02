<%@ Page Language="C#" MasterPageFile="~/App_Master/EditPage.master" AutoEventWireup="true"
    CodeFile="ParameterTypeEdit.aspx.cs" Inherits="ServiceLayer_EnumType_ParameterTypeEdit"
    Title="无标题页" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphTopToolbar" runat="Server">
    <WTF:MyToolbar ID="MyTopToolBar" runat="server" ModuleCode="ServiceLayer_EnumType_ParameterTypeEdit"
        OperatePlaceTypeValue="OperateTopBar" OnItemCommand="CurrentTool_ItemCommand" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphContent" runat="Server">
    <table class="tblContent">
        <colgroup>
            <col class="colTitle" />
            <col class="colContent" />
        </colgroup>
        <tr class="trCaption">
            <td colspan="2">枚举类型基本信息
            </td>
        </tr>
        <tr>
            <td>
                <span class="txtNoNull">*</span>枚举类型名称
            </td>
            <td>
                <WTF:MyTextBox ID="txtParameterTypeName" runat="server" CheckValueEmpty="true" ValidationGroup="SaveGroup"
                    ErrorMessage="请输入类型名称" Text="<%# objParameterType.ParameterTypeName %>"></WTF:MyTextBox>
            </td>
        </tr>
        <tr>
            <td>
                <span class="txtNoNull">*</span>枚举类型代码
            </td>
            <td>
                <WTF:MyTextBox ID="txtParameterTypeCode" CheckValueEmpty="true" ValidationGroup="SaveGroup"
                    ErrorMessage="请输入类型代码" runat="server" Text="<%# objParameterType.ParameterTypeCode %>"></WTF:MyTextBox>
            </td>
        </tr>
        <tr>
            <td>程序集全名
            </td>
            <td>
                <WTF:MyTextBox ID="txtAssemblyName" runat="server" Text="<%# objParameterType.AssemblyName %>"></WTF:MyTextBox>
            </td>
        </tr>
        <tr>
            <td>枚举类全名
            </td>
            <td>
                <WTF:MyTextBox ID="txtTypeName" runat="server" Text="<%# objParameterType.TypeName %>"></WTF:MyTextBox>
            </td>
        </tr>
        <tr>
            <td>备注
            </td>
            <td>
                <WTF:MyTextBox ID="txtRemark" runat="server" Text="<%# objParameterType.Remark %>"></WTF:MyTextBox>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphScriptcbar" runat="Server">
</asp:Content>
