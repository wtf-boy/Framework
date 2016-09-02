<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/EditPage.master" AutoEventWireup="true"
    CodeFile="ProcessEdit.aspx.cs" Inherits="ServiceLayer_Work_ProcessEdit" %>

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
            <td colspan="2">
                处理信息
            </td>
        </tr>
        <tr>
            <td>
                <span class="txtNoNull">*</span>处理名称:
            </td>
            <td>
                <WTF:MyTextBox ID="txtProcessName" ValidationGroup="SaveGroup" CheckValueEmpty="true"
                    Width="300" MaxLength="50" ErrorMessage="请输入处理名称" runat="server" Text="<%# objWork_Process.ProcessName %>"></WTF:MyTextBox>
            </td>
        </tr>
        <tr>
            <td>
                <span class="txtNoNull">*</span>程序集:
            </td>
            <td>
                <WTF:MyTextBox ID="txtAssemblyName" ValidationGroup="SaveGroup" CheckValueEmpty="true"
                    Width="300" MaxCharLength="500" ErrorMessage="请输入程序集" runat="server" Text="<%# objWork_Process.AssemblyName %>"></WTF:MyTextBox>
            </td>
        </tr>
        <tr>
            <td>
                <span class="txtNoNull">*</span>类全名:
            </td>
            <td>
                <WTF:MyTextBox ID="txtTypeName" ValidationGroup="SaveGroup" CheckValueEmpty="true"
                    Width="300" MaxCharLength="500" ErrorMessage="请输入类全名" runat="server" Text="<%# objWork_Process.TypeName %>"></WTF:MyTextBox>
            </td>
        </tr>
        <tr>
            <td>
               配置参数:
            </td>
            <td>
                <WTF:MyTextBox ID="txtProcessConfig" ValidationGroup="SaveGroup" TextMode="MultiLine"
                    Width="400" Height="250"  runat="server" Text="<%# objWork_Process.ProcessConfig %>"></WTF:MyTextBox>
            </td>
        </tr>
        <tr>
            <td>
                处理备注:
            </td>
            <td>
                <WTF:MyTextBox ID="txtProcessRemark"   TextMode="MultiLine"
                    Width="400" Height="50" MaxLength="500" runat="server" Text="<%# objWork_Process.ProcessRemark %>"></WTF:MyTextBox>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphScriptcbar" runat="Server">
</asp:Content>
