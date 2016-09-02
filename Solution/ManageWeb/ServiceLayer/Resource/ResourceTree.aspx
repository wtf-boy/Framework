<%@ Page Language="C#" MasterPageFile="~/App_Master/TreePage.master" AutoEventWireup="true"
    CodeFile="ResourceTree.aspx.cs" Inherits="ServiceLayer_Resource_ResourceTree"
    Title="无标题页" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContent" runat="Server">
    <asp:Repeater ID="rptResourceType" runat="server">
        <HeaderTemplate>
            <ul class="boxResourceType">
        </HeaderTemplate>
        <ItemTemplate>
            <li><a target="frmResourceInfo" href='<%# EncryptModuleQuery("ResourceList.aspx?ResourceTypeID="+ Eval("ResourceTypeID"))%>'>
                <%# Eval("ResourceTypeName")%></a> </li>
        </ItemTemplate>
        <FooterTemplate>
            </ul>
        </FooterTemplate>
    </asp:Repeater>
</asp:Content>
