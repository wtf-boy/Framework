<%@ Control Language="C#" AutoEventWireup="true" CodeFile="HeaderControl.ascx.cs" Inherits="DeskTop_FoldTree_HeaderControl" %>


<div id="header_cote_content">

    <div style="float: left; line-height: 30px; font-size: 14px; font-weight: bold; color: #4382DF; padding-left: 10px;">
        <%= SystemName %>
    </div>
    <div style="float: right; line-height: 30px; padding-right: 20px; font-weight: bold;">
        <asp:Literal ID="litAccount" runat="server"></asp:Literal>
        <asp:LinkButton ID="lbtnOut" CommandName="LogingOut" runat="server" OnClick="lbtnOut_Click">注销退出</asp:LinkButton>
    </div>

</div>
