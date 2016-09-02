<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FunctionMenu.aspx.cs" Inherits="DeskTop_Default_FunctionMenu" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
    <script type="text/javascript" src="../../Lib/JsBase/jquery.js"></script>
</head>
<body style="background-color: #ffffff; padding: 0px; margin: 0px;">
    <form id="form1" runat="server">
        <asp:Repeater ID="rptMainNavbar" runat="server">
            <HeaderTemplate>
                <ul id="boxMainNavbar">
            </HeaderTemplate>
            <ItemTemplate>

                <li relmoduleid='<%# Eval("ModuleID") %>' refimageurl='<%# Eval("ImageUrl") %>' refhref='<%# EncryptModuleQuery( Eval("CommandArgument").ToString()) %>'
                    refname=' <%# Eval("ModuleName") %>' style='background-image: url(<%# Eval("ImageUrl") %>)'>
                    <%# Eval("ModuleName") %>
                </li>


            </ItemTemplate>
            <FooterTemplate>
                </ul>
            </FooterTemplate>
        </asp:Repeater>
    </form>
    <script type="text/javascript">

        var relModuleID = "";
        $(function () {

            $("#boxMainNavbar > li").hover(function () {
                if ($(this).attr("relModuleID") != relModuleID) {
                    $(this).css({ "background-color": "#EEEEEE", "cursor": "pointer" });
                }

            },
             function () {
                 if ($(this).attr("relModuleID") != relModuleID) {
                     $(this).css({ "background-color": "", "cursor": "pointer" });
                 }
             }
              )

            $("#boxMainNavbar > li").click(function () {

                $("#boxMainNavbar > li").css({ "background-color": "", "cursor": "pointer" });
                $(this).css({ "background-color": "#FFE7A2", "cursor": "pointer" });
                relModuleID = $(this).attr("relModuleID");
                var refImageUrl = $(this).attr("refImageUrl");
                var refHref = $(this).attr("refHref");
                var refName = $(this).attr("refName");
                parent.OpenPageMenu(relModuleID, refImageUrl, refHref, refName);
            }
            )



        }
      )

    </script>
</body>
</html>
