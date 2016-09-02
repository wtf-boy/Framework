<%@ Page Language="C#" MasterPageFile="~/App_Master/TreePage.master" AutoEventWireup="true"
    CodeFile="ModuleTree.aspx.cs" Inherits="ServiceLayer_Module_ModuleTree" Title="无标题页"
    ViewStateMode="Disabled" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContent" runat="Server">
    <WTF:MyUlTreeView ID="tvwModule" runat="server" ExpandDepth="2" ViewStateData="false"
        Target="frmModuleInfo" ShowLines="True">
        <DataBindings>
            <asp:TreeNodeBinding DataMember="Module" TextField="ModuleName" ValueField="ModuleId"
                NavigateUrlField="NavigateUrl" />
        </DataBindings>
    </WTF:MyUlTreeView>
    <asp:XmlDataSource ID="XmlDataSource" runat="server" EnableCaching="false"></asp:XmlDataSource>
    <script type="text/javascript" src="../../Lib/JqExtend/url.QueryHelper.js"></script>
    <script type="text/javascript">
        jQuery(function () {


            if (jQuery.query.get("ModuleID") != "") {
                jQuery(".ultreeview a[href*='ModuleID=" + jQuery.query.get("ModuleID") + "']").addClass("currenta");
            }

        })


    </script>
</asp:Content>
