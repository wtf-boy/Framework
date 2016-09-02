<%@ Page Title="" Language="C#" MasterPageFile="~/App_Control/Xheditor/xheditor_Seven/resource/XheditorDialog.master"
    AutoEventWireup="true" CodeFile="DialogFile.aspx.cs" Inherits="App_Control_Xheditor_xheditor_Seven_resource_DialogFile" %>

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
                链接地址：
            </td>
            <td colspan="3">
                <asp:HiddenField ID="hidResourceID" runat="server" />
                <asp:TextBox ID="txtSelectUrl" runat="server" Width="270"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                文件上传：
            </td>
            <td colspan="3">
                <WTF:MyFileRestrictUpload ID="fupFile" runat="server" />
                <asp:Button ID="btnSave" runat="server" Text="上传" CssClass="button" OnClick="btnSave_Click" />
            </td>
        </tr>
        <tr>
            <td>
                链接文字：
            </td>
            <td colspan="3">
                <asp:TextBox ID="txtLink" runat="server" Width="270"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                选择文件：
            </td>
            <td colspan="3">
                <ol id="list">
                    <asp:Repeater ID="rptResource" runat="server">
                        <ItemTemplate>
                            <li><a href="#" ondblclick="insertHTML()" onclick="setLinkUrl('<%=txtSelectUrl.ClientID %>',this,'<%# Eval("ResourcePath") %>')">
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
    <input type="button" value="插入链接" class="button" onclick="insertHTML()" />
    <input type="button" value="关闭窗口" class="button" onclick="unloadme();return false;" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphScript" runat="Server">
    <script type="text/javascript">
        // 在当前文档位置插入

        var txtLinkid = '<%=txtLink.ClientID %>';
        function insertHTML() {
            var url = $("#" + "<%=txtSelectUrl.ClientID %>").val();
            var txtLink = $("#" + "<%=txtLink.ClientID %>").val();
            var reg = /^\d+%?$/;
            if (!url || url == "http://") {
                alert("请选链接地址！");
                return false;
            }
            if (txtLink == '') {
                alert("请输入链接名称！");
                return false;
            }
            var resultHTML = "<a href=\"" + url + "\" target=\"_blank\">" + txtLink;
            resultHTML += "</a>";
            callback(resultHTML);
        }


        function setLinkUrl(txtUrl, e, url) {
            var allChilds = document.getElementById("list").childNodes;
            $("#" + txtUrl).val(url);
            $("#list li").removeClass("selected");
            $(e).parent("li").addClass("selected");
            $("#" + txtLinkid).val($(e).text());
        }
    </script>
</asp:Content>
