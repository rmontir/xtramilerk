using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using XtramileRK.External.OpenWeather;
using XtramileRK.External.OpenWeather.Models;

namespace XtramileRK.ViewComponents
{
    [ViewComponent(Name = "WeatherInfo")]
    public class WeatherInfoViewComponent : ViewComponent
    {
        private readonly IWeatherService _weatherService;

        public WeatherInfoViewComponent(
            IWeatherService weatherService
        )
        {
            _weatherService = weatherService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string city, string countryCode)
        {
            if (string.IsNullOrWhiteSpace(city) && string.IsNullOrWhiteSpace(countryCode))
            {
                return View();
            }
            else
            {
                WeatherResponse response = await GetWeatherInfoAsync(city, countryCode).ConfigureAwait(false);
                return View(response);
            }
        }

        private async Task<WeatherResponse> GetWeatherInfoAsync(string city, string countryCode)
        {
            WeatherResponse weatherResponse = await _weatherService.GetWeatherInfoAsync(city, countryCode).ConfigureAwait(false);
            return weatherResponse;
        }
    }
}
