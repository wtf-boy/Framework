<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FieldDropDownControl.ascx.cs" Inherits="Control_FieldDropDownControl" %>
<tr>
    <td class="colTitle">
        <asp:Label ID="lblMustWrite" runat="server"></asp:Label>
        <asp:Label ID="lblFieldTitle" runat="server"></asp:Label>:</td>
    <td>
        <WTF:MyDropDownList ID="dropValue" ValidationGroup="SaveGroup" runat="server"></WTF:MyDropDownList>

        <asp:Label ID="lblHintMessage" runat="server"></asp:Label>
    </td>
</tr>
