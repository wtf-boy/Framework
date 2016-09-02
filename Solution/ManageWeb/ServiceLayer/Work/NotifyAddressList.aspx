<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/ListPage.master" AutoEventWireup="true" CodeFile="NotifyAddressList.aspx.cs" Inherits="ServiceLayer_Work_NotifyAddressList" %>

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
        DataKeyNames="NotifyAddressID" AutoGenerateColumns="False" OnRowCommand="CurrentContent_RowCommand"
        OnPagerChangeCommand="CurrentPager_PagerChangeCommand">
        <Columns>
            <WTF:NumberField></WTF:NumberField>
            <WTF:OperateField DataTextField="AddressName" HeaderText="通知联系人" SortExpression="AddressName" HeaderStyle-Width="200" ItemStyle-Width="200">
            </WTF:OperateField>
            <WTF:BoundField DataField="Address" HeaderText="通信地址" SortExpression="Address">
            </WTF:BoundField>

            <WTF:TemplateField HeaderText="通信类型" SortExpression="AddressType" HeaderStyle-Width="100" ItemStyle-Width="100">
                <ItemTemplate>
                    <%#  (int)Eval("AddressType")==1?"邮件":"手机"%>
                </ItemTemplate>
            </WTF:TemplateField>

        </Columns>
    </WTF:MyGridView>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="cphScriptcbar" runat="Server">
</asp:Content>

