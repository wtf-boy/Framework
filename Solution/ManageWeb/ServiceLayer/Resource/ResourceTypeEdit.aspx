<%@ Page Language="C#" MasterPageFile="~/App_Master/EditPage.master" AutoEventWireup="true"
    CodeFile="ResourceTypeEdit.aspx.cs" Inherits="ServiceLayer_Resource_ResourceTypeEdit"
    Title="无标题页" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphTopToolbar" runat="Server">
    <WTF:MyToolbar ID="MyTopToolBar" runat="server" ModuleCode="ServiceLayer_Resource_ResourceTypeEdit"
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
                资源类型基本信息
            </td>
        </tr>
        <tr>
            <td>
                <span class="txtNoNull">*</span>资源类型名称：
            </td>
            <td>
                <WTF:MyTextBox ID="txtResourceTypeName" CheckValueEmpty="true" ValidationGroup="SaveGroup"
                    ErrorMessage="请输入资源类型名称" runat="server" Text="<%# objResourceType.ResourceTypeName %>"></WTF:MyTextBox>
            </td>
        </tr>
        <tr>
            <td>
                <span class="txtNoNull">*</span>资源类型代码：
            </td>
            <td>
                <WTF:MyTextBox ID="txtResourceTypeCode" CheckValueEmpty="true" ValidationGroup="SaveGroup"
                    ErrorMessage="请输入资源类型代码" runat="server" Text="<%# objResourceType.ResourceTypeCode %>"></WTF:MyTextBox>
            </td>
        </tr>
        <tr>
            <td>
                <span class="txtNoNull">*</span>文件存储目录：
            </td>
            <td>
                <asp:DropDownList ID="dropStorageModeCodeType" runat="server">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="dropStorageModeCodeType"
                    Display="Dynamic" ValidationGroup="SaveGroup" runat="server" ErrorMessage="请选择文件存储方式"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                <span class="txtNoNull">*</span>存储方式：
            </td>
            <td>
                <asp:RadioButtonList ID="radStorageTypeList" runat="server">
                    <asp:ListItem Text="目录" Value="1" Selected="True">
                    </asp:ListItem>
                    <asp:ListItem Text="数据库" Value="2">
                    </asp:ListItem>
                    <asp:ListItem Text="数据库和目录" Value="3">
                    </asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td>
                <span class="txtNoNull">*</span>文件访问方式：
            </td>
            <td>
                <WTF:MyEnumRadioButtonList ID="radAccessModeCodeType" runat="server" RepeatColumns="3"
                    EnumTypeCode="AccessModeCodeType">
                </WTF:MyEnumRadioButtonList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="radAccessModeCodeType"
                    Display="Dynamic" ValidationGroup="SaveGroup" runat="server" ErrorMessage="请选择文件访问方式"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                <span class="txtNoNull">*</span>目录存储方式：
            </td>
            <td>
                <WTF:MyEnumRadioButtonList ID="radPathFormatCodeType" runat="server" RepeatColumns="2"
                    EnumTypeCode="PathFormatCodeType">
                </WTF:MyEnumRadioButtonList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="radPathFormatCodeType"
                    Display="Dynamic" ValidationGroup="SaveGroup" runat="server" ErrorMessage="请选择目录存储方式"></asp:RequiredFieldValidator>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphScriptcbar" runat="Server">
</asp:Content>
