<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/TreePage.master" AutoEventWireup="true" CodeFile="LogTree.aspx.cs" Inherits="ServiceLayer_Loging_LogTree" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContent" runat="Server">

    <WTF:MyUlTreeView ID="treeContent" runat="server" ExpandDepth="1" ViewStateData="false"
        Target="frmLogInfo" ShowLines="True">
        <DataBindings>
            <asp:TreeNodeBinding DataMember="TreeNodeMember" TextField="TreeNodeName" ValueField="TreeNodeID"
                NavigateUrlField="NavigateUrl" />
        </DataBindings>
    </WTF:MyUlTreeView>
    <asp:XmlDataSource ID="XmlDataSource" runat="server" EnableCaching="false"></asp:XmlDataSource>

</asp:Content>

