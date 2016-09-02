<%@ Page Title="操作日志详情" Language="C#" MasterPageFile="~/App_Master/EditPage.master" AutoEventWireup="true" CodeFile="OperatorHistoryInfo.aspx.cs" Inherits="ServiceLayer_Loging_OperatorHistoryInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" runat="Server">
    <base target="_self"></base>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphTopToolbar" runat="Server">
    <WTF:MyToolbar ID="MyTopToolBar" runat="server" ModuleCode="" IsAddRefresh="false" OperatePlaceTypeValue="OperateTopBar"
        OnItemCommand="CurrentTool_ItemCommand">
        <Buttons>
            <WTF:ToolButton CommandName="Back" Name="返回" ImageCss="backButton" />
        </Buttons>
    </WTF:MyToolbar>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphContent" runat="Server">

    <table class="tblContent">
        <colgroup>
            <col class="colTitle" />
            <col class="colContent" />
            <col class="colTitle" />
            <col class="colContent" />
        </colgroup>
        <tr class="trCaption">
            <td colspan="2"></td>
        </tr>
        <tr>
            <td>用户标识:
            </td>
            <td>
                <%# objloger_operationhistory.UserID %>
            </td>
            <td>操作帐号:
            </td>
            <td>
                <%# objloger_operationhistory.Account %>
            </td>
        </tr>
        <tr>
            <td>操作类型;
            </td>
            <td>
                <%# ((WTF.Logging.OperationType)objloger_operationhistory.OperationTypeID).GetEnumDescription() %>
            </td>
            <td>操作命名:
            </td>
            <td>
                <%# objloger_operationhistory.CommandName %> 
            </td>
        </tr>
        <tr>
            <td>创建时间:
            </td>
            <td><%# objloger_operationhistory.CreateDate.ToString("yyyy-MM-dd HH:mm:ss") %> 
            </td>
            <td>用户IP:
            </td>
            <td><%# objloger_operationhistory.UserHostAddress %> 
            </td>
        </tr>
        <tr>
            <td>标题:
            </td>
            <td colspan="3">
                <%# objloger_operationhistory.Title %>
            </td>

        </tr>
        <tr>
            <td>操作描述:
            </td>
            <td colspan="3">
                <%# objloger_operationhistory.Description %>
            </td>
        </tr>
        <tr>
            <td>操作数据:
            </td>
            <td colspan="3">
                <WTF:MyTextBox ID="txtOperationData" TextMode="MultiLine" Width="900" Height="320" runat="server" Text="<%# objloger_operationhistory.OperationData %>"></WTF:MyTextBox>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphScriptcbar" runat="Server">
</asp:Content>

