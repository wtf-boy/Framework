<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/ListPage.master" AutoEventWireup="true"
    CodeFile="UserRole.aspx.cs" Inherits="ServiceLayer_User_UserRole" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphTopToolbar" runat="Server">
    <WTF:MyToolbar ID="MyTopToolBar" runat="server" ModuleCode="ServiceLayer_User_UserRole"
        OperatePlaceTypeValue="OperateTopBar" OnItemCommand="CurrentTool_ItemCommand" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphSearchBar" runat="Server">


    <WTF:QueryTextBox ID="RoleName" QueryTitle="角色名称" runat="server"></WTF:QueryTextBox>
    <WTF:QueryDropDownList ID="dropModuleTypeID" QueryField="ModuleTypeID" QueryTitle="模块分类"
        runat="server">
    </WTF:QueryDropDownList>
    <WTF:QueryDropDownList ID="AuthorizeGroupID" QueryTitle="授权组"
        runat="server">
    </WTF:QueryDropDownList>
    <WTF:QueryDropDownList ID="IsSystem" QueryTitle="系统角色" runat="server">
        <asp:ListItem Text="--全部--" Value=""></asp:ListItem>
        <asp:ListItem Text="是" Value="true"></asp:ListItem>
        <asp:ListItem Text="否" Value="false"></asp:ListItem>
    </WTF:QueryDropDownList>
    <WTF:QueryDropDownList ID="IsUserRole" QueryTitle="是否用户角色" runat="server">
        <asp:ListItem Text="--全部--" Value=""></asp:ListItem>
        <asp:ListItem Text="是" Value="true"></asp:ListItem>
        <asp:ListItem Text="否" Value="false" Selected="True"></asp:ListItem>
    </WTF:QueryDropDownList>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphContent" runat="Server">
    <WTF:MyGridView ID="gdvContent" runat="server" IsSelectNoInfo="false" DataKeyNames="RoleID"
        AutoGenerateColumns="False" OnRowCommand="CurrentContent_RowCommand" OnPagerChangeCommand="CurrentPager_PagerChangeCommand">
        <Columns>
            <WTF:SelectField>
            </WTF:SelectField>
            <WTF:BoundField DataField="RoleName" HeaderText="角色名称">
            </WTF:BoundField>
            <WTF:TemplateField HeaderText="授权组名" HeaderStyle-Width="100" ItemStyle-Width="100"
                SortExpression="IsLockOut">
                <ItemTemplate>
                    <%#  GetAuthorizeGroupName(Eval("AuthorizeGroupID").ToString())%>
                </ItemTemplate>
            </WTF:TemplateField>
            <WTF:TemplateField HeaderText="是否用户角色" SortExpression="IsSystem" HeaderStyle-Width="120"
                ItemStyle-Width="120">
                <ItemTemplate>
                    <%#  Eval("IsUserRole").ConvertBool() ? "<span class='txtNoNull'>是</span>" : "否"%>
                </ItemTemplate>
            </WTF:TemplateField>
            <WTF:TemplateField HeaderText="是否系统角色" SortExpression="IsSystem" HeaderStyle-Width="120"
                ItemStyle-Width="120">
                <ItemTemplate>
                    <%#  Eval("IsSystem").ConvertBool() ? "<span class='txtNoNull'>是</span>" : "否"%>
                </ItemTemplate>
            </WTF:TemplateField>

        </Columns>
    </WTF:MyGridView>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="cphScriptcbar" runat="Server">
</asp:Content>
