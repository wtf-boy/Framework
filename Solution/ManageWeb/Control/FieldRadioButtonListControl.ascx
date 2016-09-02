<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FieldRadioButtonListControl.ascx.cs" Inherits="Control_FieldRadioButtonListControl" %>
<tr>
    <td class="colTitle">
         <asp:Label ID="lblMustWrite" runat="server"></asp:Label>
        <asp:Label ID="lblFieldTitle" runat="server"></asp:Label>:</td>
    <td>


        <WTF:MyRadioButtonList ID="radList" RepeatLayout="Table" ValidationGroup="SaveGroup" runat="server"></WTF:MyRadioButtonList>

        <asp:Label ID="lblHintMessage" runat="server"></asp:Label>
    </td>
</tr>
