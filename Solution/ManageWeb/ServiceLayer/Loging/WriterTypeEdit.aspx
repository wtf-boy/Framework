<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/EditPage.master" AutoEventWireup="true"
    CodeFile="WriterTypeEdit.aspx.cs" Inherits="ServiceLayer_Loging_WriterTypeEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphTopToolbar" runat="Server">
    <WTF:MyToolbar ID="MyTopToolBar" runat="server" ModuleCode="ServiceLayer_Loging_WriterTypeEdit"
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
                <asp:Literal ID="litCaption" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td>是否应用子类:
            </td>
            <td>
                <asp:CheckBox ID="chkChild" runat="server" Text="是否应用子类" />
            </td>
        </tr>
        <tr>
            <td>日志分类:
            </td>
            <td>
                <WTF:MyDropDownList ID="dropCategoryType" runat="server" Width="200" AutoPostBack="true" OnSelectedIndexChanged="dropCategoryType_SelectedIndexChanged">
                </WTF:MyDropDownList>
            </td>
        </tr>
        <tr>
            <td>日志记录方式：
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
