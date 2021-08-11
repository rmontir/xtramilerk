using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using XtramileRK.External.OpenWeather.Dto;
using XtramileRK.External.OpenWeather.Models;

namespace XtramileRK.External.OpenWeather
{
    public class WeatherService : IWeatherService, IDisposable
    {
        private readonly IOptions<OpenWeatherOptions> _optionsAccessor;
        private readonly HttpClient _httpClient;

        private string ApiKey => _optionsAccessor.Value.ApiKey ?? throw new InvalidOperationException("Api Key is not set");

        public WeatherService(
            IOptions<OpenWeatherOptions> optionsAccessor
        )
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("http://api.openweathermap.org/data/2.5/")
            };
            _optionsAccessor = optionsAccessor;
        }

        public async Task<WeatherResponse> GetWeatherInfoAsync(string city, string countryCode)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(city) && !string.IsNullOrWhiteSpace(countryCode))
                {
                    HttpResponseMessage httpResponseMessage = await _httpClient.GetAsync(
                        requestUri: new Uri($"weather?q={city},{countryCode}&APPID={ApiKey}&units=imperial", UriKind.Relative)
                    ).ConfigureAwait(false);

                    httpResponseMessage.EnsureSuccessStatusCode();

                    string json = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                    WeatherInfo result = JsonSerializer.Deserialize<WeatherInfo>(json);

                    if (result.Cod == 200)
                    {
                        return new WeatherResponse(false, "Weather info retrieved.", WeatherInfoDto.ConvertToDto(result));
                    }
                    else
                    {
                        throw new Exception(result.Message);
                    }
                }
                else
                {
                    throw new Exception("Weather info is not found. There is an empty parameter(s).");
                }
            }
            catch (Exception exc)
            {
                if (exc.Message.Contains("401"))
                {
                    return new WeatherResponse(true, $"Unable to access the Api, the Api Key is invalid.", null);
                }
                else if (exc.Message.Contains("404"))
                {
                    return new WeatherResponse(true, $"Weather info is not found for City: {city}.", null);
                }
                else
                {
                    return new WeatherResponse(true, exc.Message, null);
                }
            }
        }

        #region IDisposable Support

        private bool _disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _httpClient.Dispose();
                }
                _disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
