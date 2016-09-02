<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/EditPage.master" AutoEventWireup="true" CodeFile="ApplicationEdit.aspx.cs" Inherits="ServiceLayer_Loging_ApplicationEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" runat="Server">
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
            <td colspan="2">程序配置</td>
        </tr>
        <tr>
            <td>
                <span class="txtNoNull">*</span>程序名称:
            </td>
            <td>
                <WTF:MyTextBox ID="txtApplicationName" ValidationGroup="SaveGroup" CheckValueEmpty="true" MaxLength="100" ErrorMessage="请输入程序名称" runat="server" Text="<%# objloger_application.ApplicationName %>"></WTF:MyTextBox>
            </td>
        </tr>
        <tr>
            <td>
                <span class="txtNoNull">*</span>程序代码:
            </td>
            <td>
                <WTF:MyTextBox ID="txtApplicationCode" Width="300" ValidationGroup="SaveGroup" CheckValueEmpty="true" MaxLength="100" ErrorMessage="请输入程序代码" runat="server" Text="<%# objloger_application.ApplicationCode %>"></WTF:MyTextBox>
            </td>
        </tr>
        <tr>
            <td>排序号:
            </td>
            <td>
                <WTF:MyTextBox ID="txtSortIndex" Width="300" ValidationGroup="SaveGroup" MaxLength="100" ValidationExpression="\d+" ErrorMessage="请输入正确排序号" runat="server" Text="<%# objloger_application.SortIndex %>"></WTF:MyTextBox>
            </td>
        </tr>
        <tr>
            <td>是否自动释放:
            </td>
            <td>
                <asp:CheckBox ID="chkIsDispose" runat="server" />
            </td>
        </tr>
        <tr class="trCaption">
            <td colspan="2">请求值配置 多个用,逗号隔开</td>
        </tr>
        <tr>
            <td>HeaderKey:
            </td>
            <td>
                <WTF:MyTextBox ID="txtHeaderKey" Width="800" ValidationGroup="SaveGroup" MaxLength="1000" runat="server" Text="<%# objloger_application.HeaderKey %>"></WTF:MyTextBox>
            </td>
        </tr>
        <tr>
            <td>RequestKey:
            </td>
            <td>
                <WTF:MyTextBox ID="txtRequestKey" Width="800" ValidationGroup="SaveGroup" MaxLength="1000" runat="server" Text="<%# objloger_application.RequestKey %>"></WTF:MyTextBox>
            </td>
        </tr>
        <tr class="trCaption">
            <td colspan="2">日志通知配置</td>
        </tr>
        <tr>
            <td>是否通知:
            </td>
            <td>
                <asp:CheckBox ID="chkIsNotice" runat="server" />
            </td>
        </tr>

        <tr>
            <td>通知日志类型:
            </td>
            <td>
                <WTF:MyCheckBoxList ID="chkNoticeCategory" runat="server" RepeatColumns="4"></WTF:MyCheckBoxList>
            </td>
        </tr>
        <tr>
            <td>发送条件:
            </td>
            <td>每分钟日志数<WTF:MyTextBox ID="txtLogerCount" Width="50" ValidationExpression="\d+" ValidationGroup="SaveGroup" MaxLength="5" ErrorMessage="请输入每分钟日志数" runat="server" Text="<%# objloger_application.LogerCount %>"></WTF:MyTextBox>
                间隔分钟数<WTF:MyDropDownList ID="dropIntervalMinutes" runat="server"></WTF:MyDropDownList>
                累计次数
                <WTF:MyTextBox ID="txtNoticeInterval" Width="50" ValidationExpression="\d+" ValidationGroup="SaveGroup" MaxLength="5" ErrorMessage="请输入累计次数" runat="server" Text="<%# objloger_application.NoticeInterval %>"></WTF:MyTextBox>
            </td>
        </tr>
        <tr>
            <td>发送条件:
            </td>
            <td>每分钟超过日志数
                <WTF:MyTextBox ID="txtMinutesMaxCount" ValidationExpression="\d+" ValidationGroup="SaveGroup" MaxLength="5" ErrorMessage="请输入累计次数" runat="server" Text="<%# objloger_application.MinutesMaxCount %>"></WTF:MyTextBox>
            </td>
        </tr>
        <tr>
            <td>通知完再次检查分钟数:
            </td>
            <td>
                <WTF:MyTextBox ID="txtNoticeSleep" ValidationExpression="\d+" ValidationGroup="SaveGroup" MaxLength="5" ErrorMessage="请输入通知完再次检查分钟数" runat="server" Text="<%# objloger_application.NoticeSleep %>"></WTF:MyTextBox>
            </td>
        </tr>
        <tr>
            <td>通知邮箱地址:
            </td>
            <td>
                <WTF:MyTextBox ID="txtNoticeEmail" Width="800" ValidationGroup="SaveGroup" MaxLength="800" ErrorMessage="请输入通知邮箱地址" runat="server" Text="<%# objloger_application.NoticeEmail %>"></WTF:MyTextBox>多个用;分号隔开
            </td>
        </tr>
        <tr>
            <td>通知手机号码:
            </td>
            <td>
                <WTF:MyTextBox ID="txtNoticePhone" Width="500" ValidationGroup="SaveGroup" MaxLength="700" ErrorMessage="请输入通知手机号码" runat="server" Text="<%# objloger_application.NoticePhone %>"></WTF:MyTextBox>多个用;分号隔开
            </td>
        </tr>
        <tr>
            <td>程序备注:
            </td>
            <td>
                <WTF:MyTextBox ID="txtRemark" ValidationGroup="SaveGroup" TextMode="MultiLine" Width="400" Height="100" MaxLength="200" ErrorMessage="请输入程序备注" runat="server" Text="<%# objloger_application.Remark %>"></WTF:MyTextBox>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphScriptcbar" runat="Server">
</asp:Content>

