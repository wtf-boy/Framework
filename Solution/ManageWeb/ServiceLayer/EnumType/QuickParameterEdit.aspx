<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/EditPage.master" AutoEventWireup="true"
    ValidateRequest="false" CodeFile="QuickParameterEdit.aspx.cs" Inherits="ServiceLayer_EnumType_QuickParameterEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphTopToolbar" runat="Server">
    <WTF:MyToolbar ID="MyTopToolBar" runat="server" ModuleCode="ServiceLayer_EnumType_QuickParameterEdit"
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
                参数快速新增
            </td>
        </tr>
        <tr>
            <td>
                <span class="txtNoNull">*</span>参数名称
            </td>
            <td>
                <WTF:MyTextBox ID="txtParameterName" runat="server" TextMode="MultiLine" Width="400"
                    Height="300" CheckValueEmpty="true" ValidationGroup="SaveGroup" ErrorMessage="请输入参数名称"></WTF:MyTextBox>
            </td>
        </tr>
        <tr>
            <td>
                说明
            </td>
            <td>
                参数名1, 参数名2, 参数名3
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphScriptcbar" runat="Server">
</asp:Content>
