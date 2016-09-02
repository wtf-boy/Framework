<%@ Control Language="C#" AutoEventWireup="true" CodeFile="HeaderControl.ascx.cs"
    Inherits="WorkFrame_Default_HeaderControl" %>

<div class="heaer_system_info">
    <%= SystemName %>
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
    </asp:Repeater>
    <li class="header_menu_num">
        <asp:LinkButton ID="lbtnOut" CommandName="LogingOut" runat="server" OnClick="lbtnOut_Click">注销退出</asp:LinkButton>
    </li>
    </ul> <span style="clear: both;"></span>
</div>
<script type="text/javascript">
    $(function () {
        $("#header_menu_content  li").click(function () {
            $("#header_menu_content li").removeClass("header_menu_act");
            $(this).addClass("header_menu_act");
        }
        )
        $("#header_menu_content  li:first").addClass("header_menu_act");
        if ($("#header_menu_content li").length >=1) {
            $("#frmFunction").attr("src", $("#header_menu_content a:first").attr("href"))
        }

    }
    )

</script>
