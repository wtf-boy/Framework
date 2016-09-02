<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/ListPage.master" AutoEventWireup="true" CodeFile="PlanNotifyList.aspx.cs" Inherits="ServiceLayer_Work_PlanNotifyList" %>

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
        DataKeyNames="PlanNotifyID" AutoGenerateColumns="False" OnRowCommand="CurrentContent_RowCommand"
        OnPagerChangeCommand="CurrentPager_PagerChangeCommand">
        <Columns>
            <WTF:NumberField></WTF:NumberField>
            <WTF:OperateField DataTextField="AddressName" HeaderText="通知联系人" SortExpression="AddressName">
            </WTF:OperateField>
            <WTF:TemplateField HeaderText="通知类型" SortExpression="PlanResult">
                <ItemTemplate>
                    <%#  (int)Eval("PlanResult")==-1?"当作业失败时":(int)Eval("PlanResult")==1?"当作成功时":"当作业完成时"%>
                </ItemTemplate>
            </WTF:TemplateField>
            <WTF:TemplateField HeaderText="通知方式" SortExpression="NotifyType">
                <ItemTemplate>
                    <%#  (int)Eval("NotifyType")==1?"邮件":"手机"%>
                </ItemTemplate>
            </WTF:TemplateField>

        </Columns>
    </WTF:MyGridView>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="cphScriptcbar" runat="Server">
</asp:Content>

