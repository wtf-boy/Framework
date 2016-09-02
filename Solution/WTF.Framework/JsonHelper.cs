namespace WTF.Framework
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using Newtonsoft.Json.Linq;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Web.Script.Serialization;
    using System.Xml.Linq;

    public static class JsonHelper
    {
        public static T GetJsonValue<T>(this string json, string key)
        {
            JContainer container = json.JsonJContainer();
            if (container == null)
            {
                return default(T);
            }
            if (container[key] == null)
            {
                return default(T);
            }
            return container[key].ToObject<T>();
        }

        public static object JsonDeserialize(this string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return null;
            }
            return JsonConvert.DeserializeObject(input);
        }

        public static T JsonDeserialize<T>(this string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return default(T);
            }
            return JsonConvert.DeserializeObject<T>(input);
        }

        public static object JsonDeserialize(this string input, Type type)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return null;
            }
            return JsonConvert.DeserializeObject(input, type);
        }

        public static XNode JsonDeserializeXml(this string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return null;
            }
            return JsonConvert.DeserializeXNode(input);
        }

        public static string JsonFormatWriterOut(this string jsonValue)
        {
            JsonSerializer serializer = new JsonSerializer();
            TextReader reader = new StringReader(jsonValue);
            JsonTextReader reader2 = new JsonTextReader(reader);
            object obj2 = serializer.Deserialize(reader2);
            if (obj2 != null)
            {
                StringWriter textWriter = new StringWriter();
                JsonTextWriter jsonWriter = new JsonTextWriter(textWriter)
                {
                    Formatting = Formatting.Indented,
                    Indentation = 4,
                    IndentChar = ' '
                };
                serializer.Serialize(jsonWriter, obj2);
                return textWriter.ToString();
            }
            return jsonValue;
        }

        public static JContainer JsonJContainer(this string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return null;
            }
            return (JsonConvert.DeserializeObject(input) as JContainer);
        }

        public static object JsonJsDeserialize(string input)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer
            {
                MaxJsonLength = 0x7fffffff
            };
            return serializer.DeserializeObject(input);
        }

        public static T JsonJsDeserialize<T>(this string input)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer
            {
                MaxJsonLength = 0x7fffffff
            };
            return serializer.Deserialize<T>(input);
        }

        public static string JsonJsSerialize(this object data)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer
            {
                MaxJsonLength = 0x7fffffff
            };
            if (data is DataTable)
            {
                DataTable table = (DataTable)data;
                List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
                foreach (DataRow row in table.Rows)
                {
                    Dictionary<string, object> item = new Dictionary<string, object>();
                    foreach (DataColumn column in table.Columns)
                    {
                        item.Add(column.ColumnName, row[column.ColumnName]);
                    }
                    list.Add(item);
                }
                return serializer.Serialize(list);
            }
            return serializer.Serialize(data);
        }

        public static void JsonJsSerialize(this object data, StringBuilder output)
        {
            new JavaScriptSerializer { MaxJsonLength = 0x7fffffff }.Serialize(data, output);
        }

        public static string JsonSerialize(this object data)
        {
            if (data == null)
            {
                return "";
            }
            return JsonConvert.SerializeObject(data);
        }

        public static string JsonSerializeSetings(this object data)
        {
            if (data == null)
            {
                return "";
            }
            JsonSerializerSettings settings = new JsonSerializerSettings();
            IsoDateTimeConverter item = new IsoDateTimeConverter
            {
                DateTimeFormat = "yyyy-MM-dd HH:mm:ss"
            };
            settings.Converters.Add(item);
            return JsonConvert.SerializeObject(data, settings);
        }

        public static string JsonSerializeXml(this string data)
        {
            return JsonConvert.SerializeXNode(XElement.Parse(data));
        }

        public static string JsonSerializeXml(this XNode data)
        {
            if (data == null)
            {
                return "";
            }
            return JsonConvert.SerializeXNode(data);
        }

        public static string SetValue(string json, string key, object value)
        {
            object o = value ?? "";
            JObject data = json.JsonJContainer() as JObject;
            if (data == null)
            {
                data = new JObject(new JProperty(key, JToken.FromObject(o)));
            }
            else if (data[key] != null)
            {
                data.Remove(key);
                data.Add(new JProperty(key, JToken.FromObject(o)));
            }
            else
            {
                data.Add(new JProperty(key, JToken.FromObject(o)));
            }
            return data.JsonSerialize();
        }


        public static JsonSerializerSettings AddSettings(this JsonSerializerSettings objSettings, params JsonSetType[] JsonSettingsTypes)
        {
            if ((JsonSettingsTypes != null) && (JsonSettingsTypes.Length > 0))
            {
                if (JsonSettingsTypes.Contains<JsonSetType>(JsonSetType.DateTime))
                {
                    IsoDateTimeConverter item = new IsoDateTimeConverter
                    {
                        DateTimeFormat = "yyyy-MM-dd HH:mm:ss"
                    };
                    objSettings.Converters.Add(item);
                }
                if (JsonSettingsTypes.Contains<JsonSetType>(JsonSetType.LongString))
                {
                    objSettings.Converters.Add(new LongConverter());
                }
                if (JsonSettingsTypes.Contains<JsonSetType>(JsonSetType.NullIgnore))
                {
                    objSettings.NullValueHandling = NullValueHandling.Ignore;
                }
            }
            return objSettings;
        }
        public static JsonSerializerSettings CreateSettings(params JsonSetType[] jsonSetTypes)
        {
            JsonSerializerSettings objSettings = new JsonSerializerSettings();

            return objSettings.AddSettings(jsonSetTypes);
        }


    }
}

