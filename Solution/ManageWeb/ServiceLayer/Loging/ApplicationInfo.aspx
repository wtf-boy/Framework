<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/ListPage.master" AutoEventWireup="true" CodeFile="ApplicationInfo.aspx.cs" Inherits="ServiceLayer_Loging_ApplicationInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphTopToolbar" runat="Server">
    <WTF:MyToolbar ID="MyTopToolBar" runat="server" ModuleCode=""
        OperatePlaceTypeValue="OperateTopBar" OnItemCommand="CurrentTool_ItemCommand" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphSearchBar" runat="Server">
    程序编码：<%=objloger_application.ApplicationCode %> &nbsp;&nbsp;&nbsp;&nbsp;程序名称：<%=objloger_application.ApplicationName %>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphContent" runat="Server">
    <WTF:MyGridView ID="gdvContent" runat="server" DataKeyNames="CategoryID" ModuleCode=""
        AutoGenerateColumns="false" OnRowCommand="CurrentContent_RowCommand" OnPagerChangeCommand="CurrentPager_PagerChangeCommand">
        <Columns>
            <WTF:SelectField>
            </WTF:SelectField>
            <WTF:OperateField DataTextField="CategoryTypeCode" HeaderText="日志类型代码">
            </WTF:OperateField>
            <WTF:BoundField DataField="CategoryName" HeaderText="日志类型名称">
            </WTF:BoundField>
        </Columns>
    </WTF:MyGridView>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="cphScriptcbar" runat="Server">
</asp:Content>

