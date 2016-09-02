<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/EditPage.master" AutoEventWireup="true" CodeFile="DataFieldEdit.aspx.cs" Inherits="ServiceLayer_Data_DataFieldEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphTopToolbar" Runat="Server">
    <WTF:MyToolbar ID="MyTopToolBar" runat="server" ModuleCode="" OperatePlaceTypeValue="OperateTopBar"
        OnItemCommand="CurrentTool_ItemCommand" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphContent" Runat="Server">
<table class="tblContent">
        <colgroup>
            <col class="colTitle" />
            <col class="colContent" />
        </colgroup>
        <tr class="trCaption">
            <td colspan="2">
              数据信息
            </td>
        </tr><tr>
            <td>
             <span class="txtNoNull">*</span>数据值:
            </td>
            <td>
             <WTF:MyTextBox ID="txtDataValue"   ValidationGroup="SaveGroup" CheckValueEmpty="true"   MaxLength="36" ErrorMessage="请输入数据值" runat="server" Text="<%# objSys_DataField.DataValue %>"></WTF:MyTextBox>
            </td>
        </tr><tr>
            <td>
             <span class="txtNoNull">*</span>数据名称:
            </td>
            <td>
             <WTF:MyTextBox ID="txtDataTitle"   ValidationGroup="SaveGroup" CheckValueEmpty="true"   MaxCharLength="50" ErrorMessage="请输入数据名称" runat="server" Text="<%# objSys_DataField.DataTitle %>"></WTF:MyTextBox>
            </td>
        </tr></table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphScriptcbar" Runat="Server">
</asp:Content>

