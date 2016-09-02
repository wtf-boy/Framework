<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/EditPage.master" AutoEventWireup="true" CodeFile="PlanNotifyEdit.aspx.cs" Inherits="ServiceLayer_Work_PlanNotifyEdit" %>

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
            <td colspan="2">通知信息
            </td>
        </tr>
        <tr>
            <td>
                <span class="txtNoNull">*</span>通知方式:
            </td>
            <td>
                <WTF:MyEnumRadioButtonList ID="radNotifyType" runat="server" RepeatColumns="2" AutoPostBack="True" OnSelectedIndexChanged="radNotifyType_SelectedIndexChanged">
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
                <WTF:MyEnumRadioButtonList ID="radNotifyAddressID" runat="server" ValidationGroup="SaveGroup" CheckValueEmpty="true" ErrorMessage="请选择通知联系人"></WTF:MyEnumRadioButtonList>
            </td>
        </tr>
        <tr>
            <td>
                <span class="txtNoNull">*</span>通知类型:   
            </td>
            <td>
                <WTF:MyEnumRadioButtonList ID="radPlanResult" runat="server">
                    <asp:ListItem Text="当作业失败时" Selected="True" Value="-1"></asp:ListItem>
                    <asp:ListItem Text="当作成功时" Value="1"></asp:ListItem>
                    <asp:ListItem Text="当作业完成时" Value="2"></asp:ListItem>
                </WTF:MyEnumRadioButtonList>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphScriptcbar" runat="Server">
</asp:Content>

