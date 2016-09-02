<%@ Page Language="C#" MasterPageFile="~/App_Master/ListPage.master" AutoEventWireup="true"
    CodeFile="SuperUserList.aspx.cs" Inherits="ServiceLayer_User_SuperUserList" Title="无标题页" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphTopToolbar" runat="Server">
    <WTF:MyToolbar ID="MyTopToolBar" runat="server" ModuleCode="ServiceLayer_User_SuperUserList"
        OperatePlaceTypeValue="OperateTopBar" OnItemCommand="CurrentTool_ItemCommand" />
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="cphSearchBar" runat="Server">
    <WTF:QueryTextBox ID="Account" runat="server" QueryTitle="用户帐号" Width="80"></WTF:QueryTextBox>
    <WTF:QueryTextBox ID="UserName" runat="server" QueryTitle="用户名" Width="80"></WTF:QueryTextBox>
    <WTF:QueryTextBox ID="JobNo" runat="server" QueryTitle="工号" Width="80"></WTF:QueryTextBox>
    <WTF:QueryTextBox ID="NickName" runat="server" QueryTitle="昵称" Width="80"></WTF:QueryTextBox>
    <WTF:QueryTextBox ID="Email" QueryTitle="邮箱" runat="server"></WTF:QueryTextBox>
    <br />
    <WTF:QueryDropDownList ID="IsLockOut" runat="server" QueryTitle="是否锁定">
        <asp:ListItem Text="--全部--" Value=""></asp:ListItem>
        <asp:ListItem Text="锁定" Value="true"></asp:ListItem>
        <asp:ListItem Text="未锁定" Value="false"></asp:ListItem>
    </WTF:QueryDropDownList>
    <WTF:QueryDropDownList ID="IsApproved" runat="server" QueryTitle="是否审核">
        <asp:ListItem Text="--全部--" Value=""></asp:ListItem>
        <asp:ListItem Text="通过" Value="true"></asp:ListItem>
        <asp:ListItem Text="未通过" Value="false"></asp:ListItem>
    </WTF:QueryDropDownList>
    <WTF:QueryDropDownList ID="IsActivation" runat="server" QueryTitle="是否激活">
        <asp:ListItem Text="--全部--" Value=""></asp:ListItem>
        <asp:ListItem Text="是" Value="true"></asp:ListItem>
        <asp:ListItem Text="否" Value="false"></asp:ListItem>
    </WTF:QueryDropDownList>
    <WTF:QueryDropDownList ID="IsAdmin" runat="server" QueryTitle="是否主帐号">
        <asp:ListItem Text="--全部--" Value=""></asp:ListItem>
        <asp:ListItem Text="是" Value="true"></asp:ListItem>
        <asp:ListItem Text="否" Value="false"></asp:ListItem>
    </WTF:QueryDropDownList>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphContent" runat="Server">
    <WTF:MyGridView ID="gdvContent" runat="server" ModuleCode="ServiceLayer_User_SuperUserList"
        DataKeyNames="UserID" AutoGenerateColumns="False" OnRowCommand="CurrentContent_RowCommand"
        OnPagerChangeCommand="CurrentPager_PagerChangeCommand">
        <Columns>
            <WTF:SelectField>
            </WTF:SelectField>
            <WTF:BoundField HeaderText="编码" DataField="ID" HeaderStyle-Width="40"
                ItemStyle-Width="40">
            </WTF:BoundField>
            <WTF:BoundField HeaderText="工号" DataField="JobNo" HeaderStyle-Width="60"
                ItemStyle-Width="60">
            </WTF:BoundField>
            <WTF:BoundField HeaderText="用户名" DataField="UserName" HeaderStyle-Width="100"
                ItemStyle-Width="100">
            </WTF:BoundField>
            <WTF:OperateField DataTextField="Account" HeaderText="用户帐号" HeaderStyle-Width="240"
                ItemStyle-Width="240">
            </WTF:OperateField>
            <WTF:BoundField DataField="Email" HeaderText="电子邮箱">
            </WTF:BoundField>
            <WTF:TemplateField HeaderText="是否超管" SortExpression="IsSuper" HeaderStyle-Width="80"
                ItemStyle-Width="80">
                <ItemTemplate>
                    <%#  Eval("IsSuper").ConvertBool()? "<span class='txtNoNull'>超管</span>" : "普通"%>
                </ItemTemplate>
            </WTF:TemplateField>
            <WTF:TemplateField HeaderText="是否主帐号" HeaderStyle-Width="80"
                ItemStyle-Width="80">
                <ItemTemplate>
                    <%# Eval("IsAdmin").ConvertBool() ? "<span class='txtNoNull'>是</span>" : "否"%>
                </ItemTemplate>
            </WTF:TemplateField>
            <WTF:TemplateField HeaderText="是否锁定" SortExpression="IsLockOut" HeaderStyle-Width="80"
                ItemStyle-Width="80">
                <ItemTemplate>
                    <%#  Eval("IsLockOut").ConvertBool()?"<span class='txtNoNull'>是</span>":"否"%>
                </ItemTemplate>
            </WTF:TemplateField>
            <WTF:TemplateField HeaderText="是否审核" SortExpression="IsApproved" HeaderStyle-Width="80"
                ItemStyle-Width="80">
                <ItemTemplate>
                    <%#  Eval("IsApproved").ConvertBool() ? "是" : "否"%>
                </ItemTemplate>
            </WTF:TemplateField>

            <WTF:TemplateField HeaderText="是否激活" SortExpression="IsActivation" HeaderStyle-Width="80"
                ItemStyle-Width="80">
                <ItemTemplate>
                    <%#  Eval("IsActivation").ConvertBool() ? "是" : "未"%>
                </ItemTemplate>
            </WTF:TemplateField>

            <WTF:BoundField DataField="CreateDate" HeaderText="注册时期" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" HeaderStyle-Width="120"
                ItemStyle-Width="120">
            </WTF:BoundField>

        </Columns>
    </WTF:MyGridView>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphScriptcbar" runat="Server">
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
