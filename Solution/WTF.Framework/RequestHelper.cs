namespace WTF.Framework
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Configuration;
    using System.IO;
    using System.Net;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Web;

    public static class RequestHelper
    {
        private const int _Timeout = 0xea60;
        private const string _UserAgent = "Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/34.0.1847.116 Safari/537.36";

        public static string AddUrlQueryString(this string url, string addParamter, object value)
        {
            if ((url.IndexOf('?') != -1) && (url.IndexOf(addParamter) != -1))
            {
                string str = url.Split(new char[] { '?' })[0];
                string str2 = url.Split(new char[] { '?' })[1];
                string str3 = "?";
                foreach (string str4 in str2.Split(new char[] { '&' }))
                {
                    if (str4.IndexOf(addParamter) == -1)
                    {
                        str3 = str3 + str4 + "&";
                    }
                }
                str3 = str3.TrimEnd(new char[] { '&' });
                url = str + str3;
            }
            string str5 = string.Format("{0}={1}", addParamter, value.ToString());
            if (url.IndexOf('?') != -1)
            {
                url = url + "&" + str5;
                return url;
            }
            url = url + "?" + str5;
            return url;
        }

        public static bool CheckExteriorVote()
        {
            string str = "";
            string str2 = "";
            str = HttpContext.Current.Request.ServerVariables["HTTP_REFERER"];
            str2 = HttpContext.Current.Request.ServerVariables["SERVER_NAME"];
            if ((str == null) || (str.Substring(7, str2.Length) != str2))
            {
                return false;
            }
            return true;
        }

        public static bool CheckUrlReferrer(this string hostPattern)
        {
            if (string.IsNullOrWhiteSpace(hostPattern))
            {
                throw new ArgumentNullException("hostPattern参数不能为空");
            }
            string str = (CurrentContext.Request.UrlReferrer != null) ? CurrentContext.Request.UrlReferrer.Host.ToLower() : "";
            if (!(!string.IsNullOrWhiteSpace(str) && str.IsMatch(hostPattern)))
            {
                return false;
            }
            return true;
        }

        public static string ClearContentSpm(this string content)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                return "";
            }
            content = content.Replace("&spm=[^=&\"']+", "", RegexOptions.IgnoreCase);
            content = content.Replace("\\?spm=[^=&\"']+", "", RegexOptions.IgnoreCase);
            return content;
        }

        public static string ClearQuerySpm(this string url)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                return "";
            }
            url = url.Replace("&spm=[^=&]+$", "", RegexOptions.IgnoreCase);
            url = url.Replace(@"\?spm=[^=&]+$", "", RegexOptions.IgnoreCase);
            url = url.Replace("spm=[^=&]+&", "", RegexOptions.IgnoreCase);
            return url;
        }

        public static HttpWebResponse CreateHttpPost(this HttpWebRequest wreq, params HttpPostItem[] items)
        {
            if (wreq == null)
            {
                throw new Exception("HttpWebRequest 尚未初始化。");
            }
            if ((items == null) || (items.Length == 0))
            {
                throw new Exception("No HttpPostItems");
            }
            StringBuilder builder = new StringBuilder();
            foreach (HttpPostItem item in items)
            {
                builder.Append("&" + item.ToString());
            }
            byte[] bytes = Encoding.UTF8.GetBytes(builder.ToString().Substring(1));
            wreq.Method = "POST";
            wreq.ContentLength = bytes.Length;
            if (string.IsNullOrWhiteSpace(wreq.ContentType))
            {
                wreq.ContentType = "application/x-www-form-urlencoded";
            }
            wreq.KeepAlive = false;
            using (Stream stream = wreq.GetRequestStream())
            {
                stream.Write(bytes, 0, bytes.Length);
                stream.Close();
                return (wreq.GetResponse() as HttpWebResponse);
            }
        }

        public static HttpWebResponse CreateHttpPost(this HttpWebRequest wreq, IDictionary<string, ICollection<string>> parameters)
        {
            if (wreq == null)
            {
                throw new Exception("HttpWebRequest 尚未初始化。");
            }
            byte[] bytes = Encoding.UTF8.GetBytes(parameters.BuildQueryParams());
            wreq.Method = "POST";
            wreq.ContentLength = bytes.Length;
            if (string.IsNullOrWhiteSpace(wreq.ContentType))
            {
                wreq.ContentType = "application/x-www-form-urlencoded";
            }
            wreq.KeepAlive = false;
            using (Stream stream = wreq.GetRequestStream())
            {
                stream.Write(bytes, 0, bytes.Length);
                stream.Close();
                return (wreq.GetResponse() as HttpWebResponse);
            }
        }

        public static HttpWebResponse CreateHttpPost(this HttpWebRequest wreq, byte[] data)
        {
            StringBuilder builder = new StringBuilder();
            wreq.Method = "POST";
            wreq.ContentLength = data.Length;
            if (string.IsNullOrWhiteSpace(wreq.ContentType))
            {
                wreq.ContentType = "multipart/form-data";
            }
            wreq.KeepAlive = false;
            using (Stream stream = wreq.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
                stream.Close();
                return (wreq.GetResponse() as HttpWebResponse);
            }
        }

        public static HttpWebResponse CreateHttpPost(this string url, params HttpPostItem[] items)
        {
            return url.CreateRequest().CreateHttpPost(items);
        }

        public static HttpWebResponse CreateHttpPost(this string url, IDictionary<string, ICollection<string>> parameters)
        {
            return url.CreateRequest().CreateHttpPost(parameters);
        }

        public static HttpWebResponse CreateHttpPost(this string url, byte[] data)
        {
            return url.CreateRequest().CreateHttpPost(data);
        }

        public static HttpWebRequest CreateRequest(this string url)
        {
            return url.CreateRequest(null, "Get");
        }

        public static HttpWebRequest CreateRequest(this string url, NameValueCollection Headers, string Method = "Get")
        {
            return url.CreateRequest(null, Headers, Method);
        }

        public static HttpWebRequest CreateRequest(this string url, string Accept, NameValueCollection Headers, string Method = "Get")
        {
            return url.CreateRequest(Accept, Headers, "Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/34.0.1847.116 Safari/537.36", null, 0xea60, Method);
        }

        public static HttpWebRequest CreateRequest(this string url, string userAgent, string referer, int timeout, string Method = "Get")
        {
            return url.CreateRequest("", null, userAgent, referer, 0xea60, Method);
        }

        public static HttpWebRequest CreateRequest(this string url, string Accept, NameValueCollection Headers, string userAgent, string referer, int timeout, string Method = "Get")
        {
            HttpWebRequest request2 = WebRequest.Create(url) as HttpWebRequest;
            if (request2 != null)
            {
                if (!string.IsNullOrWhiteSpace(userAgent))
                {
                    request2.UserAgent = userAgent;
                }
                else
                {
                    request2.UserAgent = "Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/34.0.1847.116 Safari/537.36";
                }
                if (!string.IsNullOrWhiteSpace(referer))
                {
                    request2.Referer = referer;
                }
                else
                {
                    referer = url.Replace("^http://", "", RegexOptions.IgnoreCase);
                    if (referer.IndexOf('/') > 0)
                    {
                        referer = referer.Substring(0, referer.IndexOf('/') + 1);
                    }
                    request2.Referer = "http://" + referer;
                }
                if (!string.IsNullOrWhiteSpace(Method))
                {
                    request2.Method = Method;
                }
                if (!string.IsNullOrWhiteSpace(Accept))
                {
                    request2.Accept = Accept;
                }
                if (timeout > 0)
                {
                    request2.Timeout = timeout;
                }
                if (Headers != null)
                {
                    request2.Headers.Add(Headers);
                }
            }
            return request2;
        }

        public static string EncodeUrlQuery(this string url)
        {
            string[] strArray = url.Split(new char[] { '?' });
            if (strArray.Length > 1)
            {
                QueryStringHelper helper = new QueryStringHelper(strArray[1]);
                return (strArray[0] + helper.CreateEncodeUrlQueryString());
            }
            return url;
        }

        public static string EncryptModuleQuery(this string url)
        {
            return ((ModulePage) SysVariable.CurrentPage).EncryptModuleQuery(url);
        }

        public static string EncryptModuleUrlQuery(this string url)
        {
            return url.EncryptModuleQuery();
        }

        public static string EncryptQuery(this string url)
        {
            return ((ModulePage) SysVariable.CurrentPage).EncryptQuery(url);
        }

        public static decimal GetDecimal(string key)
        {
            return GetDecimal(key, ObjectHelper.NullDecimal);
        }

        public static decimal GetDecimal(string key, decimal defaultValue)
        {
            return GetString(key, defaultValue.ToString(), false).ConvertDecimal();
        }

        public static Guid GetDecodeEnhanced64Guid(string key)
        {
            return GetDecodeEnhanced64Guid(key, ObjectHelper.NullGuid);
        }

        public static Guid GetDecodeEnhanced64Guid(string key, Guid defaultValue)
        {
            return GetDecodeEnhanced64String(key, defaultValue.ToString()).ConvertGuid();
        }

        public static int GetDecodeEnhanced64Int(string key)
        {
            return GetDecodeEnhanced64Int(key, ObjectHelper.NullInt);
        }

        public static int GetDecodeEnhanced64Int(string key, int defaultValue)
        {
            return GetDecodeEnhanced64String(key, defaultValue.ToString()).ConvertInt();
        }

        public static long GetDecodeEnhanced64Long(string key)
        {
            return GetDecodeEnhanced64Long(key, ObjectHelper.NullLong);
        }

        public static long GetDecodeEnhanced64Long(string key, long defaultValue)
        {
            return GetDecodeEnhanced64String(key, defaultValue.ToString()).ConvertLong();
        }

        public static string GetDecodeEnhanced64String(string key)
        {
            return GetDecodeEnhanced64String(key, string.Empty);
        }

        public static string GetDecodeEnhanced64String(string key, string defaultValue)
        {
            string str = SysVariable.CurrentContext.Request[key];
            return (str.IsNoNull() ? str.DecodeEnhancedBase64() : defaultValue);
        }

        public static double GetDouble(string key)
        {
            return GetDouble(key, ObjectHelper.NullDouble);
        }

        public static double GetDouble(string key, double defaultValue)
        {
            return GetString(key, defaultValue.ToString(), false).ConvertDouble();
        }

        public static float GetFloat(string key)
        {
            return GetFloat(key, ObjectHelper.NullFloat);
        }

        public static float GetFloat(string key, float defaultValue)
        {
            return GetString(key, defaultValue.ToString(), false).ConvertFloat();
        }

        public static Guid GetGuid(string key)
        {
            return GetGuid(key, ObjectHelper.NullGuid);
        }

        public static Guid GetGuid(string key, Guid defaultValue)
        {
            return GetString(key, defaultValue.ToString(), false).ConvertGuid();
        }

        public static decimal GetHeadersDecimal(this string key)
        {
            return key.GetHeadersDecimal(ObjectHelper.NullDecimal);
        }

        public static decimal GetHeadersDecimal(this string key, decimal defaultValue)
        {
            return key.GetHeadersString(defaultValue.ToString()).ConvertDecimal();
        }

        public static double GetHeadersDouble(this string key)
        {
            return key.GetHeadersDouble(ObjectHelper.NullDouble);
        }

        public static double GetHeadersDouble(this string key, double defaultValue)
        {
            return key.GetHeadersString(defaultValue.ToString()).ConvertDouble();
        }

        public static double GetHeadersFloat(this string key)
        {
            return (double) key.GetHeadersFloat(ObjectHelper.NullFloat);
        }

        public static float GetHeadersFloat(this string key, float defaultValue)
        {
            return key.GetHeadersString(defaultValue.ToString()).ConvertFloat();
        }

        public static int GetHeadersInt(this string key)
        {
            return key.GetHeadersInt(ObjectHelper.NullInt).ConvertInt();
        }

        public static int GetHeadersInt(this string key, int defaultValue)
        {
            return key.GetHeadersString(defaultValue.ToString()).ConvertInt();
        }

        public static long GetHeadersLong(this string key)
        {
            return key.GetHeadersLong(ObjectHelper.NullLong);
        }

        public static long GetHeadersLong(this string key, long defaultValue)
        {
            return key.GetHeadersString(defaultValue.ToString()).ConvertLong();
        }

        public static string GetHeadersString(this string key, string defaultValue = "")
        {
            string str = SysVariable.CurrentContext.Request.Headers[key];
            return (str.IsNoNull() ? str : defaultValue);
        }

        public static int GetInt(string key)
        {
            return GetInt(key, ObjectHelper.NullInt);
        }

        public static int GetInt(string key, int defaultValue)
        {
            return GetString(key, defaultValue.ToString(), false).ConvertInt();
        }

        public static long GetLong(string key)
        {
            return GetLong(key, ObjectHelper.NullLong);
        }

        public static long GetLong(string key, long defaultValue)
        {
            return GetString(key, defaultValue.ToString(), false).ConvertLong();
        }

        public static T GetPageInterfaceSignature<T>(this string url, string encryptKey = "")
        {
            return url.GetPageInterfaceSignature<T>(Encoding.UTF8, encryptKey);
        }

        public static T GetPageInterfaceSignature<T>(this string url, Encoding encoding, string encryptKey = "")
        {
            return url.SignatureInterfaceMD5(encryptKey).GetPageTextJson<T>(encoding);
        }

        public static T GetPagePostInterfaceSignature<T>(this string url, byte[] data, bool IsCompress = true, string encryptKey = "")
        {
            return url.GetPagePostInterfaceSignature<T>(data, Encoding.UTF8, IsCompress, encryptKey);
        }

        public static T GetPagePostInterfaceSignature<T>(this string url, object data, bool IsCompress = true, string encryptKey = "")
        {
            return url.GetPagePostInterfaceSignature<T>(data, Encoding.UTF8, IsCompress, encryptKey);
        }

        public static T GetPagePostInterfaceSignature<T>(this string url, byte[] data, Encoding encoding, bool IsCompress = true, string encryptKey = "")
        {
            string str = data.MD5Encrypt();
            if (url.IndexOf("?") >= 0)
            {
                url = url + "&ContentMD5=" + str;
            }
            else
            {
                url = url + "?ContentMD5=" + str;
            }
            return url.SignatureInterfaceMD5(encryptKey).GetPageTextPostJson<T>(data, encoding, IsCompress);
        }

        public static T GetPagePostInterfaceSignature<T>(this string url, object data, Encoding encoding, bool IsCompress = true, string encryptKey = "")
        {
            byte[] bytes = Encoding.UTF8.GetBytes(data.JsonJsSerialize());
            return url.GetPagePostInterfaceSignature<T>(bytes, encoding, IsCompress, encryptKey);
        }

        public static string GetPageProxyText(this string url, string Seven_ProxyHost = "")
        {
            return url.CreateRequest().SetWebProxy(Seven_ProxyHost).GetPageText();
        }

        public static string GetPageProxyText(this string url, string host, int port)
        {
            return url.CreateRequest().SetWebProxy(host, port).GetPageText();
        }

        public static string GetPageProxyText(this string url, Encoding encoding, string Seven_ProxyHost = "")
        {
            return url.CreateRequest().SetWebProxy(Seven_ProxyHost).GetPageText(encoding);
        }

        public static string GetPageProxyText(this string url, Encoding encoding, string host, int port)
        {
            return url.CreateRequest().SetWebProxy(host, port).GetPageText(encoding);
        }

        public static Stream GetPageStream(this HttpWebRequest httpWebRequest, byte[] data)
        {
            return httpWebRequest.CreateHttpPost(data).GetResponseStream();
        }

        public static Stream GetPageStream(this string url, byte[] data)
        {
            return url.CreateRequest().GetPageStream(data);
        }

        public static string GetPageText(this HttpWebRequest httpWebRequest)
        {
            return httpWebRequest.GetWebResponse().GetPageText(Encoding.UTF8);
        }

        public static string GetPageText(this WebRequest httpWebRequest)
        {
            HttpWebRequest request = httpWebRequest as HttpWebRequest;
            return request.GetPageText(Encoding.UTF8);
        }

        public static string GetPageText(this string url)
        {
            return url.GetPageText(Encoding.UTF8);
        }

        public static string GetPageText(this HttpWebRequest httpWebRequest, Encoding encoding)
        {
            return httpWebRequest.GetWebResponse().GetPageText(encoding);
        }

        public static string GetPageText(this HttpWebResponse response, Encoding encoding)
        {
            string str;
            using (Stream stream = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream, encoding))
                {
                    str = reader.ReadToEnd();
                }
            }
            return str;
        }

        public static string GetPageText(this string url, Encoding encoding)
        {
            return url.CreateRequest().GetPageText(encoding);
        }

        public static T GetPageTextJson<T>(this string url)
        {
            return url.GetPageTextJson<T>(Encoding.UTF8);
        }

        public static T GetPageTextJson<T>(this string url, Encoding encoding)
        {
            return url.GetPageText(encoding).JsonJsDeserialize<T>();
        }

        public static string GetPageTextPost(this string url, byte[] data, bool IsCompress = true)
        {
            return url.GetPageTextPost(data, Encoding.UTF8, IsCompress);
        }

        public static string GetPageTextPost(this string url, byte[] data, Encoding encoding, bool IsCompress = true)
        {
            if (IsCompress)
            {
                data = data.CompressGZip();
            }
            return url.CreateHttpPost(data).GetPageText(encoding);
        }

        public static T GetPageTextPostJson<T>(this string url, byte[] data, bool IsCompress = true)
        {
            return url.GetPageTextPostJson<T>(data, Encoding.UTF8, IsCompress);
        }

        public static T GetPageTextPostJson<T>(this string url, object data, bool IsCompress = true)
        {
            return url.GetPageTextPostJson<T>(data, Encoding.UTF8, IsCompress);
        }

        public static T GetPageTextPostJson<T>(this string url, byte[] data, Encoding encoding, bool IsCompress = true)
        {
            return url.GetPageTextPost(data, encoding, IsCompress).JsonJsDeserialize<T>();
        }

        public static T GetPageTextPostJson<T>(this string url, object data, Encoding encoding, bool IsCompress = true)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(data.JsonJsSerialize());
            return url.GetPageTextPostJson<T>(bytes, encoding, IsCompress);
        }

        public static string GetRealIp()
        {
            string str = "";
            if ((HttpContext.Current == null) || (HttpContext.Current.Request == null))
            {
                return str;
            }
            if ((HttpContext.Current.Request.ServerVariables != null) && (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null))
            {
                str = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
                if (str.Length >= 10)
                {
                    string[] strArray = str.Split(new char[] { ',' });
                    if (strArray.Length > 0)
                    {
                        str = strArray[0].ToString().Trim();
                    }
                }
                if ((str.Length > 0) && (str.Length <= 15))
                {
                    return str;
                }
            }
            if ((HttpContext.Current.Request.Headers != null) && (HttpContext.Current.Request.Headers["X-Real-IP"] != null))
            {
                return HttpContext.Current.Request.Headers["X-Real-IP"].ToString();
            }
            return HttpContext.Current.Request.UserHostAddress;
        }

        public static byte[] GetResponseByte(this HttpWebRequest httpWebRequest)
        {
            string contentType = "";
            return httpWebRequest.GetResponseByte(out contentType);
        }

        public static byte[] GetResponseByte(this HttpWebResponse httpWebResponse)
        {
            byte[] buffer = new byte[httpWebResponse.ContentLength];
            return httpWebResponse.GetResponseStream().StreamResponseToBytes();
        }

        public static byte[] GetResponseByte(this string url)
        {
            string contentType = "";
            return url.GetResponseByte(out contentType);
        }

        public static byte[] GetResponseByte(this HttpWebRequest httpWebRequest, out string ContentType)
        {
            HttpWebResponse webResponse = httpWebRequest.GetWebResponse();
            ContentType = webResponse.ContentType;
            return webResponse.GetResponseStream().StreamResponseToBytes();
        }

        public static byte[] GetResponseByte(this string url, out string ContentType)
        {
            return url.CreateRequest().GetResponseByte(out ContentType);
        }

        public static string GetString(string key)
        {
            return GetString(key, string.Empty, false);
        }

        public static string GetString(string key, string defaultValue, bool isFilterSql = false)
        {
            string str = "";
            foreach (string str2 in key.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                str = SysVariable.CurrentContext.Request[str2];
                if (!string.IsNullOrWhiteSpace(str))
                {
                    if (isFilterSql)
                    {
                        str = str.FilterSql();
                    }
                    break;
                }
            }
            return (str.IsNoNull() ? str : defaultValue);
        }

        public static string GetUrlFileName(this string url)
        {
            if (url.IndexOf('?') >= 0)
            {
                url = url.Substring(0, url.IndexOf('?'));
            }
            return url.Substring(url.LastIndexOf('/') + 1);
        }

        public static int GetUrlIsExists(this HttpWebRequest objHttpWebRequest)
        {
            int num = 1;
            ServicePointManager.Expect100Continue = false;
            try
            {
                ((HttpWebResponse) objHttpWebRequest.GetResponse()).Close();
            }
            catch (WebException exception)
            {
                if (exception.Status != WebExceptionStatus.ProtocolError)
                {
                    return num;
                }
                if (exception.Message.IndexOf("500") > 0)
                {
                    return 500;
                }
                if (exception.Message.IndexOf("401") > 0)
                {
                    return 0x191;
                }
                if (exception.Message.IndexOf("403") > 0)
                {
                    return 0x193;
                }
                if (exception.Message.IndexOf("404") > 0)
                {
                    num = 0x194;
                }
            }
            return num;
        }

        public static int GetUrlIsExists(this string url)
        {
            int num = 1;
            if (url.Length > 1)
            {
                HttpWebRequest request = (HttpWebRequest) WebRequest.Create(new Uri(url));
                ServicePointManager.Expect100Continue = false;
                try
                {
                    ((HttpWebResponse) request.GetResponse()).Close();
                }
                catch (WebException exception)
                {
                    if (exception.Status != WebExceptionStatus.ProtocolError)
                    {
                        return num;
                    }
                    if (exception.Message.IndexOf("500") > 0)
                    {
                        return 500;
                    }
                    if (exception.Message.IndexOf("401") > 0)
                    {
                        return 0x191;
                    }
                    if (exception.Message.IndexOf("403") > 0)
                    {
                        return 0x193;
                    }
                    if (exception.Message.IndexOf("404") > 0)
                    {
                        num = 0x194;
                    }
                }
            }
            return num;
        }

        public static WebProxy GetWebProxy(string ProxyConfigKey = "")
        {
            if (string.IsNullOrWhiteSpace(ProxyConfigKey))
            {
                ProxyConfigKey = "Seven_ProxyHost";
            }
            string str = ConfigHelper.GetValue(ProxyConfigKey, "");
            if (string.IsNullOrWhiteSpace(str))
            {
                throw new ConfigurationErrorsException("找不到" + ProxyConfigKey + "代理配置值");
            }
            string[] strArray = str.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            string str2 = "";
            if (strArray.Length == 1)
            {
                str2 = strArray[0];
            }
            else
            {
                str2 = strArray[RandomHelper.GetRandomNumber(0, strArray.Length)];
            }
            string host = str2.Split(new char[] { ':' })[0];
            return new WebProxy(host, int.Parse(str2.Split(new char[] { ':' })[1]));
        }

        public static HttpWebResponse GetWebResponse(this HttpWebRequest httpWebRequest)
        {
            return (HttpWebResponse) httpWebRequest.GetResponse();
        }

        public static HttpWebResponse GetWebResponse(this string url)
        {
            return url.CreateRequest().GetWebResponse();
        }

        public static byte[] InputStreamByte(bool IsDecompress = true)
        {
            byte[] array = CurrentContext.Request.InputStream.StreamToBytes();
            if (IsDecompress)
            {
                array = array.DecompressGZip();
            }
            return array;
        }

        public static T InputStreamJson<T>(bool IsDecompress = true)
        {
            return InputStreamJson<T>(Encoding.UTF8, IsDecompress);
        }

        public static T InputStreamJson<T>(Encoding encoding, bool IsDecompress = true)
        {
            return InputStreamString(encoding, IsDecompress).JsonJsDeserialize<T>();
        }

        public static string InputStreamString(bool IsDecompress = true)
        {
            return InputStreamString(Encoding.UTF8, IsDecompress);
        }

        public static string InputStreamString(Encoding encoding, bool IsDecompress = true)
        {
            return encoding.GetString(InputStreamByte(IsDecompress));
        }

        public static bool IsAndroid(this EquipmentType objEquipmentType)
        {
            return (objEquipmentType == EquipmentType.Android);
        }

        public static bool IsIos(this EquipmentType objEquipmentType)
        {
            return ((objEquipmentType == EquipmentType.Iphone) || (objEquipmentType == EquipmentType.Ipad));
        }

        public static bool IsIpad(this EquipmentType objEquipmentType)
        {
            return (objEquipmentType == EquipmentType.Ipad);
        }

        public static bool IsPC(this EquipmentType objEquipmentType)
        {
            return ((objEquipmentType == EquipmentType.PC) || (objEquipmentType == EquipmentType.Unknown));
        }

        public static bool IsPhone(this EquipmentType objEquipmentType)
        {
            return ((objEquipmentType != EquipmentType.PC) && (objEquipmentType != EquipmentType.Unknown));
        }

        private static string QueryAppendKey()
        {
            string userAgent = CurrentContext.Request.UserAgent;
            string str2 = ConfigHelper.GetValue("SignatureID", "");
            string str3 = "";
            if (!string.IsNullOrWhiteSpace(str2))
            {
                if (CurrentContext.Session != null)
                {
                    object obj2 = CurrentContext.Session[str2];
                    if (obj2 != null)
                    {
                        str3 = obj2.ToString();
                    }
                }
                if (string.IsNullOrWhiteSpace(str3))
                {
                    str3 = CurrentContext.Request[str2];
                }
                if (string.IsNullOrWhiteSpace(str3))
                {
                    str3 = "";
                }
            }
            userAgent = string.IsNullOrWhiteSpace(userAgent) ? "1214c5050d7a4d9286624018d32e07aa" : userAgent;
            return (userAgent + GetRealIp() + str3);
        }

        public static bool RemoteFileExists(this string fileUrl)
        {
            bool flag = false;
            WebResponse response = null;
            try
            {
                response = WebRequest.Create(fileUrl).GetResponse();
                flag = response != null;
            }
            catch (Exception)
            {
                flag = false;
            }
            finally
            {
                if (response != null)
                {
                    response.Close();
                }
            }
            return flag;
        }

        public static HttpWebRequest SetWebProxy(this HttpWebRequest objHttpWebRequest, string ProxyConfigKey = "")
        {
            if (string.IsNullOrWhiteSpace(ProxyConfigKey))
            {
                ProxyConfigKey = "Seven_ProxyHost";
            }
            string str = ConfigHelper.GetValue(ProxyConfigKey, "");
            if (string.IsNullOrWhiteSpace(str))
            {
                throw new ConfigurationErrorsException("找不到" + ProxyConfigKey + "代理配置值");
            }
            string[] strArray = str.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            string str2 = "";
            if (strArray.Length == 1)
            {
                str2 = strArray[0];
            }
            else
            {
                str2 = strArray[RandomHelper.GetRandomNumber(0, strArray.Length)];
            }
            string host = str2.Split(new char[] { ':' })[0];
            int port = int.Parse(str2.Split(new char[] { ':' })[1]);
            objHttpWebRequest.Proxy = new WebProxy(host, port);
            return objHttpWebRequest;
        }

        public static HttpWebRequest SetWebProxy(this HttpWebRequest objHttpWebRequest, string host, int port)
        {
            objHttpWebRequest.Proxy = new WebProxy(host, port);
            return objHttpWebRequest;
        }

        public static string SignatureInterfaceMD5(this string url, string encryptKey = "")
        {
            string[] strArray = url.Split(new char[] { '?' });
            QueryStringHelper helper = new QueryStringHelper(url);
            helper.Add("SignatureStamp", TimeHelper.StampSecond.ToString());
            string str = strArray[0];
            string str2 = helper.CreateQuery().SignatureMD5(encryptKey);
            helper.Add("SignatureMD5", str2);
            return (str + helper.CreateEncodeUrlQueryString());
        }

        public static bool SignatureInterfaceMD5Check(int TimeoutSecond = 0, string encryptKey = "", string IpPattern = "")
        {
            return (SignatureInterfaceMD5CheckResult(TimeoutSecond, IpPattern, encryptKey).ResultCode == "0");
        }

        public static bool SignatureInterfaceMD5Check<T>(out T objT, int TimeoutSecond = 0, string encryptKey = "", string IpPattern = "", bool IsDecompress = true) where T: class, new()
        {
            return (SignatureInterfaceMD5CheckResult<T>(out objT, Encoding.UTF8, TimeoutSecond, IpPattern, encryptKey, IsDecompress).ResultCode == "0");
        }

        public static InvokeResult SignatureInterfaceMD5CheckResult(int TimeoutSecond = 0, string IpPattern = "", string encryptKey = "")
        {
            InvokeResult result = new InvokeResult();
            if (!string.IsNullOrWhiteSpace(IpPattern) && !GetRealIp().ToLower().IsMatch(IpPattern))
            {
                result.ResultCode = "Signature";
                result.ResultMessage = "对不起你请求的IP不允许";
                return result;
            }
            string str = GetString("SignatureMD5,CheckCode");
            if (str.IsNull())
            {
                result.ResultCode = "Signature";
                result.ResultMessage = "对不起你缺少签名参数";
                return result;
            }
            if (TimeoutSecond > 0)
            {
                int @int = GetInt("SignatureStamp");
                if (@int.IsNull())
                {
                    result.ResultCode = "Signature";
                    result.ResultMessage = "对不起你缺少签名时间";
                    return result;
                }
                if ((TimeHelper.StampSecond - @int) > TimeoutSecond)
                {
                    result.ResultCode = "Signature";
                    result.ResultMessage = "对不起你缺少签名时间超时";
                    return result;
                }
            }
            if (!CurrentContext.Request.Url.Query.TrimStart(new char[] { '?' }).Replace("&SignatureMD5=.+", "", RegexOptions.IgnoreCase).Replace("&CheckCode=.+", "", RegexOptions.IgnoreCase).DecodeUrl().SignatureCheckMD5(str, encryptKey))
            {
                result.ResultCode = "Signature";
                result.ResultMessage = "对不起你签名不正确";
                return result;
            }
            return result;
        }

        public static InvokeResult SignatureInterfaceMD5CheckResult<T>(out T objT, int TimeoutSecond = 0, string encryptKey = "", string IpPattern = "", bool IsDecompress = true) where T: class, new()
        {
            return SignatureInterfaceMD5CheckResult<T>(out objT, Encoding.UTF8, TimeoutSecond, IpPattern, encryptKey, IsDecompress);
        }

        public static InvokeResult SignatureInterfaceMD5CheckResult<T>(out T objT, Encoding encoding, int TimeoutSecond = 0, string IpPattern = "", string encryptKey = "", bool IsDecompress = true) where T: class, new()
        {
            objT = default(T);
            InvokeResult result = new InvokeResult();
            if (!string.IsNullOrWhiteSpace(IpPattern) && !GetRealIp().ToLower().IsMatch(IpPattern))
            {
                result.ResultCode = "Signature";
                result.ResultMessage = "对不起你请求的IP不允许";
                return result;
            }
            string str = GetString("SignatureMD5,CheckCode");
            if (str.IsNull())
            {
                result.ResultCode = "Signature";
                result.ResultMessage = "对不起你缺少签名参数";
                return result;
            }
            string str2 = GetString("ContentMD5");
            if (str2.IsNull())
            {
                result.ResultCode = "ContentMD5";
                result.ResultMessage = "对不起你缺少内容签名参数";
                return result;
            }
            if (TimeoutSecond > 0)
            {
                int @int = GetInt("SignatureStamp");
                if (@int.IsNull())
                {
                    result.ResultCode = "Signature";
                    result.ResultMessage = "对不起你缺少签名时间";
                    return result;
                }
                if ((TimeHelper.StampSecond - @int) > TimeoutSecond)
                {
                    result.ResultCode = "Signature";
                    result.ResultMessage = "对不起你缺少签名时间超时";
                    return result;
                }
            }
            if (!CurrentContext.Request.Url.Query.TrimStart(new char[] { '?' }).Replace("&SignatureMD5=.+", "", RegexOptions.IgnoreCase).Replace("&CheckCode=.+", "", RegexOptions.IgnoreCase).DecodeUrl().SignatureCheckMD5(str, encryptKey))
            {
                result.ResultCode = "Signature";
                result.ResultMessage = "对不起你签名不正确";
                return result;
            }
            byte[] bytes = InputStreamByte(IsDecompress);
            if (bytes.MD5Encrypt().ToLower() != str2.ToLower())
            {
                result.ResultCode = "Signature";
                result.ResultMessage = "对不起你内容签名不正确";
                return result;
            }
            objT = encoding.GetString(bytes).JsonDeserialize<T>();
            return result;
        }

        public static string SignatureQueryMD5(this string url, string appendKey = "", string encryptKey = "", bool isMustQuery = true)
        {
            string[] strArray = url.Split(new char[] { '?' });
            if (!((strArray.Length != 1) || isMustQuery))
            {
                return url;
            }
            QueryStringHelper helper = new QueryStringHelper(url);
            helper.Add("SignatureStamp", TimeHelper.StampSecond.ToString());
            string str = strArray[0];
            int num = str.LastIndexOf('/');
            string str3 = (((num >= 0) ? str.Substring(num + 1) : str) + helper.CreateQuery() + QueryAppendKey() + appendKey).SignatureMD5(encryptKey);
            helper.Add("SignatureMD5", str3);
            return (str + helper.CreateEncodeUrlQueryString());
        }

        public static bool SignatureQueryMD5Check(int TimeoutSecond = 0, string AppendKey = "", string encryptKey = "", bool isMustQuery = true)
        {
            return (SignatureQueryMD5CheckResult(TimeoutSecond, AppendKey, encryptKey, isMustQuery).ResultCode == "0");
        }

        public static InvokeResult SignatureQueryMD5CheckResult(int TimeoutSecond = 0, string AppendKey = "", string encryptKey = "", bool isMustQuery = true)
        {
            InvokeResult result = new InvokeResult();
            if (CurrentContext.Request.QueryString.Count > 0)
            {
                string str = GetString("SignatureMD5,CheckCode");
                if (str.IsNull())
                {
                    result.ResultCode = "Signature";
                    result.ResultMessage = "对不起你缺少签名参数";
                    return result;
                }
                if (TimeoutSecond > 0)
                {
                    int @int = GetInt("SignatureStamp");
                    if (@int.IsNull())
                    {
                        result.ResultCode = "Signature";
                        result.ResultMessage = "对不起你缺少签名时间";
                        return result;
                    }
                    if ((TimeHelper.StampSecond - @int) > TimeoutSecond)
                    {
                        result.ResultCode = "Signature";
                        result.ResultMessage = "对不起你缺少签名时间超时";
                        return result;
                    }
                }
                string path = CurrentContext.Request.Path;
                if (!(path.Substring(path.LastIndexOf('/') + 1) + CurrentContext.Request.Url.Query.TrimStart(new char[] { '?' }).Replace("&SignatureMD5=.+", "", RegexOptions.IgnoreCase).Replace("&CheckCode=.+", "", RegexOptions.IgnoreCase).DecodeUrl() + QueryAppendKey() + AppendKey).SignatureCheckMD5(str, encryptKey))
                {
                    result.ResultCode = "Signature";
                    result.ResultMessage = "对不起你签名不正确";
                    return result;
                }
                return result;
            }
            if (isMustQuery)
            {
                result.ResultCode = "Signature";
                result.ResultMessage = "对不起没有任何参数";
                return result;
            }
            return result;
        }

        public static string SignatureRedirectMD5(this string url, string encryptKey = "")
        {
            return url.SignatureQueryMD5(CurrentContext.Request.Url.ToString().ToLower(), encryptKey, true);
        }

        public static bool SignatureRedirectMD5Check(int TimeoutSecond = 0, string encryptKey = "", string hostPattern = "")
        {
            string str = (CurrentContext.Request.UrlReferrer == null) ? "" : CurrentContext.Request.UrlReferrer.ToString().ToLower();
            return (SignatureRedirectMD5CheckResult(TimeoutSecond, str, encryptKey).ResultCode == "0");
        }

        public static InvokeResult SignatureRedirectMD5CheckResult(int TimeoutSecond = 0, string encryptKey = "", string hostPattern = "")
        {
            InvokeResult result = new InvokeResult();
            if (CurrentContext.Request.UrlReferrer == null)
            {
                result.ResultCode = "Signature";
                result.ResultMessage = "对不起你缺少来源地址";
                return result;
            }
            if (!string.IsNullOrWhiteSpace(hostPattern) && !hostPattern.CheckUrlReferrer())
            {
                result.ResultCode = "Signature";
                result.ResultMessage = "对不起你来源地址不正确";
                return result;
            }
            string appendKey = (CurrentContext.Request.UrlReferrer == null) ? "" : CurrentContext.Request.UrlReferrer.ToString().ToLower();
            return SignatureQueryMD5CheckResult(TimeoutSecond, appendKey, encryptKey, true);
        }

        public static HttpContext CurrentContext
        {
            get
            {
                return HttpContext.Current;
            }
        }

        public static string GetServerHost
        {
            get
            {
                return HttpContext.Current.Request.ServerVariables["SERVER_NAME"].ToString();
            }
        }

        public static bool IsWeiXinBrowse
        {
            get
            {
                if (string.IsNullOrWhiteSpace(CurrentContext.Request.UserAgent))
                {
                    return false;
                }
                return CurrentContext.Request.UserAgent.ToLower().Contains("micromessenger");
            }
        }

        public static string RawUrl
        {
            get
            {
                return CurrentContext.Request.RawUrl;
            }
        }

        public static EquipmentType RequestEquipment
        {
            get
            {
                if (((SysVariable.CurrentContext == null) || (SysVariable.CurrentContext.Request == null)) || string.IsNullOrWhiteSpace(SysVariable.CurrentContext.Request.UserAgent))
                {
                    return EquipmentType.Unknown;
                }
                string userAgent = SysVariable.CurrentContext.Request.UserAgent;
                if (userAgent.ToLower().Contains("android"))
                {
                    return EquipmentType.Android;
                }
                if (userAgent.ToLower().Contains("ipad"))
                {
                    return EquipmentType.Ipad;
                }
                if (userAgent.ToLower().Contains("iphone"))
                {
                    return EquipmentType.Iphone;
                }
                return EquipmentType.PC;
            }
        }

        public static string SingleRequestUrl
        {
            get
            {
                try
                {
                    return CurrentContext.Request.Url.ToString().Split(new char[] { '?' })[0];
                }
                catch
                {
                    return CurrentContext.Request.Url.ToString();
                }
            }
        }

        public static string URLBase
        {
            get
            {
                if (URLPort == 80)
                {
                    return ("http://" + UrlHost);
                }
                return ("http://" + UrlHost + ":" + URLPort.ToString());
            }
        }

        public static string UrlHost
        {
            get
            {
                if (CurrentContext != null)
                {
                    return CurrentContext.Request.Url.Host;
                }
                return "www.Seven.com";
            }
        }

        public static string UrlHostDomain
        {
            get
            {
                string[] strArray = UrlHost.Split(new char[] { '.' });
                return (strArray[strArray.Length - 2] + "." + strArray[strArray.Length - 1]);
            }
        }

        public static int URLPort
        {
            get
            {
                if (CurrentContext != null)
                {
                    return CurrentContext.Request.Url.Port;
                }
                return 80;
            }
        }

        public static string URLSuffix
        {
            get
            {
                if (URLPort == 80)
                {
                    return (UrlHost + SysVariable.ApplicationPath);
                }
                return (UrlHost + ":" + URLPort.ToString() + SysVariable.ApplicationPath);
            }
        }

        public static string UserHostAddress
        {
            get
            {
                return GetRealIp();
            }
        }

        public class HttpPostItem
        {
            private bool _isEncoded;
            private string _parameter;
            private string _value;

            public HttpPostItem(string paramter, string postValue) : this(paramter, postValue, true)
            {
            }

            public HttpPostItem(string paramter, string postValue, bool isEncoded)
            {
                this._parameter = null;
                this._value = null;
                this._isEncoded = false;
                this._parameter = paramter;
                this._value = postValue;
                this._isEncoded = isEncoded;
            }

            public override string ToString()
            {
                return string.Format("{0}={1}", this._parameter, this._isEncoded ? this._value.EncodeUrl() : this._value);
            }
        }
    }
}

