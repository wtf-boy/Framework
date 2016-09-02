<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FieldTextBoxControl.ascx.cs" Inherits="Control_FieldTextBoxControl" %>
<tr>
    <td class="colTitle">
  <asp:Label ID="lblMustWrite" runat="server"></asp:Label>
        <asp:Label ID="lblFieldTitle" runat="server"></asp:Label>:</td>
    <td>
        <WTF:MyTextBox ID="txtValue" runat="server" ValidationGroup="SaveGroup" Width="300"></WTF:MyTextBox>
        <asp:Label ID="lblHintMessage" runat="server"></asp:Label>
    </td>
</tr>
