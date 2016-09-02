namespace WTF.Framework
{
    using System;
    using System.Net;
    using System.Net.Sockets;
    using System.Runtime.CompilerServices;
    using System.Web;
    using System.Web.UI;

    public static class SysVariable
    {
        private static string _ApplicationPath = "~";

        public static string AbsoluteToResolveApplicationPath(this string absoluteApplicationPath)
        {
            if (ApplicationPath == "")
            {
                return absoluteApplicationPath.Substring(1);
            }
            return absoluteApplicationPath.Substring(ApplicationPath.Length + 1);
        }

        public static IPAddress GetLocalIp()
        {
            IPHostEntry entry = new IPHostEntry {
                AddressList = new IPAddress[1]
            };
            int index = 0;
            for (index = 0; index < Dns.GetHostEntry(Dns.GetHostName()).AddressList.Length; index++)
            {
                if (Dns.GetHostEntry(Dns.GetHostName()).AddressList[index].AddressFamily == AddressFamily.InterNetwork)
                {
                    entry.AddressList[0] = Dns.GetHostEntry(Dns.GetHostName()).AddressList[index];
                    break;
                }
            }
            return entry.AddressList[0];
        }

        public static string ResolveApplicationPath(this string baseApplicationPath)
        {
            return (ApplicationPath + "/" + baseApplicationPath);
        }

        public static string ResolveToAbsoluteApplicationPath(this string resolveApplicationPath)
        {
            if (ApplicationPath == "")
            {
                return ("/" + resolveApplicationPath);
            }
            return (ApplicationPath + "/" + resolveApplicationPath);
        }

        public static bool AccessIsMobile
        {
            get
            {
                string[] strArray = new string[] { 
                    "iphone", "android", "phone", "mobile", "wap", "netfront", "java", "opera mobi", "opera mini", "ucweb", "windows ce", "symbian", "series", "webos", "sony", "blackberry", 
                    "dopod", "nokia", "samsung", "palmsource", "xda", "pieplus", "meizu", "midp", "cldc", "motorola", "foma", "docomo", "up.browser", "up.link", "blazer", "helio", 
                    "hosin", "huawei", "novarra", "coolpad", "webos", "techfaith", "palmsource", "alcatel", "amoi", "ktouch", "nexian", "ericsson", "philips", "sagem", "wellcom", "bunjalloo", 
                    "maui", "smartphone", "iemobile", "spice", "bird", "zte-", "longcos", "pantech", "gionee", "portalmmm", "jig browser", "hiptop", "benq", "haier", "^lct", "320x320", 
                    "240x320", "176x220", "w3c ", "acs-", "alav", "alca", "amoi", "audi", "avan", "benq", "bird", "blac", "blaz", "brew", "cell", "cldc", 
                    "cmd-", "dang", "doco", "eric", "hipt", "inno", "ipaq", "java", "jigs", "kddi", "keji", "leno", "lg-c", "lg-d", "lg-g", "lge-", 
                    "maui", "maxo", "midp", "mits", "mmef", "mobi", "mot-", "moto", "mwbp", "nec-", "newt", "noki", "oper", "palm", "pana", "pant", 
                    "phil", "play", "port", "prox", "qwap", "sage", "sams", "sany", "sch-", "sec-", "send", "seri", "sgh-", "shar", "sie-", "siem", 
                    "smal", "smar", "sony", "sph-", "symb", "t-mo", "teli", "tim-", "tosh", "tsm-", "upg1", "upsi", "vk-v", "voda", "wap-", "wapa", 
                    "wapi", "wapp", "wapr", "webc", "winw", "winw", "xda", "xda-", "Googlebot-Mobile"
                 };
                bool flag = false;
                string str = CurrentContext.Request.UserAgent.ToLower();
                if (!string.IsNullOrWhiteSpace(str))
                {
                    for (int i = 0; i < strArray.Length; i++)
                    {
                        if (str.IndexOf(strArray[i]) >= 0)
                        {
                            flag = true;
                            break;
                        }
                    }
                }
                return flag;
            }
        }

        public static string ApplicationPath
        {
            get
            {
                if (CurrentContext != null)
                {
                    _ApplicationPath = CurrentContext.Request.ApplicationPath.EndsWith("/") ? "" : CurrentContext.Request.ApplicationPath;
                }
                return _ApplicationPath;
            }
        }

        public static string ApplicationPathMap
        {
            get
            {
                if (CurrentContext == null)
                {
                    return "~";
                }
                if (ApplicationPath != "")
                {
                    return CurrentContext.Server.MapPath(ApplicationPath);
                }
                return CurrentContext.Server.MapPath("/");
            }
        }

        public static HttpContext CurrentContext
        {
            get
            {
                return HttpContext.Current;
            }
        }

        public static IHttpHandler CurrentHandler
        {
            get
            {
                return HttpContext.Current.Handler;
            }
        }

        public static Page CurrentPage
        {
            get
            {
                return (Page) HttpContext.Current.Handler;
            }
        }

        public static string GetServerOS
        {
            get
            {
                return Environment.OSVersion.VersionString;
            }
        }
    }
}

