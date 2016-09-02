<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/EditPage.master" AutoEventWireup="true" CodeFile="UserThemeEdit.aspx.cs" Inherits="SystemSafe_UserTheme_UserThemeEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" runat="Server">
    <style type="text/css">
        .ul_theme_list {
            padding: 10px;
        }

            .ul_theme_list li {
                padding: 10px;
                cursor: pointer;
                width: 400px;
                height: 200px;
                margin-left: 10px;
                float: left;
                border: 3px double #FFE7A2;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphTopToolbar" runat="Server">
    <WTF:MyToolbar ID="MyTopToolBar" runat="server" ModuleCode="" OperatePlaceTypeValue="OperateTopBar"
        OnItemCommand="CurrentTool_ItemCommand" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphContent" runat="Server">
    <table class="tblContent">
        <colgroup>
            <col class="colTitle" />
            <col class="colContent" />
        </colgroup>
        <tr class="trCaption">
            <td>我的主题</td>
            <td><span class="txtNoNull">主题设置成功将进行重新刷新主界面</span></td>
        </tr>

        <tr>
            <td>
                <span class="txtNoNull">*</span>操作方式:
            </td>
            <td>
                <WTF:MyEnumRadioButtonList ID="radOperateStyle" ValidationGroup="SaveGroup" RepeatColumns="2" CheckValueEmpty="true" ErrorMessage="请选择操作方式" runat="server">
                    <asp:ListItem Value="1" Selected="True" Text="右击操作"></asp:ListItem>
                    <asp:ListItem Value="2" Text="列操作"></asp:ListItem>
                </WTF:MyEnumRadioButtonList>
            </td>
        </tr>
        <tr>
            <td>
                <span class="txtNoNull">*</span>主题选择:
            </td>
            <td>

                <asp:HiddenField ID="hidModuleThemeID" runat="server" />
                <asp:Repeater ID="rptThemeList" runat="server">
                    <HeaderTemplate>
                        <ul class="ul_theme_list">
                    </HeaderTemplate>
                    <ItemTemplate>
                        <li title='点击选中[<%#Eval("ThemeConfigName") %>]' modulethemeid='<%#Eval("ModuleThemeID") %>' style="background-image: url('<%#  GetThemePreviewIco(Container.DataItem)%>')"></li>
                    </ItemTemplate>
                    <FooterTemplate>
                        <span class="clear"></span>
                        </ul>
                    </FooterTemplate>
                </asp:Repeater>

            </td>
        </tr>

    </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphScriptcbar" runat="Server">
    <script type="text/javascript">
        var hidModuleThemeID = '<%= hidModuleThemeID.ClientID %>';
        jQuery(function () {

            $(".ul_theme_list li").click(function () {

                $(".ul_theme_list li").css("border-color", "#FFE7A2");
                $(".ul_theme_list li").removeAttr("IsSelect");
                $(this).css("border-color", "red");
                $(this).attr("IsSelect", "1");
                $("#" + hidModuleThemeID).val($(this).attr("modulethemeid"));

            });
            var modulethemeid = $("#" + hidModuleThemeID).val();
            $(".ul_theme_list li").each(function () {

                if ($(this).attr("modulethemeid") == modulethemeid) {
                    $(this).css("border-color", "red");
                    $(this).attr("IsSelect", "1");
                }

            })
        });

        function SaveTheme() {

            var isSelect = false;
            $(".ul_theme_list li").each(function () {

                if ($(this).attr("IsSelect") != undefined) {
                    isSelect = true;
                }

            })
            if (!isSelect) {
                alert("请选择主题");
            }
            return isSelect;

        }

    </script>
</asp:Content>

