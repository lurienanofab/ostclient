using Newtonsoft.Json;
using System;

namespace OstClient.Models.Converters
{
    public class NullableDateTimeConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(DateTime?) == objectType;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (existingValue == null)
                return default(DateTime?);

            DateTime result;
            if (DateTime.TryParse(existingValue.ToString(), out result))
                return result;
            else
                return default(DateTime?);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override bool CanRead
        {
            get { return true; }
        }

        public override bool CanWrite
        {
            get { return false; }
        }
    }
}
