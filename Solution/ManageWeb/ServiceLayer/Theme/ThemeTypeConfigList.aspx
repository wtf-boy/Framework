<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/ListPage.master" AutoEventWireup="true"
    CodeFile="ThemeTypeConfigList.aspx.cs" Inherits="ServiceLayer_Theme_ThemeTypeConfigList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphTopToolbar" runat="Server">
    <WTF:MyToolbar ID="MyTopToolBar" runat="server" ModuleCode="" OperatePlaceTypeValue="OperateTopBar"
        OnItemCommand="CurrentTool_ItemCommand" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphSearchBar" runat="Server">
    <WTF:QueryTextBox ID="ConfigName" QueryTitle="配置名称" runat="server"></WTF:QueryTextBox>
    <WTF:QueryTextBox ID="ConfigKey" QueryTitle="配置键" runat="server"></WTF:QueryTextBox>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphContent" runat="Server">
    <WTF:MyGridView ID="gdvContent" runat="server" ModuleCode="" DataKeyNames="ThemeTypeConfigID"
        AutoGenerateColumns="False" OnRowCommand="CurrentContent_RowCommand" OnPagerChangeCommand="CurrentPager_PagerChangeCommand">
        <Columns>
               <WTF:SelectField>
            </WTF:SelectField>
            <WTF:OperateField DataTextField="ConfigName" HeaderText="配置名称">
            </WTF:OperateField>
            <WTF:BoundField DataField="ConfigKey" HeaderText="配置键">
            </WTF:BoundField>
            <WTF:BoundField DataField="ConfigValue" HeaderText="配置数据">
            </WTF:BoundField>
        </Columns>
    </WTF:MyGridView>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="cphScriptcbar" runat="Server">
</asp:Content>
