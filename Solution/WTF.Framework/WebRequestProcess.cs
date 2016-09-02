namespace WTF.Framework
{
    using System;
    using System.IO;
    using System.Net;
    using System.Runtime.InteropServices;
    using System.Text;

    public class WebRequestProcess
    {
        private HttpWebRequest _HttpWebRequest = null;

        protected WebRequestProcess()
        {
        }

        public static WebRequestProcess Create(string requestUriString, RequestMethod objRequestMethod = 0, int timeout = 0xea60)
        {
            WebRequestProcess process = new WebRequestProcess {
                WebRequest = System.Net.WebRequest.Create(requestUriString) as HttpWebRequest
            };
            process.WebRequest.Method = objRequestMethod.ToString();
            process.WebRequest.Timeout = timeout;
            process.WebRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/34.0.1847.116 Safari/537.36";
            return process;
        }

        public HttpWebResponse CreatePost(byte[] data)
        {
            StringBuilder builder = new StringBuilder();
            this._HttpWebRequest.Method = "POST";
            this._HttpWebRequest.ContentLength = data.Length;
            if (string.IsNullOrWhiteSpace(this._HttpWebRequest.ContentType))
            {
                this._HttpWebRequest.ContentType = "application/x-www-form-urlencoded";
            }
            this._HttpWebRequest.KeepAlive = false;
            using (Stream stream = this._HttpWebRequest.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
                stream.Close();
                return (this._HttpWebRequest.GetResponse() as HttpWebResponse);
            }
        }

        public string GetPageText()
        {
            return this.GetPageText(Encoding.UTF8);
        }

        public string GetPageText(Encoding encoding)
        {
            return this._HttpWebRequest.GetPageText(encoding);
        }

        public string GetPageTextPost(byte[] data, Encoding encoding, bool IsCompress = true)
        {
            if (IsCompress)
            {
                data = data.CompressGZip();
            }
            return this.CreatePost(data).GetPageText(encoding);
        }

        public Stream GetResponseStream()
        {
            return this._HttpWebRequest.GetWebResponse().GetResponseStream();
        }

        public HttpWebResponse GetWebResponse()
        {
            return this._HttpWebRequest.GetWebResponse();
        }

        public HttpWebRequest WebRequest
        {
            get
            {
                return this._HttpWebRequest;
            }
            set
            {
                this._HttpWebRequest = value;
            }
        }
    }
}

