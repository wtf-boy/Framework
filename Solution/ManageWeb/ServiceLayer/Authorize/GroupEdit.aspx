<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/EditPage.master" AutoEventWireup="true" CodeFile="GroupEdit.aspx.cs" Inherits="ServiceLayer_Authorize_GroupEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphTopToolbar" runat="Server">
    <WTF:MyToolbar ID="MyTopToolBar" runat="server" ModuleCode=""
        OperatePlaceTypeValue="OperateTopBar" OnItemCommand="CurrentTool_ItemCommand" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphContent" runat="Server">

    <table class="tblContent">
        <colgroup>
            <col class="colTitle" />
            <col class="colContent" />
        </colgroup>
        <tr class="trCaption">
            <td colspan="2">授权组信息
            </td>
        </tr>
        <tr>
            <td>
                <span class="txtNoNull">*</span>授权组名:
            </td>
            <td>
                <WTF:MyTextBox ID="txtAuthorizeGroupName" ValidationGroup="SaveGroup" CheckValueEmpty="true" MaxLength="50" ErrorMessage="请输入授权组名" runat="server" Text="<%# objsys_authorizegroup.AuthorizeGroupName %>"></WTF:MyTextBox>
            </td>
        </tr>
        <tr>
            <td>平台类型:
            </td>
            <td>

                <WTF:MyDropDownList ID="dropModuleTypeID" ValidationGroup="SaveGroup" ErrorMessage="请输入平台类型" runat="server"></WTF:MyDropDownList>
            </td>
        </tr>
        <tr>
            <td>是否是超管组:
            </td>
            <td>
                <asp:CheckBox ID="chkIsSupertGroup" runat="server" />
            </td>
        </tr>
        <tr>
            <td>是否允许授权自己帐号:
            </td>
            <td>
                <asp:CheckBox ID="chkIsAllowPowerSelf" runat="server" />
            </td>
        </tr>
        <tr>
            <td><span class="txtNoNull">是否收回授权</span>:
            </td>
            <td>
                <asp:CheckBox ID="chkIsRevertPower" runat="server" Text="是否自动回收下面权限" />
            </td>
        </tr>
        <tr>
            <td>备注:
            </td>
            <td>
                <WTF:MyTextBox ID="txtRemark" TextMode="MultiLine" Width="400" Height="200" ValidationGroup="SaveGroup" MaxLength="100" ErrorMessage="请输入备注" runat="server" Text="<%# objsys_authorizegroup.Remark %>"></WTF:MyTextBox>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphScriptcbar" runat="Server">
</asp:Content>

