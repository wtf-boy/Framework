<%@ Page Language="C#" MasterPageFile="~/App_Master/ListPage.master" AutoEventWireup="true"
    CodeFile="ResourceList.aspx.cs" Inherits="ServiceLayer_Resource_ResourceList"
    Title="无标题页" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphTopToolbar" runat="Server">
    <WTF:MyToolbar ID="MyTopToolBar" runat="server" ModuleCode="ServiceLayer_Resource_ResourceFrame"
        OperatePlaceTypeValue="OperateTopBar" OnItemCommand="CurrentTool_ItemCommand" />
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="cphSearchBar" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphContent" runat="Server">
    <WTF:MyGridView ID="gdvContent" runat="server" DataKeyNames="ResourceID" ModuleCode="ServiceLayer_Resource_ResourceFrame"
        AutoGenerateColumns="false" OnRowCommand="CurrentContent_RowCommand" OnPagerChangeCommand="CurrentPager_PagerChangeCommand">
        <Columns>
            <WTF:SelectField>
            </WTF:SelectField>
            <WTF:OperateField DataTextField="ResourceName" HeaderText="资源名称">
            </WTF:OperateField>

            <WTF:BoundField DataField="CreateDate" HeaderText="创建日期" HeaderStyle-Width="120"
                ItemStyle-Width="120" DataFormatString="{0:yyyy-MM-dd HH:mm}">
            </WTF:BoundField>
            <WTF:BoundField DataField="VerCount" HeaderText="版本数" HeaderStyle-Width="80"
                ItemStyle-Width="80">
            </WTF:BoundField>
        </Columns>
    </WTF:MyGridView>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphScriptcbar" runat="Server">
</asp:Content>
