<%@ WebHandler Language="C#" Class="WindowHeight" %>

using System;
using System.Web;
using WTF.Framework;
public class WindowHeight : IHttpHandler
{
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        string AutoPageSize = context.Request["AutoPageSize"];
        if (!string.IsNullOrEmpty(AutoPageSize))
        {
            CookieHelper.SetCookieValue(AutoPageSize, "AutoPageSize", DateTime.MaxValue);

        }
        context.Response.Write("");
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}