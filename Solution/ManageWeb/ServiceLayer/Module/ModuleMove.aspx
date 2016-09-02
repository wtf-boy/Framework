<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/EditPage.master" AutoEventWireup="true"
    CodeFile="ModuleMove.aspx.cs" Inherits="ServiceLayer_Module_ModuleMove" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphTopToolbar" runat="Server">
    <WTF:MyToolbar ID="MyTopToolBar" runat="server" ModuleCode="ServiceLayer_Module_ModuleMove"
        OperatePlaceTypeValue="OperateTopBar" OnItemCommand="CurrentTool_ItemCommand" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphContent" runat="Server">

    <WTF:MyUlTreeView ID="tvwModule" runat="server" ExpandDepth="1" ShowType="Radio" ViewStateData="false" DataSourceID="XmlDataSource"
        ShowLines="True">
        <DataBindings>
            <asp:TreeNodeBinding DataMember="Module" TextField="ModuleName" ValueField="ModuleId" />
        </DataBindings>
    </WTF:MyUlTreeView>

    <asp:XmlDataSource ID="XmlDataSource" runat="server" EnableCaching="false"></asp:XmlDataSource>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphScriptcbar" runat="Server">
</asp:Content>
