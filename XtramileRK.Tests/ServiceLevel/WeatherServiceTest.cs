using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;
using XtramileRK.External.OpenWeather;
using XtramileRK.External.OpenWeather.Dto;
using XtramileRK.External.OpenWeather.Models;
using XtramileRK.Tests.Fakes;

namespace XtramileRK.Tests.ServiceLevel
{
    [TestClass]
    public class WeatherServiceTest
    {
        private readonly IWeatherService _weatherService;
        private readonly IServiceProvider _serviceProvider;
        private readonly IConfiguration _configuration;

        public WeatherServiceTest()
        {
            _configuration = new ConfigurationBuilder().AddJsonFile("appsettings.test.json").Build();
            ServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddOpenWeatherServices(options =>
            {
                options.ApiKey = _configuration["OpenWeather:ApiKey"];
            });
            _serviceProvider = serviceCollection.BuildServiceProvider();

            _weatherService = _serviceProvider.GetRequiredService<IWeatherService>();
        }

        [TestMethod]
        public async Task Weather_Info_Should_Be_Found_When_City_Is_Available_In_Api()
        {
            WeatherResponse weatherResponse = await _weatherService.GetWeatherInfoAsync("Abbey", "CA").ConfigureAwait(false);
            Assert.IsFalse(weatherResponse.IsError);
            Assert.IsNotNull(weatherResponse.Data);
        }

        [TestMethod]
        public async Task Weather_Info_Should_Not_Be_Found_When_City_Is_Unavailable_In_Api()
        {
            WeatherResponse weatherResponse = await _weatherService.GetWeatherInfoAsync("100 Mile House", "CA").ConfigureAwait(false);
            Assert.IsTrue(weatherResponse.IsError);
            Assert.IsNull(weatherResponse.Data);
        }

        [TestMethod]
        public async Task Weather_Info_Should_Be_Error_When_Parameter_Has_Empty_Value()
        {
            WeatherResponse weatherResponse = await _weatherService.GetWeatherInfoAsync("", "CA").ConfigureAwait(false);
            Assert.IsTrue(weatherResponse.IsError);
            Assert.IsNull(weatherResponse.Data);
        }

        [TestMethod]
        public async Task Weather_Info_Should_Be_Error_When_Api_Key_Is_Invalid()
        {
            ServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddOpenWeatherServices(options =>
            {
                options.ApiKey = "1234567";
            });
            IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
            IWeatherService weatherService = serviceProvider.GetRequiredService<IWeatherService>();

            WeatherResponse weatherResponse = await weatherService.GetWeatherInfoAsync("Abbey", "CA").ConfigureAwait(false);
            Assert.IsTrue(weatherResponse.IsError);
            Assert.IsNull(weatherResponse.Data);
        }

        [TestMethod]
        public async Task Weather_Info_Should_Be_Error_When_Api_Key_Is_Not_Set()
        {
            ServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddOpenWeatherServices(options =>
            {
                options.ApiKey = null;
            });
            IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
            IWeatherService weatherService = serviceProvider.GetRequiredService<IWeatherService>();

            WeatherResponse weatherResponse = await weatherService.GetWeatherInfoAsync("Abbey", "CA").ConfigureAwait(false);
            Assert.IsTrue(weatherResponse.IsError);
            Assert.IsNull(weatherResponse.Data);
        }

        [TestMethod]
        public void Check_Conversion_Fahrenheit_To_Celcius_From_Dummy_Data()
        {
            WeatherInfo weatherInfo = FakeWeatherInfo.CreateDummyWeatherInfo();
            WeatherInfoDto weatherInfoDto = WeatherInfoDto.ConvertToDto(weatherInfo);
            decimal tempCelcius = (weatherInfo.Main.Temp - 32m) * 0.5556m;

            Assert.AreEqual(tempCelcius, weatherInfoDto.TempC);
        }

        [TestMethod]
        public async Task Check_Conversion_Fahrenheit_To_Celcius_From_Api_Response()
        {
            WeatherResponse weatherResponse = await _weatherService.GetWeatherInfoAsync("Abbey", "CA").ConfigureAwait(false);
            decimal tempCelcius = (weatherResponse.Data.TempF - 32m) * 0.5556m;

            Assert.AreEqual(tempCelcius, weatherResponse.Data.TempC);
        }
    }
}
