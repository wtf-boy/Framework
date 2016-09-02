<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/EditPage.master" AutoEventWireup="true" CodeFile="PlanRun.aspx.cs" Inherits="ServiceLayer_Work_PlanRun" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" runat="Server">
    <script type="text/javascript" src="../../App_Control/My97DatePicker/WdatePicker.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphTopToolbar" runat="Server">
    <WTF:MyToolbar ID="MyTopToolBar" runat="server" ModuleCode="" OperatePlaceTypeValue="OperateTopBar"
        OnItemCommand="CurrentTool_ItemCommand" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphContent" runat="Server">
    <table class="tblContent">
        <colgroup>
            <col class="colTitle" width="120" />
            <col class="colContent" />
        </colgroup>
        <tr class="trCaption">
            <td colspan="2">手动设置启动时间
            </td>
        </tr>
        <tr>
            <td>启动时间:
            </td>
            <td>
                <WTF:MyTextBox ID="txtRunDateTime" SkinID="Date" ValidationGroup="SaveGroup" Width="150" CheckValueEmpty="true" ErrorMessage="请输入启动时间" runat="server" onfocus="new WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})"></WTF:MyTextBox>
            </td>
        </tr>

    </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphScriptcbar" runat="Server">
</asp:Content>

