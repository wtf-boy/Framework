<%@ Page Language="C#" MasterPageFile="~/App_Master/ListPage.master" AutoEventWireup="true"
    CodeFile="ParameterTypeList.aspx.cs" Inherits="ServiceLayer_EnumType_ParameterTypeList"
    Title="无标题页" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphTopToolbar" runat="Server">
    <WTF:MyToolbar ID="MyTopToolBar" runat="server" ModuleCode="ServiceLayer_EnumType_ParameterTypeList"
        OperatePlaceTypeValue="OperateTopBar" OnItemCommand="CurrentTool_ItemCommand" />
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="cphSearchBar" runat="Server">
    <WTF:QueryTextBox ID="ParameterTypeName" QueryTitle="类型名称" runat="server"></WTF:QueryTextBox>
    <WTF:QueryTextBox ID="ParameterTypeCode" runat="server" QueryTitle="类型代码"></WTF:QueryTextBox>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphContent" runat="Server">
    <WTF:MyGridView ID="gdvContent" runat="server" ModuleCode="ServiceLayer_EnumType_ParameterTypeList"
        DataKeyNames="ParameterTypeID" AutoGenerateColumns="False" OnRowCommand="CurrentContent_RowCommand"
        OnPagerChangeCommand="CurrentPager_PagerChangeCommand">
        <Columns>
            <WTF:SelectField>
            </WTF:SelectField>
            <WTF:OperateField DataTextField="ParameterTypeName" HeaderText="枚举类型名称">
            </WTF:OperateField>
            <WTF:BoundField DataField="ParameterTypeCode" HeaderText="枚举类型代码">
            </WTF:BoundField>
            <WTF:BoundField DataField="TypeName" HeaderText="枚举类型全名" IsAutoSort="false">
            </WTF:BoundField>
        </Columns>
    </WTF:MyGridView>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphScriptcbar" runat="Server">
</asp:Content>
