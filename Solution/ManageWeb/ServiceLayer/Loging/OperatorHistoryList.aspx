<%@ Page Title="操作日志查看" Language="C#" MasterPageFile="~/App_Master/ListPage.master" AutoEventWireup="true" CodeFile="OperatorHistoryList.aspx.cs" Inherits="ServiceLayer_Loging_OperatorHistoryList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" runat="Server">
    <base target="_self"></base>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphTopToolbar" runat="Server">
    <WTF:MyToolbar ID="MyTopToolBar" runat="server" ModuleCode="" IsAddRefresh="false" OperatePlaceTypeValue="OperateTopBar"
        OnItemCommand="CurrentTool_ItemCommand">
        <Buttons>
            <WTF:ToolButton CommandName="Search" Name="查询" ImageCss="searchButton" />
        </Buttons>
    </WTF:MyToolbar>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphSearchBar" runat="Server">
    <WTF:QueryTextBox ID="Account" QueryTitle="操作帐号" runat="server" Width="100"></WTF:QueryTextBox>
    <WTF:QueryDropDownList ID="OperationTypeID" QueryTitle="操作方式" runat="server">
    </WTF:QueryDropDownList>
    <WTF:QueryTextBox ID="txtTitle" QueryField="Title" QueryTitle="标题" runat="server" Width="100"></WTF:QueryTextBox>
    <WTF:QueryTextBox ID="Description" QueryTitle="描述" runat="server" Width="100"></WTF:QueryTextBox>
    <WTF:QueryTextBox ID="QueryStartDate" QueryTitle="创建时间" QueryField="CreateDate" QueryMethod="GreaterThanOrEqual"
        runat="server" QueryDataType="Date" SkinID="Date" onfocus="new WdatePicker({dateFmt:'yyyy-MM-dd HH:00:00'})"></WTF:QueryTextBox>
    -
    <WTF:QueryTextBox ID="QueryEndDate" QueryField="CreateDate" QueryMethod="LessThanOrEqual"
        runat="server" QueryDataType="Date" SkinID="Date" onfocus="new WdatePicker({dateFmt:'yyyy-MM-dd HH:59:59'})"></WTF:QueryTextBox>

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphContent" runat="Server">


    <WTF:MyGridView ID="gdvContent" runat="server" ModuleCode=""
        DataKeyNames="OperationHistoryID" AutoGenerateColumns="False" IsAutoSortFields="false" OnRowCommand="CurrentContent_RowCommand"
        OnPagerChangeCommand="CurrentPager_PagerChangeCommand">
        <Columns>
            <WTF:NumberField></WTF:NumberField>
            <WTF:TemplateField HeaderText="操作类型" HeaderStyle-Width="80" ItemStyle-Width="80">
                <ItemTemplate>
                    <%#  ((WTF.Logging.OperationType)((int)Eval("OperationTypeID"))).GetEnumDescription()%>
                </ItemTemplate>
            </WTF:TemplateField>
            <WTF:BoundField DataField="Account" HeaderText="帐号" HeaderStyle-Width="50" ItemStyle-Width="50">
            </WTF:BoundField>
            <WTF:TemplateField HeaderText="标题" HeaderStyle-Width="240" ItemStyle-Width="240">
                <ItemTemplate>
                    <asp:LinkButton ID="btnDetail" runat="server" CommandName="Detail" CommandArgument='<%# Eval("OperationHistoryID") %>' Text=' <%#  Eval("Title").CutText(30)%>'></asp:LinkButton>

                </ItemTemplate>
            </WTF:TemplateField>
            <WTF:TemplateField HeaderText="操作描述">
                <ItemTemplate>
                    <%#  Eval("Description").CutText(80)%>
                </ItemTemplate>
            </WTF:TemplateField>

            <WTF:BoundField DataField="CreateDate" HeaderText="创建时间" HeaderStyle-Width="120" ItemStyle-Width="120" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}">
            </WTF:BoundField>


        </Columns>
    </WTF:MyGridView>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="cphScriptcbar" runat="Server">
</asp:Content>

