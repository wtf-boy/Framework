<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/EditPage.master" AutoEventWireup="true" CodeFile="RoleEdit.aspx.cs" Inherits="SystemSafe_Power_RoleEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphTopToolbar" runat="Server">
    <WTF:MyToolbar ID="MyTopToolBar" runat="server"
        OperatePlaceTypeValue="OperateTopBar" OnItemCommand="CurrentTool_ItemCommand" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphContent" runat="Server">

    <table class="tblContent">
        <colgroup>
            <col class="colTitle" />
            <col class="colContent" />
        </colgroup>
        <tr class="trCaption">
            <td colspan="2">角色基本信息
            </td>
        </tr>
        <tr>
            <td>
                <span class="txtNoNull">*</span>角色名称
            </td>
            <td>
                <WTF:MyTextBox ID="txtRoleName" CheckValueEmpty="true" ValidationGroup="SaveGroup"
                    ErrorMessage="请输入角色名称" runat="server" Text="<%# objRole.RoleName %>"></WTF:MyTextBox>
            </td>
        </tr>

        <tr>
            <td>角色备注
            </td>
            <td>
                <WTF:MyTextBox ID="txtRemark" runat="server" TextMode="MultiLine" Width="300" Height="50"
                    Text="<%# objRole.Remark %>" ValidationGroup="SaveGroup" MaxLength="100"></WTF:MyTextBox>
            </td>
        </tr>

        <tr>
            <td>已经授权用户:
            </td>
            <td>
                <WTF:MyEnumCheckBoxList ID="chkRoleUser" runat="server" RepeatColumns="8"></WTF:MyEnumCheckBoxList>
            </td>

        </tr>

    </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphScriptcbar" runat="Server">
</asp:Content>

