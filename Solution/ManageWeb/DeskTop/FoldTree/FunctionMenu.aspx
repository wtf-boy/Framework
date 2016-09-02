<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FunctionMenu.aspx.cs" Inherits="DeskTop_FoldTree_FunctionMenu" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
    <script type="text/javascript" src="../../Lib/JsBase/jquery.js"></script>
    <script src="../../App_Control/MyUlTreeView/UlTreeView.js" type="text/javascript"></script>

    <style type="text/css">
        .ultreeview ul li {
            line-height: 22px;
        }

            .ultreeview ul li a {
                line-height: 22px;
                color: #000000;
            }

                .ultreeview ul li a :Link, .ultreeview ul li a :Visited {
                    color: #000000;
                }

                .ultreeview ul li a :Hover, .ultreeview ul li a :Active {
                    color: #000000;
                }
    </style>
</head>
<body style="background-color: #ffffff; padding: 0px; margin: 0px; width: 100%">
    <form id="form1" runat="server">
        <dl class="boxCotePower">
            <asp:Repeater ID="rptCoteTree" runat="server">
                <ItemTemplate>
                    <dt style="background-image: url(<%#Eval("ImageUrl") %>)">
                        <%#Eval("ModuleName") %>
                    </dt>
                    <dd>
                        <WTF:MyUlTreeView ID="tvwModule" runat="server" DataSource='<%# GetTreeData(Eval("PowXml"))  %>'
                            ShowLines="True" ViewStateData="false" NavigateUrlIsClick="true">
                            <DataBindings>
                                <asp:TreeNodeBinding DataMember="Module" TextField="ModuleName" ValueField="ModuleId" NavigateUrlField="NavigateUrl" ImageUrlField="ImageUrl" />
                            </DataBindings>
                        </WTF:MyUlTreeView>
                    </dd>
                </ItemTemplate>
                <FooterTemplate>
                </FooterTemplate>
            </asp:Repeater>
        </dl>
     
    </form>
    <script type="text/javascript">

        function getShowHight() {
            var dtAllHeight = 0;
            $(".boxCotePower dt").each(function () {
                dtAllHeight += $(this).outerHeight();
            })
            return $(document).height() - dtAllHeight - 5;
        }
        jQuery(function () {
            $(".boxCotePower dt").attr("IsShow", "0");
            $(".boxCotePower dt").click(function () {

                if ($(this).attr("IsShow") == "1") {
                    $(".boxCotePower dt").attr("IsShow", "0");
                    $(this).next().slideUp("fast");

                }
                else {
                    $(".boxCotePower dt").attr("IsShow", "0");
                    $(this).attr("IsShow", "1");
                    $(this).next().height(getShowHight());
                    $(this).next().slideDown("fast").siblings("dd").not($(this)).slideUp("fast");
                }



            })
        })

        function opentreemodule(moduleid, imageUrl, href, moduleName) {

            $("#boxMainNavbar > li").css({ "background-color": "", "cursor": "pointer" });
            $(this).css({ "background-color": "#FFE7A2", "cursor": "pointer" });
            parent.OpenPageMenu(moduleid, imageUrl, href, moduleName);
            return false;

        }
    </script>
</body>
</html>
