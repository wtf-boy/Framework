<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/ListPage.master" AutoEventWireup="true"
    CodeFile="PlanList.aspx.cs" Inherits="ServiceLayer_Work_PlanList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphTopToolbar" runat="Server">
    <WTF:MyToolbar ID="MyTopToolBar" runat="server" ModuleCode="" OperatePlaceTypeValue="OperateTopBar"
        OnItemCommand="CurrentTool_ItemCommand" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphSearchBar" runat="Server">
    <WTF:QueryTextBox ID="PlanName" QueryTitle="计划名称" runat="server"></WTF:QueryTextBox>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphContent" runat="Server">
    <WTF:MyGridView ID="gdvContent" runat="server" ModuleCode="" DataKeyNames="PlanID"
        AutoGenerateColumns="False" OnRowCommand="CurrentContent_RowCommand" OnPagerChangeCommand="CurrentPager_PagerChangeCommand">
        <Columns>
            <WTF:NumberField></WTF:NumberField>
            <WTF:OperateField DataTextField="PlanName" HeaderText="计划名称" HeaderStyle-Width="240"
                ItemStyle-Width="240">
            </WTF:OperateField>
            <WTF:TemplateField HeaderText="备注">
                <ItemTemplate>
                    <%# Eval("PlanRemark").CutText(200)%>
                </ItemTemplate>
            </WTF:TemplateField>
            <WTF:TemplateField HeaderText="是否启用" HeaderStyle-Width="60" ItemStyle-Width="60">
                <ItemTemplate>
                    <%# (bool)Eval("IsEnable") ? "启用" : "禁用"%>
                </ItemTemplate>
            </WTF:TemplateField>
            <WTF:TemplateField HeaderText="上次执行时间" HeaderStyle-Width="120" ItemStyle-Width="120">
                <ItemTemplate>
                    <%# ((DateTime)Eval("LastRunDate")).ToString("yyyy-MM-dd") == "9999-12-31" ? "未执行" : Eval("LastRunDate").ConvertDate("yyyy-MM-dd HH:mm:ss")%>
                </ItemTemplate>
            </WTF:TemplateField>
        </Columns>
    </WTF:MyGridView>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="cphScriptcbar" runat="Server">
</asp:Content>
