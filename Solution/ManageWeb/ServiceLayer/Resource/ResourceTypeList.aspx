<%@ Page Language="C#" MasterPageFile="~/App_Master/ListPage.master" AutoEventWireup="true"
    CodeFile="ResourceTypeList.aspx.cs" Inherits="ServiceLayer_Resource_ResourceTypeList"
    Title="无标题页" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphTopToolbar" runat="Server">
    <WTF:MyToolbar ID="MyTopToolBar" runat="server" ModuleCode="ServiceLayer_Resource_ResourceTypeList"
        OperatePlaceTypeValue="OperateTopBar" OnItemCommand="CurrentTool_ItemCommand" />
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="cphSearchBar" runat="Server">
    资源名称：
    <WTF:QueryTextBox ID="ResourceTypeName" runat="server"></WTF:QueryTextBox>
    类型代码：
    <WTF:QueryTextBox ID="ResourceTypeCode" runat="server"></WTF:QueryTextBox>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphContent" runat="Server">
    <WTF:MyGridView ID="gdvContent" runat="server" DataKeyNames="ResourceTypeID" ModuleCode="ServiceLayer_Resource_ResourceTypeList"
        AutoGenerateColumns="false" OnRowCommand="CurrentContent_RowCommand" OnPagerChangeCommand="CurrentPager_PagerChangeCommand">
        <Columns>
            <WTF:SelectField>
            </WTF:SelectField>
            <WTF:OperateField DataTextField="ResourceTypeName" HeaderText="资源名称">
            </WTF:OperateField>
            <WTF:BoundField DataField="ResourceTypeCode" HeaderText="资源类型代码" HeaderStyle-Width="120"
                ItemStyle-Width="120">
            </WTF:BoundField>
            <WTF:BoundField DataField="ResourceTypeID" HeaderText="资源类型标识" HeaderStyle-Width="120"
                ItemStyle-Width="120">
            </WTF:BoundField>
            <WTF:BoundField DataField="AccessModeCodeType" HeaderText="访问方式" HeaderStyle-Width="120"
                ItemStyle-Width="120">
            </WTF:BoundField>
        </Columns>
    </WTF:MyGridView>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphScriptcbar" runat="Server">
</asp:Content>
