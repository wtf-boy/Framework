<%@ Page Title="" Language="C#" MasterPageFile="~/App_Control/Xheditor/xheditor_Seven/resource/XheditorDialog.master"
    AutoEventWireup="true" CodeFile="ImageDialog.aspx.cs" Inherits="App_Control_Xheditor_xheditor_Seven_resource_ImageDialog" %>

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
                图片地址：
            </td>
            <td colspan="3">
                <asp:HiddenField ID="hidResourceID" runat="server" />
                <asp:TextBox ID="txtSelectUrl" runat="server" Width="270"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                图片上传：
            </td>
            <td colspan="3">
                <WTF:MyFileRestrictUpload ID="fupFile" runat="server" />
                <asp:Button ID="btnSave" runat="server" Text="上传" CssClass="button" OnClick="btnSave_Click" />
            </td>
        </tr>
        <tr>
            <td>
                替换&nbsp;&nbsp;&nbsp;&nbsp;字：
            </td>
            <td colspan="3">
                <input id="txtReplaceText" name="txtReplaceText" style="width: 270px;" />
            </td>
        </tr>
        <tr>
            <td>
                对齐方式：
            </td>
            <td>
                <select id="xheImgAlign" style="width: 50px;" >
                    <option selected="selected" value="">默认</option>
                    <option value="left">左对齐</option>
                    <option value="right">右对齐</option>
                    <option value="top">顶端</option>
                    <option value="middle">居中</option>
                    <option value="baseline">基线</option>
                    <option value="bottom">底边</option>
                </select>
            </td>
            <td>
                边框大小：
            </td>
            <td>
                <input type="text" id="xheImgBorder" style="width: 50px;" value="0" />
            </td>
        </tr>
        <tr>
            <td>
                宽&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;度：
            </td>
            <td>
                <input type="text" id="xheImgWidth" style="width: 50px;"  />
            </td>
            <td>
                高&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;度：
            </td>
            <td>
                <input type="text" id="xheImgHeight" style="width: 50px;"  />
            </td>
        </tr>
        <tr>
            <td>
                选择图片：
            </td>
            <td colspan="3">
                <ol id="list">
                    <asp:Repeater ID="rptResource" runat="server">
                        <ItemTemplate>
                            <li><a href="#" ondblclick="insertHTML()" onclick="setUrl('<%=txtSelectUrl.ClientID %>',this,'<%# Eval("ResourcePath") %>')">
                                <%# Eval("ResourceFileName")%>
                            </a>
                                <asp:ImageButton ID="btnRemove" runat="server" AlternateText="删除"  ImageUrl="~/App_Control/Xheditor/xheditor_Seven/resource/Image/btnRemove.gif"
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
    <input type="button" value="插入图片" class="button" onclick="insertHTML()" />
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
            var border = $("#xheImgBorder").val();
            var sAlt = $("#txtReplaceText").val();
            var reg = /^\d+%?$/;
            if (!url || url == "http://") {
                alert("请选择图片！");
                return false;
            }
            var resultHTML = "<a href=\"" + url + "\"   target=\"_blank\"><img   src=\"" + url + "\"";
            if (sAlt !== '') resultHTML += ' alt="' + sAlt + '"';
            if (align !== '') resultHTML += ' align="' + align + '"';
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
            if (border !== '') {


                if (!reg.test(border)) {
                    alert("边框必须为数字");
                    return;
                } resultHTML += ' border="' + border + '"';
            }
            resultHTML += ' />';
            resultHTML += "</a>";
            callback(resultHTML);
        }
    </script>
</asp:Content>
