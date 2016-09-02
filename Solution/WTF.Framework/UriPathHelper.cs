namespace WTF.Framework
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Web;

    public static class UriPathHelper
    {
        public static readonly char PathSeparatorChar = '/';

        public static IDictionary<string, ICollection<string>> AddQueryParam(this IDictionary<string, ICollection<string>> options, string key, params string[] values)
        {
            List<string> list = new List<string>();
            if ((values == null) || (values.Length == 0))
            {
            }
            foreach (string str in values)
            {
                if (!string.IsNullOrWhiteSpace(str))
                {
                    list.Add(str);
                }
            }
            options.AddQueryParam(key, list);
            return options;
        }

        public static IDictionary<string, ICollection<string>> AddQueryParam(this IDictionary<string, ICollection<string>> options, string key, ICollection<string> value)
        {
            if ((!options.ContainsKey(key) && (value != null)) && (value.Count > 0))
            {
                options[key] = value;
            }
            return options;
        }

        public static string AppendArguments(string url, NameValueCollection data)
        {
            Arguments.NotNull<string>(url, "url");
            if ((data != null) && (data.Count > 0))
            {
                if (url.IndexOf("?") == -1)
                {
                    url = url + "?";
                }
                else if (url.IndexOf("&") > -1)
                {
                    url = url + "&";
                }
                url = url + ConstructQueryString(data);
            }
            return url;
        }

        public static string AppendArguments(string url, object values)
        {
            return AppendArguments(url, ToNameValueCollection(values));
        }

        public static string AppendArguments(string url, string key, IEnumerable items)
        {
            foreach (object obj2 in items)
            {
                url = AppendArguments(url, key, obj2);
            }
            return url;
        }

        public static string AppendArguments(string url, string key, object value)
        {
            string str = (string)Convert.ChangeType(value, typeof(string));
            return AppendArguments(url, key, str);
        }

        public static string AppendArguments(string url, string key, string value)
        {
            Arguments.NotNull<string>(url, "url");
            Arguments.NotNull<string>(key, "key");
            if (url.IndexOf("?") == -1)
            {
                url = url + "?";
            }
            else
            {
                url = url + "&";
            }
            url = url + key + "=" + HttpUtility.UrlEncode(value);
            return url;
        }

        public static string BuildQueryParams(this IDictionary<string, ICollection<string>> parameters)
        {
            if (parameters == null)
            {
                return null;
            }
            StringBuilder builder = new StringBuilder();
            int num = 0;
            foreach (KeyValuePair<string, ICollection<string>> pair in parameters)
            {
                if (num != 0)
                {
                    builder.Append('&');
                }
                if (pair.Value != null)
                {
                    if (pair.Value.Count == 1)
                    {
                        string introduced9 = HttpUtility.UrlEncode(pair.Key);
                        builder.AppendFormat("{0}={1}", introduced9, HttpUtility.UrlEncode(pair.Value.Take<string>(1).Single<string>()));
                    }
                    else
                    {
                        int num2 = 0;
                        foreach (string str in pair.Value)
                        {
                            if (num2 > 0)
                            {
                                builder.Append('&');
                            }
                            builder.AppendFormat("{0}={1}", HttpUtility.UrlEncode(pair.Key), HttpUtility.UrlEncode(str));
                            num2++;
                        }
                    }
                }
                num++;
            }
            return builder.ToString();
        }

        public static string Combine(params string[] paths)
        {
            Arguments.NotNull<string[]>(paths, "paths");
            List<string> values = new List<string>();
            for (int i = 0; i < paths.Length; i++)
            {
                string str = paths[i];
                Arguments.NotNull<string>(str, "paths");
                str = str.Trim().Trim(new char[] { PathSeparatorChar });
                if (str.Length > 0)
                {
                    values.Add(str);
                }
            }
            return string.Join(PathSeparatorChar.ToString(), values);
        }

        public static string ConstructQueryString(NameValueCollection data)
        {
            List<string> values = new List<string>();
            IEnumerator enumerator = data.GetEnumerator();

            while (enumerator.MoveNext())
            {
                Func<string, string> selector = null;
                string name = (string)enumerator.Current;
                string[] source = data.GetValues(name);
                if (source != null)
                {
                    if (selector == null)
                    {
                        selector = o => name + "=" + HttpUtility.UrlEncode(o);
                    }
                    values.AddRange(source.Select<string, string>(selector));
                }
            }

            return string.Join("&", values);
        }

        public static IDictionary<string, ICollection<string>> CreateQueryParam(this string key, params string[] values)
        {
            IDictionary<string, ICollection<string>> options = new Dictionary<string, ICollection<string>>();
            return options.AddQueryParam(key, values);
        }

        public static IDictionary<string, ICollection<string>> CreateQueryParam(this string key, ICollection<string> value)
        {
            IDictionary<string, ICollection<string>> options = new Dictionary<string, ICollection<string>>();
            return options.AddQueryParam(key, value);
        }

        public static NameValueCollection ToNameValueCollection(object values)
        {
            NameValueCollection values2 = new NameValueCollection();
            if (values != null)
            {
                TypeAccessor accessor = TypeAccessor.GetAccessor(values.GetType());
                foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(values))
                {
                    object obj2 = accessor.GetPropertyGetter(descriptor.Name)(values);
                    if (obj2 != null)
                    {
                        string str;
                        if (descriptor.PropertyType == typeof(Guid))
                        {
                            str = obj2.ToString();
                        }
                        else
                        {
                            if ((obj2 != null) && (obj2.GetType().GetInterface(typeof(ICollection).FullName) != null))
                            {
                                ICollection is2 = (ICollection)obj2;
                                int num = 0;
                                foreach (object obj3 in is2)
                                {
                                    string name = string.Concat(new object[] { descriptor.Name, "[", num, "]" });
                                    string str3 = (string)Convert.ChangeType(obj3, typeof(string));
                                    values2.Add(name, str3);
                                    num++;
                                }
                                continue;
                            }
                            str = (string)Convert.ChangeType(obj2, typeof(string));
                        }
                        values2.Add(descriptor.Name, str);
                    }
                }
            }
            return values2;
        }
    }
}

