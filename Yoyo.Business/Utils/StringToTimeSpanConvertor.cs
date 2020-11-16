using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Yoyo.Utils
{
    public class StringToTimeSpanConvertor : JsonConverter<TimeSpan>
    {
        public override TimeSpan Read(
            ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string stringValue = reader.GetString();
            if (TimeSpan.TryParseExact(
                stringValue, @"mm\:ss",
                System.Globalization.CultureInfo.InvariantCulture,
                System.Globalization.TimeSpanStyles.None, out TimeSpan value))
            {
                return value;
            }
            else
            {
                throw new InvalidCastException();
            }
        }

        public override void Write(
            Utf8JsonWriter writer, TimeSpan value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(@"mm\:ss"));
        }
    }
}
