<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/EditPage.master" AutoEventWireup="true"
    CodeFile="ModuleDataEdit.aspx.cs" Inherits="ServiceLayer_Module_ModuleDataEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphTopToolbar" runat="Server">
    <WTF:MyToolbar ID="MyTopToolBar" runat="server" ModuleCode="ServiceLayer_Module_ModuleDataEdit"
        OperatePlaceTypeValue="OperateTopBar" OnItemCommand="CurrentTool_ItemCommand" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphContent" runat="Server">
    <table class="tblContent">
        <colgroup>
            <col class="colTitle" style="width: 150px;" />
            <col class="colContent" />
        </colgroup>
        <tr class="trCaption">
            <td colspan="2">数据权限配置
            </td>
        </tr>
        <tr>
            <td>
                <span class="txtNoNull">*</span>连接串值:
            </td>
            <td>
                <WTF:MyEnumDropDownList ID="dropConnectionKey" runat="server" ValidationGroup="SaveGroup"
                    CheckValueEmpty="true" ErrorMessage="请选择连接串值">
                </WTF:MyEnumDropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <span class="txtNoNull">*</span>数据名称:
            </td>
            <td>
                <WTF:MyTextBox ID="txtDataName" ValidationGroup="SaveGroup" CheckValueEmpty="true"
                    MaxCharLength="50" ErrorMessage="请输入数据名称" runat="server" Text="<%# objSys_ModuleData.DataName %>"></WTF:MyTextBox>
            </td>
        </tr>
        <tr>
            <td>
                <span class="txtNoNull">*</span>字段名:
            </td>
            <td>
                <WTF:MyTextBox ID="txtFieldName" ValidationGroup="SaveGroup" CheckValueEmpty="true"
                    MaxLength="50" ErrorMessage="请输入字段名" runat="server" Text="<%# objSys_ModuleData.FieldName %>"></WTF:MyTextBox>
            </td>
        </tr>
        <tr>
            <td>
                <span class="txtNoNull">*</span>数据查询:
            </td>
            <td>
                <WTF:MyTextBox ID="txtDataSelect" ValidationGroup="SaveGroup" CheckValueEmpty="true"
                    Width="300" TextMode="MultiLine" Height="200" MaxLength="1000" ErrorMessage="请输入数据查询"
                    runat="server" Text="<%# objSys_ModuleData.DataSelect %>"></WTF:MyTextBox>
            </td>
        </tr>
        <tr>
            <td>
                <span class="txtNoNull">*</span>字段类型:
            </td>
            <td>
                <WTF:MyEnumRadioButtonList ID="radFieldType" runat="server" RepeatColumns="4">
                    <asp:ListItem Value="1" Selected="True" Text="int"></asp:ListItem>
                    <asp:ListItem Value="2" Text="string"></asp:ListItem>
                    <asp:ListItem Value="3" Text="bool"></asp:ListItem>
                    <asp:ListItem Value="4" Text="Guid"></asp:ListItem>
                </WTF:MyEnumRadioButtonList>
            </td>
        </tr>
        <tr>
            <td>
                <span class="txtNoNull">*</span>字段源类型:
            </td>
            <td>
                <WTF:MyEnumRadioButtonList ID="radFieldSourceType" runat="server" RepeatColumns="4">
                    <asp:ListItem Value="1" Selected="True" Text="int"></asp:ListItem>
                    <asp:ListItem Value="2" Text="string"></asp:ListItem>
                    <asp:ListItem Value="3" Text="bool"></asp:ListItem>
                    <asp:ListItem Value="4" Text="Guid"></asp:ListItem>
                </WTF:MyEnumRadioButtonList>
            </td>
        </tr>
        <tr>
            <td>Sql备注:
            </td>
            <td>select [UserTypeID] as DataValue ,[UserTypeName] as DataTitle FROM [Sys_UserType]
                或数据参数 select DataValue ,DataTitle FROM Sys_DataFieldList where DataCode='数据代码'
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphScriptcbar" runat="Server">
</asp:Content>
