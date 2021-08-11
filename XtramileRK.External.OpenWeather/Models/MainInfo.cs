using System.Text.Json.Serialization;

namespace XtramileRK.External.OpenWeather.Models
{
    public class MainInfo
    {
        [JsonPropertyName("temp")]
        public decimal Temp { get; set; }
        [JsonPropertyName("feels_like")]
        public decimal FeelsLike { get; set; }
        [JsonPropertyName("temp_min")]
        public decimal TempMin { get; set; }
        [JsonPropertyName("temp_max")]
        public decimal TempMax { get; set; }
        [JsonPropertyName("pressure")]
        public int Pressure { get; set; }
        [JsonPropertyName("humidity")]
        public int Humidity { get; set; }

        public MainInfo() { }

        public MainInfo(
            decimal temp,
            decimal feelsLike,
            decimal tempMin,
            decimal tempMax,
            int pressure,
            int humidity
        )
        {
            Temp = temp;
            FeelsLike = feelsLike;
            TempMin = tempMin;
            TempMax = tempMax;
            Pressure = pressure;
            Humidity = humidity;
        }
    }
}
