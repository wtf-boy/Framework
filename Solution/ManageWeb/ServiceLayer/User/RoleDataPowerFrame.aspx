<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RoleDataPowerFrame.aspx.cs"
    Inherits="ServiceLayer_User_RoleDataPowerFrame" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<frameset cols="230,*" framespacing="0px" style="margin: 0; cursor: col-resize;"
    bordercolor="#9d9d9d" frameborder="1">
    <frame id="frmDataPowerTree" name="frmDataPowerTree" frameborder="0" style="border-right: solid 1px #88aad6;
        cursor: col-resize;" src='<%= EncryptModuleQuery("RoleDataTree.aspx?RoleID="+RoleID)%>' />
    <frame id="frmDataPowerInfo" name="frmDataPowerInfo"   frameborder="0" style="cursor: col-resize;" />
</frameset>
 
</html>

