<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FieldCheckBoxControl.ascx.cs" Inherits="Control_FieldCheckBoxControl" %>
<tr>
    <td class="colTitle">

        <asp:Label ID="lblFieldTitle" runat="server"></asp:Label>:</td>
    <td>

        <asp:CheckBox ID="chkValue" runat="server" />
        <asp:Label ID="lblHintMessage" runat="server"></asp:Label>
    </td>
</tr>
