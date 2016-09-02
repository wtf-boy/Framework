<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/EditPage.master" AutoEventWireup="true" CodeFile="FileRestrictPicEdit.aspx.cs" Inherits="ServiceLayer_FileConfig_FileRestrictPicEdit" %>

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
                <span class="txtNoNull">*</span>创建排序号:
            </td>
            <td>
                <WTF:MyTextBox ID="txtSortIndex" ValidationExpression="\d{0,5}" ValidationGroup="SaveGroup" CheckValueEmpty="true" ErrorMessage="请输入创建排序号" runat="server" Text="<%# objresource_filerestrictpic.SortIndex %>"></WTF:MyTextBox>
            </td>
        </tr>

        <tr>
            <td>
                <span class="txtNoNull">*</span>图片宽度:
            </td>
            <td>
                <WTF:MyTextBox ID="txtImageWidth" ValidationGroup="SaveGroup" ValidationExpression="\d{0,5}" CheckValueEmpty="true" ErrorMessage="请输入图片宽度" runat="server" Text="<%# objresource_filerestrictpic.ImageWidth %>"></WTF:MyTextBox>单位(px)0不限制
            </td>
        </tr>
        <tr>
            <td>
                <span class="txtNoNull">*</span>图片高度:
            </td>
            <td>
                <WTF:MyTextBox ID="txtImageHeight" ValidationGroup="SaveGroup" ValidationExpression="\d{0,5}" CheckValueEmpty="true" ErrorMessage="请输入图片高度" runat="server" Text="<%# objresource_filerestrictpic.ImageHeight %>"></WTF:MyTextBox>单位(px)0不限制
            </td>
        </tr>
        <tr>
            <td>
                <span class="txtNoNull">*</span>是否创建水印:
            </td>
            <td class="iswater">
                <asp:CheckBox ID="chkIsCreateWaterMark" runat="server" />
            </td>
        </tr>

        <tr class="water">
            <td>
                <span class="txtNoNull">*</span>水印类型:
            </td>
            <td>
                <WTF:MyRadioButtonList ID="radWatermarkType" EnumTypeCode="HorizontalAlign" RepeatColumns="4" ValidationGroup="SaveGroup" CheckValueEmpty="true" ErrorMessage="请选择水印类型" runat="server"></WTF:MyRadioButtonList>
            </td>
        </tr>
        <tr class="water">
            <td>水印文字:
            </td>
            <td>
                <WTF:MyTextBox ID="txtWatermarkText" ValidationGroup="SaveGroup" MaxLength="250" ErrorMessage="请输入水印文字" runat="server" Text="<%# objresource_filerestrictpic.WatermarkText %>"></WTF:MyTextBox>
            </td>
        </tr>
        <tr class="water">
            <td>
                <span class="txtNoNull">*</span>水印图标:
            </td>
            <td>
                <WTF:MyRadioButtonList ID="radWaterImageID" ValidationGroup="SaveGroup" ErrorMessage="请输入水印图标" runat="server"></WTF:MyRadioButtonList>
            </td>
        </tr>
        <tr class="water">
            <td>
                <span class="txtNoNull">*</span>水平位置:
            </td>
            <td>
                <WTF:MyEnumRadioButtonList ID="radHorizontalAlign" ValidationGroup="SaveGroup" EnumTypeCode="HorizontalAlign" RepeatColumns="5" CheckValueEmpty="true" ErrorMessage="请选择水平位置" runat="server"></WTF:MyEnumRadioButtonList>
            </td>
        </tr>
        <tr class="water">
            <td>
                <span class="txtNoNull">*</span>垂直位置:
            </td>
            <td>
                <WTF:MyEnumRadioButtonList ID="radVerticalAlign" EnumTypeCode="VerticalAlign" ValidationGroup="SaveGroup" RepeatColumns="5" CheckValueEmpty="true" ErrorMessage="请选择垂直位置" runat="server"></WTF:MyEnumRadioButtonList>
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

