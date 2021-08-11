using System.Text.Json.Serialization;

namespace XtramileRK.External.OpenWeather.Models
{
    public class LocationInfo
    {
        [JsonPropertyName("lon")]
        public decimal Lon { get; set; }
        [JsonPropertyName("lat")]
        public decimal Lat { get; set; }

        public LocationInfo() { }

        public LocationInfo(
            decimal lon,
            decimal lat
        )
        {
            Lon = lon;
            Lat = lat;
        }
    }
}
