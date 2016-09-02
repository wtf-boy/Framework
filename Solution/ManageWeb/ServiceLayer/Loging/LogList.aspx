<%@ Page Language="C#" MasterPageFile="~/App_Master/ListPage.master" AutoEventWireup="true"
    CodeFile="LogList.aspx.cs" Inherits="ServiceLayer_Loging_LogList" Title="无标题页" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphTopToolbar" runat="Server">
    <WTF:MyToolbar ID="MyTopToolBar" runat="server" ModuleCode=""
        OperatePlaceTypeValue="OperateTopBar" OnItemCommand="CurrentTool_ItemCommand" IsAddOperatorLog="true" />
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="cphSearchBar" runat="Server">
    <WTF:QueryTextBox ID="ApplicationHost" QueryTitle="运行主机" Width="100" runat="server" QueryMethod="StdIn"
        QueryDataType="String"></WTF:QueryTextBox>
    <WTF:QueryTextBox ID="Account" runat="server" QueryTitle="监听用户" Width="100"></WTF:QueryTextBox>
    <WTF:QueryTextBox ID="UserHostAddress" runat="server" QueryTitle="用户IP" Width="100"></WTF:QueryTextBox>
    <WTF:QueryTextBox ID="LogTitle" QueryField="Title" runat="server" QueryTitle="日志摘要" Width="100"></WTF:QueryTextBox>
    <WTF:QueryTextBox ID="MessageID" runat="server" QueryTitle="消息标识" Width="100"></WTF:QueryTextBox>
    <br />
    <WTF:QueryDropDownList ID="dropCategoryTypeCode" QueryField="CategoryTypeCode" runat="server" QueryDataType="String" QueryTitle="日志分类">
    </WTF:QueryDropDownList>
    <WTF:QueryDropDownList ID="ModuleTypeCode" runat="server" QueryDataType="String" QueryMethod="Equal" QueryTitle="模块分类"></WTF:QueryDropDownList>
    <WTF:QueryTextBox ID="PublishDateTime" QueryField="LogDate" QueryTitle="记录日期" QueryMethod="GreaterThanOrEqual"
        runat="server" QueryDataType="Date" SkinID="Date" onfocus="new WdatePicker({dateFmt:'yyyy-MM-dd HH:mm'})"></WTF:QueryTextBox>
    -
    <WTF:QueryTextBox ID="QueryTextBox1" QueryField="LogDate" QueryMethod="LessThanOrEqual"
        runat="server" QueryDataType="Date" SkinID="Date" onfocus="new WdatePicker({dateFmt:'yyyy-MM-dd HH:mm'})"></WTF:QueryTextBox>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphContent" runat="Server">
    <WTF:MyGridView ID="gdvContent" runat="server" DataKeyNames="LogID" ModuleCode=""
        AutoGenerateColumns="false" OnRowCommand="CurrentContent_RowCommand" OnPagerChangeCommand="CurrentPager_PagerChangeCommand">
        <Columns>
            <WTF:SelectField>
            </WTF:SelectField>
            <WTF:BoundField DataField="LogID" HeaderText="标识" HeaderStyle-Width="40"
                ItemStyle-Width="40">
            </WTF:BoundField>
            <WTF:OperateField DataTextField="ApplicationName" HeaderText="程序名称" HeaderStyle-Width="120"
                ItemStyle-Width="120">
            </WTF:OperateField>
            <WTF:TemplateField HeaderText="模块名称" SortExpression="ModuleTypeCode" HeaderStyle-Width="100"
                ItemStyle-Width="100">
                <ItemTemplate>
                    <%#   GetLogModuleTypeName(Eval("ModuleTypeCode").ToString()) %>
                </ItemTemplate>
            </WTF:TemplateField>
            <WTF:TemplateField HeaderText="分类代码" SortExpression="CategoryTypeCode" HeaderStyle-Width="100"
                ItemStyle-Width="100">
                <ItemTemplate>
                    <%#   GetLogCategoryName(Eval("CategoryTypeCode").ToString()) %>
                </ItemTemplate>
            </WTF:TemplateField>

            <WTF:TemplateField HeaderText="异常摘要">
                <ItemTemplate>
                    <asp:LinkButton ID="btnDetail" runat="server" CommandName="Detail" CommandArgument='<%# Eval("LogID") %>' Text='<%# string.IsNullOrEmpty(Eval("Title").ToString()) ? (Eval("Message").ToString().CleanHtmlTags(true).CutText(40)+ Eval("RawUrl").CutText(30)) : (Eval("Title").ToString().CutText(40)+ Eval("RawUrl").CutText(30)) %>'></asp:LinkButton>
                </ItemTemplate>
            </WTF:TemplateField>
            <WTF:TemplateField HeaderText="记录时间" HeaderStyle-Width="120"
                ItemStyle-Width="120" SortExpression="LogDate">
                <ItemTemplate>
                    <%# Eval("LogDate").ConvertDate("yyyy-MM-dd HH:mm:ss")%>
                </ItemTemplate>
            </WTF:TemplateField>
        </Columns>
    </WTF:MyGridView>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphScriptcbar" runat="Server">
</asp:Content>
