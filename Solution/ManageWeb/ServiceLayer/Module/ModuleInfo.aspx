<%@ Page Language="C#" MasterPageFile="~/App_Master/InfoPage.master" AutoEventWireup="true"
    CodeFile="ModuleInfo.aspx.cs" Inherits="ServiceLayer_Module_ModuleInfo" Title="无标题页" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphTopToolbar" runat="Server">
    <WTF:MyToolbar ID="MyTopToolBar" runat="server" ModuleCode="ServiceLayer_Module_ModuleInfo"
        OperatePlaceTypeValue="OperateTopBar" OnItemCommand="CurrentTool_ItemCommand" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphContent" runat="Server">

    <div style="height: 30px; line-height: 30px; font-weight: bold;">
        <asp:Repeater ID="rptPath" runat="server">
            <HeaderTemplate>
                <ul>
                    <li class="left">模块路经：</li>
            </HeaderTemplate>
            <ItemTemplate>
                <li class="left">
                    <%# Eval("ModuleName")%>></li>
            </ItemTemplate>
        </asp:Repeater>
        <li class="left">模块标识：<asp:TextBox ID="txtModuleID" runat="server" Width="250"></asp:TextBox></li>

        </ul> <span class="clear"></span>
    </div>
    <table class="tblContent">


        <tr class="trCaption">
            <td>快速添加选择</td>
            <td colspan="2">配置信息</td>
        </tr>


        <tr>
            <td rowspan="5" valign="top">
                <WTF:MyUlTreeView ID="tvwQuickModule" runat="server" DataSourceID="XmlDataSource" RefFather="false" ShowExpandCollapse="true" TreeViewHandle="MyPowerTreeView"
                    ShowLines="True" ShowType="CheckBox" ViewStateData="true">
                    <DataBindings>
                        <asp:TreeNodeBinding DataMember="Module" TextField="ModuleName" ValueField="ModuleId" />
                    </DataBindings>
                </WTF:MyUlTreeView>

                <asp:XmlDataSource ID="XmlDataSource" runat="server" EnableCaching="false"></asp:XmlDataSource>
            </td>

        </tr>
        <tr>

            <td style="text-align: right">快速后缀:   </td>
            <td>
                <asp:TextBox ID="txtModuleName"
                    Width="200" runat="server"></asp:TextBox></td>


        </tr>
        <tr>

            <td style="text-align: right">编辑模块代码:  </td>
            <td>
                <asp:TextBox ID="txtModuleCode"
                    Width="200" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td colspan="2" style="height: 200px;">&nbsp;

            </td>
        </tr>
    </table>
    <WTF:MyToolbar ID="MyToolbarBottom" runat="server" ModuleCode="ServiceLayer_Module_ModuleInfo"
        OperatePlaceTypeValue="OperateBottomBar" OnItemCommand="CurrentTool_ItemCommand" />

    <script type="text/javascript">

        function Remove() {
            if (window.confirm("确认删除吗?")) {
                return true;
            }
            else {
                return false;
            }
        }
        //用户成员
        function ModuleSort() {


            showopen('<%=EncryptModuleQuery(string.Format("../../ServiceLayer/Module/ModuleSort.aspx?ModuleID={0}",ModuleID))%>', 400, 350);
            return false;
        }

    </script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphScriptcbar" runat="Server">
</asp:Content>
