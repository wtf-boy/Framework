<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/ListPage.master" AutoEventWireup="true"
    CodeFile="WorkNextList.aspx.cs" Inherits="ServiceLayer_Work_WorkNextList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphTopToolbar" runat="Server">
    <WTF:MyToolbar ID="MyTopToolBar" runat="server" ModuleCode="" OperatePlaceTypeValue="OperateTopBar"
        OnItemCommand="CurrentTool_ItemCommand" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphSearchBar" runat="Server">
    <WTF:QueryDropDownList ID="dropWorkInfoID" QueryField="WorkInfoID" runat="server"
        QueryTitle="作业名称">
    </WTF:QueryDropDownList>
    <WTF:QueryTextBox ID="PlanName" QueryTitle="计划名称" runat="server"></WTF:QueryTextBox>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphContent" runat="Server">
    <WTF:MyGridView ID="gdvContent" runat="server" ModuleCode="" DataKeyNames="PlanRunID"
        AutoGenerateColumns="False" OnRowCommand="CurrentContent_RowCommand" OnPagerChangeCommand="CurrentPager_PagerChangeCommand">
        <Columns>
            <WTF:SelectField>
            </WTF:SelectField>
            <WTF:OperateField DataTextField="WorkInfoName" HeaderText="作业名称" HeaderStyle-Width="200" ItemStyle-Width="200">
            </WTF:OperateField>
            <WTF:TemplateField HeaderText="计划名称">
                <ItemTemplate>
                    <%#  Eval("PlanName")%>
                </ItemTemplate>
            </WTF:TemplateField>
            <WTF:TemplateField HeaderText="正在执行" HeaderStyle-Width="80" ItemStyle-Width="80">
                <ItemTemplate>
                    <%#  (bool)Eval("IsRun")?"是":"否"%>
                </ItemTemplate>
            </WTF:TemplateField>
            <WTF:BoundField DataField="RunDate" HeaderText="下次时间" HeaderStyle-Width="120" ItemStyle-Width="120"
                DataFormatString="{0:yyyy-MM-dd HH:mm:ss}">
            </WTF:BoundField>
            <WTF:TemplateField HeaderText="运行IP或编码" HeaderStyle-Width="150" ItemStyle-Width="150">
                <ItemTemplate>
                    <%#  Eval("RunIP")%>
                </ItemTemplate>
            </WTF:TemplateField>
        </Columns>
    </WTF:MyGridView>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="cphScriptcbar" runat="Server">
</asp:Content>
