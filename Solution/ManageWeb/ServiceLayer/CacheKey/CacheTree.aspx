<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/TreePage.master" AutoEventWireup="true" CodeFile="CacheTree.aspx.cs" Inherits="ServiceLayer_CacheKey_CacheTree" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContent" runat="Server">

    <WTF:MyUlTreeView ID="treeContent" runat="server" ExpandDepth="2" ViewStateData="false"
        Target="frmCacheKeyList" ShowLines="True">
        <DataBindings>
            <asp:TreeNodeBinding DataMember="TreeNodeMember" TextField="TreeNodeName" ValueField="TreeNodeID"
                NavigateUrlField="NavigateUrl" />
        </DataBindings>
    </WTF:MyUlTreeView>
    <asp:XmlDataSource ID="XmlDataSource" runat="server" EnableCaching="false"></asp:XmlDataSource>
      <script type="text/javascript" src="../../Lib/JqExtend/url.QueryHelper.js"></script>
    <script type="text/javascript">
        jQuery(function () {


            if (jQuery.query.get("CacheSiteID") != "") {
                jQuery(".ultreeview a[href*='CacheSiteID=" + jQuery.query.get("CacheSiteID") + "']").addClass("currenta");
            }

        })


    </script>
</asp:Content>

