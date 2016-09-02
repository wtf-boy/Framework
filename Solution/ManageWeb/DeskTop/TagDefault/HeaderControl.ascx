<%@ Control Language="C#" AutoEventWireup="true" CodeFile="HeaderControl.ascx.cs"
    Inherits="WorkFrame_TagDefault_HeaderControl" %>
<div id="header_cote_content">

    <div style="float: left; line-height: 30px; font-size: 14px; font-weight: bold; color: #4382DF; padding-left: 10px;">
        <%= SystemName %>
    </div>
    <div style="float: right; line-height: 30px; padding-right: 20px; font-weight: bold;">
        <asp:Literal ID="litAccount" runat="server"></asp:Literal>
        <asp:LinkButton ID="LinkButton1" CommandName="LogingOut" runat="server" OnClick="lbtnOut_Click">注销退出</asp:LinkButton>
    </div>

</div>
<div id="header_menu_content">
    <asp:Repeater ID="rptModule" runat="server" OnItemCommand="rptModule_ItemCommand">
        <HeaderTemplate>
            <ul>
        </HeaderTemplate>
        <ItemTemplate>
            <li class="header_menu_num"><a id='Menu<%# ((int)DataBinder.Eval(Container, "ItemIndex")).ToString() %>'
                target="frmFunction" <%# Eval("ClickScriptFun").IsNoNull()?"onclick='"+Eval("ClickScriptFun")+"'":"" %>
                href='<%#  Eval("ClickScriptFun").IsNoNull()?"javascript:void(0)": Eval("TargetUrl").IsNull()? EncryptModuleQuery(string.Format("FunctionMenu.aspx?ModuleCode={0}",  Eval("ModuleCode"))): EncryptModuleQuery(string.Format(Eval("TargetUrl").ToString()+"?ModuleCode={0}",  Eval("ModuleCode")))  %>'>
                <%# Eval("ModuleName") %>
            </a></li>
        </ItemTemplate>
        <FooterTemplate>
            </ul>

        </FooterTemplate>
    </asp:Repeater>
    <span style="clear: both;"></span>
</div>
<script type="text/javascript">
    $(function () {
        $("#header_menu_content  li").click(function () {
            $("#header_menu_content li").removeClass("header_menu_act");
            $(this).addClass("header_menu_act");
        }
        )
        $("#header_menu_content  li:first").addClass("header_menu_act");
        if ($("#header_menu_content li").length >= 1) {
            $("#frmFunction").attr("src", $("#header_menu_content a:first").attr("href"))
        }

    }
    )

</script>
