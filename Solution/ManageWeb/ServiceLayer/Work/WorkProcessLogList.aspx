<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/ListPage.master" AutoEventWireup="true"
    CodeFile="WorkProcessLogList.aspx.cs" Inherits="ServiceLayer_Work_WorkProcessLogList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphTopToolbar" runat="Server">
    <WTF:MyToolbar ID="MyTopToolBar" runat="server" ModuleCode="" OperatePlaceTypeValue="OperateTopBar"
        OnItemCommand="CurrentTool_ItemCommand" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphSearchBar" runat="Server">
    <WTF:QueryTextBox ID="StepName" QueryTitle="步骤名称" Width="100" runat="server"></WTF:QueryTextBox>
    <WTF:QueryTextBox ID="Message" QueryTitle="消息内容" runat="server"></WTF:QueryTextBox>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphContent" runat="Server">
    <WTF:MyGridView ID="gdvContent" runat="server" ModuleCode="" DataKeyNames="WorkProcessLogID"
        AutoGenerateColumns="False" OnRowCommand="CurrentContent_RowCommand" OnPagerChangeCommand="CurrentPager_PagerChangeCommand">
        <Columns>
            <WTF:SelectField>
            </WTF:SelectField>
            <WTF:TemplateField HeaderText="计划名称" HeaderStyle-Width="100" ItemStyle-Width="100">
                <ItemTemplate>
                    <%#  Eval("PlanName").CutText(10)%>
                </ItemTemplate>
            </WTF:TemplateField>
            <WTF:OperateField DataTextField="StepName" HeaderText="步骤名称" HeaderStyle-Width="100"
                ItemStyle-Width="100">
            </WTF:OperateField>

            <WTF:BoundField DataField="CreateDate" HeaderText="创建日期" HeaderStyle-Width="120"
                ItemStyle-Width="120" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}">
            </WTF:BoundField>
            <WTF:BoundField DataField="StartDate" HeaderText="开始时间" HeaderStyle-Width="120"
                ItemStyle-Width="120" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}">
            </WTF:BoundField>
            <WTF:BoundField DataField="EndDate" HeaderText="结束日期" HeaderStyle-Width="120" ItemStyle-Width="120"
                DataFormatString="{0:yyyy-MM-dd HH:mm:ss}">
            </WTF:BoundField>
            <WTF:TemplateField HeaderText="日志消息">
                <ItemTemplate>
                    <a href="javascript:void(0)" onclick='return viewDetail(<%# Eval("WorkProcessLogID") %>)'>
                        <%#  Eval("Message").CutText(80)%></a>
                </ItemTemplate>
            </WTF:TemplateField>
        </Columns>
    </WTF:MyGridView>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="cphScriptcbar" runat="Server">
    <script type="text/javascript">

        function viewDetail(WorkProcessLogID) {

            //打开页面
            showopen('WorkProcessLogInfo.aspx?WorkProcessLogID=' + WorkProcessLogID, 700, 500);
            return false;

        }
    </script>
</asp:Content>
