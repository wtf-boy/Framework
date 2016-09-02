<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/PanelPage.master" AutoEventWireup="true" CodeFile="ThemeImport.aspx.cs" Inherits="ServiceLayer_Module_ThemeImport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPanel" runat="Server">

    <table width="100%">
        <tr>
            <td>
                <asp:ListBox ID="lboxThemeConfig" Height="360px" Width="355px" runat="server"
                    SelectionMode="Multiple"></asp:ListBox>
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
</asp:Content>

