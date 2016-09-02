<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ModuleFrame.aspx.cs" Inherits="ServiceLayer_Module_ModuleFrame" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
</head>
<frameset cols="230,*" framespacing="0px" style="margin: 0; cursor: col-resize;"
    bordercolor="#9d9d9d" frameborder="1">
    <frame id="frmModuleTree" name="frmModuleTree" frameborder="0" style="border-right: solid 1px #88aad6;
        cursor: col-resize;" src="../../ServiceLayer/Module/ModuleTree.aspx" />
    <frame id="frmModuleInfo" name="frmModuleInfo" frameborder="0" style="cursor: col-resize;" />
</frameset>
</html>
