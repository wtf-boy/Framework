<%@ Page Language="C#" MasterPageFile="~/App_Master/InfoPage.master" AutoEventWireup="true"
    CodeFile="OperationLogInfo.aspx.cs" Inherits="ServiceLayer_Loging_OperationLogInfo" Title="无标题页" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphTopToolbar" runat="Server">
    <WTF:MyToolbar ID="MyTopToolBar" runat="server" ModuleCode="ServiceLayer_Loging_LogInfo"
        OperatePlaceTypeValue="OperateTopBar" OnItemCommand="CurrentTool_ItemCommand" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphContent" runat="Server">
    <table class="tblContent">
        <colgroup>
            <col class="colTitle" style="width: 80px;" />
            <col class="colContent" />
            <col class="colTitle" style="width: 80px;" />
            <col class="colContent" />
        </colgroup>
        <tr class="trCaption">
            <td colspan="4">日志基本信息
            </td>
        </tr>
        <tr>
            <td>程序名称：
            </td>
            <td>
                <%#  objloger_operationloging.ApplicationName%>
            </td>
            <td>模块代码：
            </td>
            <td>
                <%#  objloger_operationloging.ModuleTypeCode%>
            </td>

        </tr>
        <tr>

            <td>用户帐号：
            </td>
            <td>
                <%#  objloger_operationloging.Account%>
            </td>
            <td>操作：
            </td>
            <td>
                <%#  ((WTF.Logging.OperationType)objloger_operationloging.OperationTypeID).GetEnumDescription()%>
            </td>
        </tr>
        <tr>
            <td>运行主机：
            </td>
            <td>
                <%#  objloger_operationloging.ApplicationHost%>
            </td>
            <td>记录时间：
            </td>
            <td>
                <%#  objloger_operationloging.CreateDate.ToString("yyyy-MM-dd HH:mm:ss")%>
            </td>

        </tr>
        <tr>

            <td>用户IP：
            </td>
            <td colspan="3">
                <%#  objloger_operationloging.UserHostAddress%>
            </td>

        </tr>

        <tr>
            <td>上次地址：
            </td>
            <td colspan="3">
                <%#  objloger_operationloging.UrlReferrer%>
            </td>
        </tr>
        <tr>
            <td>请求地址：
            </td>
            <td colspan="3">
                <%#  objloger_operationloging.RawUrl%>
            </td>
        </tr>

        <tr>
            <td>执行语句：
            </td>
            <td colspan="3"  style="height:700px;" valign="top">
                <asp:TextBox ID="txtSqlQuery" runat="server" TextMode="MultiLine" Width="900" Height="600" Text="<%#  objloger_operationloging.SqlQuery %>"></asp:TextBox>
            </td>
        </tr>

    </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphScriptcbar" runat="Server">
</asp:Content>
