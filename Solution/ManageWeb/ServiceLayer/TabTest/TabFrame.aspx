<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/TabPage.master" AutoEventWireup="true" CodeFile="TabFrame.aspx.cs" Inherits="ServiceLayer_TabTest_TabFrame" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphTabbar" runat="Server">
    <WTF:MyTabBar ID="myTabBar" runat="server"  OperatePlaceTypeValue="OperateListBar"     />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphScriptcbar" runat="Server">
    
</asp:Content>

