namespace WTF.Framework
{
    using System;
    using System.Runtime.InteropServices;
    using System.Web.UI;

    public class PageCore : Page
    {
        public Guid GetGuid(string key)
        {
            return this.GetGuid(key, ObjectHelper.NullGuid);
        }

        public Guid GetGuid(string key, Guid defaultValue)
        {
            return this.GetString(key, defaultValue.ToString(), true).ConvertGuid();
        }

        public decimal GetHeadersDecimal(string key)
        {
            return this.GetHeadersDecimal(key, ObjectHelper.NullDecimal);
        }

        public decimal GetHeadersDecimal(string key, decimal defaultValue)
        {
            return this.GetHeadersString(key, defaultValue.ToString()).ConvertDecimal();
        }

        public double GetHeadersDouble(string key)
        {
            return this.GetHeadersDouble(key, ObjectHelper.NullDouble);
        }

        public double GetHeadersDouble(string key, double defaultValue)
        {
            return this.GetHeadersString(key, defaultValue.ToString()).ConvertDouble();
        }

        public double GetHeadersFloat(string key)
        {
            return (double) this.GetHeadersFloat(key, ObjectHelper.NullFloat);
        }

        public float GetHeadersFloat(string key, float defaultValue)
        {
            return this.GetHeadersString(key, defaultValue.ToString()).ConvertFloat();
        }

        public int GetHeadersInt(string key)
        {
            return this.GetHeadersInt(key, ObjectHelper.NullInt).ConvertInt();
        }

        public int GetHeadersInt(string key, int defaultValue)
        {
            return this.GetHeadersString(key, defaultValue.ToString()).ConvertInt();
        }

        public long GetHeadersLong(string key)
        {
            return this.GetHeadersLong(key, ObjectHelper.NullLong);
        }

        public long GetHeadersLong(string key, long defaultValue)
        {
            return this.GetHeadersString(key, defaultValue.ToString()).ConvertLong();
        }

        public string GetHeadersString(string key, string defaultValue = "")
        {
            string str = string.Empty;
            foreach (string str2 in key.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                str = base.Request.Headers[str2];
                if (!string.IsNullOrEmpty(str))
                {
                    break;
                }
            }
            return (str.IsNoNull() ? str : defaultValue);
        }

        public int GetInt(string key)
        {
            return this.GetInt(key, ObjectHelper.NullInt);
        }

        public int GetInt(string key, int defaultValue)
        {
            return this.GetString(key, defaultValue.ToString(), true).ConvertInt();
        }

        public long GetLong(string key)
        {
            return this.GetLong(key, ObjectHelper.NullLong);
        }

        public long GetLong(string key, long defaultValue)
        {
            return this.GetString(key, defaultValue.ToString(), true).ConvertLong();
        }

        public string GetString(string key, bool filterSql = true)
        {
            return this.GetString(key, string.Empty, filterSql);
        }

        public string GetString(string key, string defaultValue, bool filterSql = true)
        {
            string str = "";
            foreach (string str2 in key.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                str = base.Request[str2];
                if (!string.IsNullOrWhiteSpace(str))
                {
                    if (filterSql)
                    {
                        str = str.FilterSql();
                    }
                    break;
                }
            }
            return (str.IsNoNull() ? str : defaultValue);
        }

        public virtual bool IsRelease
        {
            get
            {
                return ConfigHelper.IsRelease;
            }
        }
    }
}

