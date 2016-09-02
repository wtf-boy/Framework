<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/EditPage.master" AutoEventWireup="true" CodeFile="ModuleTypeEdit.aspx.cs" Inherits="ServiceLayer_Loging_ModuleTypeEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphTopToolbar" Runat="Server">
  <WTF:MyToolbar ID="MyTopToolBar" runat="server" ModuleCode="ServiceLayer_Loging_ModuleTypeEdit"
        OperatePlaceTypeValue="OperateTopBar" OnItemCommand="CurrentTool_ItemCommand" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphContent" Runat="Server">
 <table class="tblContent">
        <colgroup>
            <col class="colTitle" />
            <col class="colContent" />
        </colgroup>
        <tr class="trCaption">
            <td colspan="2">
                日志模块类型
            </td>
        </tr>
        <tr>
            <td>
                <span class="txtNoNull">*</span>  日志模块代码：
            </td>
            <td>
                <WTF:MyTextBox ID="txLogModuleTypeCode" CheckValueEmpty="true" ValidationGroup="SaveGroup"
                    ValidationExpression="\w{3,256}" ErrorMessage="请输入3~256分类代码" runat="server" Text="<%#  objLog_ModuleType.ModuleTypeCode %>"></WTF:MyTextBox>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                <span class="txtNoNull">*</span>  日志模块名称：
            </td>
            <td>
                <WTF:MyTextBox ID="txtLogModuleTypeName" CheckValueEmpty="true" ValidationGroup="SaveGroup"
                    ErrorMessage="请输入日志分类名称" runat="server" Text="<%# objLog_ModuleType.ModuleTypeName %>"></WTF:MyTextBox>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphScriptcbar" Runat="Server">
</asp:Content>

