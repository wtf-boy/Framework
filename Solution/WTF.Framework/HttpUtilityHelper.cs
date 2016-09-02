namespace WTF.Framework
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Web;

    public static class HttpUtilityHelper
    {
        public static string DecodeHtml(this string text)
        {
            if (text.IsNull())
            {
                return text;
            }
            return HttpUtility.HtmlDecode(text);
        }

        public static string DecodeUrl(this string text)
        {
            if (text.IsNull())
            {
                return text;
            }
            return text.DecodeUrl(Encoding.UTF8);
        }

        public static string DecodeUrl(this string text, Encoding objEncoding)
        {
            if (text.IsNull())
            {
                return text;
            }
            return HttpUtility.UrlDecode(text, objEncoding);
        }

        public static string EncodeHtml(this string text)
        {
            if (text.IsNull())
            {
                return text;
            }
            return HttpUtility.HtmlEncode(text);
        }

        public static string EncodeUrl(this string text)
        {
            if (text.IsNull())
            {
                return text;
            }
            return text.EncodeUrl(Encoding.UTF8);
        }

        public static string EncodeUrl(this string text, Encoding objEncoding)
        {
            if (text.IsNull())
            {
                return text;
            }
            return HttpUtility.UrlEncode(text, objEncoding);
        }
    }
}

