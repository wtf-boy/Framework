<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/ListPage.master" AutoEventWireup="true" CodeFile="PlanStepList.aspx.cs" Inherits="ServiceLayer_Work_PlanStepList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphTopToolbar" runat="Server">
    <WTF:MyToolbar ID="MyTopToolBar" runat="server" ModuleCode="" OperatePlaceTypeValue="OperateTopBar"
        OnItemCommand="CurrentTool_ItemCommand" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphSearchBar" runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphContent" runat="Server">

    <WTF:MyGridView ID="gdvContent" runat="server" ModuleCode=""
        DataKeyNames="PlanStepID" AutoGenerateColumns="False" OnRowCommand="CurrentContent_RowCommand"
        OnPagerChangeCommand="CurrentPager_PagerChangeCommand">
        <Columns>
            <WTF:NumberField></WTF:NumberField>
            <WTF:BoundField DataField="StepName" HeaderText="步骤名称">
            </WTF:BoundField>
            <WTF:TemplateField HeaderText="功能名称" SortExpression="ProcessName" HeaderStyle-Width="200" ItemStyle-Width="200">
                <ItemTemplate>
                    <%#  Eval("ProcessName")%>
                </ItemTemplate>
            </WTF:TemplateField>
            <WTF:TemplateField HeaderText="尝试次数" SortExpression="RunCount" HeaderStyle-Width="70" ItemStyle-Width="70">
                <ItemTemplate>
                    <%#  Eval("RunCount")%>
                </ItemTemplate>
            </WTF:TemplateField>
            <WTF:TemplateField HeaderText="间隔时间" SortExpression="RunInterval" HeaderStyle-Width="70" ItemStyle-Width="70">
                <ItemTemplate>
                    <%#  Eval("RunInterval")%>
                </ItemTemplate>
            </WTF:TemplateField>
            <WTF:TemplateField HeaderText="成功时执行操作" SortExpression="SucessProcessType" HeaderStyle-Width="200" ItemStyle-Width="200">
                <ItemTemplate>
                    <%#  (int)Eval("SucessProcessType")==1?"退出报告失败作业": (int)Eval("SucessProcessType")==2?"转到下一步":"退出报告成功作业"%>
                </ItemTemplate>
            </WTF:TemplateField>
            <WTF:TemplateField HeaderText="失败时执行操作" SortExpression="FailProcessType" HeaderStyle-Width="200" ItemStyle-Width="200">
                <ItemTemplate>
                    <%#  (int)Eval("FailProcessType")==1?"退出报告失败作业": (int)Eval("FailProcessType")==2?"转到下一步":"退出报告成功作业"%>
                </ItemTemplate>
            </WTF:TemplateField>
            <WTF:OperateField DataTextField="SortIndex" HeaderText="执行顺序" SortExpression="SortIndex" HeaderStyle-Width="80" ItemStyle-Width="80">
            </WTF:OperateField>

        </Columns>
    </WTF:MyGridView>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="cphScriptcbar" runat="Server">

    <script type="text/javascript">
        //用户成员
        function StepSort() {

            //打开页面

            showopen('<%=EncryptModuleQuery(string.Format("PlanStepSort.aspx?PlanID={0}",PlanID))%>', 400, 500);
            return false;
        }
    </script>
</asp:Content>

