<%@ Page Language="C#" MasterPageFile="~/App_Master/EditPage.master" AutoEventWireup="true"
    CodeFile="LogCategoryTypeEdit.aspx.cs" Inherits="ServiceLayer_Loging_LogCategoryTypeEdit"
    Title="无标题页" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphTopToolbar" runat="Server">
    <WTF:MyToolbar ID="MyTopToolBar" runat="server" ModuleCode="ServiceLayer_Loging_LogCategoryTypeEdit"
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
                日志分类基本信息
            </td>
        </tr>
        <tr>
            <td>
                <span class="txtNoNull">*</span> 日志分类代码：
            </td>
            <td>
                <WTF:MyTextBox ID="txtCategoryTypeCode" CheckValueEmpty="true" ValidationGroup="SaveGroup"
                    ValidationExpression="\w{3,30}" ErrorMessage="请输入3~30字符分类代码" runat="server" Text="<%#  objCategory.CategoryTypeCode %>"></WTF:MyTextBox>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                <span class="txtNoNull">*</span> 日志分类名称：
            </td>
            <td>
                <WTF:MyTextBox ID="txtCategoryName" CheckValueEmpty="true" ValidationGroup="SaveGroup"
                    ErrorMessage="请输入日志分类名称" runat="server" Text="<%# objCategory.CategoryName %>"></WTF:MyTextBox>
            </td>
        </tr>
        <tr>
            <td>
                日志记录方式：
            </td>
            <td>
                <WTF:MyCheckBoxList ID="chkLogWriteType" runat="server" RepeatColumns="4">
                    <asp:ListItem Value="1">数据库存储</asp:ListItem>
                    <asp:ListItem Value="2">文本存储</asp:ListItem>
                    <asp:ListItem Value="3">事件存储</asp:ListItem>
                    <asp:ListItem Value="4">Xml存储</asp:ListItem>
                </WTF:MyCheckBoxList>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphScriptcbar" runat="Server">
</asp:Content>
