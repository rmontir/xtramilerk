using System.Collections.Immutable;
using XtramileRK.External.OpenWeather.Models;

namespace XtramileRK.Tests.Fakes
{
    public class FakeWeatherInfo
    {
        public static WeatherInfo CreateDummyWeatherInfo()
        {
            return new WeatherInfo(
                id: 2643743,
                cod: 200,
                name: "London",
                @base: "stations",
                visibility: 10000,
                dt: 1628607840,
                timezone: 3600,
                message: "",
                coordinate: new LocationInfo(
                    lon: -0.1257m,
                    lat: 51.5085m
                ),
                weather: ImmutableHashSet.Create(new WeatherCondition(
                    id: 802,
                    main: "Clouds",
                    description: "scattered clouds",
                    icon: "03d"
                )),
                main: new MainInfo(
                    temp: 296.03m,
                    feelsLike: 295.89m,
                    tempMin: 294.26m,
                    tempMax: 297.32m,
                    pressure: 1016,
                    humidity: 58
                ),
                wind: new WindCondition(
                    speed: 0.45m,
                    deg: 268,
                    gust: 2.24m
                ),
                clouds: new CloudInfo(
                    all: 38
                ),
                sys: new SysInfo(
                    id: 2019646,
                    type: 2,
                    country: "GB",
                    sunrise: 1628570299,
                    sunset: 1628624011
                )
            );
        }
    }
}
