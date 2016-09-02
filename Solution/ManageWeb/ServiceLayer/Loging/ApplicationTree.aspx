<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/TreePage.master" AutoEventWireup="true" CodeFile="ApplicationTree.aspx.cs" Inherits="ServiceLayer_Loging_ApplicationTree" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContent" runat="Server">

    <WTF:MyUlTreeView ID="treeContent" runat="server" ExpandDepth="1" ViewStateData="false"
        Target="frmApplicationInfo" ShowLines="True">
        <DataBindings>
            <asp:TreeNodeBinding DataMember="TreeNodeMember" TextField="TreeNodeName" ValueField="TreeNodeID"
                NavigateUrlField="NavigateUrl" />
        </DataBindings>
    </WTF:MyUlTreeView>
    <asp:XmlDataSource ID="XmlDataSource" runat="server" EnableCaching="false"></asp:XmlDataSource>


    <script type="text/javascript" src="../../Lib/JqExtend/url.QueryHelper.js"></script>
    <script type="text/javascript">
        jQuery(function () {


            if (jQuery.query.get("ApplicationID") != "") {
                jQuery(".ultreeview a[href*='ApplicationID=" + jQuery.query.get("ApplicationID") + "']").addClass("currenta");
            }

        })


    </script>
</asp:Content>

