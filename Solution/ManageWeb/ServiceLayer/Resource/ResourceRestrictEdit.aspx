<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/EditPage.master" AutoEventWireup="true"
    CodeFile="ResourceRestrictEdit.aspx.cs" Inherits="ServiceLayer_Resource_ResourceRestrictEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphTopToolbar" runat="Server">
    <WTF:MyToolbar ID="MyTopToolBar" runat="server" ModuleCode="ServiceLayer_Resource_ResourceRestrictEdit"
        OperatePlaceTypeValue="OperateTopBar" OnItemCommand="CurrentTool_ItemCommand" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphContent" runat="Server">
    <table class="tblContent">
        <colgroup>
            <col class="colTitle" />
            <col class="colContent" />
        </colgroup>
        <tr class="trCaption">
            <td colspan="2">
                上传限制基本信息
            </td>
        </tr>
        <tr>
            <td>
                <span class="txtNoNull">*</span> 限制代码：
            </td>
            <td>
                <WTF:MyTextBox ID="txtRestrictCode" CheckValueEmpty="true" ValidationGroup="SaveGroup"
                    ErrorMessage="请输入限制代码" runat="server" Text="<%# objSys_ResourceRestrict.RestrictCode %>"></WTF:MyTextBox>
            </td>
        </tr>
        <tr>
            <td>
                <span class="txtNoNull">*</span> 限制名称：
            </td>
            <td>
                <WTF:MyTextBox ID="txtRestrictName" CheckValueEmpty="true" ValidationGroup="SaveGroup"
                    ErrorMessage="请输入限制名称" runat="server" Text="<%# objSys_ResourceRestrict.RestrictName %>"></WTF:MyTextBox>
            </td>
        </tr>
        <tr>
            <td>
                <span class="txtNoNull">*</span> 上传类型：
            </td>
            <td>
                <WTF:MyEnumRadioButtonList ID="radRestrictType" RepeatColumns="4" runat="server"
                    EnumTypeCode="ResourceRestrictType">
                </WTF:MyEnumRadioButtonList>
            </td>
        </tr>
        <tr>
            <td>
                  文件扩展：
            </td>
            <td>
                <WTF:MyTextBox ID="txtFileExtension"  
                   runat="server" Text="<%# objSys_ResourceRestrict.FileExtension %>"></WTF:MyTextBox>(gif,jpg,bmp,jpeg,png)
            </td>
        </tr>
        <tr>
            <td>
                <span class="txtNoNull">*</span> 文件大小：
            </td>
            <td>
                <WTF:MyTextBox runat="server" ID="txtFileMaxSize" CheckValueEmpty="true" ValidationGroup="SaveGroup"
                    ValidationExpression="[1-9]{1}\d{0,5}" Text="<%# objSys_ResourceRestrict.FileMaxSize %>"></WTF:MyTextBox>单位(K)
            </td>
        </tr>
        <tr>
            <td>
                <span class="txtNoNull">*</span>版本号：
            </td>
            <td class="VerNo">
                <WTF:MyTextBox ID="txtVerNo" CheckValueEmpty="true" ValidationGroup="SaveGroup"
                    ValidationExpression="\d{0,5}" ErrorMessage="请输入版本号" runat="server" Text="<%# objSys_ResourceRestrict.VerNo %>"></WTF:MyTextBox>单位(K)
            </td>
        </tr>
        <tr class="Ver">
            <td>
                <span class="txtNoNull">*</span> 起始版本号：
            </td>
            <td>
                <WTF:MyTextBox ID="txtBeginVerNo" CheckValueEmpty="true" ValidationGroup="SaveGroup"
                    ValidationExpression="[1-9]{1}\d{0,5}" ErrorMessage="请输入起始版本号" runat="server"
                    Text="<%# objSys_ResourceRestrict.BeginVerNo %>"></WTF:MyTextBox>
            </td>
        </tr>
        <tr class="Ver">
            <td>
                <span class="txtNoNull">*</span> 结束版本号：
            </td>
            <td>
                <WTF:MyTextBox ID="txtEndVerNo" CheckValueEmpty="true" ValidationGroup="SaveGroup"
                    ValidationExpression="[1-9]{1}\d{0,5}" ErrorMessage="请输入结束版本号" runat="server"
                    Text="<%# objSys_ResourceRestrict.EndVerNo %>"></WTF:MyTextBox>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphScriptcbar" runat="Server">
    <script type="text/javascript">

        jQuery(function () {



            if ($(".VerNo input").val() == 0) {

                $(".Ver input").attr("ValidationGroup", "SaveGroup");
                $(".Ver").show();
            }
            else {
                $(".Ver input").attr("ValidationGroup", "");
                $(".Ver").hide();
            }

            $(".VerNo input").blur(function () {

                if ($(this).val() == 0) {
                    $(".Ver input").attr("ValidationGroup", "SaveGroup");
                    $(".Ver").show();

                }
                else {
                  
                    $(".Ver input").attr("ValidationGroup", "");
                    $(".Ver").hide();

                }
            });


        })
    </script>
</asp:Content>
