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

            var epoc = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            var delta = ((DateTime) value).ToUniversalTime() - epoc;

            var ticks = (long) delta.TotalSeconds;

            writer.WriteValue(ticks);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            JsonSerializer serializer)
        {
            long ticks;
            switch (reader.TokenType)
            {
                case JsonToken.Integer:
                    ticks = Convert.ToInt64(reader.Value);
                    break;
                case JsonToken.String:
                    if (!long.TryParse(reader.Value.ToString(), out ticks))
                        throw new ArgumentException($"{reader.Value} isn't a number");
                    break;
                default:
                    throw new ArgumentException(
                        $"Unexpected token. Integer or String was expected, got {reader.TokenType}");
            }

            var date = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            date = date.AddSeconds(ticks);

            return date;
        }
    }
}