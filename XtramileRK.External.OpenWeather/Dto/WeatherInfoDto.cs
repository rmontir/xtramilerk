using System;
using System.Linq;
using XtramileRK.External.OpenWeather.Models;

namespace XtramileRK.External.OpenWeather.Dto
{
    public class WeatherInfoDto
    {
        public string Location { get; }
        public DateTime Time { get; }
        public decimal Wind { get; }
        public int Visibility { get; }
        public string SkyConditions { get; }
        public decimal TempC { get; }
        public decimal TempF { get; }
        public decimal DewPoint { get; }
        public int RelativeHumidity { get; }
        public int Pressure { get; }

        private WeatherInfoDto(
            string location,
            DateTime time,
            decimal wind,
            int visibility,
            string skyConditions,
            decimal tempC,
            decimal tempF,
            decimal dewPoint,
            int relativeHumidity,
            int pressure
        )
        {
            Location = location;
            Time = time;
            Wind = wind;
            Visibility = visibility;
            SkyConditions = skyConditions;
            TempC = tempC;
            TempF = tempF;
            DewPoint = dewPoint;
            RelativeHumidity = relativeHumidity;
            Pressure = pressure;
        }

        public static WeatherInfoDto ConvertToDto(WeatherInfo model)
        {
            return new WeatherInfoDto(
                location: $"{model.Coordinate.Lat}, {model.Coordinate.Lon}",
                time: CalculateTimestamp(model.Dt),
                wind: model.Wind.Speed,
                visibility: model.Visibility,
                skyConditions: model.Weather.First().Main,
                tempC: ConvertToCelcius(model.Main.Temp),
                tempF: model.Main.Temp,
                dewPoint: CalculateDewPoint(model.Main.Temp, model.Main.Humidity),
                relativeHumidity: model.Main.Humidity,
                pressure: model.Main.Pressure
            );
        }

        private static DateTime CalculateTimestamp(int unix_timestamp)
        {
            TimeSpan timeSpan = TimeSpan.FromSeconds(unix_timestamp);
            return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).Add(timeSpan);
        }

        private static decimal ConvertToCelcius(decimal fahrenheit)
        {
            return (fahrenheit - 32m) * 0.5556m;
        }

        private static decimal CalculateDewPoint(decimal fahrenheit, int humidity)
        {
            decimal celcius = ConvertToCelcius(fahrenheit);
            return celcius - ((100 - humidity) / 5);
        }
    }
}
