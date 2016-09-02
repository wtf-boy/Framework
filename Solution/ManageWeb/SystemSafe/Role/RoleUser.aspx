<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/ListPage.master" AutoEventWireup="true"
    CodeFile="RoleUser.aspx.cs" Inherits="SystemSafe_Role_RoleUser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphTopToolbar" runat="Server">
    <WTF:MyToolbar ID="MyTopToolBar" runat="server" ModuleCode="SystemSafe_Role_RoleUser"
        OperatePlaceTypeValue="OperateTopBar" OnItemCommand="CurrentTool_ItemCommand" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphSearchBar" runat="Server">
    <WTF:QueryTextBox ID="ID" runat="server" QueryTitle="编码" Width="80"></WTF:QueryTextBox>
    <WTF:QueryTextBox ID="Account" runat="server" QueryTitle="用户帐号" Width="80"></WTF:QueryTextBox>
    <WTF:QueryTextBox ID="UserName" runat="server" QueryTitle="用户名" Width="80"></WTF:QueryTextBox>
    <WTF:QueryTextBox ID="JobNo" runat="server" QueryTitle="工号" Width="80"></WTF:QueryTextBox>
    <WTF:QueryTextBox ID="NickName" runat="server" QueryTitle="昵称" Width="80"></WTF:QueryTextBox>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphContent" runat="Server">
    <WTF:MyGridView ID="gdvContent" DataKeyNames="UserID" AutoGenerateColumns="False" IsSelectNoInfo="false"
        OnRowCommand="CurrentContent_RowCommand" OnPagerChangeCommand="CurrentPager_PagerChangeCommand"
        runat="server">
        <Columns>
            <WTF:SelectField HeaderStyle-Width="50" ItemStyle-Width="50">
            </WTF:SelectField>
            <WTF:BoundField HeaderText="编码" DataField="ID" HeaderStyle-Width="40"
                ItemStyle-Width="40">
            </WTF:BoundField>
            <WTF:BoundField HeaderText="工号" DataField="JobNo" HeaderStyle-Width="100"
                ItemStyle-Width="100">
            </WTF:BoundField>
            <WTF:BoundField HeaderText="用户名" DataField="UserName" HeaderStyle-Width="100"
                ItemStyle-Width="100">
            </WTF:BoundField>
            <WTF:OperateField HeaderText="帐号" DataTextField="Account" HeaderStyle-Width="100"
                ItemStyle-Width="100">
            </WTF:OperateField>
            <WTF:TemplateField HeaderText="状态" HeaderStyle-Width="60" ItemStyle-Width="60">
                <ItemTemplate>
                    <%# Eval("IsLockOut").ConvertBool() ? "禁用" : "启用"%>
                </ItemTemplate>
            </WTF:TemplateField>
            <WTF:TemplateField HeaderText="用户类型" SortExpression="Sys_UserType.UserTypeName" HeaderStyle-Width="80"
                ItemStyle-Width="80">
                <ItemTemplate>
                    <%#  ((WTF.Power.Entity.Sys_UserType)Eval("Sys_UserType")).UserTypeName%>
                </ItemTemplate>
            </WTF:TemplateField>
            <WTF:BoundField HeaderText="用户昵称" DataField="NickName">
            </WTF:BoundField>
            <WTF:BoundField DataField="CreateDate" HeaderText="创建时间" HeaderStyle-Width="150"
                ItemStyle-Width="150" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}">
            </WTF:BoundField>
        </Columns>
    </WTF:MyGridView>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="cphScriptcbar" runat="Server">
</asp:Content>
