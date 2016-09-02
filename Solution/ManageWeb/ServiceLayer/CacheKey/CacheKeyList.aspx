<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/ListPage.master" AutoEventWireup="true" CodeFile="CacheKeyList.aspx.cs" Inherits="ServiceLayer_CacheKey_CacheKeyList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphTopToolbar" runat="Server">
    <WTF:MyToolbar ID="MyTopToolBar" runat="server" ModuleCode=""
        OperatePlaceTypeValue="OperateTopBar" OnItemCommand="CurrentTool_ItemCommand">
        <Buttons>
            <WTF:ToolButton CommandName="Redis" Name="Redis缓存" SortIndex="100" ImageCss="moduleButton" />
        </Buttons>
    </WTF:MyToolbar>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphSearchBar" runat="Server">
    <WTF:QueryTextBox ID="CacheKeyID" QueryTitle="健值标识" runat="server" QueryDataType="Int" QueryMethod="Equal" ValidationExpression="\d+" ErrorMessage="请输入正整数"></WTF:QueryTextBox>
    <WTF:QueryTextBox ID="CacheKey" QueryTitle="缓存Key" runat="server"></WTF:QueryTextBox>
    <WTF:QueryTextBox ID="CacheName" QueryTitle="缓存名称" runat="server"></WTF:QueryTextBox>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphContent" runat="Server">

    <WTF:MyGridView ID="gdvContent" runat="server" ModuleCode=""
        DataKeyNames="CacheKeyID" AutoGenerateColumns="False" OnRowCommand="CurrentContent_RowCommand"
        OnPagerChangeCommand="CurrentPager_PagerChangeCommand">
        <Columns>
            <WTF:BoundField DataField="CacheKey" HeaderText="缓存Key" HeaderStyle-Width="300" ItemStyle-Width="300">
            </WTF:BoundField>
            <WTF:OperateField DataTextField="CacheName" HeaderText="缓存名称" HeaderStyle-Width="300" ItemStyle-Width="300">
            </WTF:OperateField>
            <WTF:TemplateField HeaderText="备注">
                <ItemTemplate>
                    <%#  Eval("Remark")%>
                </ItemTemplate>
            </WTF:TemplateField>

        </Columns>
    </WTF:MyGridView>
    <WTF:MyToolbar ID="MyBottom" runat="server" ModuleCode=""
        OperatePlaceTypeValue="OperateBottomBar" OnItemCommand="CurrentTool_ItemCommand" />
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="cphScriptcbar" runat="Server">
</asp:Content>

