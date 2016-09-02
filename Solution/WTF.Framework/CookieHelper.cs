namespace WTF.Framework
{
    using System;
    using System.Collections.Specialized;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Web;

    public static class CookieHelper
    {
        public static bool CheckCookieSignature(string key, string CookieValue)
        {
            return CheckCookieSignature(key, CookieValue, "");
        }

        public static bool CheckCookieSignature(string key, string CookieValue, string encryptKey)
        {
            string cookieValue = (key + "MD5").GetCookieValue();
            if (string.IsNullOrWhiteSpace(cookieValue))
            {
                return false;
            }
            return CookieValue.SignatureCheckMD5(cookieValue, encryptKey);
        }

        public static bool CookieRequestExists(this string cookieName)
        {
            return ((((HttpContext.Current != null) && (HttpContext.Current.Request != null)) && (HttpContext.Current.Request.Cookies != null)) && (HttpContext.Current.Request.Cookies[cookieName] != null));
        }

        public static bool CookieResponseExists(this string cookieName)
        {
            return ((((HttpContext.Current != null) && (HttpContext.Current.Response != null)) && (HttpContext.Current.Response.Cookies != null)) && (HttpContext.Current.Response.Cookies[cookieName] != null));
        }

        public static HttpCookie GetCookie(this string cookieName)
        {
            if (((HttpContext.Current != null) && (HttpContext.Current.Request != null)) && (HttpContext.Current.Request.Cookies != null))
            {
                return HttpContext.Current.Request.Cookies[cookieName];
            }
            return null;
        }

        public static bool GetCookieSignatureValue(this string cookieName, out int value)
        {
            return cookieName.GetCookieSignatureValue("", out value);
        }

        public static bool GetCookieSignatureValue(this string cookieName, out long value)
        {
            return cookieName.GetCookieSignatureValue("", out value);
        }

        public static bool GetCookieSignatureValue(this string cookieName, out string value)
        {
            return cookieName.GetCookieSignatureValue("", out value);
        }

        public static bool GetCookieSignatureValue(this string cookieName, string encryptKey, out int value)
        {
            value = 0;
            string cookieValue = cookieName.GetCookieValue();
            if (!string.IsNullOrWhiteSpace(cookieValue) && CheckCookieSignature(cookieName, cookieValue, encryptKey))
            {
                value = int.Parse(cookieValue);
                return true;
            }
            return false;
        }

        public static bool GetCookieSignatureValue(this string cookieName, string encryptKey, out long value)
        {
            value = 0L;
            string cookieValue = cookieName.GetCookieValue();
            if (!string.IsNullOrWhiteSpace(cookieValue) && CheckCookieSignature(cookieName, cookieValue, encryptKey))
            {
                value = long.Parse(cookieValue);
                return true;
            }
            return false;
        }

        public static bool GetCookieSignatureValue(this string cookieName, string encryptKey, out string value)
        {
            value = "";
            string cookieValue = cookieName.GetCookieValue();
            if (!string.IsNullOrWhiteSpace(cookieValue))
            {
                if (CheckCookieSignature(cookieName, cookieValue, encryptKey))
                {
                    value = cookieValue;
                    return true;
                }
                value = "";
            }
            return false;
        }

        public static string GetCookieValue(this string cookieName)
        {
            if (((HttpContext.Current != null) && (HttpContext.Current.Request != null)) && (HttpContext.Current.Request.Cookies != null))
            {
                HttpCookie cookie = HttpContext.Current.Request.Cookies[cookieName];
                if (cookie != null)
                {
                    return cookie.Value.DecodeUrl();
                }
                return null;
            }
            return null;
        }

        public static string GetCookieValue(this string cookieName, string keyName)
        {
            if (((HttpContext.Current != null) && (HttpContext.Current.Request != null)) && (HttpContext.Current.Request.Cookies != null))
            {
                HttpCookie cookie = HttpContext.Current.Request.Cookies[cookieName];
                if ((cookie != null) && cookie.Values.AllKeys.Contains<string>(keyName))
                {
                    return cookie[keyName].DecodeUrl();
                }
                return null;
            }
            return null;
        }

        public static NameValueCollection GetCookieValues(this string cookieName)
        {
            if (((HttpContext.Current != null) && (HttpContext.Current.Request != null)) && (HttpContext.Current.Request.Cookies != null))
            {
                HttpCookie cookie = HttpContext.Current.Request.Cookies[cookieName];
                if (cookie != null)
                {
                    NameValueCollection values = new NameValueCollection();
                    foreach (string str in cookie.Values.AllKeys)
                    {
                        values.Add(str, cookie.Values[str].DecodeUrl());
                    }
                    return values;
                }
                return null;
            }
            return null;
        }

        public static void RemoveCookie(this string cookieName, string domains = "")
        {
            cookieName.RemoveCookie(domains, "");
        }

        public static void RemoveCookie(this string cookieName, string domains, string path = "")
        {
            HttpCookie cookie = new HttpCookie(cookieName) {
                HttpOnly = true,
                Expires = DateTime.Now.AddYears(-1)
            };
            if (!string.IsNullOrWhiteSpace(path))
            {
                cookie.Path = path;
            }
            if (!string.IsNullOrWhiteSpace(domains))
            {
                cookie.Domain = domains;
            }
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        public static void RemoveCookieKey(this string cookieName, string keyName, DateTime expires, string domains = "")
        {
            cookieName.RemoveCookieKey(keyName, expires, domains, "");
        }

        public static void RemoveCookieKey(this string cookieName, string keyName, DateTime expires, string domains, string path = "")
        {
            if (cookieName.CookieRequestExists())
            {
                HttpCookie cookie = HttpContext.Current.Request.Cookies[cookieName];
                cookie.HttpOnly = true;
                cookie.Values.Remove(keyName);
                if (!string.IsNullOrWhiteSpace(path))
                {
                    cookie.Path = path;
                }
                if (!string.IsNullOrWhiteSpace(domains))
                {
                    cookie.Domain = domains;
                }
                if (!expires.Equals(DateTime.MinValue))
                {
                    cookie.Expires = expires;
                }
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
        }

        public static void RemoveSignatureCookie(this string cookieName, string domains = "")
        {
            cookieName.RemoveCookie(domains, "");
            (cookieName + "MD5").RemoveCookie(domains, "");
        }

        private static string SelectDomain(string domains)
        {
            bool flag = false;
            if (domains.Trim().Length == 0)
            {
                return "";
            }
            string str = HttpContext.Current.Request.ServerVariables["SERVER_NAME"].ToString();
            if (!str.Contains("."))
            {
                flag = true;
            }
            string str2 = "";
            string[] strArray = domains.Split(new char[] { ';' });
            for (int i = 0; i < strArray.Length; i++)
            {
                if (str.Contains(strArray[i].Trim()))
                {
                    if (flag)
                    {
                        str2 = "";
                    }
                    else
                    {
                        str2 = strArray[i].Trim();
                    }
                    return str2;
                }
            }
            return str2;
        }

        public static void SetCookieSignatureValue(this string value, string cookieName, string domains = "")
        {
            value.SetCookieSignatureValue(cookieName, DateTime.MinValue, domains);
        }

        public static void SetCookieSignatureValue(this string value, string cookieName, DateTime expires, string domains = "")
        {
            value.SetCookieSignatureValue(cookieName, "", expires, domains);
        }

        public static void SetCookieSignatureValue(this string value, string cookieName, string encryptKey, string domains = "")
        {
            value.SetCookieSignatureValue(cookieName, encryptKey, DateTime.MinValue, domains);
        }

        public static void SetCookieSignatureValue(this string value, string cookieName, string encryptKey, DateTime expires, string domains = "")
        {
            value.SetCookieValue(cookieName, expires, domains, "");
            value.SignatureMD5(encryptKey).SetCookieValue(cookieName + "MD5", expires, domains, "");
        }

        public static void SetCookieValue(this NameValueCollection KeyValue, string cookieName, string domains = "")
        {
            KeyValue.SetCookieValue(cookieName, DateTime.MinValue, domains);
        }

        public static void SetCookieValue(this string value, string cookieName, string domains = "")
        {
            value.SetCookieValue(cookieName, DateTime.MinValue, domains);
        }

        public static void SetCookieValue(this NameValueCollection KeyValue, string cookieName, DateTime expires, string domains = "")
        {
            KeyValue.SetCookieValue(cookieName, expires, domains, "");
        }

        public static void SetCookieValue(this string value, string cookieName, DateTime expires, string domains = "")
        {
            value.SetCookieValue(cookieName, expires, domains, "");
        }

        public static void SetCookieValue(this NameValueCollection KeyValue, string cookieName, DateTime expires, string domains, string path = "")
        {
            HttpCookie cookie = new HttpCookie(cookieName);
            foreach (string str in KeyValue.AllKeys)
            {
                cookie[str] = KeyValue[str].EncodeUrl();
            }
            if (!string.IsNullOrWhiteSpace(path))
            {
                cookie.Path = path;
            }
            if (!string.IsNullOrWhiteSpace(domains))
            {
                cookie.Domain = domains;
            }
            if (!expires.Equals(DateTime.MinValue))
            {
                cookie.Expires = expires;
            }
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        public static void SetCookieValue(this string value, string cookieName, DateTime expires, string domains, string path = "")
        {
            HttpCookie cookie = new HttpCookie(cookieName) {
                Value = value.EncodeUrl()
            };
            if (!string.IsNullOrWhiteSpace(path))
            {
                cookie.Path = path;
            }
            if (!string.IsNullOrWhiteSpace(domains))
            {
                cookie.Domain = domains;
            }
            if (!expires.Equals(DateTime.MinValue))
            {
                cookie.Expires = expires;
            }
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        public static void UpdateCookieKeyValue(this string cookieName, string keyName, string keyValue, DateTime expires, string domains = "")
        {
            cookieName.UpdateCookieKeyValue(keyName, keyValue, expires, domains, "");
        }

        public static void UpdateCookieKeyValue(this string cookieName, string keyName, string keyValue, DateTime expires, string domains, string path = "")
        {
            if (((HttpContext.Current != null) && (HttpContext.Current.Request != null)) && (HttpContext.Current.Request.Cookies != null))
            {
                HttpCookie cookie = HttpContext.Current.Request.Cookies[cookieName];
                if (cookie != null)
                {
                    if (!string.IsNullOrWhiteSpace(path))
                    {
                        cookie.Path = path;
                    }
                    if (!string.IsNullOrWhiteSpace(domains))
                    {
                        cookie.Domain = domains;
                    }
                    if (!expires.Equals(DateTime.MinValue))
                    {
                        cookie.Expires = expires;
                    }
                    cookie[keyName] = keyValue.EncodeUrl();
                    HttpContext.Current.Response.Cookies.Add(cookie);
                }
            }
        }

        public static void UpdateCookieValue(this string cookieName, string value, DateTime expires, string domains = "")
        {
            cookieName.UpdateCookieValue(value, expires, domains, "");
        }

        public static void UpdateCookieValue(this string cookieName, string value, DateTime expires, string domains, string path = "")
        {
            if (((HttpContext.Current != null) && (HttpContext.Current.Request != null)) && (HttpContext.Current.Request.Cookies != null))
            {
                HttpCookie cookie = HttpContext.Current.Request.Cookies[cookieName];
                if (cookie != null)
                {
                    cookie.Value = value.EncodeUrl();
                    if (!string.IsNullOrWhiteSpace(path))
                    {
                        cookie.Path = path;
                    }
                    if (!string.IsNullOrWhiteSpace(domains))
                    {
                        cookie.Domain = domains;
                    }
                    if (!expires.Equals(DateTime.MinValue))
                    {
                        cookie.Expires = expires;
                    }
                    HttpContext.Current.Response.Cookies.Add(cookie);
                }
            }
        }
    }
}

