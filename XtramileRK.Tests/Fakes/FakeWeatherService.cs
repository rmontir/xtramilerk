using System;
using System.Threading.Tasks;
using XtramileRK.External.OpenWeather;
using XtramileRK.External.OpenWeather.Dto;
using XtramileRK.External.OpenWeather.Models;

namespace XtramileRK.Tests.Fakes
{
    public class FakeWeatherService : IWeatherService
    {
        private readonly string ApiKey;

        public FakeWeatherService(
            string apiKey
        )
        {
            ApiKey = apiKey;
        }

        public async Task<WeatherResponse> GetWeatherInfoAsync(string city, string countryCode)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(city) && !string.IsNullOrWhiteSpace(countryCode))
                {
                    if (ApiKey == null)
                    {
                        throw new InvalidOperationException("Api Key is not set");
                    }
                    else if (ApiKey == "1234567")
                    {
                        if (city == "London" && countryCode == "GB")
                        {
                            WeatherInfo weatherInfo = FakeWeatherInfo.CreateDummyWeatherInfo();
                            return await Task.FromResult(new WeatherResponse(false, "Weather info retrieved.", WeatherInfoDto.ConvertToDto(weatherInfo)));
                        }
                        else
                        {
                            throw new Exception("404");
                        }
                    }
                    else
                    {
                        throw new Exception("401");
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
                    return await Task.FromResult(new WeatherResponse(true, $"Unable to access the Api, the Api Key is invalid.", null));
                }
                else if (exc.Message.Contains("404"))
                {
                    return await Task.FromResult(new WeatherResponse(true, $"Weather info is not found for City: {city}.", null));
                }
                else
                {
                    return await Task.FromResult(new WeatherResponse(true, exc.Message, null));
                }
            }
        }
    }
}
