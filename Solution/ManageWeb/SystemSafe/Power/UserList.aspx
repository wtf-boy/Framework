<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/ListPage.master" AutoEventWireup="true" CodeFile="UserList.aspx.cs" Inherits="SystemSafe_Power_UserList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphTopToolbar" runat="Server">
    <WTF:MyToolbar ID="MyTopToolBar" runat="server"
        OperatePlaceTypeValue="OperateTopBar" OnItemCommand="CurrentTool_ItemCommand" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphSearchBar" runat="Server">
    <WTF:QueryTextBox ID="Account" runat="server" QueryTitle="用户帐号" Width="80"></WTF:QueryTextBox>
    <WTF:QueryTextBox ID="UserName" runat="server" QueryTitle="用户名" Width="80"></WTF:QueryTextBox>
    <WTF:QueryTextBox ID="JobNo" runat="server" QueryTitle="工号" Width="80"></WTF:QueryTextBox>
    <WTF:QueryTextBox ID="NickName" runat="server" QueryTitle="昵称" Width="80"></WTF:QueryTextBox>
    <WTF:QueryTextBox ID="Email" QueryTitle="电子邮箱" Width="80" runat="server"></WTF:QueryTextBox>
    <WTF:QueryDropDownList ID="IsLockOut" runat="server" QueryTitle="帐号状态">
        <asp:ListItem Value="" Text="--全部--"></asp:ListItem>
        <asp:ListItem Value="true" Text="禁用"></asp:ListItem>
        <asp:ListItem Value="false" Text="启用"></asp:ListItem>
    </WTF:QueryDropDownList>
    <WTF:QueryDropDownList ID="UserTypeCID" runat="server" QueryTitle="用户类型">
    </WTF:QueryDropDownList>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphContent" runat="Server">

   <WTF:MyGridView ID="gdvContent" DataKeyNames="UserID"
        AutoGenerateColumns="False" OnRowCommand="CurrentContent_RowCommand" OnPagerChangeCommand="CurrentPager_PagerChangeCommand"
        runat="server">
        <Columns>
            <WTF:BoundField HeaderText="编码" DataField="ID" HeaderStyle-Width="40"
                ItemStyle-Width="40">
            </WTF:BoundField>
            <WTF:TemplateField HeaderText="用户类型" HeaderStyle-Width="120" ItemStyle-Width="120">
                <ItemTemplate>
                    <%# GetUserTypeName(Eval("UserTypeCID").ConvertInt())%>
                </ItemTemplate>
            </WTF:TemplateField>
            <WTF:BoundField HeaderText="工号" DataField="JobNo" HeaderStyle-Width="60"
                ItemStyle-Width="60">
            </WTF:BoundField>
            <WTF:BoundField HeaderText="用户名" DataField="UserName" HeaderStyle-Width="200"
                ItemStyle-Width="200">
            </WTF:BoundField>
            <WTF:OperateField HeaderText="帐号" DataTextField="Account" HeaderStyle-Width="200"
                ItemStyle-Width="200">
            </WTF:OperateField>
            <WTF:TemplateField HeaderText="状态" HeaderStyle-Width="60" ItemStyle-Width="60">
                <ItemTemplate>
                    <%# Eval("IsLockOut").ConvertBool() ? "禁用" : "启用"%>
                </ItemTemplate>
            </WTF:TemplateField>
            <WTF:BoundField HeaderText="用户昵称" DataField="NickName">
            </WTF:BoundField>

            <WTF:BoundField DataField="CreateDate" HeaderText="创建时间" HeaderStyle-Width="150"
                ItemStyle-Width="150" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}">
            </WTF:BoundField>
        </Columns>
    </WTF:MyGridView>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="cphScriptcbar" runat="Server">
    <script type="text/javascript">

        function RevertUserPower() {

            if (window.confirm("确认回收此用户权限?")) {
                return true;
            }
            else {
                return false;
            }

        }
    </script>
</asp:Content>

