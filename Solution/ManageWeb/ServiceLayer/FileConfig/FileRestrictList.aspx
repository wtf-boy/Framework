<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/ListPage.master" AutoEventWireup="true" CodeFile="FileRestrictList.aspx.cs" Inherits="ServiceLayer_FileConfig_FileRestrictList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphTopToolbar" runat="Server">

    <WTF:MyToolbar ID="MyTopToolBar" runat="server" ModuleCode="" OperatePlaceTypeValue="OperateTopBar"
        OnItemCommand="CurrentTool_ItemCommand" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphSearchBar" runat="Server">
    <WTF:QueryTextBox ID="RestrictName" QueryTitle="限制名称" runat="server"></WTF:QueryTextBox>
    <WTF:QueryTextBox ID="RestrictCode" QueryTitle="限制码" runat="server"></WTF:QueryTextBox>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphContent" runat="Server">



    <WTF:MyGridView ID="gdvContent" runat="server" ModuleCode=""
        DataKeyNames="FileRestrictID" AutoGenerateColumns="False" OnRowCommand="CurrentContent_RowCommand"
        OnPagerChangeCommand="CurrentPager_PagerChangeCommand">
        <Columns>
            <WTF:SelectField></WTF:SelectField>

            <WTF:BoundField DataField="RestrictName" HeaderText="限制名称" HeaderStyle-Width="200" ItemStyle-Width="200">
            </WTF:BoundField>
            <WTF:OperateField DataTextField="RestrictCode" HeaderText="限制码" HeaderStyle-Width="200" ItemStyle-Width="200">
            </WTF:OperateField>

            <WTF:TemplateField HeaderText="类型" SortExpression="FileType" HeaderStyle-Width="80" ItemStyle-Width="80">
                <ItemTemplate>
                    <%# GetRestrictType(Eval("FileType").ConvertInt())%>
                </ItemTemplate>
            </WTF:TemplateField>
            <WTF:TemplateField HeaderText="尺寸" SortExpression="FileType" HeaderStyle-Width="80" ItemStyle-Width="80">
                <ItemTemplate>
                    <%#  Eval("IsReturnSize").ConvertBool()?"是":""%>
                </ItemTemplate>
            </WTF:TemplateField>
            <WTF:TemplateField HeaderText="Md5" SortExpression="FileType" HeaderStyle-Width="80" ItemStyle-Width="80">
                <ItemTemplate>
                    <%#  Eval("IsMd5").ConvertBool()?"是":""%>
                </ItemTemplate>
            </WTF:TemplateField>
            <WTF:TemplateField HeaderText="历史" SortExpression="FileType" HeaderStyle-Width="80" ItemStyle-Width="80">
                <ItemTemplate>
                    <%#  Eval("IsHistory").ConvertBool()?"是":""%>
                </ItemTemplate>
            </WTF:TemplateField>
            <WTF:BoundField DataField="FileExtension" HeaderText="文件扩展名">
            </WTF:BoundField>
            <WTF:TemplateField HeaderText="存储位置" HeaderStyle-Width="200" ItemStyle-Width="200">
                <ItemTemplate>
                    <%#  ((WTF.Resource.Entity.resource_filestoragepath)Eval("resource_filestoragepath")).StoragePathName%>
                </ItemTemplate>
            </WTF:TemplateField>

        </Columns>
    </WTF:MyGridView>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="cphScriptcbar" runat="Server">
</asp:Content>

