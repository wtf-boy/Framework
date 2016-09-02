namespace WTF.Framework
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Web.UI;

    public static class ScriptHelper
    {
        public static void CloseForm()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("window.parent.closeSubMainPages();");
            builder.ToString().RegisterScript(RegisterType.StartupScript);
        }

        public static void DialogOpenerReloadScript(bool IsClosed, string message = "")
        {
            StringBuilder builder = new StringBuilder();
            if (message.IsNoNull())
            {
                builder.Append("alert(\"").Append(message).Append("\");");
            }
            builder.Append("var pWindow=window.dialogArguments;  if(pWindow != null){  pWindow.location.href =pWindow.location.href; }else{  window.opener.location.href = window.opener.location.href; };");
            if (IsClosed)
            {
                builder.Append("window.close();").Append("\r\n");
            }
            builder.ToString().RegisterScript(RegisterType.ClientBlock);
        }

        public static void DialogOpenerReloadScript(string theUrl, bool IsClosed, string message = "")
        {
            StringBuilder builder = new StringBuilder();
            if (message.IsNoNull())
            {
                builder.Append("alert(\"").Append(message).Append("\");");
            }
            builder.Append("var pWindow=window.dialogArguments;  if(pWindow != null){  pWindow.location.href = '" + theUrl.EncodeUrlQuery() + "'; }else{  window.opener.location.href = '" + theUrl.EncodeUrlQuery() + "'; };");
            if (IsClosed)
            {
                builder.Append("window.close();").Append("\r\n");
            }
            builder.ToString().RegisterScript(RegisterType.ClientBlock);
        }

        public static string FormateScript(this string scriptBody)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("<Script Language=JavaScript>").Append("\r\n");
            builder.Append(scriptBody).Append("\r\n");
            builder.Append("</Script>").Append("\r\n");
            return builder.ToString();
        }

        public static void MessageDialog(this string message)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("alert(\"").Append(message).Append("\")");
            builder.ToString().RegisterScript(RegisterType.StartupScript);
        }

        public static void MessageDialog(this string message, bool isCloseForm)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("alert(\"").Append(message).Append("\");");
            if (isCloseForm)
            {
                builder.Append("window.parent.closeSubMainPages();");
            }
            builder.ToString().RegisterScript(RegisterType.StartupScript);
        }

        public static void MessageDialog(this string message, string navigateUrl)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("alert(\"").Append(message).Append("\");");
            builder.Append("location.href='").Append(navigateUrl.EncodeUrlQuery()).Append("'");
            builder.ToString().RegisterScript(RegisterType.ClientBlock);
        }

        public static void OpenerReloadScript(string theUrl, bool IsClosed)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("var pWindow=window.dialogArguments;  if(pWindow != null){  pWindow.location.href = '" + theUrl.EncodeUrlQuery() + "'; }else{  window.opener.location.href = '" + theUrl.EncodeUrlQuery() + "'; };");
            if (IsClosed)
            {
                builder.Append("window.close();").Append("\r\n");
            }
            builder.ToString().RegisterScript(RegisterType.ClientBlock);
        }

        public static void OpenerReturnValueScript(string returnVale)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" window.returnValue = '" + returnVale + "';").Append("\r\n");
            builder.Append(" window.close();").Append("\r\n");
            builder.ToString().RegisterScript(RegisterType.ClientBlock);
        }

        public static void OpenModalDialogScript(string pUrl, int pWidth, int pHeight)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("var iNewX = (screen.availWidth - " + pWidth.ToString() + ")/2;").Append("\r\n");
            builder.Append("var iNewY = (screen.availHeight - " + pHeight.ToString() + ")/2;").Append("\r\n");
            builder.Append("var theDes = \"dialogWidth:" + pWidth.ToString() + "px;dialogHeight:" + pHeight.ToString() + "px;edge:sunken;help:no;status:no;scroll:no;\"").Append("\r\n");
            builder.Append("var ReturnValue = window.showModalDialog('" + pUrl.EncodeUrlQuery() + "',window,theDes);").Append("\r\n");
            builder.ToString().RegisterScript(RegisterType.ClientBlock);
        }

        public static void OpenModelessDialogScript(string pUrl, int pWidth, int pHeight)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("var iNewX = (screen.availWidth - " + pWidth.ToString() + ")/2;").Append("\r\n");
            builder.Append("var iNewY = (screen.availHeight - " + pHeight.ToString() + ")/2;").Append("\r\n");
            builder.Append("var theDes = \"dialogWidth:" + pWidth.ToString() + "px;dialogHeight:" + pHeight.ToString() + "px;edge:sunken;help:no;status:no;scroll:no;\"").Append("\r\n");
            builder.Append("var ReturnValue = window.showModelessDialog('" + pUrl.EncodeUrlQuery() + "',window,theDes);").Append("\r\n");
            builder.ToString().RegisterScript(RegisterType.ClientBlock);
        }

        public static void RefreshFrame(string frameName)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("parent.window.frames['").Append(frameName).Append("'].location=").Append("parent.window.frames['").Append(frameName).Append("'].location;");
            builder.ToString().RegisterScript(RegisterType.ClientBlock);
        }

        public static void RefreshFrame(string frameName, string frameNavigateUrl)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("parent.window.frames['").Append(frameName).Append("'].location='").Append(frameNavigateUrl.EncodeUrlQuery()).Append("'");
            builder.ToString().RegisterScript(RegisterType.ClientBlock);
        }

        public static void RefreshFrame(string frameName, string message, string navigateUrl)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("parent.window.frames['").Append(frameName).Append("'].location=").Append("parent.window.frames['").Append(frameName).Append("'].location;");
            builder.Append("alert(\"").Append(message).Append("\");");
            builder.Append("location.href='").Append(navigateUrl.EncodeUrlQuery()).Append("'");
            builder.ToString().RegisterScript(RegisterType.ClientBlock);
        }

        public static void RefreshFrame(string frameName, string frameNavigateUrl, string message, string navigateUrl)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("parent.window.frames['").Append(frameName).Append("'].location='").Append(frameNavigateUrl.EncodeUrlQuery()).Append("';");
            builder.Append(" alert(\"").Append(message).Append("\");");
            builder.Append("location.href='").Append(navigateUrl).Append("';");
            builder.ToString().RegisterScript(RegisterType.ClientBlock);
        }

        public static void RegisterScript(this string script, RegisterType registerTypeValue)
        {
            if (registerTypeValue == RegisterType.StartupScript)
            {
                ScriptManager.RegisterStartupScript((Control) SysVariable.CurrentHandler, SysVariable.CurrentHandler.GetType(), "script", script, true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock((Control) SysVariable.CurrentHandler, SysVariable.CurrentHandler.GetType(), "script", script, true);
            }
        }
    }
}

