<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/TreePage.master" AutoEventWireup="true" CodeFile="RoleDataTree.aspx.cs" Inherits="SystemSafe_Role_RoleDataTree" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContent" runat="Server">
    <WTF:MyUlTreeView ID="tvwModule" runat="server" ExpandDepth="2" ViewStateData="false"
        Target="frmUserDataInfo" ShowLines="True">
        <DataBindings>
            <asp:TreeNodeBinding DataMember="Module" TextField="ModuleName" ValueField="ModuleId"
                NavigateUrlField="NavigateUrl" />
        </DataBindings>
    </WTF:MyUlTreeView>
    <asp:XmlDataSource ID="XmlDataSource" runat="server" EnableCaching="false"></asp:XmlDataSource>
</asp:Content>

