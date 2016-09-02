<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ApplicationFrame.aspx.cs" Inherits="ServiceLayer_Loging_ApplicationFrame" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<frameset cols="155,*" framespacing="0px" style="margin: 0; cursor: col-resize;"
    bordercolor="#9d9d9d" frameborder="1">
    <frame id="frmApplicationTree" name="frmApplicationTree" frameborder="0" style="border-right: solid 1px #88aad6;
        cursor: col-resize;" src="ApplicationTree.aspx" />
    <frame id="frmApplicationInfo" name="frmApplicationInfo" scrolling="no" frameborder="0" src=""
        style="cursor: col-resize;" />
</frameset>
</html>
