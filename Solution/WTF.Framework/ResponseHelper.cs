namespace WTF.Framework
{
    using System;

    public static class ResponseHelper
    {
        public static void Redirect(string url)
        {
            SysVariable.CurrentContext.Response.Redirect(url.EncodeUrlQuery(), true);
        }
    }
}

