using System.Text.Json.Serialization;

namespace XtramileRK.External.OpenWeather.Models
{
    public class SysInfo
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("type")]
        public int Type { get; set; }
        [JsonPropertyName("country")]
        public string Country { get; set; }
        [JsonPropertyName("sunrise")]
        public int Sunrise { get; set; }
        [JsonPropertyName("sunset")]
        public int Sunset { get; set; }

        public SysInfo() { }

        public SysInfo(
            int id,
            int type,
            string country,
            int sunrise,
            int sunset
        )
        {
            Id = id;
            Type = type;
            Country = country;
            Sunrise = sunrise;
            Sunset = sunset;
        }
    }
}
