<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/EditPage.master" AutoEventWireup="true"
    CodeFile="WorkInfoEdit.aspx.cs" Inherits="ServiceLayer_Work_WorkInfoEdit" %>

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
            <td colspan="2">作业信息
            </td>
        </tr>
        <tr>
            <td>
                <span class="txtNoNull">*</span>作业名称:
                 
               
            </td>
            <td>
                <WTF:MyTextBox ID="txtWorkInfoName" ValidationGroup="SaveGroup" CheckValueEmpty="true"
                    MaxLength="50" ErrorMessage="请输入作业名称" runat="server" Text="<%# objWork_WorkInfo.WorkInfoName %>"></WTF:MyTextBox>
            </td>
        </tr>
        <tr>
            <td><span class="txtNoNull">*</span>运行IP或编码:
            </td>
            <td>
                <WTF:MyTextBox ID="txtRunIP" ValidationGroup="SaveGroup" MaxCharLength="50" ErrorMessage="请输入运行IP或编码"
                    CheckValueEmpty="true" Width="300" runat="server" Text="<%# objWork_WorkInfo.RunIP %>"></WTF:MyTextBox>
            </td>
        </tr>
        <tr>
            <td>
                <span class="txtNoNull">*</span>作业说明:
            </td>
            <td>
                <WTF:MyTextBox ID="txtWorkInfoRemark" ValidationGroup="SaveGroup" CheckValueEmpty="true"
                    TextMode="MultiLine" Width="400px" Height="200px" MaxLength="500" ErrorMessage="请输入作业说明"
                    runat="server" Text="<%# objWork_WorkInfo.WorkInfoRemark %>"></WTF:MyTextBox>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphScriptcbar" runat="Server">
</asp:Content>
