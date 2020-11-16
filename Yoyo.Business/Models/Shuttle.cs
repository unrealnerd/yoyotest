using System;
using System.Text.Json.Serialization;
using Yoyo.Utils;

namespace Yoyo.Business.Models
{
    public class Shuttle
    {
        [JsonConverter(typeof(StringToIntConverter))]
        public int AccumulatedShuttleDistance { get; set; }
        
        [JsonConverter(typeof(StringToIntConverter))]
        public int SpeedLevel { get; set; }

        [JsonConverter(typeof(StringToIntConverter))]
        public int ShuttleNo { get; set; }
        public string Speed { get; set; }
        public string LevelTime { get; set; }

        [JsonConverter(typeof(StringToTimeSpanConvertor))]
        public TimeSpan CommulativeTime { get; set; }

        [JsonConverter(typeof(StringToTimeSpanConvertor))]
        public TimeSpan StartTime { get; set; }
        public string ApproxVo2Max { get; set; }

    }
}