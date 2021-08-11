using System.Text.Json.Serialization;

namespace XtramileRK.External.OpenWeather.Models
{
    public class WeatherCondition
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("main")]
        public string Main { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
        [JsonPropertyName("icon")]
        public string Icon { get; set; }

        public WeatherCondition() { }

        public WeatherCondition(
            int id,
            string main,
            string description,
            string icon
        )
        {
            Id = id;
            Main = main;
            Description = description;
            Icon = icon;
        }
    }
}
