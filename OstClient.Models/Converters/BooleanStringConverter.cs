using Newtonsoft.Json;
using System;
using System.Linq;

namespace OstClient.Models.Converters
{
    public class BooleanStringConverter : JsonConverter
    {
        private static readonly string[] trueStrings = { "true", "1", "yes" };

        public override bool CanConvert(Type objectType)
        {
            return typeof(string) == objectType;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return trueStrings.Contains(existingValue.ToString());
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
