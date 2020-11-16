using System.Text.Json.Serialization;
using Yoyo.Utils;

namespace Yoyo.Business.Models
{
    public class Athlete
    {
        [JsonConverter(typeof(StringToIntConverter))]
        public int Id { get; set; }
        public string Name { get; set; }
        
    }
}