<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/ListPage.master" AutoEventWireup="true"
    CodeFile="WorkInfoList.aspx.cs" Inherits="ServiceLayer_Work_WorkInfoList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphTopToolbar" runat="Server">
    <WTF:MyToolbar ID="MyTopToolBar" runat="server" ModuleCode="" OperatePlaceTypeValue="OperateTopBar"
        OnItemCommand="CurrentTool_ItemCommand" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphSearchBar" runat="Server">
    <WTF:QueryTextBox ID="WorkInfoName" QueryTitle="作业名称" runat="server"></WTF:QueryTextBox>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphContent" runat="Server">
    <WTF:MyGridView ID="gdvContent" runat="server" ModuleCode="" DataKeyNames="WorkInfoID"
        AutoGenerateColumns="False" OnRowCommand="CurrentContent_RowCommand" OnPagerChangeCommand="CurrentPager_PagerChangeCommand">
        <Columns>
            <WTF:SelectField>
            </WTF:SelectField>
            <WTF:OperateField DataTextField="WorkInfoName" HeaderText="作业名称" HeaderStyle-Width="240"
                ItemStyle-Width="240">
            </WTF:OperateField>
            <WTF:BoundField DataField="RunIP" HeaderText="IP或编码" HeaderStyle-Width="120"
                ItemStyle-Width="120">
            </WTF:BoundField>
            <WTF:TemplateField HeaderText="作业说明">
                <ItemTemplate>
                    <%# Eval("WorkInfoRemark").CutText(120)%>
                </ItemTemplate>
            </WTF:TemplateField>
            <WTF:TemplateField HeaderText="是否启用" HeaderStyle-Width="60" ItemStyle-Width="60">
                <ItemTemplate>
                    <%# (bool)Eval("IsEnable")?"启用":"禁用"%>
                </ItemTemplate>
            </WTF:TemplateField>
            <WTF:BoundField DataField="LastProcessDate" HeaderText="上次执行时间" HeaderStyle-Width="120"
                ItemStyle-Width="120" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}">
            </WTF:BoundField>
            <WTF:BoundField DataField="CreateDate" HeaderText="创建时间" HeaderStyle-Width="120"
                ItemStyle-Width="120" DataFormatString="{0:yyyy-MM-dd HH:mm}">
            </WTF:BoundField>

        </Columns>
    </WTF:MyGridView>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="cphScriptcbar" runat="Server">
</asp:Content>
