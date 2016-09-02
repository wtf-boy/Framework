<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/ListPage.master" AutoEventWireup="true"
    CodeFile="WorkLogList.aspx.cs" Inherits="ServiceLayer_Work_WorkLogList" %>

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
    <WTF:QueryTextBox ID="PlanName" QueryTitle="计划名称" Width="100" runat="server"></WTF:QueryTextBox>
    <WTF:QueryDropDownList ID="ResultType" runat="server" QueryTitle="执行状态">
        <asp:ListItem Value="" Text="--全部--"></asp:ListItem>
        <asp:ListItem Value="0" Text="正在执行"></asp:ListItem>
        <asp:ListItem Value="1" Text="执行成功"></asp:ListItem>
        <asp:ListItem Value="-1" Text="执行失败"></asp:ListItem>
    </WTF:QueryDropDownList>
    <WTF:QueryTextBox ID="RunIP" QueryTitle="运行IP" Width="100" runat="server"></WTF:QueryTextBox>
    <WTF:QueryTextBox ID="QueryStartDate" QueryTitle="运行时间" QueryField="CreateDate"
        QueryMethod="GreaterThanOrEqual"
        runat="server" QueryDataType="Date" SkinID="Date" onfocus="new WdatePicker({dateFmt:'yyyy-MM-dd HH:mm'})"></WTF:QueryTextBox>
    -
    <WTF:QueryTextBox ID="QueryEndDate" QueryField="CreateDate" QueryMethod="LessThanOrEqual"
        runat="server" QueryDataType="Date" SkinID="Date" onfocus="new WdatePicker({dateFmt:'yyyy-MM-dd HH:mm'})"></WTF:QueryTextBox>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphContent" runat="Server">
    <WTF:MyGridView ID="gdvContent" runat="server" ModuleCode="" DataKeyNames="WorkLogID"
        AutoGenerateColumns="False" OnRowCommand="CurrentContent_RowCommand" OnPagerChangeCommand="CurrentPager_PagerChangeCommand">
        <Columns>
            <WTF:SelectField>
            </WTF:SelectField>
            <WTF:TemplateField HeaderText="作业名称" HeaderStyle-Width="150"
                ItemStyle-Width="150">
                <ItemTemplate>
                    <%#  Eval("WorkInfoName").CutText(20)%>
                </ItemTemplate>
            </WTF:TemplateField>

            <WTF:TemplateField HeaderText="计划名称">
                <ItemTemplate>
                    <%#  Eval("PlanName").CutText(20)%>
                </ItemTemplate>
            </WTF:TemplateField>
            <WTF:TemplateField HeaderText="主机名" HeaderStyle-Width="150"
                ItemStyle-Width="150">
                <ItemTemplate>
                    <%#  Eval("HostName").CutText(10)%>
                </ItemTemplate>
            </WTF:TemplateField>
            <WTF:OperateField DataTextField="RunIP" HeaderStyle-Width="100"
                ItemStyle-Width="100" HeaderText="运行IP">
            </WTF:OperateField>
            <WTF:TemplateField HeaderText="执行状态" HeaderStyle-Width="100" ItemStyle-Width="100"
                SortExpression="ResultType">
                <ItemTemplate>
                    <%#  (int)Eval("ResultType")==1?"执行成功": (int)Eval("ResultType")==0?"正在执行":"执行失败" %>
                </ItemTemplate>
            </WTF:TemplateField>

            <WTF:BoundField DataField="StartDate" HeaderText="开始时间" HeaderStyle-Width="120"
                ItemStyle-Width="120" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}">
            </WTF:BoundField>
            <WTF:BoundField DataField="EndDate" HeaderText="结束时间" HeaderStyle-Width="120"
                ItemStyle-Width="120" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}">
            </WTF:BoundField>
            <WTF:TemplateField HeaderText="执行时间" HeaderStyle-Width="80" ItemStyle-Width="80">
                <ItemTemplate>
                    <%#  (((DateTime)Eval("EndDate"))-((DateTime)Eval("StartDate"))).TotalSeconds.RenderSecond() %>
                </ItemTemplate>
            </WTF:TemplateField>
        </Columns>
    </WTF:MyGridView>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="cphScriptcbar" runat="Server">
</asp:Content>
