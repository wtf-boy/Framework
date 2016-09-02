<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/EditPage.master" AutoEventWireup="true"
    CodeFile="WorkLogClear.aspx.cs" Inherits="ServiceLayer_Work_WorkLogClear" %>

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
            <col class="colTitle" />
            <col class="colContent" />
        </colgroup>
        <tr class="trCaption">
            <td colspan="2">
                日志清理
            </td>
        </tr>
        <tr>
            <td>
                <span class="txtNoNull">*</span>作业名称:
            </td>
            <td>
                 <WTF:QueryHiddenField ID="ResultType" runat="server" QueryMethod="NotEqual" Value="0" />
                <WTF:QueryDropDownList ID="WorkInfoID" runat="server">
                </WTF:QueryDropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <span class="txtNoNull">*</span>创建时间:
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
</asp:Content>
