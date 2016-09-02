<%@ Page Title="" Language="C#" MasterPageFile="~/App_Control/Xheditor/xheditor_Seven/resource/XheditorDialog.master"
    AutoEventWireup="true" CodeFile="FlashDialog.aspx.cs" Inherits="App_Control_Xheditor_xheditor_Seven_resource_FlashDialog" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <table>
        <colgroup>
            <col style="width: 80px; text-align: right;" />
            <col />
            <col style="width: 80px; text-align: right;" />
            <col />
        </colgroup>
        <tr>
            <td>
                Flash地址：
            </td>
            <td colspan="3">
                <asp:HiddenField ID="hidResourceID" runat="server" />
                <asp:TextBox ID="txtSelectUrl" runat="server" Width="270"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Flash上传：
            </td>
            <td colspan="3">
                <WTF:MyFileRestrictUpload ID="fupFile" runat="server" />
                <asp:Button ID="btnSave" runat="server" Text="上传" CssClass="button" OnClick="btnSave_Click" />
            </td>
        </tr>
        <tr>
            <td>
                宽&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;度：
            </td>
            <td>
                <input type="text" id="xheImgWidth" style="width: 40px;" />
            </td>
            <td>
                高&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;度：
            </td>
            <td>
                <input type="text" id="xheImgHeight" style="width: 40px;" />
            </td>
        </tr>
        <tr>
            <td>
                选择Flash：
            </td>
            <td colspan="3">
                <ol id="list">
                    <asp:Repeater ID="rptResource" runat="server">
                        <ItemTemplate>
                            <li><a href="#" ondblclick="insertHTML()" onclick="setUrl('<%=txtSelectUrl.ClientID %>',this,'<%# Eval("ResourcePath") %>')">
                                <%# Eval("ResourceFileName")%>
                            </a>
                                <asp:ImageButton ID="btnRemove" runat="server" AlternateText="删除" ImageUrl="~/App_Control/Xheditor/xheditor_Seven/resource/Image/btnRemove.gif"
                                    CommandArgument='<%# Eval("VerNo") %>' OnClick="btnRemove_Click" />
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ol>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphButton" runat="Server">
    <input type="button" value="插入Flash" class="button" onclick="insertHTML()" />
    <input type="button" value="关闭窗口" class="button" onclick="unloadme();return false;" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphScript" runat="Server">
    <script type="text/javascript">
        // 在当前文档位置插入
        function insertHTML() {
            var url = $("#" + "<%=txtSelectUrl.ClientID %>").val();
            var width = $("#xheImgWidth").val();
            var height = $("#xheImgHeight").val();
            var align = $("#xheImgAlign").val();

            if (!url || url == "http://") {
                alert("请选择Flash！");
                return false;
            }

            var reg = /^\d+%?$/;
            resultHTML = "<embed type= 'application/x-shockwave-flash'   classid=\"clsid:D27CDB6E-AE6D-11cf-96B8-444553540000\" wmode=\"opaque\" quality=\"high\" menu=\"false\" play=\"true\" loop=\"true\" allowfullscreen=\"true\"";
            if (width !== '') {


                if (!reg.test(width)) {
                    alert("宽度必须为数字");
                    return;
                }
                resultHTML += ' width="' + width + '"';
            }
            if (height !== '') {

                if (!reg.test(height)) {
                    alert("高度必须为数字");
                    return;
                } resultHTML += ' height="' + height + '"';
            }
            if (url !== '') resultHTML += ' src="' + url + '"';
            resultHTML += ' />';

            callback(resultHTML);
        }
    </script>
</asp:Content>
