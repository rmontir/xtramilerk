using System.Text.Json.Serialization;

namespace XtramileRK.External.OpenWeather.Models
{
    public class WindCondition
    {
        [JsonPropertyName("speed")]
        public decimal Speed { get; set; }
        [JsonPropertyName("deg")]
        public int Deg { get; set; }
        [JsonPropertyName("gust")]
        public decimal Gust { get; set; }

        public WindCondition() { }

        public WindCondition(
            decimal speed,
            int deg,
            decimal gust
        )
        {
            Speed = speed;
            Deg = deg;
            Gust = gust;
        }
    }
}
