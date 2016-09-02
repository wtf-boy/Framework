<%@ Page Language="C#" MasterPageFile="~/App_Master/EditPage.master" AutoEventWireup="true"
    ValidateRequest="false" CodeFile="DataTemplateEdit.aspx.cs" Inherits="ServiceLayer_DataTemplate_DataTemplateEdit"
    Title="无标题页" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphTopToolbar" runat="Server">
    <WTF:MyToolbar ID="MyTopToolBar" runat="server" ModuleCode="ServiceLayer_DataTemplate_DataTemplateEdit"
        OperatePlaceTypeValue="OperateTopBar" OnItemCommand="CurrentTool_ItemCommand" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphContent" runat="Server">
    <table class="tblContent">
        <colgroup>
            <col class="colTitle" />
            <col class="colContent" />
           
        </colgroup>
        <tr class="trCaption">
            <td colspan="3">
                模板基本信息
            </td>
        </tr>
        <tr>
            <td style="height: 26px">
                <span class="txtNoNull">*</span>模板名称
            </td>
            <td style="height: 26px">
                <WTF:MyTextBox ID="txtTemplateName" CheckValueEmpty="true" ValidationGroup="SaveGroup"   ErrorMessage="请输入模板名称"  runat="server" Text="<%# objDataTemplate.TemplateName %>"></WTF:MyTextBox>
            </td>
        
        </tr>
        <tr>
            <td>
                <span class="txtNoNull">*</span>模板代码
            </td>
            <td>
                <WTF:MyTextBox ID="txtTemplateCode" CheckValueEmpty="true" ValidationGroup="SaveGroup"   ErrorMessage="请输入模板代码" runat="server" Text="<%# objDataTemplate.TemplateCode %>"></WTF:MyTextBox>
            </td>
         
        </tr>
        <tr>
            <td>
                <span class="txtNoNull">*</span>模板内容
            </td>
            <td>
                <WTF:MyTextBox ID="txtTemplateContent" CheckValueEmpty="true" ValidationGroup="SaveGroup"   ErrorMessage="请输入模板内容" TextMode="MultiLine" Width="500" Height="300"
                    SkinID="txtWidth500" runat="server" Text="<%# objDataTemplate.TemplateContent %>"></WTF:MyTextBox>
            </td>
        
        </tr>
        <tr>
            <td>
                备注
            </td>
            <td>
                <WTF:MyTextBox ID="txtRemark" runat="server" Text="<%# objDataTemplate.Remark %>"></WTF:MyTextBox>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphScriptcbar" runat="Server">
</asp:Content>
