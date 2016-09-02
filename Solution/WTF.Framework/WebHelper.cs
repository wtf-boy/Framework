namespace WTF.Framework
{
    using System;
    using System.IO;
    using System.Net;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Web;
    using System.Web.UI;

    public static class WebHelper
    {
        public static string ConvertUrl(HttpContext oContext, string sBaseUrl, string sUrl)
        {
            string applicationPath;
            if (((sUrl == null) || (sUrl == string.Empty)) || IsUrlAbsolute(sUrl))
            {
                return sUrl;
            }
            if ((sUrl.StartsWith("~") && (oContext != null)) && (oContext.Request != null))
            {
                applicationPath = oContext.Request.ApplicationPath;
                if (applicationPath.EndsWith("/"))
                {
                    applicationPath = applicationPath.Substring(0, applicationPath.Length - 1);
                }
                return sUrl.Replace("~", applicationPath);
            }
            if (!(sBaseUrl != string.Empty))
            {
                return sUrl;
            }
            if ((sBaseUrl.StartsWith("~") && (oContext != null)) && (oContext.Request != null))
            {
                applicationPath = oContext.Request.ApplicationPath;
                if (applicationPath.EndsWith("/"))
                {
                    applicationPath = applicationPath.Substring(0, applicationPath.Length - 1);
                }
                sBaseUrl = sBaseUrl.Replace("~", applicationPath);
            }
            if (sBaseUrl.EndsWith("/"))
            {
                sBaseUrl = sBaseUrl.Substring(0, sBaseUrl.Length - 1);
            }
            if (sUrl.StartsWith("/"))
            {
                sUrl = sUrl.Substring(1, sUrl.Length - 1);
            }
            return (sBaseUrl + "/" + sUrl);
        }

        public static string GetChildControlPrefix(Control control)
        {
            if (control.NamingContainer.Parent == null)
            {
                return control.ID;
            }
            return string.Format("{0}_{1}", control.NamingContainer.ClientID, control.ID);
        }

        public static bool IsIP(string ip)
        {
            return Regex.IsMatch(ip, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");
        }

        public static bool IsUrlAbsolute(string url)
        {
            if (url != null)
            {
                string[] strArray = new string[] { "about:", "file:///", "ftp://", "gopher://", "http://", "https://", "javascript:", "mailto:", "news:", "res://", "telnet://", "view-source:" };
                foreach (string str in strArray)
                {
                    if (url.StartsWith(str))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static string LoadPageContent(string url)
        {
            byte[] bytes = new WebClient { Credentials = CredentialCache.DefaultCredentials }.DownloadData(url);
            return Encoding.GetEncoding("gb2312").GetString(bytes);
        }

        public static string LoadURLString(string url)
        {
            HttpWebRequest request = (HttpWebRequest) WebRequest.Create(url);
            Stream responseStream = ((HttpWebResponse) request.GetResponse()).GetResponseStream();
            string str = "";
            StreamReader reader = new StreamReader(responseStream, Encoding.GetEncoding("gb2312"));
            char[] buffer = new char[0x100];
            int length = reader.Read(buffer, 0, 0x100);
            int num2 = 0;
            while (length > 0)
            {
                num2 += Encoding.UTF8.GetByteCount(buffer, 0, 0x100);
                string str2 = new string(buffer, 0, length);
                str = str + str2;
                length = reader.Read(buffer, 0, 0x100);
            }
            return str;
        }

        public static string ReplaceSpecialChars(this string input)
        {
            input = input.Replace(" ", "_x0020_").Replace("%", "_x0025_").Replace("#", "_x0023_").Replace("&", "_x0026_").Replace("/", "_x002F_");
            return input;
        }

        public static void SetAttribute(IAttributeAccessor iaa, string key, string value, AttributeValuePosition csp)
        {
            SetAttribute(iaa, key, value, csp, ';');
        }

        public static void SetAttribute(IAttributeAccessor iaa, string key, string value, AttributeValuePosition csp, char separator)
        {
            string attribute = iaa.GetAttribute(key);
            if (string.IsNullOrEmpty(attribute))
            {
                iaa.SetAttribute(key, value);
            }
            else if (csp == AttributeValuePosition.First)
            {
                attribute = attribute.TrimStart(new char[] { separator });
                iaa.SetAttribute(key, value + separator + attribute);
            }
            else if (csp == AttributeValuePosition.Last)
            {
                iaa.SetAttribute(key, attribute.TrimEnd(new char[] { separator }) + separator + value);
            }
        }

        public static bool ValidateUrl(string url)
        {
            Uri requestUri = new Uri(url);
            try
            {
                HttpWebResponse response = (HttpWebResponse) WebRequest.Create(requestUri).GetResponse();
                return (response.StatusCode == HttpStatusCode.OK);
            }
            catch
            {
                return false;
            }
        }
    }
}

