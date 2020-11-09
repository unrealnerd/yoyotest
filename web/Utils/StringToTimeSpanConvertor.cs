using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace web.Utils
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
                return new TimeSpan(0, 0, 0);
            }
        }

        public override void Write(
            Utf8JsonWriter writer, TimeSpan value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(@"mm\:ss"));
        }
    }
}
