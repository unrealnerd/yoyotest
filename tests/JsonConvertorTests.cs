using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using web.Models;
using Xunit;

namespace tests
{
    public class JsonConvertorTests
    {
        [Fact]
        public void JsonConvertor_Handles_Valid_IntString_Input_To_Int_On_Deserializing()
        {
            var jsonString = @"{ ""ShuttleNo"":""1"" }";

            var result = JsonSerializer.Deserialize<Shuttle>(jsonString);

            Assert.NotNull(result);
            Assert.IsType<int>(result.ShuttleNo);
        }

        [Fact]
        public void JsonConvertor_Handles_InValid_IntString_Input_To_Int_On_Deserializing()
        {
            var jsonString = @"{ ""ShuttleNo"":""A"" }";

            Action deserialize = () => JsonSerializer.Deserialize<Shuttle>(jsonString);

            var ex = Assert.Throws<InvalidCastException>(deserialize);
            Assert.NotNull(ex);
        }

        [Fact]
        public void JsonConvertor_Handles_Valid_TimeSpan_Input_To_TimeSpan_On_Deserializing()
        {
            var jsonString = @"{ ""StartTime"":""10:20"" }";

            var result = JsonSerializer.Deserialize<Shuttle>(jsonString);

            Assert.NotNull(result);
            Assert.IsType<TimeSpan>(result.StartTime);
            Assert.Equal(new TimeSpan(0, 10, 20), result.StartTime);
        }

        [Fact]
        public void JsonConvertor_Handles_InValid_TimeSpan_Input_To_TimeSpan_On_Deserializing()
        {
            var jsonString = @"{ ""StartTime"":""99A"" }";

            Action deserialize = () => JsonSerializer.Deserialize<Shuttle>(jsonString);

            var ex = Assert.Throws<InvalidCastException>(deserialize);
            Assert.NotNull(ex);
        }
    }
}
