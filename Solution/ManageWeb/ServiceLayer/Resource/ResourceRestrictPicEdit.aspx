<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/EditPage.master" AutoEventWireup="true"
    CodeFile="ResourceRestrictPicEdit.aspx.cs" Inherits="ServiceLayer_Resource_ResourceRestrictPicEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphTopToolbar" runat="Server">
    <WTF:MyToolbar ID="MyTopToolBar" runat="server" ModuleCode="ServiceLayer_Resource_ResourceRestrictPicEdit"
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
                图片限制基本信息
            </td>
        </tr>
        <tr>
            <td>
                <span class="txtNoNull">*</span>版本号：
            </td>
            <td class="VerNo">
                <WTF:MyTextBox ID="txtVerNo" CheckValueEmpty="true" ValidationGroup="SaveGroup"
                    ValidationExpression="\d{0,5}" ErrorMessage="请输入版本号" runat="server" Text="<%# objSys_ResourceRestrictPic.VerNo %>"></WTF:MyTextBox>
            </td>
        </tr>
        <tr>
            <td>
                <span class="txtNoNull">*</span> 图片宽度：
            </td>
            <td>
                <WTF:MyTextBox ID="txtImageWidth" CheckValueEmpty="true" ValidationGroup="SaveGroup"
                    ValidationExpression="\d{0,4}" ErrorMessage="请输入图片宽度" runat="server" Text="<%# objSys_ResourceRestrictPic.ImageWidth %>"></WTF:MyTextBox>0不限制
            </td>
        </tr>
        <tr>
            <td>
                <span class="txtNoNull">*</span> 图片高度：
            </td>
            <td>
                <WTF:MyTextBox ID="txtImageHeight" CheckValueEmpty="true" ValidationGroup="SaveGroup"
                    ValidationExpression="\d{0,4}" ErrorMessage="请输入图片高度" runat="server" Text="<%# objSys_ResourceRestrictPic.ImageHeight %>"></WTF:MyTextBox>0不限制
            </td>
        </tr>
        <tr>
            <td>
                <span class="txtNoNull">*</span> 是否水印：
            </td>
            <td class="iswater">
                <asp:CheckBox ID="chkCreateWaterMark" runat="server" />
            </td>
        </tr>
        <tr class="water">
            <td>
                <span class="txtNoNull">*</span> 水印类型：
            </td>
            <td>
                <WTF:MyEnumRadioButtonList ID="radWatermarkType" RepeatColumns="4" runat="server"
                    EnumTypeCode="WatermarkType">
                </WTF:MyEnumRadioButtonList>
            </td>
        </tr>
        <tr class="water">
            <td>
                水印文字：
            </td>
            <td>
                <WTF:MyTextBox ID="txtWatermarkText" runat="server" Text="<%# objSys_ResourceRestrictPic.WatermarkText %>"></WTF:MyTextBox>
            </td>
        </tr>
        <tr class="water">
            <td>
                水印图标：
            </td>
            <td>
                <asp:DropDownList ID="dropWaterImage" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr class="water">
            <td>
                <span class="txtNoNull">*</span> 水平位置：
            </td>
            <td>
                <WTF:MyEnumRadioButtonList ID="radHorizontalAlign" RepeatColumns="4" runat="server"
                    EnumTypeCode="HorizontalAlign">
                </WTF:MyEnumRadioButtonList>
            </td>
        </tr>
        <tr class="water">
            <td>
                <span class="txtNoNull">*</span> 垂直位置：
            </td>
            <td>
                <WTF:MyEnumRadioButtonList ID="radVerticalAlign" RepeatColumns="4" runat="server"
                    EnumTypeCode="VerticalAlign">
                </WTF:MyEnumRadioButtonList>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphScriptcbar" runat="Server">
    <script type="text/javascript">

        if ($(".iswater input").prop("checked")) {

            $(".water").show();
        }
        else {

            $(".water").hide();
        }
        jQuery(function () {



            $(".iswater input").change(function () {

                if ($(this).prop("checked")) {
                    $(".water").show();

                }
                else {

                    $(".water").hide();

                }
            });


        })
    </script>
</asp:Content>
