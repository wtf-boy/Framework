<%@ Page Language="C#" MasterPageFile="~/App_Master/ListPage.master" AutoEventWireup="true"
    CodeFile="RoleList.aspx.cs" Inherits="ServiceLayer_User_RoleList" Title="无标题页" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphTopToolbar" runat="Server">
    <WTF:MyToolbar ID="MyTopToolBar" runat="server" ModuleCode="ServiceLayer_User_RoleList"
        OperatePlaceTypeValue="OperateTopBar" OnItemCommand="CurrentTool_ItemCommand" />
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="cphSearchBar" runat="Server">
  
    <WTF:QueryDropDownList ID="AuthorizeGroupID" QueryTitle="授权组"
        runat="server">
    </WTF:QueryDropDownList>
    <WTF:QueryTextBox ID="RoleName" QueryTitle="角色名称" runat="server"></WTF:QueryTextBox>
    <WTF:QueryDropDownList ID="IsSystem" QueryTitle="系统角色" runat="server">
        <asp:ListItem Text="--全部--" Value=""></asp:ListItem>
        <asp:ListItem Text="是" Value="true"></asp:ListItem>
        <asp:ListItem Text="否" Value="false"></asp:ListItem>
    </WTF:QueryDropDownList>
    <WTF:QueryDropDownList ID="IsUserRole" QueryTitle="用户角色" runat="server">
        <asp:ListItem Text="--全部--" Value=""></asp:ListItem>
        <asp:ListItem Text="是" Value="true"></asp:ListItem>
        <asp:ListItem Text="否" Value="false" Selected="True"></asp:ListItem>
    </WTF:QueryDropDownList>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphContent" runat="Server">
    <WTF:MyGridView ID="gdvContent" runat="server"
        DataKeyNames="RoleID" AutoGenerateColumns="False" OnRowCommand="CurrentContent_RowCommand"
        OnPagerChangeCommand="CurrentPager_PagerChangeCommand">
        <Columns>
            <WTF:SelectField>
            </WTF:SelectField>
            <WTF:OperateField DataTextField="RoleName" HeaderText="角色名称" HeaderStyle-Width="300"
                ItemStyle-Width="300">
            </WTF:OperateField>
            <WTF:TemplateField HeaderText="系统角色" SortExpression="IsSystem" HeaderStyle-Width="120"
                ItemStyle-Width="120">
                <ItemTemplate>
                    <%#  Eval("IsSystem").ConvertBool() ? "<span class='txtNoNull'>是</span>" : "否"%>
                </ItemTemplate>
            </WTF:TemplateField>
            <WTF:TemplateField HeaderText="用户角色" SortExpression="IsSystem" HeaderStyle-Width="120"
                ItemStyle-Width="120">
                <ItemTemplate>
                    <%#  Eval("IsUserRole").ConvertBool() ? "<span class='txtNoNull'>是</span>" : "否"%>
                </ItemTemplate>
            </WTF:TemplateField>
            <WTF:TemplateField HeaderText="授权组" HeaderStyle-Width="200" ItemStyle-Width="200"
                SortExpression="IsLockOut">
                <ItemTemplate>
                    <%#  GetAuthorizeGroupName(Eval("AuthorizeGroupID").ToString())%>
                </ItemTemplate>
            </WTF:TemplateField>
            <WTF:BoundField DataField="CreateDate" HeaderText="创建时间" HeaderStyle-Width="150"
                ItemStyle-Width="150" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}">
            </WTF:BoundField>

        </Columns>
    </WTF:MyGridView>
    <script type="text/javascript">
        function Remove() {
            if (window.confirm("确认删除吗?")) {
                return true;
            }
            else {
                return false;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphScriptcbar" runat="Server">
</asp:Content>
