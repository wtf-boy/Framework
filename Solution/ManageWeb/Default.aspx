<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Seven开发平台 后台登录</title>
    <script type="text/javascript" src="Lib/JsBase/jquery.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div style="height: 200px;">
        </div>
        <div style="background: url(App_Themes/Default/Image/ico_SysLogin_Bg.jpg) repeat-x left top;">
            <div style="background: url(App_Themes/Default/Image/ico_SysLoginBg.jpg) no-repeat right top; height: 271px; margin-left: auto; margin-right: auto; width: 1002px;">
                <div style="float: right; padding-top: 100px; padding-right: 100px; width: 300px;">
                    <ul style="line-height: 30px;">
                        <li>帐&nbsp;&nbsp;号：<WTF:MyTextBox ID="txtAccount" runat="server" Width="150"></WTF:MyTextBox><asp:RequiredFieldValidator
                            ID="valAccount" runat="server" ControlToValidate="txtAccount"
                            ValidationGroup="Login"
                            ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator></li>
                        <li>密&nbsp;&nbsp;码：<WTF:MyTextBox ID="txtPassword" TextMode="Password" Width="150"
                            runat="server"></WTF:MyTextBox><asp:RequiredFieldValidator ID="valPassword" runat="server"
                                ValidationGroup="Login" ControlToValidate="txtPassword" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                        </li>
                        <li>是否记住登录：<asp:CheckBox ID="chkLogin" runat="server" Checked="false" /></li>
                        <li style="text-align: center;">

                            <asp:Button ID="btnLogin" runat="server" ValidationGroup="Login"
                                Text="登&nbsp;&nbsp;录"
                                OnClick="btnLogin_Click" />
                        </li>
                    </ul>
                </div>
            </div>
        </div>
    </form>
    <script type="text/javascript">
   
    </script>
</body>
</html>
