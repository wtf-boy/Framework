<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/ListPage.master" AutoEventWireup="true" CodeFile="SolrList.aspx.cs" Inherits="ServiceLayer_Solr_SolrList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphTopToolbar" runat="Server">
    <WTF:MyToolbar ID="MyTopToolBar" runat="server" ModuleCode="" OperatePlaceTypeValue="OperateTopBar"
        OnItemCommand="CurrentTool_ItemCommand">
    </WTF:MyToolbar>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphSearchBar" runat="Server">
    <table class="tblContent">
        <colgroup>
            <col class="colTitle" style="width: 100px;" />

        </colgroup>
        <tr>
            <td>表名：</td>
            <td>
                <WTF:MyDropDownList ID="dropTableName" runat="server">
                    <asp:ListItem Value="app_freevw_install" Text="app_freevw_install"></asp:ListItem>
                    <asp:ListItem Value="app_freevw" Text="app_freevw"></asp:ListItem>
                    <asp:ListItem Value="app_softwaretb" Text="app_softwaretb"></asp:ListItem>
                    <asp:ListItem Value="app_softwaretb_android" Text="app_softwaretb_android"></asp:ListItem>
                    <asp:ListItem Value="cmsplus_articlenewtb" Text="cmsplus_articlenewtb"></asp:ListItem>
                    <asp:ListItem Value="app_newgame_tb" Text="app_newgame_tb"></asp:ListItem>
                    <asp:ListItem Value="app_product_tb" Text="app_product_tb"></asp:ListItem>
                    <asp:ListItem Value="bbs_threadvw" Text="bbs_threadvw"></asp:ListItem>
                    <asp:ListItem Value="cms_articleinfo_tb" Text="cms_articleinfo_tb"></asp:ListItem>
                    <asp:ListItem Value="wc_post_tb" Text="wc_post_tb"></asp:ListItem>
                    <asp:ListItem Value="cmsplus_articlewxtb" Text="cmsplus_articlewxtb"></asp:ListItem>
                    <asp:ListItem Value="pack_packinfo_tb" Text="pack_packinfo_tb"></asp:ListItem>
                    <asp:ListItem Value="search_searchkeyinfo_tb" Text="search_searchkeyinfo_tb"></asp:ListItem>
                    <asp:ListItem Value="bizhi_picinfo_tb" Text="bizhi_picinfo_tb"></asp:ListItem>
                    <asp:ListItem Value="pack_packinfo_vw" Text="pack_packinfo_vw"></asp:ListItem>
                </WTF:MyDropDownList>
                Solr地址:<WTF:MyDropDownList ID="dropServiceUrl" runat="server">
                    <asp:ListItem Value="Seven.Solr.ServerUrl" Text="Solr" Selected="True"></asp:ListItem>
                    <asp:ListItem Value="Seven.Solr1.ServerUrl" Text="Solr1"></asp:ListItem>
                </WTF:MyDropDownList>
                <asp:CheckBox ID="chkDebugQuery" Text="调试" runat="server" />
                开始：<asp:TextBox ID="txtStart" runat="server" Width="50" Text="0"></asp:TextBox>
                条数：<asp:TextBox ID="txtRows" runat="server" Width="50" Text="10"></asp:TextBox>

                排序<asp:TextBox ID="txtSort" runat="server" Width="300"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Query：</td>
            <td>
                <asp:TextBox ID="txtQuery" runat="server" Width="950"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>查询字段qf：</td>
            <td>
                <asp:TextBox ID="txtQF" runat="server" Width="950"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>过滤条件fq：</td>
            <td>
                <asp:TextBox ID="txtFiterQuery" runat="server" Width="950"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>bf：</td>
            <td>
                <asp:TextBox ID="txtBF" runat="server" Width="950"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>高亮字段： </td>
            <td>
                <asp:TextBox ID="txtHlField" runat="server" Width="950" Text=""></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>结果字段fl：</td>
            <td>
                <asp:TextBox ID="txtField" runat="server" Width="950"></asp:TextBox>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphContent" runat="Server">


    <asp:TextBox ID="txtResult" runat="server" Width="1000" Height="500" TextMode="MultiLine"></asp:TextBox>


</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="cphScriptcbar" runat="Server">
    <script type="text/javascript">


        $("#<%=txtResult.ClientID%>").width($(document).width());
        $("#<%=txtResult.ClientID%>").height($(document).height() - 130);

        jQuery(".tblContent").each(function ()
        {
            jQuery(this).find("colgroup col").each(function (i)
            {
                var col = this;
                var colstyle = $(col).attr("style");
                $(col).parents(".tblContent").children("tbody").children("tr").not(".trCaption,trnocol").each(function ()
                {


                    var td$ = jQuery(jQuery(this).children("td").get(i));
                    if (!td$.hasClass("nocol"))
                    {
                        td$.addClass($(col).attr("class"));

                        if (colstyle != undefined)
                        {

                            var tdstyle = td$.attr("style");
                            if (tdstyle != undefined)
                            {
                                td$.attr("style", colstyle + tdstyle);
                            }
                            else
                            {
                                td$.attr("style", colstyle);
                            }
                        }
                    }
                });
            });
        });
    </script>



</asp:Content>

