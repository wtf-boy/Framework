<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/EditPage.master" AutoEventWireup="true" CodeFile="UserPowerSet.aspx.cs" Inherits="SystemSafe_Power_UserPowerSet" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphTopToolbar" runat="Server">
    <WTF:MyToolbar ID="MyTopToolBar" runat="server"
        OperatePlaceTypeValue="OperateTopBar" OnItemCommand="CurrentTool_ItemCommand" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphContent" runat="Server">
    <asp:HiddenField ID="hidPowerModuleTypeID" runat="server" />
    <table class="tblContent">
        <colgroup>
            <col class="colTitle" style="width: 100px;" />
            <col class="colContent" />
        </colgroup>
        <tr class="trCaption">
            <td colspan="2">用户权限信息
            </td>
        </tr>
        <tr>
            <td>用户权限:
            </td>
            <td>
                <asp:Literal ID="litUserName" runat="server">

                </asp:Literal>
            </td>
        </tr>
        <tr>
            <td>用户权限:
            </td>
            <td>
                <WTF:MyUlTreeView ID="tvwPower" runat="server" DataSourceID="XmlDataSource" TreeViewHandle="objUlTreeView" ExpandDepth="1" ViewStateData="false"
                    ShowLines="True" ShowType="CheckBox">
                    <DataBindings>
                        <asp:TreeNodeBinding DataMember="Module" TextField="ModuleName" ValueField="ModuleId" />
                    </DataBindings>
                </WTF:MyUlTreeView>
                <asp:XmlDataSource ID="XmlDataSource" runat="server" EnableCaching="false"></asp:XmlDataSource>
            </td>
        </tr>
    </table>
    <div class="ajaxLoding" style="width: 200px; height: 50px; background-color: #FECEB7; display: none; position: absolute; line-height: 50px; text-align: center;">正在提交中请稍待</div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphScriptcbar" runat="Server">

    <script type="text/javascript">
        jQuery(function () {
            $(".ultreeview a:contains('***')").css("color", "red");
        });
        $(".ajaxLoding").ajaxStart(function () {
            var winWidth = $(document).width();
            var winHeight = $(document).height();
            $(this).css({ left: winWidth / 2, top: winHeight / 2 });
            $(this).show();
        });
        $("#ajaxLoding").ajaxStop(function () {
            $(this).hide();
        });
        var UserID = '<%=UserID%>';
        var AuthorizeGroupID = '<%=AuthorizeGroupID%>';
        function SavePower() {
            var isSucelss = false;
            var PowerModuleTypeID = $('#<%=hidPowerModuleTypeID.ClientID%>').val();
            $.ajax({
                url: "UserPowerHandler.ashx?<%=ModuleQuery %>",
                type: "Post",
                data: { "PowerModuleID": objUlTreeView.getSelectValue(), "UserID": UserID, "AuthorizeGroupID": AuthorizeGroupID, "PowerModuleTypeID": PowerModuleTypeID },
                dataType: "json",
                async: false,
                success: function (json) {
                    if (json.ResultCode == "0") {
                        isSucelss = true;
                    }
                    else {
                        alert(json.ResultMessage);
                        isSucelss = false;
                    }
                }
            });
            return isSucelss;
        }
    </script>
</asp:Content>

