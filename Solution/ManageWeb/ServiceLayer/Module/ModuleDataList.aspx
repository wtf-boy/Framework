<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/ListPage.master" AutoEventWireup="true"
    CodeFile="ModuleDataList.aspx.cs" Inherits="ServiceLayer_Module_ModuleDataList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphTopToolbar" runat="Server">
    <WTF:MyToolbar ID="MyTopToolBar" runat="server" ModuleCode="ServiceLayer_Module_ModuleDataList"
        OperatePlaceTypeValue="OperateTopBar" OnItemCommand="CurrentTool_ItemCommand" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphSearchBar" runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphContent" runat="Server">
    <WTF:MyGridView ID="gdvContent" runat="server" ModuleCode="ServiceLayer_Module_ModuleDataList"
        DataKeyNames="ModuleDataID" AutoGenerateColumns="False" OnRowCommand="CurrentContent_RowCommand"
        OnPagerChangeCommand="CurrentPager_PagerChangeCommand">
        <Columns>
            <WTF:SelectField>
            </WTF:SelectField>
            <WTF:BoundField DataField="ConnectionKey" HeaderText="连接串值">
            </WTF:BoundField>
            <WTF:OperateField DataTextField="DataName" HeaderText="数据名称">
            </WTF:OperateField>
            <WTF:BoundField DataField="FieldName" HeaderText="字段名">
            </WTF:BoundField>
            <WTF:TemplateField HeaderText="数据类型">
                <ItemTemplate>
                    <%#  Eval("FieldType").ConvertInt() == 1 ? "int" : Eval("FieldType").ConvertInt() == 2 ? "stiring" : "guid"%>
                </ItemTemplate>
            </WTF:TemplateField>
        </Columns>
    </WTF:MyGridView>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="cphScriptcbar" runat="Server">
</asp:Content>
