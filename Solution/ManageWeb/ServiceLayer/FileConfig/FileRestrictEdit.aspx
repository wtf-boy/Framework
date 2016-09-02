<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/EditPage.master" AutoEventWireup="true" CodeFile="FileRestrictEdit.aspx.cs" Inherits="ServiceLayer_FileConfig_FileRestrictEdit" %>

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
            <td colspan="2"></td>
        </tr>

        <tr>
            <td>
                <span class="txtNoNull">*</span>限制名称:
            </td>
            <td>
                <WTF:MyTextBox ID="txtRestrictName" ValidationGroup="SaveGroup" CheckValueEmpty="true" MaxLength="100" ErrorMessage="请输入限制名称" runat="server" Text="<%# objresource_filerestrict.RestrictName %>"></WTF:MyTextBox>
            </td>
        </tr>
        <tr>
            <td>
                <span class="txtNoNull">*</span>限制码:
            </td>
            <td>
                <WTF:MyTextBox ID="txtRestrictCode" ValidationGroup="SaveGroup" CheckValueEmpty="true" MaxLength="1000" ErrorMessage="请输入限制码" runat="server" Text="<%# objresource_filerestrict.RestrictCode %>"></WTF:MyTextBox>
            </td>
        </tr>
        <tr>
            <td>
                <span class="txtNoNull">*</span>存储路经:
            </td>

            <td>

                <WTF:MyRadioButtonList ID="radFileStoragePathID" RepeatColumns="2" ValidationGroup="SaveGroup" CheckValueEmpty="true" ErrorMessage="请选择存储路经" runat="server"></WTF:MyRadioButtonList>
            </td>
        </tr>

        <tr>
            <td>
                <span class="txtNoNull">*</span>文件访问方式:
            </td>
            <td>
                <WTF:MyRadioButtonList ID="radAccessModeCodeType" RepeatColumns="2" ValidationGroup="SaveGroup" CheckValueEmpty="true" ErrorMessage="请选择文件访问方式" runat="server"></WTF:MyRadioButtonList>
            </td>
        </tr>
        <tr>
            <td>
                <span class="txtNoNull">*</span>目录存储格式:
            </td>
            <td>

                <WTF:MyRadioButtonList ID="radPathFormatCodeType" RepeatColumns="2" ValidationGroup="SaveGroup" CheckValueEmpty="true" ErrorMessage="请选择目录存储格式" runat="server"></WTF:MyRadioButtonList>
            </td>
        </tr>

        <tr>
            <td>
                <span class="txtNoNull">*</span>上传类型:
            </td>
            <td>

                <WTF:MyEnumRadioButtonList ID="radFileType" RepeatColumns="4" EnumTypeCode="ResourceRestrictType" ValidationGroup="SaveGroup" CheckValueEmpty="true" ErrorMessage="请选择上传类型" runat="server"></WTF:MyEnumRadioButtonList>
            </td>
        </tr>
        <tr>
            <td>文件扩展名:
            </td>
            <td>
                <WTF:MyTextBox ID="txtFileExtension" ValidationGroup="SaveGroup" MaxLength="256" ErrorMessage="请输入文件扩展名" runat="server" Text="<%# objresource_filerestrict.FileExtension %>"></WTF:MyTextBox>(gif,jpg,bmp,jpeg,png) 
            </td>
        </tr>
        <tr>
            <td>
                <span class="txtNoNull">*</span>文件大小:
            </td>
            <td>
                <WTF:MyTextBox ID="txtFileMaxSize" ValidationExpression="\d+" ValidationGroup="SaveGroup" CheckValueEmpty="true" ErrorMessage="请输入文件大小" runat="server" Text="<%# objresource_filerestrict.FileMaxSize %>"></WTF:MyTextBox>
                单位(K)0不限制
            </td>
        </tr>

        <tr>
            <td>是否记录历史：</td>
            <td>
                <asp:CheckBox ID="chkIsHistory" runat="server" /></td>
        </tr>
        <tr>
            <td>是否返回实际尺寸：</td>
            <td>
                <asp:CheckBox ID="chkIsReturnSize" runat="server" /></td>
        </tr>
        <tr>
            <td>是否返回Md5值：</td>
            <td>
                <asp:CheckBox ID="chkIsMd5" runat="server" /></td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphScriptcbar" runat="Server">
</asp:Content>

