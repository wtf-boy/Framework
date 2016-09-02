using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WTF.Framework
{
    public class LongConverter : JsonConverter
    {
        // Methods
        public override bool CanConvert(Type objectType)
        {
            string fullName = objectType.FullName;
            return ((fullName != null) && ((fullName == "System.Int64") || (fullName == "System.UInt64")));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (typeof(ulong) == objectType)
            {
                return ulong.Parse(reader.Value.ToString());
            }
            return long.Parse(reader.Value.ToString());
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(value.ToString());
        }

    }



}
