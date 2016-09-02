<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/ListPage.master" AutoEventWireup="true"
    CodeFile="ModuleTypeList.aspx.cs" Inherits="ServiceLayer_Loging_ModuleTypeList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphTopToolbar" runat="Server">
    <WTF:MyToolbar ID="MyTopToolBar" runat="server" ModuleCode=""
        OperatePlaceTypeValue="OperateTopBar" OnItemCommand="CurrentTool_ItemCommand" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphSearchBar" runat="Server">
    <WTF:QueryTextBox ID="txtModuleTypeCode" QueryField="ModuleTypeCode" runat="server" QueryTitle="模块代码" Width="100"></WTF:QueryTextBox>
    <WTF:QueryTextBox ID="txtModuleTypeName" QueryField="ModuleTypeName" runat="server" QueryTitle="模块名称" Width="100"></WTF:QueryTextBox>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphContent" runat="Server">
    <WTF:MyGridView ID="gdvContent" runat="server" DataKeyNames="ModuleTypeID" ModuleCode=""
        AutoGenerateColumns="false" OnRowCommand="CurrentContent_RowCommand" OnPagerChangeCommand="CurrentPager_PagerChangeCommand">
        <Columns>
            <WTF:SelectField>
            </WTF:SelectField>
            <WTF:OperateField DataTextField="ModuleTypeCode" HeaderText="日志模块代码">
            </WTF:OperateField>
            <WTF:BoundField DataField="ModuleTypeName" HeaderText="日志模块名称">
            </WTF:BoundField>
        </Columns>
    </WTF:MyGridView>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="cphScriptcbar" runat="Server">
</asp:Content>
