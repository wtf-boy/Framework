<%@ Page Language="C#" MasterPageFile="~/App_Master/ListPage.master" AutoEventWireup="true"
    CodeFile="DataTemplateTypeList.aspx.cs" Inherits="ServiceLayer_DataTemplate_DataTemplateTypeList"
    Title="无标题页" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphTopToolbar" runat="Server">
    <WTF:MyToolbar ID="MyTopToolBar" runat="server" ModuleCode="ServiceLayer_DataTemplate_DataTemplateTypeList"
        OperatePlaceTypeValue="OperateTopBar" OnItemCommand="CurrentTool_ItemCommand" />
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="cphSearchBar" runat="Server">
    <WTF:QueryTextBox QueryTitle="类型名称" ID="DataTemplateTypeName" runat="server"></WTF:QueryTextBox>
    <WTF:QueryTextBox ID="DataTemplateTypeCode" QueryTitle="类型代码" runat="server"></WTF:QueryTextBox>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphContent" runat="Server">
    <WTF:MyGridView ID="gdvContent" runat="server" ModuleCode="ServiceLayer_DataTemplate_DataTemplateTypeList"
        DataKeyNames="DataTemplateTypeID" AutoGenerateColumns="False" OnRowCommand="CurrentContent_RowCommand"
        OnPagerChangeCommand="CurrentPager_PagerChangeCommand">
        <Columns>
            <WTF:SelectField>
            </WTF:SelectField>
            <WTF:OperateField DataTextField="DataTemplateTypeName" HeaderText="数据模板类型名称">
            </WTF:OperateField>
            <WTF:BoundField DataField="DataTemplateTypeCode" HeaderText="数据模板类型代码">
            </WTF:BoundField>
            <WTF:BoundField DataField="Remark" HeaderText="数据模板类型备注" IsAutoSort="false">
            </WTF:BoundField>
        </Columns>
    </WTF:MyGridView>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphScriptcbar" runat="Server">
</asp:Content>
