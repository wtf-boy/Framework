<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/EditPage.master" AutoEventWireup="true" CodeFile="UserPowerInfo.aspx.cs" Inherits="SystemSafe_Power_UserPowerInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" runat="Server">
    <script type="text/javascript" src="../../Lib/JsBase/jquery-jtemplates.js"></script>
    <style type="text/css">
        .roleInfo {
            border-collapse: collapse;
            width: 100%;
        }

            .roleInfo th {
                height: 30px;
                line-height: 30px;
                text-align: center;
                color: #15428B;
            }

            .roleInfo td {
                height: 30px;
                line-height: 30px;
                text-align: center;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphTopToolbar" runat="Server">
    <WTF:MyToolbar ID="MyTopToolBar" runat="server"
        OperatePlaceTypeValue="OperateTopBar" OnItemCommand="CurrentTool_ItemCommand" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphContent" runat="Server">
    <table style="border-collapse: collapse; border: solid 1px #8DB2E3; width: 100%; background-color: #ffffff; table-layout: fixed;">
        <tr>
            <td valign="top">
                <WTF:MyUlTreeView ID="tvwPower" runat="server" DataSourceID="XmlDataSource" TreeViewHandle="objUlTreeView" ExpandDepth="1" ViewStateData="false"
                    ShowLines="True" ShowType="CheckBox">
                    <DataBindings>
                        <asp:TreeNodeBinding DataMember="Module" TextField="ModuleName" ValueField="ModuleId" />
                    </DataBindings>
                </WTF:MyUlTreeView>
                <asp:XmlDataSource ID="XmlDataSource" runat="server" EnableCaching="false"></asp:XmlDataSource>
            </td>
            <td id="RoleInfo" style="width: 300px;" valign="top"></td>
        </tr>
    </table>
    <textarea id="RoleTemp" style="display: none;">
    <table class="roleInfo" >
        <thead>
        <tr>
           
            <th>角色名称</th>
            <th>授权组名</th>
        </tr>
            </thead>
        <tbody>
        {#foreach $T.Data as row}  
        <tr>
            <td>{$T.row.RoleName}</td>
            <td>{$T.row.AuthorizeGroupName}</td>
        </tr>
        {#/for}  
            </tbody>
    </table>
        </textarea>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphScriptcbar" runat="Server">
    <script type="text/javascript">
        jQuery(function () {
            $(".ultreeview a:contains('***')").css("color", "red");
        });

        $("input:checked").next().click(function () {

            var UserID = '<%=UserID%>';
           $.ajax({
               url: "PowerRoleInfo.ashx",
               type: "Post",
               data: { "PowerModuleID": $(this).prev().val(), "UserID": UserID },
               dataType: "json",
               async: false,
               success: function (json) {
                   if (json.ResultCode == "0") {
                       // 设置模板  
                       $("#RoleInfo").setTemplateElement("RoleTemp");
                       // 处理模板  
                       $("#RoleInfo").processTemplate(json);

                   }
                   else {
                       alert(json.ResultMessage);

                   }
               }
           });


       })
    </script>
</asp:Content>

