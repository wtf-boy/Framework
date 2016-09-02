<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/ListPage.master" AutoEventWireup="true" CodeFile="LogStatList.aspx.cs" Inherits="ServiceLayer_Loging_LogStatList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphTopToolbar" runat="Server">
    <WTF:MyToolbar ID="MyTopToolBar" runat="server" ModuleCode="" OperatePlaceTypeValue="OperateTopBar"
        OnItemCommand="CurrentTool_ItemCommand">
    </WTF:MyToolbar>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphSearchBar" runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphContent" runat="Server">
    <WTF:MyGridView ID="gdvContent" runat="server" ModuleCode=""
        DataKeyNames="ApplicationID" AutoGenerateColumns="False" OnRowCommand="CurrentContent_RowCommand"
        OnPagerChangeCommand="CurrentPager_PagerChangeCommand" IsAutoSortFields="false">
        <Columns>
            <WTF:NumberField></WTF:NumberField>
            <WTF:TemplateField HeaderText="程序名称">
                <ItemTemplate>
                    <%#   GetApplicationName(Eval("ApplicationID").ConvertInt()) %>
                </ItemTemplate>
            </WTF:TemplateField>
            <WTF:BoundField DataField="LogCount" HeaderText="日志数" HeaderStyle-Width="120" ItemStyle-Width="120">
            </WTF:BoundField>
            <WTF:Operate></WTF:Operate>
        </Columns>
    </WTF:MyGridView>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="cphScriptcbar" runat="Server">
</asp:Content>

