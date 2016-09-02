<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/EditPage.master" AutoEventWireup="true" CodeFile="RoleDataPower.aspx.cs" Inherits="SystemSafe_Role_RoleDataPower" %>
<%@ Import Namespace="WTF.Power.Entity" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphTopToolbar" Runat="Server">
 <WTF:MyToolbar ID="MyTopToolBar" runat="server" 
        OperatePlaceTypeValue="OperateTopBar" OnItemCommand="CurrentTool_ItemCommand" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphContent" Runat="Server">
<asp:HiddenField ID="hidDataVale" runat="server" />
    <div class="box-data-power">
        <asp:Repeater ID="rptDatalList" runat="server">
            <ItemTemplate>
                <fieldset dataid='<%# Eval("ModuleDataID")%>'>
                    <legend>
                        <%# Eval("DataName")%></legend>
                    <asp:Repeater ID="rptDataPower" runat="server" DataSource='<%# GetModuleData((Sys_ModuleData)Container.DataItem)  %>'>
                        <ItemTemplate>
                            <%# string.Format(" <input id=\"{0}\" dataValue=\"{3}\" type=\"checkbox\" name=\"{0}\" {2} /><label for=\"{0}\">{1}</label>", Eval("DataID").ToString() + Eval("DataValue").ToString(), Eval("DataTitle"), (Eval("DataSelect").ToString().ConvertStringID().IndexOf("'"+Eval("DataValue").ToString()+"'") >= 0 ? " checked=\"checked\"" : ""), Eval("DataValue"))%>
                        </ItemTemplate>
                    </asp:Repeater>
                </fieldset>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphScriptcbar" Runat="Server">
<script type="text/javascript">
    var hidDataValeID = '<%= hidDataVale.ClientID %>';
    function GetDataValue() {
        var SelectValues = "";
        $(".box-data-power fieldset").each(function () {
            SelectValues += $(this).attr("dataid");
            SelectValues += ":"
            var SelectValue = "";
            $(this).find(":checkbox").each(function () {
                if ($(this).prop("checked")) {
                    SelectValue += $(this).attr("dataValue") + ",";
                }
            });
            if (SelectValue.length > 0) {
                SelectValue = SelectValue.substring(0, SelectValue.length - 1);
            }
            SelectValues += SelectValue;
            SelectValues += ";";

        })
        SelectValues = SelectValues.substring(0, SelectValues.length - 1)
        $("#" + hidDataValeID).val(SelectValues);
        return true;
    }


    function Back() {

        window.parent.location.href = "RoleList.aspx";
        return false;
    }
   
    </script>
</asp:Content>

