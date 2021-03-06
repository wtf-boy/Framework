﻿<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/EditPage.master" AutoEventWireup="true"
    CodeFile="OperationLogClear.aspx.cs" Inherits="ServiceLayer_Loging_OperationLogClear" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" runat="Server">
    <script type="text/javascript" src="../../App_Control/My97DatePicker/WdatePicker.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphTopToolbar" runat="Server">
    <WTF:MyToolbar ID="MyTopToolBar" runat="server" ModuleCode="ServiceLayer_Loging_LogClear"
        OperatePlaceTypeValue="OperateTopBar" OnItemCommand="CurrentTool_ItemCommand" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphContent" runat="Server">
    <table class="tblContent">
        <colgroup>
            <col class="colTitle" />
            <col class="colContent" />
        </colgroup>
        <tr class="trCaption">
            <td colspan="2">清理日志
            </td>
        </tr>
        <tr>
            <td>是否应用子类:
            </td>
            <td>
                <asp:CheckBox ID="chkChild" runat="server" Text="是否应用子类" />
            </td>
        </tr>
        <tr>
            <td>表名:
            </td>
            <td>
                <WTF:QueryTextBox ID="TableName" runat="server" QueryMethod="Equal"></WTF:QueryTextBox>
            </td>
        </tr>
        <tr>
            <td>模块分类:
            </td>
            <td>
                <WTF:QueryDropDownList ID="ModuleTypeCode" runat="server" QueryDataType="String" QueryMethod="Equal"></WTF:QueryDropDownList>
            </td>
        </tr>
        <tr>
            <td>操作:
            </td>
            <td>
                <WTF:QueryDropDownList ID="OperationTypeID" runat="server"></WTF:QueryDropDownList>
            </td>
        </tr>

        <tr>
            <td>日期:
            </td>
            <td>
                <WTF:QueryTextBox ID="CreateDate" QueryDataType="Date" QueryMethod="LessThanOrEqual"
                    CheckValueEmpty="true" ValidationGroup="SaveGroup" SkinID="Date" onfocus="new WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})"
                    ErrorMessage="请输入清理日期" runat="server"></WTF:QueryTextBox>之前清理
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphScriptcbar" runat="Server">
    <script type="text/javascript">
        function Save() {
            if (window.confirm("确认清理吗?")) {
                return true;
            }
            else {
                return false;
            }
        }
    </script>
</asp:Content>
