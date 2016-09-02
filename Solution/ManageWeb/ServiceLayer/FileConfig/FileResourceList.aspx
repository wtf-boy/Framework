<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/ListPage.master" AutoEventWireup="true" CodeFile="FileResourceList.aspx.cs" Inherits="ServiceLayer_FileConfig_FileResourceList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphTopToolbar" runat="Server">
    <WTF:MyToolbar ID="MyTopToolBar" runat="server" ModuleCode="" OperatePlaceTypeValue="OperateTopBar"
        OnItemCommand="CurrentTool_ItemCommand" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphSearchBar" runat="Server">
    <WTF:QueryTextBox ID="FileResourceName" QueryTitle="文件资源名称" runat="server"></WTF:QueryTextBox>
    <WTF:QueryTextBox ID="FileResourceCode" QueryTitle="文件资源代码" runat="server"></WTF:QueryTextBox>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphContent" runat="Server">






    <WTF:MyGridView ID="gdvContent" runat="server" ModuleCode=""
        DataKeyNames="FileResourceID" AutoGenerateColumns="False" OnRowCommand="CurrentContent_RowCommand"
        OnPagerChangeCommand="CurrentPager_PagerChangeCommand">
        <Columns>
            <WTF:SelectField></WTF:SelectField>
            <WTF:OperateField DataTextField="FileResourceName" HeaderText="文件资源名称">
            </WTF:OperateField>
            <WTF:BoundField DataField="FileResourceCode" HeaderText="文件资源代码">
            </WTF:BoundField>
            <WTF:BoundField DataField="CreateDate" HeaderText="创建时间" HeaderStyle-Width="120" ItemStyle-Width="120" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" SortExpression="CreateDate">
            </WTF:BoundField>

        </Columns>
    </WTF:MyGridView>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="cphScriptcbar" runat="Server">
</asp:Content>

