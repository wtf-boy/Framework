<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/ListPage.master" AutoEventWireup="true" CodeFile="RoleList.aspx.cs" Inherits="SystemSafe_Role_RoleList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphTopToolbar" runat="Server">
    <WTF:MyToolbar ID="MyTopToolBar" runat="server" ModuleCode="SystemSafe_Role_RoleList"
        OperatePlaceTypeValue="OperateTopBar" OnItemCommand="CurrentTool_ItemCommand" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphSearchBar" runat="Server">
    <WTF:QueryTextBox ID="RoleName" QueryTitle="角色名称" runat="server"></WTF:QueryTextBox>
    <WTF:QueryDropDownList ID="AuthorizeGroupID" QueryTitle="授权组"
        runat="server">
    </WTF:QueryDropDownList>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphContent" runat="Server">
    <WTF:MyGridView ID="gdvContent" runat="server"
        DataKeyNames="RoleID" AutoGenerateColumns="False" OnRowCommand="CurrentContent_RowCommand"
        OnPagerChangeCommand="CurrentPager_PagerChangeCommand">
        <Columns>
            <WTF:SelectField>
            </WTF:SelectField>
            <WTF:OperateField DataTextField="RoleName" HeaderText="角色名称" HeaderStyle-Width="300"
                ItemStyle-Width="300">
            </WTF:OperateField>
            <WTF:TemplateField HeaderText="授权组" HeaderStyle-Width="200" ItemStyle-Width="200">
                <ItemTemplate>
                    <%#  GetAuthorizeGroupName(Eval("AuthorizeGroupID").ToString())%>
                </ItemTemplate>
            </WTF:TemplateField>
            <WTF:BoundField DataField="CreateDate" HeaderText="创建时间" HeaderStyle-Width="150"
                ItemStyle-Width="150" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}">
            </WTF:BoundField>
            <WTF:TemplateField HeaderText="角色备注">
                <ItemTemplate>
                    <%#  Eval("Remark").CutText(100)%>
                </ItemTemplate>
            </WTF:TemplateField>
        </Columns>
    </WTF:MyGridView>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="cphScriptcbar" runat="Server">
</asp:Content>

