<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/ListPage.master" AutoEventWireup="true" CodeFile="ThemeConfigList.aspx.cs" Inherits="ServiceLayer_ThemeConfig_ThemeConfigList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphTopToolbar" runat="Server">
    <WTF:MyToolbar ID="MyTopToolBar" runat="server" ModuleCode="" OperatePlaceTypeValue="OperateTopBar"
        OnItemCommand="CurrentTool_ItemCommand" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphSearchBar" runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphContent" runat="Server">


    <WTF:MyGridView ID="gdvContent" runat="server" ModuleCode=""
        DataKeyNames="ThemeConfigID" AutoGenerateColumns="False" OnRowCommand="CurrentContent_RowCommand"
        OnPagerChangeCommand="CurrentPager_PagerChangeCommand">
        <Columns>
            <WTF:SelectField></WTF:SelectField>
            <WTF:OperateField DataTextField="ThemeConfigName" HeaderText="主题名称">
            </WTF:OperateField>
            <WTF:BoundField DataField="Theme" HeaderText="主题样式" HeaderStyle-Width="200" ItemStyle-Width="200">
            </WTF:BoundField>
            <WTF:BoundField DataField="LayoutPath" HeaderText="布局样式" HeaderStyle-Width="200" ItemStyle-Width="200">
            </WTF:BoundField>

        </Columns>
    </WTF:MyGridView>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="cphScriptcbar" runat="Server">
</asp:Content>

