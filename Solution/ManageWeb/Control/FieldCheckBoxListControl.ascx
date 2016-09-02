<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FieldCheckBoxListControl.ascx.cs" Inherits="Control_FieldCheckBoxListControl" %>
<tr>
    <td class="colTitle">
        <asp:Label ID="lblMustWrite" runat="server"></asp:Label>
        <asp:Label ID="lblFieldTitle" runat="server"></asp:Label>:</td>
    <td>


        <WTF:MyCheckBoxList ID="chkboxValue" RepeatLayout="Table" ValidationGroup="SaveGroup" runat="server"></WTF:MyCheckBoxList>

        <asp:Label ID="lblHintMessage" runat="server"></asp:Label>
    </td>
</tr>
