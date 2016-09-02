<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/ListPage.master" AutoEventWireup="true"
    CodeFile="WaterImageList.aspx.cs" Inherits="ServiceLayer_Resource_WaterImageList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphTopToolbar" runat="Server">
    <WTF:MyToolbar ID="MyTopToolBar" runat="server" ModuleCode="ServiceLayer_Resource_WaterImageList"
        OperatePlaceTypeValue="OperateTopBar" OnItemCommand="CurrentTool_ItemCommand" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphSearchBar" runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphContent" runat="Server">
    <WTF:MyGridView ID="gdvContent" ModuleCode="ServiceLayer_Resource_WaterImageList"
        DataKeyNames="WaterImageID" AutoGenerateColumns="False" OnRowCommand="CurrentContent_RowCommand"
        OnPagerChangeCommand="CurrentPager_PagerChangeCommand" runat="server">
        <Columns>
            <WTF:SelectField>
            </WTF:SelectField>
            <WTF:OperateField HeaderText="水印名称" DataTextField="WaterName">
            </WTF:OperateField>
            <WTF:TemplateField HeaderText="">
                <ItemTemplate>
                    <%# Eval("WaterImagePath")%>
                </ItemTemplate>
            </WTF:TemplateField>
        </Columns>
    </WTF:MyGridView>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="cphScriptcbar" runat="Server">
</asp:Content>
