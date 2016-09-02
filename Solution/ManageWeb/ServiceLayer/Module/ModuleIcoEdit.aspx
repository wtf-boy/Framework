<%@ Page Language="C#" MasterPageFile="~/App_Master/EditPage.master" AutoEventWireup="true"
    CodeFile="ModuleIcoEdit.aspx.cs" Inherits="ServiceLayer_Module_ModuleIcoEdit"
    Title="无标题页" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphTopToolbar" runat="Server">
    <WTF:MyToolbar ID="MyTopToolBar" runat="server" ModuleCode="ServiceLayer_Module_ModuleEdit"
        OperatePlaceTypeValue="OperateTopBar" OnItemCommand="CurrentTool_ItemCommand" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphContent" runat="Server">
    <table class="tblContent">
        <colgroup>
            <col class="colTitle" />
            <col class="colContent" />
            <col class="colError" />
        </colgroup>
        <tr class="trCaption">
            <td colspan="3">
                操作按钮基础信息
            </td>
        </tr>
        <tr>
            <td>
                操作按钮分类：
            </td>
            <td>
                <asp:RadioButtonList ID="radOperateTypeID" runat="server" RepeatColumns="5">
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
            <td>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="radOperateTypeID"
                    Display="Dynamic" ValidationGroup="SaveGroup" runat="server" ErrorMessage="请选择操作按钮分类"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                图标图片：
            </td>
            <td>
                <WTF:MyTextBox ID="txtImageUrl" runat="server"></WTF:MyTextBox>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                系统图标：
            </td>
            <td colspan="2" valign="top">
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
    </table>

    <script type="text/javascript">
        var objImgText = document.getElementById("<%= txtImageUrl.ClientID %>")

        function selectImg(imgPath) {
            objImgText.value = imgPath;

        }
    </script>

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphScriptcbar" runat="Server">
</asp:Content>
