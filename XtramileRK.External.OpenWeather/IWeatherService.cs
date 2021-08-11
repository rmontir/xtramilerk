using System.Threading.Tasks;
using XtramileRK.External.OpenWeather.Models;

namespace XtramileRK.External.OpenWeather
{
    public interface IWeatherService
    {
        Task<WeatherResponse> GetWeatherInfoAsync(string city, string countryCode);
    }
}
