using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace UnixTimeConverter
{
    public class UnixTimeConverter : DateTimeConverterBase
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is DateTime))
                throw new ArgumentException("Expect DateTime as input parameter");

            var epoc = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            var delta = ((DateTime) value).ToUniversalTime() - epoc;

            var ticks = (long) delta.TotalSeconds;

            writer.WriteValue(ticks);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType != JsonToken.Integer && reader.TokenType != JsonToken.String)
                throw new ArgumentException($"Unexpected token parsing date. Integer or String was expected, got {reader.TokenType}");

            var ticks = (long) reader.Value;

            var date = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            date = date.AddSeconds(ticks);

            return date;
        }
    }
}