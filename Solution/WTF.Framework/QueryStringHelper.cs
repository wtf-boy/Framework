namespace WTF.Framework
{
    using System;
    using System.Collections.Specialized;
    using System.Text;

    public class QueryStringHelper : NameValueCollection
    {
        private Encoding _Encoding;

        public QueryStringHelper()
        {
            this._Encoding = Encoding.UTF8;
        }

        public QueryStringHelper(string url) : this(url, Encoding.UTF8)
        {
        }

        public QueryStringHelper(string url, Encoding objEncoding)
        {
            string[] strArray2;
            this._Encoding = Encoding.UTF8;
            this._Encoding = objEncoding;
            if (url.IndexOf('?') >= 0)
            {
                string[] strArray = url.Split(new char[] { '?' });
                if (strArray.Length >= 2)
                {
                    foreach (string str in strArray[1].Split(new char[] { '&' }))
                    {
                        strArray2 = str.Split(new char[] { '=' });
                        if ((((strArray2.Length >= 2) && (strArray2[0].ToLower() != "checkcode")) && (strArray2[0].ToLower() != "signaturemd5")) && (strArray2[0].ToLower() != "signaturestamp"))
                        {
                            this.Add(strArray2[0], strArray2[1]);
                        }
                    }
                }
            }
            else
            {
                foreach (string str in url.Split(new char[] { '&' }))
                {
                    strArray2 = str.Split(new char[] { '=' });
                    if ((((strArray2.Length >= 2) && (strArray2[0].ToLower() != "checkcode")) && (strArray2[0].ToLower() != "signaturemd5")) && (strArray2[0].ToLower() != "signaturestamp"))
                    {
                        this.Add(strArray2[0], strArray2[1]);
                    }
                }
            }
        }

        public string CreateEncodeUrlQuery()
        {
            if (this.Count == 0)
            {
                return "";
            }
            StringBuilder builder = new StringBuilder();
            foreach (string str in this.AllKeys)
            {
                builder.AppendFormat("{0}={1}&", str, base[str].EncodeUrl(this._Encoding));
            }
            return builder.ToString().TrimEnd(new char[] { '&' });
        }

        public string CreateEncodeUrlQueryString()
        {
            if (this.Count == 0)
            {
                return "";
            }
            StringBuilder builder = new StringBuilder();
            builder.Append("?");
            foreach (string str in this.AllKeys)
            {
                builder.AppendFormat("{0}={1}&", str, base[str].EncodeUrl(this._Encoding));
            }
            return builder.ToString().TrimEnd(new char[] { '&' });
        }

        public string CreateQuery()
        {
            if (this.Count == 0)
            {
                return "";
            }
            StringBuilder builder = new StringBuilder();
            foreach (string str in this.AllKeys)
            {
                builder.AppendFormat("{0}={1}&", str, base[str]);
            }
            return builder.ToString().TrimEnd(new char[] { '&' });
        }

        public string CreateQueryString()
        {
            if (this.Count == 0)
            {
                return "";
            }
            StringBuilder builder = new StringBuilder();
            builder.Append("?");
            foreach (string str in this.AllKeys)
            {
                builder.AppendFormat("{0}={1}&", str, base[str]);
            }
            return builder.ToString().TrimEnd(new char[] { '&' });
        }
    }
}

