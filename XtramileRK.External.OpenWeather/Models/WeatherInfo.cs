using System.Collections.Immutable;
using System.Text.Json.Serialization;

namespace XtramileRK.External.OpenWeather.Models
{
    public class WeatherInfo
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("cod")]
        public int Cod { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("base")]
        public string Base { get; set; }
        [JsonPropertyName("visibility")]
        public int Visibility { get; set; }
        [JsonPropertyName("dt")]
        public int Dt { get; set; }
        [JsonPropertyName("timezone")]
        public int Timezone { get; set; }
        [JsonPropertyName("message")]
        public string Message { get; set; }

        [JsonPropertyName("coord")]
        public LocationInfo Coordinate { get; set; }
        [JsonPropertyName("weather")]
        public ImmutableHashSet<WeatherCondition> Weather { get; set; }
        [JsonPropertyName("main")]
        public MainInfo Main { get; set; }
        [JsonPropertyName("wind")]
        public WindCondition Wind { get; set; }
        [JsonPropertyName("clouds")]
        public CloudInfo Clouds { get; set; }
        [JsonPropertyName("sys")]
        public SysInfo Sys { get; set; }

        public WeatherInfo() { }

        public WeatherInfo(
            int id,
            int cod,
            string name,
            string @base,
            int visibility,
            int dt,
            int timezone,
            string message,
            LocationInfo coordinate,
            ImmutableHashSet<WeatherCondition> weather,
            MainInfo main,
            WindCondition wind,
            CloudInfo clouds,
            SysInfo sys
        )
        {
            Id = id;
            Cod = cod;
            Name = name;
            Base = @base;
            Visibility = visibility;
            Dt = dt;
            Timezone = timezone;
            Message = message;
            Coordinate = coordinate;
            Weather = weather;
            Main = main;
            Wind = wind;
            Clouds = clouds;
            Sys = sys;
        }

        /*
         {
          "coord": {
            "lon": -0.1257,
            "lat": 51.5085
         },
         "weather": [
           {
             "id": 802,
             "main": "Clouds",
             "description": "scattered clouds",
             "icon": "03d"
           }
         ],
         "base": "stations",
         "main": {
           "temp": 296.03,
           "feels_like": 295.89,
           "temp_min": 294.26,
           "temp_max": 297.32,
           "pressure": 1016,
           "humidity": 58
         },
         "visibility": 10000,
         "wind": {
           "speed": 0.45,
           "deg": 268,
           "gust": 2.24
         },
         "clouds": {
           "all": 38
         },
         "dt": 1628607840,
         "sys": {
           "type": 2,
           "id": 2019646,
           "country": "GB",
           "sunrise": 1628570299,
           "sunset": 1628624011
         },
         "timezone": 3600,
         "id": 2643743,
         "name": "London",
         "cod": 200
        }
         */
    }
}
