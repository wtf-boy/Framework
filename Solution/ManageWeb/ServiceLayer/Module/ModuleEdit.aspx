<%@ Page Language="C#" MasterPageFile="~/App_Master/EditPage.master" AutoEventWireup="true"
    CodeFile="ModuleEdit.aspx.cs" Inherits="ServiceLayer_Module_ModuleEdit" Title="无标题页" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" runat="Server">
    <style type="text/css">
        .menucheck_a {
            color: #3E6AAA;
            cursor: pointer;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphTopToolbar" runat="Server">
    <WTF:MyToolbar ID="MyTopToolBar" runat="server" ModuleCode="ServiceLayer_Module_ModuleEdit"
        OperatePlaceTypeValue="OperateTopBar" OnItemCommand="CurrentTool_ItemCommand" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContent" runat="Server">
    <table class="tblContent">
        <colgroup>
            <col class="colTitle" style="width: 150px;" />
            <col class="colContent" />
        </colgroup>
        <tr class="trCaption">
            <td colspan="2">模块基本信息
            </td>
        </tr>
        <tr>
            <td>
                <span class="txtNoNull">*</span>模块名称：
            </td>
            <td>
                <WTF:MyTextBox ID="txtModuleName" CheckValueEmpty="true" ValidationGroup="SaveGroup"
                    MaxLength="50" ErrorMessage="请输入模块名称" runat="server" Text="<%# objModule.ModuleName %>"></WTF:MyTextBox>
            </td>
        </tr>
        <tr>
            <td>
                <span class="txtNoNull">*</span>模块分类：
            </td>
            <td>
                <asp:RadioButtonList ID="radModuleFunID" runat="server" RepeatColumns="5">
                    <asp:ListItem Value="1" Selected="True">系统分类</asp:ListItem>
                    <asp:ListItem Value="2">功能分类</asp:ListItem>
                    <asp:ListItem Value="3">操作按钮</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td>
                <span class="txtNoNull">*</span>模块代码：
            </td>
            <td>
                <WTF:MyTextBox ID="txtModuleCode" CheckValueEmpty="true" ValidationGroup="SaveGroup" Text="<%#objModule.ModuleCode %>"
                    Width="400" ErrorMessage="请输入模块代码" runat="server"></WTF:MyTextBox>
            </td>
        </tr>
        <tr>
            <td>栏目标识：
            </td>
            <td>
                <WTF:MyTextBox ID="txtCoteKeyID" runat="server" ValidationExpression="\d+" Text="<%# objModule.CoteKeyID %>"
                    Width="400" CheckValueEmpty="true" ValidationGroup="SaveGroup"
                    ErrorMessage="请输入正确栏目标识"></WTF:MyTextBox><asp:CheckBox ID="chkIsAutoCoteKeyID" runat="server" Text="自动生成" />
            </td>
        </tr>
        <tr>
            <td>是否显示：
            </td>
            <td>
                <asp:CheckBox ID="chkModuleShow" runat="server" Text="是" />
            </td>
        </tr>
        <tr>
            <td>是否编辑：
            </td>
            <td>
                <asp:CheckBox ID="chkIsEdit" runat="server" Text="是" />
            </td>
        </tr>
        <tr>
            <td>
                <span class="txtNoNull">*</span>提示信息：
            </td>
            <td>
                <WTF:MyTextBox ID="txtToolTip" CheckValueEmpty="true" ValidationGroup="SaveGroup"
                    ErrorMessage="请输入提示信息" runat="server" Text="<%# objModule.ToolTip %>"></WTF:MyTextBox>
            </td>
        </tr>
        <tr>
            <td>图标图片：
            </td>
            <td>
                <WTF:MyTextBox ID="txtImageUrl" runat="server" Text="<%# objModule.ImageUrl %>"></WTF:MyTextBox>
                <img class="icoPriew" src='<%# objModule.ImageUrl%>' />
            </td>
        </tr>
        <tr>
            <td>系统图标：
            </td>
            <td valign="top">
                <div style="padding: 5px;">
                    <asp:Repeater ID="rptImglist" runat="server">
                        <HeaderTemplate>
                            <ul>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <li style="float: left; width: 40px; text-align: center; height: 30px;">
                                <img style="cursor: hand;" src='<%# Eval("ResourcePath") %>' border="0" onclick=" selectImg('<%# Eval("ResourcePath") %>')" />
                            </li>
                        </ItemTemplate>
                        <FooterTemplate>
                            </ul>
                        </FooterTemplate>
                    </asp:Repeater>
                    <span class="clear"></span>
                </div>
            </td>
        </tr>

        <tr>
            <td>目标地址：
            </td>
            <td>
                <WTF:MyTextBox ID="txtCommandArgument" runat="server" Text="<%# objModule.CommandArgument %>"
                    Width="400"></WTF:MyTextBox>
            </td>
        </tr>
        <tr>
            <td>共享按钮权限：
            </td>
            <td>
                <WTF:MyTextBox ID="txtShareModuleID" runat="server" Text="<%# objModule.ShareModuleID %>"
                    Width="400"></WTF:MyTextBox>
                <br />
                <asp:Literal ID="litShareModule" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td>只允许超管授权：
            </td>
            <td>
                <asp:CheckBox ID="chkIsSupperPower" runat="server" Text="只允许超管授权" />
            </td>
        </tr>
        <tr class="trCaption">
            <td colspan="2">操作按钮基础信息
            </td>
        </tr>
        <tr>
            <td>是否授权：
            </td>
            <td>
                <asp:CheckBox ID="chkIsPower" Checked="true" runat="server" Text="是否授权" />
            </td>
        </tr>
        <tr>
            <td>操作按钮位置：
            </td>
            <td>
                <WTF:MyCheckBoxList ID="chkPlaceType" runat="server" RepeatColumns="5">
                    <asp:ListItem Value="101">顶部工具栏</asp:ListItem>
                    <asp:ListItem Value="102">列表工具栏</asp:ListItem>
                    <asp:ListItem Value="103">底部工具栏</asp:ListItem>
                </WTF:MyCheckBoxList>
            </td>
        </tr>
        <tr>
            <td>操作按钮分类：
            </td>
            <td>
                <asp:RadioButtonList ID="radOperateTypeID" runat="server" RepeatColumns="5">
                    <asp:ListItem Value="0" Selected="True">无</asp:ListItem>
                    <asp:ListItem Value="1">新增</asp:ListItem>
                    <asp:ListItem Value="2">编辑</asp:ListItem>
                    <asp:ListItem Value="3">删除</asp:ListItem>
                    <asp:ListItem Value="4">保存</asp:ListItem>
                    <asp:ListItem Value="5">返回</asp:ListItem>
                    <asp:ListItem Value="6">查询</asp:ListItem>
                    <asp:ListItem Value="7">查看</asp:ListItem>
                    <asp:ListItem Value="10">其它</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td>事件命令名：
            </td>
            <td>
                <WTF:MyTextBox ID="txtCommandName" runat="server" Text="<%# objModule.CommandName %>"></WTF:MyTextBox>
            </td>
        </tr>
        <tr>
            <td>点击执行脚本方法：
            </td>
            <td>
                <WTF:MyTextBox ID="txtClickScriptFun" runat="server" ValidationGroup="SaveGroup"
                    ErrorMessage="请输入正确脚本方法" Text="<%# objModule.ClickScriptFun %>"></WTF:MyTextBox>
            </td>
        </tr>
        <tr>
            <td>验证组名：
            </td>
            <td>
                <WTF:MyTextBox ID="txtGroupName" runat="server" Text="<%# objModule.ValGroupName %>"></WTF:MyTextBox>
            </td>
        </tr>
        <tr class="trCaption">
            <td colspan="2">菜单显示规则：<span class="txtNoNull">in（1,2,3）之内为真 not相反</span>  <a class="menucheckadd menucheck_a">添加</a>
            </td>
        </tr>
        <tr>
            <td>判断规则：

            </td>
            <td class="td_menu_check">
                <asp:HiddenField ID="hidMenuField" runat="server" />
                <asp:HiddenField ID="hidMenuCal" runat="server" />
                <asp:HiddenField ID="hidMenuValue" runat="server" />

            </td>
        </tr>
        <tr class="trCaption">
            <td colspan="2">权限基本信息
            </td>
        </tr>
        <tr>
            <td>
                <span class="txtNoNull">*</span>数据验证：
            </td>
            <td>
                <asp:CheckBox ID="chkIsCheckPowerData" runat="server" Text="是否数据验证" />
            </td>
        </tr>
        <tr>
            <td>
                <span class="txtNoNull">*</span>数据验证：
            </td>
            <td>
                <WTF:MyEnumCheckBoxList ID="chkDataList" runat="server" RepeatColumns="5">
                </WTF:MyEnumCheckBoxList>
            </td>
        </tr>
        <tr>
            <td>
                <span class="txtNoNull">*</span>栏目权限：
            </td>
            <td>
                <WTF:MyEnumDropDownList ID="dropCote" runat="server" Width="100">
                </WTF:MyEnumDropDownList>
            </td>
        </tr>
    </table>

    <div style="display: none;" id="menuchecktemp">
        <div class="menucheck">
            <input name="MenuField" type="text" class="textInput" style="width: 150px;" />
            <select name="MenuFieldCal">
                <option selected="selected" value="in">in</option>
                <option value="not">not</option>

            </select>
            <input name="MenuValue" type="text" class="textInput" style="width: 30px;" />
            <a class="menucheck_a">删除</a>
        </div>
    </div>
    <script type="text/javascript">
        var objImgText = document.getElementById("<%= txtImageUrl.ClientID %>")


        function selectImg(imgPath) {
            objImgText.value = imgPath;
            $(".icoPriew").attr("src", imgPath);

        }

        function SaveModule() {

            var MenuFields = "";
            var MenuValues = "";
            var MenuFieldCals = "";
            var ischeckval = true;
            var memuid = 1;
            $(".td_menu_check .menucheck").each(function () {

                memuid++;

                if ($(this).find("a").attr("id") == undefined) {
                    $(this).find("a").attr("id", "amenu" + memuid);
                }
                var MenuField = $.trim($(this).find("input[name='MenuField']").val());
                var MenuValue = $.trim($(this).find("input[name='MenuValue']").val());
                var MenuFieldCal = $.trim($(this).find("select[name='MenuFieldCal']").val());
                if (MenuField == '' || MenuValue == '') {
                    $(this).find("a").ShowErrorInfo("请输入字段名和验证值");
                    ischeckval = false;
                } else {
                    $(this).find("a").RemoveShowInfo();
                }
                MenuFields += MenuField + "|";
                MenuValues += MenuValue + "|";
                MenuFieldCals += MenuFieldCal + "|";

            });
            MenuFields = MenuFields.substring(0, MenuFields.length - 1);

            MenuValues = MenuValues.substring(0, MenuValues.length - 1);

            MenuFieldCals = MenuFieldCals.substring(0, MenuFieldCals.length - 1);
            $("#<%=hidMenuField.ClientID %>").val(MenuFields);
            $("#<%=hidMenuValue.ClientID %>").val(MenuValues);
            $("#<%=hidMenuCal.ClientID %>").val(MenuFieldCals);
            return ischeckval;

        }
        function loadmenucheck() {

            $(".menucheck a").live("click", function () {
                $(this).parent(".menucheck").remove();

            });
            $(".menucheckadd").click(function () {

                $("#menuchecktemp .menucheck").clone().appendTo(".td_menu_check");

            });
            var MenuFields = $("#<%=hidMenuField.ClientID %>").val();
            var MenuValues = $("#<%=hidMenuValue.ClientID %>").val();
            var MenuFieldCals = $("#<%=hidMenuCal.ClientID %>").val();

            if (MenuFields != "") {
                var memuid = 1;
                $.each(MenuFields.split('|'), function (i, n) {
                    memuid++;
                    var menucheck = $("#menuchecktemp .menucheck").clone();
                    menucheck.attr("id", "menu" + memuid)
                    menucheck.appendTo(".td_menu_check");
                    $("#" + "menu" + memuid).find("input[name='MenuField']").val(n);
                    $("#" + "menu" + memuid).find("input[name='MenuValue']").val(MenuValues.split('|')[i]);
                    $("#" + "menu" + memuid).find("select[name='MenuFieldCal']").val(MenuFieldCals.split('|')[i]);

                });
            }

        }
        jQuery(function () {
            loadmenucheck();
            $("#<%=txtModuleName.ClientID %>").blur(function () {

                if (jQuery.trim($("#<%=txtToolTip.ClientID %>").val()) == "") {

                    $("#<%=txtToolTip.ClientID %>").val($("#<%=txtModuleName.ClientID %>").val());
                }
            });




        });


    </script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphScriptcbar" runat="Server">
</asp:Content>
