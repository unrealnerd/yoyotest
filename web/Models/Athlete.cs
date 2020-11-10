using System.Text.Json.Serialization;
using web.Utils;

namespace web.Models
{
    public class Athlete
    {
        [JsonConverter(typeof(StringToIntConverter))]
        public int Id { get; set; }
        public string Name { get; set; }
        
    }
}