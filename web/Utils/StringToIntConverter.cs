using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace web.Utils
{
    public class StringToIntConverter : JsonConverter<int>
    {
        public override int Read(
            ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string stringValue = reader.GetString();
            if (int.TryParse(stringValue, out int value))
            {
                return value;
            }
            else
            {
                throw new InvalidCastException();
            }
        }

        public override void Write(
            Utf8JsonWriter writer, int value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
