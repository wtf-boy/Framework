<%@ Page Language="C#" MasterPageFile="~/App_Master/ListPage.master" AutoEventWireup="true"
    CodeFile="ModuleTypeList.aspx.cs" Inherits="ServiceLayer_Module_ModuleTypeList"
    Title="无标题页" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cphTopToolbar" runat="Server">
    <WTF:MyToolbar ID="MyTopToolBar" runat="server" ModuleCode="ServiceLayer_Module_ModuleTypeList"
        OperatePlaceTypeValue="OperateTopBar" OnItemCommand="CurrentTool_ItemCommand" />
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="cphSearchBar" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphContent" runat="Server">
    <WTF:MyGridView ID="gdvContent" runat="server" ModuleCode="ServiceLayer_Module_ModuleTypeList"
        OnRowCommand="CurrentContent_RowCommand" DataKeyNames="ModuleTypeID" AutoGenerateColumns="False">
        <Columns>
            <WTF:SelectField>
            </WTF:SelectField>
            <asp:BoundField DataField="ModuleTypeName" HeaderText="系统分类名称" HeaderStyle-Width="200"
                ItemStyle-Width="200" />
            <WTF:OperateField DataTextField="ModuleTypeCode" HeaderText="系统分类代码">
            </WTF:OperateField>

            <asp:BoundField DataField="ModuleTypeID" HeaderText="系统分类标识" HeaderStyle-Width="250"
                ItemStyle-Width="250" />
            <WTF:TemplateField HeaderText="是否系统" HeaderStyle-Width="120"
                ItemStyle-Width="120">
                <ItemTemplate>
                    <%#  Eval("IsSystem").ConvertBool() ? "<span class='txtNoNull'>是</span>" : "否"%>
                </ItemTemplate>
            </WTF:TemplateField>
        </Columns>
    </WTF:MyGridView>

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphScriptcbar" runat="Server">
</asp:Content>
