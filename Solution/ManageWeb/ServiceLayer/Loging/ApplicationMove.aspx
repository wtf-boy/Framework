<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/EditPage.master" AutoEventWireup="true" CodeFile="ApplicationMove.aspx.cs" Inherits="ServiceLayer_Loging_ApplicationMove" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphTopToolbar" runat="Server">
    <WTF:MyToolbar ID="MyTopToolBar" runat="server" ModuleCode=""
        OperatePlaceTypeValue="OperateTopBar" OnItemCommand="CurrentTool_ItemCommand" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphContent" runat="Server">
    <WTF:MyUlTreeView ID="tvwModule" runat="server" ExpandDepth="1" ShowType="Radio" ViewStateData="false" DataSourceID="XmlDataSource"
        ShowLines="True">
        <DataBindings>
            <asp:TreeNodeBinding DataMember="TreeNodeMember" TextField="TreeNodeName" ValueField="TreeNodeID" />
        </DataBindings>
    </WTF:MyUlTreeView>

    <asp:XmlDataSource ID="XmlDataSource" runat="server" EnableCaching="false"></asp:XmlDataSource>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphScriptcbar" runat="Server">
</asp:Content>

