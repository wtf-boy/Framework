<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OperationLogFrame.aspx.cs" Inherits="ServiceLayer_Loging_OperationLogFrame" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
</head>
<frameset cols="155,*" framespacing="0px" style="margin: 0; cursor: col-resize;"
    bordercolor="#9d9d9d" frameborder="1">
    <frame id="frmLogTree" name="frmLogTree" frameborder="0" style="border-right: solid 1px #88aad6;
        cursor: col-resize;" src="OperationLogTree.aspx" />
    <frame id="frmLogInfo" name="frmLogInfo" scrolling="no" frameborder="0" src='<%= EncryptModuleQuery("OperationLogList.aspx?ApplicationID=1") %>'
        style="cursor: col-resize;" />
</frameset>
</html>
