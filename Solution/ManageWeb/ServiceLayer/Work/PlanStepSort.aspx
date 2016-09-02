<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/PanelPage.master" AutoEventWireup="true" CodeFile="PlanStepSort.aspx.cs" Inherits="ServiceLayer_Work_PlanStepSort" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPanel" Runat="Server">

    
    <asp:HiddenField ID="hidSortID" runat="server" />
    <table width="100%">
        <tr>
            <td>
                <asp:ListBox ID="lboxSort" Height="260px" Width="255px" runat="server"></asp:ListBox>
            </td>
            <td align="left">
                <input type="button" onclick="UpTop();" value="置顶" />
                <br />
                <br />
                <input type="button" onclick="Up();" value="上移" />
                <br />
                <br />
                <input type="button" onclick="Down();" value="下移" />
                <br />
                <br />
                <input type="button" onclick="DownBottom();" value="置底" />
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <asp:Button ID="btnSave" runat="server" Text="保存" OnClick="btnSave_Click" />
                <asp:Button ID="btnClose" OnClientClick=" window.close();return false;" runat="server"
                    Text="关闭" />
            </td>
        </tr>
    </table>
    <script type="text/javascript">


        //初始化
        var objSelect = document.getElementById("<%= lboxSort.ClientID %>")
        var SortedIDObj = document.getElementById("<%= hidSortID.ClientID %>")

        //移至最顶
        function UpTop() {
            var nowIndex = objSelect.selectedIndex;
            if ((objSelect.value != "") && (nowIndex != 0)) {
                tempValue = objSelect.item(nowIndex).value;
                tempName = objSelect.item(nowIndex).text;
                for (i = nowIndex; i > 0; i--) {
                    objSelect.item(i).value = objSelect.item(i - 1).value;
                    objSelect.item(i).text = objSelect.item(i - 1).text;
                }
                objSelect.item(0).value = tempValue;
                objSelect.item(0).text = tempName;
                objSelect.item(0).selected = true;
            }
            ToSortIndex();
        }

        //移至最底
        function DownBottom() {
            var nowIndex = objSelect.selectedIndex;
            if ((objSelect.value != "") && (nowIndex != objSelect.length - 1)) {
                tempValue = objSelect.item(nowIndex).value;
                tempName = objSelect.item(nowIndex).text;
                for (i = nowIndex; i < objSelect.length - 1; i++) {
                    objSelect.item(i).value = objSelect.item(i + 1).value;
                    objSelect.item(i).text = objSelect.item(i + 1).text;
                }
                objSelect.item(objSelect.length - 1).value = tempValue;
                objSelect.item(objSelect.length - 1).text = tempName;
                objSelect.item(objSelect.length - 1).selected = true;
            }
            ToSortIndex();
        }

        //下移
        function Down() {
            var nowIndex = objSelect.selectedIndex;
            if ((objSelect.value != "") && (nowIndex != objSelect.length - 1)) {
                tempValue = objSelect.item(nowIndex).value;
                tempName = objSelect.item(nowIndex).text;
                objSelect.item(nowIndex).value = objSelect.item(nowIndex + 1).value;
                objSelect.item(nowIndex).text = objSelect.item(nowIndex + 1).text;
                objSelect.item(nowIndex + 1).value = tempValue;
                objSelect.item(nowIndex + 1).text = tempName;
                objSelect.item(nowIndex + 1).selected = true;
            }
            ToSortIndex();
        }

        //上移
        function Up() {
            var nowIndex = objSelect.selectedIndex;
            if ((objSelect.value != "") && (nowIndex != 0)) {
                tempValue = objSelect.item(nowIndex - 1).value;
                tempName = objSelect.item(nowIndex - 1).text;
                objSelect.item(nowIndex - 1).value = objSelect.item(nowIndex).value;
                objSelect.item(nowIndex - 1).text = objSelect.item(nowIndex).text;
                objSelect.item(nowIndex).value = tempValue;
                objSelect.item(nowIndex).text = tempName;
                objSelect.item(nowIndex - 1).selected = true;
            }
            ToSortIndex();
        }

        //生成排序字符串
        function ToSortIndex() {
            var SortTreeID = "";
            for (i = 0; i < objSelect.length; i++) {
                SortTreeID = SortTreeID + objSelect.item(i).value + ","
            }
            SortTreeID = SortTreeID.substring(0, SortTreeID.length - 1);
            SortedIDObj.value = SortTreeID;
        }
    </script>
</asp:Content>

