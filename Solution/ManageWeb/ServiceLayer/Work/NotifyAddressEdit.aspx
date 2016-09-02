<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/EditPage.master" AutoEventWireup="true" CodeFile="NotifyAddressEdit.aspx.cs" Inherits="ServiceLayer_Work_NotifyAddressEdit" %>

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
            <td colspan="2">通知地址</td>
        </tr>
        <tr>
            <td>通信类型:
            </td>
            <td>
                <WTF:MyEnumRadioButtonList ID="radAddressType" runat="server" RepeatColumns="2">
                    <asp:ListItem Value="1" Text="邮件" Selected="True"></asp:ListItem>
                    <asp:ListItem Value="2" Text="手机"></asp:ListItem>
                </WTF:MyEnumRadioButtonList>
            </td>
        </tr>
        <tr>
            <td>
                <span class="txtNoNull">*</span>通知联系人:
            </td>
            <td>
                <WTF:MyTextBox ID="txtAddressName" ValidationGroup="SaveGroup" CheckValueEmpty="true" MaxLength="500" ErrorMessage="请输入通知联系人" runat="server" Text="<%# objWork_NotifyAddress.AddressName %>"></WTF:MyTextBox>
            </td>
        </tr>
        <tr>
            <td>
                <span class="txtNoNull">*</span>通信地址:
            </td>
            <td>
                <WTF:MyTextBox ID="txtAddress" ValidationGroup="SaveGroup" Width="400" CheckValueEmpty="true" MaxCharLength="500" ErrorMessage="请输入通信地址" runat="server" Text="<%# objWork_NotifyAddress.Address %>"></WTF:MyTextBox>多个分号(;)隔开
            </td>
        </tr>


    </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphScriptcbar" runat="Server">
</asp:Content>

