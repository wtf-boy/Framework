<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/EditPage.master" AutoEventWireup="true" CodeFile="PlanStepEdit.aspx.cs" Inherits="ServiceLayer_Work_PlanStepEdit" %>

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
            <td colspan="2">计划步骤
            </td>
        </tr>
        <tr>
            <td>
                <span class="txtNoNull">*</span>步骤名称:
            </td>
            <td>
                <WTF:MyTextBox ID="txtStepName" ValidationGroup="SaveGroup" CheckValueEmpty="true" MaxLength="100" ErrorMessage="请输入步骤名称" runat="server" Text="<%# objWork_PlanStep.StepName %>"></WTF:MyTextBox>
            </td>
        </tr>
        <tr>
            <td>
                <span class="txtNoNull">*</span>处理功能:
            </td>
            <td>
                <WTF:MyEnumDropDownList ID="dropProcessID" Width="200" runat="server" ValidationGroup="SaveGroup" CheckValueEmpty="true" ErrorMessage="请选择处理功能"></WTF:MyEnumDropDownList>
            </td>
        </tr>

        <tr>
            <td>
                <span class="txtNoNull">*</span>成功时要执行的操作:
            </td>
            <td>


                <WTF:MyEnumRadioButtonList ID="radSucessProcessType" runat="server">
                    <asp:ListItem Value="1" Text="退出报告失败作业"></asp:ListItem>
                    <asp:ListItem Value="2" Text="转到下一步" Selected="True"></asp:ListItem>
                    <asp:ListItem Value="3" Text="退出报告成功作业"></asp:ListItem>
                </WTF:MyEnumRadioButtonList>
            </td>
        </tr>
        <tr class="trCaption">
            <td colspan="2">步骤执行失败处理
            </td>
        </tr>
        <tr>
            <td>
                <span class="txtNoNull">*</span>重试次数:
            </td>
            <td>
                <WTF:MyTextBox ID="txtRunCount" ValidationGroup="SaveGroup" ValidationExpression="\d+" CheckValueEmpty="true" ErrorMessage="请输入重试次数" runat="server" Text="<%# objWork_PlanStep.RunCount %>"></WTF:MyTextBox>
            </td>
        </tr>
        <tr>
            <td>
                <span class="txtNoNull">*</span>重试间隔:
            </td>
            <td>
                <WTF:MyTextBox ID="txtRunInterval" ValidationGroup="SaveGroup" CheckValueEmpty="true" ValidationExpression="\d+" ErrorMessage="请输入重试间隔分钟" runat="server" Text="<%# objWork_PlanStep.RunInterval %>"></WTF:MyTextBox>单位分钟
            </td>
        </tr>
        <tr>
            <td>
                <span class="txtNoNull">*</span>失败时要执行的操作:
            </td>
            <td>
                <WTF:MyEnumRadioButtonList ID="radFailProcessType" runat="server">

                    <asp:ListItem Value="1" Text="退出报告失败作业" Selected="True"></asp:ListItem>
                    <asp:ListItem Value="2" Text="转到下一步"></asp:ListItem>
                    <asp:ListItem Value="3" Text="退出报告成功作业"> </asp:ListItem>
                </WTF:MyEnumRadioButtonList>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphScriptcbar" runat="Server">
</asp:Content>

