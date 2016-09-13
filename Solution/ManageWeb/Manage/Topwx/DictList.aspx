<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/ListPage.master" AutoEventWireup="true" CodeFile="DictList.aspx.cs" Inherits="Manage_Topwx_DictList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphTopToolbar" runat="Server">
    <WTF:MyToolbar ID="MyTopToolBar" runat="server" ModuleCode="" OperatePlaceTypeValue="OperateTopBar"
        OnItemCommand="CurrentTool_ItemCommand" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphSearchBar" runat="Server">
    <WTF:QueryTextBox ID="TableName" QueryTitle="表名" runat="server"></WTF:QueryTextBox>
    <WTF:QueryTextBox ID="FieldName" QueryTitle="字段名" runat="server"></WTF:QueryTextBox>
    <WTF:QueryTextBox ID="DictDesc" QueryTitle="描述" runat="server"></WTF:QueryTextBox>
    <WTF:QueryTextBox ID="DictValue" QueryTitle="字典值" runat="server"></WTF:QueryTextBox>
    <WTF:QueryDropDownList ID="Flag" QueryTitle="状态" runat="server">
        <asp:ListItem Value="">--请选择--</asp:ListItem>
        <asp:ListItem Value="1">启用</asp:ListItem>
        <asp:ListItem Value="2">禁用</asp:ListItem>
    </WTF:QueryDropDownList>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphContent" runat="Server">

    <WTF:MyGridView ID="gdvContent" runat="server" ModuleCode=""
        DataKeyNames="Id" AutoGenerateColumns="False" OnRowCommand="CurrentContent_RowCommand"
        OnPagerChangeCommand="CurrentPager_PagerChangeCommand" IsAutoSortFields="false">
        <Columns>
            <WTF:SelectField></WTF:SelectField>
            <WTF:OperateField DataTextField="TableName" HeaderText="表名" HeaderStyle-Width="150" ItemStyle-Width="150" SortExpression="TableName">
            </WTF:OperateField>
            <WTF:BoundField DataField="FieldName" HeaderText="字段名" HeaderStyle-Width="150" ItemStyle-Width="150" SortExpression="FieldName">
            </WTF:BoundField>
            <WTF:BoundField DataField="DictDesc" HeaderText="字典值描述" HeaderStyle-Width="150" ItemStyle-Width="150">
            </WTF:BoundField>
            <WTF:TemplateField HeaderText="字典值" SortExpression="DictValue" HeaderStyle-Width="60" ItemStyle-Width="60">
                <ItemTemplate>
                    <%#  Eval("DictValue")%>
                </ItemTemplate>
            </WTF:TemplateField>
            <WTF:TemplateField HeaderText="状态"  HeaderStyle-Width="40" ItemStyle-Width="40">
                <ItemTemplate>
                    <%#  Eval("Flag").ConvertInt()==2?"禁用":"启用"%>
                </ItemTemplate>
            </WTF:TemplateField>
            <WTF:TemplateField HeaderText="排序" SortExpression="SortIndex"  HeaderStyle-Width="40" ItemStyle-Width="40">
                <ItemTemplate>
                    <%#  Eval("SortIndex")%>
                </ItemTemplate>
            </WTF:TemplateField>
            <WTF:BoundField DataField="Remark" HeaderText="备注"  HeaderStyle-Width="100">
            </WTF:BoundField>
        </Columns>
    </WTF:MyGridView>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="cphScriptcbar" runat="Server">
</asp:Content>


