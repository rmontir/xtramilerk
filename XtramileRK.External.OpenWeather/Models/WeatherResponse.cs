using System.Text.Json.Serialization;
using XtramileRK.External.OpenWeather.Dto;

namespace XtramileRK.External.OpenWeather.Models
{
    public class WeatherResponse
    {
        [JsonPropertyName("error")]
        public bool IsError { get; set; }

        [JsonPropertyName("msg")]
        public string Message { get; set; }

        [JsonPropertyName("data")]
        public WeatherInfoDto Data { get; set; }

        public WeatherResponse(
            bool isError,
            string message,
            WeatherInfoDto data
        )
        {
            IsError = isError;
            Message = message;
            Data = data;
        }
    }
}
